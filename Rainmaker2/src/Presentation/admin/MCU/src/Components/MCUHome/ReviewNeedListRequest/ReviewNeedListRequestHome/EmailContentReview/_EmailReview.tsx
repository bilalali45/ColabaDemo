import React, {useContext, useEffect, useState} from 'react';
import 'react-multi-email/style.css';
import {EmailInputBox} from '../../../../../Shared/components/EmailInputBox';
import {TextEditor} from '../../../../../Shared/components/TextEditor';
import {Controller, useForm} from 'react-hook-form';
import {RequestEmailTemplateActionsType} from '../../../../../Store/reducers/RequestEmailTemplateReducer';
import {RequestEmailTemplateActions} from '../../../../../Store/actions/RequestEmailTemplateActions';
import {Store} from '../../../../../Store/Store';
import {RequestEmailTemplate} from '../../../../../Entities/Models/RequestEmailTemplate';
import {Tokens} from '../../../../../Entities/Models/Token';
import { LocalDB } from '../../../../../Utils/LocalDB';


type props = {
  addEmailTemplateClick?: any;
  showinsertToken: boolean;
  showSendButton: boolean;
  saveAsDraft: Function;
  documentsName?: string;
  setSelectedEmailTemplate: Function,
  setselectedEmailTemplateName: Function
};

