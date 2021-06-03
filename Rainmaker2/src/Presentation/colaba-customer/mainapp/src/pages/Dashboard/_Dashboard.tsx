import React, { useEffect, useState } from 'react'
import { DashboardDesktop } from './_DashboardWeb';
import DashboardActions from '../../Store/actions/DashboardActions';
import { LoanApplicationType } from '../../Entities/Models/LoanApplicationType';


export const curruncyFormatter = (price: number, region = 'en-US') => {
    return price.toLocaleString(region);
};

export const Dashboard: React.FC = () => {
    const [loanApplications, setLoanApplication] = useState<LoanApplicationType[]>([])
    
    useEffect(() => {
        fetchLoanApps()
    }, []);

    const fetchLoanApps = async () => {
        //if (!loanApplications || !loanApplications.length) {
        const loanApplications = await DashboardActions.fetchLoggedInUserCurrentLoanApplications();
        setLoanApplication(loanApplications);
        // if (response) {
        //     dispatch({ type: DashboardActionsTypes.GetLoanApplications, payload: response });
        // }
        //}
    }

    return (
        loanApplications &&
        <DashboardDesktop userCurrentLoanApplications={loanApplications} />
    )


}

export default Dashboard;