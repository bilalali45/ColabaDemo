import React, { useContext, useEffect } from 'react'
import { Store } from '../../store/store';
import { CommonActions } from '../../store/actions/CommonActions';
import GettingStartedActions from '../../store/actions/GettingStartedActions';
import { ErrorHandler } from '../../Utilities/helpers/ErrorHandler';
import { LocalDB } from '../../lib/LocalDB';
import { ParamsService } from '../../Utilities/helpers/ParamService';
import { LoanInfoType, PrimaryBorrowerInfo, GetReviewBorrowerInfoSectionProto } from '../../Entities/Models/types';
import { LoanApplicationActionsType } from '../../store/reducers/LoanApplicationReducer';
import BorrowerActions from '../../store/actions/BorrowerActions';
import { OwnTypeEnum } from '../../Utilities/Enum';
import { NavigationHandler } from '../../Utilities/Navigation/NavigationHandler';
import { useLocation } from 'react-router';
import { UrlQueryManager } from '../../Utilities/Navigation/UrlQueryManager';

export const LoanConfig = ({ children }) => {

    const { state, dispatch } = useContext(Store);
    const { myPropertyInfo }: any = state.loanManager;

    const location = useLocation();

    let loanInfoObj: LoanInfoType = {
        loanApplicationId: null,
        loanPurposeId: null,
        loanGoalId: null,
        borrowerId: null,
        ownTypeId: null,
        borrowerName: null,
    };
    let borrowerInfo: PrimaryBorrowerInfo = {
        id: null,
        name: null,
    };

    useEffect(() => {
        storeLoInfo();
    }, []);

    useEffect(() => {
        UrlQueryManager.extractQueryFromUrl(location.search);
       // if(!(location.pathname.includes('AssetSources'))){
            storeLoanInfoData();
       // }     
        window.scrollTo(0, 0)
    }, [location.pathname])

    const storeLoInfo = async () => {
        let response = await GettingStartedActions.getLoInfo();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                LocalDB.setLOImageUrl(response.data.image);
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    };

    const getDataFromSavedState = async (loanapplicationId?: string) => {

        let loanInfoResponse = await CommonActions.getPendingLoanApplication(
            loanapplicationId
        );
        try {
            loanInfoResponse = JSON.parse(loanInfoResponse?.state);
            return loanInfoResponse;
        } catch (error) {
            return null;
        }
    }

    const storeLoanInfoData = async () => {

        let loanapplicationId = ParamsService.getParam("loanapplicationid");
        let borrowerId = null;
        let incomeId = null;
        let loanPurposeId = null;
        let loanGoalId = null;
        let myPropertyTypeId = null;
        let isLoanApplicationNew = false;
        if (loanapplicationId) {
            if (loanapplicationId == "new") {
                LocalDB.clearSessionStorage();
                loanapplicationId = null;
                isLoanApplicationNew = true;
            } else {
                LocalDB.setLoanAppliationId(loanapplicationId)
                const loanInfoResponse = await getDataFromSavedState(loanapplicationId);
                LocalDB.setLoanGoalId(String(loanInfoResponse?.loangoalid));
                loanGoalId = loanInfoResponse?.loangoalid;
                LocalDB.setLoanPurposeId(String(loanInfoResponse?.loanpurposeid));
                loanPurposeId = loanInfoResponse?.loanpurposeid;
                if (loanInfoResponse?.borrowerid) {
                    borrowerId = loanInfoResponse?.borrowerid;
                    LocalDB.setBorrowerId(loanInfoResponse?.borrowerid);
                }
                if (loanInfoResponse?.incomeid) {
                    incomeId = loanInfoResponse?.incomeid;
                    LocalDB.setIncomeId(loanInfoResponse?.incomeid);
                }
                if (loanInfoResponse?.myPropertyTypeId) {
                    myPropertyTypeId = loanInfoResponse?.myPropertyTypeId;
                    LocalDB.setMyPropertyTypeId(loanInfoResponse?.myPropertyTypeId);
                }
            }
        } else {
            loanapplicationId = LocalDB.getLoanAppliationId();
            borrowerId = LocalDB.getBorrowerId();
            incomeId = LocalDB.getIncomeId();
            loanGoalId = LocalDB.getLoanGoalId();
            loanPurposeId = LocalDB.getLoanPurposeId();
            myPropertyTypeId = LocalDB.getMyPropertyTypeId();

            loanInfoObj.loanPurposeId = loanPurposeId;
            loanInfoObj.loanGoalId = loanGoalId;
        }
        // if borrower id exist.. get borrower info
        if (borrowerId) {
            let borrowerResponse = await BorrowerActions.getBorrowerInfo(
                Number(loanapplicationId),
                Number(borrowerId)
            );
            loanInfoObj.borrowerId = borrowerId;
            if (borrowerResponse) {
                loanInfoObj.ownTypeId = borrowerResponse?.ownTypeId;
                loanInfoObj.borrowerName = borrowerResponse.firstName+' '+borrowerResponse.lastName;
            }
        }

        // Get Primary Borrower info
        if (loanapplicationId) {
            let loanInfoState = await CommonActions.getPendingLoanApplication(
                loanapplicationId
            );
            // Update Feature Setting
            NavigationHandler.updateFeatureSettting(loanInfoState?.setting);
            loanInfoObj.loanApplicationId = Number(loanapplicationId);
            loanInfoObj.loanPurposeId = loanPurposeId;
            loanInfoObj.loanGoalId = loanGoalId;

            let response = await BorrowerActions.getBorrowersForFirstReview(
                +loanapplicationId
            );
            if (response.data) {
                let primaryBorrower: GetReviewBorrowerInfoSectionProto = response.data.borrowerReviews?.find(
                    (item) => item.ownTypeId === OwnTypeEnum.PrimaryBorrower
                );
                if (primaryBorrower) {
                    // if (!borrowerId) {

                    //     loanInfoObj.borrowerId = primaryBorrower.borrowerId;
                    //     loanInfoObj.ownTypeId = primaryBorrower?.ownTypeId;
                    //     loanInfoObj.borrowerName = primaryBorrower.firstName;
                    //     LocalDB.setBorrowerId(borrowerId);
                    // }
                    borrowerInfo.id = primaryBorrower.borrowerId;
                    borrowerInfo.name = primaryBorrower.firstName+' '+primaryBorrower.lastName;
                }
            } else if (!isLoanApplicationNew) {
                // Loan Information does not exist
                // window.location.href = '/homepage';
                return;
            }

        }

        if (!isLoanApplicationNew && !loanapplicationId) {
            // Loan Information does not exist
            //window.location.href = '/homepage';
            return;
        }

        dispatch({ type: LoanApplicationActionsType.SetMyPropertyInfo, payload: { ...myPropertyInfo, primaryPropertyTypeId: myPropertyTypeId } });
        dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: { incomeId: incomeId, incomeTypeId: null } });
        LocalDB.setIncomeId(incomeId)
        LocalDB.setIncomeId(myPropertyTypeId);
        dispatch({
            type: LoanApplicationActionsType.SetLoanInfo,
            payload: loanInfoObj,
        });
        dispatch({
            type: LoanApplicationActionsType.SetPrimaryBorrowerInfo,
            payload: borrowerInfo,
        });
    };

    return (
        <React.Fragment>
            {children}
        </React.Fragment>
    )
}
