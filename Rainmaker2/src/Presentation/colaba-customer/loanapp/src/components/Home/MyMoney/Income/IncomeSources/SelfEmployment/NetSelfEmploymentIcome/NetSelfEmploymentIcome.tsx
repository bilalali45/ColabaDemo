import React from 'react'
import { NetSelfEmploymentIncomeForm } from './NetSelfEmploymentIncomeForm'

export const NetSelfEmploymentIcome = ({
    selfIncome,
    updateFormValuesOnChange }) => {
 
    const onSubmit = (data) => updateFormValuesOnChange({ key: 'annualIncome', value: Number(data?.annualIncome)})

    return (
        <section>
            <NetSelfEmploymentIncomeForm onSubmit={onSubmit} selfIncome={selfIncome} />
        </section>
    )
}
