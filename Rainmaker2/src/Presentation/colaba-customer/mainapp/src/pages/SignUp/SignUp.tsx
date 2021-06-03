import React, {
  FunctionComponent,
  useReducer,
  useCallback, useEffect, useState
} from "react";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useHistory } from "react-router-dom";

import {
  ArtWorkCreateYourAccount,
  ArtWorkPhoneVerification,
} from "../../Shared/Components/Artwork";
import { Actions, InitialState, signupReducer, initialState, PasswordValidationType } from "./reducer";
import {
  FirstNameLastNameTermsConditions,
  EmailConfirmEmail,
  MobileNumber,
  NextSubmitButton,
  FormHeader,
  FormFooter,
  PasswordConfrimPassword,
} from "./_SignUp";
import { useFormProgress } from "../../hooks/useformProgress";
import HeaderAuth from "../../components/HeaderAuth";
import { FormSteps } from "./enum";
import { TwoFactorAuth } from "../../components/TwoFactorAuth";
import { LocalDB } from "../../lib/localStorage";
import { UserActions } from "../../Store/actions/UserActions";
import { Tenant2FaSetting } from "../../../Utilities/Enum";
import { checkValidUSNumber } from "../../../Utilities/helpers/PhoneMasking";

const stripDashesAndParenthesis = (mobileNumber: string) => mobileNumber && mobileNumber.replace(/[-_() ]/g, "")

