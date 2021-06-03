import React from 'react'
import {
    EditIcon
} from '../../../../../Shared/Components/SVGs';
import {
    GetIncomeSectionReviewIncomeListProto,
    GetIncomeSectionReviewIncomesProto,
    GetIncomeSectionReviewProto
} from "./IncomeReview";
import { StringServices } from "../../../../../Utilities/helpers/StringServices";
import { NumberServices } from "../../../../../Utilities/helpers/NumberServices";
import moment from "moment";

export const IncomeReviewIncomeSubCard: React.FC<{
    incomeInfo: GetIncomeSectionReviewIncomeListProto,
    loanApplicationReviewer: GetIncomeSectionReviewIncomesProto
    getIncomeSectionReviewProto: GetIncomeSectionReviewProto,
    editIncome: Function
}> = ({ incomeInfo, loanApplicationReviewer, getIncomeSectionReviewProto, editIncome }) => {
    return (
        <>
            <div className="review-item-e-history">
                <div className="r-content">

                        <div className="eh-title">
                            <h4>{incomeInfo?.employerName}</h4>
                        </div>

                    <div className="otherinfo">
                        <ul>
                            {/* {incomeInfo.employerName &&
                            <li>
                                <div className="eh-title">
                                    <h4>{loanApplicationReviewer.employmentInfo.employmentCategory.categoryName}</h4>
                                </div>
                                <span>{loanApplicationReviewer.employmentInfo.employmentCategory.categoryName} </span> disabled this because of https://i.imgur.com/q135SDG.png{loanApplicationReviewer.employmentInfo.employerName}
                            </li>
                            } */}

                            {incomeInfo.jobTitle &&
                                <li>
                                    <span>Job Title: </span>{incomeInfo.jobTitle}
                                </li>
                            }
                            {incomeInfo.startDate &&
                                <li>
                                    <span>Start Date: </span>{moment.utc(new Date(incomeInfo.startDate)).format('dddd, MM YYYY')}
                                </li>
                            }
                            {incomeInfo.endDate &&
                                <li>
                                    <span>End Date: </span>{moment.utc(new Date(incomeInfo.endDate)).format('dddd, MM YYYY')}
                                </li>
                            }
                            {incomeInfo.employerPhoneNumber &&
                                <li>
                                    <span>Employer Phone Number: </span>{NumberServices.formatPhoneNumber(incomeInfo.employerPhoneNumber)}
                                </li>
                            }
                            {incomeInfo.yearsInProfession &&
                                <li>
                                    <span>Years in Profession: </span>{incomeInfo.yearsInProfession} {incomeInfo.yearsInProfession > 1 ? 'Years' : 'Year'}
                                </li>
                            }
                            {incomeInfo.wayOfIncome.isPaidByMonthlySalary &&
                                <li>
                                    <span>Pay Type: </span>Salaried
                            </li>
                            }
                            {incomeInfo.incomeAddress &&
                                <li>
                                    <span>Address: </span>{StringServices.addressGenerator(incomeInfo.incomeAddress)}
                                </li>
                            }
                            {
                                loanApplicationReviewer.displayName !== incomeInfo.employerName &&
                                <li>
                                    <span>Name: </span>{incomeInfo.employerName}
                                </li>
                            }

                            {incomeInfo.wayOfIncome.isPaidByMonthlySalary ?
                                <li>
                                    <span>Monthly Income: $</span>{NumberServices.curruncyFormatterIncomeHome(incomeInfo.wayOfIncome.monthlySalary)}
                                </li> :
                                <li>
                                    <span>Annual Income: $</span>{NumberServices.curruncyFormatterIncomeHome(incomeInfo.wayOfIncome.monthlySalary)}
                                </li>
                            }

                        </ul>
                    </div>

                </div>
                <div className="r-actions">
                    {/*borrowerId: number, employmentCategoryId: number, incomeInfoId :number*/}
                    <button className="btn btn-edit" onClick={() => {
                        editIncome(incomeInfo, loanApplicationReviewer, getIncomeSectionReviewProto)
                    }}>
                        <span className="icon">
                            <EditIcon />
                        </span>
                        <span className="lbl">Edit</span>
                    </button>
                </div>
            </div>
        </>
    )
}
