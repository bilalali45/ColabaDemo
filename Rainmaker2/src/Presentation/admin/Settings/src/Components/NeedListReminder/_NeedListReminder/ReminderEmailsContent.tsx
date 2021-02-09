import React, { useContext, useEffect, useRef, useState } from 'react';
import ContentBody from '../../Shared/ContentBody';
import { EmailInputBox } from '../../Shared/EmailInputBox';
import { TextEditor } from '../../Shared/TextEditor';
import { Store } from '../../../Store/Store';
import { RequestEmailTemplate } from '../../../Entities/Models/RequestEmailTemplate';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { Tokens } from '../../../Entities/Models/Token';
import { disableBrowserPrompt, enableBrowserPrompt } from '../../../Utils/helpers/Common';
import { useForm } from 'react-hook-form';
import ContentFooter from '../../Shared/ContentFooter';
import { ReminderEmailListActions } from '../../../Store/actions/ReminderEmailsActions';
import ReminderSettingTemplate, { ReminderEmailTemplate } from '../../../Entities/Models/ReminderEmailListTemplate';

type props = {
 
  insertTokenClick?: Function;
  showinsertToken?: boolean;
  setSelectedField: Function;
  showFooter: boolean;
  setShowFooter: Function;
  setCancelClick: Function;
};

