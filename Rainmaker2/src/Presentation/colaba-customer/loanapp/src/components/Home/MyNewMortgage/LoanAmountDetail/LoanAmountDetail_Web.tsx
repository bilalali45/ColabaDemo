import React, { useEffect } from "react";
import { useForm } from "react-hook-form";
import PageHead from "../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../Shared/Components/TooltipTitle";
import InputField from "../../../../Shared/Components/InputField";
import InputRadioBox from "../../../../Shared/Components/InputRadioBox";
import InputDatepicker from "../../../../Shared/Components/InputDatepicker";
import { CommaFormatted } from "../../../../Utilities/helpers/CommaSeparteMasking";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { TenantConfigFieldNameEnum } from "../../../../Utilities/Enumerations/TenantConfigEnums";

type props = {
    purchasePrice?: string;
    downPayment?: string;
    percentage?: string;
    onChangePurchase: Function;
    onChangeDownPayment: Function;
    onChangePercentage: Function;
    downPaymentHasGift?: boolean;
    setDownPaymentHasGift: Function;
    enableBtn?: boolean;
    setEnableBtn: Function;
    receiveGift?: boolean;
    setReceiveGift: Function;
    dateOfTransfer?: any;
    setDateOfTransfer: Function;
    giftAmount?: number;
    setGiftAmount: Function;
    setPurchasePrice: Function;
    setDownPayment: Function;
    setPercentage: Function;
    dateChange: Function;
    saveHandler: (data) => void;
    setIsError: Function;
    isError: boolean;
    showError : boolean | null;
}

