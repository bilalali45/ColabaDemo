import React, { useState, useEffect, useContext } from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument'
import { Store } from '../../../../Store/Store'



type ReviewNeedListRequestHomeType = {
    documentList: any[],
    saveAsDraft: Function
}

export const ReviewNeedListRequestHome = ({ documentList, saveAsDraft }: ReviewNeedListRequestHomeType) => {

    // const { state, dispatch } = useContext(Store);

    // const templateManager: any = state?.templateManager;
    // const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;

    const [documentsName, setDocumentName] = useState<string>();

    const getDocumentsName = () => {
        if (!documentList) return;
        let names: string = "";
        
        for (let i = 0; i < documentList.length; i++) {
            names += "-" + documentList[i].docName;
            if (i != documentList.length - 1)
                names = names + ",";
        }
        setDocumentName(names)
    }

    useEffect(() => {
        getDocumentsName();
    }, [documentList])

    console.log('Request Home')
    return (
        <div className="mcu-panel-body">
            <div className="row">
                <div className="col-md-4 no-padding mcu-panel-body--col">
                    <SelectedNeedListReview
                        documentList={documentList}

                    />
                </div>
                <div className="col-md-8 no-padding mcu-panel-body--col">
                    <EmailContentReview
                        documentsName={documentsName}
                        saveAsDraft={saveAsDraft}
                    />
                </div>
            </div>
        </div>
    )
}
