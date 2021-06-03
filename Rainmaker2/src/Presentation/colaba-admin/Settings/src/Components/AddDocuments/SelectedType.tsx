import React from 'react';
import { CategoryDocument } from '../../Entities/Models/CategoryDocument';
import { TemplateDocument } from '../../Entities/Models/TemplateDocument';
import { CommonDocuments } from './CommonDocuments';
import { SelectedTypeDocumentList } from './SelectedTypeDocumentList';


type SelectedTypeType = {
  selectedCatDocs: CategoryDocument;
  addNewDoc: Function;
  setVisible: Function;
  needList?: TemplateDocument[];
};

export const SelectedType = ({selectedCatDocs,addNewDoc,setVisible,needList}: SelectedTypeType) => {
  
  const renderCurrentlySelected = () => {
    if (selectedCatDocs?.catId === 'all') {
      return (
        <CommonDocuments
          needList={needList || []}
          setVisible={setVisible}
          selectedCatDocs={selectedCatDocs}
          addNewDoc={addNewDoc}
        />
      );
    } else {
      return (
        <SelectedTypeDocumentList
          setVisible={setVisible}
          documentList={selectedCatDocs?.documents}
          selectedCatDocs={selectedCatDocs}
          addNewDoc={addNewDoc}
          needList={needList}
        />
      );
    }
  };

  return (
    <>
    {renderCurrentlySelected()}
    </>
  );
};
