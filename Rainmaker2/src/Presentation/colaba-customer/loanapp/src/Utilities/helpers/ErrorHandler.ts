import { Error } from "../../Entities/Models/Error"
import { ErrorActionsType } from "../../store/reducers/ErrorReducer"
export class ErrorHandler {

    static successStatus:number[] =  [200, 204]
    static defaultErrorMessage:string = "Internal Server Error."
    static defaultErrorCode:number = 500

 
    static setError(dispatch:Function, err: any){
        if(err?.response){
            if(err?.response?.status.toString().startsWith("4")) return;
            if(err?.response?.data){
                if(err?.response?.data?.Message || err?.response?.data?.message){
                let error = new Error(err?.response?.status, err?.response?.data?.Message || err?.response?.data?.message)
                dispatch({
                    type: ErrorActionsType.SetErrorMessage,
                    payload: error
                  });
            }
            else {
                let errorMessageKey = Object.keys(err?.response?.data?.errors)[0]
                if(errorMessageKey){
                    let error = new Error(err?.response?.status, err?.response?.data?.errors[errorMessageKey])
                    dispatch({
                        type: ErrorActionsType.SetErrorMessage,
                        payload: error
                      }); 
                }
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