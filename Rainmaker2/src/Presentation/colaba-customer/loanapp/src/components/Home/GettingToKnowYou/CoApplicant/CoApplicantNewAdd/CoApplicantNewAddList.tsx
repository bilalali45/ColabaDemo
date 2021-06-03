import React, { MouseEventHandler } from "react";

import PageHead from "../../../../../Shared/Components/PageHead";
import TooltipTitle from "../../../../../Shared/Components/TooltipTitle";

import CoApplicantNewAddCard from './CoApplicantNewAddCard'
import { GetReviewBorrowerInfoSectionProto, ReviewBorrowerInfo } from "../../../../../Entities/Models/types";
const CoApplicantNewAdd: React.FC<{
  allBorrowers: ReviewBorrowerInfo, currentStep: string, setcurrentStep: Function,
  saveAndContinue: MouseEventHandler<HTMLButtonElement>,
  addBorrower: MouseEventHandler<HTMLButtonElement>,
  deleteBorrower: Function,
  editBorrower: Function
}> = ({ allBorrowers, addBorrower, deleteBorrower, editBorrower,saveAndContinue }) => {

  return (
    <div className="compo-abt-yourSelf fadein">
      <PageHead  title="Personal Information" showBackBtn = {false} />
      <TooltipTitle title="Do you want to add another co-applicant to your loan?" />
      <div data-testid="personal-info-form">
        <div className="comp-form-panel colaba-form">
          <div className="row form-group">
            <div className="col-md-12">
              <div className="co-applicantList-warp">
                {
                  allBorrowers?.borrowerReviews.map((borrowerReviews: GetReviewBorrowerInfoSectionProto) => {
                    return (
                      <>
                        <CoApplicantNewAddCard borrower={borrowerReviews} deleteBorrower={deleteBorrower} editBorrower={editBorrower} />
                      </>
                    )
                  })
                }

              </div>
            </div>
          </div>
          <div className="form-footer">
            <button
              className="btn btn-primary"
              type="submit"
              onClick={saveAndContinue}
            >
              {"Save & Continue"}
            </button>
            <button className="btn btn-inline m-left" onClick={addBorrower}>{'Add Another Borrower'}</button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CoApplicantNewAdd;