import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";



export const ResidenceMove = () => {
  
  useEffect(() => {   

  }, []);



  return (
      <div>
      <h2>Borrower Residence Move Date</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
      </div>
  );
};
