import React, { useEffect, useCallback, useState } from "react";
import { DocumentSnipet } from "./DocumentSnipet/DocumentSnipet";
import { NeedListDocumentType, DocumentFileType, FileType } from "../../../../Entities/Types/Types";
import { Http } from "rainsoft-js";
import { NeedListEndpoints } from "../../../../Store/endpoints/NeedListEndpoints";
import Spinner from "react-bootstrap/Spinner";

export const ReviewDocumentStatement = ({
  typeIdAndIdForActivityLogs,
  moveNextFile,
  currentDocument,
  currentFileIndex
}: {
  typeIdAndIdForActivityLogs: (id: string, typeIdOrDocName: string) => void,
  moveNextFile: (index: number, fileId: string, clientName: string) => void
  currentDocument: NeedListDocumentType | null;
  currentFileIndex: number
}) => {
  const [documentFiles, setDocumentFiles] = useState<FileType[]>([])
  const [loading, setLoading] = useState(false)
  const [username, setUsername] = useState('')

  const getDocumentFiles = useCallback(async (currentDocument: NeedListDocumentType) => {
    try {
      setLoading(true)

      const { id, requestId, docId } = currentDocument

      const http = new Http()

      const { data } = await http.get<DocumentFileType[]>(NeedListEndpoints.GET.documents.files(id, requestId, docId))

      const { typeId, docName, files, userName } = data[0]

      typeIdAndIdForActivityLogs(id, typeId || docName)

      setDocumentFiles(files)
      setUsername(userName)
      setLoading(false)
    } catch (error) {
      console.log(error)

      setLoading(false)

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
      {!!loading ? (
        <div className="loader-widget" style={{ height: '100vh', justifyContent: 'center', alignItems: 'flex-start', display: 'flex' }}>
          <Spinner animation="border" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        </div>
      ) : (
          <section className="document-statement--body">
            <h3>Documents</h3>
            {!!documentFiles && documentFiles.length ?
              documentFiles.map((file, index) => <DocumentSnipet
                key={index}
                index={index}
                moveNextFile={moveNextFile}
                id={currentDocument?.id!}
                requestId={currentDocument?.requestId!}
                docId={currentDocument?.docId!}
                fileId={file.fileId}
                mcuName={file.mcuName}
                clientName={file.clientName}
                currentFileIndex={currentFileIndex}
                uploadedOn={file.fileUploadedOn}
                username={username}
              />) : (
                <span>No file submitted yet</span>
              )}
            <hr />
          </section>
        )}
    </div>
  );
};
