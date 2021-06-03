import React, {useContext, useEffect, useRef, useState} from 'react';
import ContentHeader, {ContentSubHeader} from '../../Shared/ContentHeader';
import 'react-multi-email/style.css';
import {EmailInputBox} from '../../Shared/EmailInputBox';
import {TextEditor} from '../../Shared/TextEditor';
import ContentBody from '../../Shared/ContentBody';
import ContentFooter from '../../Shared/ContentFooter';
import {ErrorMessage} from '@hookform/error-message';
import {useForm} from 'react-hook-form';
import {RequestEmailTemplateActionsType} from '../../../Store/reducers/RequestEmailTemplateReducer';
import {RequestEmailTemplateActions} from '../../../Store/actions/RequestEmailTemplateActions';
import {Store} from '../../../Store/Store';
import {RequestEmailTemplate} from '../../../Entities/Models/RequestEmailTemplate';
import {Tokens} from '../../../Entities/Models/Token';
import { disableBrowserPrompt, enableBrowserPrompt } from '../../../Utils/helpers/Common';


type props = {
  addEmailTemplateClick?: any;
  insertTokenClick?: Function;
  showinsertToken?: boolean;
  setSelectedField: Function;
};

export const CreateEmailTemplates = ({
  addEmailTemplateClick,
  insertTokenClick,
  setSelectedField
}: props) => {
  const {state, dispatch} = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const allToken: any = emailTemplateManger.tokens;
  const emailTemplates: RequestEmailTemplate[] = emailTemplateManger.requestEmailTemplateData;
  const token: Tokens = emailTemplateManger.selectedToken;
  const selectedEmailTemplate = emailTemplateManger.selectedEmailTemplate;
  const isFieldEdited = emailTemplateManger.editedFields;

  const [fromEmail, setFromEmail] = useState<string>();
  const [fromEmailArray, setFromEmailArray] = useState<string[]>([]);
  const [cCEmail, setCCEmail] = useState<string>();
  const [cCEmailArray, setCCEmailArray] = useState<string[]>([]);
  const [emailBody, setEmailBody] = useState<string>();
  const [tokens, setValidTokens] = useState<Tokens[]>([]);
  const [defaultText, setDefaultText] = useState<string>();
  const [slectionPos, setSelectionPos] = useState(0);
  const [isError, setIsError] = useState<string>();
  const [validSubject, setvalidSubject] = useState<string>("");
  const {register, errors, handleSubmit, setValue, getValues, formState, trigger,setError, clearErrors} = useForm({
    mode: 'onSubmit',
    reValidateMode: 'onBlur',
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const [lastSelectedInput, setLastSelectedInput] = useState<string>('');
  const [selectedToken, setSelectedToken] = useState('');
  const [disableSaveBtn, setDisableSaveBtn] = useState<boolean>(false);


  useEffect(() => {
    if (selectedEmailTemplate) {
      setDefaultValue(selectedEmailTemplate);
    }
    
  }, []);

  useEffect(() => {
    if (token) {
      if (lastSelectedInput) {
        
        setValues(lastSelectedInput, token.symbol);
        enableBrowserPrompt();
        dispatch({
          type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
        });
      }
    }
  }, [token]);

  useEffect(() => {
    getTokens();
  }, []);

  useEffect(() => {
    if (allToken) {
      //const tokens = allToken.map((x: Tokens) => x.symbol);
      setValidTokens(allToken);
    }
  }, [allToken]);

  const getTokens = async () => {
    let tokens = await RequestEmailTemplateActions.fetchTokens();
    if (tokens) {
      dispatch({
        type: RequestEmailTemplateActionsType.SetTokens,
        payload: tokens
      });
    }
  };

  const createEmailTemplate = async (templateData: any) => {
    let result = await RequestEmailTemplateActions.insertEmailTemplate(
      templateData.templateName,
      templateData.templateDescription,
      templateData.fromEmail,
      templateData.cCEmail,
      templateData.subjectLine,
      templateData.emailBody
    );
    if (result === 200) {
      addEmailTemplateClick(true);
    }
  };

  const updateEmailTemplate = async (templateData: any) => {
    let result = await RequestEmailTemplateActions.updateEmailTemplate(
      templateData.id,
      templateData.templateName,
      templateData.templateDescription,
      templateData.fromEmail,
      templateData.cCEmail,
      templateData.subjectLine,
      templateData.emailBody
    );
    if (result === 200) {
      addEmailTemplateClick(true);
    }
  };

  const setInputError = (inputName: string, message?: string) => {
    if(inputName === "fromEmail"){
     if(fromEmail != undefined && fromEmail != ""){
        if(fromEmail.split(',').length > 1){
          setError(inputName, {
            type: "custom",
            message: message ?? "Only one email is allowed in from address.",
          });
          setIsError(inputName);
        }else{
          setIsError('');
        }
      }else{
        setError(inputName, {
          type: "custom",
          message: message ?? "Please enter valid format",
        });
        setIsError(inputName);
      }
    }else{
      setError(inputName, {
        type: "custom",
        message: message ?? "Please enter valid format",
      });
      setIsError(inputName);
    }  
  }

  const triggerInputValidation = (inputName: string, isEmailValid: boolean) => {
    if(isEmailValid){
      trigger(inputName);
      setIsError('');
    }
  }

  const clearInputError = (inputName: string) => {
    clearErrors(inputName)
  }

  const setValues = (target: string, value?: string) => {
   
    if (target === 'fromAddress') {
      if (value) addFromToken(value);
    } else if (target === 'ccAddress') {
      if (value) addCCToken(value);
    } else if (target === 'textEditor') {
      if (value) {
        setSelectedToken(value);
      }
      let prevValues: string = '';
      prevValues = getValues('emailBody');
      let concatVal: string = '';
      if (prevValues) {
        concatVal = prevValues + ' ' + value;
      } else {
        concatVal = '' + value;
      }
      setValue(target, concatVal);
      setEmailBody(concatVal);
      setValue('emailBody', concatVal, {shouldValidate: true});
    } else {
      const prevValues = getValues(target);
      if(prevValues.length < 250){
        if(prevValues){ 
            let firstPart = prevValues.substring(0, slectionPos);
            let secondPart = prevValues.substring(slectionPos);
            setValue(target, firstPart + value + secondPart);
            setSelectionPos(getValues(target).length);
            setSubjectWithValidation(firstPart + value + secondPart);              
        }else{
          setValue(target,value);
          setSelectionPos(getValues(target).length);
          setSubjectWithValidation(value)
        }    
      }      
    }
    dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
  };

  const onBlurSubjectHandler = (event: any) => { 
    const startPos = event.target.selectionStart;
    setSelectionPos(startPos);
  }

  const setDefaultValue = (template: RequestEmailTemplate) => {
      let ccEmail = template.ccAddress != "" ? template.ccAddress?.split(',') : null ;
      let fromEmail = template.fromAddress != "" ?  template.fromAddress?.split(',') : null;
      setValue('templateName', template.templateName?.toString());
      setValue('templateDescription', template.templateDescription?.toString());
      //setValue('subjectLine', template.subject?.toString());
      setSubjectWithValidation(template.subject?.toString() || "");
      setValue('fromEmail', template.fromAddress?.toString());
   
      setValue('cCEmail', template.ccAddress?.toString());
      setValue('emailBody', template.emailBody?.toString());
      setCCEmail(template.ccAddress?.toString());
      setFromEmail(template.fromAddress?.toString());
      setEmailBody(template.emailBody?.toString());
      if(ccEmail)
        setCCEmailArray(ccEmail.filter( (x: string) => x != ""));
      if(fromEmail)
        setFromEmailArray(fromEmail.filter( (x: string) => x != ""));
      setDefaultText(template.emailBody?.toString());
  };

  const handlerFromEmail = (email: string[]) => {
    enableBrowserPrompt();
    setFromEmail(email.toString());
    setFromEmailArray(email);
    setValue('fromEmail', email.toString(), {shouldValidate: true});
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
  };
  
  const handlerCCEmail = (email: string[]) => {
    enableBrowserPrompt();
    setCCEmail(email.toString());
    setCCEmailArray(email);
    setValue('cCEmail', email.toString(), {shouldValidate: true});
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
  };

  const onChnageTextEditor = (content: string) => {
    
    enableBrowserPrompt();
    setEmailBody(content);
    if(content === ""){
      setValue('emailBody', content, {shouldValidate: false});
    }else{
      setValue('emailBody', content, {shouldValidate: true});
    }  
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
  };

  const handlerClick = (clickOn: string) => {
    if(insertTokenClick){
      insertTokenClick(true);
      setLastSelectedInput(clickOn);
      setSelectedField(clickOn);
    }
    
  };

  const onSubmit = (data: any) => {  
    if(isError){
      if(isError === 'fromEmail'){
        setInputError('fromEmail');
        return;
      }else if(isError === 'cCEmail'){
        setInputError('cCEmail');
        return;
      }
    }

    if(data.fromEmail.split(',').length > 1){
        setError('fromEmail', {type: "validate", message: "Only one email is allowed in from address."});
        return;
       }
       setDisableSaveBtn(true);
      if(selectedEmailTemplate && selectedEmailTemplate.id){
        data.id = selectedEmailTemplate.id;
        updateEmailTemplate(data);
      }else{
        createEmailTemplate(data);
      }
     
      dispatch({
        type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate,
        payload: null
      });
      disableBrowserPrompt();
      dispatch({
        type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
      });
  };

  const handlerOnFocusOnTextEditor = () => {
    if(insertTokenClick){
      insertTokenClick(true);
      setLastSelectedInput('textEditor');
      setSelectedField('textEditor');
    }
    
  };

  const addFromToken = (token: string) => {
    let fromArray = [...fromEmailArray];
    if(fromArray.find( x => x == token) == undefined){
      fromArray.push(token);
      setFromEmailArray((oldArray) => [...oldArray, token]);
      setFromEmail(fromArray.toString());
      setValue('fromEmail', fromArray.toString(), {shouldValidate: true});
      setIsError('');
    }
  };

  const addCCToken = (token: string) => {
  
    let ccArray = [...cCEmailArray];
    if(ccArray.find( x => x == token) == undefined){
      ccArray.push(token);
      setCCEmailArray((oldArray) => [...oldArray, token]);
      setCCEmail(ccArray.toString());
      setValue('cCEmail', ccArray.toString(), {shouldValidate: true});
    }
  };

  const isTokenExist = (value: string) => {
    return value.includes('###') ? false : true
  }
  const setSubjectWithValidation=(text?: string) =>{
    var str = removeSpecialChars(text)
    setvalidSubject(str ? str : '');
    clearInputError('subjectLine');
  }
  const removeSpecialChars= (text?: string) => {
      return text?.replace(/[^ -~]/gi, "");
    }
  const onChangeHandler = (event?: any, field?: string) => {
    let val = event.target.value;
   
    if(field === "templateName"){
      if(event.target.value.includes('###')){
        trigger(field);
      }else{
        clearErrors(field);
      }    
    }
    if(field === "templateDescription"){
      if(event.target.value.includes('###')){
        trigger(field);
      }else{
        clearErrors(field);
      }    
    }
    if(field === "subjectLine"){
      setSubjectWithValidation(val);
    }
    enableBrowserPrompt();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
  }

  const cancelHandler = () => {
    addEmailTemplateClick(true);
    dispatch({
      type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate,
      payload: null
    });
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
    });
    disableBrowserPrompt();
  }

  return (
    <>
      <ContentSubHeader
        title={''}
        backLinkText={'Back'}
        backLink={ () => {
          addEmailTemplateClick(true);
          disableBrowserPrompt();
        }}
        className="create-email-templates-header"
      ></ContentSubHeader>
      <form data-testid="create-form">
        <ContentBody>
          <div className="row">
            <div data-testid="dv-templateName" className="col-md-5 form-group">
              <label className="settings__label">Template Name</label>
              <input           
                data-testid = "templateName-input"
                maxLength = {100}
                id="templateName"
                name="templateName"
                type="text"
                className={`settings__control ${
                  errors.templateName ? 'error' : ''
                }`}
                
                ref={register({
                  required: 'Template name is required.',
                  validate: isTokenExist,
                  maxLength: {
                    value: 100,
                    message: 'Max length limit reached.'
                  }
                })}
                onFocus={(e) => {
                  if(insertTokenClick)
                   insertTokenClick(false);
                  setLastSelectedInput('');
                  
                }}
                onChange= {(e) => onChangeHandler(e,"templateName")}
              />
              {errors.templateName && errors.templateName.type === "validate" && (
                <label data-testid="token-error" className="error">Cannot add token here</label>
              )}
              {errors.templateName && (
                <label data-testid="templateName-error"  className="error">{errors.templateName.message}</label>
              )}
            </div>
            <div data-testid="dv-templateDesc" className="col-md-7 form-group">
              <label className="settings__label">Template Description</label>
              <input              
                data-testid = "templateName-desc"
                maxLength = {100}
                id="templateDescription"
                name="templateDescription"
                type="text"
                className={`settings__control ${
                  errors.templateDescription ? 'error' : ''
                }`}
                ref={register({
                  required: 'Template description is required.',
                  validate: isTokenExist,
                })}
                onFocus={(e) => {
                  if(insertTokenClick)
                    insertTokenClick(false);
                  setLastSelectedInput('');
                }}
                onChange= {(e) => onChangeHandler(e,"templateDescription")}
              />
              {errors.templateDescription && errors.templateDescription.type === "validate" && (
                <label data-testid="token-error-desc" className="error">Cannot add token here</label>
              )}
              {errors.templateDescription && (
                <label data-testid="templateDesc-error" className="error">
                  {errors.templateDescription.message}
                </label>
              )}
            </div>
            <div data-testid="dv-fromAddress" className="col-md-12 form-group">
              <label className="settings__label">Default From Address</label>
              <EmailInputBox
                id="fromEmail"              
                handlerEmail={handlerFromEmail}
                handlerClick={() => handlerClick('fromAddress')}
                tokens={tokens}
                exisitngEmailValues={fromEmailArray}
                className={errors.fromEmail?'error':''}
                dataTestId = {'from-email'}
                setInputError = {setInputError}
                triggerInputValidation = {triggerInputValidation}
                clearInputError = {clearInputError}
              />
              {errors.fromEmail && (
                <label data-testid="fromEmail-error" className="error">{errors.fromEmail.message}</label>
              )}
              <input
                data-testid = "fromEmail-ref"
                name="fromEmail"
                type="hidden"
                ref={register({
                  required: 'From email is required.',
                })}
                value={fromEmail}
              />
              
            </div>
            <div data-testid="dv-ccAddress" className="col-md-12 form-group">
              <label className="settings__label">Default CC Address</label>
              <EmailInputBox
                id="cCEmail"              
                handlerEmail={handlerCCEmail}
                handlerClick={() => handlerClick('ccAddress')}
                tokens={tokens}
                exisitngEmailValues={cCEmailArray}
                className={errors.cCEmail?'error':''}
                dataTestId = {'cc-email'}
                setInputError = {setInputError}
                triggerInputValidation = {triggerInputValidation}
                clearInputError = {clearInputError}
              />
              {errors.cCEmail && (
                <label data-testid="ccEmail-error" className="error">{errors.cCEmail.message}</label>
              )}
              <input
                name="cCEmail"
                type="hidden"
                ref={register({
                  //required: 'CC Email is required.'
                })}
                value={cCEmail}
              />
            </div>
            <div data-testid="dv-subline" className="col-md-12 form-group">
              <label className="settings__label">Subject Line</label>
              <input        
                data-testid= "subject-input"             
                id="subjectLine"
                maxLength = {250}       
                name="subjectLine"
                value={validSubject}
                type="text"
                className={`settings__control  ${
                  errors.subjectLine ? 'error' : ''
                }`}
                ref={register({
                  required: 'Subject is required.',
                  minLength : {
                    value: 2,
                    message: 'Minimum length is 2 character.'
                  }
                })}
                onFocus={(e) => {
                  if(insertTokenClick)
                   insertTokenClick(true);
                   setLastSelectedInput('subjectLine');
                   setSelectedField('subjectLine');
                }}
                onChange= {(e) => onChangeHandler(e,"subjectLine")}
                onBlur= {onBlurSubjectHandler}
              />
              {errors.subjectLine && (
                <label data-testid="subjectLine-error" className="error">{errors.subjectLine.message}</label>
              )}
             
            </div>
          </div>

          <label data-testid="email-body-label" className="settings__label">Email Body</label>
          <TextEditor
            id="emailBody"          
            selectedToken={selectedToken}
            handlerOnFocus={handlerOnFocusOnTextEditor}
            handlerOnChange={onChnageTextEditor}
            defaultText = {defaultText}
            className={`${errors.emailBody ? 'error' : 'className-for-testid'}`}
          />
          {errors.emailBody && (
            <label data-testid="emailBody-error" className="error">{errors.emailBody.message}</label>
          )}
          <input
           data-testid="emailBody-ref"
            name="emailBody"
            type="hidden"
            ref={register({
              required: 'Email body is required.'
            })}
            value={emailBody}
          />          
        </ContentBody>
        <ContentFooter>
          <button 
           disabled = {disableSaveBtn}
           type="button" 
           data-testid= "save-btn"
           onClick={handleSubmit(onSubmit)} 
           className="settings-btn settings-btn-primary">
            Save
          </button>
          <button
            type="button"
            data-testid= "cancel-btn"
            onClick={() => {
              cancelHandler();     
            }}
            className="settings-btn settings-btn-secondry"
          >
            Cancel
          </button>
        </ContentFooter>
      </form>

        

    </>
  );
};
