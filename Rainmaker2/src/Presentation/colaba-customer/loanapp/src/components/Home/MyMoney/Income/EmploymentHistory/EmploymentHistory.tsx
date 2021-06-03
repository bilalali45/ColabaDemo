import React, { useEffect, useContext } from "react";

import { PreviousEmploymentTabs } from "./PreviousEmploymentList/PreviousEmploymentTabs";
import EmploymentHistoryActions from "../../../../../store/actions/EmploymentHistoryActions";
import { LocalDB } from "../../../../../lib/LocalDB";
import { ErrorHandler } from "../../../../../Utilities/helpers/ErrorHandler";
import {
  BorrowerIncome,
  Income,
} from "../../../../../Entities/Models/EmploymentHistory";
import { Store } from "../../../../../store/store";
import PageHead from "../../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../../Shared/Components/TooltipTitle";
import { Switch } from "react-router";
import { ApplicationEnv } from "../../../../../lib/appEnv";
import { EmploymentHistoryDetails } from "./EmploymentHistoryDetails/EmploymentHistoryDetails";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { EmploymentHistoryActionTypes } from "../../../../../store/reducers/EmploymentHistoryReducer";
import BusinessActions from "../../../../../store/actions/BusinessActions";
import { LoanInfoType } from "../../../../../Entities/Models/types";
import { LoanApplicationActionsType } from "../../../../../store/reducers/LoanApplicationReducer";
import { IsRouteAllowed } from "../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed";


const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory`;


export const EmploymentHistory = () => {


  const { state, dispatch } = useContext(Store);

  const { borrowersId, borrowersIncome }: any = state.employmentHistory;
  const { loanInfo }: any = state.loanManager;

  const allowedCategoryIds: number[] = [1, 2, 3, 4]

  useEffect(() => {

    if (borrowersId && borrowersId.length) {
      getAllBorrowersData()
    }
    
  }, []);

  const getAllBorrowersData = async () => {
    dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersIncome, payload: [] });
    let allBorrower: BorrowerIncome[] = [];
    if (borrowersId && borrowersId.length) {
      for (let id in borrowersId) {
        let res = await getBorrowersEmploymentHistory(+borrowersId[id]);
        let borrowerData = getFilteredIncomesOfBorrower(res)
        allBorrower.push(borrowerData);
      };
      if (allBorrower && allBorrower.length) {
        let loanInfoObj: LoanInfoType = {
          ...loanInfo,
          borrowerId: allBorrower[0]?.borrowerId,
          borrowerName: allBorrower[0]?.borrowerName,
          ownTypeId: null,
        }
        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
      }


      dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersIncome, payload: allBorrower });
    }
  }

  const getFilteredIncomesOfBorrower = (borrowerData:BorrowerIncome) => {
    console.log(borrowerData)
    let {borrowerIncomes} = borrowerData;
    if(borrowerIncomes){
      let incomes: Income[] = [];
      borrowerIncomes.map((income:Income)=>{
        if(allowedCategoryIds.includes(income?.employmentCategory?.categoryId) )
          incomes.push(income)
      })
      borrowerData.borrowerIncomes = incomes;
    }
    return borrowerData;
  }


  const getBorrowersEmploymentHistory = async (id: number) => {
    let res: any = await EmploymentHistoryActions.GetBorrowerIncomes(
      +LocalDB.getLoanAppliationId(), id
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {

        // await dispatch({ type: EmploymentHistoryActionTypes.SetUpdatedBorrowerIncome, payload:res.data });
        return res.data;
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  const getBorrowerUpdatedData = async (borrowerId: number) => {

    let res: any = await EmploymentHistoryActions.GetBorrowerIncomes(
      +LocalDB.getLoanAppliationId(), borrowerId
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        let allBorrowersIncome: BorrowerIncome[] = borrowersIncome && borrowersIncome.map((item: BorrowerIncome) => {
          if (item.borrowerId === loanInfo?.borrowerId) return res.data;
          return item;

        })

        dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersIncome, payload: allBorrowersIncome });
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }


  }

  const flushIncomeData = async () => {
    await dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: {} });
  }

  const deleteIncome = async (borrowerId: number, employmentHist: Income) => {
    await BusinessActions.deleteEmploymentIncome(Number(LocalDB.getLoanAppliationId()), borrowerId, employmentHist.incomeInfoId);
    getBorrowerUpdatedData(borrowerId)
  }

  const editIncome = async (borrowerId: number, borrowerName: string, owntypeId: number, borrowerIncome: Income) => {

    let loanInfoObj: LoanInfoType = {
      ...loanInfo,
      borrowerId: borrowerId,
      borrowerName: borrowerName,
      ownTypeId: owntypeId,
    }

    flushIncomeData();

    await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
    await dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: { incomeId: borrowerIncome?.incomeInfoId, incomeTypeId: borrowerIncome?.incomeType?.incomeTypeId } });
    await dispatch({ type: EmploymentHistoryActionTypes.SetIncomeInformation, payload: { incomeId: borrowerIncome?.incomeInfoId, incomeTypeId: borrowerIncome?.incomeType?.incomeTypeId } });
    LocalDB.setIncomeId(String(borrowerIncome?.incomeInfoId))
    LocalDB.setBorrowerId(String(borrowerId))


    if (borrowerIncome?.isCurrentEmployment) {
      switch (borrowerIncome?.employmentCategory?.categoryId) {
        case 1:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/EmploymentIncome`)
          break

        case 2:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome/SelfEmploymentIncome`)
          break

        case 3:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessIncomeType`)
          break

        case 4:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military/MilitaryIncome`)
          break

        case 5:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Retirement/RetirementIncomeSource`)
          break

        case 6:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Rental/RentalIncome`)
          break;

        case 7:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncomeDetails`)
          break;

        case 11:
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncomeDetails`)
          break
        default:
          NavigationHandler.navigateToPath(`/MyMoney/Income/IncomeHome/IncomeSourcesHome/${borrowerIncome?.employmentCategory?.categoryName}/${borrowerIncome?.employmentCategory?.categoryName}IncomeType`)
      }
    }
    else {
      NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory/EmploymentHistoryDetails/PreviousEmployment`)
    }

  }

  if (!borrowersId && !borrowersId?.length) {
    NavigationHandler.navigateToPath(
      `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome`
    );
    return <React.Fragment />;
}
  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead title="Employment History" handlerBack={() => { }} />
      <TooltipTitle
        title={`Looks like we’re missing some employment history. We’ll try to capture that here`}
      />
      <div className="comp-form-panel ssn-panel colaba-form">
        <div className="row">
          <div className="col-md-12">
            <section>

              <PreviousEmploymentTabs deleteIncome={deleteIncome} editIncome={editIncome} />

              <Switch>
                <IsRouteAllowed path={`${containerPath}/EmploymentHistoryDetails`} component={EmploymentHistoryDetails} />
              </Switch>


            </section>
          </div>
        </div>
      </div>
    </div>
  );
};
