import React, {useState, useEffect} from 'react';
import {CategoryDocument} from '../../Entities/Models/CategoryDocument';

type DocumentTypesType = {
  currentCategoryDocuments: CategoryDocument;
  changeCurrentDocType: Function;
  documentTypeList: CategoryDocument[];
};

export const DocumentTypes = ({documentTypeList,changeCurrentDocType,currentCategoryDocuments}: DocumentTypesType) => {
  const [documentTypeItems, setDocumentTypeList] = useState<CategoryDocument[]>(documentTypeList);

  useEffect(() => {
    setDocumentTypeList((pre: CategoryDocument[]) => {
      let items: CategoryDocument[] = [];
      let other: CategoryDocument | null = null;
      pre?.forEach((cd: CategoryDocument) => {
        if (cd.catName !== 'Other') {
          items.push(cd);
        } else {
          other = cd;
        }
      });
      if (other) {
        items.push(other);
      }
      return items;
    });
  }, [documentTypeList]);

  return ( 
      <ul data-testid="DocumentTypes">
        <li data-testid="all-docs" key={'all'} className={currentCategoryDocuments?.catId === 'all' ? 'active' : ''}  onClick={() => changeCurrentDocType('all')}>
          <a href="javascript:;">All</a>
        </li>
        {documentTypeItems?.map((p: CategoryDocument) => {
          return (
            <li 
              data-testid="doc-cat"
              key={p.catId}
              className={
                currentCategoryDocuments?.catId === p?.catId ? 'active' : ''
              }
              onClick={() => changeCurrentDocType(p?.catId)}
            >
              <a href="javascript:;" title={p?.catName}>{p?.catName}</a>
            </li>
          );
        })}
      </ul>
  );
};
