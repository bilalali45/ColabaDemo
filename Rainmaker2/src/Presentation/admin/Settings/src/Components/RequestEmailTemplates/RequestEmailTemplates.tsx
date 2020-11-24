import React, { useContext, useState } from 'react'
import { RequestEmailTemplateActionsType } from '../../Store/reducers/RequestEmailTemplateReducer';
import { Store } from '../../Store/Store';
import { RequestEmailTemplatesBody } from './_RequestEmailTemplates/RequestEmailTemplatesBody'
import { RequestEmailTemplatesHeader } from './_RequestEmailTemplates/RequestEmailTemplatesHeader'

 const RequestEmailTemplates = () => {
     
    const {state, dispatch} = useContext(Store);
    const [showEmailList, setShowEmailList] = useState<boolean>(true);
    const [showinsertToken, setshowInsertToken] = useState<boolean>(false);

    const addEmailTemplateClickHandler = () => {
        setShowEmailList(!showEmailList);     
    }

    const insertTokenClickHandler = (value: boolean) => {
        setshowInsertToken(value);
    }

    return (
        <div className="settings__request-email-templates">
            <RequestEmailTemplatesHeader
              showEmailList = {showEmailList}
              addEmailTemplateClick ={addEmailTemplateClickHandler}
              showinsertToken = {showinsertToken}
              insertTokenClick = {insertTokenClickHandler}
            />
            <RequestEmailTemplatesBody
            showEmailList = {showEmailList}
            addEmailTemplateClick={addEmailTemplateClickHandler}
            showinsertToken={showinsertToken}
            insertTokenClick={insertTokenClickHandler}
            />
        </div>
    )
}

export default RequestEmailTemplates;