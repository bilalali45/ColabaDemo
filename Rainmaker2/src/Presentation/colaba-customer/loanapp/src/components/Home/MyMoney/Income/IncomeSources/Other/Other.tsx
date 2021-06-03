import React, { useContext, useEffect } from 'react'
import { Switch } from 'react-router-dom';
import { SelectedOtherIncomeType } from '../../../../../../Entities/Models/types';
import { ApplicationEnv } from '../../../../../../lib/appEnv';
import IncomeActions from '../../../../../../store/actions/IncomeActions';
import { OtherIncomeActionTypes } from '../../../../../../store/reducers/OtherIncomeReducer';
import { Store } from '../../../../../../store/store';
import { ErrorHandler } from '../../../../../../Utilities/helpers/ErrorHandler';
import { IsRouteAllowed } from '../../../../../../Utilities/Navigation/navigation_settings/IsRouteAllowed';
import { OtherIncome } from './OtherIncome/OtherIncome';
import { OtherIncomeDetails } from './OtherIncomeDetails/OtherIncomeDetails';

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other`;


export const Other = () => {

    const { state, dispatch } = useContext(Store);
    const { incomeInfo }: any = state.loanManager;

    useEffect(() => {
        if (incomeInfo && incomeInfo?.incomeId) {
            getOtherIncomeInfo(incomeInfo?.incomeId);
        }
    }, [incomeInfo?.incomeId])

    useEffect(() => {
        GetAllIncomeGroupsWithOtherIncomeTypes()
    }, [])

    const GetAllIncomeGroupsWithOtherIncomeTypes = async () => {
        let response = await IncomeActions.GetAllIncomeGroupsWithOtherIncomeTypes();
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                dispatch({ type: OtherIncomeActionTypes.SetOtherIncomeTypeList, payload: response.data })
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    const getOtherIncomeInfo = async (incomeInfoId: number) => {
        let response = await IncomeActions.GetOtherIncomeInfo(incomeInfoId);
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                let { incomeTypeId, incomeTypeName, monthlyBaseIncome, annualBaseIncome, description, fieldInfo } = response.data;
                let values: SelectedOtherIncomeType = {
                    id: incomeTypeId,
                    name: incomeTypeName,
                    monthlySalary: monthlyBaseIncome,
                    annualSalary: annualBaseIncome,
                    description: description,
                    fieldsInfo: fieldInfo

                }
                dispatch({ type: OtherIncomeActionTypes.SetSelectedOtherIncomeType, payload: values });
            } else {
                ErrorHandler.setError(dispatch, response);
            }
        }
    }

    return (
        <Switch>
            <IsRouteAllowed path={`${containerPath}/OtherIncome`} component={OtherIncome} />
            <IsRouteAllowed path={`${containerPath}/OtherIncomeDetails`} component={OtherIncomeDetails} />
        </Switch>
    )
}
