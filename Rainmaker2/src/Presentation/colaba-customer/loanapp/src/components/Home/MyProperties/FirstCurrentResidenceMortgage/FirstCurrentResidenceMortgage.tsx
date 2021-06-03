import React from "react";
import { LocalDB } from "../../../../lib/LocalDB";
import { PropertyMortgageFirstStep } from "../PropertyMortgage/PropertyMortgageFirstStep/PropertyMortgageFirstStep";

type FirstCurrentResidenceMortgageProps = {
  address: string
}

export const FirstCurrentResidenceMortgage = ({ address }: FirstCurrentResidenceMortgageProps) => {
  return <PropertyMortgageFirstStep
    propertyId={LocalDB.getMyPropertyTypeId()}
    title={'My Current Residence'}
    address={address}
    animatedText={"We’ll now see some info about your mortgages on the property."} />
};
