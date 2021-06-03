import React, { useEffect, useState, useContext } from 'react';

import { LoanInfoType, LoanpurposeProto, ReviewBorrowerInfo } from '../../../../Entities/Models/types';

import { CommonActions } from '../../../../store/actions/CommonActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { LocalDB } from '../../../../lib/LocalDB';
import { ApplicaitonReviewMyNewMortgageList } from './ApplicaitonReviewMyNewMortgageList';
import LeftMenuHandler, { Decisions } from '../../../../store/actions/LeftMenuHandler';
import { ApplicationEnv } from '../../../../lib/appEnv';

import Loader from '../../../../Shared/Components/Loader';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';


export const ApplicaitonReviewMyNewMortgage = ({ currentStep, setcurrentStep }: any) => {

  const { state, dispatch } = useContext(Store);
  const loanManager: any = state.loanManager;
  const loanInfo: LoanInfoType = loanManager.loanInfo;
  const [reviewersList] = useState<ReviewBorrowerInfo>();
  const [maritalStatus, setMaritalStatus] = useState([]);
  const [loanpurposes, setLoanpurposes] = useState<LoanpurposeProto[]>();
  const fetchData = async () => {
    //maritalStatus = [..._maritalStatuses];
    let maritalStatuses: any = await CommonActions.getAllMaritialStatuses();
    setMaritalStatus(maritalStatuses);

    let loanPurposes: any = await CommonActions.getAllloanpurpose();
    setLoanpurposes(loanPurposes);

  }
  useEffect(() => {
    fetchData()
  }, []);

  function resolveMaritialStatus(id: number) {
    let maratialStatusName = maritalStatus.find((ms:any) => ms.id == id);
    return maratialStatusName ? maratialStatusName : null;
  }

  function resolveLoanPurpose(id: number) {
    let loanPurpose = loanpurposes && loanpurposes.find((ms) => ms.id == id);
    return loanPurpose ? loanPurpose : null;
  }

  const editBorrower = async (borrowerId: number, ownTypeId: number) => {
    let loanInfoObj: LoanInfoType = {
      ...loanInfo,
      borrowerId: borrowerId,
      ownTypeId: ownTypeId
    }

    await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
    LocalDB.setBorrowerId(String(borrowerId))
    LeftMenuHandler.addDecision(Decisions.AddBrowerFromMyNewMortgageReview);
    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself?borrowerid=${borrowerId}`)
  }

  const saveAndContinue = async () => {
    console.log("Save and continue called");
    NavigationHandler.moveNext();
  }

  return reviewersList ? <><ApplicaitonReviewMyNewMortgageList
    reviewBorrowerInfo={reviewersList}
    currentStep={currentStep}
    setcurrentStep={setcurrentStep}
    resolveMaritialStatus={resolveMaritialStatus}
    resolveLoanPurose={resolveLoanPurpose}
    editBorrower={editBorrower}
    saveAndContinue={saveAndContinue} />
    </> : <Loader type="widget"/>
}
