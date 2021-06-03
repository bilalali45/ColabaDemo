import { Error } from "../../src/Entities/Models/Error";
import { ErrorActionsType } from "../../src/Store/reducers/ErrorReducer";
 
export class ErrorHandler {

    static successStatus:number[] =  [200, 204]
    static defaultErrorMessage:string = "Something went wrong, Please try again later."
    static defaultErrorCode:number = 500

 
    static setError(dispatch:Function, err: any){
        if(err?.response){
            if(err?.response?.status.toString().startsWith("4")) return;
            if(err?.response?.data){
                if(err?.response?.data?.Message){
                let error = new Error(err?.response?.status, err?.response?.data?.Message)
                dispatch({
                    type: ErrorActionsType.SetErrorMessage,
                    payload: error
                  });
            }
            else {
                let errorMessageKey = Object.keys(err?.response?.data?.errors)[0]
                let error = new Error(err?.response?.status, err?.response?.data?.errors[errorMessageKey || 0])
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