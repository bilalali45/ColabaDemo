import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";



export const CoResidenceHistory = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2> Co Borrower Residence History</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>
  );
};
