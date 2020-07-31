import React, { useContext, useState } from 'react'
import { Document } from '../../../../../../Entities/Models/Document'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions'
import { Store } from '../../../../../../Store/Store'
import { TemplateDocument } from '../../../../../../Entities/Models/TemplateDocument'
import Spinner from 'react-bootstrap/Spinner'
import { useLocation } from 'react-router-dom'


type SelectedTypeType = {
    setVisible: Function,
    documentList: Document[],
    addNewDoc: Function,
    term?: string
}

export const SelectedDocumentTypeList = ({ documentList, addNewDoc, setVisible, term }: SelectedTypeType) => {

    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [removeDocName, setRemoveDocName] = useState<string>();
    const { state, dispatch } = useContext(Store);


    const templateManager: any = state?.templateManager;
    const templateDocuments: any = templateManager?.templateDocuments;
    const currentCategoryDocuments: any = templateManager?.currentCategoryDocuments;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;


    const location = useLocation();

    const filterUsedDocs = (templateDocs: Document[]) => {
        return documentList?.filter((cd: any) => !templateDocs?.find((td: any) => {
            console.log(cd, td);
            if(!cd?.docId) {
                if(cd?.docType?.toLowerCase() === td?.docName?.toLowerCase()) {
                    return cd;
                }
            }
            if(td?.typeId === cd?.docTypeId) {
                return cd;
            }
        }));
    }

    if (!documentList) {
        return null;
    }

    let usedDocs = location.pathname.includes('newNeedList') ? selectedTemplateDocuments : templateDocuments;

    return (
        <div className="active-docs">
            <ul className={currentCategoryDocuments?.catName == 'Other' ? 'other-ul' : ''}>
                {documentList &&
                    filterUsedDocs(usedDocs)?.map(dl => {
                        return (
                            <li
                                key={dl.docTypeId}
                                onClick={async () => {
                                    setRemoveDocName(dl?.docTypeId);
                                    setRequestSent(true)
                                    await addNewDoc(dl, 'typeId');
                                    setRequestSent(false)
                                    // setVisible(false);
                                }}>{dl?.docType}
                                {
                                    (requestSent && removeDocName === dl.docTypeId) ?
                                        <span>
                                            <Spinner size="sm" animation="border" role="status">
                                                <span className="sr-only">Loading...</span>
                                            </Spinner>
                                        </span>

                                        : ''}
                            </li>
                        )
                    })
                }

            </ul>
            {!documentList.length && term && <div className="doc-notfound"><p>No Results Found for “{term?.toLowerCase()}”</p></div>}
            {!documentList.length && !term && <div className="doc-notfound"><p>The list is empty.</p></div>}
        </div>
    )
}
