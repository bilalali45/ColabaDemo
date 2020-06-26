import React, { useState, useEffect, useCallback, useRef } from "react";
import FileViewer from "react-file-viewer";
import { useHistory } from "react-router-dom";

import { SVGprint, SVGdownload, SVGclose, SVGfullScreen } from "../Assets/SVG";
import { DocumentActions } from "../../../store/actions/DocumentActions";
import { Auth as Storage } from "../../../services/auth/Auth";

interface DocumentViewProps {
  id: string;
  requestId: string;
  docId: string;
  fileId?: string;
  clientName?: string;
  hideViewer: (currentDoc) => void;
}

export const DocumentView = (props: DocumentViewProps) => {
  const [documentParams, setDocumentParams] = useState<{
    filePath: string;
    fileType: string;
    blob: Blob;
  }>({
    blob: new Blob(),
    filePath: "",
    fileType: "",
  });
  const history = useHistory();

  const getSubmittedDocumentForView = useCallback(async () => {
    try {
      const { id, requestId, docId, fileId, clientName } = props;

      const response = (await DocumentActions.getSubmittedDocumentForView({
        id,
        requestId,
        docId,
        fileId,
        tenantId: Storage.getTenantId(),
        clientName,
      })) as any;

      const fileType: string = response.headers["content-type"];

      const documentBlob = new Blob([response.data], {
        type: fileType,
      });

      // URL required to view the document
      const filePath = URL.createObjectURL(documentBlob);

      setDocumentParams({
        blob: documentBlob,
        filePath,
        fileType: fileType.replace("image/", "").replace("application/", ""),
      });
    } catch (error) {
      alert("Something went wrong. Please try again later.");
    }
  }, [props]);

  const downloadFile = () => {
    let tempLink: any = null;
    tempLink = document.createElement("a");
    tempLink.href = documentParams.filePath;
    tempLink.setAttribute("download", clientName);
    tempLink.click();
  };

  useEffect(() => {
    getSubmittedDocumentForView();
  }, [getSubmittedDocumentForView]);

  if (!documentParams.filePath) return <></>;

  const { clientName, hideViewer } = props;

  return (
    <div className="document-view" id="screen">
      <div className="document-view--header">
        <div className="document-view--header---options">
          <ul>
            <li>
              <button className="document-view--button">
                <SVGprint />
              </button>
            </li>
            <li>
              <button
                className="document-view--button"
                onClick={() => (clientName ? downloadFile() : {})}
              >
                <SVGdownload />
              </button>
            </li>
            <li>
              <button
                className="document-view--button"
                onClick={() => hideViewer({})}
              >
                <SVGclose />
              </button>
            </li>
          </ul>
        </div>

        <span className="document-view--header---title">{clientName}</span>

        <div className="document-view--header---controls">
          <ul>
            <li>
              <button className="document-view--arrow-button">
                <em className="zmdi "></em>
              </button>
            </li>
            <li>
              <span className="document-view--counts">
                <input type="text" size={4} value="" />
              </span>
            </li>
            <li>
              <button className="document-view--arrow-button">
                <em className="zmdi "></em>
              </button>
            </li>
          </ul>
        </div>
      </div>
      <div className="document-view--body">
        <FileViewer
          fileType={documentParams.fileType}
          filePath={documentParams.filePath}
        />
      </div>
      <div className="document-view--floating-options">
        <ul>
          <li>
            <button className="button-float">
              <em className="zmdi zmdi-plus"></em>
            </button>
          </li>
          <li>
            <button className="button-float">
              <em className="zmdi zmdi-minus"></em>
            </button>
          </li>
          <li>
            <button className="button-float">
              <SVGfullScreen />
            </button>
          </li>
        </ul>
      </div>
      <iframe id="receipt" style={{ display: "none" }} title="Receipt" />
    </div>
  );
};
