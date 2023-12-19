import { CarWashSettingsApiHandlers } from "./CarWashSettings";
import { LoginTypes } from "./Login";
import { PriceGroupApiHandlers } from "./PricePolicyStore";
import { RecordsApiHandlers } from "./Records";
import { WashServiceApiHandlers } from "./WashServices";

// Эта Middleware вызывает все функции содержащие подписки на SignalR
// Action SUBSCRIBE_SIGNALR должен бросаться после удачной авторизации по REST
export function SubscribingSignalrMiddleware(store: any) {
    return (next: any) => (action: any) => {
        switch (action.type) {
            case LoginTypes.SUBSCRIBE_SIGNALR:
                WashServiceApiHandlers(store);
                CarWashSettingsApiHandlers(store);
                RecordsApiHandlers(store);
                PriceGroupApiHandlers(store)
                break;
        }

        return next(action);
    }
}
