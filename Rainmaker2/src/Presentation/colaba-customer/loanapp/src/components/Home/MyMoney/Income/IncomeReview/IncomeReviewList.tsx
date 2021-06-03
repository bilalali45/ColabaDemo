import React, { MouseEventHandler } from 'react';

import PageHead from "../../../../../Shared/Components/PageHead";

import {IncomeReviewCard} from "./IncomeReviewCard";
import {GetIncomeSectionReviewProto} from "./IncomeReview";



export const IncomeReviewList : React.FC<{
    borrowers: GetIncomeSectionReviewProto[],
    resolveMaritialStatus :Function,
    saveAndContinue: MouseEventHandler<HTMLButtonElement>,
    editIncome  :Function }> = ({ saveAndContinue,  borrowers, editIncome }) => {
    
    return (
        <div className="compo-abt-yourSelf fadein">
            <PageHead title="Review And Continue"/>

            <div className="comp-form-panel review-panel review-panel-e-history  colaba-form">
                <div className="row form-group">
                    <div className="col-md-12">
                        {(
                            borrowers?.map((getIncomeSectionReviewProto: GetIncomeSectionReviewProto) => {
                                return (
                                    <>
                                        <IncomeReviewCard getIncomeSectionReviewProto={getIncomeSectionReviewProto}  editIncome = {editIncome}  />
                                    </>
                                )
                            })
                        )}

                    </div>
                </div>

                <div className="form-footer">
                    <button className="btn btn-primary" onClick={saveAndContinue}>Save & Continue</button>
                </div>

            </div>

        </div>
    )
};
