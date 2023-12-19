export interface ICarWash {
    id: number,
    name: string,
    location: string,
    phone: string,
}

export interface IWashService {
    id: number;
    enabled: boolean;
    name: string;
    price: number;
    duration: number;
    description: string;
    composition: number[];  // список ID-шников базовых услуг (т.е. IWashService)
  }
  
  export interface ICarInfo {
      id?: number;
      regNumber: string;
      mark: string;
      model: string;
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
  