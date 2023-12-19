import * as React from 'react';
import { useState } from 'react';
import { ICarWashSettings } from '../../store/CarWashSettings';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../store';
import { SignalRConnection } from '../../transport/SignalRTransport';
import { Grid, TextField, FormControl, Container, Button } from '@mui/material';
import PhoneNumberControl from '../common/PhoneNumberControl';

export default function GeneralSettingsView() {
  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);

  const [carWashName, setCarWashName] = useState(carWashSettings.name);
  const [carWashLocation, setCarWashLocation] = useState(carWashSettings.location);
  const [phoneNumber, setPhoneNumber] = useState(carWashSettings.phone);
  const [boxesQuantity, setBoxesQuantity] = useState(carWashSettings.boxesQuantity);
  const [reservedMinutesBetweenRecords, setReservedMinutesBetweenRecords] = useState(carWashSettings.reservedMinutesBetweenRecords);

  const handleboxesQuantityClick = (e: any) => {
    let newBoxesQuantity = +e.target.value 
    if (newBoxesQuantity < 1){
      newBoxesQuantity = 1;
    }
    if (newBoxesQuantity > 20){
      newBoxesQuantity = 20
    }
    setBoxesQuantity(newBoxesQuantity)
  };

  const handleReservedMinutesClick = (e: any) => {
    let newValue = +e.target.value 
    if (newValue < 0){
      newValue = 0;
    }
    if (newValue > 1440){
      newValue = 1440
    }
    setReservedMinutesBetweenRecords(newValue)
  };

  const handleSaveClick = () => {
    carWashSettings.name = carWashName;
    carWashSettings.location = carWashLocation;
    carWashSettings.phone = phoneNumber;
    carWashSettings.boxesQuantity = boxesQuantity;
    carWashSettings.reservedMinutesBetweenRecords = reservedMinutesBetweenRecords;

    SignalRConnection.getInstance().send('update_car_wash_settings_request', carWashSettings);
  };

  return (
    <Container>
        <Grid container spacing={3} direction="column">
          <Grid item xs={4}>
            <TextField fullWidth label="Название" value={carWashName} variant="standard" onChange={(e) => setCarWashName(e.target.value)} />
          </Grid>
          <Grid item xs={4}>
            <TextField fullWidth label="Адрес" value={carWashLocation} variant="standard" onChange={(e) => setCarWashLocation(e.target.value)} />
          </Grid>
          
          <Grid item>
            <Grid container xs={12} sm={8} md={6} lg={3} spacing={3} direction="column">
              <Grid item>
                <PhoneNumberControl value={phoneNumber} onChange={setPhoneNumber} />
              </Grid>
              <Grid item>
                <TextField fullWidth label="Количество боксов" type="number" value={boxesQuantity} variant="standard" onChange={handleboxesQuantityClick}/>
              </Grid>
              <Grid item>
                <TextField fullWidth  label="Резерв времени между записями (мин)" type="number" value={reservedMinutesBetweenRecords} variant="standard" onChange={handleReservedMinutesClick}/>
              </Grid>
            </Grid>
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
