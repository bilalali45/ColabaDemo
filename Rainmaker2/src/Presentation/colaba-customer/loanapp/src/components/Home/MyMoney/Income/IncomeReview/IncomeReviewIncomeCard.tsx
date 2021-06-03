import React from 'react'
import {
    GetIncomeSectionReviewIncomesProto,
    GetIncomeSectionReviewProto, incomeInfo
} from "./IncomeReview";
import {IncomeReviewIncomeSubCard} from "./IncomeReviewIncomeSubCard";

export const IncomeReviewIncomeCard: React.FC<{
    loanApplicationReviewer: GetIncomeSectionReviewIncomesProto,
    getIncomeSectionReviewProto: GetIncomeSectionReviewProto,
    editIncome: Function
}> = ({loanApplicationReviewer, editIncome, getIncomeSectionReviewProto}) => {
    return (
        <>
            {loanApplicationReviewer &&
            <>
                {/* <div className="review-item-e-history">
                    <div className="r-content">
                        <div className="eh-title">
                            <h4>{loanApplicationReviewer.incomeCategory.id < 7 ?loanApplicationReviewer.incomeCategory.displayName:loanApplicationReviewer.displayName}</h4>
                        </div>
                    </div>
                </div> */}
                {loanApplicationReviewer?.incomeList?.map((incomeInfo: incomeInfo) => {
                    return (
                        <>
                            <IncomeReviewIncomeSubCard incomeInfo={incomeInfo.incomeInfo}
                                                       loanApplicationReviewer={loanApplicationReviewer}
                                                       getIncomeSectionReviewProto={getIncomeSectionReviewProto}
                                                       editIncome={editIncome}/>
                        </>
                    )
                })}
            </>

            }
        </>
    )
}
