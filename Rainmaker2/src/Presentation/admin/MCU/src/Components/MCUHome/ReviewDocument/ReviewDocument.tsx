import React, { useEffect, useCallback, useState, useContext } from 'react';
import { useHistory, useLocation } from 'react-router-dom';
import { Http } from 'rainsoft-js';
import Axios from 'axios';
import { DocumentView } from 'rainsoft-rc';
import _ from 'lodash';

import { ReviewDocumentHeader } from './ReviewDocumentHeader/ReviewDocumentHeader';
import { ReviewDocumentStatement } from './ReviewDocumentStatement/ReviewDocumentStatement';
import { NeedListEndpoints } from '../../../Store/endpoints/NeedListEndpoints';
import { LocalDB } from '../../../Utils/LocalDB';
import emptyIcon from '../../../Assets/images/empty-icon.svg';
import { Store } from '../../../Store/Store';
import {
  NeedListType,
  NeedListActionsType
} from '../../../Store/reducers/NeedListReducer';
import { NeedList } from '../../../Entities/Models/NeedList';
import { DocumentStatus } from '../../../Entities/Types/Types';
import { timeout } from '../../../Utils/helpers/Delay';

export const ReviewDocument = () => {
  console.log('------------------------------- Review Document--------------------------------------')
  const [currentDocument, setCurrentDocument] = useState<NeedList>();
  const [navigationIndex, setNavigationIndex] = useState(0);
  const [loading, setLoading] = useState(false);
  const [currentFileIndex, setCurrentFileIndex] = useState(0);
  const [documentDetail, setDocumentDetail] = useState(false);
  const [fileViewd, setFileViewd] = useState(false);
  const [clientName, setClientName] = useState('');
  const [previousDocumentButtonDisabled, setPreviousDocumentButtonDisabled] = useState(true);
  const [nextDocumentButtonDisabled, setNextDocumentButtonDisabled] = useState(false);
  const [acceptRejectLoading, setAcceptRejectLoading] = useState(false);
  const [haveDocuments, setHaveDocuments] = useState(false);
  const { state: AppState, dispatch } = useContext(Store);
  const { needListManager } = AppState;
  const { needList } = needListManager as Pick<NeedListType, 'needList'>;
  const [blobData, setBlobData] = useState<any>();
  
  const history = useHistory();
  const location = useLocation();
  const { state } = location;

  const goBack = () => {
    console.log('Going Back---------------------------->')
    history.push(`/needlist/${LocalDB.getLoanAppliationId()}`)
  };

 
  

  const documentsForReviewArrayIndexes = () =>
    _.keys(_.pickBy(needList, { status: DocumentStatus.PENDING_REVIEW }));

  const getDocumentForView = useCallback(
    async (id, requestId, docId, fileId) => {
      try {
        setLoading(true);

        const http = new Http();

        const authToken = LocalDB.getAuthToken();

        const url = NeedListEndpoints.GET.documents.view(
          id,
          requestId,
          docId,
          fileId
        );

        const response = await Axios.get(http.createUrl(http.baseUrl, url), {
          responseType: 'arraybuffer',
          headers: {
            Authorization: `Bearer ${authToken}`
          }
        });

        setBlobData(response);
        setFileViewd(true);
        setLoading(false);
      } catch (error) {
        alert('Something went wrong while fetching document/file from server.');

        goBack();
      }
    },
    []
  );

  const moveNextFile = useCallback(
    async (
      index: number,
      fileId: string,
      clientName: string,
      loadingFile?: boolean
    ) => {
      if (index === currentFileIndex || loading === true) return;

      if (currentDocument) {
        const { id, requestId, docId } = currentDocument;

        setCurrentFileIndex(() => index);
        setBlobData(() => null);
        setClientName(clientName);

        return getDocumentForView(id, requestId, docId, fileId);
      }
    },
    [
      setCurrentFileIndex,
      getDocumentForView,
      currentDocument,
      currentFileIndex,
      loading
    ]
  );

  const changeCurrentDocument = useCallback(
    (nextDocument: NeedList, nextIndex: number, fromHeader: boolean) => {
      const { id, requestId, docId, files } = nextDocument;

      const nextDocIndex = needList.findIndex(
        (document, index) =>
          document.docId !== nextDocument.docId &&
          document.status === DocumentStatus.PENDING_REVIEW &&
          index > nextIndex
      );

      if (nextDocIndex === -1) {
        setNextDocumentButtonDisabled(true);
      }

      setCurrentDocument(() => nextDocument);
      setNavigationIndex(() => nextIndex);
      setCurrentFileIndex(0);

      if (!!files && files.length > 0) {
        setClientName(files[0].clientName);

        getDocumentForView(id, requestId, docId, files[0].id);
      }
    },
    []
  );

  const navigateDocument = useCallback(
    (
      docs: NeedList[],
      navigateBackOrForward: string,
      fromHeader: boolean = false
    ) => {
      if (currentDocument) {
        let index: number;

        if (navigateBackOrForward === 'next') {
          const nextDocIndex = docs.findIndex(
            (doc, index) =>
              doc.docId !== currentDocument.docId &&
              index > navigationIndex &&
              doc.status === DocumentStatus.PENDING_REVIEW
          );

          index = nextDocIndex;
        } else {
          index = docs.findIndex(
            (doc, index) =>
              doc.docId !== currentDocument.docId &&
              index === navigationIndex - 1 &&
              doc.status === DocumentStatus.PENDING_REVIEW
          );

          if (index === -1) {
            const previousDoc = docs
              .filter(
                (doc, index) =>
                  doc.docId !== currentDocument.docId &&
                  doc.status === DocumentStatus.PENDING_REVIEW &&
                  index < navigationIndex
              )
              .reverse()[0];

            if (previousDoc) {
              index = docs.findIndex((doc) => doc.docId === previousDoc.docId);
            }
          }
        }

        if (index === -1) {
          if (fromHeader === false) {
            return goBack();
          }
        } else {
          const nextDocument = docs[index];

          if (
            navigateBackOrForward === 'back' &&
            nextDocumentButtonDisabled === true
          ) {
            setNextDocumentButtonDisabled(false);
          }

          if (navigateBackOrForward === 'back' && fromHeader === true) {
            let currIndex = index;

            const doc = docs
              .filter(
                (doc, index) =>
                  doc.docId !== currentDocument.docId &&
                  doc.status === DocumentStatus.PENDING_REVIEW &&
                  index < currIndex
              )
              .reverse()[0];

            if (!doc) {
              setPreviousDocumentButtonDisabled(() => true);
            }
          } else if (fromHeader === true) {
            previousDocumentButtonDisabled === true &&
              setPreviousDocumentButtonDisabled(() => false);
          }

          changeCurrentDocument(nextDocument, index, fromHeader);
        }
      }
    },
    [navigationIndex, currentDocument]
  );

  const acceptDocument = useCallback(
    async (needList: NeedList[]) => {
      if (currentDocument) {
        try {
          setAcceptRejectLoading(true);

          const { id, requestId, docId } = currentDocument;

          const http = new Http();

          await http.post(NeedListEndpoints.POST.documents.accept(), {
            id,
            requestId,
            docId
          });

          setAcceptRejectLoading(false);

          const clonedNeedList = _.cloneDeep(needList);

          const clonedCurrentDocument = clonedNeedList[navigationIndex];
          clonedCurrentDocument.status = DocumentStatus.COMPLETED;

          dispatch({
            type: NeedListActionsType.SetNeedListTableDATA,
            payload: clonedNeedList
          });

          setCurrentDocument(clonedCurrentDocument);

          await timeout(1000);

          navigateDocument(clonedNeedList, 'next');
        } catch (error) {
          alert('Something went wrong. Please try again later.');

          setAcceptRejectLoading(false);
        }
      }
    },
    [navigateDocument, currentDocument, navigationIndex, dispatch]
  );

  const rejectDocument = useCallback(
    async (needList: NeedList[], rejectDocumentMessage: string) => {
      if (currentDocument) {
        try {
          setAcceptRejectLoading(true);

          const { id, requestId, docId } = currentDocument;

          const loanApplicationId = Number(LocalDB.getLoanAppliationId());

          const http = new Http();

          await http.post(NeedListEndpoints.POST.documents.reject(), {
            loanApplicationId,
            id,
            requestId,
            docId,
            message: rejectDocumentMessage.trim()
          });

          setAcceptRejectLoading(false);

          const clonedNeedList = _.cloneDeep(needList);

          const clonedCurrentDocument = clonedNeedList[navigationIndex];
          clonedCurrentDocument.status = DocumentStatus.IN_DRAFT;

          dispatch({
            type: NeedListActionsType.SetNeedListTableDATA,
            payload: clonedNeedList
          });

          setCurrentDocument(clonedCurrentDocument);

          await timeout(1000);

          navigateDocument(needList, 'next');
        } catch (error) {
          alert('Something went wrong. Please try again later.');

          setAcceptRejectLoading(false);
        }
      }
    },
    [navigateDocument, currentDocument, navigationIndex, dispatch]
  );

  const getNextFileIndex = () => {
    if (currentDocument!.files[currentFileIndex + 1]) {
      return currentFileIndex + 1;
    }

    return -1;
  };

  const getPreviousFileIndex = () => {
    if (currentDocument!.files[currentFileIndex - 1]) {
      return currentFileIndex - 1;
    }

    return -1;
  };

  const onMoveArrowKeys = (event: KeyboardEvent) => {
    if (loading) return;

    if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
      event.preventDefault();

      if (currentDocument) {
        if (event.key === 'ArrowDown') {
          // move forward
          const fileIndex = getNextFileIndex();

          if (fileIndex !== -1) {
            const currentFile = currentDocument.files[fileIndex];

            moveNextFile(
              fileIndex,
              currentFile.id,
              currentFile.clientName,
              loading
            );
          }
        } else if (event.key === 'ArrowUp') {
          // move back
          const fileIndex = getPreviousFileIndex();

          if (fileIndex !== -1) {
            const currentFile = currentDocument.files[fileIndex];

            moveNextFile(
              fileIndex,
              currentFile.id,
              currentFile.clientName,
              loading
            );
          }
        }
      }
    }
  };

  useEffect(() => {
    //apex
    if (!!currentDocument && currentDocument.files && currentDocument.files.length) {
      setHaveDocuments(true);
    }
    else {
      setHaveDocuments(false)
    }


    window.addEventListener('keydown', onMoveArrowKeys);

    return () => {
      window.removeEventListener('keydown', onMoveArrowKeys);
    };
  }, [currentDocument, loading, currentFileIndex]);

  useEffect(() => {
    //onload Goto Top
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
  }, []);

  useEffect(() => {
    const onKeyDown = (event: any) => {
      if (event.key === 'Escape') {
        if (loading) return; // Prevent closing it down while loading

        goBack();
      }
    };

    window.addEventListener('keydown', onKeyDown);

    return () => window.removeEventListener('keydown', onKeyDown); //clear up event
  }, [goBack]);

  useEffect(() => {
    if (loading) return;

    if (!!location.state) {
      try {
        const { currentDocumentIndex, documentDetail, fileIndex } = state as any;
        const doc = needList[currentDocumentIndex];

        if (!documentDetail) {
          const nextIndex = needList.findIndex(
            (document, index) =>
              document.docId !== doc.docId &&
              document.status === DocumentStatus.PENDING_REVIEW &&
              index > currentDocumentIndex
          );

          const previousIndex = needList.findIndex(
            (document, index) =>
              document.docId !== doc.docId &&
              document.status === DocumentStatus.PENDING_REVIEW &&
              index < currentDocumentIndex
          );

          if (nextIndex === -1) {
            setNextDocumentButtonDisabled(() => true);
          }

          if (previousIndex !== -1) {
            setPreviousDocumentButtonDisabled(() => false);
          }
        }

        setNavigationIndex(currentDocumentIndex);
        setCurrentDocument(() => doc);
        !!fileIndex && setCurrentFileIndex(fileIndex);
        setDocumentDetail(() => documentDetail);

        const { id, requestId, docId, files, typeId, docName } = doc;

        if (!loading && !!files && !!files.length && files.length > 0) {
          setClientName(files[fileIndex || 0].clientName);

          getDocumentForView(id, requestId, docId, files[fileIndex || 0].id);
        }
      } catch (error) {
        console.log('error', error);

        alert('Something went wrong. Please try again.');
      }
    }
  }, [getDocumentForView, state, location.state]);


  

  return (
    <div
    data-testid="testId"
      id="ReviewDocument"
      data-component="ReviewDocument"
      className="review-document"
    >
      {!!currentDocument && (
        <ReviewDocumentHeader
          haveDocuments={haveDocuments}
          id={currentDocument.id}
          requestId={currentDocument.requestId}
          docId={currentDocument.docId}
          hideNextPreviousNavigation={
            documentDetail || documentsForReviewArrayIndexes().length === 1
          }
          buttonsEnabled={!loading}
          onClose={goBack}
          nextDocument={() => navigateDocument(needList, 'next', true)}
          previousDocument={() => navigateDocument(needList, 'back', true)}
          perviousDocumentButtonDisabled={
            previousDocumentButtonDisabled || acceptRejectLoading
          }
          nextDocumentButtonDisabled={
            nextDocumentButtonDisabled || acceptRejectLoading
          }
          documentDetail={documentDetail}
        />
      )}
      <div className="review-document-body">
        <div className="row">
          {!!currentDocument &&
            currentDocument.files &&
            currentDocument.files.length ? (
              <div className="review-document-body--content col-md-8">
                <div className="doc-view-mcu">
                  <DocumentView
                    loading={loading}
                    id={currentDocument.id}
                    requestId={currentDocument.requestId}
                    docId={currentDocument.docId}
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
          {!!currentDocument &&
            currentDocument.files &&
            currentDocument.files.length > 0 && (
              <aside className="review-document-body--aside col-md-4">
                <ReviewDocumentStatement
                  moveNextFile={moveNextFile}
                  currentDocument={currentDocument}
                  currentFileIndex={currentFileIndex}
                  acceptDocument={() => acceptDocument(needList)}
                  rejectDocument={(rejectMessage: string) =>
                    rejectDocument(needList, rejectMessage)
                  }
                  fileViewd={fileViewd}
                  documentViewLoading={loading || acceptRejectLoading}
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
