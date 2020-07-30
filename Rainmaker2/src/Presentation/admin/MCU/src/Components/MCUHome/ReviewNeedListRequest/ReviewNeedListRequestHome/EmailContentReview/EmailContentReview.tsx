import React, { useContext, useState, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';


type emailContentReviewProps = {
    documentList: TemplateDocument[];
    documentsName: string | undefined;
}

export const EmailContentReview = ({documentList, documentsName}:emailContentReviewProps) => {
    //const arr: string = "-Financial statement,-Bank statement,-Pay slip";
    const setDeafultText = () => {
        let documentNames = documentsName ? documentsName?.split(',').join("\n") : '';
        let mainText = "Hi " +borrowername+",\n\n\n To continue your application, we need some more information."+"\n\n\n"+documentNames+"\n\n\n Complete these items as soon as possible so we can continue reviewing your application."
        return mainText;
    }

    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const loanData = needListManager?.loanInfo;
    const borrowername = loanData?.borrowers[0];
    const [emailBody, setEmailBody] = useState(setDeafultText());

   

    useEffect(() =>{        
        setEmailBody(setDeafultText());  
    },[documentsName])

    useEffect(() =>{        
        setTimeout(()=>{
            saveEmailContent();
        },1000)      
    })

  const editEmailBodyHandler = (e: any) => {
     let txt = e.target.value;
     setEmailBody(txt);
   }

   const saveEmailContent = () => {
       dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody})
   }

    return (
        <div className="mcu-panel-body--content">
            <div className="mcu-panel-body padding">
         <h2 className="h2">Review email to {borrowername}</h2>
                <p>If you'd like, you can customize this email.</p>

                <textarea onBlur={saveEmailContent} value={emailBody} onChange = {(e) => {editEmailBodyHandler(e)}} name="" id="" className="form-control" rows={20}>
                </textarea>

            </div>

            <footer className="mcu-panel-footer text-right">
                <button className="btn btn-primary">Send Request</button>
            </footer>
        </div>
    )
}
