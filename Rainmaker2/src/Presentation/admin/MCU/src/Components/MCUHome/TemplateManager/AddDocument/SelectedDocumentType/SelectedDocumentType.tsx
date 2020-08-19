import React from 'react';
import {CommonDocuments} from './CommonDocuments/CommonDocuments';
import {SelectedTypeDocumentList} from './SelectedTypeDocumentList/SelectedTypeDocumentList';
import {SelectedDocumentTypeList} from './SelectedDocumentTypeList/SelectedDocumentTypeList';
import {CustomDocuments} from './CustomDocuments/CustomDocuments';
import {Document} from '../../../../../Entities/Models/Document';
import {Template} from '../../../../../Entities/Models/Template';
import {CategoryDocument} from '../../../../../Entities/Models/CategoryDocument';
import {TemplateDocument} from '../../../../../Entities/Models/TemplateDocument';

type SelectedTypeType = {
  selectedCatDocs: CategoryDocument;
  addNewDoc: Function;
  setVisible: Function;
  needList?: TemplateDocument[];
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
