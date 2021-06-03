import React from 'react'
import { Link } from 'react-router-dom'
import { LoanApplicationType } from '../../Entities/Models/LoanApplicationType'
import { ApplicationEnv } from '../../lib/appEnv'
import { LoanAmountIcon, LoanBoxGoIcon, LoanPurposeIcon, LoanStatusIcon, PropertyTypeIcon }
    from '../../Shared/Components/SVGs'
import { curruncyFormatter } from './_Dashboard'

const DashboardLoanCard: React.FunctionComponent<{ userLoanApplication: LoanApplicationType }> = ({ userLoanApplication }) => {
    return (
        <>
            <div className="col-md-6 col-lg-4">
            <Link to={`${ApplicationEnv.LoanApplicationBasePath}?loanapplicationid=${userLoanApplication.id}`}>
                <div data-testid={`loan-box-${userLoanApplication.id}`} className="loan-box">
                    <div className="loan-box-wrap">
                        {((userLoanApplication.pendingTasks) &&
                            <div className="notif-p-task">
                                <div className="notif-p-task-block">
                                    <div className="count">{userLoanApplication.pendingTasks}</div>
                                    <div className="npt-text">Tasks Pending</div>
                                </div>
                            </div>
                        )}

                        {/* <div className="notif-p-task">
                        <div className="notif-p-task-block">
                            <div className="count">10</div>
                            <div className="npt-text">Tasks Pending</div>
                        </div>
                    </div> */}

                        <ul>
                            <li>
                                <div className="icon-wrap">
                                    <PropertyTypeIcon />
                                </div>
                                <div className="content-wrap">
                                    <h4>Property Address</h4>
                                    <div className="add-wrap">
                                        <p title="">{userLoanApplication.streetAddress} #{userLoanApplication.unitNumber} <br />{userLoanApplication.cityName}, {userLoanApplication.stateName} {userLoanApplication.zipCode}</p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div className="icon-wrap">
                                    <LoanPurposeIcon />
                                </div>
                                <div className="content-wrap">
                                    <h4>Loan Purpose</h4>
                                    <div className="add-wrap">
                                        <p title="" >{userLoanApplication.loanPurpose}</p>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div className="icon-wrap">
                                    <LoanAmountIcon />
                                </div>
                                <div className="content-wrap">
                                    <h4>Loan Amount</h4>
                                    <div className="add-wrap">
                                        <span className="la-text">
                                            <sup>$</sup>{curruncyFormatter(userLoanApplication.loanAmount)}
                                        </span>
                                    </div>
                                </div>
                            </li>
                            {(userLoanApplication.mileStoneId > 0 &&
                                <li>
                                    <div className="icon-wrap">
                                        <LoanStatusIcon />
                                    </div>
                                    <div className="content-wrap">
                                        <h4>Status</h4>
                                        <div className="add-wrap">
                                            <p title="" >{userLoanApplication.mileStone}</p>
                                        </div>
                                    </div>
                                </li>
                            )}
                        </ul>
                        <div className="lb-go-wrap">

                            <LoanBoxGoIcon />

                        </div>
                        
                    </div>
                </div>
                </Link>
            </div>
        </>
    )
}

export default DashboardLoanCard;