import * as React from 'react';
import { Grid, TextField, Theme } from '@mui/material';
import { makeStyles } from '@mui/styles';
import moment from 'moment';
import { IRecord } from '../../store/Records';

const useStyles = makeStyles((theme: Theme) =>
  ({
    selectedTimePoint: {
        background: theme.palette.action.selected,
    },
  }),
);

export interface TimeSelectionViewProps {
    record?: IRecord;
    timePoints?: string[];
    selectedTimePoint?: moment.Moment;
    onChanged: (selectedTime?: moment.Moment) => void;
}
  
export function TimeSelectionView(props: TimeSelectionViewProps) {
    const classes = useStyles();
    const { record, timePoints, selectedTimePoint, onChanged } = props;
    
    const [selectedTime, setSelectedTime] = React.useState<moment.Moment | undefined>(selectedTimePoint);
    
    const handleOnClick = (timePoint: moment.Moment) => {
        setSelectedTime(timePoint)
        onChanged(timePoint);
    };

    // Переводим iso-строки в moment
    const adjustedTimeSlotList = timePoints?.map(o => moment(o, "LT")) ?? [];

    // Если открывается существующая запись, то добавляем время этой записи в список таймслотов
    if (record?.startTime){
        const startTimeFromRecord = moment(record.startTime, "LT");
        if (adjustedTimeSlotList.every(o => !o.isSame(startTimeFromRecord))){
            adjustedTimeSlotList.push(startTimeFromRecord);
            adjustedTimeSlotList.sort();
        }
    }
    
    return (
        <Grid container spacing={2}>
            {adjustedTimeSlotList?.map((timePoint, index) => (
                <Grid key={index} item xs={1}>
                    <TextField 
                        type="button"
                        className={timePoint.isSame(selectedTime) ? classes.selectedTimePoint : undefined}
                        color="secondary"
                        size="small"
                        value={`${timePoint.format('HH.mm')}`}
                        onClick={() => handleOnClick(timePoint)} />
                </Grid>
            ))}
        </Grid>
    );
}
