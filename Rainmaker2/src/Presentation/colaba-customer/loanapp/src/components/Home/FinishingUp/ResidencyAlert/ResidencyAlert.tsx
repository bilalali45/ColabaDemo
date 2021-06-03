import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const ResidenceAlert = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Residency Alert</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
