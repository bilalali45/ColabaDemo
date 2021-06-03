import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const CoBorrowerDependents = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Co Borrower Dependents</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
