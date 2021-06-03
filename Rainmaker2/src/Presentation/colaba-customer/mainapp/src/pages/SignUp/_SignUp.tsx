import React, { ChangeEvent, Fragment, FunctionComponent, useEffect, useState } from "react";
import { ErrorOption, UseFormMethods } from "react-hook-form";
import { Control } from "react-hook-form";

import InputCheckedBox from "../../components/InputCheckedBox";
import Input from "../../components/InputField";
import { SVGUser, SVGEmail, SVGMobile, SVGLock } from "../../Shared/Components/SVGs";
import { InitialState, Actions, PasswordValidationType } from "./reducer";
import { PopupModal } from "../../Shared/Components/modal";
import { LocalDB } from "../../lib/localStorage";
import { UserActions } from "../../Store/actions/UserActions";
import { Link } from "react-router-dom";
import { maskPhoneNumber } from "../../../Utilities/helpers/PhoneMasking";

const stripDashesAndParenthesis = (mobileNumber: string) => mobileNumber && mobileNumber.replace(/[-_() ]/g, "")

interface StepProps
  extends Partial<Pick<UseFormMethods, "register" | "errors">> {
  state: InitialState;
  dispatch: React.Dispatch<Actions>;
  control?: Control<Record<string, any>>;
  setError?: (name: string, error: ErrorOption) => void
  clearErrors?: (name?: string | string[]) => void
}

export const FirstNameLastNameTermsConditions: FunctionComponent<StepProps> = ({
  state,
  register,
  dispatch,
  errors,
  setError,
  clearErrors
}) => {
  const [termsAndConditions, setTermsAndConditions] = useState<boolean>(false);
  

  useEffect(() => {
    async function getTermsConditions() {
      const { data } = await LocalDB.getcaptchaCode(UserActions.getTermConditionAndAgreement, 1);

      dispatch({ type: 'TERMS_CONDITIONS_CONTENT_CHANGE', payload: data })
    }

    getTermsConditions()
  }, [])

  useEffect(() => {
    dispatch({ type: 'MODAL_OPEN_CHANGE', payload: termsAndConditions })
  }, [termsAndConditions])

  return (
    <Fragment>
      <PopupModal
        dialogClassName={`modal-terms-and-conditions`}
        size="xl"
        show={termsAndConditions}
        handlerShow={() => setTermsAndConditions(!termsAndConditions)}
        modalHeading={`Colaba Consumer Terms of Use`}
        modalHeaderData={`<span className="date-time">Last Updated: Jan 22, 2020</span>`}
        modalBodyData={state.termsConditionsContent}
      />
      <div className="form-group extend">
        <Input
          id="firstName"
          icon={<SVGUser />}
          label="First Name"
          name="firstName"
          autoFocus={true}
          value={state.firstName}
          onChange={(event:ChangeEvent<HTMLInputElement>) => {
            if (!event.target.value) {
              if (setError) {
                setError('firstName', { message: 'This field is required.' })
              }
            } else {
              if (clearErrors) {
                clearErrors('firstName')
              }
            }

            if (event.target.value.length > 0 && !/^(?!\s)[A-Za-z\s&(%'.-\s-]*$/.test(event.target.value)) {
              return false
            } else {
              dispatch({ type: "FIRST_NAME_CHANGE", payload: event.target.value })
            }
            return null;
          }}
          register={register}
          errors={errors}
        >
        {/* <PasswordHelper dispatch={dispatch} errorMessages={state.passwordValidation} currentValidation={state.validationPassed} /> */}
        </Input>
      </div>
      <div className="form-group">
        <Input
          id="lastName"
          icon={<SVGUser />}
          label="Last Name"
          name="lastName"
          value={state.lastName}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            const inputValue = event.target.value

            if (!inputValue) {
              if (setError) {
                setError('lastName', { message: 'This field is required.' })  
              }
            } {
              if (clearErrors) {
                clearErrors('lastName')  
              }
            }

            if (inputValue.length > 0 && !/^(?!\s)[A-Za-z\s&(%'.-\s-]*$/.test(inputValue)) {
              return false
            }
            
            dispatch({ type: "LAST_NAME_CHANGE", payload: inputValue })
            return true;
          }}
          register={register}
          errors={errors}
        />
      </div>
      <div className="form-group">
        <InputCheckedBox
          id="termsConditions"
          label="I agree to the"
          name="termsConditions"
          checked={!!state.termsConditions}
          onChange={() =>
            dispatch({
              type: "TERMS_CONDITIONS_CHANGE",
              payload: !state.termsConditions,
            })
          }
          onClick={() => setTermsAndConditions(true)}
          register={register}
        >
          <a id="TermsAndConditions" href="javascript:;" onClick={()=>setTermsAndConditions(!termsAndConditions)}>Terms and Conditions</a>
        </InputCheckedBox>
      </div>
    </Fragment>
  );
};

export const EmailConfirmEmail: FunctionComponent<StepProps> = ({
  state,
  register,
  dispatch,
  errors,
  setError,
  clearErrors
}) => {
  return (
    <Fragment>
      <div className="form-group extend">
        <Input
          id="Email"
          icon={<SVGEmail />}
          label="Email"
          name="email"
          autoFocus={true}
          defaultValue={state.email}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            if (!event.target.value) {
              if (setError) {
                setError('email', { message: 'Email is required.' })  
              }
            } else {
              if (clearErrors) {
                clearErrors('email')  
              }
            }

            dispatch({ type: "EMAIL_CHANGE", payload: event.target.value.trim() })
          }}
          register={register}
          errors={errors}
        />
      </div>
      <div className="form-group extend">
        <Input
          id="confirmEmail"
          icon={<SVGEmail />}
          label="Confirm Email"
          name="confirmEmail"
          defaultValue={state.confirmEmail}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            const inputValue = event.target.value

            if (!inputValue) {
              if (setError) {
                setError('confirmEmail', { message: 'Please enter same email address.' })
              }
            } else {
              if (clearErrors) {
                clearErrors('confirmEmail')
              }
            }

            dispatch({ type: "CONFIRM_EMAIL_CHANGE", payload: inputValue.trim() })
          }}
          onPaste={(event) => event.preventDefault()}
          register={register}
          errors={errors}
        />
      </div>
    </Fragment>
  );
};

