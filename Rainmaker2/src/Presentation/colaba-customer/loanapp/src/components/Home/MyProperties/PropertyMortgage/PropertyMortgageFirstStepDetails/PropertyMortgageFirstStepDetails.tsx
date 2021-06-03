import _ from 'lodash';
import React, { useContext, useEffect, useState } from 'react'
import { FirstMortgageValue } from '../../../../../Entities/Models/CurrentResidence';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { LocalDB } from '../../../../../lib/LocalDB';
import PageHead from '../../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';
import MyPropertyActions from '../../../../../store/actions/MyPropertyActions';
import { Store } from '../../../../../store/store';
import { CommaFormatted, removeCommaFormatting } from '../../../../../Utilities/helpers/CommaSeparteMasking';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { FirstCurrentResidenceMortgageDetails_Web } from '../../FirstCurrentResidenceMortgageDetails/FirstCurrentResidenceMortgageDetails_Web';

type PropertyMortgageFirstStepDetailsProps = {
  propertyId: number,
  title: string,
  address: string,
  animatedText: string
}
export type PropertyMortgageFirstStepDetailsPostObj = {
  first_Payment: string,
  first_pay_bal: string,
  prop_tax: string,
  prop_insurance: string,
  credit_limit: string,
  flood_insurance: string
}
export const PropertyMortgageFirstStepDetails = ({ propertyId, title, address, animatedText }: PropertyMortgageFirstStepDetailsProps) => {
  const { state, dispatch } = useContext(Store);
  const { myPropertyInfo }: any = state.loanManager;


  const [firstPayment, setFirstPayment] = useState<string>("");
  const [firstPaymentBalance, setFirstPaymentBalance] = useState<string>("");
  const [propTax, setPropTax] = useState<string>("");
  const [isTaxIncInPayment, setIsTaxIncludedInPayment] = useState<boolean>(
    false
  );
  const [propInsurance, setPropInsurance] = useState<string>("");
  const [
    isInsuranceIncInPayment,
    setIsInsuranceIncludedInPayment,
  ] = useState<boolean>(false);
  const [isHELOC, setIsHELOC] = useState<boolean>(false);
  const [creditLimit, setCreditLimit] = useState<string>("");
  const [floodIns, setFloodIns] = useState<string>("");
  const [isFloodInsuranceIncInPayment, setIsFloodInsuranceIncludedInPayment] = useState<boolean>(
    false
  );
  const [showPaidOff, setShowPaidOff] = useState<boolean>(false);
  const [isPaidOff, setIsPaidOff] = useState<boolean | null>(null);
  const [btnClick, setBtnClick] = useState<boolean>(false);

  useEffect(() => {
    //   if (myPropertyInfo && myPropertyInfo?.primaryPropertyTypeId){
    if (propertyId) {
      getPropertyValue();
      getFirstMortgageValue();

    }

  }, [myPropertyInfo]);


  const getPropertyValue = async () => {
    let res: any = await MyPropertyActions.getPropertyValue(
      +LocalDB.getLoanAppliationId(),
      +propertyId
      // +myPropertyInfo?.primaryPropertyTypeId
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        const { isSelling } = res.data;
        if (!isSelling) setShowPaidOff(true);
        else setIsPaidOff(true)
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const getFirstMortgageValue = async () => {
    let res: any = await MyPropertyActions.getFirstMortgageValue(
      +LocalDB.getLoanAppliationId(),
      +propertyId
      // +myPropertyInfo?.primaryPropertyTypeId
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        if (!_.isEmpty(res?.data)) {
          const {
            propertyTax,
            propertyTaxesIncludeinPayment,
            homeOwnerInsurance,
            homeOwnerInsuranceIncludeinPayment,
            firstMortgagePayment,
            unpaidFirstMortgagePayment,
            helocCreditLimit,
            isHeloc,
            floodInsurance,
            paidAtClosing,
            floodInsuranceIncludeinPayment
          } = res.data;
          setPropTax(CommaFormatted(propertyTax));
          setIsTaxIncludedInPayment(propertyTaxesIncludeinPayment);
          setPropInsurance(CommaFormatted(homeOwnerInsurance));
          setIsInsuranceIncludedInPayment(homeOwnerInsuranceIncludeinPayment);
          setFirstPayment(CommaFormatted(firstMortgagePayment));
          setFirstPaymentBalance(CommaFormatted(unpaidFirstMortgagePayment));
          setCreditLimit(CommaFormatted(helocCreditLimit));
          setFloodIns(CommaFormatted(floodInsurance))
          setIsPaidOff(paidAtClosing)
          setIsFloodInsuranceIncludedInPayment(floodInsuranceIncludeinPayment)
          setIsHELOC(isHeloc);
        }

      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const onSave = async (data: PropertyMortgageFirstStepDetailsPostObj) => {
    if (!btnClick) {
      setBtnClick(true);
      let reqData: FirstMortgageValue = preparePostAPIData(data);
      let res = await MyPropertyActions.addOrUpdateFirstMortgageValue(reqData);
      if (res) {
        if (ErrorHandler.successStatus.includes(res.statusCode)) {
          NavigationHandler.moveNext();
        } else {
          ErrorHandler.setError(dispatch, res);
        }
      }
    }
  };

  const preparePostAPIData = (data: PropertyMortgageFirstStepDetailsPostObj) => {
    const {
      first_Payment,
      first_pay_bal,
      prop_tax,
      prop_insurance,
      credit_limit,
      flood_insurance
    } = data;

    let hasFirstMortgage: FirstMortgageValue = {
      LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
      Id: +propertyId,
      // Id: +myPropertyInfo?.primaryPropertyTypeId,
      State: NavigationHandler.getNavigationStateAsString(),
      PropertyTax: prop_tax ? +removeCommaFormatting(prop_tax) : 0,
      PropertyTaxesIncludeinPayment: (isTaxIncInPayment === null || isTaxIncInPayment === undefined) ? false : isTaxIncInPayment,
      UnpaidFirstMortgagePayment: first_pay_bal
        ? +removeCommaFormatting(first_pay_bal)
        : null,
      HomeOwnerInsurance: prop_insurance
        ? +removeCommaFormatting(prop_insurance)
        : null,
      IsHeloc: (isHELOC === null || isHELOC === undefined) ? false : isHELOC,
      HelocCreditLimit: credit_limit ? +removeCommaFormatting(credit_limit) : 0,
      HomeOwnerInsuranceIncludeinPayment: (isInsuranceIncInPayment === null || isInsuranceIncInPayment === undefined) ? false : isInsuranceIncInPayment,
      FirstMortgagePayment: first_Payment
        ? +removeCommaFormatting(first_Payment)
        : null,
      FloodInsurance: flood_insurance ? +removeCommaFormatting(flood_insurance) : 0,
      PaidAtClosing: (isPaidOff === null || isPaidOff === undefined) ? false : isPaidOff,
      FloodInsuranceIncludeinPayment: (isFloodInsuranceIncInPayment === null || isFloodInsuranceIncInPayment === undefined) ? false : isFloodInsuranceIncInPayment,
    };

    return hasFirstMortgage;
  };

  if (!propertyId) {
    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/CurrentResidence`);
    return <React.Fragment />;
  }

  return (
    <section>
      <div className="compo-myMoney-income fadein">
        <PageHead title={title} handlerBack={() => { }} />
        <TooltipTitle title={animatedText} />
        <FirstCurrentResidenceMortgageDetails_Web
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
          setFloodIns={setFloodIns}
          floodIns={floodIns}
          showPaidOff={showPaidOff}
          isPaidOff={isPaidOff}
          setIsPaidOff={setIsPaidOff}
          isFloodInsuranceIncInPayment={isFloodInsuranceIncInPayment}
          setIsFloodInsuranceIncludedInPayment={setIsFloodInsuranceIncludedInPayment}
          address={address}
        />
      </div>
    </section>
  );
}
