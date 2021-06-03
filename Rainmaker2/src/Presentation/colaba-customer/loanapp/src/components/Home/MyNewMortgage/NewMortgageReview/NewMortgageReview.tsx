import React, { useEffect, useState, useContext } from 'react';

import {
    LoanInfoType,
    LoanpurposeProto,
    MortgagePropertyReviewProto
} from '../../../../Entities/Models/types';

import { CommonActions } from '../../../../store/actions/CommonActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { LocalDB } from '../../../../lib/LocalDB';
import LeftMenuHandler, { Decisions, MyMoneySteps } from '../../../../store/actions/LeftMenuHandler';
import { ApplicationEnv } from '../../../../lib/appEnv';

import Loader from '../../../../Shared/Components/Loader';
import { NewMortgageReviewList } from "./NewMortgageReviewList";
import { NavigationHandler } from '../../../../Utilities/Navigation/NavigationHandler';
import { TenantConfigFieldNameEnum } from '../../../../Utilities/Enumerations/TenantConfigEnums';
import LoanAplicationActions from '../../../../store/actions/LoanAplicationActions';


export const NewMortgageReview = ({ currentStep, setcurrentStep }: any) => {
    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const loanInfo: LoanInfoType = loanManager.loanInfo;
    const [mortgagePropertyReview, setMortgagePropertyReview] = useState<MortgagePropertyReviewProto>();
    const [maritalStatus, setMaritalStatus] = useState([]);
    const [loanpurposes, setLoanpurposes] = useState<LoanpurposeProto[]>();
    const [isClicked, setIsClicked] = useState<boolean>(false);
    const fetchData = async () => {
        //maritalStatus = [..._maritalStatuses];
        let maritalStatuses: any = await CommonActions.getAllMaritialStatuses();
        setMaritalStatus(maritalStatuses);

        let loanPurposes: any = await CommonActions.getAllloanpurpose();
        setLoanpurposes(loanPurposes);

        await LoanAplicationActions.getLoanApplicationSecondReview(Number(LocalDB.getLoanAppliationId())).then((res) => {
            setMortgagePropertyReview(res.data);
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
        let loanPurpose = loanpurposes &&loanpurposes.find((ms) => ms.id == id);
        return loanPurpose ? loanPurpose : null;
    }


    const addBorrower = async () => {

        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: null,
            ownTypeId: 2
        }

        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        LocalDB.clearBorrowerFromStorage();
        LeftMenuHandler.addDecision(Decisions.AddBrowerFromMyNewMortgageReview);
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself`);
    }

    const editBorrower = async (borrowerId: number, ownTypeId: number) => {
        console.log("edit borrower called");
        console.log("borrower id", borrowerId);
        console.log("ownTypeId id", ownTypeId);
        let loanInfoObj: LoanInfoType = {
            ...loanInfo,
            borrowerId: borrowerId,
            ownTypeId: ownTypeId
        }

        await dispatch({ type: LoanApplicationActionsType.SetLoanInfo, payload: loanInfoObj });
        LocalDB.setBorrowerId(String(borrowerId))
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/GettingToKnowYou/AboutYourself?borrowerid=${borrowerId}`)

    }

    const editMortgage = async () => {
        console.log("edit editMortgage");
        NavigationHandler.navigateToPath(`${ApplicationEnv.ApplicationBasePath}/MyNewMortgage/SubjectPropertyNewHome`)
    }


    const saveAndContinue = async () => {
        if (!isClicked) {
            setIsClicked(true);
            if (NavigationHandler.isFieldVisible(TenantConfigFieldNameEnum.IncomeSection)) {
                NavigationHandler.enableFeature(MyMoneySteps.Income);
            }
            else {
                NavigationHandler.disableFeature(MyMoneySteps.Income);
            }
            NavigationHandler.moveNext();
        }
    }

    return mortgagePropertyReview ? <><NewMortgageReviewList
        mortgagePropertyReview={mortgagePropertyReview}
        currentStep={currentStep}
        setcurrentStep={setcurrentStep}
        resolveMaritialStatus={resolveMaritialStatus}
        resolveLoanPurose={resolveLoanPurpose}
        addBorrower={addBorrower}
        editBorrower={editBorrower}
        saveAndContinue={saveAndContinue}
        editMortgage={editMortgage}
    />
    </> : <Loader type="widget" />

}