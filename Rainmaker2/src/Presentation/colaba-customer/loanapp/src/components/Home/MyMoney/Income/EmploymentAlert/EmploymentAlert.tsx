import React, { useContext, useEffect, useState } from "react";
import {
  BorrowerEmploymentHistory,
  BorrowersEmploymentHistoryList,
} from "../../../../../Entities/Models/EmploymentHistory";
import { ApplicationEnv } from "../../../../../lib/appEnv";
import { LocalDB } from "../../../../../lib/LocalDB";
import { PopupModal } from "../../../../../Shared/Components/Modal";
import EmploymentHistoryActions from "../../../../../store/actions/EmploymentHistoryActions";
import { Store } from "../../../../../store/store";
import { ErrorHandler } from "../../../../../Utilities/helpers/ErrorHandler";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { MyMoneyIncomeSteps } from "../../../../../store/actions/LeftMenuHandler";
import { EmploymentHistoryActionTypes } from "../../../../../store/reducers/EmploymentHistoryReducer";


export const EmploymentAlert = () => {
  const { dispatch } = useContext(Store);
  const [borrowersName, setBorrowersName] = useState<string[]>([]);
  const [showPopUp, setShowPopUp] = useState<boolean>(false)

  useEffect(() => {
    getBorrowersEmploymentHistory();
  }, []);

  const getBorrowersEmploymentHistory = async () => {
    let res: any = await EmploymentHistoryActions.getBorrowersEmploymentHistory(
      +LocalDB.getLoanAppliationId()
    );

    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        const {
          borrowerEmploymentHistory,
          requiresHistory,
        }: BorrowersEmploymentHistoryList = res.data;

        if (requiresHistory) {
          setShowPopUp(true)
          
          NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentAlert)
          NavigationHandler.enableFeature(MyMoneyIncomeSteps.EmploymentHistory)
          let borrowerNameList: string[] = [];
          let borrowersId: number[] = [];
          borrowerEmploymentHistory &&
            borrowerEmploymentHistory?.length &&
            borrowerEmploymentHistory.map(
              (borrower: BorrowerEmploymentHistory) => {
                borrowerNameList.push(borrower.borrowerName);
                borrowersId.push(borrower.borrowerId)
              }
            );
            dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersId, payload: borrowersId });
            dispatch({ type: EmploymentHistoryActionTypes.SetBorrowersIncome, payload: [] });
          setBorrowersName(borrowerNameList);
        }
        else{
          NavigationHandler.disableFeature(MyMoneyIncomeSteps.EmploymentAlert)
          NavigationHandler.disableFeature(MyMoneyIncomeSteps.EmploymentHistory)
          NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeReview`);
        }
      } else {
        ErrorHandler.setError(dispatch, res);
      }
    }
  };

  
  const EmploymentAlertModalData = () => {
    return (
      <div className="emp-alert-data">
        <p data-testid="alert-text">
          Looks like the following applicants don’t have two years of employment
          history yet
        </p>
        <ul>
            {
                borrowersName && borrowersName?.length && borrowersName?.map((borrower:string, idx:number)=> <li key={idx}>{borrower}</li>)
            }
        </ul>
        <p>No problem! We’ll collect that on the next page</p>
      </div>
    );
  };

  return (
    <PopupModal
    modalHeading={`Employment Alert`}
    modalBodyData={EmploymentAlertModalData()}
    modalFooterData={
      <button className="btn btn-min btn-primary" id="alert-continue-btn" data-testid="alert-continue-btn" onClick={()=>{
        NavigationHandler.disableFeature(
          MyMoneyIncomeSteps.EmploymentAlert
        );
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory`)}}>Continue</button>
        // history.push(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory`)}}>Continue</button>
        
    }
    show={showPopUp}
    handlerShow={() => {setShowPopUp(!showPopUp); NavigationHandler.moveBack();}}
    dialogClassName={`employment-alert-popup`}></PopupModal>
    )
};
