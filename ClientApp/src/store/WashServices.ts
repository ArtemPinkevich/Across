import { Action, Reducer } from 'redux';
import { SignalRConnection } from '../transport/SignalRTransport';

/////////////
// Actions
export enum WashServicesActionsType {
  RECEIVED_WASH_SERVICES = 'RECEIVED_WASH_SERVICES',
  ADD_WASH_SERVICE = 'ADD_WASH_SERVICE',
  REMOVE_WASH_SERVICE = 'REMOVE_WASH_SERVICE',
  UPDATE_WASH_SERVICE = 'UPDATE_WASH_SERVICE',
}

interface IReceivedWashServicesAction {
  type: WashServicesActionsType.RECEIVED_WASH_SERVICES;
  washServices: IWashService[];
}

interface IAddWashServiceAction {
  type: WashServicesActionsType.ADD_WASH_SERVICE;
  washService: IWashService;
}

interface IRemoveWashServiceAction {
  type: WashServicesActionsType.REMOVE_WASH_SERVICE;
  washServiceId: number;
}

interface IUpdateWashServiceAction {
  type: WashServicesActionsType.UPDATE_WASH_SERVICE;
  washService: IWashService;
}

type KnownAction = IAddWashServiceAction | IRemoveWashServiceAction | IReceivedWashServicesAction | IUpdateWashServiceAction;

/////////////
// State
export interface IWashServicesState {
  allWashServices: IWashService[];
}

export interface IWashService {
  id: number;
  enabled: boolean;
  name: string;
  washServiceSettingsDtos: IPriceGroupService[];
  description: string;
  composition: number[]; // список ID-шников базовых услуг (т.е. IWashService)
}

export interface IPriceGroupService {
  enabled: boolean;
  priceGroupId: number; // Id ценовой категории
  price: number | undefined;
  duration: number | undefined;
}

const unloadedState: IWashServicesState = {
  allWashServices: [],
};

/////////////
// Reducer
export const reducer: Reducer<IWashServicesState> = (state: IWashServicesState | undefined, incomingAction: Action): IWashServicesState => {
  if (state === undefined) {
    return unloadedState;
  }

  const action = incomingAction as KnownAction;
  switch (action.type) {
    case WashServicesActionsType.RECEIVED_WASH_SERVICES:
      return {
        ...state,
        allWashServices: action.washServices,
      };
    case WashServicesActionsType.ADD_WASH_SERVICE:
      return {
        ...state,
        allWashServices: state.allWashServices.concat(action.washService),
      };
    case WashServicesActionsType.REMOVE_WASH_SERVICE:
      return {
        ...state,
        allWashServices: state.allWashServices.filter((o) => o.id !== action.washServiceId),
      };
    case WashServicesActionsType.UPDATE_WASH_SERVICE:
      return {
        ...state,
        allWashServices: state.allWashServices.map((o) => (o.id === action.washService.id ? action.washService : o)),
      };
    default: {
      return state;
    }
  }
};

/////////////
// handlers of SignalR
export function WashServiceApiHandlers(store: any) {
  SignalRConnection.getInstance().subscribe('wash_services_broadcast', (message: { allWashServices: IWashService[] }) => {
    store.dispatch({ type: WashServicesActionsType.RECEIVED_WASH_SERVICES, washServices: message.allWashServices });
  });

  SignalRConnection.getInstance().subscribe('add_wash_service_responce', (message: IWashService) => {
    store.dispatch({ type: 'ADD_WASH_SERVICE', washService: message });
  });

  SignalRConnection.getInstance().subscribe('update_wash_service_responce', (message: IWashService) => {
    const allWashServices: IWashService[] = store.getState().washServices.allWashServices;
    if (allWashServices.findIndex((o) => o.id === message.id) === -1) {
      SignalRConnection.getInstance().send('get_services');
      return;
    }

    store.dispatch({ type: 'UPDATE_WASH_SERVICE', washService: message });
  });

  SignalRConnection.getInstance().subscribe('remove_wash_service_responce', (message) => {
    const allWashServices: IWashService[] = store.getState().washServices.allWashServices;
    if (allWashServices.findIndex((o) => o.id === message) === -1) {
      SignalRConnection.getInstance().send('get_services');
      return;
    }

    store.dispatch({ type: 'REMOVE_WASH_SERVICE', washServiceId: message });
  });
}
