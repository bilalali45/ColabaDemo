import _ from "lodash";
import React, { ChangeEvent, useContext, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { BorrowerIncome, EmploymentHistoryDetails } from "../../../../../../Entities/Models/EmploymentHistory";
import { ApplicationEnv } from "../../../../../../lib/appEnv";

import { LocalDB } from "../../../../../../lib/LocalDB";
import InputField from "../../../../../../Shared/Components/InputField";
import EmploymentHistoryActions from "../../../../../../store/actions/EmploymentHistoryActions";
import { EmploymentHistoryActionTypes } from "../../../../../../store/reducers/EmploymentHistoryReducer";
import { Store } from "../../../../../../store/store";
import {
  CommaFormatted,
  removeCommaFormatting,
} from "../../../../../../Utilities/helpers/CommaSeparteMasking";
import { ErrorHandler } from "../../../../../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";

type PreviousEmploymentAmountPostObj = {
  netAnnualIncome: string;
}

type PreviousEmploymentAmountObj = {
  employerAnnualSalary: string;
}

export const PreviousEmploymentAmount = () => {
  const { state, dispatch } = useContext(Store);
  const {
    borrowersIncome,
    previousEmployerInfo,
    previousEmployerAddress,
    previousEmploymentIncome
  }: any = state.employmentHistory;
  const { loanInfo }: any = state.loanManager;
  const [netAnnualIncome, setNetAnnualIncome] = useState<string>("");
  const [btnClick, setBtnClick] = useState<boolean>(false);

  const {
    register,
    errors,
    handleSubmit,
    clearErrors,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "all",
    shouldFocusError: true,
    shouldUnregister: true,
  });


  useEffect(()=>{
    if(previousEmploymentIncome){
      const{employerAnnualSalary} = previousEmploymentIncome;
      let wayOfIncome = {
        employerAnnualSalary:employerAnnualSalary,
      }
      setInitialData(wayOfIncome)
    }
    
  },[previousEmployerInfo])

  const setInitialData = (data: PreviousEmploymentAmountObj) =>{
    if(!_.isEmpty(data)){
      const{employerAnnualSalary} = data;
      setNetAnnualIncome(employerAnnualSalary);
      
    }
  }
  
  const onSubmit = async (data:PreviousEmploymentAmountPostObj) => {
    
      let reqData: EmploymentHistoryDetails = preparePostAPIData(data);
      let res = await EmploymentHistoryActions.addOrUpdatePreviousEmploymentDetail(
        reqData
      );
      if (res) {
        if (ErrorHandler.successStatus.includes(res.statusCode)) {
          if (!btnClick) {
            setBtnClick(true);
          dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerInfo, payload: undefined});
          dispatch({type: EmploymentHistoryActionTypes.SetPreviousEmployerAddress, payload: undefined});
          getAllBorrowersData()
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory`);
          }
        } else {
          ErrorHandler.setError(dispatch, res);
        }
    }
  };
  const preparePostAPIData = (data:PreviousEmploymentAmountPostObj) => {
    let prevEmploymentData: EmploymentHistoryDetails = {
      LoanApplicationId: +LocalDB.getLoanAppliationId(),
      BorrowerId: loanInfo.borrowerId,
      State: NavigationHandler.getNavigationStateAsString(),
      EmploymentInfo: previousEmployerInfo,
      EmployerAddress: previousEmployerAddress,
      WayOfIncome: {
        EmployerAnnualSalary: data && Number(removeCommaFormatting(data?.netAnnualIncome)),
      },
    };
    return prevEmploymentData;
  };

  const getAllBorrowersData = async() =>{
    let res = await getBorrowersEmploymentHistory(loanInfo?.borrowerId);
    let allBorrowersIncome:BorrowerIncome[] = borrowersIncome && borrowersIncome.map((item:BorrowerIncome)=>{
      if(item.borrowerId === loanInfo.borrowerId) return res;
      return item;

    })
    
    dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersIncome, payload: allBorrowersIncome });
  }
  const getBorrowersEmploymentHistory = async (id:number) => {
    let res: any = await EmploymentHistoryActions.GetBorrowerIncomes(
      +LocalDB.getLoanAppliationId(), id
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        return res.data;
        
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const netAnnualIncomeOnChangeHandler = (event: ChangeEvent<HTMLInputElement>) => {
    clearErrors("netAnnualIncome");
    const value = event.currentTarget.value;
    if (value.length > 0 && !/^[0-9,]{1,20}$/g.test(value)) {
        return false;
    }
    setNetAnnualIncome(value.replace(/\,/g, ''))
    return null
  };

  return (
    <React.Fragment>
      <div className="row">
        <div className="col-sm-12">
          <h4>{`How much did you make at ${
            previousEmployerInfo && previousEmployerInfo.EmployerName
          }?`}</h4>
        </div>
      </div>
      <form
        id="net-annual-income-form"
        data-testid="net-annual-income-form"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off">
          <div className="p-body">
        <div className="row">
          <div className="col-sm-6">
            <InputField
              label={"Net Annual Income"}
              data-testid={"net-annual-income"}
              id={"net-annual-income"}
              name="netAnnualIncome"
              icon={<i className="zmdi zmdi-money"></i>}
              type={"text"}
              placeholder={"Amount"}
              onChange={netAnnualIncomeOnChangeHandler}
              value={netAnnualIncome? CommaFormatted(netAnnualIncome):""}
              register={register}
              rules={{
                required: "This field is required.",
                validate: {
                  validity: value => !(value === "0.00")  || "Amount must be greater than 0"
                },
              }}
              errors={errors}
            />
          </div>
        </div>
        </div>
        <div className="p-footer">
          <button className="btn btn-primary" id="prev-emp-amt-btn" data-testid="prev-emp-amt-btn" type="submit" onClick={handleSubmit(onSubmit)}>
            SAVE EMPLOYMENT HISTORY
          </button>
        </div>
      </form>
    </React.Fragment>
  );
};