export const EmailReview = ({
  addEmailTemplateClick,
  showSendButton,
  saveAsDraft,
  documentsName,
  setSelectedEmailTemplate,
  setselectedEmailTemplateName
}: props) => {
  const {state, dispatch} = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const allToken: any = emailTemplateManger.tokens;
  const emailTemplates: RequestEmailTemplate[] = emailTemplateManger.requestEmailTemplateData;
  const token: Tokens = emailTemplateManger.selectedToken;
  const selectedEmailTemplate = emailTemplateManger.selectedEmailTemplate;
  const draftEmail: any = emailTemplateManger.draftEmail;
  const emailContent = emailTemplateManger?.emailContent;
  const isEdit = emailTemplateManger?.isEdit;
  const isListUpdated = emailTemplateManger?.listUpdated;
  const selectedId = emailTemplateManger?.selectedId;

  const [test, setTest] = useState<number>(1);
  const [fromEmail, setFromEmail] = useState<string>();
  const [fromEmailArray, setFromEmailArray] = useState<string[]>([]);
  const [toEmail, setToEmail] = useState<string>();
  const [toEmailArray, setToEmailArray] = useState<string[]>([]);
  const [cCEmail, setCCEmail] = useState<string>();
  const [cCEmailArray, setCCEmailArray] = useState<string[]>([]);
  const [subject, setSubject] = useState<string>();
  const [emailBody, setEmailBody] = useState<string>();
  const [tokens, setValidTokens] = useState<string[]>([]);
  const [isSendBtnDisable, setSendBtnDisable] = useState<boolean>(false);
  const {register, errors, handleSubmit, setValue, reset, trigger,setError} = useForm();
  const [defaultText, setDefaultText] = useState<string>();

  useEffect(() => {
    if(selectedEmailTemplate){
      reset();
      setDefaultValue(selectedEmailTemplate)
    }
  }, [selectedEmailTemplate])


  useEffect(() => {
    if(draftEmail){
      if(isListUpdated){
        getSelectedEmailTemplate(selectedId.toString());
        setSelectedEmailTemplate(selectedId.toString());
        dispatch({ type: RequestEmailTemplateActionsType.SetListUpdated, payload: false})
      }else if(draftEmail.emailTemplateId){
        setDefaultValue(draftEmail);
        setSelectedEmailTemplate(draftEmail.emailTemplateId);
        dispatch({type: RequestEmailTemplateActionsType.SelectedEmailId, payload: draftEmail.emailTemplateId});
        let data = emailTemplates.find(item => item.id === draftEmail.emailTemplateId)
        setselectedEmailTemplateName(data?.templateName)
      }else if(draftEmail.change === true && !isEdit){
        getSelectedEmailTemplate(selectedEmailTemplate.id.toString());
        setSelectedEmailTemplate(selectedEmailTemplate.id.toString());
      }else if(draftEmail.change === true && isEdit){
       return;
      }
      else{
        getSelectedEmailTemplate(emailTemplates[0]?.id?.toString());
        setSelectedEmailTemplate(emailTemplates[0]?.id?.toString());
      }
     
    }
  }, [draftEmail])

  useEffect(() => {
    if(emailContent){
      setDefaultValue(emailContent);
      setSelectedEmailTemplate(emailContent.emailTemplateId);
    }else if(!selectedEmailTemplate && !draftEmail){
      getSelectedEmailTemplate(emailTemplates[0]?.id?.toString());
      setSelectedEmailTemplate(emailTemplates[0]?.id?.toString());
    }
  }, [])

  useEffect(() => {
  if(documentsName && (selectedEmailTemplate || emailContent)){
    if(isEdit){
      setDefaultValue(emailContent)
    }else{
      if(selectedEmailTemplate)
       setDefaultValue(selectedEmailTemplate)
    }
   
  }
  }, [documentsName])

  const getSelectedEmailTemplate = async (id?: string) => {
    let loanApplicationId = LocalDB.getLoanAppliationId();
    let result = await RequestEmailTemplateActions.fetchDraftEmailTemplate(id, loanApplicationId)
    dispatch({type:RequestEmailTemplateActionsType.SetSelectedEmailTemplate, payload: result})
  }

  const setDefaultValue = (template: any) => {
      let ccEmail = template.ccAddress?.split(',');
      let fromEmail = template.fromAddress?.split(',');
      let toEmail = template.toAddress?.split(',');
      let emailBodyContent = template.emailBody?.replace('###RequestDocumentList###', documentsName ? documentsName: '');

      setValue('fromEmail', template.fromAddress?.toString());
      setValue('toEmail', template.toAddress?.toString());
      setValue('cCEmail', template.ccAddress?.toString());
      setValue('subjectLine', template.subject?.toString());
      setSubject(template.subject?.toString());   
      setValue('emailBody', emailBodyContent?.toString());
      setCCEmail(template.ccAddress?.toString());
      setFromEmail(template.fromAddress?.toString());
      setToEmail(template.toAddress?.toString())
      setEmailBody(emailBodyContent?.toString());
      setDefaultText(emailBodyContent?.toString());
  
      if(ccEmail && template.ccAddress != "" && template.toAddress != "null")
       setCCEmailArray(ccEmail.filter( (x: string) => x != ""));
      if(fromEmail && template.fromAddress != "" && template.toAddress != "null")
       setFromEmailArray(fromEmail.filter( (x: string) => x != ""));
      if(toEmail && template.toAddress != "" && template.toAddress != "null")
       setToEmailArray(toEmail.filter( (x: string) => x != ""));

       let mappedEmailContent = {
          emailTemplateId : template.id != undefined ? template.id : template.emailTemplateId,
          fromAddress: template.fromAddress != "" ? template.fromAddress : null,
          toAddress: template.toAddress != "" ? template.toAddress : null,
          ccAddress: template.ccAddress != "" ? template.ccAddress : null,
          subject: template.subject,
          emailBody: emailBodyContent
        }
       
        dispatch({
         type: RequestEmailTemplateActionsType.SetEmailContent,
         payload: mappedEmailContent
       });
  };

  const handlerFromEmail = (email: string[]) => {
    setFromEmail(email.toString());
    setFromEmailArray(email);
    setValue('fromEmail', email.toString(), {shouldValidate: true});
    let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }  
    emailContentDetail.fromAddress = email.toString();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
  };

  const handlerToEmail = (email: string[]) => {
    console.log('CreateTomailTemplate', email);
    setToEmail(email.toString());
    setToEmailArray(email);
    setValue('toEmail', email.toString(), {shouldValidate: true});
    let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }
    emailContentDetail.toAddress = email.toString();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
  };

  const handlerCCEmail = (email: string[]) => {
    setCCEmail(email.toString());
    setCCEmailArray(email);
    setValue('cCEmail', email.toString(), {shouldValidate: true});
    let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }
    emailContentDetail.ccAddress = email.toString();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
  };

  const onChnageTextEditor = (content: string) => {
    setEmailBody(content);
    setValue('emailBody', content, {shouldValidate: true});
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
    let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }
    emailContentDetail.emailBody = content.toString();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
  };

  const onBlurTextEditor = (content: string) => {
    setEmailBody(content);
    setValue('emailBody', content, {shouldValidate: false});
    let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
  };

  const onBlurSubjectHandler = (event: any) => {
   let subject = event.target.value;
   let emailContentDetail : any;
    if(emailContent){
      emailContentDetail = emailContent;
    }else{
      emailContentDetail = {};
    }
    emailContentDetail.subject = subject.toString();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEmailContent,
      payload: emailContentDetail
    });
    dispatch({ type: RequestEmailTemplateActionsType.SetEdit, payload: true})
  };

  const onSubmit = (data: any, e: any) => {
    if(data.fromEmail.split(',').length > 1){
      setError('fromEmail', {type: "validate", message: "Only one email is allowed in from address."});
      return;
    }
    resetFields();
    setSendBtnDisable(true);
    saveAsDraft(false);
    dispatch({
      type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate,
      payload: []
    });
  };

  const subjectOnchangeHandler = (event: any) => {
    let value = event.target.value;
    setSubject(value);
  }

  const resetFields = () => {
    setFromEmailArray([]);
    setToEmailArray([]);
    setCCEmailArray([]);  
    setDefaultText('<p></p>');
  }

  const resetValidations = () => {

  }

  const errorHandlerFromEmail = () => {
   console.log('----------> errorHandlerFromEmail')
   trigger('fromEmail');
  
   setError('fromEmail', {type: "validate", message: "Dont Forget Your Username Should Be Cool!"});
  }

  const errorHandlerToEmail = () => {
    console.log('----------> errorHandlerToEmail')
    trigger('toEmail');
  }

  const errorHandlerCCEmail = () => {
    console.log('----------> errorHandlerCCEmail')
    trigger('cCEmail');
  }

  const sendRequestButton = () => {
    if (showSendButton) {
      return (
        <>
          <footer className="mcu-panel-footer text-right">
            <button
              onClick={handleSubmit(onSubmit)}
              type="button"
              disabled={isSendBtnDisable}             
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
console.log('---------->----->errors.fromEmail', errors.fromEmail)
  return (
    <>
      <form>
      <div className="mcu-panel-body">
          <div className="grid-form">                   
            <div className={`grid-form-group ${errors.fromEmail ? 'error': ''}`}>
              <label className="grid-form-label">From</label>
              <EmailInputBox
                handlerEmail={handlerFromEmail}
                tokens={tokens}
                exisitngEmailValues={fromEmailArray}
                className={`grid-form-control`}
              />
              <input
                name="fromEmail"
                type="hidden"
                ref={register({
                  required: 'Please enter valid email format'
                })}
                value={fromEmail}
              />
              {errors.fromEmail && (
                <label className="error">{errors.fromEmail.message}</label>
              )}

              {errors.fromEmail && errors.fromEmail.type === "validate" && (
                <label className="error">{errors.fromEmail.message}</label>
              )}
            </div>

            <div className={`grid-form-group ${errors.toEmail ? 'error': ''}`}>
              <label className="grid-form-label">To</label>
              <EmailInputBox
                handlerEmail={handlerToEmail}
                tokens={tokens}
                exisitngEmailValues={toEmailArray}
                className={`grid-form-control`}
              />
              <input
                name="toEmail"
                type="hidden"
                ref={register({
                  required: 'Please enter valid email format'
                })}
                value={toEmail}
              />
              {errors.toEmail && (
                <label className="error">{errors.toEmail.message}</label>
              )}
            </div>

            <div className={`grid-form-group ${errors.cCEmail ? 'error': ''}`}>
              <label className="grid-form-label">CC</label>
              <EmailInputBox
                handlerEmail={handlerCCEmail}
                tokens={tokens}
                exisitngEmailValues={cCEmailArray}
                className={`grid-form-control`}
              />
              <input
                name="cCEmail"
                type="hidden"               
                value={cCEmail}
              />
              {errors.cCEmail && (
                <label className="error">{errors.cCEmail.message}</label>
              )}
            </div>

            <div className={`grid-form-group ${errors.subjectLine ? 'error': ''}`}>
              <label className="grid-form-label">Subject</label>
              <input
                placeholder={`Input your Subject`}
                maxLength = {250}
                name="subjectLine"
                type="text"             
                className={`grid-form-control`}
                ref={register({
                  required: 'Subject is required.',
                  minLength : {
                    value: 2,
                    message: 'Minimum length is 2 character.'
                  }
                })}
                onBlur={(e: any) => onBlurSubjectHandler(e)}
                value={subject}
                onChange= {(e) => subjectOnchangeHandler(e)}
              />

              {errors.subjectLine && (
                <label className="error">{errors.subjectLine.message}</label>
              )}
            </div>
          </div>
          <TextEditor
            handlerOnChange={onChnageTextEditor}
            defaultText = {defaultText}
            className={errors.emailBody ? 'error' : ''}
            onBlurTextEditor = {onBlurTextEditor}
          />
          {errors.emailBody && (
            <label className="error">{errors.emailBody.message}</label>
          )}
          <input
            name="emailBody"
            type="hidden"
            ref={register({
              required: 'Email body is required.'
            })}
            value={emailBody}
          />
           
        </div>
        
        {sendRequestButton()}
      </form>
    </>
  );
};
