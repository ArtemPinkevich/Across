import * as React from 'react';
import { ICarWashSettings } from '../../store/CarWashSettings';
import WorkScheduleItemView from './WorkScheduleItemView';
import { Container, Grid } from '@mui/material';

export interface IWorkScheduleProps {
    carWashSettings: ICarWashSettings;
  }
    
export default function WorkScheduleView(props: IWorkScheduleProps) {
    const { carWashSettings } = props;
  
    if (!carWashSettings.workTime){
        carWashSettings.workTime = {
            mondayBegin : '',
            mondayEnd: '',
            tuesdayBegin : '',
            tuesdayEnd: '',
            wednesdayBegin : '',
            wednesdayEnd: '',
            thursdayBegin : '',
            thursdayEnd: '',
            fridayBegin : '',
            fridayEnd: '',
            saturdayBegin : '',
            saturdayEnd: '',
            sundayBegin : '',
            sundayEnd: '',
        };
    }

    return (
        <Container>
            <Grid container spacing={3} direction="column">
                
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Понедельник" 
                        beginTime={carWashSettings.workTime.mondayBegin} 
                        endTime={carWashSettings.workTime.mondayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.mondayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.mondayEnd = o} />
                </Grid>
                
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Вторник" 
                        beginTime={carWashSettings.workTime.tuesdayBegin} 
                        endTime={carWashSettings.workTime.tuesdayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.tuesdayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.tuesdayEnd = o} />
                </Grid>
                    
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Среда" 
                        beginTime={carWashSettings.workTime.wednesdayBegin} 
                        endTime={carWashSettings.workTime.wednesdayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.wednesdayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.wednesdayEnd = o} />
                </Grid>
                        
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Четверг" 
                        beginTime={carWashSettings.workTime.thursdayBegin} 
                        endTime={carWashSettings.workTime.thursdayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.thursdayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.thursdayEnd = o} />
                </Grid>
                        
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Пятница" 
                        beginTime={carWashSettings.workTime.fridayBegin} 
                        endTime={carWashSettings.workTime.fridayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.fridayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.fridayEnd = o} />
                </Grid>
                        
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Суббота" 
                        beginTime={carWashSettings.workTime.saturdayBegin} 
                        endTime={carWashSettings.workTime.saturdayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.saturdayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.saturdayEnd = o} />
                </Grid>
                        
                <Grid item xs={4}>
                    <WorkScheduleItemView 
                        dayName="Воскресенье" 
                        beginTime={carWashSettings.workTime.sundayBegin} 
                        endTime={carWashSettings.workTime.sundayEnd}
                        onBeginTimeChnaged={(o) => carWashSettings.workTime.sundayBegin = o}
                        onEndTimeChnaged={(o) => carWashSettings.workTime.sundayEnd = o} />
                </Grid>
            </Grid>
        </Container>
    );
}
