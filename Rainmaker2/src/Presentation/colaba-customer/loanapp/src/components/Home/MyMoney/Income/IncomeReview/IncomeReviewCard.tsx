import React from 'react'
import {
    ReviewSingleApplicantIcon } from '../../../../../Shared/Components/SVGs';
import {GetIncomeSectionReviewIncomesProto, GetIncomeSectionReviewProto} from "./IncomeReview";
import {IncomeReviewIncomeCard} from "./IncomeReviewIncomeCard";

export const IncomeReviewCard: React.FC<{
    getIncomeSectionReviewProto: GetIncomeSectionReviewProto,
    editIncome: Function
}> = ({getIncomeSectionReviewProto, editIncome}) => {
    return (
        <>  
           {( getIncomeSectionReviewProto?.incomeTypes && <section className="r-e-history-list">
                <div className="review-item">
                    <div className="r-icon">
                        <ReviewSingleApplicantIcon/>
                    </div>
                    <div className="r-content">
                        <div className="title">
                            <h4>{getIncomeSectionReviewProto.borrowerName}</h4>
                        </div>
                        <div className="c-type">
                            {(getIncomeSectionReviewProto.ownType.id === 1 ? 'Primary-Applicant' : 'Co-Applicant')}
                        </div>

                    </div>
                </div>


                {(  getIncomeSectionReviewProto?.incomeTypes?.map((ar: GetIncomeSectionReviewIncomesProto) => {
                        return (
                            <>
                                <IncomeReviewIncomeCard loanApplicationReviewer={ar} editIncome={editIncome} getIncomeSectionReviewProto={getIncomeSectionReviewProto}/>
                            </>
                        )
                    })
                )}
            </section>)}
        </>
    )
}
