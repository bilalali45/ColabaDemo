import React, { useEffect, useCallback, useState, useRef } from 'react';
import { Http } from 'rainsoft-js';
import Spinner from 'react-bootstrap/Spinner';
import { DocumentSnipet } from './DocumentSnipet/DocumentSnipet';
import { DocumentFileType, FileType } from '../../../../Entities/Types/Types';
import { NeedListEndpoints } from '../../../../Store/endpoints/NeedListEndpoints';
import { NeedList } from '../../../../Entities/Models/NeedList';
import { DocumentStatus } from '../../../../Entities/Types/Types';
import Dropdown from 'react-bootstrap/Dropdown';
import { ReviewDocumentActivityLog } from '../ReviewDocumentActivityLog/ReviewDocumentActivityLog';
import { ReviewDocumentActions } from '../../../../Store/actions/ReviewDocumentActions';

const Footer = ({
  acceptDocumentOnly,
  acceptDocument,
  rejectDocument,
  setRejectPopup,
  rejectModalOpen,
  status,
  acceptRejectEnabled,
}: {
  acceptDocumentOnly?: any;
  acceptDocument: () => void;
  rejectDocument: () => void;
  setRejectPopup: () => void;
  rejectModalOpen: boolean;
  status?: string;
  acceptRejectEnabled: boolean;
  //requestAgain: () => void;
}) => {

  const [acceptDocumentAlert, setAcceptDocumentAlert] = useState<number>(0);
  const rejectAndCloseRejectPopUp = () => {
    rejectDocument();
  };

  const checkAlertMsgForAcceptDocumentOnly = (status:number) => {
    acceptDocumentOnly();
    setAcceptDocumentAlert(status);
  }

  if (status === DocumentStatus.COMPLETED) {

    if (rejectModalOpen) {
      return (
        <footer className="document-statement--footer">
          <div className="row">
            <div className="col-md-6">
              <button
                className="btn btn-secondry btn-block"
                disabled={acceptRejectEnabled}
                onClick={setRejectPopup}
              >
                Cancel
              </button>
            </div>
            <div className="col-md-6">
              <button
                className="btn btn-primary btn-block"
                disabled={acceptRejectEnabled}
                onClick={rejectAndCloseRejectPopUp}
              >
                Add to Draft
              </button>
            </div>
          </div>
        </footer>
      );
    } else {
      return (
        // <footer
        //   className="document-statement--footer alert alert-success"
        //   role="alert"
        // >
        //   This document has been accepted.
        // </footer>
        <footer className="document-statement--footer">
          <div className="row">
            <div className="col-md-6"></div>
            <div className="col-md-6">
              <button
                data-testid="accept-doc-btn"
                className="btn btn-primary btn-block"
                //disabled={acceptRejectEnabled}
                onClick={setRejectPopup}
              >
                Request Again
            </button>
            </div>
          </div>
        </footer>
      );
    }


  } else if (status === DocumentStatus.IN_DRAFT) {
    return (
      <footer
        className="document-statement--footer alert alert-primary"
        role="alert"
      >
        This document has been saved as draft.
      </footer>
    );
  } else if (status === DocumentStatus.STARTED || status === DocumentStatus.BORROWER_TO_DO) {

    if (acceptDocumentAlert == 1) {
      return (
        <footer className="document-statement--footer">
          <div className="row">
            <div className="col-md-6">
              <button
                className="btn btn-secondry btn-block"
                //disabled={acceptRejectEnabled}
                onClick={()=>checkAlertMsgForAcceptDocumentOnly(0)}
              >
                No
              </button>
            </div>
            <div className="col-md-6">
              <button
                data-testid="accept-doc-btn"
                className="btn btn-primary btn-block"
                //disabled={acceptRejectEnabled}
                onClick={()=>{checkAlertMsgForAcceptDocumentOnly(2);acceptDocument(); }}
              >
                Yes
              </button>
            </div>
          </div>
        </footer>
      )
    } else if (acceptDocumentAlert == 2) {
      return (
        <footer className="document-statement--footer alert alert-success">This document has been accepted.</footer>
      )
    } else {
      return (
        <footer className="document-statement--footer">
          <div className="row">
            <div className="col-md-6"></div>
            <div className="col-md-6">
              <button
                data-testid="accept-doc-btn"
                className="btn btn-primary btn-block"
                //disabled={acceptRejectEnabled}
                onClick={()=>checkAlertMsgForAcceptDocumentOnly(1)} // acceptDocument
              >
                Accept Document
              </button>
            </div>
          </div>
        </footer>
      )
    }

  } else if (rejectModalOpen) {
    return (
      <footer className="document-statement--footer">
        <div className="row">
          <div className="col-md-6">
            <button
              className="btn btn-secondry btn-block"
              disabled={acceptRejectEnabled}
              onClick={setRejectPopup}
            >
              Cancel
            </button>
          </div>
          <div className="col-md-6">
            <button
              className="btn btn-primary btn-block"
              disabled={acceptRejectEnabled}
              onClick={rejectAndCloseRejectPopUp}
            >
              Add to Draft
            </button>
          </div>
        </div>
      </footer>
    );
  }

  return status === DocumentStatus.PENDING_REVIEW ? (
    <footer className="document-statement--footer">
      <div className="row">
        <div className="col-md-6" >
          <button
            data-testid="reject-doc-btn"
            className="btn btn-secondry btn-block"
            disabled={acceptRejectEnabled}
            onClick={setRejectPopup}
          >
            Reject Document
          </button>
        </div>
        <div className="col-md-6">
          <button
            data-testid="accept-doc-btn"
            className="btn btn-primary btn-block"
            disabled={acceptRejectEnabled}
            onClick={acceptDocument}
          >
            Accept Document
          </button>
        </div>
      </div>
    </footer>
  ) : null;
};

