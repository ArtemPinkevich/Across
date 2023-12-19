import { SignalRConnection } from "../transport/SignalRTransport";

/*
        Это пример взаимодействия с бэком через Middleware
        видится, что прибегать к такому методу нужно только тогда, когда:
            1. Action должен быть обработан несколькими Middleware/Reduser
            2. Помимо взаимодействия с бэком что-то должно произойти со Store
        в остальных случаях предлагается использовать SignalRConnection прям во View.
export const signalRMiddlewareExample = (store: any) => (next: any) => (action: any) => {
    SignalRConnection.getInstance().send('Send', 'first');
}
*/

/*
Это пример Middleware как функции + взаимодействие с бэком через + обработка Action и вызов next
Можно удалить через некоторое время 
export function signalRMiddlewareExample2(store: any) {
    return (next: any) => (action: any) => {
        if (action.signalR) {
            switch (action.type) {
                case "":
                    console.log('signalRMiddleware')
                    //_hub.server.methodOnTheServer();
                    break;
                // default:
                //     {
                //         const myCurrentState = store.getState().objectWithinState;
                //         _hub.server.methodOnTheServer2(action.type, myCurrentState);
                //     }
            }
        }
        return next(action);
    }
}
*/