const validationSchema = Yup.object().shape({
  firstName: Yup.string()
    .required("This field is required."),
  // .max(100, "First Name must be at most 100 characters"),
  // .matches(/^[a-zA-Z.-&(%'.-]+$/, "Only alphabets are allowed."),
  lastName: Yup.string()
    .trim()
    .required("This field is required."),
  // .max(100, "Last Name must be at most 100 characters")
  // .matches(/^[a-zA-Z.-&(%'.-]+$/, "Only alphabets are allowed."),
  email: Yup.string()
    .trim()
    .required("This field is required.")
    .matches(
      /^[A-Z0-9._-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
      "Please enter a valid email address."
    ),
  confirmEmail: Yup.string().required("This field is required").oneOf(
    [Yup.ref("email"), null],
    "Please enter same email address."
  ), termsConditions: Yup.bool().oneOf([true], "Please accept terms & conditions."),
  mobileNumber: Yup.string()
    .required("This field is required.")
    .test(
      "len",
      "Please enter a valid phone number.",
      (mobileNumber) => stripDashesAndParenthesis(mobileNumber ?? "").length === 10 && checkValidUSNumber(stripDashesAndParenthesis(mobileNumber ?? ""))
    ),
  password: Yup.string().required("This field is required"),
  confirmPassword: Yup.string().oneOf(
    [Yup.ref("password"), null],
    "Confirm password field doesn't match."
  ),
});

const getButtonLabel = (isLast: boolean, currentStep: number) =>  {
  if (isLast) return "CREATE YOUR ACCOUNT";

  if (
    [
      FormSteps.FIRST_LAST_NAME_TERMS_CONDITIONS,
      FormSteps.EMAIL_CONFIRM_EMAIL,
    ].includes(currentStep)
  ) {
    return "NEXT";
  }

  if (currentStep === FormSteps.MOBILE_NUMBER) {
    return "AGREE AND CONTINUE";
  }

  if (currentStep === FormSteps.PHONE_VERIFICATION_CODE) {
    return "VERIFY";
  }
  return null;
};

const shouldButtonDisable = (
  currentStep: number,
  state: Partial<InitialState>
): boolean => {
  if (state.validationPassed) {
    return false
  }

  if (
    currentStep === FormSteps.FIRST_LAST_NAME_TERMS_CONDITIONS &&
    state.termsConditions === true
  ) {
    return false;
  }

  if (
    [
      FormSteps.EMAIL_CONFIRM_EMAIL,
      FormSteps.MOBILE_NUMBER
    ].includes(currentStep)
  ) {
    return false;
  }

  if (FormSteps.PASSWORD_CONFIRM_PASSWORD === currentStep) {
    return false
  }

  return true;
};

type TitleDescription = {
   title: string; description: string 
}

type TitleDescriptions= {
  [index: string]: TitleDescription;
}
const headerTitleDescription: TitleDescriptions = {
  "0": {
    title: "Create Your Account",
    description: "We're excited to start your mortgage process.",
  },
  "1": {
    title: "Create Your Account",
    description: "We're excited to start your mortgage process.",
  },
  "2": {
    title: "Phone Verification And Consent To Contact",
    description:
      "Enter your phone number below and we'll send you a verification code.",
  },
  "3": {
    title: "",
    description: ""
  },
  "4": {
    title: "Create Your Account",
    description: "We're excited to start your mortgage process.",
  },
};

export const SignUp: FunctionComponent = () => {
  const { register, trigger, errors, control, setError, clearErrors } = useForm({
    resolver: yupResolver(validationSchema)
  });

  const [twoFAIntervalValue, setTwoFAIntervalValue] = useState(0)
  const [enterKeyPressed, setEnterKeyPressed] = useState(false)

  const history = useHistory()

  const [state, dispatch] = useReducer<React.Reducer<InitialState, Actions>>(
    signupReducer,
    initialState
  );

  const resetStateAndMoveToStepOne = () => {
    dispatch({ type: 'RESET_STATE', payload: {} })
    goBack(3)
  }

  const steps = [
    <FirstNameLastNameTermsConditions
      state={state}
      register={register}
      dispatch={dispatch}
      errors={errors}
      setError={setError}
      clearErrors={clearErrors}
    />,
    <EmailConfirmEmail
      state={state}
      register={register}
      dispatch={dispatch}
      errors={errors}
      setError={setError}
      clearErrors={clearErrors}
    />,
    <MobileNumber
      state={state}
      register={register}
      dispatch={dispatch}
      errors={errors}
      control={control}
      setError={setError}
      clearErrors={clearErrors}
    />,
    <TwoFactorAuth
      backHandler={() => goBack()}
      phone={stripDashesAndParenthesis(state.mobileNumber ?? "")}
      email={state.email}
      fromScreen="phone"
      next2FaInSeconds={twoFAIntervalValue}
      afterOtpValidTimeExpireHandler={resetStateAndMoveToStepOne}
      successOnVerifyOtpHandler={() => goForward()}
      fromSignUp={true}
      updateRequestSid={(requestSid) => dispatch({ type: 'REQUEST_SID_CHANGE', payload: requestSid })}
      updateDontAsk2Fa={(dontAsk) => dispatch({ type: 'DONT_ASK_2FA_CHANGE', payload: dontAsk })}
    />,
    <PasswordConfrimPassword
      state={state}
      register={register}
      dispatch={dispatch}
      errors={errors}
      setError={setError}
      clearErrors={clearErrors}
    />,
  ];

  const [currentStep, goForward, goBack] = useFormProgress();
  const isFirstStep = currentStep === FormSteps.FIRST_LAST_NAME_TERMS_CONDITIONS;
  const isLastStep = currentStep === steps.length - 1;

  const onSubmit =
    async (
      event: React.MouseEvent<HTMLButtonElement, MouseEvent> | KeyboardEvent,
      currentStep: number,
      email: string,
      firstName: string,
      lastName: string,
      mobileNumber: string,
    ) => {
      event.preventDefault();
      if (currentStep === FormSteps.FIRST_LAST_NAME_TERMS_CONDITIONS) {
        const response = await trigger([
          "firstName",
          "lastName",
          "termsConditions",
        ]);

        if (response) {
          return goForward();
        }
      } else if (currentStep === FormSteps.EMAIL_CONFIRM_EMAIL) {
        try {
          const response = await trigger(["email", "confirmEmail"]);

          if (response) {
            const { data: { exists } } = await LocalDB.getcaptchaCode(UserActions.doesCustomerAccountExist, email)

            if (exists) {
              return setError('email', { message: "Email already exist." })
            }

            await LocalDB.getcaptchaCode(UserActions.insertContactEmailLog, { firstName, lastName, email })

            if (state.skip2FA.inActiveForAll === true) {
              dispatch({ type: "MOBILE_NUMBER_SKIPPED_CHANGE", payload: true });

              goForward(3);
            } else {
              goForward()
            }
          }
        } catch (error) {
          alert(error.message);
        }
      } else if (currentStep === FormSteps.MOBILE_NUMBER) {
        const response = await trigger(["mobileNumber"]);

        if (response) {
          await LocalDB.getcaptchaCode(UserActions.insertContactEmailPhoneLog, { firstName, lastName, email, phone: `+1${stripDashesAndParenthesis(mobileNumber)}` })
          goForward();
        }
      }
    };

  const { title, description } = headerTitleDescription[currentStep.toString()] as TitleDescription;

  const onSkip = () => {
    dispatch({ type: "MOBILE_NUMBER_SKIPPED_CHANGE", payload: true });
    goForward(2); // skip code verification step and jump to password/confirm passsword step.
  };

  const registerUser = async () => {
    const passwordResponse = await trigger(['password'])
    const response = await trigger(['password', 'confirmPassword'])

    if (response === false || passwordResponse === false) return

    const {
      firstName,
      lastName,
      email,
      mobileNumber: phone,
      password,
      DontAsk2Fa,
      RequestSid,
      mobileNumberSkipped
    } = state

    const payload = {
      firstName,
      lastName,
      email,
      phone: !!phone ? stripDashesAndParenthesis(phone) : null,
      password,
      DontAsk2Fa,
      MapPhoneNumber: mobileNumberSkipped ? false : true,
      RequestSid,
      Skipped2Fa: mobileNumberSkipped
    }

    try {
      const { data } = await LocalDB.getcaptchaCode(UserActions.register, payload);
      const { token, refreshToken, cookiePath } = data;

      LocalDB.storeCookiePath(cookiePath)
      LocalDB.storeAuthTokens(token, refreshToken);

      if (state.DontAsk2Fa === true) {
        await LocalDB.getcaptchaCode(UserActions.dontAsk2Fa, { dntAsk2fa: true });
      }

      history.push('/homepage');
    } catch (error) {
      alert(error.message)
    }
  }

  // get2FACustomerConfig
  useEffect(() => {
    async function get2FACustomerConfig() {
      const { data: { data: { tenantTwoFaStatus } } } = await LocalDB.getcaptchaCode(UserActions.getTenant2FaConfig, true)

      if ([Tenant2FaSetting.UserPrefrences].includes(tenantTwoFaStatus)) {
        dispatch({ type: 'SKIP_2FA_CHANGE', payload: { userPrefrences: true } })
      } else if (tenantTwoFaStatus === Tenant2FaSetting.InActiveForAll) {
        dispatch({ type: 'SKIP_2FA_CHANGE', payload: { inActiveForAll: true } })
      } else if (tenantTwoFaStatus === Tenant2FaSetting.RequiredForAll) {
        dispatch({ type: 'SKIP_2FA_CHANGE', payload: { requiredForAll: true } })
      }
    }

    get2FACustomerConfig()
  }, [])

  // Get get2FaIntervalValue
  useEffect(() => {
    async function get2FaIntervalValue() {
      const { data: { resend2FaIntervalSeconds } } = await LocalDB.getcaptchaCode(UserActions.get2FaIntervalValue, true)

      setTwoFAIntervalValue(resend2FaIntervalSeconds)
    }

    get2FaIntervalValue()
  }, [])

  // Enter key pressed handling
  useEffect(() => {
    if (enterKeyPressed || state.modalOpened) return

    const onEnterKeyPressed = async (event: any) => {
      // While modal is opened, on enter key pressed, closes the modal and moved to next screen
      // This will prevent goin to next screen and forces to press enter key again.
      if (['TermsAndConditions', 'readAgreement'].includes(event.target.id)) {
        return
      }

      if (event.key === 'Enter') {
        if (state.validationPassed) {
          await registerUser()
        }

        setEnterKeyPressed(true)
        await onSubmit(event, currentStep, state.email, state.firstName, state.lastName, state.mobileNumber ?? "")
        setEnterKeyPressed(false)
      }
    }

    window.addEventListener('keyup', onEnterKeyPressed)

    return () => window.removeEventListener('keyup', onEnterKeyPressed)
  }, [state.modalOpened, enterKeyPressed, currentStep, isLastStep, state.password, state.email, state.firstName, state.lastName, state.mobileNumber, state.validationPassed])

  const validatePassword = useCallback(async (password: string) => {
    dispatch({ type: 'PASSWORD_VALIDATION_CHANGE', payload: [] })

    const passwordValidationResult: PasswordValidationType[] = []

    if (password.trim().length < 8) {
      passwordValidationResult.push({
        rule: 'FailedMinimumPasswordLength',
        result: true
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedMinimumPasswordLength',
        result: false
      })
    }

    if (/[0-9]/g.test(password)) {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneNumber',
        result: false
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneNumber',
        result: true
      })
    }

    if (/([A-Z])+([ -~])*/.test(password)) {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneLetter',
        result: false
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneLetter',
        result: true
      })
    }

    if (/[@!#$%+?=~]/g.test(password)) {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneSpecialCharacter',
        result: false
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedAtLeastOneSpecialCharacter',
        result: true
      })
    }

    if (/([a-zA-Z0-9!@#$%^&*()-_+])\1{2}/.test(password)) {
      passwordValidationResult.push({
        rule: 'FailedNoIdenticalCharacters',
        result: true
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedNoIdenticalCharacters',
        result: false
      })
    }

    if (/(?:(?=012|123|234|345|456|567|678|789|abc|bcd|cde|def|efg|fgh|ghi|hij|ijk|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz)\w)+\w/.test(password)) {
      passwordValidationResult.push({
        rule: 'FailedNoSequentialCharacters',
        result: true
      })
    } else {
      passwordValidationResult.push({
        rule: 'FailedNoSequentialCharacters',
        result: false
      })
    }

    dispatch({ type: 'PASSWORD_VALIDATION_CHANGE', payload: passwordValidationResult })
  }, [])

  useEffect(() => {
    validatePassword(state.password)
  }, [state.password])

  return (
    <div data-testid="signup-sreen" className="colaba__page-signup auth-user-p">
      <HeaderAuth />
      <div className="container colaba-form">
        <div className="row">
          <div className={`col-md-${currentStep === FormSteps.PHONE_VERIFICATION_CODE ? 12 : 5}`}>
            {currentStep !== FormSteps.PHONE_VERIFICATION_CODE && (
              <FormHeader
                title={title}
                description={!!description ? description : ""}
              />
            )}

            {steps[currentStep]}

            {currentStep !== FormSteps.PHONE_VERIFICATION_CODE && (
              <NextSubmitButton
                label={getButtonLabel(isLastStep, currentStep) ?? undefined}
                onClick={async (event) => {
                  if (state.validationPassed) {
                    setEnterKeyPressed(true)
                    await registerUser()
                    setEnterKeyPressed(false)

                    return
                  }

                  setEnterKeyPressed(true)
                  await onSubmit(event, currentStep, state.email, state.firstName, state.lastName, state.mobileNumber ?? "")
                  setEnterKeyPressed(false)
                }}
                disabled={enterKeyPressed || shouldButtonDisable(currentStep, state)}
                displaySkipStepButton={currentStep === FormSteps.MOBILE_NUMBER && (state.skip2FA.userPrefrences === true)}
                onSkip={onSkip}
              />
            )}

            {currentStep !== FormSteps.PHONE_VERIFICATION_CODE && <FormFooter isLastStep={isLastStep} isFirstStep={isFirstStep} goBack={() => goBack()} />}
          </div>

          {currentStep !== FormSteps.PHONE_VERIFICATION_CODE && (
            <div className="col-md-5 offset-md-2">
              <div className={`artwork ${"step-" + currentStep}`}>
                
                {[
                  FormSteps.FIRST_LAST_NAME_TERMS_CONDITIONS,
                  FormSteps.EMAIL_CONFIRM_EMAIL,
                ].includes(currentStep) && <ArtWorkCreateYourAccount />}
                
                {[
                  FormSteps.MOBILE_NUMBER,
                ].includes(currentStep) && <ArtWorkPhoneVerification />}

                {currentStep === FormSteps.PASSWORD_CONFIRM_PASSWORD && <ArtWorkCreateYourAccount />}
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};
