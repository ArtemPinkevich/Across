import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Grid,
  Stack,
  PaperProps,
  Paper,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Typography,
} from '@mui/material';
import { makeStyles } from '@mui/styles';
import { ICarInfo, IRecord } from '../../store/Records';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../store';
import { ICarWashSettings } from '../../store/CarWashSettings';
import { SignalRConnection } from '../../transport/SignalRTransport';
import { TimeSelectionView } from './TimeSelectionView';
import { ServicesSelectionView } from './ServicesSelectionView';
import { DaySelectionView } from './DaySelectionView';
import { IWashService } from '../../store/WashServices';
import { useEffect, useState } from 'react';
import moment from 'moment';
import Draggable from 'react-draggable';
import PhoneNumberControl from '../common/PhoneNumberControl';
import { API_DATE_FORMAT } from '../../Logic/Constants';
import { ICarBody, IPriceGroup } from '../../store/PricePolicyStore';

function PaperComponent(props: PaperProps) {
  return (
    <Draggable handle='#draggable-dialog-title' cancel={'[class*="MuiDialogContent-root"]'}>
      <Paper {...props} />
    </Draggable>
  );
}

export interface BookingViewProps {
  record?: IRecord;
  open: boolean;
  onClose: () => void;
  selectedDay: moment.Moment;
}

export const useStyles = makeStyles((theme) => ({
  centredText: {
    textAlign: 'center',
  },
}));

