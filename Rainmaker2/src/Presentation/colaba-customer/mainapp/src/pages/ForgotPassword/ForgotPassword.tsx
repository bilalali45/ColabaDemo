import React, { useCallback, useContext, useState } from "react";
import _ from "lodash/fp";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { SVGEmail } from "../../Shared/Components/SVGs";
import { UserActions } from "../../Store/actions/UserActions";
import HeaderAuth from "../../components/HeaderAuth";
import { ArtWorkPasswordAssistance } from "../../Shared/Components/Artwork";
import InputField from '../../components/InputField';
import { LocalDB } from "../../lib/localStorage";
import { ErrorHandler } from "../../../Utilities/helpers/ErrorHandler";
import { Store } from "../../Store/Store";
import { ErrorView } from "../../components/ErrorView";
import SuccessMessage from "./_SuccessMessage";

type formData = {
  email: string;
}
const ForgotPassword = () => {
  const {
    register,
    errors,
    handleSubmit,
    clearErrors
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const [successMessage, setSuccessMessage] = useState<boolean>(false);
  const [userNotExistMsg, setUserNotExistMsg] = useState<boolean>(false)
  const { state, dispatch } = useContext(Store);

  const error: any = state.error;
  const errorObj = error.error;


  const onSubmit = useCallback(async (data: formData) => {

    let res = await LocalDB.getcaptchaCode(UserActions.forgotPassword, data.email);
    if (res) {
      if (ErrorHandler.successStatus.includes(res.statusCode)) {
        setSuccessMessage(true);
      }
      else {
        ErrorHandler.setError(dispatch, res);
        if (res?.response?.status === 400 && res?.response?.data?.message === "User does not exist") {
          setUserNotExistMsg(true)
        }

      }
    }


  }, [])

  const forgotPasswordContent = () => {
    return (
      <div data-testid="forgot-password" id="forgot-password" className="colaba__page-forget-password auth-user-p">
        {errorObj && errorObj.message && <ErrorView />}
        <HeaderAuth className="colaba__page-forget-password-header"></HeaderAuth>

        <div className="container colaba-form">

          <div className="row">
            <div className="col-md-5">
              <form
                id="forgot-pass-form"
                data-testid="forgot-pass-form"
                className="colaba-form"
                onSubmit={handleSubmit(onSubmit)}>

                <header className="colaba__page-forget-password-header  auth-user-p-header">
                  <h1 className="h1" id="forgot-pass-header" data-testid="forgot-pass-header">Password Assistance</h1>
                  <span className="tagline" id="forgot-pass-tagline" data-testid="forgot-pass-tagline">Enter your email to reset your password.</span>
                </header>

                <div className="form-group extend">

                  <InputField
                    className={`${userNotExistMsg && 'error'}`}
                    data-testid="forgot-password-input"
                    maxLength={100}
                    id="password"
                    icon={<SVGEmail />}
                    label={"Email"}
                    name="email"
                    type={"text"}
                    register={register}
                    onChange={() => { setSuccessMessage(false); setUserNotExistMsg(false); clearErrors("email") }}
                    rules={{
                      required: {
                        value: true,
                        message: "This field is required.",
                      },
                      pattern: {
                        value: /^[A-Z0-9._-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                        message: "Please enter a valid email address.",
                      }
                    }}
                    autoFocus
                    errors={errors}
                  >
                    {userNotExistMsg && (
                      <span className="form-error" role="alert" data-testid={"user-not-exist-error"}>
                        {"Given Email address does not exist, please enter correct email address"}
                      </span>
                    )
                    }
                  </InputField>

                </div>
                <div className="form-group">
                  <button id="reset-pass-btn" data-testid="reset-pass-btn" className="btn btn-primary btn-lg btn-block" type="submit" onClick={handleSubmit(onSubmit)}>
                    RESET PASSWORD
                </button>
                </div>
              </form>

              <label id="need-acc-lbl" className="text-dark">Need an account? </label>
              {/* <a href="javascript:;" onClick={() => { history.push("/signup/"); }}>
                Sign Up
              </a> */}
              <Link id="signup-link" to={`/signup`} data-testid="signup-btn"> Sign Up</Link>
            </div>
            <div className="col-md-5 offset-md-2">
              <div className="artwork">
                <ArtWorkPasswordAssistance />
              </div>
            </div>
          </div>
        </div>
      </div>
    )
  }

  return (
    successMessage ? <SuccessMessage /> : forgotPasswordContent()
  );
};

export default ForgotPassword;
