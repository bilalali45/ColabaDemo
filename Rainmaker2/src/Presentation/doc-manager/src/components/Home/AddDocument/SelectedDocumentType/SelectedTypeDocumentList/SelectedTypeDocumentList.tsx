import React, {ChangeEvent} from 'react';
import {SelectedDocumentTypeList} from '../SelectedDocumentTypeList/SelectedDocumentTypeList';
import SearchIcon from '../../../../../../Assets/images/search-icon.svg';
import {CustomDocuments} from '../CustomDocuments/CustomDocuments';
import { Document } from '../../../../../Models/Document';

type SelectedTypeType = {
  setVisible: Function;
  documentList: Document[];
  selectedCatDocs: any;
  addNewDoc: Function;
  needList?: any;
};

export const SelectedTypeDocumentList = ({
  documentList,
  selectedCatDocs,
  addNewDoc,
  setVisible,
  needList
}: SelectedTypeType) => {
  const handleSearch = (e: ChangeEvent<HTMLInputElement>) => {};


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
    { selectedCatDocs?.catName === 'Other' && (
        <CustomDocuments setVisible={setVisible} addDocToTemplate={addNewDoc} />
        )}
    </div>
  );
};
