import React from 'react';
import { Grid, Checkbox, FormControlLabel, TextField, InputAdornment } from '@mui/material';
import TimelapseIcon from '@mui/icons-material/Timelapse';
import { IPriceGroup } from '../../../store/PricePolicyStore';
import { IPriceGroupService } from '../../../store/WashServices';

export interface IPriceGroupServiceItemProps {
  priceGroup: IPriceGroup;
  priceGroupService: IPriceGroupService;
}

export const PriceGroupServiceItem: React.FC<IPriceGroupServiceItemProps> = React.memo((props: IPriceGroupServiceItemProps) => {
  const { priceGroup, priceGroupService } = props;

  const [isEnabled, setIsEnabled] = React.useState<boolean>(priceGroupService.enabled);
  const [priceService, setPriceService] = React.useState<IPriceGroupService>(priceGroupService);

  const handleOnIsEnabledChange = React.useCallback(
    (_, checked) => {
      setIsEnabled(checked);
      priceGroupService.enabled = checked;
    },
    [priceService]
  );

  const handleOnPriceChange = React.useCallback(
    (event) => {
      setPriceService({ ...priceService, price: +event.target.value });
      priceGroupService.price = +event.target.value;
    },
    [priceService]
  );

  const handleOnDurationChange = React.useCallback(
    (event) => {
      setPriceService({ ...priceService, duration: +event.target.value });
      priceGroupService.duration = +event.target.value;
    },
    [priceService]
  );

  return (
    <Grid item xs component='fieldset' container direction='row' style={{ paddingBottom: '15px' }}>
      <legend>
        <FormControlLabel
          label={`Ценовая категория: ${priceGroup.name}`}
          control={<Checkbox value={isEnabled} checked={isEnabled} onChange={handleOnIsEnabledChange} />}
        />
      </legend>
      <Grid item xs={4}>
        <TextField
          required
          label='Стоимость'
          type='number'
          disabled={!isEnabled}
          value={priceService.price}
          onChange={isEnabled ? handleOnPriceChange : undefined}
          InputProps={{
            startAdornment: <InputAdornment position='start'>{'\u20bd'}</InputAdornment>,
          }}
        />
      </Grid>
      <Grid item xs={4}>
        <TextField
          required
          label='Продолжительность (мин.)'
          type='number'
          disabled={!isEnabled}
          value={priceService.duration}
          onChange={isEnabled ? handleOnDurationChange : undefined}
          InputProps={{
            startAdornment: (
              <InputAdornment position='start'>
                <TimelapseIcon color='action' fontSize='small' />
              </InputAdornment>
            ),
          }}
        />
      </Grid>
    </Grid>
  );
});
