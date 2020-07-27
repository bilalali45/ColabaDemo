import React, { ChangeEvent, useState, useRef, useEffect } from "react";

import {
  DocEditIcon,
  DocviewIcon,
} from "../../../../../../shared/Components/Assets/SVG";
import { UserActions } from "../../../../../../store/actions/UserActions";
import { Document } from "../../../../../../entities/Models/Document";
import { FileUpload } from "../../../../../../utils/helpers/FileUpload";
import erroricon from "../../../../../../assets/images/warning-icon.svg";
import refreshIcon from "../../../../../../assets/images/refresh.svg";
import { DateFormatWithMoment } from "../../../../../../utils/helpers/DateFormat";

type DocumentItemType = {
  disableSubmitButton: Function;
  file: Document;
  viewDocument: Function;
  changeName: Function;
  deleteDoc: Function;
  indexKey: number;
  retry: Function;
  fileAlreadyExists: Function;
  handleDelete: Function;
  shouldFocus: boolean;
  toggleFocus: Function;
};

export const DocumentItem = ({
  file,
  viewDocument,
  changeName,
  deleteDoc,
  fileAlreadyExists,
  retry,
  indexKey,
  disableSubmitButton,
  handleDelete,
  shouldFocus,
  toggleFocus
}: DocumentItemType) => {
  const [filename, setfilename] = useState<string>("");
  const [iseditable, seteditable] = useState<any>(true);
  const [nameExists, setNameExists] = useState<any>(false);
  const [isdeleted, setdeleted] = useState<any>(false);
  const [showInput, setShowInput] = useState<boolean>(false);
  const [validFilename, setValidFilename] = useState(true)

  const txtInput: any = useRef(null);

  useEffect(() => {
    setfilename(
      FileUpload.removeSpecialChars(
        FileUpload.removeDefaultExt(file.clientName)
      )
    );
  }, [file]);

  useEffect(() => {

  }, [file.editName === true && showInput]);



  useEffect(() => {
    if (!file.editName) {
      setShowInput(false);
    }
  }, [file.editName])

  const rename = (e) => {
    toggleFocus(file, false, true)
    seteditable(false);
    if (filename === "") {
      setNameExists(true);
      return;
    }
    let nameExists = changeName(file, filename);
    if (nameExists === false) {
      setNameExists(true);
    }
  };

  const EditTitle = () => {
    changeName(file, filename);
    toggleFocus(file, true)
    setShowInput(true);
    if (showInput) {
      if (txtInput.current) {
        txtInput.current.focus();
      }
    }
  };

  const deleteDOChandeler = () => {
    file.uploadReqCancelToken.cancel();
    deleteDoc(file.clientName);
    setNameExists(false);
  };
  const cancelDeleteDOC = () => {
    // disableSubmitButton(false)
    handleDelete(file);
  };

  const renderDeleteBox = () => {
    return (
      <>
        <div className="document-confirm-wrap">
          <div className="row">
            <div className="col-sm-7">
              <div className="dc-text">
                <p>Are you sure to delete this file?</p>
              </div>
            </div>

            <div className="col-sm-5">
              <div className="dc-actions">
                <button
                  className="btn btn-small btn-secondary"
                  onClick={() => cancelDeleteDOC()}
                >
                  No
                </button>
                <button
                  className="btn btn-small btn-primary"
                  onClick={() => {
                    file.uploadReqCancelToken.cancel();
                    deleteDoc(file.clientName);
                  }}
                >
                  Yes
                </button>
              </div>
            </div>
          </div>
        </div>
      </>
    );
  };

  const renderDocListActions = () => {
    return (
      <div className="doc-list-actions">
        {file.editName ? (
          <ul className="editable-actions">
            <li>
              <button
                onClick={(e) => {
                  setShowInput(false);
                  rename(e);
                }}
                className="btn btn-primary doc-rename-btn"
              >
                Save
              </button>
            </li>
          </ul>
        ) : (
            <>
              <ul className="readable-actions">
                {file.file && !file.uploadProgress && (
                  <li>
                    <a onClick={EditTitle} title="Rename" tabIndex={-1}>
                      {<DocEditIcon />}
                    </a>
                  </li>
                )}

                <li>
                  <a
                    onClick={() => viewDocument(file)}
                    title="View Document"
                    tabIndex={-1}
                  >
                    {<DocviewIcon />}
                  </a>
                </li>

                {file.file && file.uploadProgress < 100 && (
                  <li>
                    <a
                      title="Cancel"
                      onClick={() => deleteDOChandeler()}
                      tabIndex={-1}
                    >
                      <i className="zmdi zmdi-close"></i>
                    </a>
                  </li>
                )}
                {file.uploadStatus === "done" && (
                  <li>
                    <a title="Uploaded" className="icon-uploaded" tabIndex={-1}>
                      <i className="zmdi zmdi-check"></i>
                    </a>
                  </li>
                )}
              </ul>
            </>
          )}
      </div>
    );
  };
  const doubleClickHandler = (isUploaded: string | undefined) => {
    if (isUploaded === 'done') {
      return;

    }
    toggleFocus(file, true)
    changeName(file, filename);
  }
  const renderFileTitle = () => {
    console.log('filename', filename, filename.split(".")[0])

    return (
      <div className="title">
        {file.editName ? (
          <input
            autoFocus={file.focused}
            style={{ border: nameExists ? "1px solid #D7373F" : "none" }}
            ref={txtInput}
            maxLength={255}
            type="text"
            value={filename.split(".")[0]}
            onChange={(e) => {
              !validFilename && setValidFilename(true)
              setNameExists(false);
              if (fileAlreadyExists(file, e.target.value)) {
                setNameExists(true);
              }
              if (FileUpload.nameTest.test(e.target.value)) {
                setfilename(e.target.value);
                return;
              } else {
                !!validFilename && setValidFilename(false)
              }

              setNameExists(true);
            }}
            onKeyDown={(e) => {
              if (e.keyCode === 13) {
                rename(e);
              }
            }}
            onBlur={(e) => rename(e)}
          />
        ) : (
            <p> {file.clientName}</p>
          )}
      </div>
    );
  };

  const renderFileContent = () => {
    return (
      <div className="dl-info">
        {nameExists ? (
          <span className="dl-errorrename">File name must be unique.</span>
        ) : (
            <>
              <span className="dl-date">
                {DateFormatWithMoment(file.fileUploadedOn)}
              </span>
              <span className="dl-text-by"> by </span>
              <span className="dl-text-auther">{UserActions.getUserName()}</span>
              <span className="dl-pipe"> | </span>
              <span className="dl-filesize">{FileUpload.getFileSize(file)}</span>
            </>
          )}
      </div>
    );
  };

  const renderAllowedFile = () => {
    return (
      <li className="doc-li">
        {!file.deleteBoxVisible && (
          <div
            className={
              file.editName
                ? "editableview doc-liWrap"
                : "noneditable doc-liWrap"
            }
          >
            <div className="doc-icon">
              <i className={file.docLogo}></i>
            </div>
            <div onDoubleClick={(e) => doubleClickHandler(file.uploadStatus)} className="doc-list-content">
              {renderFileTitle()}
              {!validFilename && (<span className='text-danger'>File name cannot contain any special characters</span>)}
              {/* {renderFileContent()} */}
            </div>
            {renderDocListActions()}
          </div>
        )}
        {file.file && file.uploadProgress < 100 && file.uploadProgress > 0 && (
          <div
            className="progress-upload"
            style={{ width: file.uploadProgress + "%" }}
          ></div>
        )}
      </li>
    );
  };

  const renderSizeNotAllowed = () => {
    return (
      <li className="doc-li item-error">
        <div className="noneditable doc-liWrap">
          <div className="doc-icon">
            <img src={erroricon} alt="" />
          </div>
          <div className="doc-list-content">
            <div className="title">
              <p>{file.clientName}</p>
            </div>
            <div className="dl-info">
              <span className="dl-text">
                {" "}
                File size over {FileUpload.allowedSize}mb limit{" "}
              </span>
            </div>
          </div>
          <div className="doc-list-actions">
            <ul className="editable-actions">
              <li
                onClick={() => {
                  retry(file);
                }}
              >
                <a title="Retry" className="icon-retry" tabIndex={-1}>
                  <span className="retry-txt">Retry</span>{" "}
                  <img src={refreshIcon} alt="" />
                </a>
              </li>
              <li>
                <a
                  onClick={() => {
                    deleteDoc(file.clientName);
                  }}
                  tabIndex={-1}
                  title="Remove"
                >
                  <i className="zmdi zmdi-close"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </li>
    );
  };

  const renderTypeIsNotAllowed = () => {
    return (
      <li className="doc-li item-error">
        <div className="noneditable doc-liWrap">
          <div className="doc-icon">
            <img src={erroricon} alt="" />
          </div>
          <div className="doc-list-content">
            <div className="title">
              <p>{file.clientName}</p>
            </div>
            <div className="dl-info">
              <span className="dl-text">
                {" "}
                File type is not supported. Allowed types: PDF, JPEG, PNG
              </span>
            </div>
          </div>
          <div className="doc-list-actions">
            <ul className="editable-actions">
              <li
                onClick={() => {
                  retry(file);
                }}
              >
                <a title="Retry" className="icon-retry" tabIndex={-1}>
                  <span className="retry-txt">Retry</span>{" "}
                  <img src={refreshIcon} alt="" />
                </a>
              </li>
              <li>
                <a
                  onClick={() => deleteDoc(file.clientName)}
                  tabIndex={-1}
                  title="Remove"
                >
                  <i className="zmdi zmdi-close"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </li>
    );
  };

  const renderNotAllowedFile = () => {
    if (file.notAllowedReason === "FileSize") {
      return renderSizeNotAllowed();
    } else if (file.notAllowedReason === "FileType") {
      return renderTypeIsNotAllowed();
    }
    return null;
  };

  if (file.notAllowed) {
    return renderNotAllowedFile();
  }

  return renderAllowedFile();
};
