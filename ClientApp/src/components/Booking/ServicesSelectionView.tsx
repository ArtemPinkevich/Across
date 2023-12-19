import * as React from 'react';
import { useSelector } from 'react-redux';
import { Autocomplete, FormControl, Grid, TextField, Theme } from '@mui/material';
import { makeStyles } from '@mui/styles';
import { IWashService } from '../../store/WashServices';
import { ApplicationState } from '../../store';
import { ICarBody, IPriceGroup } from '../../store/PricePolicyStore';

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
  carBodyId?: number;
  onChanged: (carBody: ICarBody | undefined, washServiceId: number | undefined, washServicesIds: number[]) => void;
}
  
export function ServicesSelectionView(props: ServicesSelectionViewProps) {
  const { mainServiceId, additionServicesIds, carBodyId, onChanged } = props;
  
  const allWashServices: IWashService[] = useSelector((state: ApplicationState) => state.washServices !== undefined ? state.washServices.allWashServices : []);
  const allCarBodies: ICarBody[] = useSelector((state: ApplicationState) => state.carStore!.allCarBodies);
  const allPriceGroups: IPriceGroup[] = useSelector((state: ApplicationState) => state.pricePolicyState !== undefined ? state.pricePolicyState.allPriceGroup : []);


  const selectedCarBody = allCarBodies.find(o => o.carBodyId === carBodyId);
  const selectedMainService = allWashServices.find(o => o.id === mainServiceId);
  const selectedAdditionServices = additionServicesIds ? allWashServices.filter(o => additionServicesIds.includes(o.id) && o.id !== mainServiceId) : [];
  const defaultFiltredWashServices = carBodyId ? getNewFiltredWashServices(carBodyId) : allWashServices;
  const defaultAvailableAdditionServices = selectedMainService ? getNewAvailableAdditionServices(selectedCarBody, selectedMainService) : [];

  const [mainSevice, setMainSevice] = React.useState<IWashService | undefined>(selectedMainService);
  const [additionServices, setAdditionServices] = React.useState<IWashService[]>(selectedAdditionServices);
  const [filtredWashServices, setFiltredWashServices] = React.useState<IWashService[]>(defaultFiltredWashServices);
  const [availableAdditionServices, setAvailableAdditionServices] = React.useState<IWashService[]>(defaultAvailableAdditionServices);
  
  const handleOnCarBodyChange = (event: any, newSelectedCarBody: ICarBody | null) => {
    const newFiltredWashServices = newSelectedCarBody ? getNewFiltredWashServices(newSelectedCarBody.carBodyId) : allWashServices;
    setFiltredWashServices(newFiltredWashServices);

    if (!mainSevice || !newSelectedCarBody){
      onChanged(newSelectedCarBody ?? undefined, mainSevice?.id, []);
      return;
    }
    
    // Если выбранная ранее услуга недоступна для данного кузова, то сбрасываем всё
    const newMainService = newFiltredWashServices.find(o => o.id === mainSevice.id);
    if (!newMainService){
      setMainSevice(undefined);
      setAdditionServices([]);
      setAvailableAdditionServices([]);
      onChanged(newSelectedCarBody, undefined, []);
      return
    }

    // Корректируем список доступных доп.услуг в зависимости от вновь выбранного кузова 
    const newAvailableAdditionServices = getNewAvailableAdditionServices(newSelectedCarBody, newMainService);
    setAvailableAdditionServices(newAvailableAdditionServices);
    
    // Корректируем список ранее выбранных доп.услуг в зависимости от вновь выбранного кузова 
    const additionServicesIds = additionServices.map(o => o.id);
    const newAdditionServices = newAvailableAdditionServices.filter(o => additionServicesIds.includes(o.id))
    setAdditionServices(newAdditionServices);
    
    onChanged(newSelectedCarBody, newMainService?.id, newAdditionServices.map(o => o.id));
  }

  function getNewFiltredWashServices(carBodyId: number): IWashService[] {
    let newFiltredWashServices = allWashServices;
      const priceGroup = allPriceGroups.find(o => o.carBodies.find(c => c.carBodyId === carBodyId));
      if (priceGroup){
        newFiltredWashServices = allWashServices.filter(w => w.washServiceSettingsDtos.find(o => o.priceGroupId === priceGroup.id && o.enabled))
      }

      return newFiltredWashServices;
  }
  
  function getNewAvailableAdditionServices(carBody: ICarBody | undefined, mainService: IWashService): IWashService[] {
    const filtredWashServices = carBody ? getNewFiltredWashServices(carBody.carBodyId) : allWashServices;
    const newAvailableAdditionServices = filtredWashServices.filter(o => o.id !== mainService.id && (!o.composition || o.composition?.length === 0) && !mainService.composition?.includes(o.id));
    return newAvailableAdditionServices;
  }
  
  const handleOnMainServiceChange = (event: any, newMainService: IWashService | null) => {
      setMainSevice(newMainService ?? undefined);
      setAdditionServices([]);
      const newAvailableAdditionServices = newMainService ? getNewAvailableAdditionServices(selectedCarBody, newMainService) : [];
      setAvailableAdditionServices(newAvailableAdditionServices);
      onChanged(selectedCarBody, newMainService?.id, []);
  }

  const handleOnAdditionServicesChange = (e: any, newValue: IWashService[] | null) => {
    if (!mainSevice || !newValue)
      return

    setAdditionServices(newValue);
    onChanged(selectedCarBody, mainSevice.id, newValue.map(o => o.id));
  };
  
  return (
    <Grid container spacing={1} direction="column">
      
      <Grid item xs={4}>
        <FormControl sx={{ m: 1, minWidth: 300 }}>
          <Autocomplete
            options={allCarBodies}
            value={selectedCarBody}
            getOptionLabel={(option) => option.carBodyName}
            onChange={handleOnCarBodyChange}
            disableClearable
            renderInput={(params) => (
              <TextField
                {...params}
                variant="standard"
                label="Кузов автомобиля"
              />
            )}/>
        </FormControl>
      </Grid>
      
      <Grid item xs={4}>
        <FormControl sx={{ m: 1, minWidth: 300 }}>
          <Autocomplete
            key={mainSevice?.id}
            options={filtredWashServices}
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
            value={additionServices}
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
