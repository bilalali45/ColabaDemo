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
  const [docList, setDocList] = useState<UploadedDocuments[] | [] | null>(null);
  const [currentDoc, setCurrentDoc] = useState<ViewDocumentType>();
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
        Auth.getLoanAppliationId(),
        Auth.getTenantId()
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
        {data.map((item: Document) => {
          const { clientName, id: fileId } = item;

          return (
            <span className="block-element">
              <Link
                to="#"
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
        {data.map((item: Document) => {
          return (
            <span className="block-element">
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

    return sortedUploadedDocuments.map((item: UploadedDocuments) => {
      if (!item?.files?.length) return;
      const { files, docId, requestId, id } = item;
      const sortedFiles = _.orderBy(
        files,
        (file) => new Date(file.fileUploadedOn),
        ["desc"]
      );

      return (
        <tr>
          <td>
            <span className="doc-name">
              <em className="far fa-file"></em> {item.docName}
            </span>
          </td>
          {renderFileNameColumn(sortedFiles, { id, requestId, docId })}
          {renderAddedColumn(sortedFiles)}
        </tr>
      );
    });
  };

  const loanHomeHandler = () => {
    history.push("/activity");
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

  const getSubmittedDocumentForView = async (
    id,
    requestId,
    docId,
    fileId,
    tenantId
  ) => {
    const response = (await DocumentActions.getSubmittedDocumentForView({
      id,
      requestId,
      docId,
      fileId,
      tenantId,
    })) as any;
    setBlobData(response);
  };
  const clearBlob = () => {
    DocumentActions.documentViewCancelToken.cancel();
    setBlobData(null);
  };
  return (
    <React.Fragment>
      <div className="UploadedDocumentsTable">
        {submittedDocs && renderTable(submittedDocs)}

        {submittedDocs?.length === 0 && renderNoData()}
      </div>
      {currentDoc?.docId && (
        <DocumentView
          hideViewer={(value: boolean) => {
            document.body.style.overflow = "visible";
            document.body.removeAttribute("style");
            clearBlob();
            if (value === false) {
              let abc: any = {};
              setCurrentDoc(abc);
            }
          }}
          {...currentDoc}
          fileId={currentDoc.fileId}
          tenantId={Auth.getTenantId()}
          blobData={blobData}
          submittedDocumentCallBack={getSubmittedDocumentForView}
        />
      )}
    </React.Fragment>
  );
};
