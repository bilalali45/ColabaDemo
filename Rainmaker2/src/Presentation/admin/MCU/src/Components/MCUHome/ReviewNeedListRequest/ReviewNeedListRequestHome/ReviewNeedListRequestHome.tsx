import React, { useState, useEffect } from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument'


type needListRequestReviewProps = {
    documentList: TemplateDocument[];
}


export const ReviewNeedListRequestHome = ({documentList} : needListRequestReviewProps) => {
    const [documentsName, setDocumentName] = useState<string>();
    
    const getDocumentsName = () => {
        if(!documentList) return;
        let names: string ="";
     for(let i = 0; i < documentList.length; i++){
         names = "-"+documentList[i].docName;
         if(i != documentList.length-1)
          names = names+",";
     }
     setDocumentName(names)
    }
    
    useEffect(() =>{
        getDocumentsName();
    })

    return (
        <div className="mcu-panel-body">
            <div className="row">
                <div className="col-md-4 no-padding mcu-panel-body--col">
                    <SelectedNeedListReview
                    documentList = {documentList}
                    
                    />
                </div>
                <div className="col-md-8 no-padding mcu-panel-body--col">
                    <EmailContentReview
                     documentList = {documentList}
                     documentsName = {documentsName}
                    />
                </div>
            </div>            
        </div>
    )
}
