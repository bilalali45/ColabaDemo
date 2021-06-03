import _ from 'lodash';
import React, { useContext, useEffect, useState } from 'react'
import { SecondMortgageValue } from '../../../../../Entities/Models/CurrentResidence';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { LocalDB } from '../../../../../lib/LocalDB';
import PageHead from '../../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';
import MyPropertyActions from '../../../../../store/actions/MyPropertyActions';
import { Store } from '../../../../../store/store';
import { CommaFormatted, removeCommaFormatting } from '../../../../../Utilities/helpers/CommaSeparteMasking';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { SecondCurrentResidenceMortgageDetails_Web } from '../../SecondCurrentResidenceMortgageDetails/SecondCurrentResidenceMortgageDetails_Web';

type PropertyMortgageSecondStepDetailsProps = {
    propertyId: number, 
    title: string, 
    address: string,
    animatedText: string
}
export type PropertyMortgageSecondStepPostObj = {
    second_Payment: string, 
    second_pay_bal: string,
    credit_limit: string
}

export const PropertyMortgageSecondStepDetails = ({ propertyId, title, address, animatedText }: PropertyMortgageSecondStepDetailsProps) => {
    const { state, dispatch } = useContext(Store);
    const { myPropertyInfo }: any = state.loanManager;

    const [secondPayment, setSecondPayment] = useState<string>("");
    const [secondPaymentBalance, setSecondPaymentBalance] = useState<string>("");
    const [isHELOC, setIsHELOC] = useState<boolean>(false);
    const [creditLimit, setCreditLimit] = useState<string>("");
    const [btnClick, setBtnClick] = useState<boolean>(false);
    const [showPaidOff, setShowPaidOff] = useState<boolean>(false);
    const [isPaidOff, setIsPaidOff] = useState<boolean | null>(null);


    useEffect(() => {
        // if (myPropertyInfo && myPropertyInfo?.primaryPropertyTypeId) {
        if (propertyId) {
            getPropertyValue();
            getSecondMortgageValue();
        }

    }, [myPropertyInfo]);


    const getSecondMortgageValue = async () => {
        let res: any = await MyPropertyActions.getSecondMortgageValue(
            +LocalDB.getLoanAppliationId(),
            +propertyId
            // +myPropertyInfo?.primaryPropertyTypeId
        );

        if (res) {
            if (ErrorHandler.successStatus.includes(res.statusCode)) {
                if (!_.isEmpty(res?.data)) {
                    const {
                        secondMortgagePayment,
                        unpaidSecondMortgagePayment,
                        helocCreditLimit,
                        isHeloc,
                        paidAtClosing
                    } = res.data;
                    setSecondPayment(CommaFormatted(secondMortgagePayment));
                    setSecondPaymentBalance(CommaFormatted(unpaidSecondMortgagePayment));
                    setCreditLimit(CommaFormatted(helocCreditLimit));
                    setIsHELOC(isHeloc);
                    setIsPaidOff(paidAtClosing)
                }

            } else {
                ErrorHandler.setError(dispatch, res);
            }
        }
    };

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

    const onSave = async (data: PropertyMortgageSecondStepPostObj) => {
        if (!btnClick) {
            setBtnClick(true);
            let reqData: SecondMortgageValue = preparePostAPIData(data);
            let res = await MyPropertyActions.addOrUpdateSecondMortgageValue(reqData);
            if (res) {
                if (ErrorHandler.successStatus.includes(res.statusCode)) {
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyProperties/AllProperties`)
                } else {
                    ErrorHandler.setError(dispatch, res);
                }
            }
        }
    };

    const preparePostAPIData = (data: PropertyMortgageSecondStepPostObj) => {
        const { second_Payment, second_pay_bal, credit_limit } = data;

        let hasFirstMortgage: SecondMortgageValue = {
            LoanApplicationId: Number(LocalDB.getLoanAppliationId()),
            Id: +propertyId,
            // Id: +myPropertyInfo?.primaryPropertyTypeId,
            State: NavigationHandler.getNavigationStateAsString(),
            UnpaidSecondMortgagePayment: second_pay_bal
                ? +removeCommaFormatting(second_pay_bal)
                : null,
            IsHeloc: isHELOC === null ? false : isHELOC,
            HelocCreditLimit: credit_limit ? +removeCommaFormatting(credit_limit) : 0,

            SecondMortgagePayment: second_Payment
                ? +removeCommaFormatting(second_Payment)
                : null,
            PaidAtClosing: (isPaidOff === null || isPaidOff === undefined) ? false : isPaidOff,
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
                <SecondCurrentResidenceMortgageDetails_Web
                    secondPayment={secondPayment}
                    setSecondPayment={setSecondPayment}
                    secondPaymentBalance={secondPaymentBalance}
                    setSecondPaymentBalance={setSecondPaymentBalance}
                    isHELOC={isHELOC}
                    setIsHELOC={setIsHELOC}
                    creditLimit={creditLimit}
                    setCreditLimit={setCreditLimit}
                    onSave={onSave}
                    showPaidOff={showPaidOff}
                    isPaidOff={isPaidOff}
                    setIsPaidOff={setIsPaidOff}
                    address={address}
                />
            </div>
        </section>
    );
}
