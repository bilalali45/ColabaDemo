import React, { useEffect, useCallback, useState } from "react";
import { useHistory, useLocation } from "react-router-dom";
import axios from "axios";

import { ReviewDocumentHeader } from "./ReviewDocumentHeader/ReviewDocumentHeader";
import { ReviewDocumentStatement } from "./ReviewDocumentStatement/ReviewDocumentStatement";
import { DocumentView } from "./../../../Shared/DocumentView";
import {
  NeedListDocumentType,
  DocumentParamsType,
} from "../../../Entities/Types/Types";

export const ReviewDocument = () => {
  const [currentDocument, setCurrentDocument] = useState<
    NeedListDocumentType
  >();
  const [documentList1, setDocumentList1] = useState<NeedListDocumentType[]>(
    []
  );
  const [navigationIndex, setNavigationIndex] = useState(0);

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
    async (
      documentList: NeedListDocumentType[],
      currentDocumentIndex: number
    ) => {
      setCurrentDocument(() => documentList[currentDocumentIndex]);
      setDocumentList1(() => documentList);

      const document = documentList[currentDocumentIndex];
      const { id, requestId, docId, files } = document;

      const params = {
        id,
        requestId,
        docId,
        fileId: files[0].id,
        tenantId: 1,
      };

      try {
        const response = await axios.get(
          "https://alphamaingateway.rainsoftfn.com/api/documentmanagement/document/view",
          {
            responseType: "arraybuffer",
            params: { ...params },
            headers: {
              Authorization:
                "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNQ1UiLCJVc2VyUHJvZmlsZUlkIjoiMSIsIlVzZXJOYW1lIjoicmFpbnNvZnQiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicmFpbnNvZnQiLCJGaXJzdE5hbWUiOiJTeXN0ZW0iLCJMYXN0TmFtZSI6IkFkbWluaXN0cmF0b3IiLCJFbXBsb3llZUlkIjoiMSIsImV4cCI6MTU5NDcyNDQ3NCwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.9lfp9MZkGcqrIGSQgS9uOByF2TTGArlv32l-62Ozy08",
            },
          }
        );

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
      } catch (error) {}
    },
    [setDocumentParams]
  );

  const nextDocument = useCallback(() => {
    if (navigationIndex === documentList1.length - 1) return;

    setCurrentDocument(() => documentList1[navigationIndex + 1]);
    setNavigationIndex(() => navigationIndex + 1);
  }, [navigationIndex]);

  const previousDocument = useCallback(() => {
    if (navigationIndex === 0) return;

    setCurrentDocument(() => documentList1[navigationIndex - 1]);
    setNavigationIndex(() => navigationIndex - 1);
  }, [navigationIndex]);

  useEffect(() => {
    if (!!location.state) {
      try {
        const { documentList, currentDocumentIndex } = state as any;

        if (!!documentList && documentList.length) {
          setNavigationIndex(currentDocumentIndex);
          getDocumentForView(documentList, currentDocumentIndex);
        }
      } catch (error) {
        console.log("error", error);

        alert("Something went wrong. Please try again.");
      }
    }
  }, [state, getDocumentForView, location.state]);

  return (
    <div
      id="ReviewDocument"
      data-component="ReviewDocument"
      className="review-document"
    >
      <ReviewDocumentHeader
        onClose={goBack}
        nextDocument={nextDocument}
        previousDocument={previousDocument}
      />
      <div className="review-document-body">
        <div className="row">
          <div className="review-document-body--content col-md-8">
            <DocumentView
              loading={!documentParams.filePath}
              filePath={documentParams.filePath}
              fileType={documentParams.fileType}
            />
          </div>
          {/* review-document-body--content */}
          <aside className="review-document-body--aside col-md-4">
            <ReviewDocumentStatement
              documentName={!!currentDocument ? currentDocument.docName : ""}
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
