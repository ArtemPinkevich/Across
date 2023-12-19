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
import { makeStyles } from '@mui/styles';
import { ICarBody, IPriceGroup } from '../../../store/PricePolicyStore';
import { useEffect, useState } from 'react';
import { SignalRConnection } from '../../../transport/SignalRTransport';

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
  textField: {
    maxWidth: theme.typography.pxToRem(500),
  },
}));

export interface AddOrUpdatePriceGroupDialogProps {
  priceGroup?: IPriceGroup;
  onCancel: () => void;
  onOk: (priceGroup: IPriceGroup) => void;
}

export function AddOrUpdatePriceGroupDialog(props: AddOrUpdatePriceGroupDialogProps) {
  const classes = useStyles();
  const { priceGroup, onCancel, onOk } = props;
  const editingPriceGroup = priceGroup ?? {} as IPriceGroup;
  
  const [name, setName] = useState<string>(editingPriceGroup.name);
  const [description, setDescription] = useState(editingPriceGroup.description ?? '');
  const [selectedCarBodies, setSelectedCarBodies] = useState<number[]>(editingPriceGroup?.carBodies?.map(o => o.carBodyId) ?? []);
  const [carBodies, setCarBodies] = useState<ICarBody[]>([])

  
  useEffect(() => {
    SignalRConnection.getInstance().invoke('get_car_bodies')
      .then((result: ICarBody[]) => {
            setCarBodies(result);
      })
      .catch((reason) => { console.error(reason) });
    }, []);
    
  const handleCancel = () => {
    onCancel();
  };

  const handleOk = () => {
    editingPriceGroup.name = name;
    editingPriceGroup.description = description;
    editingPriceGroup.carBodies = carBodies.filter(o => selectedCarBodies.includes(o.carBodyId));
    onOk(editingPriceGroup);
  };

  const handleOnCompositioChange = (e: any) => {
    setSelectedCarBodies(e.target.value as number[]);
  };

  const handleOnNameChange = React.useCallback((event) => {
    setName(event.target.value);
  }, []);

  return (
    <Dialog onClose={handleCancel} open={true} fullWidth maxWidth="md">
      <DialogTitle>{priceGroup === undefined ? 'Создание ценовой группы' : `Редактирование ценовой группы`}</DialogTitle>
      <DialogContent>
        <Container style={{ margin: '10px 0px 0px 0px' }}>
          <Grid container spacing={3} direction="column">
            <Grid item>
              <TextField id="price-policy-name-field" className={classes.textField} value={name} onChange={handleOnNameChange} label="Наименование" />
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel id="demo-mutiple-chip-label">Состав ценовой группы</InputLabel>
                <Select
                  labelId="demo-mutiple-chip-label"
                  id="demo-mutiple-chip"
                  fullWidth
                  multiple
                  value={selectedCarBodies}
                  onChange={handleOnCompositioChange}
                  input={<Input id="select-multiple-chip" />}
                  renderValue={(selected) => (
                    <div className={classes.chips}>
                       {(selected as number[]).map((value) =>
                        <Chip key={value} label={carBodies.find((o) => o.carBodyId === value)?.carBodyName} className={classes.chip} />
                      )}
                    </div>
                  )}
                >
                  {carBodies.map((carBody) => (
                    <MenuItem key={carBody.carBodyId} value={carBody.carBodyId}>
                      {carBody.carBodyName}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12}>
              <TextField
                fullWidth
                label="Описание"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                inputProps={{ maxLength: 100 }}
              />
            </Grid>
          </Grid>
        </Container>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel} color="primary" autoFocus>
          Отмена
        </Button>
        <Button onClick={handleOk} color="primary">
          Готово
        </Button>
      </DialogActions>
    </Dialog>
  );
}
