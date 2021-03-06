import React, { useContext, useEffect, useRef, useState } from 'react'
import { CurrentInView } from '../../../Models/CurrentInView';
import { SelectedFile } from '../../../Models/SelectedFile';
import DocumentActions from '../../../Store/actions/DocumentActions';
import { DocumentActionsType } from '../../../Store/reducers/documentsReducer';
import { ViewerActionsType } from '../../../Store/reducers/ViewerReducer';
import { Store } from '../../../Store/Store';
import { LocalDB } from '../../../Utilities/LocalDB';
import { ViewerTools } from '../../../Utilities/ViewerTools';

export const RenameFile = ({
  editMode,
  isWorkBenchFile

}
  : {
    editMode: Function;
    isWorkBenchFile: boolean
  }
) => {

  const [renameMCUName, setRenameMCUName] = useState('');
  const [filenameUnique, setFilenameUnique] = useState(true);
  const [validFilename, setValidFilename] = useState(true);
  const [filenameEmpty, setFilenameEmpty] = useState(false);
  const [mcuNamePreviousName, setMCUNamePreviousName] = useState('');
  const [itemViewd, setItemViewd] = useState(false);
  const inputRef = useRef<HTMLInputElement | null>(null);
  const [mcuNamesUpdated, setMcuNamesUpdated] = useState<
    { fileId: string; mcuName: string }[]
  >([]);
  const { state, dispatch } = useContext(Store);
  const { currentDoc, importedFileIds,documentItems }: any = state.documents;
  const { currentFile, selectedFileData, isRenameEditMode }: any = state.viewer;
  const regex = /[^a-zA-Z0-9- ]/g; // This regex will allow only alphanumeric values with - and spaces.
  let loanApplicationId = LocalDB.getLoanAppliationId();

  useEffect(() => {
    if (currentFile)
      setInputValue()
  }, [])


  const getFileExtension = (fileName: string) =>
    fileName.substring(fileName.lastIndexOf('.'));

  const getFileNameWithoutExtension = (fileName: string) =>
    fileName.substring(0, fileName.lastIndexOf('.'));

  const setInputValue = () => {

    setRenameMCUName("")
    const filenameWithoutExtension =
      getFileNameWithoutExtension(renameMCUName) ||
      getFileNameWithoutExtension(currentFile.name);

    if (renameMCUName !== '') {
      setRenameMCUName(getFileNameWithoutExtension(renameMCUName).trim());
    } else {
      setRenameMCUName(filenameWithoutExtension.trim());
    }

    setMcuNamesUpdated(
      currentDoc?.files.map((file: any) => {
        return {
          fileId: file.fileId,
          mcuName:
            file.mcuName === ''
              ? getFileNameWithoutExtension(file.clientName)
              : getFileNameWithoutExtension(file.mcuName)
        };
      })
    );
  };


  const getMcuNameUpdated = (fileId: string): string => {
    const item = mcuNamesUpdated.find((item) => item.fileId === fileId);

    return !!item ? item.mcuName : '';
  };


  const allowFileRenameMCU = (
    filename: string,
    fileId: string,
    addToList: boolean = true
  ): boolean => {
    const clonedArray = [...mcuNamesUpdated];

    // Why filter? because we don't want to check filename of current file being renamed
    const mcuNameAlreadyInList = clonedArray
      .filter((file) => file.fileId !== fileId)
      .some((file) => {
        return file.mcuName.trim() === filename.trim();
      });

    // This condition will make sure we are not saving each value in string
    // addToList === false, means we don't want to save it in List setMcuNamesUpdated
    if (addToList === false) {
      return mcuNameAlreadyInList;
    } else if (mcuNameAlreadyInList) {
      return false;
    }

    const documentFile = clonedArray.find((file) => file.fileId === fileId);

    if (documentFile) {
      documentFile.mcuName = filename;
    }

    setMcuNamesUpdated(() => clonedArray);
    return true;
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
      const filenameAllowed = allowFileRenameMCU(newName, currentFile.id, true);

      if (filenameAllowed) {
        setFilenameUnique(() => true);

        const fileExtension = getFileExtension(currentFile.name);

        const newNameWithFileExtension = `${newName}${fileExtension}`;

        setRenameMCUName(currentFile.name);
        const mcuNameUpdated = getMcuNameUpdated(currentFile.id);

        if (mcuNamePreviousName === `${mcuNameUpdated}${fileExtension}`) {
          return setRenameMCUName(
            () => `${renameMCUName.trim()}${fileExtension}`
          );
          // } else if (mcuNamePreviousName === '' && newNameWithFileExtension === mcuName) {
          //   return setRenameMCUName(name);
          // } else if (
          //   mcuNamePreviousName === '' &&
          //   mcuName === '' &&
          //   newName === clientName
          // ) {
          //   return setRenameMCUName(clientName);
        }

        try {
          let id = currentFile.isWorkBenchFile ? currentFile.id : currentDoc.id
          let fileId = currentFile.fileId
          if(newName !== "" && !allowFileRenameMCU(newName, currentFile?.id, false) && !regex.test(newName)){
            let fileName = selectedFileData.name
              const fileExt = getFileExtension(fileName);
              let selectedFile = new SelectedFile(currentFile.id,newName + fileExt, currentFile.fileId )
            await dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFile});
            let newFile: any = new CurrentInView(
              currentFile.id,
              currentFile.src,
              newNameWithFileExtension,
              currentFile.isWorkBenchFile,
              currentFile.fileId,
  
            );
            dispatch({
              type: ViewerActionsType.SetCurrentFile,
              payload: newFile,
            });
              await DocumentActions.renameDoc(id, currentDoc.requestId, currentDoc.docId, fileId, newName + fileExt)
              setMCUNamePreviousName(() => newNameWithFileExtension);
            setRenameMCUName(() => newNameWithFileExtension);
            
            if (currentFile.isWorkBenchFile) {
              let d = await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
  
            }
            else {
              
               await DocumentActions.getDocumentItems(dispatch, importedFileIds);

            }
            
        }
          
          
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


  const showingNewNameInList = (newName:string)=>{
    ViewerTools.currentFileName = newName
    newName = newName+"|"
        let allDocs = documentItems.map((doc:any)=>{
          doc.files.map((file:any)=>{
            if(file.id === selectedFileData.fileId){
              file.mcuName && file.mcuName.length? file.mcuName = newName : file.clientName = newName
              return file
            }

            return file
          })
          return doc
        })

        dispatch({ type: DocumentActionsType.SetDocumentItems, payload: allDocs });
      }

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
      editMode(false)
      renameDocumentMCU(newValue.trim(), event, true);
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
    editMode(false)
    renameDocumentMCU(newValue.trim(), event, true);
  };


  const validateFilename = (value: string) => {
    setValidFilename(true);
    setFilenameEmpty(false);

    if (value === '') {
      setFilenameEmpty(true);
    }

    !filenameUnique && setFilenameUnique(true);


    if (regex.test(value)) {
      setValidFilename(false);
    }

    // Here we won't be saving name to list insdie ReviewDocumentStatement.tsx, its just for checking
    // We will only save name in list onBlur or onEnter events.
    const filenameAlreadyInList = allowFileRenameMCU(value, currentFile?.id, false);

    if (filenameAlreadyInList) {
      setFilenameUnique(false);
    }

    setRenameMCUName(value);
  };

  useEffect(() => {
    if (isRenameEditMode) {
      inputRef.current?.focus();
    }
  }, [isRenameEditMode]);


  const renderNameError = () => {

    let uniqueNameError = 'File name must be unique';
    let nameEmptyError = 'File name cannot be empty';
    let specialCharsError = 'File name cannot contain any special characters'

    if (filenameEmpty) {
      return (
        <label className="document-snipet--detail error rename-input-error" data-testid="empty-file-name-error">
          {nameEmptyError}
        </label>
      )
    } else if (!filenameUnique) {
      return (
        <label className="document-snipet--detail error rename-input-error" data-testid="unique-file-name-error">
          {uniqueNameError}
        </label>
      )
    } else if (!validFilename) {
      return (
        <label className="document-snipet--detail error rename-input-error" data-testid="special-character-error">
          {specialCharsError}
        </label>
      )
    }
  }

  return (

    <div className="document-snipet--left">
      <div className="document-snipet--input-group rename-input-group">

        <React.Fragment>
          <input
            data-testid="rename-file"
            ref={inputRef}
            className={`${(!filenameUnique || !!filenameEmpty || !validFilename) &&
              'error'
              }`}
            maxLength={250}
            size={38}
            type="text"
            value={renameMCUName}
            onBlur={(event) => onBlur(renameMCUName.trim(), event)}
            onChange={(event) =>{showingNewNameInList(event.target.value); validateFilename(event.target.value)}}
            onClick={(event) => event.stopPropagation()}
            onKeyDown={(event) => onKeyDown(event, renameMCUName.trim())}
          />
        </React.Fragment>


      </div>
      {renderNameError()}
    </div>

  );
}
