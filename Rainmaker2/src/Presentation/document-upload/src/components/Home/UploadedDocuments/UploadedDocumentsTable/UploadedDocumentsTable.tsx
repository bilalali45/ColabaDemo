import React, { useState, useEffect, useCallback } from "react";
import { useHistory, Link } from "react-router-dom";

import { UploadedDocuments } from "../../../../entities/Models/UploadedDocuments";
import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Auth } from "../../../../services/auth/Auth";
import { Document } from "../../../../entities/Models/Document";
import DocUploadIcon from "../../../../assets/images/upload-doc-icon.svg";
import { DateFormat } from "../../../../utils/helpers/DateFormat";
import { DocumentView } from "../../../../shared/Components/DocumentView/DocumentView";

interface ViewDocumentType {
  id: string;
  requestId: string;
  docId: string;
  clientName?: string;
  fileId?: string;
}

export const UploadedDocumentsTable = () => {
  const [docList, setDocList] = useState<UploadedDocuments[] | [] | null>(null);
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType>();

  const history = useHistory();

  const fetchUploadedDocuments = async () => {
    const uploadedDocs = await DocumentActions.getSubmittedDocuments(
      Auth.getLoanAppliationId(),
      Auth.getTenantId()
    );

    if (uploadedDocs) {
      setDocList(uploadedDocs);
    }
  };

  const renderFileNameColumn = (data, params: ViewDocumentType) => {
    return (
      <td>
        {data.map((item: Document) => {
          const { clientName, id: fileId } = item;

          return (
            <Link
              className="block-element"
              to="#"
              onClick={() => {
                setCurrentDoc({
                  ...params,
                  fileId,
                  clientName,
                });
              }}
            >
              {clientName}
            </Link>
          );
        })}
      </td>
    );
  };

  const renderAddedColumn = (data) => {
    return (
      <td>
        {data.map((item: Document) => {
          return (
            <tr>
              <span className="block-element">
                {DateFormat(item.fileUploadedOn, true)}
              </span>
            </tr>
          );
        })}
      </td>
    );
  };

  const renderUploadedDocs = (data) => {
    return data.map((item: UploadedDocuments) => {
      if (!item.files.length) return;

      const { files, docId, requestId, id } = item;

      return (
        <tbody>
          <tr>
            <td>
              <em className="far fa-file"></em> {item.docName}
            </td>
            {renderFileNameColumn(files, { id, requestId, docId })}
            {renderAddedColumn(files)}
          </tr>
        </tbody>
      );
    });
  };

  const loanHomeHandler = () => {
    history.push("/activity");
  };

  const renderTable = (data) => {
    if (!data || data.length === 0) return;
    return (
      <table className="table  table-striped">
        <thead>
          <tr>
            <th>Documents</th>
            <th>File Name</th>
            <th>Added</th>
          </tr>
        </thead>
        {renderUploadedDocs(data)}
      </table>
    );
  };

  const renderNoData = () => {
    return (
      <div className="no-document">
        <div className="no-document--wrap">
          <div className="no-document--img">
            <img src={DocUploadIcon} alt="Your don't have any files!" />
          </div>
          <label htmlFor="inputno-document--text">
            Your don't have any files.
            <br />
            Go to{" "}
            <a tabIndex={-1} onClick={loanHomeHandler}>
              Loan Home
            </a>
          </label>
        </div>
      </div>
    );
  };

  useEffect(() => {
    if (!docList?.length) {
      fetchUploadedDocuments();
    }
  }, [docList]);

  return (
    <React.Fragment>
      <div className="UploadedDocumentsTable">
        {renderTable(docList)}

        {docList?.length === 0 && renderNoData()}
      </div>
      {!!currentDoc?.docId && (
        <DocumentView
          hideViewer={setCurrentDoc}
          {...currentDoc}
          fileId={currentDoc.fileId}
        />
      )}
    </React.Fragment>
  );
};
