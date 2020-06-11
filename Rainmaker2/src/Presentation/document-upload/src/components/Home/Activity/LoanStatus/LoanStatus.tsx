import React, { useState, useEffect, useContext } from 'react'
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { LoanApplication } from '../../../../entities/Models/LoanApplication';

import icon1 from '../../../../assets/images/property-address-icon.svg';
import icon2 from '../../../../assets/images/property-type-icon.svg';
import icon3 from '../../../../assets/images/loan-purpose-icon.svg';
import icon4 from '../../../../assets/images/loan-amount-icon.svg';



export const LoanStatus = () => {

    const [loanInfo, setLoanInfo] = useState<LoanApplication>()

    useEffect(() => {
        if (!loanInfo) {
            fetchLoanStatus();
        }
    }, [])

    const fetchLoanStatus = async () => {
        let loanInfoRes: LoanApplication | undefined = await LaonActions.getLoanApplication('1');
        if (loanInfoRes) {
            setLoanInfo(loanInfoRes);
        }
    }

    if (!loanInfo) {
        return <p>...loading...</p>
    }

    return (
        <section className="row">
            <div className="container">
                <div className="LoanStatus nbox-wrap">
                    <div className="nbox-wrap--body">
                        <ul className="row ls-wrap">
                            <li className="col-sm-3 ls-box">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon1} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Property Address</h4>
                                        <p className="LoanStatus--text">{loanInfo.addressName}, {loanInfo.countyName}, {loanInfo.stateName}, USA</p>
                                    </div>
                                </div>
                            </li>
                            <li className="col-sm-3 ls-box">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon2} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Property Type</h4>
                                        <p className="LoanStatus--text">
                                            {loanInfo.propertyType}
                                        </p>
                                    </div>


                                </div>
                            </li>
                            <li className="col-sm-3 ls-box">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon3} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Purpose</h4>
                                        <p className="LoanStatus--text">
                                            {loanInfo.loanPurpose}
                                        </p>
                                    </div>


                                </div>
                            </li>
                            <li className="col-sm-3 ls-box">
                                <div className="i-wrap">
                                    <div className="icon-wrap">
                                        <img src={icon4} alt="" />
                                    </div>
                                    <div className="c-wrap">
                                        <h4 className="LoanStatus--heading">Loan Amount</h4>
                                        <p className="LoanStatus--text">
                                            <div className="number-loanAmount">
                                                <span>{loanInfo.amount}</span>
                                            </div>
                                        </p>
                                    </div>


                                </div>
                            </li>

                        </ul>
                    </div>
                </div>
            </div>
        </section>
    )
}
