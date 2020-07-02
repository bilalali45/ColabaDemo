import React, {
  useState,
  useEffect,
  useCallback,
  FunctionComponent,
  Fragment,
} from "react";
import FileViewer from "react-file-viewer";
import printJS from "print-js";

import { SVGprint, SVGdownload, SVGclose, SVGfullScreen } from "../Assets/SVG";
import { DocumentActions } from "../../../store/actions/DocumentActions";
import { Auth as Storage } from "../../../services/auth/Auth";
import { Loader } from "../Assets/loader";
import { TransformWrapper, TransformComponent } from "react-zoom-pan-pinch";

interface DocumentViewProps {
  id: string;
  requestId: string;
  docId: string;
  fileId?: string;
  clientName?: string;
  hideViewer: (currentDoc) => void;
}

interface DocumentParamsType {
  filePath: string;
  fileType: string;
  blob: Blob;
}

export const DocumentView: FunctionComponent<DocumentViewProps> = ({
  id,
  requestId,
  docId,
  fileId,
  clientName,
  hideViewer,
}) => {
  const [documentParams, setDocumentParams] = useState<DocumentParamsType>({
    blob: new Blob(),
    filePath: "",
    fileType: "",
  });

  const getSubmittedDocumentForView = useCallback(async () => {
    try {
      const response = (await DocumentActions.getSubmittedDocumentForView({
        id,
        requestId,
        docId,
        fileId,
        tenantId: Storage.getTenantId(),
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
  }, [docId, fileId, id, requestId]);

  const printDocument = useCallback(() => {
    const { filePath, fileType } = documentParams;
    // At the moment we are just allowing images or pdf files to be uplaoded
    const type = ["jpeg", "jpg", "png"].includes(fileType) ? "image" : "pdf";

    printJS({ printable: filePath, type });
  }, [documentParams.filePath, documentParams.fileType]);

  const downloadFile = () => {
    let temporaryDownloadLink: HTMLAnchorElement;

    temporaryDownloadLink = document.createElement("a");
    temporaryDownloadLink.href = documentParams.filePath;
    temporaryDownloadLink.setAttribute("download", clientName!); // added ! because client name can't be null

    temporaryDownloadLink.click();
  };

  useEffect(() => {
    getSubmittedDocumentForView();
  }, [getSubmittedDocumentForView]);

  return (
    <div className="document-view" id="screen">
      <div className="document-view--header">
        <div className="document-view--header---options">
          <ul>
            {!!documentParams.filePath && (
              <Fragment>
                <li>
                  <button
                    className="document-view--button"
                    onClick={printDocument}
                  >
                    <SVGprint />
                  </button>
                </li>
                <li>
                  <button
                    className="document-view--button"
                    onClick={downloadFile}
                  >
                    <SVGdownload />
                  </button>
                </li>
              </Fragment>
            )}
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
          {!!documentParams.filePath ? (
            // <TransformWrapper
            //   defaultScale={1}
            //   defaultPositionX={200}
            //   defaultPositionY={100}
            // >
            //   {({ zoomIn, zoomOut, resetTransform, ...rest }) => (
            //     <React.Fragment>
            //       <div className="tools">
            //         <button onClick={zoomIn}>+</button>
            //         <button onClick={zoomOut}>-</button>
            //         <button onClick={resetTransform}>x</button>
            //       </div>
            //       <TransformComponent>
                    <FileViewer
                      fileType={documentParams.fileType}
                      filePath={documentParams.filePath}
                    />
            //       </TransformComponent>
            //     </React.Fragment>
            //   )}
            // </TransformWrapper>
          ) : (
              <Loader height={"94vh"} />
            )}
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
