import React, {useContext, useEffect, useState} from 'react'

import {Store} from "../../../../../store/store";
import { LoanInfoType} from "../../../../../Entities/Models/types";

import BorrowerActions from "../../../../../store/actions/BorrowerActions";
import {LocalDB} from "../../../../../lib/LocalDB";
import {LoanApplicationActionsType} from "../../../../../store/reducers/LoanApplicationReducer";
import {NavigationHandler} from "../../../../../Utilities/Navigation/NavigationHandler";
import {ApplicationEnv} from "../../../../../lib/appEnv";
import {GettingToKnowYouSteps, LeftMenuItems} from "../../../../../store/actions/LeftMenuHandler";
import Loader from "../../../../../Shared/Components/Loader";

import {ErrorHandler} from "../../../../../Utilities/helpers/ErrorHandler";
import {IncomeReviewList} from "./IncomeReviewList";
import IncomeReviewActions from "../../../../../store/actions/IncomeReviewActions";
import {BusinessActionTypes} from "../../../../../store/reducers/BusinessIncomeReducer";


export type GetIncomeSectionReviewProto = {
    borrowerId: number
    borrowerName: string
    ownType :GetIncomeSectionReviewIncomeProtoOwnType
    incomeTypes: Array<GetIncomeSectionReviewIncomesProto>
}

export type GetIncomeSectionReviewIncomesProto = {
    id :number
    name :string
    displayName :string
    incomeList :incomeInfo[]
    incomeCategory: GetIncomeSectionReviewIncomeProtoOwnType
}
export type incomeInfo = {
    incomeInfo :GetIncomeSectionReviewIncomeListProto
}
export type GetIncomeSectionReviewIncomeListProto = {
    employerName: string
    jobTitle: string
    startDate: string
    endDate: string,
    yearsInProfession: number,
    employerPhoneNumber: string
    employedByFamilyOrParty: boolean
    hasOwnershipInterest: boolean
    ownershipInterest: number
    incomeInfoId: number,
    isCurrentIncome: true,
    incomeAddress :GetIncomeSectionReviewEmploymentAddressProto
    wayOfIncome: GetIncomeSectionReviewWayOfIncomeProto
}


export type GetIncomeSectionReviewEmploymentInfoEmploymentOtherIncomesProto = {
    incomeTypeId :number,
    annualIncome :number
    name :string,
    displayName :string
}


export type GetIncomeSectionReviewEmploymentAddressProto = {
    streetAddress: string,
    unitNo: string,
    cityId: null,
    cityName: string,
    countryId: number,
    stateId: number,
    stateName: string,
    zipCode: string
    countryName?: string
}

export type GetIncomeSectionReviewWayOfIncomeProto = {
    isPaidByMonthlySalary: false,
    hourlyRate: number | null,
    hoursPerWeek: string | null,
    annualSalary: number,
    monthlySalary: number,
}

export type GetIncomeSectionReviewIncomeProtoOwnType = {
    id :number
    name :string
    displayName: string
}


export const IncomeReview = () => {
    const {state, dispatch} = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    
    const [maritalStatus] = useState([]);
    const [incomeReviewers, setIncomeReviewers] = useState<GetIncomeSectionReviewProto[]>([]);
    const [isClicked, setIsClicked] = useState<boolean>(false);

    useEffect(() => {
        fetchData()
    }, []);

    const fetchData = async () => {
        if (LocalDB.getLoanAppliationId() != undefined) {
            let response = await IncomeReviewActions.GetBorrowerIncomesForReview(Number(LocalDB.getLoanAppliationId())/*1172*/);
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    setIncomeReviewers(response.data.borrowers)
                }
                else {
                    ErrorHandler.setError(dispatch, response);
                }
            }
        }
    }

    const flushIncomeData = async () => {
        await dispatch({ type: BusinessActionTypes.SetCurrentBusinessDetails, payload: {} });
        await dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: {} });
        LocalDB.clearIncomeFromStorage();
    }
    const editIncome = async (incomeInfo: GetIncomeSectionReviewIncomeListProto,
                              loanApplicationReviewer : GetIncomeSectionReviewIncomesProto,
                              getIncomeSectionReviewProto: GetIncomeSectionReviewProto) => {
        //borrowerId: number,borrowerName: string,ownTypeId: number, employmentCategoryId: number, incomeInfoId :number, isCurrentIncome :boolean

        let {borrowerId,borrowerName } = getIncomeSectionReviewProto;
        let ownTypeId = getIncomeSectionReviewProto.ownType.id;
        let {incomeInfoId,isCurrentIncome }= incomeInfo;
        let employmentCategoryId = loanApplicationReviewer.incomeCategory.id;

        console.info("borrowerId ",borrowerId,"  employmentCategoryId ",employmentCategoryId," incomeInfoId ",incomeInfoId)
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: borrowerId,
            borrowerName: borrowerName,
            ownTypeId: ownTypeId,
        }
        flushIncomeData();
        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        await dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: { incomeId: incomeInfoId, incomeTypeId: employmentCategoryId } });
        LocalDB.setBorrowerId(String(borrowerId))
        LocalDB.setIncomeId(String(incomeInfoId))


        if (employmentCategoryId === 1 && !isCurrentIncome) {
            NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/EmploymentHistory/EmploymentHistoryDetails/PreviousEmployment`);
        }
        else {
            switch(employmentCategoryId) {
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
                    NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/EmploymentIncome`)
                //NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/${incomeProto.employmentCategory?.categoryName}/${incomeProto.employmentCategory?.categoryName}IncomeType`)
            }
        }
    }


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
                NavigationHandler.enableFeature(LeftMenuItems.GettingToKnowYou);
            }

            NavigationHandler.moveNext();
        }
    }

    /* const confirmDeleteBorrower = async () => {
         await BorrowerActions.deleteSecondaryBorrower(Number(LocalDB.getLoanAppliationId()), borrowerId);
         fetchData();
         setBorrowerInfoInStore();
     }

     const setBorrowerInfoInStore = async () => {
         if (loanInfo.borrowerId === borrowerId) {
             let borrowerList = reviewersList?.borrowerReviews;
             let borrowerIdx = borrowerList.findIndex(borrower => borrower.borrowerId === borrowerId)
             let currentBorrower = borrowerList[borrowerIdx - 1]
             let loanInfoObj: LoanInfoType = {
                 ...loanInfo,
                 borrowerId: currentBorrower.borrowerId,
                 ownTypeId: currentBorrower.ownTypeId,
                 borrowerName: currentBorrower.firstName
             }
             await dispatch({type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj});
         }

     }*/
    /*const onSubmit = (data) => {
         const primaryAddOrUpdateMaritalStatusPayload = new PrimaryAddOrUpdateMaritalStatusPayload(2002,"test",data.maritialStatus);
        MaritalStatusActions.addOrUpdatePrimaryBorrowerMaritalStatus(primaryAddOrUpdateMaritalStatusPayload);
    }*/

    return incomeReviewers ? <><IncomeReviewList
        borrowers={incomeReviewers}
        resolveMaritialStatus={resolveMaritialStatus}
        editIncome={editIncome}
        saveAndContinue={saveAndContinue}/>

    </> : <Loader type="widget"/>
}
