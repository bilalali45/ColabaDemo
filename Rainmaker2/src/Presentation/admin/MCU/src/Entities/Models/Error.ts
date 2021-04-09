import { ErrorActionsType } from "../../Store/reducers/ErrorReducer";

export class Error {
    
    public status: number;
    public message:string;
    static successStatus:number[] =  [200, 204]
    static defaultErrorMessage:string = "Something went wrong, Please try again later."
    static defaultErrorCode:number = 400

    constructor(status: number, message: string,) {
       
        this.status = status;
        this.message = message;
        
    }

    static setError(dispatch:Function, err: any){
        if(err?.response){
            if(err?.response?.data){
                if(err?.response?.data?.message){
                let error = new Error(err?.response?.status, err?.response?.data?.message)
                dispatch({
                    type: ErrorActionsType.SetErrorMessage,
                    payload: error
                  });
            }
            else {
                let errorMessageKey = Object.keys(err?.response?.data?.errors)[0]
                let error = new Error(err?.response?.status, err?.response?.data?.errors[errorMessageKey])
                dispatch({
                    type: ErrorActionsType.SetErrorMessage,
                    payload: error
                  });
            }
        }
        }
        else if(err?.request){
            let error = new Error(this.defaultErrorCode, err?.request)
            dispatch({
                type: ErrorActionsType.SetErrorMessage,
                payload: error
              });
        }

        else{
            let error = new Error(this.defaultErrorCode, this.defaultErrorMessage)
            dispatch({
                type: ErrorActionsType.SetErrorMessage,
                payload: error
              });
        }
        
    }
}