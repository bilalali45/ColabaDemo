import React, { useState, useEffect, useContext } from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument'
import { Store } from '../../../../Store/Store'





export const ReviewNeedListRequestHome = () => {

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;

    const [documentsName, setDocumentName] = useState<string>();
    
    const getDocumentsName = () => {
        if(!selectedTemplateDocuments) return;
        let names: string ="";
     for(let i = 0; i < selectedTemplateDocuments.length; i++){
         names += "-"+selectedTemplateDocuments[i].docName;
         if(i != selectedTemplateDocuments.length-1)
          names = names+",";
     }
     setDocumentName(names)
    }
    
    useEffect(() =>{
        getDocumentsName();
    },[selectedTemplateDocuments])

console.log('Request Home')
    return (
        <div className="mcu-panel-body">
            <div className="row">
                <div className="col-md-4 no-padding mcu-panel-body--col">
                    <SelectedNeedListReview
                    documentList = {selectedTemplateDocuments}
                    
                    />
                </div>
                <div className="col-md-8 no-padding mcu-panel-body--col">
                    <EmailContentReview
                     documentsName = {documentsName}
                    />
                </div>
            </div>            
        </div>
    )
}
