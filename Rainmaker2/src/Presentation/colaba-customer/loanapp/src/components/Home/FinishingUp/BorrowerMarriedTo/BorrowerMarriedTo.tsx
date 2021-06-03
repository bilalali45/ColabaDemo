import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const BorrowerMarriedTo = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Borrower Married To</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
