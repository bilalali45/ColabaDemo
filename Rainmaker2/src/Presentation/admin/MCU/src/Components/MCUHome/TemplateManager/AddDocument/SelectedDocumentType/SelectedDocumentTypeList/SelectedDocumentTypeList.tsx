import React, { useContext, useState } from 'react'
import { Document } from '../../../../../../Entities/Models/Document'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions'
import { Store } from '../../../../../../Store/Store'
import { TemplateDocument } from '../../../../../../Entities/Models/TemplateDocument'
import Spinner from 'react-bootstrap/Spinner'


type SelectedTypeType = {
    setVisible: Function,
    documentList: Document[],
    addNewDoc: Function
}

export const SelectedDocumentTypeList = ({ documentList, addNewDoc, setVisible }: SelectedTypeType) => {

    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [removeDocName, setRemoveDocName] = useState<string>();
    const { state, dispatch } = useContext(Store);


    const templateManager: any = state?.templateManager;
    const templateDocuments: any = templateManager?.templateDocuments;

    const filterUsedDocs = (templateDocs: Document[]) => {
        return documentList?.filter((cd: any) => !templateDocs?.find((td: any) => td?.typeId === cd?.docTypeId));
    }

    if (!documentList) {
        return null;
    }

    return (

        <div className="active-docs">
            <ul>
                {documentList &&
                    filterUsedDocs(templateDocuments)?.map(dl => {
                        return (
                            <li onClick={async () => {
                                setRemoveDocName(dl?.docTypeId);
                                setRequestSent(true)
                                await addNewDoc(dl.docTypeId, 'typeId');
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
            {!documentList.length && <div className="doc-notfound"><p>No Results Found for “Page 5 0f 5 Case checking  account”</p></div>}
        </div>
    )
}
