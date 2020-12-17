import React, {
  RefObject,
  useRef,
  useState,
  useContext,
  useEffect,
} from "react";
import { Http } from "rainsoft-js";
import { FileItem as FileItemModel } from "../../../../../../Models/FileItem";
import {
  FileIcon,
  ReassignListIcon,
  SyncIcon,
  ReadyToSync,
  SynchedIcon,
  SyncFailed,
  TrashCan,
} from "../../../../../../shared/Components/Assets/SVG";
import { DocumentActionsType } from "../../../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../../../Store/Store";
import { ReassignDropdown } from "../ReassignDropdown/ReassignDropdown";
import DocumentActions from "../../../../../../Store/actions/DocumentActions";
import { CurrentInView } from "../../../../../../Models/CurrentInView";
import moment from "moment";
import erroricon from "../../../../../../Assets/images/warning-icon.svg";
import refreshIcon from "../../../../../../Assets/images/refresh.svg";
import { RenameFile } from "../../../../../../shared/Components/Assets/RenameFile";
import { getDateString, getFileDate } from "../../../../../../Utilities/helpers/DateFormat";
import { LocalDB } from "../../../../../../Utilities/LocalDB";
import { ConsoleLogger } from "../../../../../../Assets/js/rs-authorization";
import { AnnotationActions } from "../../../../../../Utilities/AnnotationActions";
import { readSync } from "fs";
import { fireEvent } from "@testing-library/react";
import { Viewer } from "../../../../../../Utilities/Viewer";
import { ViewerActions } from "../../../../../../Store/actions/ViewerActions";

