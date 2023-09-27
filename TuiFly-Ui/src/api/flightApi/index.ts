import { FlightRequest } from "../../nswag"
import { ApiClient } from "../ApiClient"

export const flightFindAll=(flight?:FlightRequest)=>{
    return ApiClient().findAll(flight);
}