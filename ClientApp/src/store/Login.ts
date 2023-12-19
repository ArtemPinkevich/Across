import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

/////////////
// Enums of ActionTypes
export enum LoginTypes {
    SIGNIN = 'SIGN_IN',
    SIGNOUT = 'SIGN_OUT',
    SUBSCRIBE_SIGNALR = 'SUBSCRIBE_SIGNALR'
}

/////////////
// State
export interface UserState {
    isLogined: boolean;
    id: string;
    accessToken: string;
    refreshToken: string;
    expireDateTime: Date;
}

/////////////
// Actions
interface ISignInAction { 
    type: LoginTypes.SIGNIN;
    userState: UserState;
}

interface ISignOutAction { type: LoginTypes.SIGNOUT }

interface IInitAction { type: LoginTypes.SUBSCRIBE_SIGNALR }

type KnownAction = ISignInAction | ISignOutAction | IInitAction;

/////////////
// Actions Creators
export const actionCreators = {
    signIn: (data: UserState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({type: LoginTypes.SIGNIN, userState: data})
    },
    
    subscribeOnSignalR: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: LoginTypes.SUBSCRIBE_SIGNALR })
    }
}


/////////////
// Store on undefined
const unloadedState: UserState = { 
    isLogined: false,
    id: '',
    accessToken: '',
    refreshToken: '',
    expireDateTime: new Date(),
};

/////////////
// Reducer
export const reducer: Reducer<UserState> = (state: UserState | undefined, incomingAction: Action): UserState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case LoginTypes.SIGNIN:
            return {
                ...state,
                isLogined: true,
                id: action.userState.id,
                accessToken: action.userState.accessToken,
                refreshToken: action.userState.refreshToken,
                expireDateTime: action.userState.expireDateTime
            };
    }

    return state;
};
