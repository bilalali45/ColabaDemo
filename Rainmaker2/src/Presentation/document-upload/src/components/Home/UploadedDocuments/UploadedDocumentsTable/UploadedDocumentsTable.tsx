import React, { useState, useEffect, useCallback, useContext } from "react";
import { useHistory, Link } from "react-router-dom";
import _ from "lodash";

import { UploadedDocuments } from "../../../../entities/Models/UploadedDocuments";
import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Auth } from "../../../../services/auth/Auth";
import { Document } from "../../../../entities/Models/Document";
import DocUploadIcon from "../../../../assets/images/upload-doc-icon.svg";
import { DateFormatWithMoment } from "../../../../utils/helpers/DateFormat";
//import { DocumentView } from "../../../../shared/Components/DocumentView/DocumentView";
import { DocumentView } from "rainsoft-rc";
import { Store } from "../../../../store/store";
import { DocumentsActionType } from "../../../../store/reducers/documentReducer";
import { clear } from "console";

interface ViewDocumentType {
  id: string;
  requestId: string;
  docId: string;
  clientName?: string;
  fileId?: string;
}

export const UploadedDocumentsTable = () => {
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType | null>();
  const [blobData, setBlobData] = useState<any | null>();

  const history = useHistory();

  const { state, dispatch } = useContext(Store);
  const { submittedDocs }: any = state.documents;

  useEffect(() => {
    fetchUploadedDocuments();
  }, []);

  const fetchUploadedDocuments = async () => {
    if (!submittedDocs) {
      let uploadedDocs = await DocumentActions.getSubmittedDocuments(
        Auth.getLoanAppliationId()
      );
      if (uploadedDocs) {
        dispatch({
          type: DocumentsActionType.FetchSubmittedDocs,
          payload: uploadedDocs,
        });
      }
    }
  };

  const checkFreezBody = async () => {
    if (document.body.style.overflow == "hidden") {
      document.body.removeAttribute("style");
    } else {
      document.body.style.overflow = "hidden";
    }
  };

  const renderFileNameColumn = (data, params: ViewDocumentType) => {
    return (
      <td>
        {data.map((item: Document, index: number) => {
          const { clientName, id: fileId } = item;

          return (
            <span className="block-element" key={index}>
              <Link
                to="#"
                className="link-filename"
                title={clientName}
                onClick={() => {
                  setCurrentDoc({
                    ...params,
                    fileId,
                    clientName,
                  });

                  checkFreezBody();
                }}
              >
                {clientName}
              </Link>
            </span>
          );
        })}
      </td>
    );
  };

  const renderAddedColumn = (data) => {
    return (
      <td>
        {data.map((item: Document, index: number) => {
          return (
            <span className="block-element" key={index}>
              {DateFormatWithMoment(item.fileUploadedOn, true)}
            </span>
          );
        })}
      </td>
    );
  };

  const renderUploadedDocs = (data) => {
    const sortedUploadedDocuments = _.orderBy(data, (item) => item.docName, [
      "asc",
    ]);

    return sortedUploadedDocuments.map(
      (item: UploadedDocuments, index: number) => {
        if (!item?.files?.length) return;
        const { files, docId, requestId, id } = item;
        const sortedFiles = _.orderBy(
          files,
          (file) => new Date(file.fileUploadedOn),
          ["desc"]
        );

        return (
          <tr key={index}>
            <td>
              <span className="doc-name" title={item.docName}>
                <em className="far fa-file"></em> {item.docName}
              </span>
            </td>
            {renderFileNameColumn(sortedFiles, { id, requestId, docId })}
            {renderAddedColumn(sortedFiles)}
          </tr>
        );
      }
    );
  };

  const loanHomeHandler = () => {
    history.push(`/activity/${Auth.getLoanAppliationId()}`);
  };

  const renderTable = (data) => {
    if (!data || data?.length === 0) return;
    return (
      <table className="table  table-striped">
        <thead>
          <tr>
            <th>Documents</th>
            <th>File Name</th>
            <th>Added</th>
          </tr>
        </thead>
        <tbody>{renderUploadedDocs(data)}</tbody>
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
          <label className="inputno-document--text">
            Your don't have any uploaded files.
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

  const getSubmittedDocumentForView = async (id, requestId, docId, fileId) => {
    const response = (await DocumentActions.getSubmittedDocumentForView({
      id,
      requestId,
      docId,
      fileId,
    })) as any;
    setBlobData(response);
  };

  const clearBlob = () => {
    DocumentActions.documentViewCancelToken.cancel();
    setBlobData(null);
  };

  const hdieViewer = () => {
    document.body.style.overflow = "visible";
    document.body.removeAttribute("style");
    clearBlob();
    setCurrentDoc(null);
  };

  return (
    <React.Fragment>
      <div className="UploadedDocumentsTable">
        {submittedDocs && renderTable(submittedDocs)}

        {submittedDocs?.length === 0 && renderNoData()}
      </div>
      {!!currentDoc && currentDoc?.docId && (
        <DocumentView
          hideViewer={hdieViewer}
          {...currentDoc}
          fileId={currentDoc.fileId}
          blobData={blobData}
          submittedDocumentCallBack={getSubmittedDocumentForView}
        />
      )}
    </React.Fragment>
  );
};
