import React, { useState, useEffect, useCallback, useContext } from "react";
import { useHistory, Link } from "react-router-dom";
import _ from "lodash";

import { UploadedDocuments } from "../../../../entities/Models/UploadedDocuments";
import { DocumentActions } from "../../../../store/actions/DocumentActions";
import { Auth } from "../../../../services/auth/Auth";
import { Document } from "../../../../entities/Models/Document";
import DocUploadIcon from "../../../../assets/images/upload-doc-icon.svg";
import DocUploadIconMobile from "../../../../assets/images/upload-doc-icon-mobile.svg";
import { DateFormatWithMoment } from "../../../../utils/helpers/DateFormat";
import { DocumentView } from "../../../../shared/Components/DocumentView/DocumentView";
// import { DocumentView } from "rainsoft-rc";
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
  const loan: any = state.loan;
  const { isMobile } = loan;
  useEffect(() => {
    fetchUploadedDocuments();
  }, []);

  const fetchUploadedDocuments = async () => {
    if (!submittedDocs) {
      let uploadedDocs = await DocumentActions.getSubmittedDocuments(
        Auth.getLoanAppliationId()
      );     
      if (uploadedDocs) {
        const sortedUploadedDocuments = _.orderBy(uploadedDocs, (item) => item.docName, ["asc",]);
        dispatch({
          type: DocumentsActionType.FetchSubmittedDocs,
          payload: sortedUploadedDocuments,
        });
      }
    }
  };

  const checkFreezBody = async () => {
    if (document.body.style.overflow == "hidden") {
      document.body.removeAttribute("style");
      document.body.classList.remove("lockbody");
    } else {
      document.body.style.overflow = "hidden";
      document.body.classList.add("lockbody");
    }
  };

  const renderFileNameColumn = (data, params: ViewDocumentType) => {
    if (isMobile?.value) {
      return (
        <div className="udl-m-otherinfo-wrap">
          {data.map((item: Document, index: number) => {
            const { clientName, id: fileId } = item;

            return (
              <div className="m-doc-filename" key={index}>
                <Link data-testid='doc-file-link'
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

                  <div className="pd-m-arrow-icon"><i className="zmdi zmdi-chevron-right"></i></div>
                </Link>

                {/* <div className="m-doc-date-wrap">
                  <div data-testid='added-date' className="m-doc-date" key={index}>
                    {DateFormatWithMoment(item.fileUploadedOn, true)}
                  </div>
                </div> */}
              </div>
            );
          })}
        </div>

      )
    }
    else {
      return (
        <td>
          {data.map((item: Document, index: number) => {
            const { clientName, id: fileId } = item;

            return (
              <span data-testid='doc-file' className="block-element" key={index}>
                <Link data-testid='doc-file-link'
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
    }
  };

  const renderAddedColumn = (data) => {
    if (isMobile?.value) {
      return;
    } else {
      return (
        <td>
          {data.map((item: Document, index: number) => {
            return (
              <span data-testid='added-date' className="block-element" key={index}>
                {DateFormatWithMoment(item.fileUploadedOn, true)}
              </span>
            );
          })}
        </td>
      );
    }
  };

  const renderUploadedDocs = (data) => {  
    return data.map(
      (item: UploadedDocuments, index: number) => {
        if (!item?.files?.length) return;
        const { files, docId, requestId, id } = item;
        const sortedFiles = _.orderBy(files, (file) => new Date(file.fileUploadedOn), ["desc"]);

        return (
          <tr key={index}>
            <td data-testid='doc-type'>
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

  const renderUploadedDocsMobile = (data) => {
    const sortedUploadedDocuments = _.orderBy(data, (item) => item.docName, ["asc",]);

    return sortedUploadedDocuments.map(
      (item: UploadedDocuments, index: number) => {
        if (!item?.files?.length) return;
        const { files, docId, requestId, id } = item;
        const sortedFiles = _.orderBy(files, (file) => new Date(file.fileUploadedOn), ["desc"]);

        return (
          <div className="uploaded-doc-list-mobile" key={index}>
            {/* <div data-testid='doc-type' className="udl-m-icon-wrap">
              
            </div> */}
            <div className="udl-m-content-wrap">
              <div className="doc-name" title={item.docName}>
                <em className="far fa-file"></em> {item.docName}
              </div>
              {renderFileNameColumn(sortedFiles, { id, requestId, docId })}
            </div>
          </div>
        );
      }
    );
  };

  const loanHomeHandler = () => {
    history.push(`/activity/${Auth.getLoanAppliationId()}`);
  };

  const renderTable = (data) => {
    if (!data || data?.length === 0) return;
    if (isMobile?.value) {
      return (renderUploadedDocsMobile(data))
    }
    else {
      return (
        <table data-testid='uploaded-docs-head' className="table  table-striped">
          <thead >
            <tr>
              <th>Documents</th>
              <th>File Name</th>
              <th>Added</th>
            </tr>
          </thead>
          <tbody>{renderUploadedDocs(data)}</tbody>
        </table>
      );
    }

  };

  const renderNoData = () => {
    return (
      <div className="no-document">
        <div className="no-document--wrap">
          <div className="no-document--img">
            <img src={isMobile?.value ? DocUploadIconMobile : DocUploadIcon} alt="Your don't have any files!" />
          </div>
          <label className="inputno-document--text">
            You don't have any uploaded files.
            <br />
            Go to{" "}
            <a tabIndex={-1} onClick={loanHomeHandler}>
              Loan Center.
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
    document.body.classList.remove("lockbody");
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
          isMobile={isMobile}
        />
      )}
    </React.Fragment>
  );
};
