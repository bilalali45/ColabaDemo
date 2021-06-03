import React from 'react';
import { SelfEmploymentIncomeForm } from './SelfEmploymentIncomeForm';


export const SelfEmploymentIncome = ({ selfIncome, updateFormValuesOnChange }) => {

    const onSubmit = (data) => updateFormValuesOnChange({ key: 'info', value: data })

    return (
        <SelfEmploymentIncomeForm onSubmit={onSubmit} selfIncome={selfIncome} />
    )
}
