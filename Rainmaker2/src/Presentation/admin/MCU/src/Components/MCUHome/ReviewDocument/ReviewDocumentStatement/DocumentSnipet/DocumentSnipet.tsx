import React, { useState, useEffect } from "react";
import { Http } from "rainsoft-js";
import { NeedListEndpoints } from "../../../../../Store/endpoints/NeedListEndpoints";

export const DocumentSnipet = ({
  index,
  moveNextFile,
  clientName,
  active,
  mcuName,
  id,
  requestId,
  docId,
  fileId
}: {
  index: number,
  moveNextFile: (index: number) => void
  clientName: string;
  id: string
  requestId: string
  docId: string
  fileId: string
  mcuName: string
  active?: string;
}) => {
  const [editingModeEnabled, setEditingModeEnabled] = useState(false);
  const [renameMCUName, setRenameMCUName] = useState("");

  const getFileExtension = (fileName: string) => fileName.substring(fileName.lastIndexOf('.'))

  const getFileNameWithoutExtension = (fileName: string) => fileName.substring(0, fileName.lastIndexOf("."))

  const setInputValue = (event: any) => {
    event.stopPropagation()

    setEditingModeEnabled(() => true);

    const filenameWithoutExtension = getFileNameWithoutExtension(renameMCUName) || getFileNameWithoutExtension(mcuName || clientName)

    setRenameMCUName(filenameWithoutExtension);
  };

  const cancelEdit = (event?: any) => {
    if (event) {
      event.stopPropagation()
    }

    if (renameMCUName !== "") {
      const fileExtension = getFileExtension(mcuName || clientName)

      setRenameMCUName(`${renameMCUName}${fileExtension}`) // This will keep name persistant on edit / cacnel again and again
    } else {
      setRenameMCUName("") //This will bring either mcuName or clientName with file extension
    }

    setEditingModeEnabled(false)
  }

  const renameDocumentMCU = async (event: any) => {
    try {
      event.stopPropagation()

      //this condition will cancel API call if field is empty or name is unchanged or equal to mcuname or client name
      //if true this condtion will cancel edit.
      if (renameMCUName == "" || renameMCUName === getFileNameWithoutExtension(mcuName || clientName)) {
        return cancelEdit()
      }

      const fileExtension = getFileExtension(mcuName || clientName)
      const newName = `${renameMCUName}${fileExtension}`

      const data = { id, requestId, docId, fileId, newName }

      const http = new Http()
      await http.post(NeedListEndpoints.POST.documents.renameMCU(), {
        ...data
      })

      setRenameMCUName(() => newName)
      setEditingModeEnabled(() => false)
    } catch (error) {
      console.log('error', error)
      setRenameMCUName(mcuName || clientName)
      setEditingModeEnabled(false)
    }
  }

  const moveNext = (event: any) => {
    event.stopPropagation()
    moveNextFile(index)
  }

  const eventBubblingHandler = (event: React.MouseEvent<HTMLDivElement | HTMLButtonElement, MouseEvent>) => {
    switch (event.currentTarget.id) {
      case 'moveNext':
        return moveNext(event)
      case 'rename':
        return renameDocumentMCU(event)
      case 'enableMode':
        return setInputValue(event)
      case 'cancel':
        return cancelEdit(event)
    }
  }

  useEffect(() => {
    setRenameMCUName(mcuName || clientName)
  }, [setRenameMCUName, mcuName, clientName])

  return (
    <div className={`document-snipet ${!!active ? "active" : ""}`} style={{ cursor: 'pointer' }} id="moveNext" onClick={eventBubblingHandler}>
      <div className="document-snipet--left">
        <div className="document-snipet--input-group">
          {!!editingModeEnabled ? (
            <React.Fragment>
              <input
                onChange={(e) => setRenameMCUName(e.target.value.replace(/[^a-zA-Z0-9-]/g, ''))}
                type="text"
                size={38}
                value={renameMCUName}
                onClick={event => event.stopPropagation()}
              />
              <button className="document-snipet-btn-ok" id="rename" onClick={eventBubblingHandler}>
                <em className="zmdi zmdi-check"></em>
              </button>
            </React.Fragment>
          ) : (
              renameMCUName || mcuName || clientName
            )}
        </div>
        <small className="document-snipet--detail">
          By Richard Glenn Randall on Apr 17, 2020 at 4:31 AM
        </small>
      </div>
      <div className="document-snipet--right">
        {!!editingModeEnabled && (
          <button
            id="cancel"
            className="document-snipet-btn-cancel"
            onClick={eventBubblingHandler}
          >
            <em className="zmdi zmdi-close"></em>
          </button>
        )}
        {!editingModeEnabled && (
          <button className="document-snipet-btn-edit" id="enableMode" onClick={eventBubblingHandler}>
            <em className="icon-edit2"></em>
          </button>
        )}
      </div>
    </div>
  );
};
