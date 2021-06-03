import React, { useEffect, useState, useContext } from 'react';

import { LoanInfoType, LoanpurposeProto, ReviewBorrowerInfo } from '../../../../Entities/Models/types';

import { CommonActions } from '../../../../store/actions/CommonActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { LocalDB } from '../../../../lib/LocalDB';
import { ApplicaitonReviewAfterConsentList } from './ApplicaitonReviewAfterConsentList';
import LeftMenuHandler, { Decisions } from '../../../../store/actions/LeftMenuHandler';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { PopupModal } from '../../../../Shared/Components/Modal';
import BorrowerActions from '../../../../store/actions/BorrowerActions';
import Loader from '../../../../Shared/Components/Loader';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { TenantConfigFieldNameEnum } from '../../../../Utilities/Enumerations/TenantConfigEnums';
import { MyNewMortgageSteps } from '../../../../Utilities/Navigation/navigation_config/MyNewMortgage';
import LoanAplicationActions from '../../../../store/actions/LoanAplicationActions';


export const ApplicaitonReviewAfterConsent = ({ currentStep, setcurrentStep }: any) => {

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const [deletePopup, setDeletePopup] = useState<boolean>(false);
    const [borrowerId, setBorrowerId] = useState<number>(0);
    const [firstname, setFirstname] = useState<string>('');

    const [reviewersList, setReviewersList] = useState<ReviewBorrowerInfo>();
    const [maritalStatus, setMaritalStatus] = useState([]);
    const [loanpurposes, setLoanpurposes] = useState<LoanpurposeProto[]>();
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const fetchData = async () => {
        //maritalStatus = [..._maritalStatuses];
        let maritalStatuses: any = await CommonActions.getAllMaritialStatuses();
        setMaritalStatus(maritalStatuses);

        let loanPurposes: any = await CommonActions.getAllloanpurpose();
        setLoanpurposes(loanPurposes);
        await LoanAplicationActions.getLoanApplicationFirstReview(Number(LocalDB.getLoanAppliationId())).then((res) => {
            setReviewersList(res.data);
        });
    }
    useEffect(() => {
        fetchData()
    }, []);

    function resolveMaritialStatus(id: number) {
        let maratialStatusName = maritalStatus.find((ms) => ms.id == id);
        return maratialStatusName ? maratialStatusName : null;
    }

    function resolveLoanPurpose(id: number) {
        let loanPurpose = loanpurposes?.find((ms) => ms.id == id);
        return loanPurpose ? loanPurpose : null;
    }

    const deleteBorrower = async (borrowerId: number, firstName: string) => {
        setBorrowerId(borrowerId);
        setFirstname(firstName);
        setDeletePopup(true);
    }

    const editBorrower = async (borrowerId: number, ownTypeId: number) => {
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: borrowerId,
            ownTypeId: ownTypeId
        }

        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        LocalDB.setBorrowerId(String(borrowerId))
        LeftMenuHandler.addDecision(Decisions.AddBrowerFromApplicationReviewAfterConsent);
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself?borrowerid=${borrowerId}`)
    }

    const saveAndContinue = async () => {
        if (!isClicked) {
            setIsClicked(true);
            if (!NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.PropertyTypeSubjectProperty)) {
                NavigationHandler.disableFeature(MyNewMortgageSteps.SubjectPropertyNewHome);
            }
            else {
                NavigationHandler.enableFeature(MyNewMortgageSteps.SubjectPropertyNewHome);
            }
            NavigationHandler.moveNext();
        }
    }

    const setBorrowerInfoInStore = async () => {
        if (loanInfo.borrowerId === borrowerId) {
            let borrowerList = reviewersList?.borrowerReviews;
            let borrowerIdx = borrowerList?.findIndex(borrower => borrower.borrowerId === borrowerId)
            let currentBorrower = borrowerList[borrowerIdx - 1]
            if(currentBorrower){
                let loanInfoObj: LoanInfoType = {
                    ...loanInfo,
                    borrowerId: currentBorrower?.borrowerId,
                    ownTypeId: currentBorrower?.ownTypeId,
                    borrowerName: currentBorrower?.firstName+' '+currentBorrower?.lastName,
                }
                await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
            }
            
            
        }

    }

    const confirmDeleteBorrower = async () => {
        await BorrowerActions.deleteSecondaryBorrower(Number(LocalDB.getLoanAppliationId()), borrowerId);
        fetchData();
        setDeletePopup(false);
        setBorrowerInfoInStore();
    }

    return reviewersList ? <><ApplicaitonReviewAfterConsentList
        reviewBorrowerInfo={reviewersList}
        currentStep={currentStep}
        setcurrentStep={setcurrentStep}
        resolveMaritialStatus={resolveMaritialStatus}
        resolveLoanPurose={resolveLoanPurpose}
        editBorrower={editBorrower}
        saveAndContinue={saveAndContinue}
        deleteBorrower={deleteBorrower} />
        <PopupModal
            modalHeading={`Remove ${firstname} ?`}
            modalBodyData={<p>Are You Sure You Want To Delete {firstname} From This
             Loan Application? The Data You've Entered Will Not Be Recoverable.</p>}
            modalFooterData={<button className="btn btn-min btn-primary" onClick={confirmDeleteBorrower}>Yes</button>}
            show={deletePopup}
            handlerShow={() => setDeletePopup(!deletePopup)}
            dialogClassName={`delete-borrower-popup`}
        ></PopupModal></> : <Loader type="widget" />
}
