import { useParams } from 'react-router';
import { BookingView } from './Booking/BookingView';
import { NoCarWashView } from './NoCarWashView';

export interface MainViewProps {
    carWashId: string;
  }

export default function MainView(props: MainViewProps) {
    const params = useParams<MainViewProps>();
    let id = parseInt(params.carWashId);
    if (isNaN(id))
        return (<NoCarWashView/>);
    return ( <BookingView selectedDay={new Date()} carWashId={id}/> );
}
