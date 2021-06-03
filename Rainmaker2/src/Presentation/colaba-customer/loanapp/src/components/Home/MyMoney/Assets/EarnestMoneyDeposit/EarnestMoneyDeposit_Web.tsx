import React, { useContext } from 'react'
import { useForm } from 'react-hook-form';
import InputField from '../../../../../Shared/Components/InputField';
import InputRadioBox from '../../../../../Shared/Components/InputRadioBox';
import PageHead from '../../../../../Shared/Components/PageHead';
import TooltipTitle from '../../../../../Shared/Components/TooltipTitle';
import { Store } from '../../../../../store/store';
import { CommaFormatted } from '../../../../../Utilities/helpers/CommaSeparteMasking';
import { StringServices } from '../../../../../Utilities/helpers/StringServices';

type props = {
    setEarnestMoneyDeposit: Function;
    hasEarnestMoneyDeposit: boolean | null;
    onSubmit: (data) => void;
    setAmount: Function;
    amount: string | null;
}

export const EarnestMoneyDepositWeb = ({ setEarnestMoneyDeposit, hasEarnestMoneyDeposit, onSubmit, amount, setAmount }: props) => {

    const { state } = useContext(Store);
    const { loanInfo }: any = state.loanManager;

    const {
        register,
        errors,
        handleSubmit,
        clearErrors
    } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "all",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    return (
        <div className="compo-assets fadein">

            <PageHead title="Assets" />

            <TooltipTitle title={`${StringServices.capitalizeFirstLetter(loanInfo?.borrowerName)} Please tell us about yours and co-applicant Assets`} />

            <form
                id="earnest-money-form"
                data-testid="earnest-money-form"
                onSubmit={handleSubmit(onSubmit)}
                autoComplete="off">

                <div className="comp-form-panel assets-panel colaba-form">
                    <div className="row form-group">
                        <div className="col-md-12">
                            <div className="listaddress-warp">
                                <div className="list-add">
                                    <div data-testid="subtitle" className="cont-add">
                                        Have you made an earnest money deposit on this purchase?
                            </div>
                                </div>
                            </div>

                        </div>


                    </div>
                    <div className="row form-group">
                        <div className="col-md-6">
                            <InputRadioBox
                                dataTestId="earnestMoney"
                                id=""
                                className=""
                                name="earnestMoney"
                                checked={hasEarnestMoneyDeposit  === true ? true : hasEarnestMoneyDeposit}
                                value={"Yes"}
                                onChange={() => {                                   
                                    setEarnestMoneyDeposit(true);
                                }}
                                register={register}
                                rules={{
                                    required: "Please select one",
                                }}
                                errors={errors}
                            >Yes</InputRadioBox>

                            {hasEarnestMoneyDeposit &&
                                <div className="earnestMoney-field-Wrap">
                                    <InputField
                                        label={"Deposit Amount"}
                                        data-testid="earnestMoneyDAmount-input"
                                        id=""
                                        name="earnestMoneyDAmount"
                                        icon={<i className="zmdi zmdi-money"></i>}
                                        type={"text"}
                                        placeholder={"Amount"}
                                        register={register}
                                        value={CommaFormatted(amount)}
                                        rules={{
                                            required: "This field is required.",
                                        }}
                                        errors={errors}
                                        onChange={(event) => {
                                            clearErrors();
                                            let value = event.target.value;
                                            if (value.length > 0 && !/^[0-9,]{1,11}$/g.test(value)) {
                                                return false;
                                            }
                                            setAmount(value.replace(/\,/g, ''));
                                            return true
                                        }}

                                    />
                                </div>
                            }


                        </div>
                        {(hasEarnestMoneyDeposit === true || hasEarnestMoneyDeposit === false) &&
                          <div className="col-md-12">
                          <InputRadioBox
                              dataTestId="earnestMoney-2"
                              id=""
                              className=""
                              name="earnestMoney"
                              checked={hasEarnestMoneyDeposit === true ? false : true}
                              value={"No"}
                              onChange={() => {                                  
                                  setEarnestMoneyDeposit(false);
                              }}
                              register={register}
                              rules={{
                                  required: "Please select one",
                              }}
                              errors={errors}
                          >No</InputRadioBox>
                      </div>

                        }
                        {hasEarnestMoneyDeposit === null &&
                          <div className="col-md-12">
                          <InputRadioBox
                              dataTestId="earnestMoney-2"
                              id=""
                              className=""
                              name="earnestMoney"
                              value={"No"}
                              onChange={() => {                                  
                                  setEarnestMoneyDeposit(false);
                              }}
                              register={register}
                              rules={{
                                  required: "Please select one",
                              }}
                              errors={errors}
                          >No</InputRadioBox>
                      </div>

                        }
                        {errors?.earnestMoney && <span className="form-error no-padding" role="alert" data-testid="earnest-error">{errors?.earnestMoney?.message}</span>}
                    </div>
                    <div className="form-footer">
                        <button
                            data-testid="submitBtn"
                            className="btn btn-primary"
                            onClick={handleSubmit(onSubmit)}>
                            {"Save & Continue"}
                        </button>
                    </div>

                </div>
            </form>

        </div>
    );
}
