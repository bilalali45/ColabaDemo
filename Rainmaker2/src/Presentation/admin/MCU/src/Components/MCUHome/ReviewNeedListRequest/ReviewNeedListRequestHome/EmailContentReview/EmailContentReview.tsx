import React, { useContext, useState, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';
import { TextArea } from '../../../../../Shared/components/TextArea';
import Spinner from 'react-bootstrap/Spinner';


type emailContentReviewProps = {
    documentsName: string | undefined;
    saveAsDraft: Function;
    emailTemplate?: string;
}
export const errorText = "Invalid character entered";

export const EmailContentReview = ({documentsName, saveAsDraft, emailTemplate = ''}:emailContentReviewProps) => {
    
    const setDeafultText = () => {
        let str: string = '';
        let documentNames = documentsName ? documentsName?.split(',').join("\n") : '';
        if(emailTemplate){
            str = emailTemplate.replace("{user}",borrowername).replace("{documents}",documentNames);          
        }
        return str       
    }

    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const loanData = needListManager?.loanInfo;
    const borrowername = loanData?.borrowers[0];
    const [emailBody, setEmailBody] = useState(setDeafultText());
    const [isValid, setIsValid] = useState<boolean>(false);
    const regex = /^[ A-Za-z0-9-,.!@#$%^&*()_+=`~{}\s]*$/i;
   

    useEffect(() =>{     
        setEmailBody(setDeafultText());  
    },[emailTemplate])

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
       dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
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

            <footer className="mcu-panel-footer text-right">
                <button onClick={() => saveAsDraft(false)} className="btn btn-primary">Send Request</button>
            </footer>
        </div>
    )
}
