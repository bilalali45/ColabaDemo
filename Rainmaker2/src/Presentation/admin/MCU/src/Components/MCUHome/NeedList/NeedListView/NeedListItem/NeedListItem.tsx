import React, { FunctionComponent } from "react";
import { NeedListDocumentType } from "../../../../../Entities/Types/Types";

interface NeedListItemProps {
  index: number;
  toDocumentReview: (index: number) => void;
}

export const NeedListItem: FunctionComponent<
  NeedListDocumentType & NeedListItemProps
> = (props) => {
  const { docName, status, files, toDocumentReview, index } = props;
  let statusBullet;

  if (status === "Pending review") {
    statusBullet = "pending";
  } else if (status === "Started") {
    statusBullet = "started";
  } else if (status === "Borrower") {
    statusBullet = "borrower";
  } else if (status === "Completed") {
    statusBullet = "completed";
  }

  const pendingReview = status === "Pending review";

  return (
    <div className="tr row-shadow">
      <div className="td">
        <span className="f-normal">
          <strong>{docName}</strong>
        </span>
      </div>
      <div className="td">
        <span className={`status-bullet ${statusBullet}`}></span> {status}
      </div>
      <div className="td">
        {!!files.length &&
          files.map((file) => (
            <span className="block-element">{file.clientName}</span>
          ))}
      </div>
      <div className="td">
        <span className="block-element">
          <a href="">
            <em className="icon-refresh success"></em>
          </a>
        </span>
        {!!pendingReview && (
          <React.Fragment>
            <span className="block-element">
              <a href="">
                <em className="icon-refresh failed"></em>
              </a>
            </span>
            <span className="block-element">
              <a href="">
                <em className="icon-refresh success"></em>
              </a>
            </span>
          </React.Fragment>
        )}
      </div>
      <div className="td">
        {!!pendingReview ? (
          <a
            href="#"
            className="btn btn-secondry btn-sm"
            onClick={() => (pendingReview ? toDocumentReview(index) : {})}
          >
            Review
          </a>
        ) : (
            <React.Fragment>
              <a href="" className="btn btn-default btn-sm">
                Details
            </a>
              <a href="" className="btn btn-delete btn-sm">
                <em className="zmdi zmdi-close"></em>
              </a>
            </React.Fragment>
          )}
      </div>
    </div>
  );
};
