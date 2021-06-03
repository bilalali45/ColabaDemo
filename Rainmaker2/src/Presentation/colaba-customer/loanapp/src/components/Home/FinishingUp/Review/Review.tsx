import React, { useEffect } from "react";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const Review = () => {
  
  useEffect(() => {   

  }, []);



  return (
    <div>
      <h2>Finishing Up Review</h2>
      <button onClick={ () => { NavigationHandler.moveNext(); } } >Move Next</button>
    </div>

    
  );
};
