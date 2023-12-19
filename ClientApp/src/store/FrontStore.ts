import moment from 'moment';
import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

/////////////
// Enums of ActionTypes
export enum FrontActionTypes {
    SCHEDULE_DATE_SELECTED = 'SCHEDULE_DATE_SELECTED',
}

/////////////
// Actions
interface IScheduleDateChangeAction { 
    type: FrontActionTypes.SCHEDULE_DATE_SELECTED;
    selectedDate: moment.Moment;
}

type KnownAction = IScheduleDateChangeAction;

/////////////
// Actions Creators
export const frontActionCreators = {
    scheduleDateChange: (data: moment.Moment): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({type: FrontActionTypes.SCHEDULE_DATE_SELECTED, selectedDate: data})
    },
}


/////////////
// State
export interface IFrontState {
    selectedSchaduleDate: moment.Moment;
}

/////////////
// Store on undefined
const unloadedState: IFrontState = {
    selectedSchaduleDate: moment(),
};

/////////////
// Reducer
export const reducer: Reducer<IFrontState> = (state: IFrontState | undefined, incomingAction: Action): IFrontState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case FrontActionTypes.SCHEDULE_DATE_SELECTED:
            return {
                ...state,
                selectedSchaduleDate: action.selectedDate
            };
        default: {
            return state
        }

    }
};
