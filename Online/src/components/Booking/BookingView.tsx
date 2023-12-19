import { 
  Button, TextField,
  Grid, Stack, Typography, Box } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { TimeSelectionView } from './TimeSelectionView';
import { ServicesSelectionView } from './ServicesSelectionView';
import { DaySelectionView } from './DaySelectionView';
import { useEffect, useState } from 'react';
import moment from 'moment';
import PhoneNumberControl from '../common/PhoneNumberControl';
import { IWashService, ICarInfo, IRecord, ICarWash } from '../../Models/AllModels';
import { BookingResult } from '../BookingResult';
import { API_DATE_FORMAT } from '../../Models/Constants';

export interface BookingViewProps {
  selectedDay: Date;
  carWashId: number;
}

export const useStyles = makeStyles((theme) => ({
    centredText: {
      textAlign: 'center',
    },
}));

export function BookingView(props: BookingViewProps) {
    const classes = useStyles();
    const { selectedDay } = props;
    
    const [carwash, setCarwash] = useState<ICarWash>();
    const [allWashServices, setAllWashServices] = useState<IWashService[]>([]);
    const [mainServiceId, setMainServiceId] = useState<number | undefined>();
    const [selectedServicesIds, setSelectedServicesIds] = useState<number[]>([]);
    const [durationMin, setDurationMin] = useState<number>(0);
    const [price, setPrice] = useState<number>(0);
    const [selectedDate, setSelectedDate] = useState(selectedDay); // ??
    const [timeSlots, setTimeSlots] = useState<string[]>([]);
    const [startTime, setStartTime] = useState<string | undefined>();
    const [phoneNumber, setPhoneNumber] = useState<string | undefined>();
    const [carNumber, setCarNumber] = useState<string>();
    const [carBrand, setCarBrand] = useState<string>();
    const [carModel, setCarModel] = useState<string>();
    const [isSuccessfulBooked, setIsSuccessfulBooked] = useState<boolean>();

    const carWashId = props.carWashId; // TODO достать айдишник
    
    useEffect(() => {
      var serverUrl = process.env.REACT_APP_SERVER_API_URL;
      
      fetch(`${serverUrl}/api/online/get_carwash_info/${carWashId}`)
          .then(res => res.json())
          .then(
              (result: ICarWash) => {
                setCarwash(result)
              },
              (error) => {
                  console.error(error);
              }
          );

      fetch(`${serverUrl}/api/online/get_services/${carWashId}`)
          .then(res => res.json())
          .then(
              (result: {allWashServices: IWashService[]}) => {
                  setAllWashServices(result.allWashServices)
              },
              (error) => {
                  console.error(error);
              }
          );
  
    }, []);
    
    useEffect(() => {
      recalcTimePoints();
    }, [durationMin, selectedDate]);
    
    const recalcTimePoints = () => {
      if (durationMin === 0){
        setTimeSlots([])
        return;
      }

      let dateTimeFrom = moment();
      // Если открывается запись из прошлого
      if (selectedDate < new Date()){
        // Если это НЕ сегодня, то очищаем тайм слоты и НЕ делаем запрос
        if (!moment(selectedDate).isSame(moment(), 'day')){
          setTimeSlots([])
          return;
        }
      }
      
      // Если открывается запись из будущего
      if (selectedDate > new Date()){
        // Если это НЕ сегодня, то сбрасываем время
        if (!moment(selectedDate).isSame(moment(), 'day')){
          dateTimeFrom = moment(selectedDate);
        }
      }
      
      var serverUrl = process.env.REACT_APP_SERVER_API_URL;
      fetch(`${serverUrl}/api/online/free_time_points_request/${carWashId}/${dateTimeFrom.format(API_DATE_FORMAT)}/${durationMin}`)
        .then(res => res.json())
        .then(
            (result) => {
              setTimeSlots(result.freeTimePoints)
            },
            (error) => {
                console.error(error);
            }
        );
    }
    
    const recalcDurationAndPrice = (selectedMainServiceId: number | undefined, selectedAdditionServicesIds: number[]) => {
      if (!selectedMainServiceId){
        setDurationMin(0);
        setPrice(0);
        return;
      }

      let summDuration = 0;
      let summPrice = 0;
      selectedAdditionServicesIds.forEach(id => {
        const washService = allWashServices.find(o => o.id === id);
        if (washService){
          summDuration += washService.duration;
          summPrice += washService.price;
        }
      });

      const mainWashService = allWashServices.find(o => o.id === selectedMainServiceId);
      if (mainWashService){
        summDuration += mainWashService.duration;
        summPrice += mainWashService.price;
      }

      setDurationMin(summDuration);
      setPrice(summPrice);
    }
    
    const handleMainServiceIdChanged = (washServiceId: number | undefined) => {
      setMainServiceId(washServiceId);
      recalcDurationAndPrice(washServiceId, []);
    };

    const handleAdditionServicesIdsChanged = (washServicesIds: number[]) => {
      setSelectedServicesIds(washServicesIds);
      recalcDurationAndPrice(mainServiceId, washServicesIds);
    };
    
    const handleSaveClick = () => {
      if (!mainServiceId){
        alert('Необходимо выбрать основную услугу')
        return
      }
      else if (!startTime) {
        alert('Необходимо выбрать время')
        return
      }
      else if (!phoneNumber) {
        alert('Необходимо указать номер телефона')
        return
      }
      
      const carInfo : ICarInfo = {
        id: undefined,  // ??
        regNumber: carNumber ?? '',
        mark: carBrand ?? '',
        model: carModel ?? '',
      }
      
      const recordDto : IRecord = {
        id: undefined,
        carWashId: carWashId,
        boxId: undefined,
        price: price,
        date: moment(selectedDate).format(API_DATE_FORMAT),
        startTime: moment(startTime).format("LT"),
        durationMin: durationMin,
        mainServiceId: mainServiceId,
        additionServicesIds: selectedServicesIds,
        carInfo: carInfo,
        phoneNumber: `+7${phoneNumber}`
      }

      
      var request = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(recordDto)
      }
      var serverUrl = process.env.REACT_APP_SERVER_API_URL;
      fetch(`${serverUrl}/api/online/update_record_request`, request)
        .then(res => res.json())
        .then(
            (result) => {
              if (result){
                setIsSuccessfulBooked(true)
              }
              else{
                setIsSuccessfulBooked(false)
              }
            },
            (error) => {
              setIsSuccessfulBooked(false)
            }
        )
        .catch(
          (error) => {
            setIsSuccessfulBooked(false)
          })
    };

    if (isSuccessfulBooked !== undefined){
      return (<BookingResult isSuccessful={isSuccessfulBooked}/>)
    }
    
    return (
      <Grid container spacing={3} direction="column">
        <Grid item xs={12}>
          <div style={{ flexGrow: 1, textAlign: 'center'}}>
            <Typography variant="h4" my={3}>Онлайн бронирование</Typography>
            <Typography variant="h6" >Автомойка {carwash?.name}</Typography>
          </div>
        </Grid>
        
        <Grid item xs={4}>
          <ServicesSelectionView 
            allWashServices={allWashServices}
            onMainServiceChanged={handleMainServiceIdChanged}
            onAdditionServicesChanged={handleAdditionServicesIdsChanged}/>
        </Grid>

        <Grid item xs={6}>
          <Stack direction="row" justifyContent="flex-end" alignItems="center" spacing={2}>
            <TextField label="Длительность (мин.)" variant="filled" value={durationMin} onChange={e => setDurationMin(+e.target.value)} inputProps={{ readOnly: true, }}/>
            <TextField label="Сумма" variant="filled" value={price} onChange={(e) => {setPrice(+e.target.value)}} inputProps={{ readOnly: true, }}/>
          </Stack>
        </Grid>
        
        <Grid item xs={12} mt={3}>
          <div style={{ flexGrow: 1, textAlign: 'center'}}>
            <DaySelectionView selectedDateDefault={selectedDate} blockPast onChanged={day => setSelectedDate(day)}/>
          </div>
        </Grid>

        <Grid item>
          <TimeSelectionView selectedTimePoint={startTime} timePoints={timeSlots} onChanged={o => setStartTime(o)} />
        </Grid>

        <Grid item xs={12}>
          <PhoneNumberControl value={phoneNumber} onChange={(value) => setPhoneNumber(value)} />
        </Grid>

        <Grid item xs={12}>
          <Stack direction={{  xs: 'column', sm: 'column', md: 'column', lg: 'row'}} spacing={3}>
            <TextField
              id="carNumber" 
              label="Гос. номер" 
              variant="standard"
              value={carNumber}
              autoComplete="off"
              onChange={(e) => {setCarNumber(e.target.value)}}
            />
            <TextField
              id="carBrand" 
              label="Марка авто" 
              variant="standard"
              value={carBrand}
              onChange={(e) => {setCarBrand(e.target.value)}}
            />
            <TextField
              id="carModel" 
              label="Модель авто" 
              variant="standard"
              value={carModel}
              onChange={(e) => {setCarModel(e.target.value)}}
            />
          </Stack>
        </Grid>

        <Grid item xs={6} my={3}>
          <Box display="flex" justifyContent="flex-end">
            <Button variant="contained" color="success" size="large" onClick={handleSaveClick}>Записаться</Button>
          </Box>
        </Grid>
        
      </Grid>
    );
  }
