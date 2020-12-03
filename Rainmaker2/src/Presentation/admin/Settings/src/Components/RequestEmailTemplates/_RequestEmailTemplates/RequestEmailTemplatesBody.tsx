import React from 'react'
import ContentBody from '../../Shared/ContentBody'
import { CreateEmailTemplates } from './CreateEmailTemplates'
import { EmailTemplatesList } from './EmailTemplatesList'


type props = {
    addEmailTemplateClick? : any;
    showEmailList: boolean;
    showinsertToken: boolean;
    insertTokenClick? : any;   
    }

export const RequestEmailTemplatesBody = ({addEmailTemplateClick, showEmailList, showinsertToken, insertTokenClick}: props) => {
    return (
        <div className={`settings__content-area--body no-padding ${showEmailList ? 'show-email-templates-list' : 'create-email-templates'}`}>
            { showEmailList 
             ? 
             <EmailTemplatesList
              addEmailTemplateClick={addEmailTemplateClick}
              insertTokenClick={insertTokenClick}
             />
             :
            <CreateEmailTemplates
                 addEmailTemplateClick={addEmailTemplateClick}
                 insertTokenClick={insertTokenClick}
                 showinsertToken={showinsertToken}
            />
            }
            
        </div>
    )
}
