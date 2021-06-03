import React from "react";
import { PropertyFirstMortgageDetailsForm } from "../PropertyMortgage/PropertyFirstMortgageDetailsForm/PropertyFirstMortgageDetailsForm";

type FirstCurrentResidenceMortgageDetails_WebProps = {
  firstPayment: string;
  setFirstPayment: Function;
  firstPaymentBalance: string;
  setFirstPaymentBalance: Function;
  propTax: string;
  setPropTax: Function;
  isTaxIncInPayment: boolean;
  setIsTaxIncludedInPayment: Function;
  propInsurance: string;
  setPropInsurance: Function;
  isInsuranceIncInPayment: boolean;
  setIsInsuranceIncludedInPayment: Function;
  isHELOC: boolean;
  setIsHELOC: Function;
  creditLimit: string;
  setCreditLimit: Function;
  onSave: Function;
  setFloodIns:Function;
  floodIns:string;
  showPaidOff:boolean;
  isPaidOff :boolean | null;
  setIsPaidOff: Function;
  isFloodInsuranceIncInPayment : boolean;
  setIsFloodInsuranceIncludedInPayment :Function;
  address: string

};
export const FirstCurrentResidenceMortgageDetails_Web = ({
  firstPayment,
  setFirstPayment,
  firstPaymentBalance,
  setFirstPaymentBalance,
  propTax,
  setPropTax,
  isTaxIncInPayment,
  setIsTaxIncludedInPayment,
  propInsurance,
  setPropInsurance,
  isInsuranceIncInPayment,
  setIsInsuranceIncludedInPayment,
  isHELOC,
  setIsHELOC,
  creditLimit,
  setCreditLimit,
  onSave,
  setFloodIns,
  floodIns,
  showPaidOff,
  isPaidOff,
  setIsPaidOff,
  isFloodInsuranceIncInPayment,
  setIsFloodInsuranceIncludedInPayment,
  address
}: FirstCurrentResidenceMortgageDetails_WebProps) => {

  return (
    <>
      <PropertyFirstMortgageDetailsForm
        firstPayment={firstPayment}
        setFirstPayment={setFirstPayment}
        firstPaymentBalance={firstPaymentBalance}
        setFirstPaymentBalance={setFirstPaymentBalance}
        propTax={propTax}
        setPropTax={setPropTax}
        isTaxIncInPayment={isTaxIncInPayment}
        setIsTaxIncludedInPayment={setIsTaxIncludedInPayment}
        propInsurance={propInsurance}
        setPropInsurance={setPropInsurance}
        isInsuranceIncInPayment={isInsuranceIncInPayment}
        setIsInsuranceIncludedInPayment={setIsInsuranceIncludedInPayment}
        isHELOC={isHELOC}
        setIsHELOC={setIsHELOC}
        creditLimit={creditLimit}
        setCreditLimit={setCreditLimit}
        onSave={onSave}
        homeAddress={address}
        setFloodIns = {setFloodIns}
        floodIns = {floodIns}
        showPaidOff={showPaidOff}
        isPaidOff = {isPaidOff}
        setIsPaidOff = {setIsPaidOff}
        isFloodInsuranceIncInPayment={isFloodInsuranceIncInPayment}
        setIsFloodInsuranceIncludedInPayment ={setIsFloodInsuranceIncludedInPayment}
      />
    </>
  );
};
