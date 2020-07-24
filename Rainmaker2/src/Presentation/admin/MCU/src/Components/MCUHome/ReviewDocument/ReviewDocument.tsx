import React, { useEffect, useCallback, useState } from "react";
import { useHistory, useLocation } from "react-router-dom";
import { Http } from "rainsoft-js";
import Axios from "axios";
import { DocumentView } from 'rainsoft-rc';
import _ from 'lodash'

import { ReviewDocumentHeader } from "./ReviewDocumentHeader/ReviewDocumentHeader";
import { ReviewDocumentStatement } from "./ReviewDocumentStatement/ReviewDocumentStatement";
import { NeedListDocumentType } from "../../../Entities/Types/Types";
import { NeedListEndpoints } from "../../../Store/endpoints/NeedListEndpoints";
import { LocalDB } from "../../../Utils/LocalDB";
import emptyIcon from '../../../Assets/images/empty-icon.svg';
export const ReviewDocument = () => {
  const [currentDocument, setCurrentDocument] = useState<
    NeedListDocumentType
  >();
  const [documentList1, setDocumentList1] = useState<NeedListDocumentType[]>(
    []
  );
  const [navigationIndex, setNavigationIndex] = useState(0);
  const [loading, setLoading] = useState(false);
  const [currentFileIndex, setCurrentFileIndex] = useState(0)
  const [documentDetail, setDocumentDetail] = useState(false)
  const [typeIdId, setTypeIdId] = useState<{ id: string | null, typeId: string | null }>({
    id: null,
    typeId: null
  })
  const [clientName, setClientName] = useState('')
  const [perviousDocumentButtonDisabled, setPerviousDocumentButtonDisabled] = useState(true)
  const [nextDocumentButtonDisabled, setNextDocumentButtonDisabled] = useState(false)

  const tenantId = LocalDB.getTenantId()

  const history = useHistory();
  const location = useLocation();
  const { state } = location;

  const goBack = () => {
    history.push("/needlist");
  };

  const [blobData, setBlobData] = useState<any>();

  const documentsForReviewArrayIndexes = () => _.keys(_.pickBy(documentList1, { status: 'Pending review' }))

  const getDocumentForView = useCallback(
    async (id, requestId, docId, fileId, tenantId) => {
      const params = {
        id,
        requestId,
        docId,
        fileId,
        tenantId,
      };

      try {
        setLoading(true)

        const http = new Http();

        const authToken = LocalDB.getAuthToken()

        const url = NeedListEndpoints.GET.documents.view(
          id,
          requestId,
          docId,
          fileId,
          tenantId
        )

        const response = await Axios.get(http.createUrl(http.baseUrl, url), {
          params,
          responseType: 'arraybuffer',
          headers: {
            Authorization: `Bearer ${authToken}`
          }
        })

        setBlobData(response)
        setLoading(false)
      } catch (error) {
        setLoading(false)

        alert('Something went wrong while fetching document/file from server.')
      } finally {
        setLoading(false)
      }
    },
    []
  );

  const onNextDocument = useCallback(() => {
    const pendingReviewDocuments: NeedListDocumentType[] = documentList1.filter((document: NeedListDocumentType) => document.status === 'Pending review')

    const indexOfReview = navigationIndex + 1

    const currentDocument = pendingReviewDocuments[indexOfReview]

    if (!pendingReviewDocuments[navigationIndex + 2] && !nextDocumentButtonDisabled) {
      setNextDocumentButtonDisabled(true)
    }

    if (perviousDocumentButtonDisabled === true) {
      setPerviousDocumentButtonDisabled(false)
    }

    if (!currentDocument) return

    const { id, requestId, docId, files } = currentDocument

    setCurrentDocument(() => currentDocument);
    setNavigationIndex(() => indexOfReview);
    setCurrentFileIndex(0)
    setTypeIdId({ id: null, typeId: null })

    if (!!files && files.length > 0) {
      setClientName(files[0].clientName)

      getDocumentForView(id, requestId, docId, files[0].id, 1);
    }
  }, [nextDocumentButtonDisabled, perviousDocumentButtonDisabled, navigationIndex, documentList1, getDocumentForView]);

  const onPreviousDocument = useCallback(() => {
    const pendingReviewDocuments: NeedListDocumentType[] = documentList1.filter((document: NeedListDocumentType) => document.status === 'Pending review')

    const indexOfReview = navigationIndex - 1

    const currentDocument = pendingReviewDocuments[indexOfReview]

    if (!pendingReviewDocuments[navigationIndex - 2] && !perviousDocumentButtonDisabled) {
      setPerviousDocumentButtonDisabled(true)
    }

    if (nextDocumentButtonDisabled === true) {
      setNextDocumentButtonDisabled(false)
    }

    if (!currentDocument) return

    const { id, requestId, docId, files } = currentDocument

    setCurrentDocument(() => currentDocument);
    setNavigationIndex(() => indexOfReview);
    setCurrentFileIndex(() => 0)
    setTypeIdId({ id: null, typeId: null })

    if (!!files && files.length > 0) {
      setClientName(files[0].clientName)

      getDocumentForView(id, requestId, docId, files[0].id, tenantId);
    }
  }, [nextDocumentButtonDisabled, perviousDocumentButtonDisabled, documentList1, navigationIndex, documentList1, getDocumentForView, tenantId]);

  const moveNextFile = useCallback(async (index: number, fileId: string, clientName: string, loadingFile?: boolean) => {

    if (index === currentFileIndex || loadingFile) return

    if (currentDocument) {
      const { id, requestId, docId } = currentDocument

      const tenantId = LocalDB.getTenantId()

      setCurrentFileIndex(() => index)
      setBlobData(() => null)
      setClientName(clientName)

      !loadingFile && getDocumentForView(id, requestId, docId, fileId, tenantId)
    }
  }, [setCurrentFileIndex, getDocumentForView, currentDocument, currentFileIndex])

  const setTypeIdAndIdForActivityLogs = useCallback((id, typeIdOrDocName) => {
    setTypeIdId({ id, typeId: typeIdOrDocName })
  }, [])

  useEffect(() => {
    //onload Goto Top
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
  }, [])

  useEffect(() => {
    const onKeyDown = (event: any) => {
      if (event.key === 'Escape') {
        goBack()
      }
    }

    window.addEventListener('keydown', onKeyDown)

    return () => window.removeEventListener('keydown', onKeyDown) //clear up event
  }, [goBack])

  useEffect(() => {
    if (loading) return

    if (!!location.state) {
      try {
        const { documentList, currentDocumentIndex, documentDetail } = state as any;
        const doc: NeedListDocumentType = documentList[currentDocumentIndex];

        if (!!documentList && documentList.length) {
          if (!documentDetail) {
            const pendingReviewDocuments: NeedListDocumentType[] = documentList.filter((document: NeedListDocumentType) => document.status === 'Pending review')

            if (pendingReviewDocuments.length > 0) {
              const index = pendingReviewDocuments.findIndex((document: NeedListDocumentType) => document.docId === doc.docId)

              if (!pendingReviewDocuments[index + 1]) {
                setNextDocumentButtonDisabled(() => true)
              }

              if (pendingReviewDocuments[index - 1] && perviousDocumentButtonDisabled === true) {
                setPerviousDocumentButtonDisabled(() => false)
              }

              setNavigationIndex(index);
            }
          } else {
            setNavigationIndex(currentDocumentIndex);
          }

          setDocumentList1(() => documentList);
          setCurrentDocument(() => documentList[currentDocumentIndex]);
          setDocumentDetail(() => documentDetail)

          const { id, requestId, docId, files, typeId, docName } = doc

          if (!loading && !!files && !!files.length && files.length > 0) {
            setClientName(files[0].clientName)

            getDocumentForView(
              id,
              requestId,
              docId,
              files[0].id,
              tenantId
            );
          } else {
            setTypeIdId({ id, typeId: !!typeId ? typeId : docName })
          }
        }
      } catch (error) {
        console.log("error", error);

        alert("Something went wrong. Please try again.");
      }
    }
  }, [getDocumentForView, perviousDocumentButtonDisabled, state, location.state, tenantId]);

  const getNextFileIndex = () => {
    if (currentDocument?.files[currentFileIndex + 1]) {
      return currentFileIndex + 1
    }

    return -1
  }

  const getPreviousFileIndex = () => {
    if (currentDocument?.files[currentFileIndex - 1]) {
      return currentFileIndex - 1
    }

    return -1
  }

  const onMoveArrowKeys = (event: KeyboardEvent) => {
    if (loading) return

    if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
      event.preventDefault()

      if (currentDocument) {

        if (event.key === 'ArrowDown') { // move forward
          const fileIndex = getNextFileIndex()

          if (fileIndex !== -1) {
            const currentFile = currentDocument.files[fileIndex]

            moveNextFile(fileIndex, currentFile.id, currentFile.clientName, loading)
          }
        } else if (event.key === 'ArrowUp') { // move back
          const fileIndex = getPreviousFileIndex()

          if (fileIndex !== -1) {
            const currentFile = currentDocument.files[fileIndex]

            moveNextFile(fileIndex, currentFile.id, currentFile.clientName, loading)
          }
        }
      }
    }
  }

  useEffect(() => {
    window.addEventListener('keydown', onMoveArrowKeys)

    return () => { window.removeEventListener('keydown', onMoveArrowKeys) }
  }, [currentDocument, loading, currentFileIndex])

  return (
    <div
      id="ReviewDocument"
      data-component="ReviewDocument"
      className="review-document"
    >
      <ReviewDocumentHeader
        id={typeIdId.id}
        typeId={typeIdId.typeId}
        hideNextPreviousNavigation={documentDetail || documentsForReviewArrayIndexes().length === 1}
        buttonsEnabled={!loading}
        onClose={goBack}
        nextDocument={onNextDocument}
        previousDocument={onPreviousDocument}
        perviousDocumentButtonDisabled={perviousDocumentButtonDisabled}
        nextDocumentButtonDisabled={nextDocumentButtonDisabled}
        documentDetail={documentDetail}
      />
      <div className="review-document-body">
        <div className="row">
          {!!currentDocument && currentDocument.files && currentDocument.files.length ? (
            <div className="review-document-body--content col-md-8">
              <div className="doc-view-mcu">
                <DocumentView
                  loading={loading}
                  id={currentDocument.id}
                  requestId={currentDocument.requestId}
                  docId={currentDocument.docId}
                  tenantId={tenantId}
                  clientName={clientName}
                  blobData={blobData}
                  hideViewer={() => { }}
                />
              </div>
            </div>
          ) : (
              <div className="no-preview">
                <div className="no-preview--wrap">
                  <div className="clearfix">
                    <img src={emptyIcon} alt="No preview available" />
                  </div>
                  <h2>Nothing In {currentDocument?.docName}</h2>
                  <p>No file submitted yet</p>
                </div>
              </div>
            )}
          {/* review-document-body--content */}
          {!!currentDocument && currentDocument.files && currentDocument.files.length > 0 && (
            <aside className="review-document-body--aside col-md-4">
              <ReviewDocumentStatement
                typeIdAndIdForActivityLogs={setTypeIdAndIdForActivityLogs}
                moveNextFile={moveNextFile}
                loadingFile={!!loading ? true : false}
                currentDocument={!!currentDocument ? currentDocument : null}
                currentFileIndex={currentFileIndex}
              />
            </aside>
          )}
          {/* review-document-body--aside */}
        </div>
      </div>
      {/* review-document-body */}
    </div>
  );
};
