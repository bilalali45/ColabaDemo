import React from 'react';
import {CommonDocuments} from './CommonDocuments/CommonDocuments';
import {SelectedTypeDocumentList} from './SelectedTypeDocumentList/SelectedTypeDocumentList';
import {SelectedDocumentTypeList} from './SelectedDocumentTypeList/SelectedDocumentTypeList';
import {CustomDocuments} from './CustomDocuments/CustomDocuments';


type SelectedTypeType = {
  selectedCatDocs: any;
  addNewDoc: Function;
  setVisible: Function;
  needList?: any;
};

export const SelectedType = ({
  selectedCatDocs,
  addNewDoc,
  setVisible,
  needList
}: SelectedTypeType) => {
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
    <div className="pop-detail-doc">
      <div className="pop-detail-doc--body">{renderCurrentlySelected()}</div>
    </div>
  );
};
