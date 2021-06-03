import React from 'react'
import { Link } from 'react-router-dom'
import { ApplicationEnv } from '../../lib/appEnv'
import { LoanAddIcon } from '../../Shared/Components/SVGs'

export const DashboardCreateLoanApplication: React.FC = () => {
    return (
        <>
        <Link to={`${ApplicationEnv.LoanApplicationBasePath}?loanapplicationid=new`} data-testid="create-newloan-application-btn" className="add-n-app col-md-6 col-lg-4">
                <div className="loan-box">
                    <div className="loan-box-wrap">
                        <a className="add-n-app-box">
                            <div>
                                <LoanAddIcon />
                            </div>
                            <div>
                                Add New <br />
                            Loan Application
                            </div>
                        </a>
                    </div>
                </div>
        </Link>
        </>
    )
}

export default DashboardCreateLoanApplication;