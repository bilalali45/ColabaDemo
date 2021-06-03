import React, { useContext, useEffect, useState } from 'react';
import { IncomeSourceType } from '../../../../../Entities/Models/types';
import IncomeActions from '../../../../../store/actions/IncomeActions';
import { CommonType } from '../../../../../store/reducers/CommonReducer';
import { Store } from '../../../../../store/store';
import { ErrorHandler } from '../../../../../Utilities/helpers/ErrorHandler';
import { IncomeSourcesWeb } from './IncomeSources_Web';
import { ApplicationEnv } from '../../../../../lib/appEnv';
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import { MilitaryIncomeActionTypes } from '../../../../../store/reducers/MilitaryIncomeReducer';
import { LoanApplicationActionsType } from '../../../../../store/reducers/LoanApplicationReducer';
import { LocalDB } from '../../../../../lib/LocalDB';
import Loader from '../../../../../Shared/Components/Loader';


const incomeSourcesPath = {
    'IncomeSources': '/IncomeSources/',
    'Employment': '/Employment/EmploymentIncome',
    'Self Employment / Independent Contractor': '/SelfIncome/SelfEmploymentIncome',
    'Business': '/Business/BusinessIncomeType',
    'Military Pay': '/Military/MilitaryIncome',
    'Retirement': '/Retirement/RetirementIncomeSource',
    'Other': '/Other/OtherIncome',
    'Rental': '/Rental/',
}

const createIncomeSourcePath = (title: string) => {
    return `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome${incomeSourcesPath[title]}`;
}

export const IncomeSources = () => {

    const { state, dispatch } = useContext(Store);
    const loanManager: any = state.loanManager;
    const [incomeSourceList, setIncomeSourceList] = useState<IncomeSourceType[]>();

    useEffect(() => {
        GetSourceOfIncomeList();
        dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryServiceAddress, payload: undefined });
        dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryEmployer, payload: undefined });
        dispatch({ type: MilitaryIncomeActionTypes.SetMilitaryPaymentMode, payload: undefined });
        dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: undefined });
        dispatch({ type: CommonType.SetIncomePopupTitle, payload: null });
        LocalDB.clearIncomeFromStorage();
    }, [])

    const GetSourceOfIncomeList = async () => {
        let response = await IncomeActions.GetSourceOfIncomeList();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                setIncomeSourceList(response.data?.map((source: IncomeSourceType) => {
                    return {
                        ...source,
                        path: createIncomeSourcePath(source?.displayName)
                    }
                }));
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const selectTypeHandler = async (item: any) => {
        console.log('=========> Borrower In store', loanManager?.loanInfo?.borrowerName)
        // let title = `${StringServices.capitalizeFirstLetter(loanManager?.loanInfo?.borrowerName)}'s ${StringServices.capitalizeFirstLetter(item.displayName)} Income`;
        await dispatch({ type: CommonType.SetIncomePopupTitle, payload: item.displayName });
        NavigationHandler.navigateToPath(item.path);
    }

    return (
        <div data-testid="incomesources-screen">

            {incomeSourceList && incomeSourceList?.length > 0 &&
                <IncomeSourcesWeb
                    IncomeSourceList={incomeSourceList}
                    selectType={selectTypeHandler}
                />
            }

            {incomeSourceList?.length === 0 &&
                <div><Loader type="widget"/></div>
            }

        </div>
    )
}
