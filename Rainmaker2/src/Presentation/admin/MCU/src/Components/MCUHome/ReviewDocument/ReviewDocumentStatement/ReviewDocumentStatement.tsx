import React, { useEffect, useCallback, useState } from "react";
import { DocumentSnipet } from "./DocumentSnipet/DocumentSnipet";
import { NeedListDocumentType, NeedListDocumentFileType, DocumentFileType, FileType } from "../../../../Entities/Types/Types";
import { Http } from "rainsoft-js";
import { NeedListEndpoints } from "../../../../Store/endpoints/NeedListEndpoints";

export const ReviewDocumentStatement = ({
  typeIdAndIdForActivityLogs,
  moveNextFile,
  currentDocument,
}: {
  typeIdAndIdForActivityLogs: (id: string, typeIdOrDocName: string) => void,
  moveNextFile: (index: number) => void
  currentDocument: NeedListDocumentType | null;
}) => {
  const [documentFiles, setDocumentFiles] = useState<FileType[]>([])

  const getDocumentFiles = useCallback(async (currentDocument: NeedListDocumentType) => {
    try {
      const { id, requestId, docId } = currentDocument

      const http = new Http()

      const { data } = await http.get<DocumentFileType[]>(NeedListEndpoints.GET.documents.files(id, requestId, docId))

      const { typeId, docName, files } = data[0]

      typeIdAndIdForActivityLogs(id, typeId || docName)

      setDocumentFiles(files)
    } catch (error) {
      console.log(error)
      alert('Something went wrong while getting files for document. Please try again.')
    }
  }, [setDocumentFiles])

  useEffect(() => {
    if (currentDocument) {
      getDocumentFiles(currentDocument)
    }
  }, [getDocumentFiles, currentDocument])

  return (
    <div
      id="ReviewDocumentStatement"
      data-component="ReviewDocumentStatement"
      className="document-statement"
    >
      <header className="document-statement--header">
        <h2>{currentDocument?.docName}</h2>
      </header>
      <section className="document-statement--body">
        <h3>Documents</h3>
        {/* <DocumentSnipet status="active" /> */}
        {!!documentFiles && documentFiles.length ?
          documentFiles.map((file, index) => <DocumentSnipet
            index={index}
            moveNextFile={moveNextFile}
            id={currentDocument?.id!}
            requestId={currentDocument?.requestId!}
            docId={currentDocument?.docId!}
            fileId={file.fileId}
            mcuName={file.mcuName}
            clientName={file.clientName}
          />) : (
            <span>No file submitted yet</span>
          )}

        <hr />

        {/* <div className="clearfix">
          <span className="float-right activity-logs-icon">
            <em className="icon-letter"></em>
          </span>
          <h3>Activity Logs</h3>
        </div> */}
      </section>
    </div>
  );
};
