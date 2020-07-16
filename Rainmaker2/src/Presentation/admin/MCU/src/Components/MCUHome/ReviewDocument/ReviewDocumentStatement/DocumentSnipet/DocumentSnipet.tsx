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

  const getFileNameOrExtension = (returnType?: string) => {
    const variableTobeUsed = mcuName || clientName

    if (returnType === 'extension') {
      const extensionOfFile = variableTobeUsed.substring(variableTobeUsed.lastIndexOf('.'))
      return extensionOfFile
    }

    const fileNameWithoutExtension = variableTobeUsed.substring(0, variableTobeUsed.lastIndexOf("."))

    return fileNameWithoutExtension
  }

  const setInputValue = (event: any) => {
    event.stopPropagation()

    setEditingModeEnabled(() => true);

    const fileNameWithoutExtension = getFileNameOrExtension()

    setRenameMCUName(fileNameWithoutExtension);
  };

  const cancelEdit = (event: any) => {
    event.stopPropagation()

    setRenameMCUName("")
    setEditingModeEnabled(false)
  }

  const renameDocumentMCU = async (event: any) => {
    try {
      event.stopPropagation()

      if (renameMCUName === "") return

      const fileExtension = getFileNameOrExtension('extension')

      const http = new Http()

      const newName = `${renameMCUName}${fileExtension}`

      const data = { id, requestId, docId, fileId, newName }

      await http.post(NeedListEndpoints.POST.documents.renameMCU(), {
        ...data
      })

      setRenameMCUName(() => newName)
      setEditingModeEnabled(() => false)
    } catch (error) {
      console.log('error', error)
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
                onChange={(e) => setRenameMCUName(e.target.value)}
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
