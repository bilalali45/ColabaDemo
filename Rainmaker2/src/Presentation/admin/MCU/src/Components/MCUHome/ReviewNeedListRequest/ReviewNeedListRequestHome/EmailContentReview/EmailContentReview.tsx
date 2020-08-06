import React, { useContext, useState, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';
import { TextArea } from '../../../../../Shared/components/TextArea';
import Spinner from 'react-bootstrap/Spinner';
import { LocalDB } from '../../../../../Utils/LocalDB';


type emailContentReviewProps = {
    documentsName: string | undefined;
    saveAsDraft: Function;
    emailTemplate?: string;
    showSendButton: boolean;
    documentList: any;
    documentHash: string | undefined;
    setHash: Function
}
export const errorText = "Invalid character entered";

export const EmailContentReview = ({documentsName, saveAsDraft, emailTemplate = '', showSendButton, documentList, documentHash, setHash}:emailContentReviewProps) => {
    
    console.log('documentHash',documentHash)
    const setDeafultText = () => {
        let str: string = '';
        let documentNames = documentsName ? documentsName?.split(',').join("\n") : '';
        if(emailTemplate){
            str = emailTemplate.replace("{user}",borrowername).replace("{documents}",documentNames);
            hashDocuments();     
        }

        let length = documentsName?.split(',').length;  
        if(length){
            dispatch({type: TemplateActionsType.SetDocumentLength, payload: length })
        }        
        return str       
    }

    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const templateManager: any = state?.templateManager;
    const isDocumentDraft = templateManager?.isDocumentDraft;
    const emailContent = templateManager?.emailContent;
    const previousDocLength = templateManager?.documentLength;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments || [];
    const loanData = needListManager?.loanInfo;
    const borrowername = loanData?.borrowers[0];
    const [emailBody, setEmailBody] = useState<string>();
    const [isValid, setIsValid] = useState<boolean>(false);
    const regex = /^[ A-Za-z0-9-,.!@#$%^&*()_+=`~{}\s]*$/i;
   

    useEffect(() =>{ 
        if(isDocumentDraft?.requestId) {
            draftExist();         
        }else{
            draftNotExist(); 
        }     
    },[emailTemplate])

    const hashDocuments = () => {
     let hash = LocalDB.encodeString(JSON.stringify(documentList))
     setHash(hash);
    }


  const editEmailBodyHandler = (e: any) => {
     let txt = e.target.value;
     if(regex.test(txt)){
        setEmailBody(txt);
        setIsValid(false)
     }else{
        setIsValid(true)
     }   
   }

   const saveEmailContent = () => {
       if(emailBody){
        dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
       }     
   }

   const draftExist = () => {
       if(!previousDocLength){
        if(selectedTemplateDocuments[0].message != ''){
            setEmailBody(selectedTemplateDocuments[0].message); 
            dispatch({type: TemplateActionsType.SetEmailContent, payload: selectedTemplateDocuments[0].message})
            dispatch({type: TemplateActionsType.SetDocumentLength, payload: selectedTemplateDocuments.length }) 
        }else{
            setEmailBody(setDeafultText()); 
            dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
        }
        return ;
       }else{
        if(previousDocLength != selectedTemplateDocuments.length){
            setEmailBody(setDeafultText());
            dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
        }else{
            setEmailBody(emailContent);
            dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
        }
       }
    
   }
   const draftNotExist = () => {
       debugger
       if(emailTemplate){   
        let Newhash = LocalDB.encodeString(JSON.stringify(selectedTemplateDocuments))
        if(documentHash != Newhash){
              setEmailBody(setDeafultText());
             }
        else{
             setEmailBody(emailContent);
            } 
       }    
   }

   const sendRequestButton = () => {
       if(!showSendButton){
        return(
            <>
             <footer className="mcu-panel-footer text-right">
                 <button onClick={() => saveAsDraft(false)} className="btn btn-primary">Send Request</button>
             </footer>
            </>
        )
       }else{
        return(
            <>
             <footer className="mcu-panel-footer text-center alert alert-success">Need list has been sent.</footer>
            </>
        )  
       }
       
   }

   if(!emailTemplate){
    return (
        <div className="loader-widget loansnapshot">
          <Spinner animation="border" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        </div>
      );
   }
  
   
    return (
        <div className="mcu-panel-body--content">
            <div className="mcu-panel-body padding">
         <h2 className="h2">Review email to {borrowername}</h2>
                <p>If you'd like, you can customize this email.</p>
                <TextArea
                 focus = {true}
                 textAreaValue = {emailBody} 
                 onBlurHandler = {saveEmailContent}
                 onChangeHandler = {editEmailBodyHandler}
                 errorText = {errorText}
                 isValid = {isValid}
                />

            </div>
                 {sendRequestButton()}
           

           
        </div>
    )
}
