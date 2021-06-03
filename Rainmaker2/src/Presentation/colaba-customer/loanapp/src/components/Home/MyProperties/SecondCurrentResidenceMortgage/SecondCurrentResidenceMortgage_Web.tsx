import React from "react";
import { PropertySecondMortgageForm } from "../PropertyMortgage/PropertySecondMortgageForm/PropertySecondMortgageForm";

type SecondCurrentResidenceMortgage_WebProps = {
  haveMortgage: boolean | null;
  setHaveMortgage: Function;
  onSave: Function;
  address: string;
};

export const SecondCurrentResidenceMortgage_Web = ({
  haveMortgage,
  setHaveMortgage,
  onSave,
  address
}: SecondCurrentResidenceMortgage_WebProps) => {

  return (
    <>
      <PropertySecondMortgageForm
        haveMortgage={haveMortgage}
        setHaveMortgage={setHaveMortgage}
        onSave={onSave}
        homeAddress={address}
      />
    </>
  );
};
