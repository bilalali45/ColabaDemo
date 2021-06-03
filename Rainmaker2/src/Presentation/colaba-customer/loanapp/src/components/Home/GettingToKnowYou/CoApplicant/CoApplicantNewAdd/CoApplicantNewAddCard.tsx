import React from "react";

import { AddCoAppIcon, DeleteIcon, IconView } from '../../../../../Shared/Components/SVGs';
import { GetReviewBorrowerInfoSectionProto } from "../../../../../Entities/Models/types";


const CoApplicantNewAddCard: React.FC<{
    borrower: GetReviewBorrowerInfoSectionProto, 
    deleteBorrower :Function,
    editBorrower :Function}> = ({borrower, deleteBorrower, editBorrower}) => {

  return (
    <div className="list-co-applicant">
      <div className="icon-co-app">
        <AddCoAppIcon />
      </div>
      <div className="cont-co-app">
        <h4>{borrower.firstName} {borrower.lastName}</h4>
        <p>{(borrower.ownTypeId === 1) ? "Primary Borrower" : "Co-Applicant"}</p>
      </div>
      <div className="actions-co-app">

        <button className="btn btn-view" data-borrower-id={borrower.borrowerId} onClick = {()=>{editBorrower(borrower.borrowerId, borrower.ownTypeId)}}>
          <span className="icon">
            <IconView />
          </span>
        </button>
        {(borrower.ownTypeId === 2) && 
        <button className="btn btn-delete" data-borrower-id={borrower.borrowerId} onClick = {()=>{deleteBorrower(borrower.borrowerId, borrower.firstName, borrower.lastName)}}>
          <span className="icon" >
            <DeleteIcon />
          </span>
        </button>}
      </div>
    </div>
  )
}

export default CoApplicantNewAddCard;