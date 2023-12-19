import { Action, Reducer } from 'redux';
import { SignalRConnection } from '../transport/SignalRTransport';

/////////////
// Actions
export enum PricePolicyActionsType {
  RECEIVED_PRICE_GROUP = 'RECEIVED_PRICE_GROUP',
  ADD_PRICE_GROUP = 'ADD_PRICE_GROUP',
  REMOVE_PRICE_GROUP = 'REMOVE_PRICE_GROUP',
  UPDATE_PRICE_GROUP = 'UPDATE_PRICE_GROUP',
}

interface IReceivedPriceGroupAction {
  type: PricePolicyActionsType.RECEIVED_PRICE_GROUP;
  priceGroups: IPriceGroup[];
}

interface IAddPriceGroupAction {
  type: PricePolicyActionsType.ADD_PRICE_GROUP;
  priceGroup: IPriceGroup;
}

interface IRemovePriceGroupAction {
  type: PricePolicyActionsType.REMOVE_PRICE_GROUP;
  priceGroupId: number;
}

interface IUpdatePriceGroupAction {
  type: PricePolicyActionsType.UPDATE_PRICE_GROUP;
  priceGroup: IPriceGroup;
}

type KnownAction = IAddPriceGroupAction | IRemovePriceGroupAction | IReceivedPriceGroupAction | IUpdatePriceGroupAction;

/////////////
// State
export interface IPriceGroupState {
  allPriceGroup: IPriceGroup[];
}

export interface ICarBody {
  carBodyId: number;
  carBodyName: string;
}

export interface IPriceGroup {
  id: number;
  name: string;
  carBodies: ICarBody[];
  description: string;
}

const unloadedState: IPriceGroupState = {
  allPriceGroup: [
    // {
    //     id: 1,
    //     number: 1,
    //     carBodyTypes: [CarBodyType.Sedan, CarBodyType.Hatchback, CarBodyType.Wagon, CarBodyType.Cabriolet,
    //                 CarBodyType.Suv, CarBodyType.Crossover, CarBodyType.Pickup, CarBodyType.Van,
    //                 CarBodyType.Limousine, CarBodyType.Minivan, CarBodyType.Bus],
    //     carBodies: [],
    //     description: 'Здесь будет описание',
    // },
    // {
    //     id: 2,
    //     number: 2,
    //     carBodyTypes: [CarBodyType.Sedan, CarBodyType.Hatchback, CarBodyType.Wagon, CarBodyType.Cabriolet,
    //                 CarBodyType.Suv, CarBodyType.Crossover, CarBodyType.Pickup],
    //     carBodies: [],
    //     description: '',
    // }
  ],
};

/////////////
// Reducer
export const reducer: Reducer<IPriceGroupState> = (state: IPriceGroupState | undefined, incomingAction: Action): IPriceGroupState => {
  if (state === undefined) {
    return unloadedState;
  }

  const action = incomingAction as KnownAction;
  switch (action.type) {
    case PricePolicyActionsType.RECEIVED_PRICE_GROUP:
      return {
        ...state,
        allPriceGroup: action.priceGroups.slice(0),
      };
    case PricePolicyActionsType.ADD_PRICE_GROUP:
      return {
        ...state,
        allPriceGroup: state.allPriceGroup.concat(action.priceGroup),
      };
    case PricePolicyActionsType.REMOVE_PRICE_GROUP:
      return {
        ...state,
        allPriceGroup: state.allPriceGroup.filter((o) => o.id !== action.priceGroupId),
      };
    case PricePolicyActionsType.UPDATE_PRICE_GROUP:
      return {
        ...state,
        allPriceGroup: state.allPriceGroup.map((o) => (o.id === action.priceGroup.id ? action.priceGroup : o)),
      };
    default: {
      return state;
    }
  }
};

/////////////
// handlers of SignalR
export function PriceGroupApiHandlers(store: any) {
  SignalRConnection.getInstance().subscribe('price_groups_broadcast', (message: { allPriceGroups: IPriceGroup[] }) => {
    store.dispatch({ type: PricePolicyActionsType.RECEIVED_PRICE_GROUP, priceGroups: message.allPriceGroups });
  });

  SignalRConnection.getInstance().subscribe('add_price_group_responce', (message: IPriceGroup) => {
    store.dispatch({ type: 'ADD_PRICE_GROUP', priceGroup: message });
  });

  SignalRConnection.getInstance().subscribe('update_price_group_responce', (priceGroup: IPriceGroup) => {
    const allPriceGroup: IPriceGroup[] = store.getState().pricePolicyState.allPriceGroup;
    if (allPriceGroup.findIndex((o) => o.id === priceGroup.id) === -1) {
      SignalRConnection.getInstance().send('get_price_groups');
      return;
    }

    store.dispatch({ type: 'UPDATE_PRICE_GROUP', priceGroup: priceGroup });
  });

  SignalRConnection.getInstance().subscribe('remove_price_group_responce', (message) => {
    const allPriceGroup: IPriceGroup[] = store.getState().pricePolicyState.allPriceGroup;
    if (allPriceGroup.findIndex((o) => o.id === message) === -1) {
      SignalRConnection.getInstance().send('get_price_groups');
      return;
    }

    store.dispatch({ type: 'REMOVE_PRICE_GROUP', priceGroupId: message });
  });
}
