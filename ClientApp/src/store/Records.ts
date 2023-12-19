import { Action, Reducer } from "redux";
import { SignalRConnection } from "../transport/SignalRTransport";

/////////////
// Actions
export enum RecordsActionsType {
    RECEIVED_RECORDS = 'RECEIVED_RECORDS',
    RECORD_RECEIVED = 'RECORD_RECEIVED',
    RECORD_DISCARDED = 'RECORD_DISCARDED',
}

interface IReceivedRecordsAction { 
    type: RecordsActionsType.RECEIVED_RECORDS,
    records: IRecord[];
}

interface IRecordReceivedAction { 
    type: RecordsActionsType.RECORD_RECEIVED,
    record: IRecord;
}

interface IRecordDiscardedAction { 
    type: RecordsActionsType.RECORD_DISCARDED,
    discardRecordDto: IDiscardRecordDto;
}

type KnownAction = IReceivedRecordsAction | IRecordReceivedAction | IRecordDiscardedAction;

/////////////
// State
export interface IRecordsState {
    records: IRecord[];
}

interface IDiscardRecordDto {
    discardedRecordId: number;
    successed: boolean;
}

export interface ICarInfo {
    id?: number;
    regNumber: string;
    mark: string;
    model: string;
    carBodyId?: number;
}

export interface IRecord {
    id: number | undefined;
    carWashId: number;
    boxId?: number;
    price: number;
    date: string        // Строка формата yyyy.MM.dd, например "2022.01.30"
    startTime: string   // Строка формата TimeOnly, например "08:00"
    durationMin: number;
    mainServiceId?: number;
    additionServicesIds: number[];
    carInfo: ICarInfo;
    phoneNumber?: string;
}

const unloadedState: IRecordsState = { 
    records: []
};

/////////////
// Reducer
export const reducer: Reducer<IRecordsState> = (state: IRecordsState | undefined, incomingAction: Action): IRecordsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case RecordsActionsType.RECEIVED_RECORDS:
            return {
                ...state,
                records: action.records,
            };
        case RecordsActionsType.RECORD_RECEIVED:
            return {
                ...state,
                records: state.records.filter(o => o.id !== action.record.id).concat(action.record),
            };
        case RecordsActionsType.RECORD_DISCARDED:
            return {
                ...state,
                records: state.records.filter(o => o.id !== action.discardRecordDto.discardedRecordId),
            };
        default: {
            return state
        }
    }
};

/////////////
// handlers of SignalR
export function RecordsApiHandlers(store: any){
    SignalRConnection.getInstance().subscribe('records_responce', (message: IRecord[]) =>{
        store.dispatch({type: RecordsActionsType.RECEIVED_RECORDS, records: message})
    });
    
    SignalRConnection.getInstance().subscribe('record_broadcast', (record: IRecord) =>{
        store.dispatch({type: RecordsActionsType.RECORD_RECEIVED, record: record})
    });

    SignalRConnection.getInstance().subscribe('discard_record_broadcast', (discardRecordDto: IDiscardRecordDto) =>{
        store.dispatch({type: RecordsActionsType.RECORD_DISCARDED, discardRecordDto: discardRecordDto})
    });
}
