import React, { useState, useEffect, useContext } from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument'
import { Store } from '../../../../Store/Store'
import { LocalDB } from '../../../../Utils/LocalDB'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { useHistory } from 'react-router-dom'



type ReviewNeedListRequestHomeType = {
    documentList: any[],
    saveAsDraft: Function,
    showSendButton: boolean
    documentHash: string | undefined
    setHash: Function
    defaultEmail: string | undefined
}

export const ReviewNeedListRequestHome = ({ documentList, saveAsDraft, showSendButton, documentHash, setHash, defaultEmail }: ReviewNeedListRequestHomeType) => {
    const [documentsName, setDocumentName] = useState<string>();
    const [emailTemplate, setEmailTemplate] = useState();

    const history = useHistory();
 
    useEffect(() => {
        history.push(`/newNeedList/${LocalDB.getLoanAppliationId()}`);
    }, [!documentList]);

    const getDocumentsName = () => {
        if (!documentList) return;
        let names: string = "<ul>";
          
        for (let i = 0; i < documentList.length; i++) {
            names += "<li>" + documentList[i].docName+"</li>";
            if (i != documentList.length - 1)
            names = names + "\n";
        }
        names += "</ul>"
        setDocumentName(names)
    }
    
   
    const getEmailTemplate = async () => {
        let res: any = await TemplateActions.fetchEmailTemplate();
        setEmailTemplate(res);
    }

    useEffect(() => {
        getDocumentsName();
        //getEmailTemplate();
    }, [documentList])

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
                        emailTemplate = {emailTemplate}
                        showSendButton = {showSendButton}
                        documentList={documentList}
                        documentHash = {documentHash}
                        setHash = {setHash}
                        defaultEmail = {defaultEmail}
                    />
                </div>
            </div>
        </div>
    )
}
