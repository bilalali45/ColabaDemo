import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const ResidenceHistoryList = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Residency History List</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
