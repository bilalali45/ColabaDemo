import React from "react";
import { LocalDB } from "../../../../lib/LocalDB";
import { PropertyMortgageSecondStepDetails } from "../PropertyMortgage/PropertyMortgageSecondStepDetails/PropertyMortgageSecondStepDetails";

type SecondCurrentResidenceMortgageDetailsProps = {
  address:string
}
export const SecondCurrentResidenceMortgageDetails = ({address}: SecondCurrentResidenceMortgageDetailsProps) => {
  
  return <PropertyMortgageSecondStepDetails
    propertyId={LocalDB.getMyPropertyTypeId()}
    title={'My Current Residence'} 
    address={address}
    animatedText={"Tell us about the property you would like to refinance."}/>
};
