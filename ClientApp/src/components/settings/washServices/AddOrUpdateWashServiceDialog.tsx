import * as React from 'react';
import {
  Dialog,
  DialogTitle,
  Button,
  DialogActions,
  DialogContent,
  Container,
  Grid,
  TextField,
  FormControl,
  InputLabel,
  Input,
  Chip,
  MenuItem,
  Select,
  Theme,
} from '@mui/material';
import { IPriceGroupService, IWashService } from '../../../store/WashServices';
import { useSelector } from 'react-redux';
import { ApplicationState } from '../../../store';
import { makeStyles } from '@mui/styles';
import { IPriceGroup } from '../../../store/PricePolicyStore';
import { PriceGroupServiceList } from './PriceGroupServiceList';

const useStyles = makeStyles((theme: Theme) => ({
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
    maxWidth: 300,
  },
  chips: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  chip: {
    margin: 2,
  },
  noLabel: {
    marginTop: theme.spacing(3),
  },
}));

export interface AddOrUpdateWashServiceDialogProps {
  priceGroupList: IPriceGroup[];
  washService?: IWashService;
  onCancel: () => void;
  onOk: (washService: IWashService) => void;
}

export function AddOrUpdateWashServiceDialog(props: AddOrUpdateWashServiceDialogProps) {
  const classes = useStyles();
  const { priceGroupList, washService, onCancel, onOk } = props;
  const editingWashService: IWashService = washService ?? ({} as IWashService);

  const allWashServices: IWashService[] = useSelector((state: ApplicationState) =>
    state.washServices !== undefined ? state.washServices.allWashServices : []
  );
  const baseWashServices = allWashServices.filter(
    (o) => (o.composition === undefined || o.composition === null || o.composition.length === 0) && o.id !== editingWashService.id
  );

  // Генерация editingWashService.washServiceSettingsDtos
  let forGenerating = priceGroupList;
  if (editingWashService.washServiceSettingsDtos){
    const currentWashServiceSettingsDtosIds = editingWashService.washServiceSettingsDtos.map(o => o.priceGroupId);
    forGenerating = priceGroupList.filter(o => !currentWashServiceSettingsDtosIds.includes(o.id));
  }
  else{
    editingWashService.washServiceSettingsDtos = [];
  }
  
  for (let i = 0; i < forGenerating.length; i++) {
    editingWashService.washServiceSettingsDtos.push({
      priceGroupId: forGenerating[i].id,
      price: undefined,
      duration: undefined,
      enabled: false,
    });
  }
  
  const [name, setName] = React.useState(editingWashService.name);
  const [priceGroupServiceList, setPriceGroupServiceList] = React.useState<IPriceGroupService[]>(editingWashService.washServiceSettingsDtos ?? []);
  const [description, setDescription] = React.useState(editingWashService.description ?? '');
  const [composition, setComposition] = React.useState(editingWashService.composition ?? []);

  const handleCancel = () => {
    onCancel();
  };

  const handleOk = () => {
    editingWashService.enabled = editingWashService.enabled ?? true;
    editingWashService.name = name;
    editingWashService.description = description;
    editingWashService.composition = composition;
    editingWashService.washServiceSettingsDtos = priceGroupServiceList;
    onOk(editingWashService);
  };
  
  const handleOnCompositioChange = (e: any) => {
    setComposition(e.target.value as number[]);
  };
  
  return (
    <Dialog onClose={handleCancel} open={true} fullWidth maxWidth='md'>
      <DialogTitle>{washService === undefined ? 'Создание услуги' : 'Редактирование услуги'}</DialogTitle>
      <DialogContent>
        <Container style={{ margin: '5px 0px 0px 0px' }}>
          <Grid container spacing={3} direction='column'>
            <Grid item xs={12}>
              <TextField required fullWidth label='Название' value={name} onChange={(e) => setName(e.target.value)} inputProps={{ maxLength: 50 }} />
            </Grid>
            <Grid item>
              <PriceGroupServiceList priceGroupServiceList={priceGroupServiceList}/>
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label='Описание'
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                inputProps={{ maxLength: 100 }}
              />
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel id='demo-mutiple-chip-label'>Состав</InputLabel>
                <Select
                  labelId='demo-mutiple-chip-label'
                  id='demo-mutiple-chip'
                  fullWidth
                  multiple
                  value={composition}
                  onChange={handleOnCompositioChange}
                  input={<Input id='select-multiple-chip' />}
                  renderValue={(selected) => (
                    <div className={classes.chips}>
                      {(selected as number[]).map((value) => (
                        <Chip key={value} label={baseWashServices.find((o) => o.id === value)?.name} className={classes.chip} />
                      ))}
                    </div>
                  )}
                >
                  {baseWashServices.map((washService) => (
                    <MenuItem key={washService.id} value={washService.id}>
                      {washService.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </Container>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel} color='primary' autoFocus>
          Отмена
        </Button>
        <Button onClick={handleOk} color='primary'>
          Готово
        </Button>
      </DialogActions>
    </Dialog>
  );
}
