import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import React, { ChangeEvent, useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  CurrentEmploymentDetails,
  EmploymentOtherIncomes,
  OtherDefaultIncomeTypes,
} from "../../../../../../../Entities/Models/Employment";
import { LocalDB } from "../../../../../../../lib/LocalDB";

import InputCheckedBox from "../../../../../../../Shared/Components/InputCheckedBox";
import InputField from "../../../../../../../Shared/Components/InputField";

import EmploymentActions from "../../../../../../../store/actions/EmploymentActions";
import { Store } from "../../../../../../../store/store";
import { ErrorHandler } from "../../../../../../../Utilities/helpers/ErrorHandler";
import { EmploymentIncomeActionTypes } from "../../../../../../../store/reducers/EmploymentIncomeReducer";
import { ApplicationEnv } from "../../../../../../../lib/appEnv";

import {
  CommaFormatted,
  removeCommaFormatting,
} from "../../../../../../../Utilities/helpers/CommaSeparteMasking";
type AdditionalIncomePostObj = {
  Bonus: boolean,
  Overtime: boolean,
  Commission: boolean,
  annualBonusInc: string,
  annualOvertimeInc: string,
  annualCommissionInc: string,
}
type otherIncomeTextFieldObj = {
  name: string,
  label: string,
}
export const AdditionalIncome = () => {
  const {
    register,
    errors,
    handleSubmit,
    getValues,
    setValue,
    clearErrors,
    unregister,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const { state, dispatch } = useContext(Store);
  const {
    employerInfo,
    employerAddress,
    wayOfIncome,
    additionalIncome,
  }: any = state.employment;
  const { loanInfo }: any = state.loanManager;
  const [incomeTypes, setIncomeTypes] = useState<OtherDefaultIncomeTypes[]>();
  const [checked, setChecked] = useState<boolean[]>([]);
  const [btnClick, setBtnClick] = useState<boolean>(false);
  const [fieldValue, setFieldValue] = useState<string>("");

  const otherIncomeTextField: otherIncomeTextFieldObj[] = [
    {
      name: "annualBonusInc",
      label: "Annual Bonus Income",
    },
    {
      name: "annualCommissionInc",
      label: "Annual Commission Income",
    },
    {
      name: "annualOvertimeInc",
      label: "Annual Overtime Income",
    },
  ];

  useEffect(() => {
    checked.map((c: boolean, idx: number) => {
        switch (idx) {
          case 0:
            if(c) registerField("annualBonusInc");
            else unregister("annualBonusInc");
            break;
          case 1:
            if(c) registerField("annualCommissionInc");
            else unregister("annualCommissionInc");
            break;
          case 2:
            if(c) registerField("annualOvertimeInc");
            else unregister("annualOvertimeInc");
            break;
        }
    });
  }, [checked]);

  useEffect(() => {
    getEmploymentOtherDefaultIncomeTypes();
  }, []);

  useEffect(() => {
    if (employerInfo) {
      if (additionalIncome) {
        setInitialData();
      }
    } else {
      NavigationHandler.navigateToPath(
        `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources`
      );
    }
  }, [incomeTypes]);

  const registerField = (fieldName: string) => {
    register(fieldName, {
      required: "This field is required.",
    });
  };

  const setInitialData = async () => {
    additionalIncome.map((income: EmploymentOtherIncomes) => {
      let id = incomeTypes?.filter((i) => i.id === income.incomeTypeId)[0];
      let field: HTMLInputElement | null = document.querySelector("#" + id?.name);
      if (field) field.click();  
      if (income.incomeTypeId === 1) {
        setValue("annualOvertimeInc", CommaFormatted(Number(income?.annualIncome)));
      } else if (income.incomeTypeId === 2) {
        setValue("annualBonusInc", CommaFormatted(Number(income?.annualIncome)));
      } else if (income.incomeTypeId === 3) {
        setValue("annualCommissionInc", CommaFormatted(Number(income?.annualIncome)));
      }
    });
  };

  const onSubmit = async (data: AdditionalIncomePostObj) => {
    if (!btnClick) {
      setBtnClick(true);
      let reqData: CurrentEmploymentDetails = preparePostAPIData(data);
      let res = await EmploymentActions.addOrUpdateCurrentEmployer(reqData);
      if (res) {
        if (ErrorHandler.successStatus.includes(res.statusCode)) {
          dispatch({
            type: EmploymentIncomeActionTypes.SetEmployerInfo,
            payload: undefined,
          });
          dispatch({
            type: EmploymentIncomeActionTypes.SetEmployerAddress,
            payload: undefined,
          });
          dispatch({
            type: EmploymentIncomeActionTypes.SetWayOfIncome,
            payload: undefined,
          });
          dispatch({
            type: EmploymentIncomeActionTypes.SetAdditionIncome,
            payload: undefined,
          });
          NavigationHandler.moveNext();
        } else {
          ErrorHandler.setError(dispatch, res);
        }
      }
    }
  };

  const preparePostAPIData = (data: AdditionalIncomePostObj) => {
    let otherIncomeData = filledOtherIncomeData(data);
    let currentEmploymentDetails: CurrentEmploymentDetails = {
      BorrowerId: +loanInfo.borrowerId,
      LoanApplicationId: +LocalDB.getLoanAppliationId(),
      EmploymentInfo: employerInfo,
      EmployerAddress: employerAddress,
      WayOfIncome: wayOfIncome,
      EmploymentOtherIncomes: otherIncomeData,
      State: NavigationHandler.getNavigationStateAsString(),
    };
    return currentEmploymentDetails;
  };

  const filledOtherIncomeData = (data: AdditionalIncomePostObj) => {
    
    let {
      Bonus,
      Overtime,
      Commission,
      annualBonusInc,
      annualOvertimeInc,
      annualCommissionInc,
    } = data;
    let allOtherIncomes: EmploymentOtherIncomes[] = [];
    let employmentOtherIncomes: EmploymentOtherIncomes;
    if (!!Bonus && Bonus) {
      employmentOtherIncomes = {
        IncomeTypeId: 2,
        AnnualIncome: Number(removeCommaFormatting(annualBonusInc)),
      };
      allOtherIncomes.push(employmentOtherIncomes);
    }
    if (!!Overtime && Overtime) {
      employmentOtherIncomes = {
        IncomeTypeId: 1,
        AnnualIncome: Number(removeCommaFormatting(annualOvertimeInc)),
      };
      allOtherIncomes.push(employmentOtherIncomes);
    }
    if (!!Commission && Commission) {
      employmentOtherIncomes = {
        IncomeTypeId: 3,
        AnnualIncome: Number(removeCommaFormatting(annualCommissionInc)),
      };
      allOtherIncomes.push(employmentOtherIncomes);
    }

    return allOtherIncomes;
  };

  const getEmploymentOtherDefaultIncomeTypes = async () => {
    let res: any = await EmploymentActions.getEmploymentOtherDefaultIncomeTypes();
    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        setIncomeTypes(res.data);
        let data = res.data;
        let checkedArr: boolean[] = [];
        data &&
          data?.length &&
          data.map(() => {
            checkedArr.push(false);
          });
        setChecked(checkedArr);
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const onCheckboxChecked = (e:ChangeEvent<HTMLInputElement>, idx: number) => {
    let val = e.target.value;
    if (val !== "true") {
      setChecked((prevState) => [
        ...prevState.map((item, index) => (index === idx ? true : item)),
      ]);
    } else {
      setChecked((prevState) => [
        ...prevState.map((item, index) => (index === idx ? false : item)),
      ]);
    }
  };

  const fieldOnChangeHandler = (event:ChangeEvent<HTMLInputElement>, name: string| undefined) => {
    if(name){
      clearErrors(name);
      const value = event.currentTarget.value;
      if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
        setValue(name, CommaFormatted(Number(fieldValue.replace(/\,/g, ""))));
          return false;
      }
      setFieldValue(CommaFormatted(Number(value.replace(/\,/g, ""))))
      setValue(name, CommaFormatted(Number(value.replace(/\,/g, ""))));
      return true;
    }
    
    return false

  };

  return (
    <div className="compo-additIncome">
      
    <div className="form-group">
    <h4>{`Select any additional income you received at ${employerInfo && employerInfo.EmployerName}`}</h4>
    </div>

      <div className="p-body">        
        <div className="row">
        <div className="col-sm-7">
          <div className="addit-info-wrap">
            <div className="input-heading">
              <h5>Please average each of the last two years</h5>
            </div>
            <form
              id="employer-info-form"
              data-testid="employer-info-form"
              onSubmit={handleSubmit(onSubmit)}
              autoComplete="off">
              {incomeTypes &&
                incomeTypes?.length &&
                incomeTypes?.map(
                  (incomeType: OtherDefaultIncomeTypes, idx: number) => (
                    <div className="form-group" key={idx}>
                      <InputCheckedBox
                      data-testid={incomeType?.name}
                        id={incomeType?.name}
                        className={incomeType?.name}
                        name={incomeType?.name}
                        label={incomeType?.displayName}
                        // checked={true}
                        value={
                          checked && checked.length && checked[idx]?.toString()
                        }
                        onChange={(e:ChangeEvent<HTMLInputElement>) => onCheckboxChecked(e, idx)}
                        register={register}
                        rules={
                          {
                            // required: "This field is required.",
                          }
                        }
                        errors={errors}></InputCheckedBox>

                      {/* {checked && checked[idx] && ( */}
                      <div
                        className={`chk-base-c && ${
                          checked && !checked[idx] && "d-none"
                        }`}>
                        <InputField
                          label={otherIncomeTextField[idx]?.label!}
                          data-testid={otherIncomeTextField[idx]?.name}
                          id={otherIncomeTextField[idx]?.name}
                          name={otherIncomeTextField[idx]?.name!}
                          icon={<i className="zmdi zmdi-money"></i>}
                          type={"text"}
                          placeholder={"Amount"}
                          register={register}
                          maxLength={15}
                          // value={fieldValues && fieldValues.length ? CommaFormatted(fieldValues[idx]):""}
                          rules={
                            {
                              // required: "This field is required.",
                            }
                          }
                          onChange={(e:ChangeEvent<HTMLInputElement>) =>
                            fieldOnChangeHandler(
                              e,
                              otherIncomeTextField[idx]?.name
                            )
                          }
                          onFocus = {()=> {
                            let values = getValues();
                            if(values && values[otherIncomeTextField[idx]?.name!]){
                              let value = values[otherIncomeTextField[idx]?.name!]
                              setFieldValue(CommaFormatted(Number(value.replace(/\,/g, ""))))
                            }
                            else setFieldValue("")}
                          }
                          errors={errors}
                        />
                      </div>
                      {/* )} */}
                      {/* {errors.incomeCheckbox && <span className="form-error pt-2 pb-0" role="alert" data-testid="performedMilitaryService-error">{errors.incomeCheckbox.message}</span>} */}
                    </div>
                  )
                )}
            </form>
          </div>
        </div>
      </div>
      </div>
      
      <div className="p-footer">
        <button
          className="btn btn-primary"
          type="button"
          // disabled={true}
          data-testid="additional-income-submit"
          id="additional-income-submit"
          onClick={handleSubmit(onSubmit)}>
          SAVE INCOME SOURCE
        </button>
      </div>
    </div>
  );
};
