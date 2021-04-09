import React, { useContext, useEffect, useRef, useState } from 'react';
import ContentBody from '../../Shared/ContentBody';
import { EmailInputBox } from '../../Shared/EmailInputBox';
import { TextEditor } from '../../Shared/TextEditor';
import { Store } from '../../../Store/Store';
import { RequestEmailTemplate } from '../../../Entities/Models/RequestEmailTemplate';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { Tokens } from '../../../Entities/Models/Token';
import {
  disableBrowserPrompt,
  enableBrowserPrompt
} from '../../../Utils/helpers/Common';
import { useForm } from 'react-hook-form';
import ContentFooter from '../../Shared/ContentFooter';
import LoanStatusUpdateModel, { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { LoanStatusUpdateActionsType } from '../../../Store/reducers/LoanStatusUpdateReducer';
import { LoanStatusUpdateActions } from '../../../Store/actions/LoanStatusUpdateActions';

type props = {
  insertTokenClick?: Function;
  showinsertToken?: boolean;
  setSelectedField: Function;
  disableSaveBtn: boolean;
  setDisableSaveBtn: Function;
  setToStatusError: Function;

};

export const ColabaStatusEmailTemplateContent = ({
  insertTokenClick,
  setSelectedField,
  disableSaveBtn,
  setDisableSaveBtn,
  setToStatusError
}: props) => {
  const { state, dispatch } = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const allToken: any = emailTemplateManger.tokens;
  const token: Tokens = emailTemplateManger.selectedToken;
  const loanStatusManager: any = state.loanStatusManager;
  const selectedLoanStatus: LoanStatus = loanStatusManager.selectedLoanStatus;

  const [fromEmail, setFromEmail] = useState<string>();
  const [fromEmailArray, setFromEmailArray] = useState<string[]>([]);
  const [cCEmail, setCCEmail] = useState<string>();
  const [cCEmailArray, setCCEmailArray] = useState<string[]>([]);
  const [emailBody, setEmailBody] = useState<string>();
  const [tokens, setValidTokens] = useState<Tokens[]>([]);
  const [defaultText, setDefaultText] = useState<string>();
  const [slectionPos, setSelectionPos] = useState(0);
  const [isError, setIsError] = useState<string>();
  const [validSubject, setvalidSubject] = useState<string>('');
  const [showSuccessMsg, setShowSuccessMsg] = useState<boolean>(false);
  const {
    register,
    errors,
    handleSubmit,
    setValue,
    getValues,
    formState,
    trigger,
    setError,
    clearErrors
  } = useForm({
    mode: 'onSubmit',
    reValidateMode: 'onBlur',
    criteriaMode: 'firstError',
    shouldFocusError: true,
    shouldUnregister: true
  });
  const [lastSelectedInput, setLastSelectedInput] = useState<string>('');
  const [selectedToken, setSelectedToken] = useState('');


  useEffect(() => {
    if (selectedLoanStatus) {
      resetFields();
      setTimeout(() => {
        setDefaultValue(selectedLoanStatus);
      }, 20);
    }
  }, [selectedLoanStatus?.fromStatusId]);

  useEffect(() => {
    if (token) {
      if (lastSelectedInput) {
        setValues(lastSelectedInput, token.symbol);
        enableBrowserPrompt();
        dispatch({
          type: RequestEmailTemplateActionsType.SetEditedFields,
          payload: true
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

  const resetFields = () => {
    clearErrors();
    setSubjectWithValidation('');
    setFromEmail('');
    setCCEmail('');
    setFromEmailArray([]);
    setCCEmailArray([]);
    setDefaultText('<p></p>');
    setEmailBody('');
  };

  const getTokens = async () => {
    let tokens = await LoanStatusUpdateActions.fetchTokens();
    if (tokens) {
      dispatch({
        type: RequestEmailTemplateActionsType.SetTokens,
        payload: tokens
      });
    }
  };

  const createEmailTemplate = async (loanStatusData: LoanStatus) => {
    let result = await LoanStatusUpdateActions.addLoanStatusEmail(loanStatusData);
    if (result === 200) {
      setShowSuccessMsg(true);
      fetchLoanStatusUpdate(loanStatusData.id);
      setTimeout(() => {
        setShowSuccessMsg(false);
      }, 1000)
      selectedLoanStatus.EditMode = false;
    }
  };

  const fetchLoanStatusUpdate = async (id?: number) => {
    let loanStatusData: LoanStatusUpdateModel | undefined = await LoanStatusUpdateActions.fetchLoanStatusUpdate();
    dispatch({type: LoanStatusUpdateActionsType.SetLoanStatusData, payload: loanStatusData}); 
    let newData = loanStatusData?.loanStatus?.find((item : LoanStatus) => item.id === id);
    dispatch({type: LoanStatusUpdateActionsType.SetSelectedLoanStatus, payload: newData}); 
  }


  const setInputError = (inputName: string, message?: string) => {
    if (inputName === 'fromEmail') {
      if (fromEmail != undefined && fromEmail != '') {
        if (fromEmail.split(',').length > 1) {
          setError(inputName, {
            type: 'custom',
            message: message ?? 'Only one email is allowed in from address.'
          });
          setIsError(inputName);
        } else {
          setIsError('');
        }
      } else {
        setError(inputName, {
          type: 'custom',
          message: message ?? 'Please enter valid format'
        });
        setIsError(inputName);
      }
    } else {
      setError(inputName, {
        type: 'custom',
        message: message ?? 'Please enter valid format'
      });
      setIsError(inputName);
    }
  };

  const triggerInputValidation = (inputName: string, isEmailValid: boolean) => {
    if (isEmailValid) {
      trigger(inputName);
      setIsError('');
    }
  };

  const clearInputError = (inputName: string) => {
    clearErrors(inputName);
  };

  const setValues = (target: string, value?: string) => {
    setDisableSaveBtn(false);
    selectedLoanStatus.EditMode = true;
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
          setSubjectWithValidation(value);
        }
      }
    }
    dispatch({
      type: RequestEmailTemplateActionsType.SetSelectedToken,
      payload: null
    });
  };

  const onBlurSubjectHandler = (event: any) => {
    const startPos = event.target.selectionStart;
    setSelectionPos(startPos);
  };

  const setDefaultValue = (template: LoanStatus) => {
    let ccEmail =
      template.ccAddress != '' ? template.ccAddress?.split(',') : null;
    let fromEmail =
      template.fromAddress != '' ? template.fromAddress?.split(',') : null;
    setSubjectWithValidation(template.subject?.toString() || '');
    setValue('fromEmail', template.fromAddress?.toString());
    setValue('cCEmail', template.ccAddress?.toString());
    setValue('emailBody', template.body?.toString());
    setCCEmail(template.ccAddress?.toString());
    setFromEmail(template.fromAddress?.toString());
    setEmailBody(template.body?.toString());
    if (ccEmail) setCCEmailArray(ccEmail.filter((x: string) => x != ''));
    if (fromEmail) setFromEmailArray(fromEmail.filter((x: string) => x != ''));
    setDefaultText(template.body?.toString());
  };

  const handlerFromEmail = (email: string[]) => {
    enableBrowserPrompt();
    setFromEmail(email.toString());
    setFromEmailArray(email);
    setValue('fromEmail', email.toString(), { shouldValidate: true });
    setDisableSaveBtn(false);
    selectedLoanStatus.EditMode = true;
  };

  const handlerCCEmail = (email: string[]) => {
    enableBrowserPrompt();
    setCCEmail(email.toString());
    setCCEmailArray(email);
    setValue('cCEmail', email.toString(), { shouldValidate: true });
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields,
      payload: true
    });
    setDisableSaveBtn(false);
    selectedLoanStatus.EditMode = true;
  };

  const onChnageTextEditor = (content: string) => {
    enableBrowserPrompt();
    setEmailBody(content);
    if (content === '') {
      setValue('emailBody', content, { shouldValidate: false });
    } else {
      setValue('emailBody', content, { shouldValidate: true });
    }
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields,
      payload: true
    });
    setDisableSaveBtn(false);
    selectedLoanStatus.EditMode = true;
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
      setError('fromEmail', {
        type: 'validate',
        message: 'Only one email is allowed in from address.'
      });
      return;
    }
    if (selectedLoanStatus.toStatusId === 0) {
      setToStatusError('error');
      return;
    }

    setDisableSaveBtn(true);
    selectedLoanStatus.fromAddress = data.fromEmail;
    selectedLoanStatus.ccAddress = data.cCEmail;
    selectedLoanStatus.subject = data.subjectLine;
    selectedLoanStatus.body = data.emailBody;
    selectedLoanStatus.recurringTime = new Date("2021-01-25T01:26:45.707");
    selectedLoanStatus.isActive = true;

    createEmailTemplate(selectedLoanStatus);

    disableBrowserPrompt();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields,
      payload: false
    });
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
    if (fromArray.find((x) => x == token) == undefined) {
      fromArray.push(token);
      setFromEmailArray((oldArray) => [...oldArray, token]);
      setFromEmail(fromArray.toString());
      setValue('fromEmail', fromArray.toString(), { shouldValidate: true });
      setIsError('');
    }
    selectedLoanStatus.EditMode = true;
  };

  const addCCToken = (token: string) => {
    let ccArray = [...cCEmailArray];
    if (ccArray.find((x) => x == token) == undefined) {
      ccArray.push(token);
      setCCEmailArray((oldArray) => [...oldArray, token]);
      setCCEmail(ccArray.toString());
      setValue('cCEmail', ccArray.toString(), { shouldValidate: true });
    }
    selectedLoanStatus.EditMode = true;
  };

  const setSubjectWithValidation = (text?: string) => {
    var str = removeSpecialChars(text);
    setvalidSubject(str ? str : '');
    clearInputError('subjectLine');
  };
  const removeSpecialChars = (text?: string) => {
    return text?.replace(/[^ -~]/gi, '');
  };
  const onChangeHandler = (event?: any, field?: string) => {
    let val = event.target.value;
    setSubjectWithValidation(val);
    enableBrowserPrompt();
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields,
      payload: true
    });
    setDisableSaveBtn(false);
    selectedLoanStatus.EditMode = true;
  };

  const cancelHandler = () => {
    selectedLoanStatus.EditMode = false;
    let cloneObje = {...selectedLoanStatus}
    dispatch({
      type: LoanStatusUpdateActionsType.SetSelectedLoanStatus,
      payload: {}
    });
    setTimeout(()=> {
      dispatch({
        type: LoanStatusUpdateActionsType.SetSelectedLoanStatus,
        payload: cloneObje
      });
    },10)
    dispatch({
      type: RequestEmailTemplateActionsType.SetEditedFields,
      payload: false
    });
    disableBrowserPrompt();
    setDisableSaveBtn(true);
  };

  return (
    <>
      <ContentBody className="colaba-status-body">
        <div data-testid="ColabaStatusEmailTemplateContentBody">
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
              <label data-testid="fromEmail-error" className="error">
                {errors.fromEmail.message}
              </label>
            )}
            <input
              data-testid="fromEmail-ref"
              name="fromEmail"
              type="hidden"
              ref={register({
                required: 'From email is required.'
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
              <label data-testid="ccEmail-error" className="error">
                {errors.cCEmail.message}
              </label>
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
                minLength: {
                  value: 2,
                  message: 'Minimum length is 2 character.'
                }
              })}
              onFocus={(e) => {
                if (insertTokenClick) insertTokenClick(true);
                setLastSelectedInput('subjectLine');
                setSelectedField('subjectLine');
              }}
              onChange={(e) => onChangeHandler(e, 'subjectLine')}
              onBlur={onBlurSubjectHandler}
            />
            {errors.subjectLine && (
              <label data-testid="subjectLine-error" className="error">
                {errors.subjectLine.message}
              </label>
            )}
          </div>
        </div>

        <label data-testid="email-body-label" className="settings__label">
          Email Body
        </label>
        <TextEditor
          id="emailBody"
          selectedToken={selectedToken}
          handlerOnFocus={handlerOnFocusOnTextEditor}
          handlerOnChange={onChnageTextEditor}
          defaultText={defaultText}
          className={`${errors.emailBody ? 'error' : 'className-for-testid'}`}
        />
        {errors.emailBody && (
          <label data-testid="emailBody-error" className="error">
            {errors.emailBody.message}
          </label>
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
        </div>
      </ContentBody>
      <ContentFooter className={`${showSuccessMsg ? 'alert alert-success':''}`}>
        <div data-testid="ColabaStatusEmailTemplateContentFooter">
        {!showSuccessMsg &&
          <>
            <button
              disabled={disableSaveBtn}
              type="button"
              data-testid="save-btn"
              onClick={handleSubmit(onSubmit)}
              className="settings-btn settings-btn-primary"
            >
              Save
          </button>
            <button
              disabled={disableSaveBtn}
              type="button"
              data-testid="cancel-btn"
              onClick={() => {
                cancelHandler();
              }}
              className="settings-btn settings-btn-secondry"
            >
              Cancel
          </button>
          </>
        }

        {showSuccessMsg &&
          <span>Loan Status Updated successfully!</span>
        }
        </div>
      </ContentFooter>

    </>
  );
};
