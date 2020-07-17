import React, { useContext } from 'react'
import { Document } from '../../../../../../Entities/Models/Document'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions'
import { Store } from '../../../../../../Store/Store'
import { TemplateDocument } from '../../../../../../Entities/Models/TemplateDocument'


type SelectedTypeType = {
    setVisible: Function,
    documentList: Document[],
    addNewDoc: Function
}

export const SelectedDocumentTypeList = ({ documentList, addNewDoc, setVisible }: SelectedTypeType) => {

    const {state, dispatch} = useContext(Store);

    const templateManager : any = state?.templateManager;
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
                            <li onClick={() => {
                                addNewDoc(dl.docTypeId, 'typeId');
                                // setVisible(false);
                            }}>{dl?.docType}</li>
                        )
                    })
                }
                
            </ul>
            {!documentList.length && <div className="doc-notfound"><p>No Results Found for “Page 5 0f 5 Case checking  account”</p></div>}
            </div>
    )
}
