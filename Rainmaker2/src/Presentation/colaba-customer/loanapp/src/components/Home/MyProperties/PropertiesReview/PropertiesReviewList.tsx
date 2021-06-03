import { MyPropertyReviewProto } from '../../../../Entities/Models/types';
import React, { MouseEventHandler } from 'react';
import PageHead from '../../../../Shared/Components/PageHead';
import { ReviewSingleApplicantIcon } from '../../../../Shared/Components/SVGs';
import { PropertyReviewCard } from './PropertyReviewCard';



export const PropertiesReviewList: React.FC<{
    myPropertyReviewProto: MyPropertyReviewProto,
    saveAndContinue: MouseEventHandler<HTMLButtonElement>,
    editProperty: Function
}> = ({ myPropertyReviewProto, editProperty, saveAndContinue }) => {

    return (
        <div className="compo-abt-yourSelf fadein ApplicaitonReviewAfterConsentList" data-testid="propertyReviewList">
            <PageHead title="Review And Continue" showBackBtn={false} />
            <div className="comp-form-panel review-panel review-panel-e-history  colaba-form no-minheight">
                <div className="row form-group">
                    <div className="col-md-12">
                        <section className="r-e-history-list">
                            <div className="review-item">
                                <div className="r-icon">
                                    <ReviewSingleApplicantIcon />
                                </div>
                                <div className="r-content">
                                    <div className="title">
                                        <h4>{myPropertyReviewProto?.primaryApplicantName}</h4>
                                    </div>
                                    <div className="c-type">
                                        Primary-Applicant
                                </div>

                                </div>
                            </div>

                            {
                                myPropertyReviewProto?.propertyList?.filter(x => x.typeId == 1)?.length > 0 && myPropertyReviewProto ? <><PropertyReviewCard
                                    properties={myPropertyReviewProto?.propertyList?.filter(x => x.typeId == 1)}
                                    heading="Current Residence"
                                    editProperty={editProperty}
                                />
                                </> : null
                            }

                            {
                                myPropertyReviewProto?.propertyList?.filter(x => x.typeId != 1)?.length > 0 && myPropertyReviewProto ? <><PropertyReviewCard
                                    properties={myPropertyReviewProto?.propertyList?.filter(x => x.typeId != 1)}
                                    heading="Additional Property"
                                    editProperty={editProperty}
                                />
                                </> : null
                            }
                        </section>
                    </div>
                </div>
                <div className="form-footer">
                    <button data-testid="save-btn" className="btn btn-primary" onClick={saveAndContinue} > {"Save & Continue"} </button>
                </div>
            </div>
        </div>
    )
};