import React from 'react'
import { useForm } from "react-hook-form";
import { AssetTypesByCategory } from '../../../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../../../lib/appEnv';
import IconRadioBox from '../../../../../../../../Shared/Components/IconRadioBox';
import InputField from '../../../../../../../../Shared/Components/InputField';
import {
    IconCheckingAccount,
    IconSavingsAccount,
    Iconcheck
} from "../../../../../../../../Shared/Components/SVGs";
import { CommaFormatted } from '../../../../../../../../Utilities/helpers/CommaSeparteMasking';
import { NavigationHandler } from '../../../../../../../../Utilities/Navigation/NavigationHandler';

type props = { 
    setAccountSelected: Function;
    accountSeleced: AssetTypesByCategory;
    setAccountSave :Function;
    hasAccountSave: boolean;
    institutionName: string;
    acountNumber: string;
    balance: string;
    setInstitutionName :Function;
    setacountNumber :Function;
    setBalance :Function;
    onSubmit: (data) => void;
    assetTypesByCategory: AssetTypesByCategory[];
}

export const DetailsOfBankAccountWeb = ({ 
    accountSeleced, 
    setAccountSelected,
    hasAccountSave,
    institutionName,
    acountNumber,
    balance,
    setInstitutionName,
    setacountNumber,
    setBalance,
    onSubmit,
    assetTypesByCategory
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

      const RenderBankAccountType = (assetTypesByCategories: AssetTypesByCategory[]) => {
        const icons = {
            'Checking Account': <IconCheckingAccount />,
            'Savings Accoount': <IconSavingsAccount />,
          }
          return (
              <>
              {assetTypesByCategories?.map((item: AssetTypesByCategory) => {
                     return(
                  <div className="col-md-6">
                  <IconRadioBox
                      id={item.id}
                      className= {accountSeleced?.id === item.id ? "active" :''}
                      name="radio1"
                      checked={accountSeleced?.id === item.id ?  true: false}
                      value={item.name}
                      title={item.displayName}
                      Icon={icons[item.name]}
                      handlerClick = {() => {
                          setAccountSelected(item)
                      }}
                  />
              </div>
                     )
              })

              }
              </>
          )
      }
    return (
        <div>
            <form
                id="bank-form"
                data-testid="bank-form"
                autoComplete="off"
                >

                <div className="p-body">
                    <div data-testid="sub-title" className="form-group">
                        <h3 className="h3">What type of account is it?</h3>
                    </div>
                    <div className="row">
                        {
                           RenderBankAccountType(assetTypesByCategory) 
                        }
                    </div>
                    {!!accountSeleced &&
                        <div className="row">
                            <div className="col-md-6">
                                <InputField
                                    data-testid="financialInstitution-input"
                                    name= "financialInstitution"
                                    label={`Financial Institution`}
                                    placeholder={`Financial Institution Name`}
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
                                    data-testid="currentBalance-input"
                                    icon={<i className="zmdi zmdi-money"></i>}
                                    name= "currentBalance"
                                    label={`Current Balance`}                                  
                                    placeholder={`00.00`}
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
                    }
             {hasAccountSave &&
                <div data-testid="save-msg" className="col-md-6">
               <span className="msg-succeed">
                 <Iconcheck /> Your Account Has Been Saved!
               </span>
              </div>}
             

                   {!!accountSeleced && !hasAccountSave &&
                    <div data-testid="" className="form-footer">
                        <button
                            data-testid="saveBtn"
                            className="btn btn-primary float-right"
                            onClick={handleSubmit(onSubmit)}>
                        
                            {"Save Assets"}
                        </button>
                    </div>
                   }

                   {hasAccountSave &&
                    <div data-testid="saved-actions" className="form-footer">
                    <button
                        data-testid="AddAnotherAssetBtn"
                        disabled={false}
                        className="btn btn-primary float-right"
                        onClick={() => { 
                            NavigationHandler.moveNext();
                        }}
                        >Add another asset</button>
                    <button
                        data-testid="AddAnotherBankAccountBtn"
                        disabled={false}
                        className="btn btn-secondary float-right"
                        onClick={() => {                         
                           NavigationHandler.moveBack()
                            setTimeout(() => {
                                NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/BankAccount/DetailsOfBankAccount`);                            
                            },0)
                        }}
                    >Add Another Bank Account
                    </button>
    
                </div> }
                    


                </div>
            </form>
        </div>

    )
}
