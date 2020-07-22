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

    setRenameMCUName(filenameWithoutExtension);
  };

  const cancelEdit = (event?: any) => {
    if (event) {
      event.stopPropagation()
    }

    if (!filenameUnique) return

    if (renameMCUName !== "") {
      const fileExtension = getFileExtension(mcuName || clientName)

      setRenameMCUName(`${renameMCUName.trim()}${fileExtension}`) // This will keep name persistant on edit / cacnel again and again
    } else {
      setRenameMCUName("") //This will bring either mcuName or clientName with file extension
    }

    setEditingModeEnabled(false)
  }

  const renameDocumentMCU = async (event?: any) => {
    let newName: string

    try {
      if (event) {
        event.stopPropagation()
      }

      //this condition will cancel API call if field is empty or name is unchanged or equal to mcuname or client name
      //if true this condtion will cancel edit.
      if (renameMCUName == "" || renameMCUName === getFileNameWithoutExtension(mcuName || clientName)) {
        return cancelEdit()
      }

      const fileExtension = getFileExtension(mcuName || clientName)

      newName = `${renameMCUName.trim()}${fileExtension}`

      if (allowFileRenameMCU(renameMCUName.trim(), fileId) === false) { //this condition is false if filename already assigned to some other file
        inputRef.current?.focus()

        !!setFilenameUnique && setFilenameUnique(false)

        return
      }

      !filenameUnique && setFilenameUnique(true)

      const data = { id, requestId, docId, fileId, newName }

      const http = new Http()

      await http.post(NeedListEndpoints.POST.documents.renameMCU(), {
        ...data
      })

      setFilenameUnique(true)
      setRenameMCUName(() => newName)
      setEditingModeEnabled(() => false)
    } catch (error) {
      console.log('error', error)

      alert('Something went wrong. Please try again.')

      setRenameMCUName(() => mcuName || clientName)
      setEditingModeEnabled(false)
    }
  }

  const moveNext = (event: any) => {
    event.stopPropagation()

    moveNextFile(index, fileId, clientName)
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

  const onKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === 'Enter') {
      renameDocumentMCU()
    }
  }

  useEffect(() => {
    setRenameMCUName(mcuName || clientName)
  }, [setRenameMCUName, mcuName, clientName])

  useEffect(() => {
    if (editingModeEnabled) {
      inputRef.current?.focus()
    }
  }, [editingModeEnabled])

  const errorClass = () => {
    return !filenameUnique && 'error';
  }

  return (
    <div className={`document-snipet ${index === currentFileIndex && 'focus'} ${editingModeEnabled && 'edit'}`} style={{ cursor: 'pointer' }} id="moveNext" onClick={eventBubblingHandler}>
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
                onBlur={() => renameDocumentMCU()}
                onKeyDown={onKeyDown}
                ref={inputRef}
                className={`${!filenameUnique && 'error'}`}
              />
            </React.Fragment>
          ) : (
              renameMCUName || mcuName || clientName
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
