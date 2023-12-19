import * as React from 'react';
import { ICarWashSettings } from '../../store/CarWashSettings';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../store';
import { SignalRConnection } from '../../transport/SignalRTransport';
import WorkScheduleView from './WorkScheduleView';
import { Grid, Container, Button } from '@mui/material';

export default function WorkScheduleTab() {
  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);
  
    const handleSaveClick = () => {
        // carWashSettings.schedule заполняется внутри WorkScheduleView
        SignalRConnection.getInstance().send('update_car_wash_settings_request', carWashSettings);
    };

  return (
      <Container style={{ margin: '40px 0px 0px 0px'}}>
        <Grid container spacing={3} direction="column">
          <Grid item xs={4}>
            <WorkScheduleView carWashSettings={carWashSettings} />
          </Grid>
          <Grid item xs={4}>
            <Button variant="contained" color="primary" onClick={handleSaveClick}>
              Сохранить
            </Button>
          </Grid>
        </Grid>
      </Container>
  );
}
