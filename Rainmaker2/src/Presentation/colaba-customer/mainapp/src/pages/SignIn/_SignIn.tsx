import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useForm, Controller } from "react-hook-form";
import InputMask from "react-input-mask";
import { SVGEmail, SVGLock, SVGMobile } from "../../Shared/Components/SVGs";
import {
  ArtWorkBoyCoding,
  ArtWorkPhoneVerification,
} from "../../Shared/Components/Artwork";
import { Store } from "../../Store/Store";
import Input from "../../components/InputField";
import { LocalDB } from "../../lib/localStorage";
import { UserActions } from "../../Store/actions/UserActions";
import { PopupModal } from "../../Shared/Components/modal";
import { checkValidUSNumber, UnMaskPhone } from "../../../Utilities/helpers/PhoneMasking";

interface step1Props {
  loginHandler: (data:any) => void;
  error?: string;
}
interface step2Props {
  agreeAndContinueHandler: (data:any) => void;
  backHandler: () => void;
  skipThisStepHandler: Function;
  showSkipLink: boolean;
  error: string;
}
export const SignStep1: React.FC<step1Props> = ({
  loginHandler,
  error,
}: step1Props) => {
  const { state } = useContext(Store);
  const { userInfo }: any = state.user;
  const { register, errors, handleSubmit, setError, clearErrors } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  useEffect(() => {
    if (error) {
      showErrorOnField(
        "email",
        "The user name or password provided is incorrect."
      );
      showErrorOnField("password", "");
    }
  }, [error]);

  const showErrorOnField = (field: string, message: string) => {
    setError(field, {
      type: "server",
      message: message,
    });
  };

  return (
    <div className="row" data-testid="signIn-form-step1">
      <div className="col-md-6">
        <header
          data-testid="signIn-step1-header"
          className="colaba__page-signin-header auth-user-p-header"
        >
          <h1 className="h1">Login To Your Account</h1>
          <span className="tagline">
            Welcome Back! Sign In To Continue Your Mortgage Process.
          </span>

          <br />
        </header>
        <div className="form-group extend">
          <label className="form-label" htmlFor="email">
            <div className="form-icon">
              <SVGEmail />
            </div>{" "}
            <span className="form-text">Email</span>
          </label>
          <input
            autoFocus={true}
            data-testid="signinEmail-input"
            maxLength={100}
            id="email"
            name="email"
            type="text"
            className={`form-control ${errors.email ? "error" : ""}`}
            defaultValue={userInfo.userName}
            ref={register({
              required: "This field is required.",
              pattern: {
                value: /^[A-Z0-9._-]+@[A-Z0-9-]+\.[A-Z]{2,}$/i,
                message: "Please enter a valid email address.",
              },
              maxLength: {
                value: 100,
                message: "Max length limit reached.",
              },
            })}
            onFocus={() => { }}
            onChange={() => {
              clearErrors("email");
            }}
          />
          {errors.email && (
            <span data-testid="email-error" className="form-error">
              {errors.email.message}
            </span>
          )}
        </div>
       
          <Input
            data-testid="signinPassword-input"
            maxLength={100}
            id="password"
            name="password"
            type="password"
            icon={<SVGLock />}
            label={"Password"}
            register={register}
            onChange={() => clearErrors("password")}
            rules={{
              required: "This field is required.",
            }}
            errors={errors}
          />
        
        <span data-testid="forgot-password-link">
          <Link to={`/forgotPassword`} data-testid="forgotpassword-btn" id="forgotpasswordBtn" className="forget-your-password">
            Forgot your password?
          </Link>
        </span>
        <div className="form-group extend">
          <button
            id="loginBtn"
            data-testid="login-btn"
            className="btn btn-primary btn-lg btn-block"
            onClick={handleSubmit(loginHandler)}
            type="submit"
          >
            {" "}
            Login{" "}
          </button>
        </div>
        <label id="dontHaveAccountLink" data-testid="dont-have-account-link" className="text-dark">
          Don't have an account?&nbsp;
        </label>{" "}
        <Link to={`/signup`} data-testid="signUp-link" id="signUpLink">
          Sign Up
        </Link>
      </div>
      <div className="col-md-5 offset-md-1">
        <div className="artwork">
          <ArtWorkBoyCoding />
        </div>
      </div>
    </div>
  );
};

