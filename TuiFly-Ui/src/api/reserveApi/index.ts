import { ReservationRequest } from "../../nswag";
import { ApiClient } from "../ApiClient"

export const reserveApi=(reservation?:ReservationRequest)=>{
    return ApiClient().save(reservation);
}
