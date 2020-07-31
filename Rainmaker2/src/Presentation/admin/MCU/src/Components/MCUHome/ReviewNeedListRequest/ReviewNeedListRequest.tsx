import React, { useContext } from 'react'
import { ReviewNeedListRequestHeader } from './ReviewNeedListRequestHeader/ReviewNeedListRequestHeader'
import { ReviewNeedListRequestHome } from './ReviewNeedListRequestHome/ReviewNeedListRequestHome'
import { TemplateDocument } from '../../../Entities/Models/TemplateDocument';
import { Store } from '../../../Store/Store';

export const ReviewNeedListRequest = () => {
    
    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;


    const saveAsDraft = () => {
        console.log('Save as Draft');
    }
    console.log('selectedTemplateDocuments',selectedTemplateDocuments)
    return (
        <div className="mcu-panel">
            <ReviewNeedListRequestHeader
             saveAsDraft={saveAsDraft}
            />
            <ReviewNeedListRequestHome
           documentList = {selectedTemplateDocuments}
            />
        </div>
    )
}
