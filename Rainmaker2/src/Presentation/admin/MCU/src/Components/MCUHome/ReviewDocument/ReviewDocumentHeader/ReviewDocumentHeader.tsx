import React from "react";

export const ReviewDocumentHeader = ({
  onClose,
  nextDocument,
  previousDocument,
}: {
  onClose: () => void;
  nextDocument: () => void;
  previousDocument: () => void;
}) => {
  return (
    <div
      id="ReviewDocumentHeader"
      data-component="ReviewDocumentHeader"
      className="review-document-header"
    >
      <div className="row">
        <div className="review-document-header--left col-md-4">
          <h2>Review Document</h2>
        </div>

        <div className="review-document-header--center col-md-4">
          <div className="btn-group">
            <button className="btn btn-default" onClick={previousDocument}>
              <em className="zmdi zmdi-arrow-left"></em> Review Previous
              Document
            </button>
            <button className="btn btn-default" onClick={nextDocument}>
              Review Next Document <em className="zmdi zmdi-arrow-right"></em>
            </button>
          </div>
        </div>

        <div className="review-document-header--right col-md-4">
          <button className="btn" onClick={onClose}>
            <em className="zmdi zmdi-close"></em>
          </button>
        </div>
      </div>
    </div>
  );
};
