import React, { useEffect, useCallback, useState } from "react";
import { useHistory, useLocation } from "react-router-dom";
import { Http } from "rainsoft-js";
import Axios from "axios";
import { DocumentView } from 'rainsoft-rc';
import _, { indexOf } from 'lodash'

import { ReviewDocumentHeader } from "./ReviewDocumentHeader/ReviewDocumentHeader";
import { ReviewDocumentStatement } from "./ReviewDocumentStatement/ReviewDocumentStatement";
import {
  NeedListDocumentType,
  DocumentParamsType,
} from "../../../Entities/Types/Types";
import { NeedListEndpoints } from "../../../Store/endpoints/NeedListEndpoints";
import { LocalDB } from "../../../Utils/LocalDB";

export const ReviewDocument = () => {
  const [currentDocument, setCurrentDocument] = useState<
    NeedListDocumentType
  >();
  const [documentList1, setDocumentList1] = useState<NeedListDocumentType[]>(
    []
  );
  const [navigationIndex, setNavigationIndex] = useState(0);
  const [loading, setLoading] = useState(true);
  const [currentFileIndex, setCurrentFileIndex] = useState(0)
  const [documentDetail, setDocumentDetail] = useState(false)
  const [typeIdId, setTypeIdId] = useState<{ id: string | null, typeId: string | null }>({
    id: null,
    typeId: null
  })

  const tenantId = LocalDB.getTenantId()

  const history = useHistory();
  const location = useLocation();
  const { state } = location;

  const goBack = () => {
    history.push("/needlist");
  };

  const [blobData, setBlobData] = useState<any>();

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
            Authorization: `Bearer ${LocalDB.getAuthToken()}`
          }
        })

        setBlobData(response)
        setLoading(false)
      } catch (error) {
        setLoading(false)

        alert('Something went wrong while fetching document/file from server.')
      }
    },
    []
  );

  const nextDocument = useCallback(() => {
    const indexes = _.keys(_.pickBy(documentList1, { status: 'Pending review' }))

    const indexOfReivew = indexes.findIndex(value => Number(value) > navigationIndex)

    if (indexOfReivew === -1) return //No review document found

    const doc: NeedListDocumentType = documentList1[indexOfReivew];
    const { id, requestId, docId, files } = doc

    setCurrentDocument(() => documentList1[indexOfReivew]);
    setNavigationIndex(() => indexOfReivew);
    setCurrentFileIndex(0)
    setTypeIdId({ id: null, typeId: null })

    !!files && files.length > 0 && getDocumentForView(id, requestId, docId, files[0].id, 1);
  }, [navigationIndex, documentList1, getDocumentForView]);

  const previousDocument = useCallback(() => {
    const indexes = _.reverse(_.keys(_.pickBy(documentList1, { status: 'Pending review' })))

    console.log('navIndex', navigationIndex)

    const indexOfReivew = navigationIndex === 1 ? 0 : indexes.findIndex(value => Number(value) < navigationIndex)

    if (indexOfReivew === -1) return //No review document found

    const doc: NeedListDocumentType = documentList1[indexOfReivew];

    const { id, requestId, docId, files } = doc

    setCurrentDocument(() => documentList1[indexOfReivew]);
    setNavigationIndex(() => indexOfReivew);
    setCurrentFileIndex(() => 0)
    setTypeIdId({ id: null, typeId: null })

    !!files && files.length > 0 && getDocumentForView(id, requestId, docId, files[0].id, tenantId);
  }, [navigationIndex, documentList1, getDocumentForView, tenantId]);

  const moveNextFile = useCallback(async (index: number) => {
    if (index === currentFileIndex) return

    if (currentDocument) {
      const { id, requestId, docId, files } = currentDocument

      const tenantId = LocalDB.getTenantId()

      setCurrentFileIndex(() => index)
      setBlobData(() => null)

      !loading && currentFileIndex > 0 && await getDocumentForView(id, requestId, docId, files[index].id, tenantId)
    }
  }, [loading, setCurrentFileIndex, getDocumentForView, currentDocument, currentFileIndex])

  const setTypeIdAndIdForActivityLogs = useCallback((id, typeIdOrDocName) => {
    setTypeIdId({ id, typeId: typeIdOrDocName })
  }, [])

  useEffect(() => {
    if (!!location.state) {
      try {
        const { documentList, currentDocumentIndex, documentDetail } = state as any;
        const doc: NeedListDocumentType = documentList[currentDocumentIndex];

        if (!!documentList && documentList.length) {
          setNavigationIndex(currentDocumentIndex);
          setDocumentList1(() => documentList);
          setCurrentDocument(() => documentList[currentDocumentIndex]);
          setDocumentDetail(() => documentDetail)

          const { id, requestId, docId, files } = doc

          !loading && !!files && !!files.length && files.length > 0 && getDocumentForView(
            id,
            requestId,
            docId,
            files[0].id,
            tenantId
          );
        }
      } catch (error) {
        console.log("error", error);

        alert("Something went wrong. Please try again.");
      }
    }
  }, [getDocumentForView, state, location.state, tenantId]);

  return (
    <div
      id="ReviewDocument"
      data-component="ReviewDocument"
      className="review-document"
    >
      <ReviewDocumentHeader
        id={typeIdId.id}
        typeId={typeIdId.typeId}
        documentDetail={documentDetail}
        buttonsEnabled={!loading}
        onClose={goBack}
        nextDocument={nextDocument}
        previousDocument={previousDocument}
      />

      <div className="review-document-body">
        <div className="row">
          <div className="review-document-body--content col-md-8">
            {!!currentDocument && currentDocument.files && currentDocument.files.length ? (
              <div className="doc-view-mcu">
              <DocumentView
                loading={loading}
                id={currentDocument.id}
                requestId={currentDocument.requestId}
                docId={currentDocument.docId}
                fileId={currentDocument.files[currentFileIndex || 0].id}
                submittedDocumentCallBack={getDocumentForView}
                tenantId={tenantId}
                clientName={currentDocument.files[currentFileIndex || 0].clientName}
                blobData={blobData}
                hideViewer={() => { }}
              />
              </div>

            ) : (
                <h3>No preview available</h3>
              )}

          </div>
          {/* review-document-body--content */}
          <aside className="review-document-body--aside col-md-4">
            <ReviewDocumentStatement
              typeIdAndIdForActivityLogs={setTypeIdAndIdForActivityLogs}
              moveNextFile={moveNextFile}
              currentDocument={!!currentDocument ? currentDocument : null}
            />
          </aside>
          {/* review-document-body--aside */}
        </div>
      </div>
      {/* review-document-body */}
    </div>
  );
};
