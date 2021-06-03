import React, { useContext, useEffect, useState } from 'react'
import { SelectedOtherIncomeType } from '../../../../../../../Entities/Models/types'
import { CommonType } from '../../../../../../../store/reducers/CommonReducer'
import { OtherIncomeActionTypes } from '../../../../../../../store/reducers/OtherIncomeReducer'
import { Store } from '../../../../../../../store/store'
import { StringServices } from '../../../../../../../Utilities/helpers/StringServices'
import { NavigationHandler } from '../../../../../../../Utilities/Navigation/NavigationHandler'
import { OtherIncomeWeb } from './OtherIncome_Web'


export const OtherIncome = () => {

    const { state, dispatch } = useContext(Store);
    const commonManager: any = state.commonManager;
    const { otherIncomeTypeList }: any = state.otherIncomeManager;
    const [otherIncomeTypes, setOtherIncomeTypes] = useState<[]>([]);

    useEffect(() => {
        if (otherIncomeTypeList) {
            setOtherIncomeTypes(otherIncomeTypeList);
        }
        renderTitle();
    }, [otherIncomeTypeList])

    const renderTitle = async () => {                
        await dispatch({ type: CommonType.SetIncomePopupTitle, payload: 'Other' });
    };
    const handleOtherIncomeTypeChange = async (data: SelectedOtherIncomeType) => {
        data.monthlySalary = null;
        data.annualSalary = null;
        data.description = null;
        let title = `${commonManager?.incomePopupTitle} / ${StringServices.capitalizeFirstLetter(data.name)}`;
        await dispatch({ type: CommonType.SetIncomePopupTitle, payload: title });
        await dispatch({ type: OtherIncomeActionTypes.SetSelectedOtherIncomeType, payload: data });
        NavigationHandler.navigation?.moveNext();
    }


    return (
        <OtherIncomeWeb
            otherIncomeTypes={otherIncomeTypes}
            OtherIncomeTypeChange={handleOtherIncomeTypeChange}
        />
    )
}
