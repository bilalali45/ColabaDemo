import React, { useState, useEffect, useRef } from "react";
import { Http } from "rainsoft-js";

import { NeedListEndpoints } from "../../../../../Store/endpoints/NeedListEndpoints";
import { SVGeditFile } from "../../../../../Shared/SVG";
import { DateTimeFormat } from "../../../../../Utils/helpers/DateFormat";

export const DocumentSnipet = ({
  index,
  moveNextFile,
  clientName,
  mcuName,
  id,
  requestId,
  docId,
  fileId,
  currentFileIndex,
  uploadedOn,
  username,
  allowFileRenameMCU
}: {
  index: number,
  moveNextFile: (index: number, fileId: string, clientName: string) => void
  clientName: string;
  id: string
  requestId: string
  docId: string
  fileId: string
  mcuName: string
  currentFileIndex: number
  uploadedOn: string
  username: string
  allowFileRenameMCU: (filename: string, fileId: string) => boolean
}) => {
  const [editingModeEnabled, setEditingModeEnabled] = useState(false);
  const [renameMCUName, setRenameMCUName] = useState("");
  const [filenameUnique, setFilenameUnique] = useState(true)
  const inputRef = useRef<HTMLInputElement | null>(null)

  const getFileExtension = (fileName: string) => fileName.substring(fileName.lastIndexOf('.'))

  const getFileNameWithoutExtension = (fileName: string) => fileName.substring(0, fileName.lastIndexOf("."))

  const setInputValue = (event: any) => {
    event.stopPropagation()

    setEditingModeEnabled(() => true);

    const filenameWithoutExtension = getFileNameWithoutExtension(renameMCUName) || getFileNameWithoutExtension(mcuName || clientName)

    if (renameMCUName !== "") {
      setRenameMCUName(getFileNameWithoutExtension(renameMCUName));
    } else {
      setRenameMCUName(filenameWithoutExtension)
    }
  };

  const cancelEdit = (event?: any) => {
    if (event) {
      event.stopPropagation()
    }

    // if (!filenameUnique) return

    if (renameMCUName !== "") {
      const fileExtension = getFileExtension(mcuName || clientName)

      setRenameMCUName(() => `${renameMCUName.trim()}${fileExtension}`) // This will keep name persistant on edit / cacnel again and again
    } else {
      setRenameMCUName(() => "") //This will bring either mcuName or clientName with file extension
    }

    if (!filenameUnique) {
      setRenameMCUName(mcuName || clientName)

      setFilenameUnique(true)
    }

    setEditingModeEnabled(false)
  }

  const renameDocumentMCU = async (newName: string, event?: any, onBlur: boolean = false) => {
    if (event) {
      event.stopPropagation()
      event.preventDefault()
    }

    if (newName) {
      //lets check if new filename without extension is not equal to other files in this document
      const filenameAllowed = allowFileRenameMCU(newName, fileId)

      if (filenameAllowed) {
        setFilenameUnique(() => true)
        setEditingModeEnabled(() => false)

        const fileExtension = getFileExtension(mcuName || clientName)

        const newNameWithFileExtension = `${newName}${fileExtension}`

        setRenameMCUName(() => newNameWithFileExtension)

        try {
          const data = { id, requestId, docId, fileId, newName: newNameWithFileExtension }

          const http = new Http()

          // 1. This will prevent API call
          // 2. This will not make unnecessary Rename Logs in BE
          if (mcuName === data.newName || clientName === data.newName) {
            return
          }

          await http.post(NeedListEndpoints.POST.documents.renameMCU(), {
            ...data
          })
        } catch (error) {
          //swallod error because it shold not update

          // alert('something went wrong while updating file name')
        }

      } else {
        // 1. We need to check if renaming being triggered by onBlur event
        // 2. We will simply cancel edit and will bring back values from mcuName or ClientName
        // 3. We will only fallback here if there is filename conflict else we will save filename on Blur
        if (onBlur) {
          setFilenameUnique(() => true)
          setEditingModeEnabled(() => false)
          setRenameMCUName(() => "")
        } else
          setFilenameUnique(() => false)
        inputRef.current?.focus()
      }
    }
  }

  const moveNext = (event: any) => {
    event.stopPropagation()

    moveNextFile(index, fileId, clientName)
  }

  const onKeyDown = (event: React.KeyboardEvent<HTMLInputElement>, newValue: string) => {
    if (event.key === 'Enter') {
      renameDocumentMCU(newValue)
    }
  }

  const eventBubblingHandler = (event: React.MouseEvent<HTMLDivElement | HTMLButtonElement, MouseEvent>) => {
    switch (event.currentTarget.id) {
      case 'moveNext':
        return moveNext(event)
      case 'rename':
        return renameDocumentMCU(renameMCUName.trim(), event)
      case 'enableMode':
        return setInputValue(event)
      case 'cancel':
        return cancelEdit(event)
    }
  }

  const onBlur = (newValue: string, event: any) => {
    renameDocumentMCU(newValue, event, true)
  }

  const onDoubleClick = (event: any) => {
    if (editingModeEnabled) return

    setInputValue(event)
  }

  useEffect(() => {
    if (editingModeEnabled) {
      inputRef.current?.focus()
    }
  }, [editingModeEnabled])

  return (
    <div onDoubleClick={(event) => onDoubleClick(event)} className={`document-snipet ${index === currentFileIndex && 'focus'} ${editingModeEnabled && 'edit'}`} style={{ cursor: 'pointer' }} id="moveNext" onClick={eventBubblingHandler}>
      <div className="document-snipet--left">
        <div className="document-snipet--input-group">
          {!!editingModeEnabled ? (
            <React.Fragment>
              <input
                onChange={(e) => setRenameMCUName(e.target.value.replace(/[^a-zA-Z0-9- ]/g, ''))}
                type="text"
                size={38}
                value={renameMCUName}
                onClick={event => event.stopPropagation()}
                onBlur={(event) => onBlur(renameMCUName.trim(), event)}
                onKeyDown={(event) => onKeyDown(event, renameMCUName.trim())}
                ref={inputRef}
                maxLength={255}
                className={`${!filenameUnique && 'error'}`}
              />
            </React.Fragment>
          ) : (
              <p title={renameMCUName || mcuName || clientName}>{renameMCUName || mcuName || clientName}</p>
            )}
        </div>
        <small className="document-snipet--detail">
          {`By ${username} on ${DateTimeFormat(uploadedOn, true)}`}
        </small>
        {!filenameUnique && (<small className="document-snipet--detail error">Filename must be unique</small>)}
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
            <SVGeditFile />
          </button>
        )}
      </div>
    </div>
  );
};
