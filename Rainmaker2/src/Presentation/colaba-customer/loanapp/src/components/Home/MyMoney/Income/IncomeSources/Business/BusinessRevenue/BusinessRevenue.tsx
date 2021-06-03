import React, { useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";




import { Store } from "../../../../../../../store/store";
import InputField from "../../../../../../../Shared/Components/InputField";
import { CommaFormatted, removeCommaFormatting } from "../../../../../../../Utilities/helpers/CommaSeparteMasking";
import { BusinessActionTypes } from "../../../../../../../store/reducers/BusinessIncomeReducer";

import { ApplicationEnv } from "../../../../../../../lib/appEnv";

import { CurrentBusinessDetails } from "../../../../../../../Entities/Models/Business";

import BusinessActions from "../../../../../../../store/actions/BusinessActions";
import { ErrorHandler } from "../../../../../../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import { LoanApplicationActionsType } from "../../../../../../../store/reducers/LoanApplicationReducer";
import { StringServices } from "../../../../../../../Utilities/helpers/StringServices";





export const BusinessRevenue = () => {
    const { state, dispatch } = useContext(Store);
    const {
        businessInfo
    }: any = state.business;

    const [netAnnualIncome, setNetAnnualIncome] = useState<string>(businessInfo?.annualIncome);
    const [btnClick] = useState<boolean>(false);
    const [isClicked, setIsClicked] = useState<boolean>(false);

    const {
        register,
        errors,
        handleSubmit,
        clearErrors,
        setError,
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    useEffect(() => {
        () => {
            setIsClicked(false)
        }
    }, []);

    const onSubmit = async () => {
        if (!isClicked) {
            setIsClicked(true)

            if (netAnnualIncome == undefined || netAnnualIncome == "")
                return;

            if (!btnClick) {
                let currentBusinessDetails: CurrentBusinessDetails = { ...businessInfo }
                //currentBusinessDetails.loanApplicationId = 41351;
                currentBusinessDetails.annualIncome = +removeCommaFormatting(netAnnualIncome);
                let res = await BusinessActions.addOrUpdateBusiness(currentBusinessDetails)
                if (res) {
                    if (ErrorHandler.successStatus.includes(res.statusCode)) {
                        currentBusinessDetails.id = res.data;
                        await dispatch({ type: BusinessActionTypes.SetCurrentBusinessDetails, payload: {} });
                        await dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: {} });
                        //NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/Popup/IncomeSources`);
                        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`);

                    } else {
                        ErrorHandler.setError(dispatch, res);
                    }
                }
            }
        }
    };

    return (
        <React.Fragment>

            <form
                id="net-annual-income-form"
                data-testid="net-annual-income-form"
                onSubmit={handleSubmit(onSubmit)}
                autoComplete="off">
                <div className="p-body">
                    <div className="form-group">
                        <h4>{`How much do you make from ${StringServices.capitalizeFirstLetter(businessInfo?.businessName)}?`}</h4>
                    </div>

                    <div className="row">
                        <div className="col-sm-6">
                            {/*<InputField
                            label={"Net Annual Income"}
                            data-testid={"net-annual-income"}
                            id={"net-annual-income"}
                            name="netAnnualIncome"
                            icon={<i className="zmdi zmdi-money"></i>}
                            type={"text"}
                            placeholder={"Amount"}
                            onChange={netAnnualIncomeOnChangeHandler}
                            onBlur={() => {
                                let netAnlIncome = Number(netAnnualIncome).toFixed(2);
                                if (netAnlIncome === "NaN") return;

                                else if(+netAnnualIncome == 0.0 || netAnnualIncome == undefined)
                                    setError("netAnnualIncome", {
                                        type: "server",
                                        message: "This field is required.",
                                    });

                                else
                                setNetAnnualIncome(CommaFormatted(String(netAnlIncome)));
                                //setMonthRent(String(rent))
                            }}

                            value={netAnnualIncome}
                            register={register}
                            rules={{
                                required: "This field is required.",
                            }}
                            errors={errors}
                        />*/}

                            <InputField
                                label={"Net Annual Income"}
                                data-testid={"net-annual-income"}
                                id={"net-annual-income"}
                                name="netAnnualIncome"
                                icon={<i className="zmdi zmdi-money"></i>}
                                value={netAnnualIncome ? CommaFormatted(netAnnualIncome) : ''}
                                type={'text'}
                                placeholder={"Amount"}
                                register={register}
                                rules={{
                                    required: "This field is required.",
                                }}
                                errors={errors}
                                onBlur={() => {
                                    let netAnlIncome = Number(netAnnualIncome).toFixed(2);
                                    if (netAnlIncome === "NaN") return;

                                    else if (+netAnnualIncome == 0.0 || netAnnualIncome == undefined)
                                        setError("netAnnualIncome", {
                                            type: "server",
                                            message: "This field is required.",
                                        });

                                    else
                                        setNetAnnualIncome(CommaFormatted(String(netAnlIncome)));
                                }}
                                onChange={(event: React.FormEvent<HTMLInputElement>) => {
                                    clearErrors("netAnnualIncome")
                                    const value = event.currentTarget.value;
                                    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
                                        return false;
                                    }
                                    setNetAnnualIncome(value.replace(/\,/g, ''))
                                    return true;
                                }}
                            />
                        </div>
                    </div>
                </div><div className="p-footer">
                    <button className="btn btn-primary" type="submit" onChange={handleSubmit(onSubmit)} data-testid="net-annual-income-submit">
                        SAVE INCOME SOURCE
                    </button>
                </div>
            </form>
        </React.Fragment>
    );
};