export const MobileNumber: FunctionComponent<StepProps> = ({
  state,
  register,
  dispatch,
  errors,
  setError,
  clearErrors
}) => {
  const [showModal, setshowModal] = useState<boolean>(false);
  const [agreementHtml, setAgreementHtml] = useState<string>();

  useEffect(() => {
    const getAgreementHtml = async () => {
      let result = await LocalDB.getcaptchaCode(UserActions.getTermConditionAndAgreement, 2);

      setAgreementHtml(result.data);
    }

    getAgreementHtml();
  }, [])

  useEffect(() => {
    dispatch({ type: 'MODAL_OPEN_CHANGE', payload: showModal })
  }, [showModal])

  return (
    <Fragment>
      <Input
        icon={<SVGMobile />}
        autoFocus={true}
        label="Mobile Number"
        name="mobileNumber"
        errors={errors}
        register={register}
        placeholder={`(458) 458-8789`}
        maxLength={14}
        value={!!state.mobileNumber ? maskPhoneNumber(state.mobileNumber) : ''}
        onChange={(event: ChangeEvent<HTMLInputElement>) => {
          const mobileNumber = stripDashesAndParenthesis(event.target.value)

          if (mobileNumber.length === 0) {
            if (setError) {
              setError('mobileNumber', { message: 'Mobile number is required.' })  
            }
          }
          else {
            if (clearErrors) {
              clearErrors('mobileNumber')  
            }
          }

          if (mobileNumber.length > 0 && !/^[0-9]{1,10}$/g.test(mobileNumber)) {
            return false
          }
          
          dispatch({ type: "MOBILE_NUMBER_CHANGE", payload: mobileNumber })
          return true;
        }}
      />

      <div className="form-group extend">
        <a href="javascript:;" onClick={() => setshowModal(true)} id="readAgreement">Read the agreement</a>
      </div>
      <PopupModal
        dialogClassName={`modal-read-agreement modal-sign-up-read-agreement`}
        size="xl"
        show={showModal}
        handlerShow={() => setshowModal(!showModal)}
        modalHeading={`Privacy and agreement`}
        modalBodyData={agreementHtml}
      />
    </Fragment>
  );
};

