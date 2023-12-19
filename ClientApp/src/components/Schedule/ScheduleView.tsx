import * as React from 'react';
import { Grid, Paper, Theme } from '@mui/material';
import { makeStyles } from '@mui/styles'
import ScheduleNavMenu from './ScheduleNavMenu';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../store';
import { BookingView } from '../Booking/BookingView';
import {Calendar, momentLocalizer} from 'react-big-calendar';
import { ICarWashSettings } from '../../store/CarWashSettings';
//стили для календаря, иначе какая-то жижа выходит
import "react-big-calendar/lib/css/react-big-calendar.css";

//Необходим для работы Calendar из react-big-calendar
import moment from 'moment';
//Импорт языка для отображения, а так же формата записи (вместо AM и PM привычный для нас вид от 00:00 до 23:00)
import 'moment/locale/ru';

//Тестовые ивенты для отображения в календаре
//import events from './events'
import { IRecord } from '../../store/Records';
import { useEffect } from 'react';
import { SignalRConnection } from '../../transport/SignalRTransport';
import { API_DATE_FORMAT } from '../../Logic/Constants';

const useStyles = makeStyles((theme: Theme) =>
  ({
    root: {
      flexGrow: 1,
    },
    paper: {
      padding: theme.spacing(1),
      textAlign: 'center',
      color: theme.palette.text.secondary,
    },
  }),
);

export default function ScheduleView() {
  const classes = useStyles();

  const date: moment.Moment = useSelector((state: ApplicationState) => state.frontStore?.selectedSchaduleDate ?? moment());

  const [selectedRecord, setSelectedRecord] = React.useState<IRecord | undefined>(undefined)
  const [isBookingWindowOpened, setIsBookingWindowOpened] = React.useState(false)

  //Необходимо для использования Calendar из react-big-calendar
  let Moment = moment;
  Moment.locale('ru');
  const localizer = momentLocalizer(Moment)

  const records: IRecord[] = useSelector((state: ApplicationState) => state.records?.records ?? []);
  const carWashSettings: ICarWashSettings | undefined = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);
  const boxesCount = carWashSettings.boxesQuantity;
  
  useEffect(() => {
    SignalRConnection.getInstance().send('get_records', carWashSettings.id, date.format(API_DATE_FORMAT));
  }, []);

  const getBoxEvents = (records: IRecord[], boxNumber: number) => {
    const events = records.filter(e => e.boxId === boxNumber && e.startTime !== undefined).map(o => {
      const startDateTimeMoment = moment(o.date, API_DATE_FORMAT);
      const onlyTimeMoment = moment(o.startTime, "LT");
      startDateTimeMoment.set('hour', onlyTimeMoment.hour())
      startDateTimeMoment.set('minute', onlyTimeMoment.minute())
      
      const startDate = startDateTimeMoment.toDate();
      const endDate = new Date(new Date(startDate).setMinutes(startDate.getMinutes() + o.durationMin));
      return {
          id: o.id,
          title: `${o.carInfo?.regNumber} ${o.carInfo?.mark} ${o.carInfo?.model}`,
          start: startDate,
          end: endDate,
    }});

    return events;
  }

  const handleOnDoubleClickEvent = (arg: any) => {
    const selectedRecord = records.find(o => o.id === arg.id);
    setSelectedRecord(selectedRecord);
    setIsBookingWindowOpened(true)
  };
  
  // Ширина выставляется равномерно для всех боксов
  let boxes = [];
  for (let i = 1; i < boxesCount + 1; i++) {
    boxes.push(
      <Grid key={i} item xs>
          <Paper className={classes.paper}>{`Бокс ${i}`}</Paper>
          <Calendar
            date={date.toDate()}
            onNavigate={() => {}}  // Заглушка чтобы не сыпался ворнинг, который говорит мол рефрешь эвентов нужно делать по этому событию. 
                                  //Но нам незачем так делать, так как календарей много, а список записей один и он обновляется при смене даты т.е. в ScheduleDaySelectionView
            localizer={localizer}
            events={getBoxEvents(records, i)}
            defaultView={'day'}
            toolbar={false}
            onDoubleClickEvent={handleOnDoubleClickEvent}
          />
        </Grid>
    )
  }
  
  return (
    <React.Fragment>
      <ScheduleNavMenu />
      <div className={classes.root}>
        <Grid container spacing={0}>
          {boxes}
        </Grid>
      </div>
      {isBookingWindowOpened ? <BookingView record={selectedRecord} open={true} onClose={() => setIsBookingWindowOpened(false)} selectedDay={moment()}/> : null}
    </React.Fragment>
  );
}
