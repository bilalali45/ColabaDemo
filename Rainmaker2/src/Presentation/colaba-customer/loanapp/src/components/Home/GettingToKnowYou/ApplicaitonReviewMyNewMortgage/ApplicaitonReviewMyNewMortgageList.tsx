import { GetReviewBorrowerInfoSectionProto, ReviewBorrowerInfo } from '../../../../Entities/Models/types';
import React, { MouseEventHandler } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import { ReviewCoApplicantIcon, LockIcon, ReviewHomeIcon } from '../../../../Shared/Components/SVGs';

import { ApplicaitonReviewMyNewMortgageCard } from './ApplicaitonReviewMyNewMortgageListCard';
import { StringServices } from '../../../../Utilities/helpers/StringServices';



export const ApplicaitonReviewMyNewMortgageList : React.FC<{
    reviewBorrowerInfo: ReviewBorrowerInfo,
    currentStep:string, 
    setcurrentStep:Function,
    resolveMaritialStatus :Function, 
    resolveLoanPurose :Function, 
    saveAndContinue :MouseEventHandler<HTMLButtonElement>,
    editBorrower  :Function }> = ({ resolveMaritialStatus, reviewBorrowerInfo, editBorrower,saveAndContinue, resolveLoanPurose }) => {
    return (
        <div className="compo-abt-yourSelf fadein ApplicaitonReviewMyNewMortgageList">
            <PageHead title="Review And Continue" showBackBtn = {false} />
            <div className="comp-form-panel review-panel colaba-form">
                <div className="row form-group ">
                    <div className="col-md-12">
                        <h4>Please review the details and continue.</h4>
                        {(reviewBorrowerInfo?.loanPurpose) &&
                            <div className="review-item">
                                <div className="r-icon">
                                <ReviewHomeIcon/>
                                     {/* <img src={resolveLoanPurose(reviewBorrowerInfo?.loanPurpose).image} /> */}
                                </div>
                                <div className="r-content">
                                    <div className="title">
                                        <h4>Type of transaction</h4>
                                    </div>
                                    <div className="otherinfo">
                                        {StringServices.removeText(resolveLoanPurose(reviewBorrowerInfo?.loanPurpose).name,'I Want To','')}
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

                        {(reviewBorrowerInfo.applyingWithSomeone && false) &&
                            <div className="review-item">
                                <div className="r-icon">
                                    <ReviewCoApplicantIcon />
                                </div>
                                <div className="r-content">
                                    <div className="title">
                                        <h4>Applying with someone</h4>
                                    </div>
                                    <div className="otherinfo">
                                        {reviewBorrowerInfo.applyingWithSomeone ? 'Yes' : 'No'}
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
                        {(  reviewBorrowerInfo?.borrowerReviews?.map((ar: GetReviewBorrowerInfoSectionProto) => {
                                return (
                                    <>
                                        <ApplicaitonReviewMyNewMortgageCard loanApplicationReviewer={ar} resolveMaritialStatus={resolveMaritialStatus} editBorrower={editBorrower} />
                                    </>
                                )
                            })
                        )}

                    </div>
                </div>
                <div className="form-footer">
                <button className="btn btn-primary" onClick={saveAndContinue} > {"Save & Continue"} </button>
                </div>
            </div>
        </div>
    )
};