export function BookingView(props: BookingViewProps) {
  const classes = useStyles();
  const { record, selectedDay, open, onClose } = props;

  // const fakeTimeSlots = [
  //   '2021-10-30T13:50:00.0000000',
  //   '2021-10-30T14:00:00.0000000',
  //   '2021-10-30T14:10:00.0000000',
  //   '2021-10-30T14:20:00.0000000',
  //   '2021-10-30T14:30:00.0000000',
  //   '2021-10-30T14:40:00.0000000',
  //   '2021-10-30T14:50:00.0000000',
  //   '2021-10-30T15:00:00.0000000',
  //   '2021-10-30T15:10:00.0000000',
  //   '2021-10-30T15:20:00.0000000',
  //   '2021-10-30T15:30:00.0000000',
  //   '2021-10-30T15:40:00.0000000',
  //   '2021-10-30T15:50:00.0000000',
  //   '2021-10-30T16:00:00.0000000',
  //   '2021-10-30T16:10:00.0000000'
  // ];
  
  const [mainServiceId, setMainServiceId] = useState<number | undefined>(record?.mainServiceId);
  const [selectedServicesIds, setSelectedServicesIds] = useState<number[]>(record?.additionServicesIds ?? []);
  const [durationMin, setDurationMin] = useState<number>(record?.durationMin ?? 0);
  const [price, setPrice] = useState<number>(record?.price ?? 0);
  const [selectedDate, setSelectedDate] = useState(record?.date ? moment(record.date, API_DATE_FORMAT) : selectedDay);
  const [timeSlots, setTimeSlots] = useState<string[]>([]);
  const [startTime, setStartTime] = useState<moment.Moment | undefined>(record ? moment(record.startTime, 'LT') : undefined);
  const [availableBoxes, setAvailableBoxes] = useState<number[]>([]);
  const [boxId, setBoxId] = useState<number | undefined>(record?.boxId);
  const [phoneNumber, setPhoneNumber] = useState(record?.phoneNumber);
  const [carNumber, setCarNumber] = useState<string>(record?.carInfo?.regNumber ?? '');
  const [carBrand, setCarBrand] = useState<string>(record?.carInfo?.mark ?? '');
  const [carModel, setCarModel] = useState<string>(record?.carInfo?.model ?? '');
  const [carBodyId, setCarBodyId] = useState<number | undefined>(record?.carInfo?.carBodyId);

  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);
  const allWashServices: IWashService[] = useSelector((state: ApplicationState) =>
    state.washServices !== undefined ? state.washServices.allWashServices : []
  );
  const records: IRecord[] = useSelector((state: ApplicationState) => state.records?.records ?? []);
  const allPriceGroups: IPriceGroup[] = useSelector((state: ApplicationState) => state.pricePolicyState !== undefined ? state.pricePolicyState.allPriceGroup : []);

  useEffect(() => {
    recalcTimePoints();
  }, [durationMin, selectedDate]);

  useEffect(() => {
    recalcAvailableBoxes();
  }, [startTime]);

  useEffect(() => {
    if (availableBoxes.length > 0) {
      if (!boxId || !availableBoxes.includes(boxId)) {
        setBoxId(availableBoxes[0]);
      }
    }
  }, [availableBoxes]);

  const recalcTimePoints = () => {
    if (durationMin === 0) {
      setTimeSlots([]);
      return;
    }

    let dateTimeFrom = moment();
    // Если открывается запись из прошлого
    if (selectedDate < moment()) {
      // Если это НЕ сегодня, то очищаем тайм слоты и НЕ делаем запрос
      if (!selectedDate.isSame(moment(), 'day')) {
        setTimeSlots([]);
        return;
      }
    }

    // Если открывается запись из будущего
    if (selectedDate > moment()) {
      // Если это НЕ сегодня, то сбрасываем время
      if (!selectedDate.isSame(moment(), 'day')) {
        dateTimeFrom = selectedDate;
      }
    }

    SignalRConnection.getInstance()
      .invoke('free_time_points_request', carWashSettings.id, selectedDate.format(API_DATE_FORMAT), durationMin)
      .then((res) => setTimeSlots(res.freeTimePoints))
      .catch((reason) => {
        console.error(reason);
      });
  };

  const hasBoxFreeTimeSlot = (boxId: number, day: moment.Moment, startTime: moment.Moment, duration: number) => {
    const boxRecords = records.filter((o) => o.boxId == boxId && moment(o.date, API_DATE_FORMAT).isSame(day, 'day'));

    for (const curRecord of boxRecords) {
      const recordStart = moment(curRecord.startTime, 'LT');
      const recordEnd = recordStart.clone().add(curRecord.durationMin, 'minute');
      const candidateStart = startTime;
      const candidateEnd = candidateStart.clone().add(durationMin, 'minute');

      if (recordStart >= candidateStart && recordEnd < candidateEnd) return false;

      if (recordEnd > candidateStart && recordEnd <= candidateEnd) return false;

      if (recordStart <= candidateStart && recordEnd >= candidateEnd) return false;
    }

    return true;
  };

  const recalcAvailableBoxes = () => {
    if (!startTime) {
      return;
    }

    const availableBoxes = [];
    for (let i = 1; i < carWashSettings.boxesQuantity + 1; i++) {
      if (hasBoxFreeTimeSlot(i, selectedDate, startTime, durationMin)) {
        availableBoxes.push(i);
      }
    }

    if (startTime.format('LT') === record?.startTime && record?.boxId) {
      if (!availableBoxes.includes(record.boxId)) {
        availableBoxes.push(record.boxId);
        availableBoxes.sort();
      }
    }

    setAvailableBoxes(availableBoxes);
  };

  const recalcDurationAndPrice = (selectedMainServiceId: number | undefined, selectedAdditionServicesIds: number[], carBodyId: number | undefined) => {
    if (!carBodyId || !selectedMainServiceId) {
      setDurationMin(0);
      setPrice(0);
      return;
    }

    const priceGroup = allPriceGroups.find(o => o.carBodies.find(c => c.carBodyId === carBodyId))
    if (!priceGroup){
      setDurationMin(0);
      setPrice(0);
      return;
    }

    let summDuration = 0;
    let summPrice = 0;
    selectedAdditionServicesIds.forEach((id) => {
      const washService = allWashServices.find((o) => o.id === id);
      if (washService) {
        const washServiceSettings = washService.washServiceSettingsDtos.find(o => o.priceGroupId === priceGroup.id)
        if (washServiceSettings){
          summDuration += washServiceSettings?.duration ?? 0;
          summPrice += washServiceSettings?.price ?? 0;
        }
      }
    });

    const mainWashService = allWashServices.find((o) => o.id === selectedMainServiceId);
    if (mainWashService) {
      const washServiceSettings = mainWashService.washServiceSettingsDtos.find(o => o.priceGroupId === priceGroup.id)
      if (washServiceSettings){
        summDuration += washServiceSettings.duration ?? 0;
        summPrice += washServiceSettings.price ?? 0;
      }
    }

    setDurationMin(summDuration);
    setPrice(summPrice);
  };
  
  const handleChanged = (carBody: ICarBody | undefined, mainWashServiceId: number | undefined, additionWashServicesIds: number[]) => {
    setCarBodyId(carBody?.carBodyId);
    setMainServiceId(mainWashServiceId);
    setSelectedServicesIds(additionWashServicesIds);
    recalcDurationAndPrice(mainWashServiceId, additionWashServicesIds, carBody?.carBodyId);
  };

  const handleSaveClick = () => {
    const carInfo: ICarInfo = {
      id: props.record?.id,
      regNumber: carNumber ?? '',
      mark: carBrand ?? '',
      model: carModel ?? '',
      carBodyId: carBodyId
    };

    const recordDto: IRecord = {
      id: record?.id,
      carWashId: carWashSettings.id,
      boxId: boxId,
      price: price,
      date: selectedDate.format(API_DATE_FORMAT),
      startTime: moment(startTime).format('LT'),
      durationMin: durationMin,
      mainServiceId: mainServiceId,
      additionServicesIds: selectedServicesIds,
      carInfo: carInfo,
      phoneNumber: phoneNumber,
    };

    SignalRConnection.getInstance().send('update_record_request', recordDto);

    onClose();
  };

  const handleDiscardClick = () => {
    if (record) {
      SignalRConnection.getInstance().send('discard_record_request', record.id);
    }
    onClose();
  };

  const handleClose = () => {
    onClose();
  };

  return (
    <Dialog onClose={handleClose} open={open} fullWidth={true} maxWidth='md' PaperComponent={PaperComponent} aria-labelledby='draggable-dialog-title'>
      {/* HACK. div добавлен для избежания ворнинга типа "validateDOMNesting(...): <h4> cannot appear as a child of <h2></h2>" */}
      <DialogTitle style={{ cursor: 'move' }}>
        <div>
          <Typography variant='h4' className={classes.centredText}>
            Информация о записи
          </Typography>
        </div>
      </DialogTitle>

      <DialogContent>
        <Grid container spacing={3} direction='column'>
          <Grid item xs={4}>
            <ServicesSelectionView
              mainServiceId={record?.mainServiceId}
              additionServicesIds={record?.additionServicesIds}
              carBodyId={carBodyId}
              onChanged={handleChanged}
            />
          </Grid>

          <Grid item xs={6}>
            <Stack direction='row' justifyContent='flex-end' alignItems='center' spacing={2}>
              <TextField label='Длительность (мин.)' variant='filled' value={durationMin} onChange={(e) => setDurationMin(+e.target.value)} />
              <TextField
                label='Сумма'
                variant='filled'
                value={price}
                onChange={(e) => {
                  setPrice(+e.target.value);
                }}
              />
            </Stack>
          </Grid>

          <Grid item xs={12}>
            <div style={{ flexGrow: 1, textAlign: 'center' }}>
              <DaySelectionView selectedDateDefault={selectedDate} blockPast onChanged={(day) => setSelectedDate(day)} />
            </div>
          </Grid>

          <Grid item>
            <TimeSelectionView record={record} selectedTimePoint={startTime} timePoints={timeSlots} onChanged={(o) => setStartTime(o)} />
          </Grid>

          <Grid item>
            {/* Не знаю почему, но только таким образом получилось ограничить размер Select-а по сетке */}
            {/* кажется это из-за того, что родительский контейнер direction="column" */}
            <Grid container xs={6} sm={4} md={3} lg={3} spacing={4} direction="column">
              <Grid item>
                <FormControl fullWidth={true} variant='standard'>
                  <InputLabel id='box'>Бокс</InputLabel>
                  <Select labelId='box' value={boxId ?? ''} onChange={(e) => setBoxId(+e.target.value)}>
                    {availableBoxes.map((box) => (
                      <MenuItem key={box} value={box}>
                        {box}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
              <Grid item>
                <PhoneNumberControl value={phoneNumber} onChange={(value) => setPhoneNumber(value)} />
              </Grid>
            </Grid>
          </Grid>

          <Grid item xs={12}>
            <Stack direction='row' spacing={10}>
              <TextField
                id='carNumber'
                label='Гос. номер'
                variant='standard'
                value={carNumber}
                autoComplete='off'
                onChange={(e) => {
                  setCarNumber(e.target.value);
                }}
              />
              <TextField
                id='carBrand'
                label='Марка авто'
                variant='standard'
                value={carBrand}
                onChange={(e) => {
                  setCarBrand(e.target.value);
                }}
              />
              <TextField
                id='carModel'
                label='Модель авто'
                variant='standard'
                value={carModel}
                onChange={(e) => {
                  setCarModel(e.target.value);
                }}
              />
            </Stack>
          </Grid>
        </Grid>
      </DialogContent>

      <DialogActions>
        {record ? (
          <Button variant='contained' color='warning' onClick={handleDiscardClick}>
            Отменить запись
          </Button>
        ) : null}
        <Button variant='contained' color='success' onClick={handleSaveClick}>
          Сохранить
        </Button>
        <Button variant='outlined' onClick={handleClose} color='primary'>
          Закрыть
        </Button>
      </DialogActions>
    </Dialog>
  );
}
