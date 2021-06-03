import React from 'react';
import { ReviewSingleApplicantIcon, ReviewCoApplicantIcon, EditIcon, DeleteIcon } from '../../../../Shared/Components/SVGs';
import { GetReviewBorrowerInfoSectionProto } from '../../../../Entities/Models/types';
import { StringServices } from '../../../../Utilities/helpers/StringServices';
import {NumberServices} from "../../../../Utilities/helpers/NumberServices";

export const ApplicaitonReviewAfterConsentCard: React.FC<{
    resolveMaritialStatus: Function,
    loanApplicationReviewer: GetReviewBorrowerInfoSectionProto,
    deleteBorrower: Function,
    editBorrower: Function
}> = ({ loanApplicationReviewer, resolveMaritialStatus, deleteBorrower, editBorrower }) => {

    return (

        <>
            <div className="review-item">
                <div className="r-icon">
                    {(loanApplicationReviewer.ownTypeId == 1 ? <ReviewSingleApplicantIcon /> : <ReviewCoApplicantIcon />)}
                </div>
                <div className="r-content">
                    <div className="title">
                        <h4>{StringServices.capitalizeFirstLetter(loanApplicationReviewer.firstName)} {StringServices.capitalizeFirstLetter(loanApplicationReviewer.lastName)}</h4>
                    </div>
                    <div className="c-type">
                        {(loanApplicationReviewer.ownTypeId == 1 ? 'Primary-Applicant' : 'Co-Applicant')}
                    </div>
                    <div className="email">
                        <a href={`mailto:${loanApplicationReviewer.emailAddress}`}>
                            {loanApplicationReviewer.emailAddress}
                        </a>
                    </div>
                    {(loanApplicationReviewer.cellPhone) &&
                    <div className="phone">
                        <a href={`tel:${loanApplicationReviewer.cellPhone}`}>
                            {NumberServices.formatPhoneNumber(loanApplicationReviewer.cellPhone) }
                        </a>
                    </div>}

                    {(loanApplicationReviewer.maritalStatusId) &&
                        <div className="m-staus">
                            {(resolveMaritialStatus(loanApplicationReviewer.maritalStatusId)) ? resolveMaritialStatus(loanApplicationReviewer.maritalStatusId).name : ""}
                        </div>
                    }
                    {/*{(loanApplicationReviewer.isVaEligible) ?
                        <div className="otherinfo">
                            I am veteran
                        </div>:
                        <div className="otherinfo">
                            Not currently serving, veteran, or surviving spouse
                        </div>
                    }*/}
                    <div className="otherinfo">
                        {loanApplicationReviewer?.borrowerAddress && StringServices.addressGenerator(loanApplicationReviewer?.borrowerAddress)}
                    </div>

                </div>
                <div className="r-actions">
                    <button className="btn btn-edit" onClick={() => { editBorrower(loanApplicationReviewer.borrowerId, loanApplicationReviewer.ownTypeId) }}>
                        <span className="icon">
                            <EditIcon />
                        </span>
                        <span className="lbl">Edit</span>
                    </button>
                    {(loanApplicationReviewer.ownTypeId > 1 && <>
                        <button className="btn btn-delete" onClick={() => { deleteBorrower(loanApplicationReviewer.borrowerId, loanApplicationReviewer.firstName, loanApplicationReviewer.lastName) }}>
                            <span className="icon">
                                <DeleteIcon />
                            </span>
                        </button>
                    </>)}

                </div>
            </div>
           
        </>
    )
};
