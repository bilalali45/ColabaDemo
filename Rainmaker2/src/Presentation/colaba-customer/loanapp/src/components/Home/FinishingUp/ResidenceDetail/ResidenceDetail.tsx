import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const ResidenceDetail = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Residency Detail</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
