import { Env } from "../../config/environment";
import { Client } from "../../nswag"

export const ApiClient=()=>{
    const url = Env.apiEnv
    return new Client(url);
}
