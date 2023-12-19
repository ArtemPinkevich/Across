import * as React from 'react';
import { Theme, Typography, List, ListItem, ListItemText, Container } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { useEffect, useState } from 'react';
import { useHistory } from "react-router-dom";
import { ICarWashSettings } from '../store/CarWashSettings';
import { SignalRConnection } from '../transport/SignalRTransport';
import * as CarWashStore from '../store/CarWashSettings';
import * as CarStore from '../store/CarStore';
import { useDispatch } from 'react-redux';
import { ICarBody } from '../store/PricePolicyStore';

const useStyles = makeStyles((theme: Theme) =>
  ({
    root: {
      width: '100%',
      maxWidth: 800,
      backgroundColor: theme.palette.background.paper,
    },
  }),
);

export default function ChooseCarWash() {
	const classes = useStyles();
  const [carWashList, setCarWashList] = useState([]);
  const history = useHistory();
  const dispatch = useDispatch();
    
  useEffect(() => {
    SignalRConnection.getInstance().invoke('car_washes_by_user_request').then(result =>
      {
        const carWashList = result.carWashDtos;
        if (carWashList.length === 1){
          applyCarWash(carWashList[0].id)
          return;
        }
        
        setCarWashList(carWashList);
      }).catch((reason) => { console.error(reason) });
  }, []);

  
  const handleToggleWashServiceClick = (carWashSettings: ICarWashSettings) => {
    applyCarWash(carWashSettings.id)
  };

  
  const applyCarWash = (carWashId: number) => {
    SignalRConnection.getInstance().send('get_services', carWashId);
    SignalRConnection.getInstance().send('get_price_groups', carWashId);
    SignalRConnection.getInstance().invoke('get_car_bodies').then((carBodies: ICarBody[]) => {
      dispatch(CarStore.actionCreators.setCarBodies(carBodies))
    });
    SignalRConnection.getInstance().invoke('car_wash_settings_request', carWashId).then(carWashSettings => {
      dispatch(CarWashStore.actionCreators.setCarWashSettings(carWashSettings))
      history.push("/schedule")
    });
  };
  
  return (
		<Container className={classes.root}>
			<Typography variant="h6">Выбор автомойки</Typography>
			<List>
				{carWashList.map((carWashSettings: ICarWashSettings) => 
          <ListItem key={carWashSettings.id} role={undefined} dense button onClick={() => handleToggleWashServiceClick(carWashSettings)}>
            <ListItemText primary={carWashSettings.name} />
            <ListItemText primary={carWashSettings.location} />
          </ListItem>
        )}
			</List>
		</Container>
  );
}
