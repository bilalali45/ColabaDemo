import React from "react";
import { LocalDB } from "../../../../lib/LocalDB";
import { PropertyMortgageSecondStep } from "../PropertyMortgage/PropertyMortgageSecondStep/PropertyMortgageSecondStep";

type SecondCurrentResidenceMortgageProps = {
  address:string;
}
export const SecondCurrentResidenceMortgage = ({address}:SecondCurrentResidenceMortgageProps) => {
  return <PropertyMortgageSecondStep
    propertyId={LocalDB.getMyPropertyTypeId()}
    title={'My Current Residence'} 
    address={address}
    animatedText={"Onwards to second mortgages (if you have any)"}/>

};
