import React, {ChangeEvent} from 'react';
import {SelectedDocumentTypeList} from '../SelectedDocumentTypeList/SelectedDocumentTypeList';
import {Document} from '../../../../../../Entities/Models/Document';
import {Template} from '../../../../../../Entities/Models/Template';
import {CategoryDocument} from '../../../../../../Entities/Models/CategoryDocument';
import SearchIcon from '../../../../../../Assets/images/search-icon.svg';
import {CustomDocuments} from '../CustomDocuments/CustomDocuments';
import {TemplateDocument} from '../../../../../../Entities/Models/TemplateDocument';
import { useLocation } from 'react-router-dom';

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
  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {};

  const location = useLocation();
  return (
    <div data-testid="selected-cat-docs-container">
      <div className="s-wrap">
        <h4>{selectedCatDocs?.catName}</h4>
      </div>
      <SelectedDocumentTypeList
        setVisible={setVisible}
        documentList={documentList}
        addNewDoc={addNewDoc}
        needList={needList}
      />
      {!location?.pathname?.includes('templateManager') && selectedCatDocs?.catName === 'Other' && (
        <CustomDocuments setVisible={setVisible} addDocToTemplate={addNewDoc} />
      )}
    </div>
  );
};
