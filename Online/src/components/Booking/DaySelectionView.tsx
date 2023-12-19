import * as React from 'react';
import { Grid, IconButton, Theme, Typography } from '@mui/material';
import { makeStyles } from '@mui/styles';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import moment from 'moment';

const useStyles = makeStyles((theme: Theme) =>
  ({
    centredText: {
      textAlign: 'center',
    }
  }),
);

export interface DaySelectionViewProps {
  blockPast?: boolean;
  onChanged: (date: Date) => void;
  selectedDateDefault: Date;
}

export function DaySelectionView(props: DaySelectionViewProps) {
    const classes = useStyles();
    const { selectedDateDefault, blockPast, onChanged } = props;
    
    const [selectedDate, setSelectedDate] = React.useState(moment(selectedDateDefault));

    const handleNextPrevDayClick = (addingDayCount: number) => {
      const newDate = selectedDate.add(addingDayCount, 'days').clone();
      setSelectedDate(newDate);
      onChanged(newDate.toDate())
    }

    return (
        <Grid container direction="row" justifyContent="center" alignItems="center" spacing={4}>
            <Grid item >
                <IconButton disabled={blockPast && selectedDate.isSame(new Date(), "day")} size="large" color="inherit" onClick={() => handleNextPrevDayClick(-1)}>
                    <ArrowBackIosNewIcon fontSize="medium"/>
                </IconButton>
            </Grid>
          
            <Grid item>
                <Typography variant="h4" className={classes.centredText}>{moment(selectedDate).format('DD MMMM')}</Typography>
                <Typography variant="body1" className={classes.centredText}>{moment(selectedDate).format('dddd')}</Typography>
            </Grid>
          
            <Grid item >
                <IconButton size="large" color="inherit" onClick={() => handleNextPrevDayClick(1)}>
                    <ArrowForwardIosIcon fontSize="medium"/>
                </IconButton>
            </Grid>
        </Grid>
    );
}
