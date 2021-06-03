import React from "react";
import { PropertyFirstMortgageForm } from "../PropertyMortgage/PropertyFirstMortgageForm/PropertyFirstMortgageForm";

type FirstCurrentResidenceMortgage_WebProps = {
  propTax: string;
  setPropTax: Function;
  insurance: string;
  setInsurance: Function;
  haveMortgage: boolean | null;
  setHaveMortgage: Function;
  onSave: Function;
  setFloodIns:Function;
  floodIns:string;
  address: string
};
export const FirstCurrentResidenceMortgage_Web = ({
  propTax,
  setPropTax,
  insurance,
  setInsurance,
  haveMortgage,
  setHaveMortgage,
  onSave,
  setFloodIns,
  floodIns,
  address
}: FirstCurrentResidenceMortgage_WebProps) => {

  return (
    <>
      <PropertyFirstMortgageForm
        propTax={propTax}
        setPropTax={setPropTax}
        insurance={insurance}
        setInsurance={setInsurance}
        haveMortgage={haveMortgage}
        setHaveMortgage={setHaveMortgage}
        onSave={onSave}
        homeAddress={address}
        floodIns={floodIns}
        setFloodIns={setFloodIns}

      />
    </>
  );
};
