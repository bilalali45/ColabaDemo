import React, {ChangeEvent, useState} from 'react';
import { CategoryDocument } from '../../Entities/Models/CategoryDocument';
import { Document } from '../../Entities/Models/Document';
import { TemplateDocument } from '../../Entities/Models/TemplateDocument';
import { SVGSearch } from '../Shared/SVG';
import { SelectedDocumentTypeList } from './SelectedDocumentTypeList';

type SelectedTypeType = {
  setVisible: Function;
  selectedCatDocs: CategoryDocument;
  addNewDoc: Function;
  needList: TemplateDocument[];
};

export const CommonDocuments = ({
  selectedCatDocs,
  addNewDoc,
  setVisible,
  needList
}: SelectedTypeType) => {
  const [selectedCachedDoc, setSelectedCachedDoc] = useState<CategoryDocument>(
    selectedCatDocs
  );
  const [isSearched, setSearched] = useState<boolean>(false);
  const [term, setTerm] = useState<string>();

  const handleSearch = ({target: {value}}: ChangeEvent<HTMLInputElement>) => {
    setTerm(value);
    setSelectedCachedDoc((pre: CategoryDocument) => {
      let results = selectedCatDocs?.documents?.filter((cd: Document) => {
        if (cd.docType.toLowerCase().includes(value.toLowerCase())) {
          setSearched(false);
          return cd;
        } else {
          setSearched(true);
        }
      });
      return {
        ...selectedCachedDoc,
        documents: results
      };
    });
  };

  return (
    <>
      <div className="settings__add-docs-popup--search">
        <input
        data-testid="search-doc-name"
          className="settings__add-docs-popup--search-input"
          maxLength={255}
          autoFocus={true}
          onChange={handleSearch}
          type="search"
          placeholder="Enter follow up name…"
        />
        <button className="settings__add-docs-popup--search-submit">
          {/* <i className="zmdi zmdi-search"></i> */}
          <SVGSearch/>
        </button>
      </div>

      <div className="settings__add-docs-popup--search-details">
        <h4 className="h4">{isSearched ? 'Search Result' : selectedCatDocs.catName}</h4>
        
        <SelectedDocumentTypeList
          term={term}
          setVisible={setVisible}
          documentList={selectedCachedDoc?.documents}
          addNewDoc={addNewDoc}
          needList={needList}
        />
      </div>
    </>
  );
};
