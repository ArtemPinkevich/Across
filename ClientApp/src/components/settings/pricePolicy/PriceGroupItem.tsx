import * as React from 'react';
import { Typography, IconButton, Menu, MenuItem, Card, CardContent, Chip, Grid, Box } from '@mui/material';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { SignalRConnection } from '../../../transport/SignalRTransport';
import { ICarBody, IPriceGroup } from '../../../store/PricePolicyStore';
import { AddOrUpdatePriceGroupDialog } from './AddOrUpdatePriceGroupDialog';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../../store';
import { ICarWashSettings } from '../../../store/CarWashSettings';

export interface IPriceGroupItemProps {
  priceGroup: IPriceGroup;
}

export default function PriceGroupItem(props: IPriceGroupItemProps) {
  const { priceGroup } = props;

  const carWashSettings: ICarWashSettings = useSelector((state: ApplicationState) => state.carWashSettings!.carWashSettings);

  const [isMenuOpened, setIsMenuOpened] = React.useState<null | HTMLElement>(null);
  const [isDialogOpened, setIsDialogOpened] = React.useState(false);

  const handleMenuClick = (event: React.MouseEvent<HTMLButtonElement>, priceGroup: IPriceGroup) => {
    setIsMenuOpened(event.currentTarget);
  };

  const handleMenuClose = () => {
    setIsMenuOpened(null);
  };

  const handleEditClick = () => {
    setIsDialogOpened(true);
  };

  const handleRemoveClick = () => {
    SignalRConnection.getInstance().send('remove_price_group', priceGroup.id, carWashSettings.id);
    setIsMenuOpened(null);
  };

  const handleUpdatePriceGroupCancel = () => {
    setIsMenuOpened(null);
    setIsDialogOpened(false);
  };

  const handleUpdatePriceGroupOk = (priceGroup: IPriceGroup) => {
    setIsMenuOpened(null);
    setIsDialogOpened(false);
    SignalRConnection.getInstance().send('update_price_group', priceGroup, carWashSettings.id);
  };

  return (
    <Card sx={{ my: 4 }}>
      <Box sx={{ display: 'flex', p: 1, m: 1, bgcolor: 'background.paper', borderRadius: 1 }}>
        <CardContent sx={{ width: '90%' }}>
          <Typography variant='h5'>{priceGroup.name}</Typography>
          <br />
          <Typography sx={{ fontSize: 14 }} color='text.secondary' gutterBottom>
            Состав ценовой группы (список кузовов автомобилей)
          </Typography>

          {priceGroup.carBodies?.map((carBody: ICarBody) => (
            <Chip key={carBody.carBodyId} label={carBody.carBodyName} sx={{ m: 1 }} />
          ))}

          {priceGroup.description ? (
            <Box>
              <Typography sx={{ mt: 3, mb: 1 }} color='text.secondary'>
                Описание ценовой группы
              </Typography>
              <Typography variant='body2' sx={{ ml: 3 }}>
                {priceGroup.description}
              </Typography>
            </Box>
          ) : null}
        </CardContent>

        <Grid width={"10%"} container justifyContent='end' alignItems='center'>
          <IconButton
            aria-label='Действия'
            aria-controls={`services-item-menu`}
            aria-haspopup='true'
            onClick={(event) => handleMenuClick(event, priceGroup)}
          >
            <MoreVertIcon />
          </IconButton>
          <Menu id={`services-item-menu`} anchorEl={isMenuOpened} open={Boolean(isMenuOpened)} onClose={handleMenuClose}>
            <MenuItem onClick={handleEditClick}>Редактировать</MenuItem>
            <MenuItem onClick={handleRemoveClick}>Удалить</MenuItem>
          </Menu>
        </Grid>
      </Box>

      {isDialogOpened ? (
        <AddOrUpdatePriceGroupDialog priceGroup={priceGroup} onCancel={handleUpdatePriceGroupCancel} onOk={handleUpdatePriceGroupOk} />
      ) : null}
    </Card>
  );
}
