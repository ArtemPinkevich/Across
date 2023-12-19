import { Action, Reducer } from "redux";
import { SignalRConnection } from "../transport/SignalRTransport";
import { AppThunkAction } from '.';

/////////////
// Actions
export enum WashSetingsActionsType {
    RECEIVED_CAR_WASH_SETTINGS = 'RECEIVED_CAR_WASH_SETTINGS',
    SET_CAR_WASH_SETTINGS = 'SET_CAR_WASH_SETTINGS',
}

interface IReceivedCarWashSettingsAction { 
    type: WashSetingsActionsType.RECEIVED_CAR_WASH_SETTINGS,
    carWashSettings: ICarWashSettings;
}

interface ISetCarWashSettingsAction { 
    type: WashSetingsActionsType.SET_CAR_WASH_SETTINGS,
    carWashSettings: ICarWashSettings;
}

type KnownAction = IReceivedCarWashSettingsAction | ISetCarWashSettingsAction;

/////////////
// Actions Creators
export const actionCreators = {
    setCarWashSettings: (data: ICarWashSettings): AppThunkAction<ISetCarWashSettingsAction> => (dispatch, getState) => {
        dispatch({type: WashSetingsActionsType.SET_CAR_WASH_SETTINGS, carWashSettings: data})
    },
}

/////////////
// State
export interface ICarWashSettingsState {
    carWashSettings: ICarWashSettings;
}

export interface ICarWashSettings {
    id: number;
    name: string;
    location: string;
    phone: string;
    boxesQuantity: number;
    reservedMinutesBetweenRecords: number;
    workTime: IWorkTime;
}

export interface IWorkTime {
    mondayBegin : string;
    mondayEnd: string;
    tuesdayBegin : string;
    tuesdayEnd: string;
    wednesdayBegin : string;
    wednesdayEnd: string;
    thursdayBegin : string;
    thursdayEnd: string;
    fridayBegin : string;
    fridayEnd: string;
    saturdayBegin : string;
    saturdayEnd: string;
    sundayBegin : string;
    sundayEnd: string;
}


const unloadedState: ICarWashSettingsState = {
    carWashSettings: {
        id: 1,
        name: 'carWashName',
        location: 'address',
        phone: '79990001122',
        boxesQuantity: 3,
        reservedMinutesBetweenRecords: 0,
        workTime: {
            mondayBegin : '2021-10-14T03:00:00.000Z',
            mondayEnd: '2021-10-14T13:00:00.000Z',
            tuesdayBegin : '',
            tuesdayEnd: '',
            wednesdayBegin : '',
            wednesdayEnd: '',
            thursdayBegin : '',
            thursdayEnd: '',
            fridayBegin : '',
            fridayEnd: '',
            saturdayBegin : '',
            saturdayEnd: '',
            sundayBegin : '',
            sundayEnd: '',
        }
    },
};

/////////////
// Reducer
export const reducer: Reducer<ICarWashSettingsState> = (state: ICarWashSettingsState | undefined, incomingAction: Action): ICarWashSettingsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case WashSetingsActionsType.SET_CAR_WASH_SETTINGS:
        case WashSetingsActionsType.RECEIVED_CAR_WASH_SETTINGS:
            return {
                ...state,
                carWashSettings: action.carWashSettings,
            };
        default: {
            return state
        }
    }
};

/////////////
// handlers of SignalR
export function CarWashSettingsApiHandlers(store: any){
    SignalRConnection.getInstance().subscribe('car_wash_settings_broadcast', (message: ICarWashSettings) => {
        store.dispatch({type: WashSetingsActionsType.RECEIVED_CAR_WASH_SETTINGS, carWashSettings: message})
    });
}
