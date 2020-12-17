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

const nonExistentFileId = '000000000000000000000000';

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

  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const { currentDoc }: any = state.documents;
  const instance: any = viewer?.instance;
  const { selectedFileData, currentFile }: any = state.viewer;
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
    setCurrentDocument();
    let cancelCurrentFileViewRequest:boolean = false;
    if(selectedFileData && selectedFileData?.fileId === file.fileId){
      cancelCurrentFileViewRequest = true;
    }
    
    let success = await DocumentActions.moveWorkBenchFileToTrash(
      file.id,
      file.fileId, 
      cancelCurrentFileViewRequest
    );

    if (success) { 
      if(selectedFileData && selectedFileData?.fileId === file.fileId){
        if (viewer?.currentFile) {
          ViewerActions.resetInstance(dispatch)
        }
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
        await DocumentActions.getCurrentWorkbenchItem(dispatch);
      }
      else{
        
      let d = await DocumentActions.getWorkBenchItems(dispatch);
      }
      let docs = await DocumentActions.getTrashedDocuments(dispatch)
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
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
        setCurrentDocument();
        
        let selectedFileData = new SelectedFile(file.id, DocumentActions.getFileName(file), file.fileId )
        if (viewer?.currentFile) {
          ViewerActions.resetInstance(dispatch)
          
        }
        dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData});
        let f = await DocumentActions.getFileToView(
          file?.id,
          nonExistentFileId,
          nonExistentFileId,
          file.fileId
        );

        
        
        
        let currentFile = new CurrentInView(file.id, f, getFileName(), true, file.fileId);
        dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
      };
    

      const getAnnoations = async() => {
        const{id}:any =  currentDoc
        const { id: fileId, isWorkBenchFile }: any = file;
        let body = {
          id, fromRequestId: nonExistentFileId, fromDocId: nonExistentFileId, fromFileId: fileId
            }
       
        await AnnotationActions.fetchAnnotations(body, true)
        
    }
    

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

  const getCurrentFileSelectedStyle = () => {
    if (selectedFileData && file.fileId === selectedFileData.fileId) {
      return "selected"
    }
    return '';
  }
  return (
    <li key={file.name} className={`${isDraggingItem ? 'dragging' : ''}  ${getCurrentFileSelectedStyle()}`}>
      <div className="l-icon">
        <FileIcon />
      </div>
      <div
        onDoubleClick={(event) => onDoubleClick(event)}
        onClick={viewFileForWorkBench}
        draggable={!editingModeEnabled ? true : false}
        onDragStart={(e: any) => {
          DocumentActions.showFileBeingDragged(e, file);
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
  );
}
