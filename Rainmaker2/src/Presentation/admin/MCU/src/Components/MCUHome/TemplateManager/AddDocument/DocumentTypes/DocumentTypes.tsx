import React, { useState, useEffect } from 'react'
import { CategoryDocument } from '../../../../../Entities/Models/CategoryDocument'

type DocumentTypesType = {
    currentCategoryDocuments: CategoryDocument
    changeCurrentDocType: Function
    documentTypeList: CategoryDocument[]
}

export const DocumentTypes = ({ documentTypeList, changeCurrentDocType, currentCategoryDocuments }: DocumentTypesType) => {

    const [documentTypeItems, setDocumentTypeList] = useState<CategoryDocument[]>(documentTypeList);

    useEffect(() => {
        setDocumentTypeList((pre: CategoryDocument[]) => {
            let items: CategoryDocument[] = [];
            let other: CategoryDocument | null = null;
            pre.forEach((cd: CategoryDocument) => {
                if (cd.catName !== 'Other') {
                    items.push(cd);
                } else {
                    other = cd;
                }
            })
            if (other) {
                items.push(other);
            }
            return items;
        })
    }, [documentTypeList])

    return (
        <div className="list-doc-cat">
            <div key={'all'} className={`listAll ${currentCategoryDocuments?.catId === 'all' ? 'active' : ''} `} onClick={() => changeCurrentDocType('all')}>
                All
            </div>
            <ul className="ul-list-doc-cat">
                {
                    documentTypeItems?.map((p: CategoryDocument) => {
                        return (
                            <li key={p.catId} className={currentCategoryDocuments?.catId === p?.catId ? 'active' : ''} onClick={() => changeCurrentDocType(p?.catId)}>{p?.catName}</li>
                        )
                    })
                }
            </ul>

        </div>
    )
}
