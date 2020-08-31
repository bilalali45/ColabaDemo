import React, { useState, useRef, useEffect } from "react";

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
  const [nameExists, setNameExists] = useState<any>(false);
  const [showInput, setShowInput] = useState<boolean>(false);
  const [focus, setFocus] = useState<boolean>(false);
  const [validFilename, setValidFilename] = useState(true)
  const [filename, setFilename] = useState<string>("")

  const txtInput = useRef<HTMLInputElement>(null);

  const doubleClickHandler = (isUploaded: string | undefined) => {
    if (isUploaded === 'done' || validFilename === false || nameExists === true || filename === "") {
      return;
    }

    toggleFocus(file, true)

    changeName(file, filename);
  }

  const modifyFilename = (filename: string) => {
    // Hide Filename validation on each Input only if its not valid
    !validFilename && setValidFilename(() => true)

    setNameExists(() => false)

    // Check speical characters only - and spaces allowed
    if (FileUpload.nameTest.test(filename) === false) {
      setValidFilename(false)
    }

    // Check if filename is unique
    if (fileAlreadyExists(file, filename)) {
      setNameExists(true);
    }

    setFilename(filename)
  }

  const onChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    modifyFilename(event.target.value)
  }

  const onKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === 'Enter') {
      if (nameExists === true || validFilename === false || filename === "") {
        return event.preventDefault()
      }

      toggleFocus(file, true);

      changeName(file, filename);
    }
  }

  const onBlur = (event: React.FocusEvent<HTMLInputElement>) => {
    if (nameExists === true || validFilename === false || filename === "") {
      return event.preventDefault()
    }

    toggleFocus(file, true);

    changeName(file, filename);
  }

  useEffect(() => {
    setFilename(
      FileUpload.removeSpecialChars(
        FileUpload.removeDefaultExt(file.clientName)
      )
    );
  }, [file]);

  useEffect(() => {
    if (!file.editName) {
      setShowInput(false);
    }
  }, [file.editName]);

  useEffect(() => {
    if (file.focused) {

      if (txtInput?.current) {
        txtInput.current.focus();
      }
    }
  }, [file.focused, file.editName]);

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

  const renderDocListActions = () => {
    return (
      <div className="doc-list-actions">
        {file.editName ? (
          <ul className="editable-actions">
            <li>
              <button
                onClick={(e) => {
                  if (nameExists === true || validFilename === false || filename === "") {
                    return e.preventDefault()
                  }

                  toggleFocus(file, true);

                  changeName(file, filename);
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

  const renderFileTitle = () => {
    return (
      <div className="title">
        {file.editName ? (
          <input
            ref={txtInput}
            style={{ border: nameExists === true || validFilename === false || filename === "" ? "1px solid #D7373F" : "none" }}
            maxLength={250}
            type="text"
            value={filename} //filename is default value on edit without extension
            onChange={onChange}
            onKeyDown={onKeyDown}
            onBlur={onBlur}
          />
        ) : (
            <p> {file.clientName}</p>
          )}
      </div>
    )
  }

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
              {!validFilename && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name cannot contain any special characters</span>
                </div>
              )}
              {!!nameExists && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name must be unique.</span>
                </div>
              )}
              {filename === "" && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name cannot be empty.</span>
                </div>
              )}
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
