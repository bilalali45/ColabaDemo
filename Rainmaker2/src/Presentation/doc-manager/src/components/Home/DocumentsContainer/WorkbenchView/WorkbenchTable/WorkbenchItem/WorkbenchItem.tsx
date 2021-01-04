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


export const nonExistentFileId = '000000000000000000000000';

export const WorkbenchItem = ({ file, setDraggingSelf, setDraggingItem, refReassignDropdown }: any) => {

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
  const { currentFile, selectedFileData, isLoading, isFileChanged, showingConfirmationAlert, fileToChangeWhenUnSaved }: any = state.viewer;

  const loanApplicationId = LocalDB.getLoanAppliationId();

  const toggleReassignDropdown = async (e: any) => {

    if (isFileChanged && file?.id === currentFile?.id) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

      return;
    }

    let target = e.target
    await setCurrentDocument();

    setReassignDropdownTarget(target);
    setShowingReassignDropdown(!showingReassignDropdown);
    showingReassignDropdown ? refReassignDropdown.current.classList.remove("freeze") : refReassignDropdown.current.classList.add("freeze");
  };

  const hideReassign = () => {

    setShowingReassignDropdown(false);
    refReassignDropdown.current?.classList.remove("freeze")
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

    if (isFileChanged && file?.fileId === currentFile?.fileId) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

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
        if (viewer?.currentFile) {
          ViewerActions.resetInstance(dispatch)
        }
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
        await DocumentActions.getCurrentWorkbenchItem(dispatch, importedFileIds);
      }
      else {

        let d = await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
      }
      let docs = await DocumentActions.getTrashedDocuments(dispatch, importedFileIds)
    }
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
      return;
    }
    // if (isFileChanged) {
    //   dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
    //   dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { file, document } });
    //   return;
    // }

    viewFile(file, null, dispatch);
  };

  const viewFile = async (file: any, document: any, dispatch: any) => {
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
    setCurrentDocument();

    let selectedFileData = new SelectedFile(file.id, DocumentActions.getFileName(file), file.fileId)
    if (viewer?.currentFile) {
      ViewerActions.resetInstance(dispatch)

    }
    dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData });
    let f = await DocumentActions.getFileToView(
      file?.id,
      nonExistentFileId,
      nonExistentFileId,
      file.fileId,
      false,
      true,
      false
    );

    let currentFile = new CurrentInView(file.id, f, getFileName(), true, file.fileId);
    dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });
    dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
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
            let dragView: any = window.document.getElementById('fileBeingDragged');
            window.document.body.removeChild(dragView);
          }}
          className="d-name">
          {!!editingModeEnabled ? (
            <RenameFile
              editingModeEnabled={editingModeEnabled}
              editMode={editMode}
              isWorkBenchFile={true}
            />
          ) : (
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
            )}
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
