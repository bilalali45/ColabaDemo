import React, { useState, useEffect } from 'react'


type DocumentTypesType = {
    currentCategoryDocuments: any
    changeCurrentDocType: Function
    documentTypeList: any[]
}

export const DocumentTypes = ({ documentTypeList, changeCurrentDocType, currentCategoryDocuments }: DocumentTypesType) => {

    const [documentTypeItems, setDocumentTypeList] = useState<any[]>(documentTypeList);

    const reorderCatsToShowOtherInLast = () => {
        let cats = [];
        let otherCat = null;
        for (const cat of documentTypeItems) {
            if (cat?.catName !== 'Other') {
                cats.push(cat);
            } else {
                otherCat = cat;
            }
        }

        return [...cats, otherCat];
    }

    return (
        <div className="list-doc-cat">
            <div key={'all'} className={`listAll ${currentCategoryDocuments?.catId === 'all' ? 'active' : 'active'} `} onClick={() => changeCurrentDocType('all')}>
                All
            </div>
            <ul className="ul-list-doc-cat">
                {

                    reorderCatsToShowOtherInLast()?.map((p: any) => {
                        return (
                            <li data-testid='doc-cat' key={p.catId} className={currentCategoryDocuments?.catId === p?.catId ? 'active' : ''} onClick={() => changeCurrentDocType(p?.catId)}>{p?.catName}</li>
                        )
                    })
                }
            </ul>

        </div>
    )
}
