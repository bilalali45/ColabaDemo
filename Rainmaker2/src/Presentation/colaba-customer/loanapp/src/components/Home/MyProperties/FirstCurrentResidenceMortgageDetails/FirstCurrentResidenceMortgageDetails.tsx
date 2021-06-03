import React from "react";
import { LocalDB } from "../../../../lib/LocalDB";

import { PropertyMortgageFirstStepDetails } from "../PropertyMortgage/PropertyMortgageFirstStepDetails/PropertyMortgageFirstStepDetails";

 type FirstCurrentResidenceMortgageDetailsProps = {
  address: string
 }
export const FirstCurrentResidenceMortgageDetails = ({address}: FirstCurrentResidenceMortgageDetailsProps) => {
  return <PropertyMortgageFirstStepDetails
    propertyId={LocalDB.getMyPropertyTypeId()}
    title={'My Current Residence'} 
    address={address}
    animatedText={"In order to determine your current liabilities, please provide some information on your first mortgage"}/>
};