export const ReminderEmailsContent = ({
  insertTokenClick,
  setSelectedField,
  showFooter,
  setShowFooter,
  setCancelClick
}: props) => {
  const { state, dispatch } = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const reminderEmailManger: any = state.emailReminderManager;
  const allToken: any = emailTemplateManger.tokens;
  const emailTemplates: RequestEmailTemplate[] = emailTemplateManger.requestEmailTemplateData;
  const token: Tokens = emailTemplateManger.selectedToken;
  const selectedEmailTemplate = emailTemplateManger.selectedEmailTemplate;
  const isFieldEdited = emailTemplateManger.editedFields;
  const selectedreminderEmail = reminderEmailManger.selectedReminderEmail;
  const reminderEmailData = reminderEmailManger.reminderEmailData;

  const [fromEmail, setFromEmail] = useState<string>();
  const [fromEmailArray, setFromEmailArray] = useState<string[]>([]);
  const [cCEmail, setCCEmail] = useState<string>();
  const [cCEmailArray, setCCEmailArray] = useState<string[]>([]);
  const [emailBody, setEmailBody] = useState<string>();
  const [tokens, setValidTokens] = useState<Tokens[]>([]);
  const [defaultText, setDefaultText] = useState<string>();
  const [slectionPos, setSelectionPos] = useState<number | null>(0);
  const [isError, setIsError] = useState<string>();
  const [validSubject, setvalidSubject] = useState<string>("");
  const { register, errors, handleSubmit, setValue, getValues, formState, trigger, setError, clearErrors } = useForm({
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
    if (selectedreminderEmail) {
      if (selectedreminderEmail.email) {
        resetFields();
        setTimeout(() => {
          setDefaultValue(selectedreminderEmail.email);
        }, 20)
      } else {
        resetFields();
      }

    }
  }, [selectedreminderEmail]);

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
      setValidTokens(allToken);
    }
  }, [allToken]);

  const getTokens = async () => {
    let tokens = await ReminderEmailListActions.fetchTokens();
    if (tokens) {
      dispatch({
        type: RequestEmailTemplateActionsType.SetTokens,
        payload: tokens
      });
    }
  };

  const createEmailTemplate = async (templateData: ReminderSettingTemplate) => {
    let result = await ReminderEmailListActions.addReminderEmails(templateData);
    if (result === 200) {
      setCancelClick(true);
      selectedreminderEmail.isEditMode = false;
    }
  };

  const updateEmailTemplate = async (templateData: ReminderSettingTemplate) => {
    let result = await ReminderEmailListActions.updateReminderEmails(templateData);
    if (result === 200) {
      setCancelClick(true);
      selectedreminderEmail.isEditMode = false;
    }
  };

  const setInputError = (inputName: string, message?: string) => {
   
    if (inputName === "fromEmail") {
      if (fromEmail != undefined && fromEmail != "") {
        if(fromEmail.split(',').length > 1){
          setError(inputName, {
            type: "custom",
            message: message ?? "Only one email is allowed in from address.",
          });
          setIsError(inputName);
        }else{
          setIsError('');
        }
      } else {
        setError(inputName, {
          type: "custom",
          message: message ?? "Please enter valid format",
        });
        setIsError(inputName);
      }
    } else {
      setError(inputName, {
        type: "custom",
        message: message ?? "Please enter valid format",
      });
      setIsError(inputName);
    }
  }

  const triggerInputValidation = (inputName: string, isEmailValid: boolean) => {
    if (isEmailValid) {
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
      setValue('emailBody', concatVal, { shouldValidate: true });
    } else {
      setShowFooter(true);
      selectedreminderEmail.isEditMode = true;
      const prevValues = getValues(target);
      if (prevValues.length < 250) {
        if (prevValues) {
          let firstPart = prevValues.substring(0, slectionPos);
          let secondPart = prevValues.substring(slectionPos);
          setValue(target, firstPart + value + secondPart);
          setSelectionPos(getValues(target).length);
          setSubjectWithValidation(firstPart + value + secondPart);
        } else {
          setValue(target, value);
          setSelectionPos(getValues(target).length);
          setSubjectWithValidation(value)
        }
      }
    }
    dispatch({ type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null });
  };

  const onBlurSubjectHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    const startPos = event.target.selectionStart;
    setSelectionPos(startPos);
  }

  const setDefaultValue = (template: RequestEmailTemplate) => {
    let ccEmail = template.ccAddress != "" ? template.ccAddress?.split(',') : null;
    let fromEmail = template.fromAddress != "" ? template.fromAddress?.split(',') : null;
    setSubjectWithValidation(template.subject?.toString() || "");
    setValue('fromEmail', template.fromAddress?.toString());

    setValue('cCEmail', template.ccAddress?.toString());
    setValue('emailBody', template.emailBody?.toString());
    setCCEmail(template.ccAddress?.toString());
    setFromEmail(template.fromAddress?.toString());
    setEmailBody(template.emailBody?.toString());
    if (ccEmail) {
      setCCEmailArray(ccEmail.filter((x: string) => x != ""));
    }

    if (fromEmail) {
      setFromEmailArray(fromEmail.filter((x: string) => x != ""));
    }
    setDefaultText('<p></p>');
    setTimeout(() => {
      setDefaultText(template.emailBody?.toString());

    }, 20);

  };

  const resetFields = () => {
    clearErrors();
    setSubjectWithValidation("");
    setFromEmail('')
    setCCEmail('');
    setFromEmailArray([]);
    setCCEmailArray([]);
    setDefaultText('<p></p>');
    setEmailBody('');
  }

  const handlerFromEmail = (email: string[]) => {
    enableBrowserPrompt();
    setFromEmail(email.toString());
    setFromEmailArray(email);
    setValue('fromEmail', email.toString(), { shouldValidate: true });
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
    setShowFooter(true);
    selectedreminderEmail.isEditMode = true;
  };

  const handlerCCEmail = (email: string[]) => {
    enableBrowserPrompt();
    setCCEmail(email.toString());
    setCCEmailArray(email);
    setValue('cCEmail', email.toString(), { shouldValidate: true });
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
    setShowFooter(true);
    selectedreminderEmail.isEditMode = true;
  };

  const onChnageTextEditor = (content: string) => {
    enableBrowserPrompt();
    setEmailBody(content);
    if (content === "") {
      setValue('emailBody', content, { shouldValidate: false });
    } else {
      setValue('emailBody', content, { shouldValidate: true });
    }
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
    setShowFooter(true);
    selectedreminderEmail.isEditMode = true;
  };

  const handlerClick = (clickOn: string) => {
    if (insertTokenClick) {
      insertTokenClick(true);
      setLastSelectedInput(clickOn);
      setSelectedField(clickOn);
    }
  };

  const onSubmit = (data: any) => {
    if (isError) {
      if (isError === 'fromEmail') {
        setInputError('fromEmail');
        return;
      } else if (isError === 'cCEmail') {
        setInputError('cCEmail');
        return;
      }
    }

    if (data.fromEmail.split(',').length > 1) {
      setError('fromEmail', { type: "validate", message: "Only one email is allowed in from address." });
      return;
    }
    setDisableSaveBtn(true);
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
    });
    if (selectedreminderEmail && selectedreminderEmail.id) {
      let SeletedListData = reminderEmailData.filter((item: ReminderSettingTemplate) => item.id === selectedreminderEmail.id);
      data.id = selectedreminderEmail.id;
      selectedreminderEmail.noOfDays = Number(SeletedListData[0].noOfDays);
      selectedreminderEmail.recurringTime = SeletedListData[0].recurringTime;
      selectedreminderEmail.email = [new ReminderEmailTemplate(selectedreminderEmail.email.id, data.fromEmail, data.cCEmail, data.subjectLine, data.emailBody)];
      updateEmailTemplate(selectedreminderEmail);
      setShowFooter(false);
    } else {
      let SeletedListData = reminderEmailData.filter((item: ReminderSettingTemplate) => item.id === '');
      selectedreminderEmail.noOfDays = Number(SeletedListData[0].noOfDays);
      //selectedreminderEmail.noOfDays = Number(selectedreminderEmail.noOfDays);
      selectedreminderEmail.email = [new ReminderEmailTemplate("", data.fromEmail, data.cCEmail, data.subjectLine, data.emailBody)];
      createEmailTemplate(selectedreminderEmail);
      setShowFooter(false);
    }

    disableBrowserPrompt();
  };

  const handlerOnFocusOnTextEditor = () => {
    if (insertTokenClick) {
      insertTokenClick(true);
      setLastSelectedInput('textEditor');
      setSelectedField('textEditor');
    }

  };

  const addFromToken = (token: string) => {
    let fromArray = [...fromEmailArray];
    if (fromArray.find(x => x == token) == undefined) {
      fromArray.push(token);
      setFromEmailArray((oldArray) => [...oldArray, token]);
      setFromEmail(fromArray.toString());
      setValue('fromEmail', fromArray.toString(), { shouldValidate: true });
      setIsError('');
    }
    setShowFooter(true);
    if (selectedreminderEmail?.isActive) {
      selectedreminderEmail.isEditMode = true;
    };
  }


  const addCCToken = (token: string) => {
    let ccArray = [...cCEmailArray];
    if (ccArray.find(x => x == token) == undefined) {
      ccArray.push(token);
      setCCEmailArray((oldArray) => [...oldArray, token]);
      setCCEmail(ccArray.toString());
      setValue('cCEmail', ccArray.toString(), { shouldValidate: true });
    }
    setShowFooter(true);
    selectedreminderEmail.isEditMode = true;
  };

  const setSubjectWithValidation = (text?: string) => {
    var str = removeSpecialChars(text)
    setvalidSubject(str ? str : '');
    clearInputError('subjectLine');
  }
  const removeSpecialChars = (text?: string) => {
    return text?.replace(/[^ -~]/gi, "");
  }
  const onChangeHandler = (event?: any, field?: string) => {
    let val = event?.target.value;
    setSubjectWithValidation(val);
    enableBrowserPrompt();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: true
    });
    setShowFooter(true);
    selectedreminderEmail.isEditMode = true;
  }

  const cancelHandler = () => {
    disableBrowserPrompt();
    setShowFooter(false);
    setCancelClick(true);
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields, payload: false
    });
  }


  return (
    <>
      <ContentBody className={`nlre-settings-body ${selectedreminderEmail?.isActive ? '':'makeFreez'} ${(showFooter && selectedreminderEmail?.isActive) ? 'footerEnabled' : 'footerDisabled'}`}>       
        <div className="row">
          <div data-testid="dv-fromAddress" className="col-md-12 form-group">
            <label className="settings__label">From Address</label>
            <EmailInputBox
              id="fromEmail"
              handlerEmail={handlerFromEmail}
              handlerClick={() => handlerClick('fromAddress')}
              tokens={tokens}
              exisitngEmailValues={fromEmailArray}
              className={errors.fromEmail ? 'error' : ''}
              dataTestId={'from-email'}
              setInputError={setInputError}
              triggerInputValidation={triggerInputValidation}
              clearInputError={clearInputError}
            />
            {errors.fromEmail && (
              <label data-testid="fromEmail-error" className="error">{errors.fromEmail.message}</label>
            )}
            <input
              data-testid="fromEmail-ref"
              name="fromEmail"
              type="hidden"
              ref={register({
                required: 'From email is required.',
              })}
              value={fromEmail}
            />

          </div>
          <div data-testid="dv-ccAddress" className="col-md-12 form-group">
            <label className="settings__label">CC Address</label>
            <EmailInputBox
              id="cCEmail"
              handlerEmail={handlerCCEmail}
              handlerClick={() => handlerClick('ccAddress')}
              tokens={tokens}
              exisitngEmailValues={cCEmailArray}
              className={errors.cCEmail ? 'error' : ''}
              dataTestId={'cc-email'}
              setInputError={setInputError}
              triggerInputValidation={triggerInputValidation}
              clearInputError={clearInputError}
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
              data-testid="subject-input"
              id="subjectLine"
              maxLength={250}
              name="subjectLine"
              value={validSubject}
              type="text"
              className={`settings__control  ${errors.subjectLine ? 'error' : ''
                }`}
              ref={register({
                required: 'Subject is required.',

              })}
              onFocus={(e) => {
                if (insertTokenClick)
                  insertTokenClick(true);
                setLastSelectedInput('subjectLine');
                setSelectedField('subjectLine');

              }}
              onChange={(e: any) => { onChangeHandler(e, "subjectLine") }}
              onBlur={onBlurSubjectHandler}

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
          handlerOnChange={(e: string) => onChnageTextEditor(e)}
          defaultText={defaultText}
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
      {showFooter && selectedreminderEmail?.isActive &&
        <ContentFooter>
          <button
            disabled={!selectedreminderEmail?.isActive}
            type="button"
            data-testid="save-btn"
            onClick={handleSubmit(onSubmit)}
            className={`settings-btn settings-btn-primary ${selectedreminderEmail?.isActive ? '' : 'disabled'}`}>
            Save
              </button>
          <button
            type="button"
            disabled={!selectedreminderEmail?.isActive}
            data-testid="cancel-btn"
            onClick={() => {
              cancelHandler();
            }}
            className={`settings-btn settings-btn-secondry ${selectedreminderEmail?.isActive ? '' : 'disabled'}`}
          >
            Cancel
              </button>
        </ContentFooter>
      }

    </>
  )
}