export const LoanAmountDetailWeb = ({
    purchasePrice,
    downPayment,
    percentage,
    onChangePurchase,
    onChangeDownPayment,
    onChangePercentage,
    downPaymentHasGift,
    setDownPaymentHasGift,
    enableBtn,
    setEnableBtn,
    receiveGift,
    setReceiveGift,
    dateOfTransfer,
    setDateOfTransfer,
    giftAmount,
    setGiftAmount,
    setPurchasePrice,
    setDownPayment,
    setPercentage,
    dateChange,
    saveHandler,
    setIsError,
    showError
}: props) => {
    const { register, errors, handleSubmit, clearErrors, setError } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "firstError",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
     if(showError === true){
        setError("PurchasePrice", {
            type: "server",
            message: "Purchase price should be between $50,000 and $100,000,000",
        }); 
     }
    },[showError])

    return (
        <div className="compo-subject-P compo-subject-P-info fadein">
            <PageHead
                title="Loan Information"
            />
            <TooltipTitle title="Tell us about the loan you would like to obtain. If you don't know the exact amount, an estimate is fine." />
            <form data-testid="loanAmount-form">
                <div className="comp-form-panel colaba-form">
                    <div className=" row price-detail-group ">
                        <div className="col-sm-5">
                            <InputField
                                label={"Purchase Price"}
                                data-testid="purchase-price"
                                id=""
                                name="PurchasePrice"
                                type={"text"}
                                icon={<i className="zmdi zmdi-money"></i>}
                                placeholder={"Purchase value"}
                                register={register}
                                value={CommaFormatted(purchasePrice)}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onChange={(event) => {
                                    clearErrors("PurchasePrice");
                                    let value = event.target.value;
                                    if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                        return false;
                                    }

                                    if (Number(value.replace(/\,/g, '')) <  50000) {
                                        setError("PurchasePrice", {
                                            type: "server",
                                            message: "Purchase price should be between $50,000 and $100,000,000",
                                        });
                                    }

                                    if (value.length > 1) {
                                        clearErrors();
                                    }

                                    if (value.length > 9) {
                                        return false;
                                    }

                                    setPurchasePrice(value.replace(/\,/g, ''));
                                    onChangePurchase(value.replace(/\,/g, ''));
                                    return true;
                                }}
                                onBlur = {() => {                               
                                    if (Number(purchasePrice) <  50000) {
                                        setError("PurchasePrice", {
                                            type: "server",
                                            message: "Purchase price should be between $50,000 and $100,000,000",
                                        });
                                        setIsError(true)
                                    }else{
                                        setIsError(false);
                                    }
                                }}
                            />
                        </div>
                        <div className="col-sm-5">
                            <InputField
                                label={"Down Payment"}
                                data-testid="down-payement"
                                id=""
                                name="DownPayment"
                                type={"text"}
                                icon={<i className="zmdi zmdi-money"></i>}
                                placeholder={"Down Payment Amount"}
                                register={register}
                                value={CommaFormatted(downPayment)}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onChange={(event) => {
                                    clearErrors("DownPayment")
                                    let value = event.target.value;

                                    if (value.length > 0 && !/^[0-9,.]{1,20}$/g.test(value)) {
                                        return false;
                                    }

                                    setDownPayment(value.replace(/\,/g, ''));
                                    onChangeDownPayment(value.replace(/\,/g, ''))
                                    return true;
                                }}
                            />
                        </div>
                        <div className="col-sm-2">
                            <InputField
                                label={""}
                                data-testid="percent-value"
                                id=""
                                name="percent"
                                type={"text"}
                                icon={<i>%</i>}
                                placeholder={"20.00"}
                                register={register}
                                value={CommaFormatted(percentage)}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onChange={(event) => {
                                    clearErrors("percent");
                                    let value = event.target.value;

                                    if (value.length > 0 && !/^[0-9,.]{1,20}$/g.test(value)) {
                                        return false;
                                    }

                                    setPercentage(value.replace(/\,/g, ''));
                                    onChangePercentage(value.replace(/\,/g, ''));
                                    return true;
                                }}
                            />
                        </div>
                    </div>



                    {
                        NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.AnyPartOfDownPaymentGift) &&

                        <div data-testid="downpaymentHasGift-question" className="form-group reduce">
                            <h4>Is any part of the down payment a gift?</h4>
                            <div data-testid="downpaymentHasGift-question-div" className="r-wrap">
                                <div className="clearfix">
                                    <InputRadioBox
                                        dataTestId={"downpaymentHasGift-yes"}
                                        id=""
                                        className=""
                                        name="gift"
                                        value="true"
                                        register={register}
                                        onChange={() => {

                                            setDownPaymentHasGift(true);
                                            setEnableBtn(false);
                                        }}
                                        checked={downPaymentHasGift ? true : false}
                                    >Yes</InputRadioBox>
                                </div>

                                {downPaymentHasGift === undefined &&
                                    <div className="clearfix">
                                        <InputRadioBox
                                            dataTestId={"downpaymentHasGift-no"}
                                            id=""
                                            className=""
                                            name="gift"
                                            value="false"
                                            register={register}
                                            onChange={() => {

                                                setDownPaymentHasGift(false);
                                                setEnableBtn(true);
                                                setGiftAmount('');
                                                setDateOfTransfer(null);
                                                setReceiveGift(undefined);
                                            }}
                                        >No</InputRadioBox>
                                    </div>
                                }
                                {downPaymentHasGift != undefined &&
                                    <div className="clearfix"><InputRadioBox
                                        dataTestId={"downpaymentHasGift-no"}
                                        id=""
                                        className=""
                                        name="gift"
                                        value="false"
                                        register={register}
                                        checked={downPaymentHasGift ? false : true}
                                        onChange={() => {

                                            setDownPaymentHasGift(false);
                                            setEnableBtn(true);
                                            setGiftAmount('');
                                            setDateOfTransfer(null);
                                            setReceiveGift(undefined);
                                        }}
                                    >No</InputRadioBox></div>
                                }


                            </div>
                        </div>
                    }

                    {
                        downPaymentHasGift === true &&
                        <div className="form-group group3">
                            <div className="r-wrap">
                                <div className="clearfix">
                                    <InputRadioBox
                                        id=""
                                        className=""
                                        name="giftReceived"
                                        value="true"
                                        register={register}
                                        checked={receiveGift ? true : false}
                                        onChange={() => {
                                            console.log('---Onchange start---')
                                            setReceiveGift(true);
                                            setGiftAmount('');
                                            setDateOfTransfer(null);
                                            console.log('---Onchange end---')
                                        }}
                                    >Yes, I have received the gift</InputRadioBox>
                                </div>
                                {receiveGift === true &&
                                    <div className="subgroup">
                                        <div className="row">
                                            <div className="col-sm-5">
                                                <InputDatepicker
                                                    isPreviousDateAllowed={true}
                                                    isFutureDateAllowed={false}
                                                    label="Date of Transfer"
                                                    dateFormat="MM/dd/yyyy"
                                                    name="dot"
                                                    handleOnChange={dateChange}
                                                    autoComplete={'off'}
                                                    selected={dateOfTransfer}
                                                    handleOnChangeRaw={(e) => { e.preventDefault(); }}
                                                />
                                            </div>

                                            <div className="col-sm-5">

                                                <InputField
                                                    label={"Gift amount"}
                                                    data-testid=""
                                                    id=""
                                                    name="Giftamount"
                                                    type={"text"}
                                                    icon={<i className="zmdi zmdi-money"></i>}
                                                    placeholder={"Gift amount"}
                                                    register={register}
                                                    value={CommaFormatted(giftAmount)}
                                                    errors={errors}
                                                    onChange={(event) => {

                                                        let value = event.target.value;
                                                        if (value.length > 0 && !/^[0-9,]{1,15}$/g.test(value)) {
                                                            return false;
                                                        }
                                                        setGiftAmount(value.replace(/\,/g, ''));
                                                        if (value.length > 1 && !!dateOfTransfer) {
                                                            setEnableBtn(true)
                                                        } else {
                                                            setEnableBtn(false)
                                                        }
                                                        return true;
                                                    }}
                                                />

                                            </div>
                                        </div>

                                    </div>
                                }


                            </div>
                            <div className="r-wrap">
                                {receiveGift === undefined &&
                                    <div className="clearfix">
                                        <InputRadioBox
                                            id=""
                                            className=""
                                            name="giftReceived"
                                            value="false"
                                            register={register}
                                            onChange={() => {
                                                console.log('---Onchange start 1---')
                                                setReceiveGift(false);
                                                setGiftAmount('');
                                                setDateOfTransfer(null);
                                                console.log('---Onchange end 1---')
                                            }}
                                        >No, I'm expecting to receive it later</InputRadioBox>
                                    </div>
                                }
                                {receiveGift != undefined &&
                                    <InputRadioBox
                                        id=""
                                        className=""
                                        name="giftReceived"
                                        value="false"
                                        checked={receiveGift ? false : true}
                                        register={register}
                                        onChange={() => {
                                            console.log('---Onchange start 2---')
                                            setReceiveGift(false);
                                            setGiftAmount('');
                                            setDateOfTransfer(null);
                                            console.log('---Onchange end 2---')
                                        }}
                                    >No, I'm expecting to receive it later</InputRadioBox>
                                }

                                {receiveGift === false &&
                                    <div className="subgroup">
                                        <div className="row">
                                            <div className="col-sm-5">
                                                <InputDatepicker
                                                    isPreviousDateAllowed={false}
                                                    isFutureDateAllowed={true}
                                                    label="Expected Date Of Transfer"
                                                    dateFormat="MM/dd/yyyy"
                                                    name="edot"
                                                    handleOnChange={dateChange}
                                                    autoComplete={'off'}
                                                    selected={dateOfTransfer}
                                                    handleOnChangeRaw={(e) => { e.preventDefault(); }}
                                                />
                                            </div>

                                            <div className="col-sm-5">

                                                <InputField
                                                    label={"Expected Gift amount"}
                                                    data-testid=""
                                                    id=""
                                                    name="ExpectedGiftamount"
                                                    type={"text"}
                                                    icon={<i className="zmdi zmdi-money"></i>}
                                                    placeholder={"Gift amount"}
                                                    register={register}
                                                    value={CommaFormatted(giftAmount)}
                                                    errors={errors}
                                                    onChange={(event) => {
                                                        let value = event.target.value;
                                                        if (value.length > 0 && !/^[0-9,]{1,15}$/g.test(value)) {
                                                            return false;
                                                        }
                                                        setGiftAmount(value.replace(/\,/g, ''));
                                                        if (value.length > 1 && !!dateOfTransfer) {
                                                            setEnableBtn(true)
                                                        } else {
                                                            setEnableBtn(false)
                                                        }
                                                        return true;
                                                    }}
                                                />

                                            </div>
                                        </div>

                                    </div>}

                            </div>
                        </div>
                    }






                    <div className="form-footer">
                        <button
                            data-testid="saveBtn"
                            disabled={!enableBtn}
                            className="btn btn-primary"
                            onClick={handleSubmit(saveHandler)}
                        >
                            {"Save & Continue"}
                        </button>


                    </div>
                </div>
            </form>
        </div>
    )
}
