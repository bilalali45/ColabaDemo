import React, { useEffect, useState, useContext } from 'react';
import { LoanInfoType, ReviewBorrowerInfo } from '../../../../Entities/Models/types';
import BorrowerActions from '../../../../store/actions/BorrowerActions';
import { CommonActions } from '../../../../store/actions/CommonActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { LocalDB } from '../../../../lib/LocalDB';
import { ApplicaitonReviewList } from './ApplicaitonReviewList';

import { GettingToKnowYouSteps } from '../../../../store/actions/LeftMenuHandler';
import { ApplicationEnv } from '../../../../lib/appEnv';
import { PopupModal } from '../../../../Shared/Components/Modal';
import Loader from '../../../../Shared/Components/Loader';
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';


export const ApplicaitonReview = ({ currentStep, setcurrentStep }: any) => {

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const [reviewersList, setReviewersList] = useState<ReviewBorrowerInfo>();
    const [maritalStatus, setMaritalStatus] = useState([]);
    const [deletePopup, setDeletePopup] = useState<boolean>(false);
    const [borrowerId, setBorrowerId] = useState<number>(0);
    const [firstname, setFirstname] = useState<string>('');

    const [isClicked, setIsClicked] = useState<boolean>(false);
    const fetchData = async () => {
        let maritalStatuses: any = await CommonActions.getAllMaritialStatuses();
        setMaritalStatus(maritalStatuses);
        await BorrowerActions.getBorrowersForFirstReview(Number(LocalDB.getLoanAppliationId())).then((res) => {
            setReviewersList(res.data);
        });
    }


    const editBorrower = async (borrowerId: number, ownTypeId: number) => {
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: borrowerId,
            ownTypeId: ownTypeId
        }

        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        LocalDB.setBorrowerId(String(borrowerId))
        // LeftMenuHandler.addDecision(Decisions.AddBrowerFromApplicationReview);
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself?borrowerid=${borrowerId}`)
    }

    const deleteBorrower = async (borrowerId: number, firstname: string) => {
        setBorrowerId(borrowerId);
        setFirstname(firstname);

        setDeletePopup(true);
    }

    const addBorrower = async () => {
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: null,
            ownTypeId: 2,
            borrowerName: ""
        }
        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        LocalDB.clearBorrowerFromStorage();
        // LeftMenuHandler.addDecision(Decisions.AddBrowerFromApplicationReview);
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself`);
    }
    useEffect(() => {
        fetchData()
    }, []);

    function resolveMaritialStatus(id: number) {
        let maratialStatusName = maritalStatus.find((ms) => ms.id == id);
        return maratialStatusName;
    }

    const saveAndContinue = async () => {
        if (!isClicked) {
            setIsClicked(true)

            const allBorrwers = await BorrowerActions.getAllBorrower(+LocalDB.getLoanAppliationId());

            let filterdBorrowers = allBorrwers?.data?.filter(x => NavigationHandler.filterBorrowerByFieldConfiguration(x));

            if (filterdBorrowers?.length == 0) {
                NavigationHandler.disableFeature(GettingToKnowYouSteps.SSN);
            } else {
                NavigationHandler.enableFeature(GettingToKnowYouSteps.SSN);
            }

            NavigationHandler.moveNext();
        }
    }

    const confirmDeleteBorrower = async () => {
        await BorrowerActions.deleteSecondaryBorrower(Number(LocalDB.getLoanAppliationId()), borrowerId);
        fetchData();
        setDeletePopup(false);
        setBorrowerInfoInStore();
    }

    const setBorrowerInfoInStore = async () => {
        if (loanInfo.borrowerId === borrowerId) {
            let borrowerList = reviewersList?.borrowerReviews;
            let borrowerIdx = borrowerList?.findIndex(borrower => borrower.borrowerId === borrowerId)
            if(borrowerIdx && borrowerList){
                let currentBorrower = borrowerList[borrowerIdx - 1]
                let loanInfoObj: LoanInfoType = {
                    ...loanInfo,
                    borrowerId: currentBorrower?.borrowerId,
                    ownTypeId: currentBorrower.ownTypeId,
                    borrowerName: currentBorrower?.firstName+' '+currentBorrower?.lastName,
                }
                await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
            }
            
        }

    }


    return reviewersList ? <><ApplicaitonReviewList
        applicationReviewersList={reviewersList}
        resolveMaritialStatus={resolveMaritialStatus}
        currentStep={currentStep}
        setcurrentStep={setcurrentStep}
        addBorrower={addBorrower}
        deleteBorrower={deleteBorrower}
        editBorrower={editBorrower}
        saveAndContinue={saveAndContinue} />
        <PopupModal
            modalHeading={`Remove ${firstname} ?`}
            modalBodyData={<p>Are you sure you want to delete {firstname} from this
             loan application? The data you've entered will not be recoverable.</p>}
            modalFooterData={<button className="btn btn-min btn-primary" onClick={confirmDeleteBorrower}>Yes</button>}
            show={deletePopup}
            handlerShow={() => setDeletePopup(!deletePopup)}
            dialogClassName={`delete-borrower-popup`}
        ></PopupModal>
    </> : <Loader type="widget" />
}
