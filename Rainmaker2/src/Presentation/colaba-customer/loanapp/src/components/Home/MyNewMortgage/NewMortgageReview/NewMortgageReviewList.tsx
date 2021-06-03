import {
    GetReviewBorrowerInfoSectionProto,
    MortgagePropertyReviewProto
} from '../../../../Entities/Models/types';
import React, { MouseEventHandler } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import { ReviewCoApplicantIcon, LockIcon, ReviewHomeIcon } from '../../../../Shared/Components/SVGs';


import { StringServices } from '../../../../Utilities/helpers/StringServices';

import {NewMortgageReviewCard} from "./NewMortgageReviewCard";
import {NewMortgagePropertyCard} from "./NewMortgagePropertyCard";



export const NewMortgageReviewList : React.FC<{
    mortgagePropertyReview: MortgagePropertyReviewProto,
    currentStep:string, 
    setcurrentStep:Function,
    resolveMaritialStatus :Function, 
    resolveLoanPurose :Function,
    editMortgage :Function,
    saveAndContinue :MouseEventHandler<HTMLButtonElement>,
    addBorrower :MouseEventHandler<HTMLButtonElement>, 
    editBorrower  :Function }> = ({ resolveMaritialStatus, mortgagePropertyReview, editBorrower,saveAndContinue, resolveLoanPurose, editMortgage }) => {
    return (
        <div className="compo-abt-yourSelf fadein ApplicaitonReviewAfterConsentList">
            <PageHead title="Review And Continue" showBackBtn = {false} />
            <div className="comp-form-panel review-panel colaba-form">
                <div className="row form-group">
                    <div className="col-md-12">
                        <h4>Please review the details and continue.</h4>
                        {(mortgagePropertyReview?.loanPurpose) &&
                            <div className="review-item">
                                <div className="r-icon">
                                    <ReviewHomeIcon/>
                                     {/* <img src={resolveLoanPurose(mortgagePropertyReview?.loanPurpose).image} /> */}
                                </div>
                                <div className="r-content">
                                    <div className="title">
                                        <h4>Type of transaction</h4>
                                    </div>
                                    <div className="otherinfo">
                                        {StringServices.removeText(resolveLoanPurose(mortgagePropertyReview?.loanPurpose).name,'I Want To','')}
                                    </div>
                                </div>
                                <div className="r-actions">
                                    <button className="btn btn-lock">
                                        <span className="icon">
                                            <LockIcon />
                                        </span>
                                    </button>
                                </div>
                            </div>
                        }

                        {(mortgagePropertyReview.applyingWithSomeone && false) &&
                            <div className="review-item">
                                <div className="r-icon">
                                    <ReviewCoApplicantIcon />
                                </div>
                                <div className="r-content">
                                    <div className="title">
                                        <h4>Applying with someone</h4>
                                    </div>
                                    <div className="otherinfo">
                                        {mortgagePropertyReview.applyingWithSomeone ? 'Yes' : 'No'}
                                    </div>
                                </div>
                                <div className="r-actions">
                                    <button className="btn btn-lock">
                                        <span className="icon">
                                            <LockIcon />
                                        </span>
                                    </button>
                                </div>
                            </div>
                        }
                        {(  mortgagePropertyReview?.borrowerReviews?.map((ar: GetReviewBorrowerInfoSectionProto) => {
                                return (
                                    <>
                                        <NewMortgageReviewCard  loanApplicationReviewer={ar} resolveMaritialStatus={resolveMaritialStatus}  editBorrower={editBorrower} key={ar.borrowerId} />
                                    </>
                                )
                            })
                        )}
                        <NewMortgagePropertyCard propertyDetails = {mortgagePropertyReview.propertyInfo} editMortgage = {editMortgage}/>


                    </div>
                </div>
                <div className="form-footer">
                <button className="btn btn-primary" onClick={saveAndContinue} > {"Save & Continue"} </button>
                </div>
            </div>
        </div>
    )
};