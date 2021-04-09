import React, { useContext, useEffect } from "react";
import { Store } from "../../Store/Store";

export const ErrorView = () => {

  const { state, dispatch } = useContext(Store);
  const error:any = state.error;
  const errorObj = error.error;


  useEffect(()=>{

    console.log("status: "+ errorObj.status + " message: " + errorObj.message)
  },[errorObj])


  return (
    <div data-testid="error-view">
      {/* <h1>{errorObj.status}</h1>
      <h2>{errorObj.message}</h2> */}
    </div>
  );
};
