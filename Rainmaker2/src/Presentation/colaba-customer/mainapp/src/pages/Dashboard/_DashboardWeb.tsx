import React from 'react'
import DashboardLoanCard from './_DashboardWebLoanCard'
import { LoanApplicationType } from '../../Entities/Models/LoanApplicationType';
import DashboardCreateLoanApplication from './_DashboardCreateLoanApplication';

export const DashboardDesktop: React.FC<{ userCurrentLoanApplications: LoanApplicationType[] }> = ({ userCurrentLoanApplications }) => {
    return (
        <>  {(userCurrentLoanApplications.length > 0
            &&
            userCurrentLoanApplications.map((userLoanApplication: LoanApplicationType) => (
                <DashboardLoanCard key={userLoanApplication.id} userLoanApplication={userLoanApplication} />
            )) 
        )}
            {  
                <DashboardCreateLoanApplication />
            }
        </>
    )
}
