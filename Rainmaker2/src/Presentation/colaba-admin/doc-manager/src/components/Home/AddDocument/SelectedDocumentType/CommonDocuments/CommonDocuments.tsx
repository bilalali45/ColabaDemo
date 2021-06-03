import React, {ChangeEvent, useEffect, useState} from 'react';
import {SelectedDocumentTypeList} from '../SelectedDocumentTypeList/SelectedDocumentTypeList';



import SearchIcon from '../../../../../Assets/images/search-icon.svg';
import { CategoryDocument } from '../../../../../Models/CategoryDocument';
import { Document } from '../../../../../Models/Document';


type SelectedTypeType = {
  setVisible: Function;
  selectedCatDocs: any;
  addNewDoc: Function;
  needList: any[];
};

export const CommonDocuments = ({
  selectedCatDocs,
  addNewDoc,
  setVisible,
  needList
}: SelectedTypeType) => {
  const [selectedCachedDoc, setSelectedCachedDoc] = useState<any>(
    selectedCatDocs
  );
  const [isSearched, setSearched] = useState<boolean>(false);
  const [term, setTerm] = useState<string>();

  const handleSearch = ({target: {value}}: ChangeEvent<HTMLInputElement>) => {
    setTerm(value);

    setSelectedCachedDoc((pre: CategoryDocument) => {
      let results = selectedCatDocs?.documents?.filter((cd: Document) => {
        if (cd?.docType?.toLowerCase().includes(value.toLowerCase())) {
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
    <div className="common-wrap">
      <div className="s-wrap">
        <input
        data-testid="search-doc-name"
          maxLength={255}
          autoFocus={true}
          onChange={handleSearch}
          type="name"
          placeholder="Enter document name…"
        />
        <div className="s-icon">
          <img src={SearchIcon} alt="" />
        </div>
      </div>

      <div className="b-title">
        <h4>{isSearched ? 'Search Result' : selectedCatDocs.catName} </h4>
      </div>

      <SelectedDocumentTypeList
        term={term}
        setVisible={setVisible}
        documentList={selectedCachedDoc?.documents}
        addNewDoc={addNewDoc}
        needList={needList}
      />
    </div>
  );
};
