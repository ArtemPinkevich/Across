import * as Login from './Login';
import * as PricePolicyStore from './PricePolicyStore';
import * as WashServices from './WashServices';
import * as CarWashSettings from './CarWashSettings';
import * as Records from './Records';
import * as FrontStore from './FrontStore';
import * as CarStore from './CarStore';

// The top-level state object
export interface ApplicationState {
    user: Login.UserState | undefined;
    pricePolicyState: PricePolicyStore.IPriceGroupState |  undefined;
    washServices: WashServices.IWashServicesState |  undefined;
    carWashSettings: CarWashSettings.ICarWashSettingsState |  undefined;
    records: Records.IRecordsState |  undefined;
    frontStore: FrontStore.IFrontState |  undefined;
    carStore: CarStore.ICarState |  undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    login: Login.reducer,
    pricePolicyState: PricePolicyStore.reducer,
    washServices: WashServices.reducer,
    carWashSettings: CarWashSettings.reducer,
    records: Records.reducer,
    frontStore: FrontStore.reducer,
    carStore: CarStore.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
