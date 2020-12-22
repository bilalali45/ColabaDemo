import React, { useContext, useEffect, useRef, useState } from "react";
import { CurrentInView } from "../../../../Models/CurrentInView";
import { RenameFile } from "../../../../shared/Components/Assets/RenameFile";
import { Endpoints } from "../../../../Store/endpoints/Endpoints";
import { ViewerActionsType } from "../../../../Store/reducers/ViewerReducer";
import { Store } from "../../../../Store/Store";
import { AnnotationActions } from "../../../../Utilities/AnnotationActions";
import { datetimeFormatRenameFile } from "../../../../Utilities/helpers/DateFormat";

export const ViewerHeader = () => {
  const [editingModeEnabled, setEditingModeEnabled] = useState(false);
  const [fileName, setFileName] = useState("");
  const [filenameUnique, setFilenameUnique] = useState(true);
  const [validFilename, setValidFilename] = useState(true);
  const [filenameEmpty, setFilenameEmpty] = useState(false);
  const [mcuNamePreviousName, setMCUNamePreviousName] = useState("");
  const [itemViewd, setItemViewd] = useState(false);
  
  const inputRef = useRef<HTMLInputElement | null>(null);
  
  
  const { state, dispatch } = useContext(Store);
  const { currentFile, selectedFileData }: any = state.viewer;

  const onDoubleClick = (
    event: React.MouseEvent<HTMLDivElement, MouseEvent>
  ) => {
      editMode(true);
    
  };

  const editMode = (isEditEnabled: boolean) => {
    setEditingModeEnabled(isEditEnabled);
  };

  useEffect(() => {
    if (editingModeEnabled) {
      inputRef.current?.focus();
    }
  }, [editingModeEnabled]);


  return (
    <div
      data-testid="document-item"
      onDoubleClick={(event) => onDoubleClick(event)}
      id="docName"
      className="vc-head-hWrap"
      >
      {!!editingModeEnabled ? (
        <RenameFile
          editingModeEnabled={editingModeEnabled}
          editMode={editMode}
          isWorkBenchFile={false}
        />
      ) : (
        <h2 title={selectedFileData?.name}>
         {selectedFileData?.name}
        </h2>
      )}
    </div>
  );
};
