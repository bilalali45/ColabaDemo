import React, { useState, useEffect, useContext } from "react";
import { useHistory } from "react-router-dom";
import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Endpoints } from "../../../../store/endpoints/Endpoints";
import { Auth } from "../../../../services/auth/Auth";
import IconEmptyDocRequest from "../../../../assets/images/empty-doc-req-icon.svg";
import IconDoneDocRequest from "../../../../assets/images/done-doc-req-icon.svg";
import { Store } from "../../../../store/store";
import { DocumentsActionType } from "../../../../store/reducers/documentReducer";
import { Loader } from "../../../../shared/Components/Assets/loader";

export const DocumentsStatus = () => {
  const { state, dispatch } = useContext(Store);

  const history = useHistory();

  const { pendingDocs, submittedDocs}: any = state.documents;
  const loan: any = state.loan;
    const {isMobile} = loan; 
  useEffect(() => {
    if (!pendingDocs?.length) {
      fetchPendingDocs();
    } 

    if (!submittedDocs?.length) {
      fetchSubmittedDocs();
    }
  }, []);

  const getStarted = () => {
    let loanApplicationId = Auth.getLoanAppliationId();
    history.push(`/documentsRequest/${loanApplicationId}`);
  };

  const fetchPendingDocs = async () => {
    let docsPending = await DocumentActions.getPendingDocuments(
      Auth.getLoanAppliationId()
    );
    if (docsPending) {
      dispatch({
        type: DocumentsActionType.FetchPendingDocs,
        payload: docsPending,
      });
      dispatch({
        type: DocumentsActionType.SetCurrentDoc,
        payload: docsPending[0],
      });
      // setPendingDocs(docsPending);
    }
  };

  const fetchSubmittedDocs = async () => {
    let submittedDocs = await DocumentActions.getSubmittedDocuments(
      Auth?.getLoanAppliationId()
    );
    if (submittedDocs) {
      dispatch({
        type: DocumentsActionType.FetchSubmittedDocs,
        payload: submittedDocs,
      });
    }
  };

  const renderNoPendingDocs = () => {
    return (
      <div className="DocumentStatus box-wrap empty">
        <div className="box-wrap--header clearfix">
          <h2 className="heading-h2"> Task List</h2>
        </div>
        <div className="box-wrap--body clearfix">
          <div className="edc-flex">
            <div className="eds-wrap">
              <div className="eds-img">
                <img src={IconEmptyDocRequest} alt="" />
              </div>
              <div className="eds-txt">
                <p data-testid="pending-docs-length">You have 0 tasks to complete.</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  };

  const renderCompletedDocs = () => {
    return (
      <div data-testid="complete-pending-docs" className="DocumentStatus box-wrap empty">
        <div className="box-wrap--header clearfix">
          <h2 className="heading-h2">Task List</h2>
        </div>
        <div className="box-wrap--body clearfix">
          <div className="edc-flex">
            <div className="eds-wrap">
              <div className="eds-img">
                <img src={IconDoneDocRequest} alt="" />
              </div>
              <div className="eds-txt">
                <p>
                  You’ve completed all tasks for now!
                  <br />
                  We’ll let you know if we need anything else.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  };


  if(isMobile?.value) {
    return (null)
}

  if (!pendingDocs) {
    return <Loader containerHeight={"476px"} />;
  }

  if (submittedDocs?.length && !pendingDocs?.length) {
    return renderCompletedDocs();
  }

  if (pendingDocs.length == 0) {
    return renderNoPendingDocs();
  }
  return (
    <div className="DocumentStatus hasData box-wrap">
      <div className="overlay-DocumentStatus">
        <div className="box-wrap--header clearfix">
          <h2 className="heading-h2"> Task List</h2>
          <p data-testid="borrower-pending-docs">
            You have{" "}
            <span className="DocumentStatus--count">{pendingDocs.length}</span>{" "}
            {pendingDocs.length == 1 ?"item":"items"} to complete
          </p>
        </div>
        <div className="box-wrap--body clearfix">
          <ul className="list">
            {pendingDocs.map((item: any, index: any) => {
              if (index < 8)
                return (
                  <li data-testid="borrower-pending-doc" title={item.docName} key={index}>
                    {" "}
                    {item.docName}{" "}
                  </li>
                );
            })}
          </ul>
        </div>
        <div className="box-wrap--footer clearfix">
          {/* <button className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button> */}
          <button data-testid="get-started" onClick={getStarted} className="btn btn-primary">
            Get Started <em className="zmdi zmdi-arrow-right"></em>
          </button>
        </div>
      </div>
    </div>
  );
};
