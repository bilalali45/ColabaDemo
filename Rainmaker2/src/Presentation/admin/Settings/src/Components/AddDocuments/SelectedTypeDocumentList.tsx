import React from 'react';
import { useLocation } from 'react-router-dom';
import { CategoryDocument } from '../../Entities/Models/CategoryDocument';
import { Document } from '../../Entities/Models/Document';
import { TemplateDocument } from '../../Entities/Models/TemplateDocument';
import { CustomDocuments } from './CustomDocuments';
import { SelectedDocumentTypeList } from './SelectedDocumentTypeList';

type SelectedTypeType = {
  setVisible: Function;
  documentList: Document[];
  selectedCatDocs: CategoryDocument;
  addNewDoc: Function;
  needList?: TemplateDocument[];
};

export const SelectedTypeDocumentList = ({
  documentList,
  selectedCatDocs,
  addNewDoc,
  setVisible,
  needList
}: SelectedTypeType) => {

  const location = useLocation();
  return (
    <div className="settings__add-docs-popup--search-details" data-testid="selected-cat-docs-container">
        <h4 className="h4">{selectedCatDocs?.catName}</h4>
      <SelectedDocumentTypeList
        setVisible={setVisible}
        documentList={documentList}
        addNewDoc={addNewDoc}
        needList={needList}
      />
      {!location?.pathname?.includes('ManageDocumentTemplate') && selectedCatDocs?.catName === 'Other' && (
        <CustomDocuments setVisible={setVisible} addDocToTemplate={addNewDoc} />
      )}
    </div>
  );
};
