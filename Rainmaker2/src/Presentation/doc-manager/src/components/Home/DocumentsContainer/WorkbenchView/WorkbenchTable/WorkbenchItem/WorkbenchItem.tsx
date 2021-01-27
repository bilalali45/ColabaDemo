import React, { useState, useRef, useContext, useEffect } from 'react'
import { FileIcon, TrashCan, SyncIcon, ReassignListIcon } from '../../../../../../shared/Components/Assets/SVG'
import Overlay from 'react-bootstrap/Overlay'
import Popover from 'react-bootstrap/Popover'
import { Store } from '../../../../../../Store/Store'
import { DocumentActionsType } from '../../../../../../Store/reducers/documentsReducer'
import { ReassignDropdown } from '../../../DocumentTableView/DocumentsTable/ReassignDropdown/ReassignDropdown'
import { CurrentInView } from '../../../../../../Models/CurrentInView'
import { RenameFile } from '../../../../../../shared/Components/Assets/RenameFile'
import DocumentActions from '../../../../../../Store/actions/DocumentActions'
import { ViewerActionsType } from '../../../../../../Store/reducers/ViewerReducer'
import { getDateString, getFileDate } from '../../../../../../Utilities/helpers/DateFormat'
import { LocalDB } from '../../../../../../Utilities/LocalDB'
import { DocumentRequest } from '../../../../../../Models/DocumentRequest'
import { AnnotationActions } from '../../../../../../Utilities/AnnotationActions'
import { SelectedFile } from '../../../../../../Models/SelectedFile'
import { ViewerActions } from '../../../../../../Store/actions/ViewerActions'
import { ConfirmationAlert } from '../../../../././ConfirmationAlert/ConfirmationAlert'
import { ViewerTools } from '../../../../../../Utilities/ViewerTools'
import erroricon from "../../../../../../Assets/images/warning-icon.svg";
import { FileUpload } from '../../../../../../Utilities/helpers/FileUpload'


export const nonExistentFileId = '000000000000000000000000';