const getNumberLetterOrSpecialCharValidation = (errorMessages: PasswordValidationType[]) => {
  const result = errorMessages.filter(errorMessage => {
    if (errorMessage.rule === 'FailedAtLeastOneSpecialCharacter' && errorMessage.result === true) {
      return true
    }

    if (errorMessage.rule === 'FailedAtLeastOneLetter' && errorMessage.result === true) {
      return true
    }

    if (errorMessage.rule === 'FailedAtLeastOneNumber' && errorMessage.result === true) {
      return true
    }

    return false
  })

  if (result.length > 1) {
    return true
  }

  return false
}

const PasswordHelper: FunctionComponent<{ errorMessages: PasswordValidationType[], dispatch: React.Dispatch<Actions>, currentValidation: boolean }> = ({ errorMessages, dispatch, currentValidation }) => {
  const failedMinimumPasswordLength = errorMessages.some(errorMessage => errorMessage.rule === 'FailedMinimumPasswordLength' && errorMessage.result === true)
  const failedHasSeqAndIdenticalCharacters = errorMessages.some(errorMessage => (errorMessage.rule === 'FailedNoSequentialCharacters' || errorMessage.rule === 'FailedNoIdenticalCharacters') && errorMessage.result === true)
  const failedNumberOrLetterOrSpecialCharacter = getNumberLetterOrSpecialCharValidation(errorMessages)

  const validationsPassed = failedMinimumPasswordLength === false && failedHasSeqAndIdenticalCharacters === false && failedNumberOrLetterOrSpecialCharacter === false;

  setTimeout(() => {
    if (validationsPassed) {
      !currentValidation && dispatch({ type: 'VALIDATION_PASSED', payload: true })
    } else {
      !!currentValidation && dispatch({ type: 'VALIDATION_PASSED', payload: false })
    }
  }, 100);

  return validationsPassed ? null : (
    <div className="password-help">
      <div className={`password-help-wrap ${validationsPassed == false && 'failed'}`}>
        <h5>Password help:</h5>
        <ul>
          <li className={`${failedMinimumPasswordLength ? 'failed' : 'success'}`}>At Least 8 characters</li>
          <li className={`${failedNumberOrLetterOrSpecialCharacter ? 'failed' : 'success'}`}>At least 2 of the following
            <ul>
              <li>1 letter (case sensitive)</li>
              <li>1 number</li>
              <li>1 of these Special characters: @!#$%+?=~</li>
            </ul>
          </li>
          <li className={`${failedHasSeqAndIdenticalCharacters ? 'failed' : 'success'}`}>No more than 2 identical or sequential characters (111, aaa,123, abc, !!!)</li>
        </ul>
      </div>
    </div>
  );
}

