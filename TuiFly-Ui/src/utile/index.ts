import { FlightRequest, PassengerRequest } from "../nswag";
import {
  IErrorForm,
  IErrorMsg,
  IFlightListPages,
} from "../nswag/types";

export const canDisplay=(page:number,list:IFlightListPages[])=>{
  let res = list?.find((item) => item.page === page);
  if(res){
    if(Array.isArray(res.list)){
      return res.list.length > 0
    }
  }
  return false
}

export const getCurrentDate = (): string => {
  const currentDate = new Date();
  const year = currentDate.getFullYear();
  let month = String(currentDate.getMonth() + 1).padStart(2, "0");
  let day = String(currentDate.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};

export const validationForm = (flight?: FlightRequest) => {
  const msgError: IErrorMsg = {
    city: false,
    time: false,
  };
  const error: IErrorForm = {
    isValid: true,
    msg: msgError,
  };

  if (flight?.departureCity === flight?.arrivalCity) {
    error.isValid = false;
    error.msg.city = true;
  }
  if (flight?.departureDate && flight?.arrivalDate) {
    if (new Date(flight.departureDate) > new Date(flight?.arrivalDate)) {
      error.isValid = false;
      error.msg.time = true;
    }
  }
  if (!(flight?.departureCity && flight?.arrivalCity)) {
    error.isValid = false;
    error.msg.city = true;
  }
  if (!(flight?.departureDate && flight?.arrivalDate)) {
    error.isValid = false;
    error.msg.time = true;
  }
  return error;
};


export const isValidEmail = (email: string) => {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
};

export const validatePassengers = (passengers: PassengerRequest[],champsErrorFirstName:string,champsErrorLastName:string,champsErrorEmail:string) => {
  const validationErrors: { [key: string]: any }[] = [];
  for(let i = 0; i < passengers.length;i++ ){
    const errors: { [key: string]: any } = {};
    const passenger = passengers[i]
    if (!passenger.firstName) {
      errors.firstName = champsErrorFirstName;
    }
    if (!passenger.lastName) {
      errors.lastName = champsErrorLastName;
    }
    if (!passenger.email) {
      errors.email = champsErrorEmail;
    } else if (!isValidEmail(passenger.email)) {
      errors.email = "Invalid email address";
    }
    if(Object.keys(errors).length > 0){
      errors.index = passenger.index
      validationErrors.push(errors)
    }
  }
  return validationErrors
};