export const ReviewDocumentStatement = ({
  moveNextFile,
  currentDocument,
  currentFileIndex,
  acceptDocument,
  rejectDocument,
  documentViewLoading,
  fileViewd
}: {
  moveNextFile: Function;
  currentDocument: NeedList;
  currentFileIndex: number;
  acceptDocument: () => void;
  rejectDocument: (rejectMessage: string) => void;
  documentViewLoading: boolean;
  fileViewd: boolean;
}) => {
  const [documentFiles, setDocumentFiles] = useState<FileType[]>([]);
  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState('');
  const [mcuNamesUpdated, setMcuNamesUpdated] = useState<
    { fileId: string; mcuName: string }[]
  >([]);
  const [rejectDocumentModal, setRejectDocumentModal] = useState(false);
  const [rejectDocumentMessage, setRejectDocumentMessage] = useState('');
  const [currentDocId, setCurrentDocId] = useState<string>('');
  const [currentFileName, setCurrentFileName] = useState('');
  const [acceptDocumentAlert, setAcceptDocumentAlert] = useState<boolean>(false);

  const acceptDocForStarted = () => {
    setAcceptDocumentAlert(!acceptDocumentAlert);
  }

  const getFileNameWithoutExtension = (fileName: string) =>
    fileName.substring(0, fileName.lastIndexOf('.'));

  const documentStateBodyRef = useRef<HTMLSelectElement>(null);

  const getDocumentFiles = useCallback(
    async (currentDocument: NeedList) => {
      try {
        setLoading(true);
        await requestDocumentFiles(currentDocument);
        setLoading(false);
      } catch (error) {
        console.log(error);

        setLoading(false);

        alert(
          'Something went wrong while getting files for document. Please try again.'
        );
      }
    },
    [setDocumentFiles]
  );

  const requestDocumentFiles = async (currentDocument: NeedList) => {
    const { id, requestId, docId } = currentDocument;

    const data = await ReviewDocumentActions.requestDocumentFiles(id, requestId, docId);
    const { files, userName } = data[0];
    setDocumentFiles(files);
    setUsername(userName);
    setMcuNamesUpdated(
      files.map((file: any) => {
        return {
          fileId: file.fileId,
          mcuName:
            file.mcuName === ''
              ? getFileNameWithoutExtension(file.clientName)
              : getFileNameWithoutExtension(file.mcuName)
        };
      })
    );
  };

  const getMcuNameUpdated = (fileId: string): string => {
    const item = mcuNamesUpdated.find((item) => item.fileId === fileId);
    return !!item ? item.mcuName : '';
  };

  const allowFileRenameMCU = (
    filename: string,
    fileId: string,
    addToList: boolean = true
  ): boolean => {
    const clonedArray = [...mcuNamesUpdated];

    // Why filter? because we don't want to check filename of current file being renamed
    const mcuNameAlreadyInList = clonedArray
      .filter((file) => file.fileId !== fileId)
      .some((file) => {
        return file.mcuName.trim() === filename.trim();
      });

    // This condition will make sure we are not saving each value in string
    // addToList === false, means we don't want to save it in List setMcuNamesUpdated
    if (addToList === false) {
      return mcuNameAlreadyInList;
    } else if (mcuNameAlreadyInList) {
      return false;
    }

    const documentFile = clonedArray.find((file) => file.fileId === fileId);

    if (documentFile) {
      documentFile.mcuName = filename;
    }

    setMcuNamesUpdated(() => clonedArray);
    return true;
  };

  const checkDialog = () => {
    return {
      overflow: rejectDocumentModal ? 'hidden' : ''
    };
  };

  const onChangeTextArea = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
    setRejectDocumentMessage(event.target.value);
  };

  const validateAndRejectDocument = () => {
    if (rejectDocumentMessage.trim() === '') {
      return false;
    }

    rejectDocument(rejectDocumentMessage);

    if (documentViewLoading === false) {
      setRejectDocumentModal((prevState) => !prevState);
    }
  };

  const setRejectPopup = () => {
    setRejectDocumentModal((prevState) => !prevState);
  };

  useEffect(() => {
    if (currentDocument && currentDocId !== currentDocument.docId) {
      setCurrentDocId(currentDocument.docId);
      getDocumentFiles(currentDocument);
    }
  }, [currentDocument]);

  useEffect(() => {
    if (currentDocument) {
      requestDocumentFiles(currentDocument);
    }
  }, [fileViewd]);

  useEffect(() => {
    // Set reject document message when document changed.
    setRejectDocumentMessage(
      `Hi ${currentDocument!.userName}, please submit the ${currentDocument!.docName
      } again.`
    );

    setRejectDocumentModal(false); // Force close reject modal on next documentload
  }, [currentDocument!.docName]);

  const renderStatus = (status: string) => {
    let cssClass = '';
    switch (status) {
      case 'Pending review':
        cssClass = 'status-bullet pending';
        break;
      case 'Started':
        cssClass = 'status-bullet started';
        break;
      case 'Borrower to do':
        cssClass = 'status-bullet borrower';
        break;
      case 'Completed':
        cssClass = 'status-bullet completed';
        break;
      case 'In draft':
        cssClass = 'status-bullet indraft';
        break;
      default:
        cssClass = 'status-bullet pending';
    }
    return (
      <span className={`file-status`}><span data-testid="needListStatus" className={cssClass}></span> {currentDocument.status}</span>
    );
  };

  return (
    <div
      id="ReviewDocumentStatement"
      data-component="ReviewDocumentStatement"
      className="document-statement"
    >
      <header className="document-statement--header">
        <div>
          <h2 title={currentDocument.docName}>{currentDocument.docName}</h2>
          {renderStatus(currentDocument.status)}
        </div>

        <Dropdown>
          <Dropdown.Toggle
            size="lg"
            variant="primary"
            className="mcu-dropdown-toggle no-caret"
            id="dropdown-basic"
            data-testid="activity-logBtn"
          >
            Activity Log
          </Dropdown.Toggle>
          {currentDocument.id !== null && (
            <Dropdown.Menu>
              <ReviewDocumentActivityLog
                id={currentDocument.id}
                requestId={currentDocument.requestId!}
                docId={currentDocument.docId}
              />
            </Dropdown.Menu>
          )}
        </Dropdown>
      </header>
      {!!loading ? (
        <div
          className="loader-widget"
          style={{
            height: '100vh',
            justifyContent: 'center',
            alignItems: 'flex-start',
            display: 'flex'
          }}
        >
          <Spinner animation="border" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        </div>
      ) : (
          <div className="document-statement--body-footer">
            <section
              ref={documentStateBodyRef}
              className="document-statement--body"
              style={checkDialog()}
            >
              {/* <h3>Documents</h3> */}
              {!!documentFiles && documentFiles.length ? (
                documentFiles.map((file, index) => (
                  <DocumentSnipet
                    key={index}
                    id={currentDocument?.id!}
                    index={index}
                    moveNextFile={async (
                      index: number,
                      fileId: string,
                      clientName: string,
                      mcuName: string,
                      loadingFile?: boolean
                    ) => {
                      moveNextFile(index, fileId, clientName, mcuName, loadingFile).then(
                        async () => {
                          setCurrentFileName(clientName);
                          if (currentDocument) {
                            await requestDocumentFiles(currentDocument);
                          }
                        }
                      );
                    }}
                    requestId={currentDocument.requestId}
                    docId={currentDocument.docId}
                    fileId={file.fileId}
                    mcuName={file.mcuName}
                    clientName={file.clientName}
                    isCurrent={currentFileName === file.clientName}
                    currentFileIndex={currentFileIndex}
                    uploadedOn={file.fileUploadedOn}
                    isRead={file.isRead}
                    username={username}
                    allowFileRenameMCU={allowFileRenameMCU}
                    getMcuNameUpdated={getMcuNameUpdated}
                  />
                ))
              ) : (
                  <span>No file submitted yet</span>
                )}

              {rejectDocumentModal && (
                <div className="dialogbox">
                  <div className="dialogbox-backdrop"></div>
                  <div className="dialogbox-slideup" data-testid="req-doc-dialog">
                    <h2 className="h2">Request this document again.</h2>
                    <p>
                      Let the borrower know what you need to mark it as complete
                  </p>
                    <textarea
                      style={{
                        borderColor:
                          rejectDocumentMessage.trim() === '' ? 'red' : ''
                      }}
                      className="form-control"
                      rows={6}
                      value={rejectDocumentMessage}
                      onChange={onChangeTextArea}
                      maxLength={255}
                    />
                    {rejectDocumentMessage.trim() === '' && (
                      <div style={{ color: 'red' }}>This field is required.</div>
                    )}
                  </div>
                </div>
              )}
              
              {acceptDocumentAlert && currentDocument?.status == DocumentStatus.STARTED &&
                <div className="dialogbox">
                  <div className="dialogbox-backdrop"></div>
                  <div className="dialogbox-slideup" data-testid="req-doc-dialog">
                    <div className="dialogbox-error">
                      <div className="dialogbox-error-icon"><i className="zmdi zmdi-alert-triangle"></i></div>
                      <p>Are you sure you want to accept these documents? This item will disappear from the borrower’s Needs List.</p>
                    </div>
                  </div>
                </div>
              }

              {acceptDocumentAlert && currentDocument?.status == DocumentStatus.BORROWER_TO_DO &&
                <div className="dialogbox">
                  <div className="dialogbox-backdrop"></div>
                  <div className="dialogbox-slideup" data-testid="req-doc-dialog">
                    <div className="dialogbox-error">
                      <div className="dialogbox-error-icon"><i className="zmdi zmdi-alert-triangle"></i></div>
                      <p>Are you sure you want to accept these documents? This item will disappear from the borrower’s Needs List.</p>
                    </div>
                  </div>
                </div>
              }



            </section>
            <Footer
              acceptDocumentOnly={acceptDocForStarted}
              status={currentDocument?.status}
              acceptDocument={acceptDocument}
              rejectDocument={validateAndRejectDocument}
              setRejectPopup={setRejectPopup}
              rejectModalOpen={rejectDocumentModal}
              acceptRejectEnabled={documentViewLoading} // Prevent click on document loading and on accept/reject API Call
            />
          </div>
        )}
    </div>
  );
};
