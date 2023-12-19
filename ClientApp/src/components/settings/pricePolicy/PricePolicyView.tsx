import * as React from 'react';
import { Theme, Button, Container } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../../store';
import { SignalRConnection } from '../../../transport/SignalRTransport';
import { makeStyles } from '@mui/styles';
import { ICarWashSettings } from '../../../store/CarWashSettings';
import { IPriceGroup } from '../../../store/PricePolicyStore';
import PriceGroupItem from './PriceGroupItem';
import { AddOrUpdatePriceGroupDialog } from './AddOrUpdatePriceGroupDialog';

const useStyles = makeStyles((theme: Theme) =>
  ({
    root: {
      width: '100%',
      maxWidth: 800,
      backgroundColor: theme.palette.background.paper,
    },
    title: {
      flexGrow: 1,
      textAlign: 'center',
    },
    paper: {
      padding: theme.spacing(1),
      textAlign: 'center',
      color: theme.palette.text.secondary,
      whiteSpace: 'nowrap',
      marginBottom: theme.spacing(1),
    },
    button: {
      margin: theme.spacing(1),
    },
  }),
);

export default function PricePolicyView() {
  const [isUpdateWashServiceOpened, setIsUpdateWashServiceOpened] = React.useState(false)
	const classes = useStyles();

  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);
  const allPriceGroup: IPriceGroup[] = useSelector((state: ApplicationState) => state.pricePolicyState !== undefined ? state.pricePolicyState.allPriceGroup : []);
  
  const handleUpdateWashServiceCancel = () => {
    setIsUpdateWashServiceOpened(false);
  };
  
  const handleUpdateWashServiceOk = (priceGroup: IPriceGroup) => {
    setIsUpdateWashServiceOpened(false);
    SignalRConnection.getInstance().send('add_price_group', priceGroup, carWashSettings.id);
  };
  
  const handleAddClick = () => {
    setIsUpdateWashServiceOpened(true);
  };

  return (
    <Container className={classes.root}>
			<Button variant="contained" color="primary" className={classes.button} onClick={handleAddClick} startIcon={<AddIcon />}>
				Добавить
			</Button>
      {allPriceGroup.map((priceGroup: IPriceGroup) => <PriceGroupItem key={priceGroup.id} priceGroup={priceGroup} /> )}
      
      { 
          isUpdateWashServiceOpened 
          ? <AddOrUpdatePriceGroupDialog
              onCancel={handleUpdateWashServiceCancel} 
              onOk={handleUpdateWashServiceOk} />
          : null
      }
    </Container>
    );
}
