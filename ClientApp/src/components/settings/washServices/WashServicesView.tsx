import * as React from 'react';
import {
  Typography,
  Theme,
  List,
  ListItem,
  ListItemIcon,
  IconButton,
  ListItemSecondaryAction,
  ListItemText,
  Switch,
  Button,
  Container,
  Menu,
  MenuItem,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Paper,
  Stack,
  Select,
  FormControl,
  InputLabel,
  Grid,
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../../store';
import { IWashService } from '../../../store/WashServices';
import { SignalRConnection } from '../../../transport/SignalRTransport';
import { AddOrUpdateWashServiceDialog } from './AddOrUpdateWashServiceDialog';
import { makeStyles } from '@mui/styles';
import { ICarWashSettings } from '../../../store/CarWashSettings';
import { IPriceGroup } from '../../../store/PricePolicyStore';

const useStyles = makeStyles((theme: Theme) => ({
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
}));

export default function WashServicesView() {
  const [isUpdateWashServiceOpened, setIsUpdateWashServiceOpened] = React.useState(false);
  const classes = useStyles();
  const [isMenuOpened, setIsMenuOpened] = React.useState<null | HTMLElement>(null);
  const [isNotificationDialogOpen, setisNotificationDialogOpen] = React.useState<boolean>(false);
  const [selectedPriceGroupId, setSelectedPriceGroup] = React.useState<number>(-1);

  // Почему-то все менюшки ссылаются на последний элемент списка. Поэтому приходится проставлять currentMenuItem в момент открытия меню.
  const [currentMenuItem, setCurrentMenuItem] = React.useState<undefined | IWashService>(undefined);

  const pricePolicyList: IPriceGroup[] = useSelector((state: ApplicationState) => state.pricePolicyState!.allPriceGroup);
  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);
  const allWashServices: IWashService[] = useSelector((state: ApplicationState) =>
    state.washServices !== undefined ? state.washServices.allWashServices : []
  );
  const baseWashServices = allWashServices.filter((o) => o.composition === undefined || o.composition === null || o.composition.length === 0);
  const compositeWashServices = allWashServices.filter((o) => o.composition !== undefined && o.composition !== null && o.composition.length > 0);

  const handleUpdateWashServiceCancel = () => {
    setIsMenuOpened(null);
    setIsUpdateWashServiceOpened(false);
  };

  const handleUpdateWashServiceOk = (washService: IWashService) => {
    setIsMenuOpened(null);
    setIsUpdateWashServiceOpened(false);
    if (washService.id) {
      SignalRConnection.getInstance().send('update_wash_service', washService, carWashSettings.id);
    } else {
      SignalRConnection.getInstance().send('add_wash_service', washService, carWashSettings.id);
    }
  };

  const handleToggleWashServiceClick = (washService: IWashService) => {
    washService.enabled = !washService.enabled;
    SignalRConnection.getInstance().send('update_wash_service', washService, carWashSettings.id);
  };

  const handleMenuClick = (event: React.MouseEvent<HTMLButtonElement>, washService: IWashService) => {
    setIsMenuOpened(event.currentTarget);
    setCurrentMenuItem(washService);
  };

  const handleMenuClose = () => {
    setIsMenuOpened(null);
  };

  const handleDetailsClick = () => {
    setIsUpdateWashServiceOpened(true);
  };

  const handleAddClick = () => {
    setCurrentMenuItem(undefined);
    setIsUpdateWashServiceOpened(true);
  };

  const handleRemoveClick = () => {
    if (currentMenuItem !== undefined && currentMenuItem !== null) {
      SignalRConnection.getInstance().send('remove_service', currentMenuItem.id);
    }
    setIsMenuOpened(null);
  };

  function getWashServicesItem(washService: IWashService) {
    if (selectedPriceGroupId !== -1 && !washService.washServiceSettingsDtos.find(o => o.priceGroupId === selectedPriceGroupId)?.enabled){
      return
    }

    return (
      <ListItem key={washService.id} role={undefined} dense button>
        <ListItemIcon>
          <Switch
            edge='end'
            onChange={() => handleToggleWashServiceClick(washService)}
            checked={washService.enabled}
            inputProps={{ 'aria-labelledby': 'switch-list-label-bluetooth' }}
          />
        </ListItemIcon>
        <ListItemText primary={washService.name} style={{ width: '300px' }} />

        {/*Надо добавить другое отображение элементов*/}
        {/* <ListItemText primary={`${washService.price}\u20bd`} />
        <ListItemText primary={`${washService.duration} мин`} /> */}
        <ListItemSecondaryAction>
          <IconButton
            aria-label='Действия'
            aria-controls={`services-item-menu`}
            aria-haspopup='true'
            onClick={(event) => handleMenuClick(event, washService)}
          >
            <MoreVertIcon />
          </IconButton>
          <Menu id={`services-item-menu`} anchorEl={isMenuOpened} open={Boolean(isMenuOpened) && currentMenuItem?.id === washService.id} onClose={handleMenuClose}>
            <MenuItem onClick={handleDetailsClick}>Подробнее</MenuItem>
            <MenuItem onClick={handleRemoveClick}>Удалить</MenuItem>
          </Menu>
        </ListItemSecondaryAction>
      </ListItem>
    );
  }
  
  return (
    <Container className={classes.root}>
      <Stack spacing={2}>
        <Paper sx={{ p: 2 }}> 
          <Grid container justifyContent="space-between" alignItems="center">
            <Grid item>
              <Button
                variant='contained'
                color='primary'
                className={classes.button}
                onClick={pricePolicyList.length !== 0 ? handleAddClick : () => setisNotificationDialogOpen(true)}
                startIcon={<AddIcon />}
              >
                Добавить услугу
              </Button>
            </Grid>
            <Grid item>
              <Grid container justifyContent="flex-end">
                <FormControl size="small" sx={{ minWidth: 200 }}>
                  <InputLabel>Фильтр по ценовым группам</InputLabel>
                  <Select
                    value={selectedPriceGroupId}
                    onChange={(e) => setSelectedPriceGroup(+e.target.value)}
                  >
                    <MenuItem key={-1} value={-1}>Все группы</MenuItem>
                    {pricePolicyList.map((priceGroup: IPriceGroup) => <MenuItem key={priceGroup.id} value={priceGroup.id}>{priceGroup.name}</MenuItem>)}
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
          </Grid>

        </Paper>
        
        <Paper sx={{ p: 2 }}>
          <Typography variant='h6'>Базовые услуги</Typography>
          <List>{baseWashServices.map((washService: IWashService) => getWashServicesItem(washService))}</List>
        </Paper>
        
        <Paper sx={{ p: 2 }}>
          <Typography variant='h6'>Комплексные услуги</Typography>
          <List>{compositeWashServices.map((washService: IWashService) => getWashServicesItem(washService))}</List>
        </Paper>
      </Stack>

      {isUpdateWashServiceOpened ? (
        <AddOrUpdateWashServiceDialog
          priceGroupList={pricePolicyList}
          washService={currentMenuItem}
          onCancel={handleUpdateWashServiceCancel}
          onOk={handleUpdateWashServiceOk}
        />
      ) : null}

      <Dialog open={isNotificationDialogOpen} onClose={() => setisNotificationDialogOpen(false)}>
        <DialogTitle>
          <Typography variant='h6'>Невозможно добавить услугу</Typography>
        </DialogTitle>
        <DialogContent>
          <Typography variant='body2'>Для добавления услуги сначала необходимо создать ценовую категорию.</Typography>
          <Typography variant='body2'>Это можно сделать в разделе "Настройки"&rarr;"Ценовые группы".</Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setisNotificationDialogOpen(false)}>Ok</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}
