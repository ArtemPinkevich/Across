import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import { ICarBody } from './PricePolicyStore';

/////////////
// Actions
export enum CarActionsType {
  SET_CAR_BODIES = 'SET_CAR_BODIES',
}

interface ISetCarBodiesAction {
  type: CarActionsType.SET_CAR_BODIES;
  carBodies: ICarBody[];
}

type KnownAction = ISetCarBodiesAction;

/////////////
// Actions Creators
export const actionCreators = {
  setCarBodies: (data: ICarBody[]): AppThunkAction<ISetCarBodiesAction> => (dispatch, getState) => {
      dispatch({type: CarActionsType.SET_CAR_BODIES, carBodies: data})
  },
}

/////////////
// State
export interface ICarState {
  allCarBodies: ICarBody[];
}

const unloadedState: ICarState = {
  allCarBodies: [],
};

/////////////
// Reducer
export const reducer: Reducer<ICarState> = (state: ICarState | undefined, incomingAction: Action): ICarState => {
  if (state === undefined) {
    return unloadedState;
  }

  const action = incomingAction as KnownAction;
  switch (action.type) {
    case CarActionsType.SET_CAR_BODIES:
      return {
        ...state,
        allCarBodies: action.carBodies,
      };
    default: {
      return state;
    }
  }
};
