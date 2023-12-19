import * as signalR from "@microsoft/signalr";
import { HubConnectionState } from "@microsoft/signalr";
import { getValueFromLocalStorege, LocalStorageKey } from "../Logic/LocalStorageService";

export class SignalRConnection {
    private static instance: SignalRConnection;
    private _connection: signalR.HubConnection | undefined;
    
    public static getInstance(): SignalRConnection {
        if (!SignalRConnection.instance) {
            SignalRConnection.instance = new SignalRConnection();
        }

        return SignalRConnection.instance;
    }
    
    private constructor() {
    }

    public connect(callback: () => void) {
        /********************************/
        // подключение без аутентификационного токена (JWT)
        /********************************/
        // this._connection = new signalR.HubConnectionBuilder()
        //     .withUrl("/washme")
        //     .build();
          
        /********************************/                    
        // подключение с использованием аутентификационного токена (JWT)
        /********************************/
        
        const token = getValueFromLocalStorege(LocalStorageKey.ACCESS_TOKEN) ?? '';
        var serverApi = process.env.REACT_APP_SERVER_API_URL;
        this._connection = new signalR.HubConnectionBuilder()
                            .withUrl(`${serverApi}/washme`, {accessTokenFactory: () => token, withCredentials: false})
                            .withAutomaticReconnect()
                            .build();
                            
        /********************************/
        // эти методы может вызывать сервер
        /********************************/
        // подписка на метод InitializationResponse
        this._connection.on("InitializationResponse", (msg: string) => {
            //console.log(msg);
        })


        /********************************/
        // подключение к серверу
        /********************************/
        this._connection.start().then(() => {
            console.log('!! CONNECTED !!');
            
            callback();
        });
    }

    public invoke<T = any> (methodName: string, ...args: any[]): Promise<T> {
        if (this._connection?.state === HubConnectionState.Connected) {
            return this._connection.invoke(methodName, ...args);
        }
        else {
            console.warn(`SignalRConnection. Try send. connection.state is ${this._connection?.state}`)
            
            return new Promise((resolve, reject) => {
                reject(new Error("Отсутствуе соединение с сервером"));
              });
        }
    }
    
    public subscribe(methodName: string, handler: (...args: any[]) => void) {
        this._connection?.on(methodName, handler)
    }
    
    public send = (methodName: string, ...args: any[]) => {
        if (this._connection?.state === HubConnectionState.Connected){
            this._connection.send(methodName, ...args);
        }
        else{
            console.warn(`SignalRConnection. Try send. connection.state is ${this._connection?.state}`)
        }
    }
}
