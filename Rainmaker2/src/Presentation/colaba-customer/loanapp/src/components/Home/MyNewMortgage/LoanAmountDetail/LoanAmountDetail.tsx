import React, { useState, useEffect, useContext } from "react";
import moment from "moment";
import { LoanAmountDetailWeb } from "./LoanAmountDetail_Web";
import MyNewMortgageActions from "../../../../store/actions/MyNewMortgageActions";
import { LocalDB } from "../../../../lib/LocalDB";
import { ErrorHandler } from "../../../../Utilities/helpers/ErrorHandler";
import { LoanAmountDetails } from "../../../../Entities/Models/LoanAmountDetail";
import { LoanCalculations, LoanInfoCalculated } from "../../../../lib/loancalculations";
import { Store } from "../../../../store/store";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";



export const LoanAmountDetail = () => {

    const { dispatch } = useContext(Store);
    const [purchasePrice, setPurchasePrice] = useState<string>("");
    const [downPayment, setDownPayment] = useState<string>("");
    const [percentage, setPercentage] = useState<string>("");
    const [downPaymentHasGift, setDownPaymentHasGift] = useState<boolean>(undefined);
    const [enableBtn, setEnableBtn] = useState<boolean>(false);
    const [receiveGift, setReceiveGift] = useState<boolean>(undefined);
    const [dateOfTransfer, setDateOfTransfer] = useState<Date>(null);
    const [giftAmount, setGiftAmount] = useState<number>();
    const [btnClick, setBtnClick] = useState<boolean>(false);
    const [isError, setIsError] = useState<boolean>(false);
    const [showError, setShowError] = useState<boolean>(null);

    useEffect(() => {
        getLoanAmountDetail();
    }, [])

    const getLoanAmountDetail = async () => {
        let loanApplicationId = +(LocalDB.getLoanAppliationId())
        let response = await MyNewMortgageActions.getSubjectPropertyLoanAmountDetail(loanApplicationId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                let loanInfoDetail = new LoanAmountDetails(
                    response.data.loanApplicationId,
                    response.data.propertyValue,
                    response.data.downPayment,
                    response.data.giftPartOfDownPayment,
                    response.data.giftPartReceived,
                    response.data.dateOfTransfer,
                    response.data.giftAmount
                )
                if (loanInfoDetail.PropertyValue) {
                    populateLoanAmountDetail(loanInfoDetail);
                }

            } else {
                ErrorHandler.setError(dispatch, response);

            }
        }
        applyTenantConfiguration();
    }

    const applyTenantConfiguration = (): void => {
        if (!NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.AnyPartOfDownPaymentGift)) {
            setEnableBtn(true);
        }
    }

    const addOrUpdateLoanAmountDetail = async (model: LoanAmountDetails) => {
        let response = await MyNewMortgageActions.addOrUpdateLoanAmountDetail(model);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                NavigationHandler.moveNext();
            } else {
                ErrorHandler.setError(dispatch, response);
                setBtnClick(false);
            }
        }
    }

    const onChangePurchaseHandler = (purchaseValue: string) => {
        let loanInfo: LoanInfoCalculated = LoanCalculations.calculatedownPaymentandPercent(Number(purchaseValue), percentage ? Number(percentage) : 0);
        if (loanInfo) {
            setDownPayment(loanInfo.downPayment);
            setPercentage(loanInfo.percentValue);
        } else {
            setDownPayment("");
            setPercentage("");
        }

    }

    const onChangeDownPaymentHandler = (downPaymentValue: string) => {
        let loanInfo: LoanInfoCalculated = LoanCalculations.calculatedownPaymentPercent(purchasePrice ? Number(purchasePrice) : 0, Number(downPaymentValue));
        if (loanInfo) {
            setPurchasePrice(loanInfo.purchasePrice);
            setPercentage(loanInfo.percentValue);
            if (Number(downPaymentValue) != Number(loanInfo.downPayment)) {
                setDownPayment(loanInfo.downPayment);
            }

        } else {
            if (purchasePrice) {
                setDownPayment("");
                setPercentage("");
            } else {
                setDownPayment("0");
                setPercentage("");
            }

        }

    }

    const onChangePercentageHandler = (percentValue: string) => {
        let loanInfo: LoanInfoCalculated = LoanCalculations.calculatedownPaymentandPercent(purchasePrice ? Number(purchasePrice) : 0, Number(percentValue));
        if (loanInfo) {
            setPurchasePrice(loanInfo.purchasePrice);
            setDownPayment(loanInfo.downPayment);
            if (Number(percentValue) > 97) {
                setPercentage(loanInfo.percentValue);
            }

        } else {
            setDownPayment("");
            setPercentage("0");
        }

    }

    const dateChangeHandler = (date) => {
        if (!!date && !!giftAmount) {
            setEnableBtn(true)
        } else {
            setEnableBtn(false)
        }
        setDateOfTransfer(date);
    }



    const saveHandler = () => {
        setShowError(false);
        if (!isError) {
            if (!btnClick) {
                setBtnClick(true);
                let loanInfoAmountDetail = new LoanAmountDetails(
                    +(LocalDB.getLoanAppliationId()),
                    Number(purchasePrice),
                    Number(downPayment),
                    downPaymentHasGift,
                    downPaymentHasGift ? receiveGift : null,
                    downPaymentHasGift ? moment(dateOfTransfer).format('YYYY-MM-DD') : null,
                    downPaymentHasGift ? Number(giftAmount) : null,
                    NavigationHandler.getNavigationStateAsString()
                )
                addOrUpdateLoanAmountDetail(loanInfoAmountDetail);

            }
        } else {
            setShowError(true);
        }
    }

    const populateLoanAmountDetail = (model: LoanAmountDetails) => {
        if (model) {
            let loanInfo: LoanInfoCalculated = LoanCalculations.calculatedownPaymentPercent(model.PropertyValue, model.DownPayment);
            if (model?.PropertyValue) setPurchasePrice(model?.PropertyValue.toString());
            if (model?.DownPayment) setDownPayment(model?.DownPayment.toString());
            setPercentage(loanInfo?.percentValue);
            setDownPaymentHasGift(model?.GiftPartOfDownPayment);

            if (model?.GiftPartOfDownPayment) {
                //let date = moment(model?.DateOfTransfer).format("MM/DD/YYYY");        
                setReceiveGift(model?.GiftPartReceived);
                setDateOfTransfer(new Date(model?.DateOfTransfer));
                setGiftAmount(model?.GiftAmount);

            }
            setEnableBtn(true);
        }
    }

    return (
        <LoanAmountDetailWeb
            purchasePrice={purchasePrice}
            downPayment={downPayment}
            percentage={percentage}
            onChangePurchase={onChangePurchaseHandler}
            onChangeDownPayment={onChangeDownPaymentHandler}
            onChangePercentage={onChangePercentageHandler}
            downPaymentHasGift={downPaymentHasGift}
            setDownPaymentHasGift={setDownPaymentHasGift}
            enableBtn={enableBtn}
            setEnableBtn={setEnableBtn}
            receiveGift={receiveGift}
            setReceiveGift={setReceiveGift}
            dateOfTransfer={dateOfTransfer}
            setDateOfTransfer={setDateOfTransfer}
            giftAmount={giftAmount}
            setGiftAmount={setGiftAmount}
            setPurchasePrice={setPurchasePrice}
            setDownPayment={setDownPayment}
            setPercentage={setPercentage}
            dateChange={dateChangeHandler}
            saveHandler={saveHandler}
            isError={isError}
            setIsError={setIsError}
            showError={showError}
        />
    )
}
