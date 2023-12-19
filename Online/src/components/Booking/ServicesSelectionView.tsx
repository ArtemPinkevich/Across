import * as React from 'react';
import { Autocomplete, FormControl, Grid, TextField, Theme } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { IWashService } from '../../Models/AllModels';

const useStyles = makeStyles((theme: Theme) =>
  ({
    selectedTimePoint: {
        background: theme.palette.action.selected,
    },
    chips: {
      display: 'flex',
      flexWrap: 'wrap',
    },
    chip: {
      margin: 2,
    },
  }),
);

export interface ServicesSelectionViewProps {
  mainServiceId?: number;
  additionServicesIds?: number[];
  allWashServices: IWashService[];
  onMainServiceChanged: (washServicesIds: number | undefined) => void;
  onAdditionServicesChanged: (washServicesIds: number[]) => void;
}
  
export function ServicesSelectionView(props: ServicesSelectionViewProps) {
  const classes = useStyles();
  const { allWashServices, onMainServiceChanged, onAdditionServicesChanged } = props;
  
  const [mainSevice, setMainSevice] = React.useState<IWashService | undefined>();
  const [additionServices, setAdditionServices] = React.useState<IWashService[]>();
  
  const handleOnMainServiceChange = (event: any, newValue: IWashService | null) => {
      setAdditionServices([]);
      setMainSevice(newValue ?? undefined);
      onMainServiceChanged(newValue?.id);
  }

  const handleOnAdditionServicesChange = (e: any, newValue: IWashService[] | null) => {
    if (!mainSevice || !newValue)
      return

    const servicesIds: number[] = newValue.map(o => o.id);
    setAdditionServices(newValue);
    onAdditionServicesChanged(servicesIds);
  };

  let availableAdditionServices: IWashService[] = []
  if (mainSevice !== undefined){
      availableAdditionServices = allWashServices.filter(o => o.id !== mainSevice.id && (!o.composition || o.composition?.length === 0) && !mainSevice.composition?.includes(o.id));
  }

  return (
    <Grid container spacing={3} direction="column">
      
      <Grid item xs={4}>
        <FormControl sx={{ m: 1, minWidth: 300 }}>
          <Autocomplete
            options={allWashServices}
            value={mainSevice}
            getOptionLabel={(option) => option.name}
            onChange={handleOnMainServiceChange}
            disableClearable
            renderInput={(params) => (
              <TextField
                {...params}
                variant="standard"
                label="Основная услуга"
              />
            )}/>
        </FormControl>
      </Grid>
      
      <Grid item >
        <FormControl sx={{ m: 1, minWidth: 300 }}>
          <Autocomplete
            multiple
            disableCloseOnSelect
            options={availableAdditionServices}
            defaultValue={additionServices}
            getOptionLabel={(option) => option.name}
            onChange={handleOnAdditionServicesChange}
            renderInput={(params) => (
              <TextField
                {...params}
                variant="standard"
                label="Дополнительные услуги"
              />
            )}/>
        </FormControl>
      </Grid>
    </Grid> 
  );
}
