import React from 'react'
import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentsStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'

export const Activity = () => {
    return (
        <div>
            <h1>Activity</h1>
            <LoanStatus/>
            <LoanProgress/>
            <DocumentStatus/>
            <ContactUs/>
        </div>
    )
}