export const SignStep2: React.FC<step2Props> = ({
  agreeAndContinueHandler,
  skipThisStepHandler,
  backHandler,
  showSkipLink,
  error,
}: step2Props) => {
  const { state } = useContext(Store);
  const { userInfo }: any = state.user;
  const {
    register,
    errors,
    handleSubmit,
    clearErrors,
    setError,
    control,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });
  const [showModal, setshowModal] = useState<boolean>(false);
  const [agreementHtml, setAgreementHtml] = useState<string>();
  const [enableAgreeBtn, setEnableAgreeBtn] = useState<boolean>(true);

  useEffect(() => {
    getAgreementHtml();
  }, []);

  useEffect(() => {
    if (error) {
      setError("mobile", {
        type: "server",
        message: error,
      });
    }
  }, [error]);

  const getAgreementHtml = async () => {
    let result = await LocalDB.getcaptchaCode(
      UserActions.getTermConditionAndAgreement,
      2
    );
    setAgreementHtml(result.data);
  };

  const numberChangeHandler = (e: React.FormEvent<HTMLInputElement>) => {
    clearErrors();
    let unmaskedNumber = UnMaskPhone(e.currentTarget.value);
    if (unmaskedNumber.length === 10) {
      let validNumber = checkValidUSNumber(unmaskedNumber); //regex.test(unmaskedNumber);
      if (validNumber) {
        setEnableAgreeBtn(false);
      } else {
        setEnableAgreeBtn(true);
        setError("mobile", {
          type: "server",
          message: "Please enter a valid phone number.",
        });
      }

    } else {
      setEnableAgreeBtn(true);
    }
  }


  return (
    <div className="row" data-testid="signIn-form-step2">
      <div className="col-md-6">
        <header
          data-testid="step2-header"
          className="colaba__page-signin-header auth-user-p-header"
        >
          <h1 className="h1">Phone Verification And Consent To Contact</h1>
          <span className="tagline">
            Enter your phone number below and we'll send you a verification
            code.
          </span>
        </header>

        <div className="form-group">
          <Controller
            autoFocus={true}
            data-testid="phone"
            name="mobile"
            control={control}
            defaultValue={userInfo.phoneNumber}
            placeholder={`(458) 458-879`}
            render={() => (
              <InputMask
                mask="(999) 999-9999"
                onChange={(e) => numberChangeHandler(e)}
                defaultValue={userInfo.phoneNumber}
              >
                {(inputProps:any) => (
                  <Input
                    {...inputProps}
                    icon={<SVGMobile />}
                    label="Mobile Number"
                    name="mobileNumber"
                    rules={{
                      required: {
                        value: true,
                        message: "This field is required.",
                      }
                    }}
                    errors={errors}
                    register={register}
                    type="tel"
                    autoFocus={true}
                    placeholder={`(458) 458-879`}
                    defaultValue={userInfo.phoneNumber}
                    className={`${errors.mobile && 'error'}`}
                  >{errors.mobile && (
                    <span data-testid="phone-error" className="form-error">
                      {errors.mobile.message}
                    </span>
                  )}</Input>
                )}
              </InputMask>
            )}
          />

          
        </div>

        <div className="form-group extend">
          <a
            data-testid="read-agreement-link"
            href="javascript:;"
            onClick={() => {
              setshowModal(true);
            }}
          >
            Read the agreement
          </a>
          <br />
        </div>
        <div className="form-group extend make-seprate-two">
          <button
            disabled={enableAgreeBtn}
            className={`btn btn-primary btn-lg  ${showSkipLink ? "mr-auto" : "btn-block"
              }`}
            data-testid="agree-and-continue-btn"
            onClick={handleSubmit(agreeAndContinueHandler)}
            type="submit"
          >
            {" "}
            Agree and Continue{" "}
          </button>
          {showSkipLink && (
            <a
              data-testid="skip-btn"
              onClick={() => skipThisStepHandler()}
              className="btn-skip"
            >
              Skip this step
            </a>
          )}
        </div>

        <div className="make-seprate-two">
          <button
            data-testid="back-btn"
            onClick={backHandler}
            className="btn btn-back mr-auto"
          >
            <em className="zmdi zmdi-arrow-left"></em> Back
          </button>
          <span>
            <label className="text-dark no-margin">
              Don't have an account?&nbsp;
            </label>{" "}
            <Link to={`/signup/`} data-testid="signup-btn">
              Sign Up
            </Link>
          </span>
        </div>
      </div>
      <div className="col-md-5 offset-md-1">
        <div className="artwork">
          <ArtWorkPhoneVerification />
        </div>
      </div>

      <PopupModal
        dialogClassName={`modal-read-agreement modal-sign-up-read-agreement`}
        size="xl"
        show={showModal}
        handlerShow={() => setshowModal(!showModal)}
        modalHeading={`Privacy and agreement`}
        modalBodyData={agreementHtml}
      />
    </div>
  );
};
