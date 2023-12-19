import { useDispatch, useSelector } from 'react-redux';
import { API_DATE_FORMAT } from '../../Logic/Constants';
import { ApplicationState } from '../../store';
import { ICarWashSettings } from '../../store/CarWashSettings';
import { frontActionCreators } from '../../store/FrontStore';
import { SignalRConnection } from '../../transport/SignalRTransport';
import { DaySelectionView } from '../Booking/DaySelectionView';

export interface ScheduleDaySelectionViewProps {
  selectedDate: moment.Moment;
}

export function ScheduleDaySelectionView(props: ScheduleDaySelectionViewProps) {
    const { selectedDate } = props;
    const dispatch = useDispatch();
    const carWashSettings: ICarWashSettings | undefined = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);

    const handleSelectedDateChanged = (date: moment.Moment) => {
      SignalRConnection.getInstance().send('get_records', carWashSettings.id, date.format(API_DATE_FORMAT));
      dispatch(frontActionCreators.scheduleDateChange(date));
    }

    return (
      <DaySelectionView selectedDateDefault={selectedDate} onChanged={handleSelectedDateChanged}/>
    );
}