export const PasswordConfrimPassword: FunctionComponent<StepProps> = ({
  state,
  register,
  dispatch,
  errors,
  clearErrors,
  setError
}) => {
  const [showPasswordHelp, setShowPasswordHelp] = useState(false)

  return (
    <Fragment>
      <Input
        data-testid="reset-password-input"
        maxLength={100}
        id="password"
        icon={<SVGLock />}
        label="Password"
        name="password"
        type="password"
        register={register}
        autoFocus={true}
        onBlur={() => {
          setShowPasswordHelp(false)

          if (state.password.length > 0 && state.validationPassed === false) {
            if (setError) {
              setError('password', { message: 'Password must be matched with given instructions.' })
            }
          }
        }}
        value={state.password}
        onCopy={(event) => event.preventDefault()}
        onChange={(event: ChangeEvent<HTMLInputElement>) => {
          if (clearErrors) {
            clearErrors('password')
          }
          
          if (event.target.value.length === 0) {
            if (setError) {
              setError('password', { message: 'Password is required.' })
            }
            
          }
          // We are hiding password helper on focus out
          // Here if user tries to type, we are showing helper again.
          if (showPasswordHelp === false) {
            setShowPasswordHelp(true)
          }

          dispatch({ type: 'PASSWORD_CHANGE', payload: event.target.value.replace(/\s/g, "") })
        }}
        rules={{ required: true }}
        errors={errors}
      > {showPasswordHelp && state.passwordValidation.length > 0 && state.password && <PasswordHelper dispatch={dispatch} errorMessages={state.passwordValidation} currentValidation={state.validationPassed} />}
      </Input>
 
        <Input
          data-testid="confirm-password-input"
          maxLength={100}
          id="confirmPassword"
          icon={<SVGLock />}
          label="Confirm Password"
          name="confirmPassword"
          type="password"
          register={register}
          rules={{ required: true }}
          onChange={(event: ChangeEvent<HTMLInputElement>) => {
            if (event.target.value.length === 0) {
              if (setError) {
                setError('confirmPassword', { message: "Confirm password field doesn't match." })
              }
            } else {
              if (clearErrors) {
                clearErrors('confirmPassword')
              }
            }

            dispatch({ type: 'CONFIRM_PASSWORD_CHANGE', payload: event.target.value })
          }}
          onCopy={(event) => event.preventDefault()}
          onPaste={(event) => event.preventDefault()}
          errors={errors}
        ></Input>
      
    </Fragment>
  )
}

interface NextSubmitButonProps
  extends React.DetailedHTMLProps<
  React.ButtonHTMLAttributes<HTMLButtonElement>,
  HTMLButtonElement
  > {
  label?: string;
  displaySkipStepButton?: boolean
  onSkip: () => void
}

export const NextSubmitButton: FunctionComponent<NextSubmitButonProps> = ({
  label,
  onClick,
  displaySkipStepButton,
  onSkip,
  ...rest
}) => {
  return (
    <div className="form-group extend handle-skip">
      <button
        className="btn btn-primary btn-lg btn-block"
        type="submit"
        id="submit"
        onClick={onClick}
        {...rest}
      >
        {label}
      </button>
      {displaySkipStepButton && <button type="submit" onClick={onSkip} className="btn-skip">Skip this step</button>}
    </div>
  );
};

export const FormFooter: FunctionComponent<{
  isFirstStep: boolean;
  isLastStep: boolean;
  goBack: () => void;
}
> = ({
  isFirstStep,
  isLastStep,
  goBack,
}) => {
    return (
      <div className="form-group extend">
        <div className="d-flex align-items-center">
          {!isFirstStep && !isLastStep && (
            <button id="goBack" className="btn btn-back mr-auto" onClick={goBack}>
              <em className="zmdi zmdi-arrow-left"></em> Back
            </button>
          )}
          <div className="d-inline-block">
            <label className="text-dark" id="alreadyText">Already have an account?</label>
            <Link to={`/signin`} data-testid="signin-btn" id="Login"> Login</Link>
          </div>
        </div>
      </div>
    );
  };

export const FormHeader: FunctionComponent<{
  title: string;
  description?: string;
}> = ({
  title,
  description,
}) => {
    return (
      <header className="colaba__page-signup-header  auth-user-p-header">
        <h1 className="h1" id="title">{title}</h1>
        <span className="tagline" id="description">{description}</span>
      </header>
    );
  };
