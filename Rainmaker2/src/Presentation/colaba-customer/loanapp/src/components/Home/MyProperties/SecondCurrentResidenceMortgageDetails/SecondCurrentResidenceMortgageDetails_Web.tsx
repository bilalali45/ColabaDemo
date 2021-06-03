import React from "react";
import { SecondCurrentResidenceMortgageDetails } from "../PropertyMortgage/PropertySecondMortgageDetailsForm/PropertySecondMortgageDetailsForm";

type SecondCurrentResidenceMortgageDetails_WebProps = {
  secondPayment: string;
  setSecondPayment: Function;
  secondPaymentBalance: string;
  setSecondPaymentBalance: Function;
  isHELOC: boolean;
  setIsHELOC: Function;
  creditLimit: string;
  setCreditLimit: Function;
  onSave: Function;
  showPaidOff: boolean;
  isPaidOff :boolean | null;
  setIsPaidOff :Function;
  address: string
};
export const SecondCurrentResidenceMortgageDetails_Web = ({
  secondPayment,
  setSecondPayment,
  secondPaymentBalance,
  setSecondPaymentBalance,
  isHELOC,
  setIsHELOC,
  creditLimit,
  setCreditLimit,
  onSave,
  showPaidOff,
  isPaidOff,
  setIsPaidOff,
  address
}: SecondCurrentResidenceMortgageDetails_WebProps) => {

  return (
    <>
      <SecondCurrentResidenceMortgageDetails
        secondPayment={secondPayment}
        setSecondPayment={setSecondPayment}
        secondPaymentBalance={secondPaymentBalance}
        setSecondPaymentBalance={setSecondPaymentBalance}
        isHELOC={isHELOC}
        setIsHELOC={setIsHELOC}
        creditLimit={creditLimit}
        setCreditLimit={setCreditLimit}
        onSave={onSave}
        homeAddress={address}
        showPaidOff={showPaidOff}
        isPaidOff = {isPaidOff}
        setIsPaidOff = {setIsPaidOff}
      />
    </>
  );
};
