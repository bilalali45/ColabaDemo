import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const CitizenshipStatus = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Citizenship Status</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
