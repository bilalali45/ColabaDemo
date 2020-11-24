import React, { useState, useRef, useEffect, useContext, Fragment } from "react";

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
import { Store } from '../../../../../../store/store';
import Dropdown from 'react-bootstrap/Dropdown'
import Modal from 'react-bootstrap/Modal'
type DocumentItemType = {
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
  handleDelete,
  shouldFocus,
  toggleFocus
}: DocumentItemType) => {
  const [nameExists, setNameExists] = useState<any>(false);
  const [showInput, setShowInput] = useState<boolean>(false);
  const [focus, setFocus] = useState<boolean>(false);
  const [validFilename, setValidFilename] = useState(true)
  const [filename, setFilename] = useState<string>("")

  const { state, dispatch } = useContext(Store);
  const loan: any = state.loan;
  const { isMobile } = loan;
  const [renameModalShow, setRenameModalShow] = useState(true);
  const [openItemDropdown, setOpenItemDropdown] = useState(false);
  const txtInput = useRef<HTMLInputElement>(null);

  const doubleClickHandler = (isUploaded: string | undefined) => {
    if (isMobile?.value) {
      return;
    }
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
    modifyFilename(event.target.value);
    if (!event.target.value.trim()) {
      setFilename('')
    }
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
    if (isMobile?.value) {
      return;
    }
    if (nameExists === true || validFilename === false || filename === "") {

      return event.preventDefault()
    }

    toggleFocus(file, true);

    changeName(file, filename);
  }

  useEffect(() => {
    let name = FileUpload.removeSpecialChars(
      FileUpload.removeDefaultExt(file.clientName)
    );
    setFilename(name);
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
    setRenameModalShow(true);
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
    file?.uploadReqCancelToken?.cancel();
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
                data-testid="name-save-btn"
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
                    <a data-testid={`file-edit-btn-${indexKey}`} onClick={EditTitle} title="Rename" tabIndex={-1}>
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
                      data-testid={`file-remove-btn-${indexKey}`}
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
                    <a data-testid="done-upload" title="Uploaded" className="icon-uploaded" tabIndex={-1}>
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

  const renderDocListActionsMobile = () => {
    return (
      <div className={`doc-list-actions doc-list-actions-mobile`}>

        {file.uploadStatus === "done" ?
          <div className="m-d-l-submitted-icon">
            <a data-testid="done-upload" title="Uploaded" className="icon-uploaded" tabIndex={-1}>
              <i className="zmdi zmdi-check"></i>
            </a>
          </div>
          :
          <Dropdown onToggle={(e) => { setOpenItemDropdown(e) }}>
            <Dropdown.Toggle id="doc-list-m-actions" as="div" data-testid={`file-options-${indexKey}`}>
              <i className="zmdi zmdi-more-vert"></i>
            </Dropdown.Toggle>

            <Dropdown.Menu>
              {file.file && !file.uploadProgress && (
                <div className="m-d-l-item" data-testid={`file-edit-btn-${indexKey}`} onClick={EditTitle} title="Rename" tabIndex={-1}>
                  <div className="d-l-m-icon">{<DocEditIcon />}</div>
                  <span>Rename</span>
                </div>

              )}
              <div className="m-d-l-item" onClick={() => viewDocument(file)}
                title="View Document"
                tabIndex={-1}
              >
                <div className="d-l-m-icon">
                  {<DocviewIcon />}
                </div>
                <span>View File</span>
              </div>
              {file.file && file.uploadProgress < 100 && (
                <div className="m-d-l-item"
                  data-testid={`file-remove-btn-${indexKey}`}
                  title="Cancel"
                  onClick={() => deleteDOChandeler()}
                  tabIndex={-1}
                >
                  <div className="d-l-m-icon">
                    <i className="zmdi zmdi-close"></i>
                  </div>
                  <span>Delete</span>
                </div>
              )}

            </Dropdown.Menu>
          </Dropdown>
        }




        {/* <ul className="readable-actions">
         


          
                {file.uploadStatus === "done" && (
                  <li>
                    <a data-testid="done-upload" title="Uploaded" className="icon-uploaded" tabIndex={-1}>
                      <i className="zmdi zmdi-check"></i>
                    </a>
                  </li>
                )}
              </ul> */}
      </div>
    );
  };

  const renderFileTitle = () => {
    return (
      <div className="title">
        {file.editName ? (
          <input
            data-testid="file-item-rename-input"
            ref={txtInput}
            style={{ border: nameExists === true || validFilename === false || filename === "" ? "1px solid #D7373F" :file.focused?"1px solid #206cf2":"none" }}
            maxLength={250}
            type="text"
            value={filename} //filename is default value on edit without extension
            onChange={onChange}
            onKeyDown={onKeyDown}
            onBlur={onBlur}
          />
        ) : (
            <p onClick={() => {
              if(isMobile.value && file.uploadStatus === 'done') {
                viewDocument(file)
              }
            }} title={file.clientName}> {file.clientName}</p>
          )}
      </div>
    )
  }


  const renderFileTitleMobile = () => {
    return (
      <div className="rename-doc-input">
        <input
          data-testid="rename-input-adaptive"
          ref={txtInput}
          maxLength={250}
          type="text"
          className={nameExists === true || validFilename === false || filename === "" ? "haveError" : ""}
          value={filename} //filename is default value on edit without extension
          onChange={onChange}
          onKeyDown={onKeyDown}
          onBlur={onBlur}
          autoFocus={true}
        />
      </div>
    )
  }

  const renderAllowedFile = () => {
    return (
      <li className={`doc-li ${file.editName && file.focused ? " editmode" : ""} ${openItemDropdown ? " dopen" : ""}`} data-testid="file-item">
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
            <div data-testid={`file-container-${indexKey}`} onDoubleClick={(e) => doubleClickHandler(file.uploadStatus)} className="doc-list-content">
              {renderFileTitle()}
              {/* {!validFilename && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name cannot contain any special characters</span>
                </div>
              )}
              {!!nameExists && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name must be unique.</span>
                </div>
              )}
              {file.uploadStatus !== 'done' && filename.trim() === "" && (
                <div className="dl-info">
                  <span className="dl-errorrename">File name cannot be empty.</span>
                </div>
              )} */}
              {renderErrors()}
            </div>

            {!isMobile?.value ?
              renderDocListActions() : renderDocListActionsMobile()
            }


          </div>
        )}
        {file.file && file.uploadProgress < 100 && file.uploadProgress > 0 && (
          <div
            data-testid="upload-progress-bar"
            className="progress-upload"
            style={{ width: file.uploadProgress + "%" }}
          ></div>
        )}
      </li>
    );
  };

  const renderSizeNotAllowed = () => {
    return (
      <li data-testid="size-not-allowed-item" className="doc-li item-error">
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
                File size must be under {FileUpload.allowedSize} mb
                {/* File size over {FileUpload.allowedSize}mb limit{" "} */}
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
      <li className="doc-li item-error" data-testid="type-not-allowed-item">
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
                File type is not supported. Allowed types: PDF,JPEG,PNG
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
                  data-testid={`file-remove-btn-${indexKey}`}
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
  
  const renderFileUploadFailed = () => {
    return (
      <li className="doc-li item-error" data-testid="type-not-allowed-item">
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
                {file?.failedReason}
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
                  data-testid={`file-remove-btn-${indexKey}`}
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
    } else if (file.notAllowedReason === "Failed") {
      return renderFileUploadFailed();
    }
    return null;
  };

  const renderErrors = () => {

    if (!validFilename) {
      return (
        <div className="dl-info">
          <span className="dl-errorrename" data-testid="special-character-error">File name cannot contain any special characters</span>
        </div>
      )
    } else if (nameExists) {
      return (
        <div className="dl-info">
          <span className="dl-errorrename"data-testid="unique-file-name-error">File name must be unique.</span>
        </div>
      )
    } else if (file.uploadStatus === 'pending' && filename.trim() === '') {
      return (
        <div className="dl-info">
          <span className="dl-errorrename" data-testid="empty-file-name-error">File name cannot be empty.</span>
        </div>
      )
    }
  }

  const renderDocListPopupMobile = () => {
    return (
      <Modal
        show={renameModalShow}
        className="rename-popup"
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        data-testid="rename-popup"

      >
        <Modal.Header>
          <Modal.Title>
            Rename Document?
        </Modal.Title>
          <button type="button" className="close" onClick={() => {
            if (!validFilename || nameExists || filename.trim() === '') {
              return;
            }
            if (filename?.length) {
              toggleFocus(file, true);
              changeName(file, filename);
              return setRenameModalShow(false);
            }
          }}>
            <span aria-hidden="true">×</span><span className="sr-only" >Close</span></button>
        </Modal.Header>
        <Modal.Body>


          <div data-testid="rename-popup-doc-container" className="m-rename-popup-docWrap">
            <div className="m-popup-doc-li" >
              {!file.deleteBoxVisible && (
                <Fragment>
                  <div className="mp-d-l-wrap">
                    <div className="doc-icon">
                      <i className={file.docLogo}></i>
                    </div>

                    <div className="m-d-l-info">
                      <h4>Original Document Name</h4>
                      <p>{file.clientName}</p>
                    </div>

                  </div>

                  <div className="doc-list-content">
                    {renderFileTitleMobile()}
                    {renderErrors()}
                  </div>
                </Fragment>
              )}

            </div>
          </div>


        </Modal.Body>
        <Modal.Footer>
          <button data-testid="name-save-btn-adaptive" className="btn btn-primary" onClick={() => {
            if (nameExists === true || validFilename === false || filename.trim() === "") {
              return;
            }
            toggleFocus(file, true);
            changeName(file, filename);
            setRenameModalShow(false)
          }}>Save</button>
        </Modal.Footer>
      </Modal>
    )
  }

  if (file.notAllowed || file.uploadStatus === 'failed') {
    return renderNotAllowedFile();
  }

  return (
    <Fragment>
      {isMobile?.value && file?.focused && file?.editName ? renderDocListPopupMobile() : renderAllowedFile()}
    </Fragment>
  )
    ;
};
