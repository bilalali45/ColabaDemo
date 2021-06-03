import React, { MouseEventHandler } from 'react';
import { ApplicaitonReviewListCard } from './ApplicaitonReviewListCard';
import { GetReviewBorrowerInfoSectionProto, ReviewBorrowerInfo } from '../../../../Entities/Models/types';
import PageHead from '../../../../Shared/Components/PageHead';


export const ApplicaitonReviewList : React.FC<{
    applicationReviewersList: ReviewBorrowerInfo,
    currentStep:string, 
    setcurrentStep:Function,
    resolveMaritialStatus :Function, 
    saveAndContinue: MouseEventHandler<HTMLButtonElement>,
    addBorrower :MouseEventHandler<HTMLButtonElement>, 
    deleteBorrower  :Function,
    editBorrower  :Function }> = ({ saveAndContinue, applicationReviewersList, resolveMaritialStatus,   deleteBorrower, addBorrower,editBorrower }) => {
    
    return (
        <div className="compo-abt-yourSelf fadein ApplicaitonReviewList">
            <PageHead title="Review And Continue" showBackBtn = {false}/>

            <div className="comp-form-panel review-panel colaba-form">
                <div className="row form-group">
                    <div className="col-md-12">
                        {(
                            applicationReviewersList?.borrowerReviews?.map((ar: GetReviewBorrowerInfoSectionProto) => {
                                return (
                                    <>                                    
                                        <ApplicaitonReviewListCard loanApplicationReviewer={ar} resolveMaritialStatus={resolveMaritialStatus}  editBorrower = {editBorrower} deleteBorrower={deleteBorrower}  />
                                    </>
                                )
                            })
                        )}

                    </div>
                </div>

                <div className="form-footer">

                <button className="btn btn-primary" onClick={saveAndContinue}>Save & Continue</button>
                    {(applicationReviewersList?.borrowerReviews.length< 4 &&
                        <button className="btn btn-inline m-left" onClick={addBorrower} >{'Add Co-applicant'}</button>
                    )}

                </div>

            </div>

        </div>
    )
};
