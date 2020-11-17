import React, {
  useState,
  useEffect,
  useCallback,
  FunctionComponent,
  Fragment,
  useContext
} from "react";
import FileViewer from "react-file-viewer";
import printJS from "print-js";
import { SVGprint, SVGdownload, SVGclose, SVGfullScreen } from "../Assets/SVG";
import { Loader } from "../Assets/loader";
import { TransformWrapper, TransformComponent } from "react-zoom-pan-pinch";
import { Store } from "../../../store/store";


interface DocumentViewProps {
  id: string;
  requestId: string;
  docId: string;
  blobData?: any | null;
  submittedDocumentCallBack?: Function;
  fileId?: string;
  clientName?: string;
  hideViewer: (currentDoc) => void;
  file?: any;
  clearBlob?: Function;
  isMobile?:any
}

let timer: any = null;

interface DocumentParamsType {
  filePath: string;
  fileType: string;
  blob: any;
}

export const DocumentView: FunctionComponent<DocumentViewProps> = ({
  id,
  requestId,
  docId,
  fileId,
  clientName,
  hideViewer,
  file,
  blobData,
  submittedDocumentCallBack,
  clearBlob,
  isMobile
}) => {
  const [documentParams, setDocumentParams] = useState<DocumentParamsType>({
    blob: new Blob(),
    filePath: "",
    fileType: "",
  });
  const { state, dispatch } = useContext(Store);
  const loan: any = state.loan;
  //const { isMobile } = loan;
  const [pan, setPan] = useState<any>(true);
  const [scale, setScale] = useState<any>(1);
  const getDocumentForViewBeforeUpload = useCallback(() => {
    const fileBlob = new Blob([file], { type: "image/png" });
    const filePath = URL.createObjectURL(fileBlob);
    file &&
      setDocumentParams({
        blob: file,
        filePath,
        fileType: file.type.replace("image/", "").replace("application/", ""),
      });
  }, [file]);


  useEffect(() => {
    setPan(pan)
  }, [pan])
  const enabalePan = (e: any) => {
    setScale(e.scale)
    return e.scale;
  };
  useEffect(() => {
    getPanValue(scale)
  }, [scale])

  const getSubmittedDocumentForView = useCallback(async () => {
    try {
      if (submittedDocumentCallBack) {
        submittedDocumentCallBack(id, requestId, docId, fileId);
      }

      // URL required to view the document
    } catch (error) {
      console.log(error);
      alert("Something went wrong. Please try again later.");
      hideViewer({});
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

  const onEscapeKeyPressed = useCallback(
    (event) => {
      if (event.keyCode === 27) {
        hideViewer({});
      }
    },
    [hideViewer]
  );

  useEffect(() => {
    if (file) {
      getDocumentForViewBeforeUpload();
    } else {
      if (!blobData) {
        getSubmittedDocumentForView();
      } else {
        const fileType: string = blobData.headers["content-type"];
        const documentBlob = new Blob([blobData.data], { type: fileType });
        const filePath = URL.createObjectURL(documentBlob);
        setDocumentParams({
          blob: documentBlob,
          filePath,
          fileType: fileType.replace("image/", "").replace("application/", ""),
        });
      }
    }
  }, [
    getSubmittedDocumentForView,
    getDocumentForViewBeforeUpload,
    file,
    blobData,
  ]);

  useEffect(() => {
    window.addEventListener("keydown", onEscapeKeyPressed, false);

    /*var elements = document.getElementsByClassName("classname");
    for (var i = 0; i < elements.length; i++) {
      elements[i].addEventListener('click', ()=>{ document.body.removeAttribute('style'); }, false);
    }*/

    //document.querySelector('.document-view--button').addEventListener("click", ()=>{  document.body.removeAttribute('style');  })

    //this will remove listener on unmount
    return () => {
      window.removeEventListener("keydown", onEscapeKeyPressed, false);

      document
        .getElementById("closeDocumentView")
        ?.addEventListener("click", () => {
          document.body.removeAttribute("style");
        });
    };
  }, [onEscapeKeyPressed]);


  const removeOverflow = () => {
    document.body.removeAttribute("style");
  };



  const getPanValue = (e: any) => {
    // let a:any = e;

    if (e > 1) {
      setPan(false)
      console.log(e)
    } else {
      setPan(true)
    }
  }




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
                id={"closeDocumentView"}
                className="document-view--button"
                onClick={() => {
                  hideViewer(false);
                }}
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
      <div className="zoomview-wraper">
        {isMobile?.value ? <TransformWrapper
          defaultScale={1}
          wheel={{ wheelEnabled: false, touchPadEnabled: true }}
          pan={{ disabled: pan, animationTime: 0  }}
          zoomIn={{ animation: false, animationTime: 0 }}
          zoomOut={{ animation: false, animationTime: 0 }}
          reset={{ animation: false, animationTime: 0 }}
          doubleClick={{ disabled: true }}
          scalePadding={{ animationTime: 0} }
          onZoomChange={(e) => { enabalePan(e) }}
        >
          {({ zoomIn, zoomOut, resetTransform }) => (
            <div>
              <TransformComponent>
                <div className="document-view--body" data-private>
                  {!!documentParams.filePath ? (
                    <FileViewer
                      fileType={documentParams.fileType}
                      filePath={documentParams.filePath}
                    />
                  ) : (
                      <Loader height={"94vh"} />
                    )}
                </div>
              </TransformComponent>
              <div className="document-view--floating-options">
                <ul>
                  <li>
                    <button className="button-float" onClick={zoomIn}>
                      <em className="zmdi zmdi-plus"></em>
                    </button>
                  </li>
                  <li>
                    <button className="button-float" onClick={zoomOut}>
                      <em className="zmdi zmdi-minus"></em>
                    </button>
                  </li>
                  <li>
                    <button className="button-float" onClick={resetTransform}>
                      <SVGfullScreen />
                    </button>
                  </li>
                </ul>
              </div>
            </div>
          )}

        </TransformWrapper>
          : <TransformWrapper
            defaultScale={1}
            wheel={{ wheelEnabled: false }}
          >

            {({ zoomIn, zoomOut, resetTransform }) => (
              <div>
                <TransformComponent>
                  <div className="document-view--body" data-private>
                    {!!documentParams.filePath ? (
                      <FileViewer
                        fileType={documentParams.fileType}
                        filePath={documentParams.filePath}
                      />
                    ) : (
                        <Loader height={"94vh"} />
                      )}
                  </div>
                </TransformComponent>
                <div className="document-view--floating-options">
                  <ul>
                    <li>
                      <button className="button-float" onClick={zoomIn}>
                        <em className="zmdi zmdi-plus"></em>
                      </button>
                    </li>
                    <li>
                      <button className="button-float" onClick={zoomOut}>
                        <em className="zmdi zmdi-minus"></em>
                      </button>
                    </li>
                    <li>
                      <button className="button-float" onClick={resetTransform}>
                        <SVGfullScreen />
                      </button>
                    </li>
                  </ul>
                </div>
              </div>
            )}

          </TransformWrapper>
        }




      </div>
    </div>
  );
};
