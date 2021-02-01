import React, { useState, useRef, useContext, useEffect, MutableRefObject } from "react";
import {
  MinusIcon,
  PlusIcon,
} from "../../../../../../shared/Components/Assets/SVG";
import DocumentActions from "../../../../../../Store/actions/DocumentActions";
import { DocumentRequest } from "../../../../../../Models/DocumentRequest";
import { Document } from "../../../../../../Models/Document";
import { DocumentActionsType } from "../../../../../../Store/reducers/documentsReducer";
import { Store } from "../../../../../../Store/Store";
import { FilesList } from "../FilesList/FilesList";
import { ViewerActions } from "../../../../../../Store/actions/ViewerActions";
import { LocalDB } from "../../../../../../Utilities/LocalDB";
import { DocumentFile } from "../../../../../../Models/DocumentFile";
import { CategoryDocument } from "../../../../../../Models/CategoryDocument";
import { fail } from "assert";
import { ViewerActionsType } from "../../../../../../Store/reducers/ViewerReducer";
import { PDFActions } from "../../../../../../Utilities/PDFActions";
import { ViewerTools } from "../../../../../../Utilities/ViewerTools";
import { PDFThumbnails } from "../../../../../../Utilities/PDFThumbnails";
import { CurrentInView } from "../../../../../../Models/CurrentInView";

type DocumentItemType = {
  // isDragging: boolean
  docInd: number,
  document: DocumentRequest;
  setFileClicked: Function;
  fileClicked: boolean;
  setOpenReassignDropdown: any;
  getDocswithfailedFiles: Function;
  setRetryFile: Function;
  selectedDoc: DocumentRequest,
  retryFile,
  setDroppedOnDoc: Function,
  setWasFileDropped: Function,
  setFilesDroppedFromPC: any
};

