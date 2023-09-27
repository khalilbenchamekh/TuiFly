import { FlightRequest, FlightResponse } from ".";

export interface ICities {
  label: number;
  value: string;
}
export interface ILib {
  frLib: string;
  enLib: string;
}
export interface IErrorMessage {
  City: ILib;
  time: ILib;
}
export interface IReserve {
  airline: ILib;
  flightNumber: ILib;
  from: ILib;
  to: ILib;
  timetDep: ILib;
  timetRet: ILib;
  price: ILib;
  booking: ILib;
}

export interface IFlightListPages {
  page: number;
  list?: Array<FlightResponse>;
}

export interface IPassengerError {
  errorPage:ILib;
  lastName: ILib;
  firstName: ILib;
  email: ILib;
}

export interface IPassenger {
  name:ILib;
  lastName: ILib;
  firstName: ILib;
  email: ILib;
  submit: ILib;
  cancel:ILib
}

export interface IErrorMsg {
  city?: boolean;
  time?: boolean;
}
export interface IErrorForm {
  isValid: boolean;
  msg: IErrorMsg;
}

export interface IErrorPassengerFormularMsg {
  lastName?: boolean;
  firstName?: boolean;
  email?: boolean;
}

export interface IErrorPassengerFormular {
  isValid: boolean;
  msg: IErrorPassengerFormularMsg;
}

export interface IFlightObject extends FlightRequest{
  nbPassenger ?: number
}

