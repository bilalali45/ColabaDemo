import React from 'react';
import Dropdown from 'react-bootstrap/Dropdown';
import { NeedList } from '../../../../Entities/Models/NeedList';

import { ReviewDocumentActivityLog } from '../ReviewDocumentActivityLog/ReviewDocumentActivityLog';

export const ReviewDocumentHeader = ({
  id,
  docId,
  requestId,
  hideNextPreviousNavigation,
  buttonsEnabled,
  onClose,
  nextDocument,
  previousDocument,
  perviousDocumentButtonDisabled,
  nextDocumentButtonDisabled,
  documentDetail,
  haveDocuments,
  currentDocument
}: {
  id: string | null;
  docId: string,
  requestId: string
  hideNextPreviousNavigation: boolean;
  buttonsEnabled: boolean;
  onClose: () => void;
  nextDocument: () => void;
  previousDocument: () => void;
  perviousDocumentButtonDisabled: boolean;
  nextDocumentButtonDisabled: boolean;
  documentDetail: boolean;
  haveDocuments?: boolean;
  currentDocument:NeedList
}) => {
  console.log(currentDocument)
  return (
    <div data-testid = "review-headerts"
      id="ReviewDocumentHeader"
      data-component="ReviewDocumentHeader"
      className="review-document-header"
    >
      <div className="row">
        <div className="review-document-header--left col-md-4">
          <h2>{!!documentDetail ? 'Document Details' : 'Review Document'}</h2>
        </div>
        {!hideNextPreviousNavigation && (
          <React.Fragment>
            <div className="review-document-header--center col-md-4">
              <div className="btn-group">
                <button
                data-testid="previous-doc-btn"
                  className="btn btn-default"
                  disabled={perviousDocumentButtonDisabled}
                  onClick={buttonsEnabled ? previousDocument : () => { }}
                >
                  <em className="zmdi zmdi-arrow-left"></em> Review Previous
                  Document
                </button>
                <button
                data-testid="next-doc-btn"
                  className="btn btn-default"
                  disabled={nextDocumentButtonDisabled}
                  onClick={buttonsEnabled ? nextDocument : () => { }}
                >
                  Review Next Document{' '}
                  <em className="zmdi zmdi-arrow-right"></em>
                </button>
              </div>
            </div>
          </React.Fragment>
        )}
        <div
          className={`review-document-header--right col-md-${
            !hideNextPreviousNavigation ? 4 : 8
            }`}
        >
          {/* <button className="btn btn-primary">Activity Log</button> */}
          {currentDocument.status !== "Manually added" && haveDocuments === false ? (
            <Dropdown>
              <Dropdown.Toggle
                size="lg"
                variant="primary"
                className="mcu-dropdown-toggle no-caret"
                id="dropdown-basic"
              >
                Activity Log
            </Dropdown.Toggle>
              {id !== null && (
                <Dropdown.Menu>
                  <ReviewDocumentActivityLog requestId={requestId} docId={docId} id={id} />
                </Dropdown.Menu>
              )}
            </Dropdown>) : null
          }
          <button data-testid="review-closeBtnTs" className="btn btn-close" onClick={onClose}>
            <em className="zmdi zmdi-close"></em>
          </button>
        </div>
      </div>
    </div>
  );
};
