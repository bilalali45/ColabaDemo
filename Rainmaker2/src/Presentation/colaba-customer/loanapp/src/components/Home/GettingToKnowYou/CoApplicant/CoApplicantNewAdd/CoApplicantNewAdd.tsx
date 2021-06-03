import React, {useState, useEffect, useContext } from "react";

import BorrowerActions from "../../../../../store/actions/BorrowerActions";
import CoApplicantNewAddList from './CoApplicantNewAddList'
import { LoanInfoType, ReviewBorrowerInfo } from "../../../../../Entities/Models/types";
import { LocalDB } from "../../../../../lib/LocalDB";

import { Store } from "../../../../../store/store";
import { LoanApplicationActionsType } from "../../../../../store/reducers/LoanApplicationReducer";
import LeftMenuHandler, { Decisions } from "../../../../../store/actions/LeftMenuHandler";
import { ApplicationEnv } from "../../../../../lib/appEnv";
import { PopupModal } from "../../../../../Shared/Components/Modal";

import Loader from "../../../../../Shared/Components/Loader";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";

export const CoApplicantNewAdd = ({ currentStep, setcurrentStep }: any) => {
  const { state, dispatch } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [allBorrowers, setAllBorowers] = useState<ReviewBorrowerInfo>();

  const [deletePopup, setDeletePopup] = useState<boolean>(false);
  const [borrowerId, setBorrowerId] = useState<number>(0);
  const [firstname, setFirstname] = useState<string>('');
  
  const [isClicked, setIsClicked] = useState<boolean>(false);
  const fetchData = async () => {
    await BorrowerActions.getBorrowersForFirstReview(Number(LocalDB.getLoanAppliationId()))
    .then((res)=> { setAllBorowers(res.data);});
}

  const deleteBorrower = async (borrowerId: number, firstName: string) => {
    setBorrowerId(borrowerId);
    setFirstname(firstName);
    setDeletePopup(true);
  }

  const addBorrower = async () => {
    if (!isClicked) {
      setIsClicked(true);
      let loanInfoObj: LoanInfoType = {
        ...loanInfo,
        borrowerId: null,
        ownTypeId: 2
      }
      await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
      LocalDB.clearBorrowerFromStorage();
      LeftMenuHandler.addDecision(Decisions.AddBrowerFromCoApplicant);
      NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself`);
    }
  }

  const editBorrower = async (borrowerId: number, ownTypeId:number) => {
    let loanInfoObj: LoanInfoType = {
      ...loanInfo,
      borrowerId: borrowerId,
      ownTypeId: ownTypeId
    }

    await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
    LocalDB.setBorrowerId(String(borrowerId))
    LeftMenuHandler.addDecision(Decisions.AddBrowerFromCoApplicant);
    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself?borrowerid=${borrowerId}`)
  }

  const saveAndContinue = async () => {
    console.log("Save and continue called");
    NavigationHandler.moveNext();
  }

  useEffect(() => {
    fetchData()
  }, []);



  const setBorrowerInfoInStore= async() => {
    if(loanInfo.borrowerId === borrowerId){
        let borrowerList = allBorrowers?.borrowerReviews;
      if (borrowerList) {
        let borrowerIdx = borrowerList.findIndex(borrower => borrower.borrowerId === borrowerId)
        let currentBorrower = borrowerList[borrowerIdx-1]
        if (currentBorrower) {
          let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: currentBorrower.borrowerId,
            ownTypeId: currentBorrower.ownTypeId,
            borrowerName:currentBorrower.firstName+' '+currentBorrower.lastName,
          }
          await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });    
        }
        
      }
      
    }
    
}

const confirmDeleteBorrower = async()=> {
  await BorrowerActions.deleteSecondaryBorrower(Number(LocalDB.getLoanAppliationId()), borrowerId);
  fetchData();
  setDeletePopup(false);
        setBorrowerInfoInStore();
}
  return allBorrowers ? <><CoApplicantNewAddList
    allBorrowers={allBorrowers}
    addBorrower={addBorrower}
    deleteBorrower={deleteBorrower}
    currentStep={currentStep}
    setcurrentStep={setcurrentStep}
    editBorrower={editBorrower}
    saveAndContinue={saveAndContinue} />
     <PopupModal
      modalHeading={`Remove ${firstname} ?`}
      modalBodyData={<p>Are You Sure You Want To Delete {firstname} From This
             Loan Application? The Data You've Entered Will Not Be Recoverable.</p>}
      modalFooterData={<button className="btn btn-min btn-primary" onClick={confirmDeleteBorrower}>Yes</button>}
      show={deletePopup}
      handlerShow={() => setDeletePopup(!deletePopup)}
      dialogClassName={`delete-borrower-popup`}
    ></PopupModal>
    </> : <Loader type="widget"/>

}
