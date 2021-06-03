import React from 'react';
import { useForm } from "react-hook-form";
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import InputField from '../../../../../../../../Shared/Components/InputField'
import { Iconcheck } from '../../../../../../../../Shared/Components/SVGs'
import { CommaFormatted } from '../../../../../../../../Utilities/helpers/CommaSeparteMasking';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler'


type props = { 
    setAccountSave :Function;
    hasAccountSave: boolean;
    institutionName: string;
    acountNumber: string;
    balance: string;
    setInstitutionName :Function;
    setacountNumber :Function;
    setBalance :Function;
    onSubmit: (data) => void;
}


export const RetirementAccountDetailsWeb = ({
    hasAccountSave,
    institutionName,
    acountNumber,
    balance,
    setInstitutionName,
    setacountNumber,
    setBalance,
    onSubmit,
}: props) => {

    const { register, errors, handleSubmit, clearErrors } = useForm({
        mode: "onSubmit",
        reValidateMode: "onBlur",
        criteriaMode: "firstError",
        shouldFocusError: true,
        shouldUnregister: true,
    });

    const institutionOnChangeHandler = (event) => {
        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[a-zA-Z0-9%&(.'\-\s]{1,150}$/g.test(value)) {
          return false
        }
        setInstitutionName(value)
        clearErrors("financialInstitution");
        return true;
      };


      const balanceOnChangeHandler = (event) => {
        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
            return false;
        }
        setBalance(value.replace(/\,/g, ''))
        clearErrors("currentBalance");
          return true;
      };

      const accountOnChangeHandler = (event) => {
        const value = event.currentTarget.value;
        if (value.length > 0 && !/^[0-9]{1,99}$/g.test(value)) {
            return false;
        }
        setacountNumber(value)
        clearErrors("accountNumber");
          return true;
      };

    return (
        <div className="RetirementAccountDetails">
            <form
                id="retirement-form"
                data-testid="retirement-form"
                autoComplete="off"
                >
            <div className="p-body">
                <div className="row">
                    <div className="col-md-6">
                    <InputField
                    data-testid="financialInstitution-input"
                                    name= "financialInstitution"
                                    label={`Financial Institution`}
                                    placeholder={`Business Name Here`}
                                    value = {institutionName}
                                    register={register}
                                    rules={{
                                    required: "This field is required.",
                                   }}
                                   onChange={(e) => {
                                    institutionOnChangeHandler(e)
                                }}
                                errors={errors}
                                />
                    </div>
                    <div className="col-md-6">
                                <InputField
                                data-testid="accountNumber-input"
                                    name= "accountNumber"
                                    label={`Account Number`}
                                    placeholder={`***-***-XXXX`}
                                    value={acountNumber}
                                    register={register}
                                    rules={{
                                    required: "This field is required.",
                                   }}
                                   onChange={(e) => {
                                    accountOnChangeHandler(e)
                                }}
                                errors={errors}
                                />
                            </div>
                    <div className="col-md-6">
                    <InputField
                    currentBalance-input= "currentBalance-input"
                                    icon={<i className="zmdi zmdi-money"></i>}
                                    name= "currentBalance"
                                    label={`Current Market Value`}                                  
                                    placeholder={`Amount`}
                                    value={CommaFormatted(balance)}
                                    register={register}
                                    rules={{
                                    required: "This field is required.",
                                   }}
                                   onChange={(e) => {
                                    balanceOnChangeHandler(e)
                                }}
                                errors={errors}
                                />
                    </div>
                </div>

                {hasAccountSave &&
                <div  className="col-md-6">
               <span data-testid="save-msg" className="msg-succeed">
                 <Iconcheck /> Your Account Has Been Saved!
               </span>
              </div>}

              {!hasAccountSave &&
                    <div className="form-footer">
                        <button
                            data-testid="saveBtn"
                            className="btn btn-primary float-right"
                            onClick={handleSubmit(onSubmit)}>
                        
                            {"Save Assets"}
                        </button>
                    </div>
                   }

            </div>
         {hasAccountSave &&
            <div data-testid="saved-actions" className="form-footer">
                <button
                    data-testid="AddAnotherAssetBtn"
                    disabled={false}
                    className="btn btn-primary float-right"
                    onClick={() => {
                        NavigationHandler.moveNext();

                    }}>Add another asset</button>
                <button
                    data-testid="AddAnotherRetirementAccountBtn"
                    disabled={false}
                    className="btn btn-secondary float-right"
                    onClick={() => {
                        NavigationHandler.moveBack()
                            setTimeout(() => {
                                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/RetirementAccount/RetirementAccountDetails`);
                            },0)
                     }}
                >Add Another Retirement Account
                </button>

            </div>
            }
</form>
        </div>
    )
}
