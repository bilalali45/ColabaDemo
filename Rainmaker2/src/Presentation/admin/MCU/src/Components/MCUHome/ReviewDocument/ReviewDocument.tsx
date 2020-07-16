import React, { useEffect, useCallback, useState } from "react";
import { useHistory, useLocation } from "react-router-dom";
import { Http } from "rainsoft-js";
import Axios from "axios";

import { ReviewDocumentHeader } from "./ReviewDocumentHeader/ReviewDocumentHeader";
import { ReviewDocumentStatement } from "./ReviewDocumentStatement/ReviewDocumentStatement";
import { DocumentView } from "./../../../Shared/DocumentView";
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

  const tenantId = LocalDB.getTenantId()

  const history = useHistory();
  const location = useLocation();
  const { state } = location;

  const goBack = () => {
    history.push("/needlist");
  };

  const [documentParams, setDocumentParams] = useState<DocumentParamsType>({
    blob: new Blob(),
    filePath: "",
    fileType: "",
  });

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
        setLoading(true);

        // const http = new Http();

        // const response = (await http.get(
        //   NeedListEndpoints.GET.documents.view(
        //     id,
        //     requestId,
        //     docId,
        //     fileId,
        //     tenantId
        //   )
        // )) as any;

        const response = await Axios.get('https://alphamaingateway.rainsoftfn.com/api/documentmanagement/document/view', {
          params,
          responseType: 'arraybuffer',
          headers: {
            Authorization: `Bearer ${LocalDB.getAuthToken()}`
          }
        })

        const fileType: string = response.headers["content-type"];

        const documentBlob = new Blob([response.data], {
          type: fileType,
        });

        // URL required to view the document
        const filePath = URL.createObjectURL(documentBlob);

        setDocumentParams({
          blob: documentBlob,
          filePath,
          fileType: fileType.replace("image/", "").replace("application/", ""),
        });
      } catch (error) {
        setLoading(false);
      } finally {
        setLoading(false);
      }
    },
    [setDocumentParams]
  );

  const nextDocument = useCallback(() => {
    if (navigationIndex === documentList1.length - 1) return;

    const doc: NeedListDocumentType = documentList1[navigationIndex + 1];

    setCurrentDocument(() => documentList1[navigationIndex + 1]);
    setNavigationIndex(() => navigationIndex + 1);

    getDocumentForView(doc.id, doc.requestId, doc.docId, doc.files[0].id, 1);
  }, [navigationIndex, documentList1, getDocumentForView]);

  const previousDocument = useCallback(() => {
    if (navigationIndex === 0) return;

    const doc: NeedListDocumentType = documentList1[navigationIndex - 1];

    const { id, requestId, docId, files } = doc

    setCurrentFileIndex(() => 0)
    setCurrentDocument(() => documentList1[navigationIndex - 1]);
    setNavigationIndex(() => navigationIndex - 1);

    getDocumentForView(id, requestId, docId, files[0].id, tenantId);
  }, [navigationIndex, documentList1, getDocumentForView, tenantId]);

  const moveNextFile = useCallback(async (index: number) => {
    if (index === currentFileIndex) return

    if (currentDocument) {
      const { id, requestId, docId, files } = currentDocument

      setCurrentFileIndex(index)

      getDocumentForView(id, requestId, docId, files[index].id, tenantId)
    }
  }, [setCurrentFileIndex, getDocumentForView, currentDocument, currentFileIndex, tenantId])

  useEffect(() => {
    if (!!location.state) {
      try {
        const { documentList, currentDocumentIndex } = state as any;
        const doc: NeedListDocumentType = documentList[currentDocumentIndex];

        if (!!documentList && documentList.length) {
          setNavigationIndex(currentDocumentIndex);
          setDocumentList1(() => documentList);
          setCurrentDocument(() => documentList[currentDocumentIndex]);

          const { id, requestId, docId, files } = doc

          getDocumentForView(
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
  }, [state, getDocumentForView, location.state, tenantId]);

  return (
    <div
      id="ReviewDocument"
      data-component="ReviewDocument"
      className="review-document"
    >
      <ReviewDocumentHeader
        buttonsEnabled={!loading}
        onClose={goBack}
        nextDocument={nextDocument}
        previousDocument={previousDocument}
      />
      <div className="review-document-body">
        <div className="row">
          <div className="review-document-body--content col-md-8">
            {!!currentDocument && currentDocument.files.length && (
              <DocumentView
                loading={loading}
                filePath={documentParams.filePath}
                fileType={documentParams.fileType}
                clientName={currentDocument.files[currentFileIndex || 0].clientName}
              />
            )}
          </div>
          {/* review-document-body--content */}
          <aside className="review-document-body--aside col-md-4">
            <ReviewDocumentStatement
              moveNextFile={moveNextFile}
              currentDocument={!!currentDocument ? currentDocument : null}
              files={
                !!currentDocument && currentDocument.files.length
                  ? currentDocument.files
                  : []
              }
            />
          </aside>
          {/* review-document-body--aside */}
        </div>
      </div>
      {/* review-document-body */}
    </div>
  );
};