export const WorkbenchItem = ({ file, setDraggingSelf, setDraggingItem, refReassignDropdown, setIsDraggingOver, removeFailedItem }) => {

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

  const refReassignPopover = useRef<any>(null);
  const refReassignlink = useRef<any>(null);

  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const { currentDoc, importedFileIds }: any = state.documents;
  const instance: any = viewer?.instance;
  const { currentFile, selectedFileData, isLoading, isFileChanged, performNextAction, fileToChangeWhenUnSaved }: any = state.viewer;

  const loanApplicationId = LocalDB.getLoanAppliationId();


  useEffect(()=>{
    if(fileToChangeWhenUnSaved && fileToChangeWhenUnSaved.isWorkbenchFile && performNextAction){
        performNextActionFn()
    }

  },[performNextAction])


const performNextActionFn= async () =>{
  
  switch (fileToChangeWhenUnSaved.action) {
    case "view":
      if(DocumentActions.performNextAction){
        dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
        DocumentActions.performNextAction =false;
        await viewFile(fileToChangeWhenUnSaved.file)
      
      }
      break;

    case "delete":
      if(DocumentActions.performNextAction){  
    file = fileToChangeWhenUnSaved.file
    DocumentActions.performNextAction = false
      await moveWorkBenchToTrash()
      dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
      }
      break;

    case "reassign":
      if(DocumentActions.performNextAction){
        if(fileToChangeWhenUnSaved.selectedFile.id === file.id)
    setShowingReassignDropdown(true)
      }
      break;
   
    default:
      break;
}
}

  const toggleReassignDropdown = async (e: any) => {

    let target = e.target
    await setCurrentDocument();

    setReassignDropdownTarget(target);
    setShowingReassignDropdown(!showingReassignDropdown);
    showingReassignDropdown ? refReassignDropdown.current.classList.remove("freeze") : refReassignDropdown.current.classList.add("freeze");
  };

  const hideReassign = async() => {
    if(refReassignlink && refReassignlink.current)
     refReassignlink?.current?.classList.remove("overlayOpen")
    if(fileToChangeWhenUnSaved && fileToChangeWhenUnSaved?.selectedFile?.id === currentFile?.fileId )
    await viewFile(fileToChangeWhenUnSaved.selectedFile)
    setShowingReassignDropdown(false);
    refReassignDropdown.current?.classList.remove("freeze")
    if(reassignDropdownTarget)setReassignDropdownTarget(null);
  }

  const handleClickOutside = (event: any) => {
    if (refReassignPopover && !refReassignPopover.current?.contains(event.target) && !refReassignlink.current?.contains(event.target)) {
      hideReassign();
    }
  }

  useEffect(() => {
    window.addEventListener("mousedown", handleClickOutside);
    return () => {
      window.removeEventListener("mousedown", handleClickOutside);
    };
  }, [showingReassignDropdown]);

  useEffect(() => {
    dispatch({ type: DocumentActionsType.SetIsDragging, payload: isDraggingItem });
    if (selectedFileData && file.fileId === selectedFileData.fileId)
      dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: isDraggingItem });
  }, [isDraggingItem]);

  const setCurrentDocument = () => {
    let document = new DocumentRequest(file?.id,
      nonExistentFileId,
      nonExistentFileId,
      "",
      "",
      "",
      [],
      "",
      ""
    )
    if (document) {
      dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: null });

      dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: document });
    }

  }
  const moveWorkBenchToTrash = async () => {
    DocumentActions.performNextAction = false
    if (isFileChanged && file?.fileId === currentFile?.fileId) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
      dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { file, document:null, action:"delete", isWorkbenchFile:true } });
      return;
    }

    setCurrentDocument();
    let cancelCurrentFileViewRequest: boolean = false;
    if (selectedFileData && selectedFileData?.fileId === file.fileId) {
      cancelCurrentFileViewRequest = true;
    }

    let success = await DocumentActions.moveWorkBenchFileToTrash(
      file.id,
      file.fileId,
      cancelCurrentFileViewRequest
    );

    if (success) {  
      if (selectedFileData && selectedFileData?.fileId === file.fileId) {
          ViewerActions.resetInstance(dispatch)
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
        await DocumentActions.getCurrentWorkbenchItem(dispatch, importedFileIds);
      }
      else {

        let d = await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
      }
      let docs = await DocumentActions.getTrashedDocuments(dispatch, importedFileIds)
    }
    dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: null });
    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
  };

  const renderFileActions = () => {
    return (
      <ul>
        <li key={"trash"}>
          <a
            data-title="Trash Bin"
            onClick={moveWorkBenchToTrash}>
            <TrashCan />
          </a>
        </li>
        <li className={`reAssBtn`}>
          <a
            ref={refReassignlink}
            data-title="Reassign"
            onClick={(e) => toggleReassignDropdown(e)}
            className={showingReassignDropdown ? "overlayOpen" : ""}>
            <ReassignListIcon />
          </a>
        </li>
      </ul>
    );
  };

  const viewFileForWorkBench = async () => {

    if (isFileChanged) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
      dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { file, document:null, action:"view", isWorkbenchFile: true } });
      return;
    }
   

    viewFile(file);
  };

  const viewFile = async (file: any) => {
    DocumentActions.performNextAction = false
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
    await DocumentActions.getWorkBenchItems(dispatch, importedFileIds)
    setCurrentDocument();

    let selectedFileData = new SelectedFile(file.id, DocumentActions.getFileName(file), file.fileId)
      ViewerActions.resetInstance(dispatch)
    dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData });
    ViewerTools.currentFileName = selectedFileData.name

    let f = await DocumentActions.getFileToView(
      file?.id,
      nonExistentFileId,
      nonExistentFileId,
      file.fileId,
      false,
      true,
      false,
      dispatch
    );

    let currentFile = new CurrentInView(file.id, f, getFileName(), true, file.fileId);
    dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });
    dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: null });
    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
    
  }





  //   const getAnnoations = async() => {
  //     const{id}:any =  currentDoc
  //     const { id: fileId, isWorkBenchFile }: any = file;
  //     let body = {
  //       id, fromRequestId: nonExistentFileId, fromDocId: nonExistentFileId, fromFileId: fileId
  //         }

  //     await AnnotationActions.fetchAnnotations(body, true)

  // }


  const getFileName = () => {
    if (file?.mcuName) return file?.mcuName;
    return file?.clientName;
  };

  const onDoubleClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>
  ) => {
    if (file) {

      let currentFile: any = new CurrentInView(
        file.id,
        file.src,
        getFileName(),
        true,
        file.fileId
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

  const dragStartHandler = (e: any) => {

    if (file) {
      DocumentActions.showFileBeingDragged(e, file);
      let fileObj = {
        id: file.id,
        fromRequestId: nonExistentFileId,
        fromDocId: nonExistentFileId,
        fromFileId: file.fileId,
        fileName: file.mcuName ? file.mcuName : file.clientName,
        isFromThumbnail: false,
        isFromWorkbench: true,
        isFromCategory: false
      }
      setIsDraggingItem(true);
      setDraggingSelf(true);
      setDraggingItem(true);
      e.dataTransfer.setData("file", JSON.stringify(fileObj));
    }

  }

  const getCurrentFileSelectedStyle = () => {
    if (selectedFileData && file.fileId === selectedFileData.fileId) {
      return "selected"
    }
    return '';
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
            <li>
              <a
                data-testid={``}
                onClick={() => removeFailedItem(file)}
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

  const renderSizeNotAllowed = () => {
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
                File size must be under {FileUpload.allowedSize} mb
                {/* File size over {FileUpload.allowedSize}mb limit{" "} */}
              </span>
            </div>
          </div>
          <div className="action-btns">
          <ul>
            <li>
              <a
                data-testid={``}
                onClick={() => removeFailedItem(file)}
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
      // return renderFileUploadFailed();
    }
    return null
  };

  if (file.notAllowed || file.uploadStatus === 'failed') {
    return renderNotAllowedFile();
  }

  return (
    <>
      <li key={file.name} className={`${isDraggingItem ? 'dragging' : ''}  ${getCurrentFileSelectedStyle()}`}>
        <div className="l-icon">
          <FileIcon />
        </div>
        <div
          onDoubleClick={(event) => onDoubleClick(event)}
          onClick={viewFileForWorkBench}
          draggable={!editingModeEnabled ? true : false}
          onDragStart={(e: any) => {

            dragStartHandler(e)
          }}

          onDragEnd={() => {
            setIsDraggingItem(false);
            setIsDraggingOver(false);
            let dragView: any = window.document.getElementById('fileBeingDragged');
            window.document.body.removeChild(dragView);
          }}
          className="d-name">
              <div>
                <p title={getFileName()}>{getFileName()}</p>
                {file.file && file.uploadProgress <= 100 ? null :
                  <div className="modify-info">
                    <span className="mb-lbl">{file.fileModifiedOn ? "Modified By:" : "Uploaded By:"}</span>{" "}
                    <span className="mb-name">
                      {file.userName ? file.userName : "Borrower"}{" "}
                      {getFileDate(file)}
                    </span>
                  </div>}
              </div>
        </div>
        <div className={`dl-actions ${showingReassignDropdown ? "show" : ""}`}>
          {renderFileActions()}
          {showingReassignDropdown ? (
            <ReassignDropdown
              visible={showingReassignDropdown}
              hide={hideReassign}
              container={refReassignDropdown?.current}
              target={reassignDropdownTarget}
              selectedFile={file}
              isFromWorkbench={true}
              placement="top-end"
              refReassignPopover={refReassignPopover}
            />) : null
          }
        </div>

        {file && file.uploadProgress > 0 && (
          <div
            data-testid="upload-progress-bar"
            className="progress-upload"
            style={{ width: file.uploadProgress + "%" }}
          ></div>
        )}
      </li>
      {/* { isFileChanged && showingConfirmationAlert && fileToChangeWhenUnSaved?.file === file ? <ConfirmationAlert
        viewFile={(file: any, document: any, dispatch: any) => viewFile(file, document, dispatch)}
      /> : ''} */}
    </>
  );
}
