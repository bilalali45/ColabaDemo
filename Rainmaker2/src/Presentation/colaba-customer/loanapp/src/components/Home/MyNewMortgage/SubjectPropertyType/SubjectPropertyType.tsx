import React, { useContext, useEffect, useState } from "react";
import { LoanPurposeType } from "../../../../Entities/Models/types";
import { LocalDB } from "../../../../lib/LocalDB";

import MyNewMortgageActions from "../../../../store/actions/MyNewMortgageActions";
import { Store } from "../../../../store/store";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { SubjectPropertyTypeWeb } from "./SubjectPropertyType_Web";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

export const SubjectPropertyNewHome = () => {
  const { dispatch } = useContext(Store);
  const [propertyTypes, setPropertyTypes] = useState<LoanPurposeType[]>();
  const [selectedPropertyType, setSelectedPropertyType] = useState<number>();

  useEffect(() => {
    getAllPropertyTypes();
    getPropertyType();
  }, []);

  const getAllPropertyTypes = async () => {
    let response = await MyNewMortgageActions.getAllpropertytypes();
    if(response){
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        setPropertyTypes(response.data);
      }else{
        ErrorHandler.setError(dispatch, response);
      }
    }
    
  };

  const getPropertyType = async () => {
    let response = await MyNewMortgageActions.getpropertytype(+LocalDB.getLoanAppliationId());
    if(response){
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        setSelectedPropertyType(response.data.id);
      }else{
        ErrorHandler.setError(dispatch, response);
      }
    }
    
  };

  const saveHandler = async (id: number) => {
    let response = await MyNewMortgageActions.addorupdatepropertytype(
      +LocalDB.getLoanAppliationId(),
      id,
      NavigationHandler.getNavigationStateAsString()
    );

    if (response) {
      if (ErrorHandler.successStatus.includes(response.statusCode)) {
        NavigationHandler.moveNext();
      }else{
        ErrorHandler.setError(dispatch, response);
      }
    }
  };

  return (
    <SubjectPropertyTypeWeb
      propertyTypes= {propertyTypes}
      selectedPropertyType = {selectedPropertyType}
      setSelectedPropertyType = {setSelectedPropertyType}
      saveHandler={saveHandler}
    />
  );
};