export const DocumentItem = ({
  docInd,
  document,
  setFileClicked,
  fileClicked,
  setOpenReassignDropdown,
  getDocswithfailedFiles,
  setRetryFile,
  selectedDoc,
  retryFile,
  setDroppedOnDoc,
  setWasFileDropped,
  setFilesDroppedFromPC
}: DocumentItemType) => {
  // const [isDragging, setIsDragging] = useState<boolean>(false);

  const [show, setShow] = useState(true);

  const [showReassignOverlay, setShowReassign] = useState<boolean>(false);
  const [draggingOverItem, setDraggingOverItem] = useState<boolean>(false);

  const [targetReassign, setTargetReassign] = useState(null);
  const refReassignOverlay = useRef(null);
  const { state, dispatch } = useContext(Store);
  const { currentDoc, documentItems, uploadFailedDocs, isDragging, isDraggingSelf, importedFileIds }: any = state.documents;
  const selectedfiles: Document[] = currentDoc?.files || null;
  let loanApplicationId = LocalDB.getLoanAppliationId();
  const { currentFile, isFileChanged, selectedFileData }: any = state.viewer;

  const [confirmDelete, setConfirmDelete] = useState<boolean>(false);


  let fileListRef = useRef<HTMLUListElement>(null);

  useEffect(() => {

    if (document && currentDoc && document.docId === currentDoc.docId && !fileClicked) {
      // fileListRef.current?.scrollIntoView({
      //   behavior: 'smooth',
      //   block: 'start',
      // });
      setFileClicked(true)
    }
  }, [currentDoc])

  useEffect(() => {
    if (document === selectedDoc && !show) {
      setShow(true)
    }

  }, [selectedDoc])


  const handleSync = () => {
    setShow(!show);
  };

  const onDrophandler = async (e: any) => {
    let filesFromPC = e?.dataTransfer?.files;
    if (filesFromPC?.length) {
      setDroppedOnDoc(document);
      setWasFileDropped(true);
      setFilesDroppedFromPC(filesFromPC);
      return;
    }
    setWasFileDropped(false);
    let file = JSON.parse(e.dataTransfer.getData('file'));

    if (!file.indexes) {
      if (document.files.find(f => f?.id === isDraggingSelf?.id)) {
        return;
      }
    }

    if (isFileChanged && file?.fromFileId === currentFile?.fileId) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });
      dispatch({ type: ViewerActionsType.SetFileToChangeWhenUnSaved, payload: { file: null, document: null, action: "dragged", isWorkbenchFile: false } });
      return;
    }
    if (isDragging) {
      dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
      dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
    }


    let { isFromWorkbench, isFromCategory, isFromThumbnail, isFromTrash } = file;

    if (isFromWorkbench) {

      let success = await DocumentActions.moveFromWorkBenchToCategory(
        document.id,
        document.requestId,
        document.docId,
        file.fromFileId,
      );

      if (success) {
        if (selectedFileData && selectedFileData.fileId === file.id) {
          let currentDocument = documentItems.filter((doc: any) => doc.docId === document.docId)
          setCurrentDocument(currentDocument)
          await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: null });
          let currFile = new CurrentInView(currentFile.id, currentFile.src, currentFile.name, false, currentFile.fileId);
          await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currFile });
        }
        await DocumentActions.getDocumentItems(dispatch, importedFileIds)
        await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
      }

    } else if (isFromCategory) {
      let success = await DocumentActions.reassignDoc(
        file.id,
        file.fromRequestId,
        file.fromDocId,
        file.fromFileId,
        document.requestId,
        document.docId

      );

      if (success) {
        if (selectedFileData && selectedFileData.fileId === file.id) {
          let currentDocument = documentItems.filter((doc: any) => doc.docId === document.docId)
          setCurrentDocument(currentDocument)
          await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: null });
          let currFile = new CurrentInView(currentFile.id, currentFile.src, currentFile.name, false, currentFile.fileId);
          await dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currFile });
        }

        await DocumentActions.getDocumentItems(dispatch, importedFileIds)
      }
    } else if (isFromThumbnail) {
      dispatch({ type: ViewerActionsType.SetIsSaving, payload: true });
      let { id, requestId, docId } = document
      let fileObj = {
        id,
        requestId,
        docId,
        fileId: "000000000000000000000000",
        isFromCategory: true
      }

      let fileData = await PDFActions.createNewFileFromThumbnail(file.indexes, currentFile, document.files);
      let success = await ViewerTools.saveFileWithAnnotations(fileObj, fileData, true, dispatch, document, importedFileIds, file.indexes);

      // let saveAnnotation = await AnnotationActions.saveAnnotations(annotationObj,true);
      if (!!success) {
        await PDFThumbnails.removePages(file.indexes)
        await DocumentActions.getDocumentItems(dispatch, importedFileIds)
        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true })
      }
      dispatch({ type: ViewerActionsType.SetIsSaving, payload: false });
    } else if (isFromTrash) {

      let success = await DocumentActions.moveFromTrashToCategory(
        document.id,
        document.requestId,
        document.docId,
        file.fromFileId,
      );

      if (success) {
        await DocumentActions.getDocumentItems(dispatch, importedFileIds)
        await DocumentActions.getTrashedDocuments(dispatch, importedFileIds);
      }

    }

    dispatch({ type: ViewerActionsType.SetPerformNextAction, payload: false });
    setShow(true);
  }

  const setCurrentDocument = (document: any) => {
    if (document) {
      dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: null });
    }

    dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: document });
  };
  const renderDeleteDocSlider = (document: DocumentRequest) => {
    return (
      <div className="list-remove-alert" data-testid="delete-alert">
        <span className="list-remove-text">
          Remove this document type?
          {(document.status === 'Borrower to do' || document.status === 'Started') ? " It’ll disappear from the borrower’s Needs List?" : null}
        </span>
        <div className="list-remove-options">
          <button data-testid="confirm-doc-delete"
            onClick={() => {
              deleteDoc();
              setConfirmDelete(false);
            }}
            className="btn btn-sm btn-secondry"
          >
            Yes
              </button>
          <button data-testid="hide-doc-alert"
            onClick={() => setConfirmDelete(false)}
            className="btn btn-sm btn-primary"
          >
            No
              </button>
        </div>
      </div>
    )
  }
  const deleteDoc = async () => {

    try {
      await DocumentActions.deleteDocCategory(
        document.id,
        document.requestId,
        document.docId
      );
    } catch (error) {
      // file.uploadStatus = "failed";
      console.log("error during file submit", error);
      console.log("error during file submit", error.response);
    }
    getDocswithfailedFiles()

  }





  const CapitalizeText = (text: string) => {
    if (text) {
      var splitStr = text.toLowerCase().split(' ');
      for (var i = 0; i < splitStr.length; i++) {
        splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
      }
      return splitStr.join(' ');
    }
  }


  const renderStatus = (status: string) => {
    let cssClass = '';
    switch (status) {
      case 'Pending review':
        cssClass = 'pending';
        break;
      case 'Started':
        cssClass = 'started';
        break;
      case 'Borrower to do':
        cssClass = 'borrower';
        break;
      case 'Completed':
        cssClass = 'completed';
        break;
      case 'In draft':
        cssClass = 'indraft';
        break;
      case 'Manually added':
        cssClass = 'manualyadded';
        break;
      default:
        cssClass = 'pending';
    }
    return cssClass;
  };

  const renderDocumentTile = () => {
    return (
      <div className="dm-dt-tr1" data-testid="document-item">
        <div className="dm-dt-tr1-left" >
          <h4 data-testid="document-name" title={document.docName} className="viewed" onClick={handleSync}>
            {show ? <MinusIcon /> : <PlusIcon />} {document.docName}
          </h4>
          {/* {show && <div className="link-hiddenFiles"><a >Hidden (2 files)</a> </div>} */}
        </div>
        <div className="dm-dt-tr1-right d-flex align-items-center">
          <div className={`lbl-status capitalize ${renderStatus(document?.status)}`}>{CapitalizeText(document?.status)}</div>
          {/*</div>*/}
          {/*<div>*/}
          {document.files && document.files.length === 0 ?
            (<button
              data-testid="btn-doc-delete"
              onClick={() => setConfirmDelete(true)}
              className="btn btn-delete btn-sm"
            >
              <em className="zmdi zmdi-close"></em>
            </button>) : null
          }


        </div>
        {confirmDelete &&
          renderDeleteDocSlider(document)
        }
      </div>
    );
  };

  return (
    <section data-testid="doc-dnd"
      onMouseLeave={() => setDraggingOverItem(false)}
      className={`dm-dt-tr doc-m-cat-list`}
      onDragEnter={(e: any) => {
        e.preventDefault();
        setDraggingOverItem(true);
      }}
      onDragOver={(e: any) => {
        e.preventDefault();
        setDraggingOverItem(true);
      }}

      onDrop={(e: any) => {
        e.preventDefault();
        onDrophandler(e);
        setDroppedOnDoc(document);
        setDraggingOverItem(false);

      }}
      ref={fileListRef}>
      {renderDocumentTile()}
      {show && (
        <FilesList
          document={document}
          docInd={docInd}
          setRetryFile={setRetryFile}
          setFileClicked={setFileClicked}
          getDocswithfailedFiles={getDocswithfailedFiles}
          setOpenReassignDropdown={setOpenReassignDropdown}
          retryFile={retryFile}
        />
      )}
      {draggingOverItem && !document?.files?.find(f => f?.id == isDraggingSelf?.id) ? <div className="dropwarp"
        onDragLeave={(e: any) => {
          e.preventDefault();
          setDraggingOverItem(false);
        }}><span>Drop Here</span></div> : ''}
    </section>
  );
};
