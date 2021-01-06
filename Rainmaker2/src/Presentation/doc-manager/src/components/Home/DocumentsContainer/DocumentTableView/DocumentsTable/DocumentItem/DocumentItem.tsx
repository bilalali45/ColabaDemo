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

type DocumentItemType = {
  // isDragging: boolean
  docInd: number,
  document: DocumentRequest;
  refReassignDropdown: any;
  setFileClicked: Function;
  fileClicked: boolean;
  setOpenReassignDropdown: any;
  getDocswithfailedFiles: Function;
  setRetryFile: Function;
  inputRef: MutableRefObject<HTMLInputElement>,
  selectedDoc: DocumentRequest,
  retryFile
};

export const DocumentItem = ({
  docInd,
  document,
  refReassignDropdown,
  setFileClicked,
  fileClicked,
  setOpenReassignDropdown,
  getDocswithfailedFiles,
  setRetryFile,
  inputRef,
  selectedDoc,
  retryFile
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
  const { currentFile, isFileChanged }: any = state.viewer;

  const [confirmDelete, setConfirmDelete] = useState<boolean>(false);


  let fileListRef = useRef<HTMLUListElement>(null);

  useEffect(() => {

    if (document && currentDoc && document.docId === currentDoc.docId && !fileClicked) {
      fileListRef.current?.scrollIntoView({
        behavior: 'smooth',
        block: 'start',
      });
      setFileClicked(true)
    }
  }, [currentDoc])

  useEffect(() => {
    if (document === selectedDoc && !show) {
      setShow(true)
    }

  }, [selectedDoc])

  const handleClick = () => {
    setShow(!show);
  };
  const handleClickReassign = (event: any) => {
    setShowReassign(!showReassignOverlay);
    setTargetReassign(event.target);
  };
  const hideReassignOverlay = () => {
    setShowReassign(false);
  };

  const createNewFileForThePage = (index: number) => {
    // DocumentActions.uploadFile(index);
  };

  const handleSync = () => {
    setShow(!show);
  };

  const onDrophandler = async (e: any) => {
    let file = JSON.parse(e.dataTransfer.getData('file'));

    if (!file.indexes) {
      if (document.files.find(f => f.id === isDraggingSelf.id)) {
        return;
      }
    }

    console.log(file);
    if (isFileChanged && file?.fromFileId === currentFile?.fileId) {
      dispatch({ type: ViewerActionsType.SetShowingConfirmationAlert, payload: true });

      return;
    }
    if (isDragging) {
      dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
      dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
    }


    let { isFromWorkbench, isFromCategory, isFromThumbnail, isFromTrash } = file;
    dispatch({ type: ViewerActionsType.SetIsSaving, payload: true });
    if (isFromWorkbench) {

      let success = await DocumentActions.moveFromWorkBenchToCategory(
        document.id,
        document.requestId,
        document.docId,
        file.fromFileId,
      );

      if (success) {
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
        await DocumentActions.getDocumentItems(dispatch, importedFileIds)
      }
    } else if (isFromThumbnail) {
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
    dispatch({ type: ViewerActionsType.SetIsSaving, payload: false });
    setShow(true);
  }

  const renderDeleteDocSlider = (document: DocumentRequest) => {
    return (
      <div className="list-remove-alert">
        <span className="list-remove-text">
          Are you sure you want to delete this document type?
        <br />
          {(document.status === 'Borrower to do' || document.status === 'Started') ? " This item will disappear from the borrower's Needs List." : null}
        </span>
        <div className="list-remove-options">
          <button
            onClick={() => {
              deleteDoc();
              setConfirmDelete(false);
            }}
            className="btn btn-sm btn-secondry"
          >
            Yes
              </button>
          <button
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
      <div className="dm-dt-tr1">
        <div className="dm-dt-tr1-left">
          <h4 title={document.docName} className="viewed" onClick={handleSync}>
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
              data-testid="btn-delete"
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
    <section
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
        setDraggingOverItem(false);
      }}
      ref={fileListRef}>
      {renderDocumentTile()}
      {show && (
        <FilesList
          document={document}
          docInd={docInd}
          refReassignDropdown={refReassignDropdown}
          setRetryFile={setRetryFile}
          setFileClicked={setFileClicked}
          getDocswithfailedFiles={getDocswithfailedFiles}
          setOpenReassignDropdown={setOpenReassignDropdown}
          inputRef={inputRef}
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
