import React, { useState, useEffect, useRef } from 'react';
import { Http } from 'rainsoft-js';

import { NeedListEndpoints } from '../../../../../Store/endpoints/NeedListEndpoints';
import { SVGeditFile } from '../../../../../Shared/SVG';
import { datetimeFormatRenameFile } from '../../../../../Utils/helpers/DateFormat';
import { NeedList } from '../../../../../Entities/Models/NeedList';

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
  isRead,
  username,
  allowFileRenameMCU,
  getMcuNameUpdated,
  isCurrent
}: {
  index: number;
  moveNextFile: (index: number, fileId: string, clientName: string) => void;
  clientName: string;
  id: string;
  requestId: string;
  docId: string;
  fileId: string;
  mcuName: string;
  currentFileIndex: number;
  uploadedOn: string;
  isRead: boolean;
  username: string;
  isCurrent: boolean;
  allowFileRenameMCU: (
    filename: string,
    fileId: string,
    addToList?: boolean
  ) => boolean;
  getMcuNameUpdated: (fileId: string) => string;
}) => {
  const [editingModeEnabled, setEditingModeEnabled] = useState(false);
  const [renameMCUName, setRenameMCUName] = useState('');
  const [filenameUnique, setFilenameUnique] = useState(true);
  const [validFilename, setValidFilename] = useState(true);
  const [filenameEmpty, setFilenameEmpty] = useState(false);
  const [mcuNamePreviousName, setMCUNamePreviousName] = useState('');
  const inputRef = useRef<HTMLInputElement | null>(null);

  const getFileExtension = (fileName: string) =>
    fileName.substring(fileName.lastIndexOf('.'));

  const getFileNameWithoutExtension = (fileName: string) =>
    fileName.substring(0, fileName.lastIndexOf('.'));

  const setInputValue = (event: any) => {
    event.stopPropagation();

    setEditingModeEnabled(() => true);

    const filenameWithoutExtension =
      getFileNameWithoutExtension(renameMCUName) ||
      getFileNameWithoutExtension(mcuName || clientName);

    if (renameMCUName !== '') {
      setRenameMCUName(getFileNameWithoutExtension(renameMCUName).trim());
    } else {
      setRenameMCUName(filenameWithoutExtension.trim());
    }
  };

  const renameDocumentMCU = async (
    newName: string,
    event?: any,
    onBlur: boolean = false
  ) => {
    if (event) {
      event.stopPropagation();
      event.preventDefault();
    }

    if (newName) {
      //lets check if new filename without extension is not equal to other files in this document
      const filenameAllowed = allowFileRenameMCU(newName, fileId, true);

      if (filenameAllowed) {
        setFilenameUnique(() => true);
        setEditingModeEnabled(() => false);

        const fileExtension = getFileExtension(mcuName || clientName);

        const newNameWithFileExtension = `${newName}${fileExtension}`;

        const data = {
          id,
          requestId,
          docId,
          fileId,
          newName: newNameWithFileExtension
        };

        const mcuNameUpdated = getMcuNameUpdated(fileId);

        if (mcuNamePreviousName === `${mcuNameUpdated}${fileExtension}`) {
          return setRenameMCUName(
            () => `${renameMCUName.trim()}${fileExtension}`
          );
        } else if (mcuNamePreviousName === '' && data.newName === mcuName) {
          return setRenameMCUName(mcuName || clientName);
        } else if (
          mcuNamePreviousName === '' &&
          mcuName === '' &&
          data.newName === clientName
        ) {
          return setRenameMCUName(clientName);
        }

        try {
          const http = new Http();

          await http.post(NeedListEndpoints.POST.documents.renameMCU(), {
            ...data
          });

          setMCUNamePreviousName(() => newNameWithFileExtension);
          setRenameMCUName(() => newNameWithFileExtension);
        } catch (error) {
          //swallod error because it should not update

          alert('something went wrong while updating file name');
        }
      } else {
        // 1. We need to check if renaming being triggered by onBlur event
        // 3. We will only fallback here if there is filename conflict else we will save filename on Blur
        if (onBlur) {
          setFilenameUnique(() => false);
        } else setFilenameUnique(() => false);
        inputRef.current?.focus();
      }
    }
  };

  const moveNext = (event: any) => {
    event.stopPropagation();
    // getDocumentFiles(currentDocument);
    moveNextFile(index, fileId, clientName);
  };

  const eventBubblingHandler = (
    event: React.MouseEvent<HTMLDivElement | HTMLButtonElement, MouseEvent>
  ) => {
    switch (event.currentTarget.id) {
      case 'moveNext':
        return moveNext(event);
      case 'rename':
        return renameDocumentMCU(renameMCUName.trim(), event);
      case 'enableMode':
        return setInputValue(event);
    }
  };

  const onKeyDown = (
    event: React.KeyboardEvent<HTMLInputElement>,
    newValue: string
  ) => {
    if (event.key === 'Enter') {
      if (
        validFilename === false ||
        filenameUnique === false ||
        filenameEmpty === true
      ) {
        return event.preventDefault();
      }

      renameDocumentMCU(newValue.trim());
    }
  };

  const onBlur = (
    newValue: string,
    event: React.FocusEvent<HTMLInputElement>
  ) => {
    if (
      filenameUnique === false ||
      validFilename === false ||
      filenameEmpty === true
    ) {
      return event.preventDefault();
    }

    renameDocumentMCU(newValue.trim(), event, true);
  };

  const onDoubleClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>
  ) => {
    if (editingModeEnabled) return;

    setInputValue(event);
  };

  const validateFilename = (value: string) => {
    setValidFilename(true);
    setFilenameEmpty(false);

    if (value === '') {
      setFilenameEmpty(true);
    }

    !filenameUnique && setFilenameUnique(true);

    const regex = /[^a-zA-Z0-9- ]/g; // This regex will allow only alphanumeric values with - and spaces.

    if (regex.test(value)) {
      setValidFilename(false);
    }

    // Here we won't be saving name to list insdie ReviewDocumentStatement.tsx, its just for checking
    // We will only save name in list onBlur or onEnter events.
    const filenameAlreadyInList = allowFileRenameMCU(value, fileId, false);

    if (filenameAlreadyInList) {
      setFilenameUnique(false);
    }

    setRenameMCUName(value);
  };

  useEffect(() => {
    if (editingModeEnabled) {
      inputRef.current?.focus();
    }
  }, [editingModeEnabled]);

  return (
    <div
      onDoubleClick={(event) => onDoubleClick(event)}
      className={`document-snipet ${index === currentFileIndex && 'focus'} ${
        editingModeEnabled && 'edit'
        }`}
      style={{ cursor: 'pointer' }}
      id="moveNext"
      onClick={eventBubblingHandler}
    >
      <div className="document-snipet--left">
        <div className="document-snipet--input-group">
          {!!editingModeEnabled ? (
            <React.Fragment>
              <input
                ref={inputRef}
                className={`${
                  (!filenameUnique || !!filenameEmpty || !validFilename) &&
                  'error'
                  }`}
                maxLength={250}
                size={38}
                type="text"
                value={renameMCUName}
                onBlur={(event) => onBlur(renameMCUName.trim(), event)}
                onChange={(event) => validateFilename(event.target.value)}
                onClick={(event) => event.stopPropagation()}
                onKeyDown={(event) => onKeyDown(event, renameMCUName.trim())}
              />
            </React.Fragment>
          ) : (
              <p title={renameMCUName.trim() || mcuName || clientName}>
                {!isCurrent && !isRead && index !== 0 ? <strong>
                  {renameMCUName.trim() || mcuName || clientName}
                </strong>
                  :
                  renameMCUName.trim() || mcuName || clientName
                }
              </p>
            )}
        </div>
        <small className="document-snipet--detail">
          {`By ${username} on ${datetimeFormatRenameFile(uploadedOn)}`}
        </small>
        {!!filenameEmpty && (
          <label className="document-snipet--detail error">
            File name cannot be empty
          </label>
        )}
        {!filenameUnique && (
          <label className="document-snipet--detail error">
            File name must be unique
          </label>
        )}
        {!validFilename && (
          <label className="document-snipet--detail error">
            File name cannot contain any special characters
          </label>
        )}
      </div>
      <div className="document-snipet--right">
        {!editingModeEnabled && (
          <button
            className="document-snipet-btn-edit"
            id="enableMode"
            onClick={eventBubblingHandler}
          >
            <SVGeditFile />
          </button>
        )}
      </div>
    </div>
  );
};
