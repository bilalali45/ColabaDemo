import React, {useContext, useState, useEffect} from 'react';
import {Store} from '../../../../../Store/Store';
import {TemplateDocument} from '../../../../../Entities/Models/TemplateDocument';
import {TemplateActionsType} from '../../../../../Store/reducers/TemplatesReducer';
import {TextArea} from '../../../../../Shared/components/TextArea';
import Spinner from 'react-bootstrap/Spinner';
import {LocalDB} from '../../../../../Utils/LocalDB';
import { enableBrowserPrompt } from '../../../../../Utils/helpers/Common';

type emailContentReviewProps = {
  documentsName: string | undefined;
  saveAsDraft: Function;
  emailTemplate?: string;
  showSendButton: boolean;
  documentList: any;
  documentHash: string | undefined;
  setHash: Function;
  defaultEmail: string | undefined

};
export const errorText = 'Invalid character entered';

export const EmailContentReview = ({
  documentsName,
  saveAsDraft,
  emailTemplate = '',
  showSendButton,
  documentList,
  documentHash,
  setHash,
  defaultEmail
}: emailContentReviewProps) => {

  const setDeafultText = () => {
    let str: string = '';
    let payload = LocalDB.getUserPayload();
    let mcuName = payload.FirstName+' '+payload.LastName;
    let documentNames = documentsName
      ? documentsName?.split(',').join(' \r\n')
      : '';
    if (emailTemplate) {
      str = emailTemplate
        .replace('{user}', borrowername)
        .replace('{documents}', documentNames)
        .replace('{mcu}',mcuName);
      hashDocuments();
      enableBrowserPrompt();
    }
    return str;
  };

  const {state, dispatch} = useContext(Store);

  const needListManager: any = state?.needListManager;
  const templateManager: any = state?.templateManager;
  const isDocumentDraft = templateManager?.isDocumentDraft;
  const emailContent = templateManager?.emailContent;
  const previousDocLength = templateManager?.documentLength;
  const selectedTemplateDocuments: TemplateDocument[] =
    templateManager?.selectedTemplateDocuments || [];
  const loanData = needListManager?.loanInfo;
  const borrowername = loanData?.borrowers[0];
  const [emailBody, setEmailBody] = useState<string>();
  const [isValid, setIsValid] = useState<boolean>(false);
  const [isSendBtnDisable, setSendBtnDisable] = useState<boolean>(false);

  const regex = /^[a-zA-Z0-9~`!@#\$%\^&\*\(\)_\-\+={\[\}\]\|\\:;"'<,>\.\?\/\s  ]*$/i;

  console.log('defaultEmail',defaultEmail)


  useEffect(() => {
    if (Boolean(isDocumentDraft?.requestId)) {
      draftExist();
    } else {
      draftNotExist();
    }
  }, [emailTemplate]);

  const hashDocuments = () => {
    let hash = LocalDB.encodeString(JSON.stringify(documentList));
    setHash(hash);
  };

  const editEmailBodyHandler = (e: any) => {
    let txt = e.target.value;
    if (regex.test(txt)) {
      setEmailBody(txt);
      setIsValid(false);
    } else {
      setIsValid(true);
    }
  };

  const saveEmailContent = () => {
    if (emailBody) {
      dispatch({type: TemplateActionsType.SetEmailContent, payload: emailBody});
    }
  };

  const draftExist = () => {
    if (!documentHash) {
      if (selectedTemplateDocuments[0].message != '') {
        let body = selectedTemplateDocuments[0].message.replace('<br />',' \r\n')
        setEmailBody(body);
        enableBrowserPrompt();
        dispatch({
          type: TemplateActionsType.SetEmailContent,
          payload: body
        });
        hashDocuments();
      } else {
        setEmailBody(setDeafultText());
        enableBrowserPrompt();
        dispatch({
          type: TemplateActionsType.SetEmailContent,
          payload: emailBody
        });
      }
      return;
    } else {
      let Newhash = LocalDB.encodeString(JSON.stringify(documentList));
      if (documentHash != Newhash) {
        setEmailBody(setDeafultText());
        enableBrowserPrompt();
        dispatch({
          type: TemplateActionsType.SetEmailContent,
          payload: emailBody
        });
      } else {
        setEmailBody(emailContent);
        enableBrowserPrompt();
        dispatch({
          type: TemplateActionsType.SetEmailContent,
          payload: emailBody
        });
      }
    }
  };
  const draftNotExist = () => {
    if (emailTemplate) {
      let Newhash = LocalDB.encodeString(JSON.stringify(documentList));
      if (documentHash != Newhash) {
        setEmailBody(setDeafultText());
      } else {
        setEmailBody(emailContent);
      }
      enableBrowserPrompt();
    }
  };

  const sendRequestButton = () => {
    if (showSendButton) {
      return (
        <>
          <footer className="mcu-panel-footer text-right">
            <button disabled={isSendBtnDisable}
              onClick={
                () =>{
                  setSendBtnDisable(true)
                  saveAsDraft(false)
                }}
              className="btn btn-primary"
            >
              Send Request
            </button>
          </footer>
        </>
      );
    } else {
      return (
        <>
          <footer className="mcu-panel-footer text-center alert alert-success">
            Need list has been sent.
          </footer>
        </>
      );
    }
  };

  if (!emailTemplate) {
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
        <h3 className="text-ellipsis" title={'Review email to '+ borrowername}>Review email to {borrowername}</h3>
        <p>If you'd like, you can customize this email.</p>
        <TextArea
          focus={true}
          textAreaValue={emailBody}
          onBlurHandler={saveEmailContent}
          onChangeHandler={editEmailBodyHandler}
          errorText={errorText}
          isValid={isValid}
          placeholderValue={'Type your message'}
        />
      </div>
      {sendRequestButton()}
    </div>
  );
};
