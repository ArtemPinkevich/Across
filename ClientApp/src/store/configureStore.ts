import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { connectRouter, routerMiddleware } from 'connected-react-router';
import { History } from 'history';
import { ApplicationState, reducers } from './';
import { SignalRConnection } from '../transport/SignalRTransport';
import { WashServiceApiHandlers } from './WashServices';
import { CarWashSettingsApiHandlers } from './CarWashSettings';
import { RecordsApiHandlers } from './Records';
import { SubscribingSignalrMiddleware } from './SubscribingSignalrMiddleware';

export default function configureStore(history: History, initialState?: ApplicationState) {

    const middleware = [
        thunk,
        routerMiddleware(history),
        SubscribingSignalrMiddleware
    ];

    const rootReducer = combineReducers({
        ...reducers,
        router: connectRouter(history)
    });

    const enhancers = [];
    const windowIfDefined = typeof window === 'undefined' ? null : window as any; // eslint-disable-line @typescript-eslint/no-explicit-any
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) {
        enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    }

    const store = createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
    
    // �������� �� ������� SignalR. ������������ ���� ����� ��� ������ ��� �����������.
    //WashServiceApiHandlers(store);
    //CarWashSettingsApiHandlers(store);
    //RecordsApiHandlers(store);

    return store;
}
