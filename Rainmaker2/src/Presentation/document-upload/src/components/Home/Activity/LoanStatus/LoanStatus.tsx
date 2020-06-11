import React, { useState, useEffect, useContext } from 'react'
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { LoanApplication } from '../../../../entities/Models/LoanApplication';

export const LoanStatus = () => {

    const [loanInfo, setLoanInfo] = useState<LoanApplication>()

    useEffect(() => {
        if (!loanInfo) {
            fetchLoanStatus();
        }
    }, [])

    const fetchLoanStatus = async () => {
        let loanInfoRes : LoanApplication | undefined = await LaonActions.getLoanApplication('1');
        if (loanInfoRes) {
            setLoanInfo(loanInfoRes);
        }
    }

    if (!loanInfo) {
        return <p>...loading...</p>
    }

    return (
        <div className="LoanStatus box-wrap">
            <div className="box-wrap--body">
                <ul>
                    <li>
                        <h2 className="heading-h2">Loan Application</h2>
                    </li>
                    <li>
                        <div className="flex">
                            <div className="flex--side">
                                <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                <p className="LoanStatus--text">{loanInfo.loanPurpose}</p>
                            </div>
                            <div className="flex--side">
                                <h4 className="LoanStatus--heading">Property Type</h4>
                                <p className="LoanStatus--text">{loanInfo.propertyType}</p>
                            </div>
                        </div>
                    </li>
                    <li>
                        <h5 className="LoanStatus--heading">Property Address</h5>
                        <p className="LoanStatus--text">{loanInfo.addressName}, {loanInfo.countyName}, {loanInfo.stateName}, USA</p>
                    </li>
                    <li>
                        <h5 className="LoanStatus--heading">Loan Amount</h5>
                        <p className="LoanStatus--text">{loanInfo.amount}</p>
                    </li>
                </ul>
            </div>
        </div>
    )
}
