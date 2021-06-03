import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const ResidenceAddress = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Residency Address</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