export const FileItem = ({
  file,
  setDraggingSelf,
  setDraggingItem,
  document,
  refReassignDropdown,
  docInd,
  fileInd,
  retry,
  deleteFile,
  getDocswithfailedFiles
}: any) => {
  const [
    showingReassignDropdown,
    setShowingReassignDropdown,
  ] = useState<boolean>(false);
  const [
    reassignDropdownTarget,
    setReassignDropdownTarget,
  ] = useState<HTMLDivElement>();
  //const refReassignDropdown = useRef<HTMLDivElement>(null);
  const [editingModeEnabled, setEditingModeEnabled] = useState(false);
  const [isDraggingItem, setIsDraggingItem] = useState(false);

  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const { currentFile, selectedFileData, isLoading }: any = state.viewer;
  const documents: any = state.documents;
  const isDragging: any = documents;
  const filesToSync: any = documents?.filesToSync || [];
  const isSynching: any = documents?.isSynching;
  const syncStarted: any = documents?.syncStarted;
  const instance: any = viewer?.instance;
  const loanApplicationId = LocalDB.getLoanAppliationId();
  const toggleReassignDropdown = async (e: any) => {
    let target = e.target
    await setCurrentDocument();

    setReassignDropdownTarget(target);
    setShowingReassignDropdown(true);
  };

  const hideReassign = () => setShowingReassignDropdown(false);
  useEffect(() => {
    dispatch({ type: DocumentActionsType.SetIsDragging, payload: isDraggingItem });
  }, [isDraggingItem]);

  const moveDocToTrash = async () => {
    let cancelCurrentFileViewRequest: boolean = false;
    if (selectedFileData && selectedFileData?.fileId === file.id) {
      cancelCurrentFileViewRequest = true;
    }
    setCurrentDocument();
    let success = await DocumentActions.moveCatFileToTrash(
      document.id,
      document.requestId,
      document.docId,
      file.id,
      cancelCurrentFileViewRequest
    );

    if (success) {
      await getDocswithfailedFiles()

      if (selectedFileData && selectedFileData?.fileId === file.id) {
        await DocumentActions.getCurrentDocumentItems(dispatch, false);
      }
      let res = await DocumentActions.getTrashedDocuments(dispatch);
    }
  };

  const toggleSyncAlert = () => {
    if(syncStarted) {
      return;
    }
    if (file.byteProStatus !== 'Synchronized') {
      console.log('isSynching', isSynching, docInd, fileInd);
      let fileToSync: any = { document, file, syncStatus: 'started' }

      let selectedFiles;

      if (doesFileExist()) {
        selectedFiles = filesToSync.filter((f: any) => f.document === document && f.file !== file);
      } else {
        selectedFiles = [fileToSync, ...filesToSync];
      }
      selectedFiles = selectedFiles.map((sf: any) => {
        return sf;
      });
      dispatch({ type: DocumentActionsType.SetFilesToSync, payload: selectedFiles })
    }

  }

  const doesFileExist = () => filesToSync?.map((f: any) => f.file.id)?.includes(file.id)

  const setCurrentDocument = () => {
    if (document) {
      dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: null });
    }

    dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: document });
  };

  const viewFileForDocCategory = async () => {
    dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false });
    ViewerActions.resetInstance(dispatch);
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });

    if (file.file && file.uploadProgress <= 100) return;

    await setCurrentDocument();
    await DocumentActions.viewFile(document, file, dispatch);

  };

  const onDoubleClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>
  ) => {
    if (file.file && file.uploadProgress <= 100) return;
    if (file) {
      let currentFile: any = new CurrentInView(
        document.id,
        file.src,
        DocumentActions.getFileName(file),
        false,
        file.id
      );
      dispatch({
        type: ViewerActionsType.SetCurrentFile,
        payload: currentFile,
      });
      setCurrentDocument();
      editMode(true);
    }
  };


  const editMode = (isEditEnabled: boolean) => {
    setEditingModeEnabled(isEditEnabled);
  };

  const DragStartHandler = async (e: any) => {
    DocumentActions.showFileBeingDragged(e, file);
    let FileData = {
      id: document.id,
      fromRequestId: document.requestId,
      fromDocId: document.docId,
      fromFileId: file.id,
      fileName: file.mcuName ? file.mcuName : file.clientName,
      isFromThumbnail: false,
      isFromWorkbench: false,
      isFromCategory: true
    }
    setIsDraggingItem(true);
    setDraggingSelf(true);
    setDraggingItem(true);
    e.dataTransfer.setData("file", JSON.stringify(FileData));

  }


  const renderFileActions = () => {
    if (doesFileExist()) {
      return (
        <ul>
          {renderSyncIcon()}
        </ul>
      )
    }
    return (
      <ul>
        <li className={`delBtn`}>
          <a
            data-title="Trash Bin"
            onClick={moveDocToTrash}>
            <TrashCan />
          </a>
        </li>
        <li className={`reAssBtn`}>
          <a
            data-title="Reassign"
            onClick={(e) => toggleReassignDropdown(e)}
            className={showingReassignDropdown ? "overlayOpen" : ""}>
            <ReassignListIcon />
          </a>
        </li>
        {renderSyncIcon()}
      </ul>
    );
  };


  const renderSyncIcon = () => {
    let fileNeedToBeSynched = filesToSync.find((fs: any) => fs.file.id === file.id);
    let syncStartedClass = 'active';
    let syncFailedClass = 'active failed'
    let syncCompleteClass = 'active complete';

    let syncNotCheckedClass = '';

    if (fileNeedToBeSynched) {
      switch (fileNeedToBeSynched.syncStatus) {

        case 'started':
          fileNeedToBeSynched.syncStatusClass = syncStartedClass;
          break;

        case 'failed':
          fileNeedToBeSynched.syncStatusClass = syncFailedClass;
          break;

        case 'successful':
          fileNeedToBeSynched.syncStatusClass = syncCompleteClass;
          break;

        default:
          break;
      }
    } else {
      switch (file.byteProStatus) {
        case 'Synchronized':
          syncNotCheckedClass = 'complete';
          break;

        case 'Error':
          syncNotCheckedClass = 'failed';
          break;

        default:
          break;
      }
    }


    let caclClass = fileNeedToBeSynched?.syncStatusClass || syncNotCheckedClass;

    return (
      <li className={`syncBtn ${caclClass}`}>
        <a
          className={`${syncStarted ? 'disabled' : ''}`}
          data-title={file.byteProStatus === 'Not synchronized' ? "Sync To LOS" : file.byteProStatus}
          onClick={toggleSyncAlert}>
          <SyncIcon />
          <SynchedIcon />
        </a>
      </li>
    )
  }

  const renderTypeIsNotAllowed = () => {
    return (
      <li className="item-error" data-testid="type-not-allowed-item">
        <div className="l-icon">
          <img src={erroricon} alt="" />
        </div>
        <div className="d-name">
          <div>
            <p>{file.clientName}</p>
            <div className="modify-info">
              <span className="mb-text">
                {" "}
                File type is not supported. Allowed types: PDF,JPEG,PNG
              </span>
            </div>
          </div>
        </div>
        <div className="action-btns">
          <ul>
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
                data-testid={``}
                onClick={() => deleteFile(file)}
                tabIndex={-1}
                title="Remove"
              >
                <i className="zmdi zmdi-close"></i>
              </a>
            </li>
          </ul>
        </div>
      </li>
    );
  };

  const renderFileUploadFailed = () => {
    return (
      <li className="item-error" data-testid="type-not-allowed-item">
        <div className="l-icon">
          <img src={erroricon} alt="" />
        </div>
        <div className="d-name">
          <div>
            <p>{file.clientName}</p>
            <div className="modify-info">
              <span className="mb-text">
                {" "}
                {file?.failedReason}
              </span>
            </div>
          </div>
        </div>
        <div className="action-btns">
          <ul>
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
                data-testid={``}
                onClick={() => deleteFile(file)}
                tabIndex={-1}
                title="Remove"
              >
                <i className="zmdi zmdi-close"></i>
              </a>
            </li>
          </ul>
        </div>
      </li>
    );
  };

  const renderNotAllowedFile = () => {
    if (file.notAllowedReason === "FileType") {
      return renderTypeIsNotAllowed();
    } else if (file.notAllowedReason === "Failed") {
      return renderFileUploadFailed();
    }
    return null
  };

  if (file.notAllowed || file.uploadStatus === 'failed') {
    return renderNotAllowedFile();
  }

  const getCurrentFileSelectedStyle = () => {
    if (selectedFileData && file.id === selectedFileData.fileId) {
      return "selected"
    }
    return '';
  }

  return (
    <li key={file.name} className={`${isDraggingItem ? 'dragging' : ''} ${getCurrentFileSelectedStyle()}`}
      draggable={!editingModeEnabled ? true : false}
      onDragStart={async (e: any) => {
        await DragStartHandler(e)
      }}
      onDragEnd={() => {
        let dragView: any = window.document.getElementById('fileBeingDragged');
         window.document.body.removeChild(dragView);
        setIsDraggingItem(false);
      }}
    >
      <div className="l-icon">
        <FileIcon />
      </div>
      <div
        className="d-name"
        onDoubleClick={(event) => onDoubleClick(event)}
        onClick={viewFileForDocCategory}
      >
        {!!editingModeEnabled ? (
          <RenameFile
            editingModeEnabled={editingModeEnabled}
            editMode={editMode}
            isWorkBenchFile={false}
          />
        ) : (
            <div>
              <p title={DocumentActions.getFileName(file)}>{DocumentActions.getFileName(file)}</p>
              {file.file && file.uploadProgress <= 100?null : 
              <div className="modify-info">
                <span className="mb-lbl">{file.fileModifiedOn ? "Modified By:" : "Uploaded By:"}</span>{" "}
                <span className="mb-name">
                  {file.userName ? file.userName : "Borrower"}{" "}
                  {getFileDate(file)}
                </span>
              </div>}
            </div>
          )}
      </div>
      {doesFileExist() && <div className={`syncActive`} style={{ flex: 1 }}>
      </div>}
      <div className={`dl-actions ${showingReassignDropdown ? "show" : ""}`}>

        {file.file && file.uploadProgress < 100 ? null : renderFileActions()}
        {showingReassignDropdown ? (
          <ReassignDropdown
            visible={showingReassignDropdown}
            hide={hideReassign}
            container={refReassignDropdown?.current}
            target={reassignDropdownTarget}
            selectedFile={file}
            isFromWorkbench={false}
            getDocswithfailedFiles={getDocswithfailedFiles}
          />) : null
        }
      </div>
      {file.file && file.uploadProgress < 100 && (
        <div
          data-testid="upload-progress-bar"
          className="progress-upload"
          style={{ width: file.uploadProgress + "%" }}
        ></div>
      )}
    </li>
  );
};
