export class Env {
    public static apiEnv : string
}

export const initialisationEnv=async ()=>{
    const res =await fetch("./configuration.json")
    const json =await res.json()
    
    Env.apiEnv = json.apiEnv  
}