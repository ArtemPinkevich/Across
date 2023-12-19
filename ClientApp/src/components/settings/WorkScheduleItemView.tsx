import * as React from 'react';
import { Grid, TextField } from '@mui/material';
import TimePicker from '@mui/lab/TimePicker';
import { Container } from 'reactstrap';
import { Moment } from 'moment';
import moment from 'moment';

export interface IWorkScheduleItemProps {
    dayName: string;
    beginTime: string;
    endTime: string;
    onBeginTimeChnaged: (time: string) => void;
    onEndTimeChnaged: (time: string) => void;
  }
    
export default function WorkScheduleItemView(props: IWorkScheduleItemProps) {
  const { dayName, beginTime, endTime, onBeginTimeChnaged, onEndTimeChnaged } = props;
    
  const [begin, setBegin] = React.useState<Moment | null>(moment(beginTime, "LT"));
  const [end, setEnd] = React.useState<Moment | null>(moment(endTime, "LT"));

  const beginChanged = (newValue: Moment | null) => {
    setBegin(newValue);
    const likeTimeOnly = newValue?.format("LT")
    onBeginTimeChnaged(likeTimeOnly ?? '')
  };

  const endChanged = (newValue: Moment | null) => {
    setEnd(newValue);
    const likeTimeOnly = newValue?.format("LT")
    onEndTimeChnaged(likeTimeOnly ?? '');
  };
  
  return (
      <Container>
        <Grid container spacing={3}>
          <Grid item xs={4}>
            <TimePicker
              label={`${dayName}, c`}
              value={begin}
              onChange={beginChanged}
              renderInput={(params: any) => <TextField {...params} />}
            />
          </Grid>
          <Grid item xs={4}>
            <TimePicker
              label={`${dayName}, до`}
              value={end}
              onChange={endChanged}
              renderInput={(params: any) => <TextField {...params} />}
            />
          </Grid>
        </Grid>
      </Container>
  );
}
