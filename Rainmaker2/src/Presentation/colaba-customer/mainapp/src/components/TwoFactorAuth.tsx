import React, { useState, useEffect, useContext } from "react";
import { useHistory } from "react-router-dom";
import moment from "moment";
import { useForm } from "react-hook-form";
import { ArtWorkPhoneVerification } from "../Shared/Components/Artwork";
import InputCheckedBox from "./InputCheckedBox";
import { LocalDB } from "../lib/localStorage";
import { UserActions } from "../Store/actions/UserActions";
import { Store } from "../Store/Store";
import { UserActionsType } from "../Store/reducers/UserReducer";
import { LoggedInResponseType } from "../lib/types";
import { Timer } from "../Shared/Components/Timer";

interface Props {
  backHandler: () => void;
  phone: string;
  email: string;
  verificationSid?: string;
  fromScreen: string;
  next2FaInSeconds?: number;
  otpValidTill?: string;
  afterOtpValidTimeExpireHandler: () => void;
  sendAttemptsCount?: number;
  successOnVerifyOtpHandler: (data: LoggedInResponseType) => void;
  fromSignUp?: boolean;
  verifyAttemptCount?: number;
  message?: string;
  updateRequestSid?: (requestSid: string) => void;
  updateDontAsk2Fa?: (dontAsk: boolean) => void;
}

export const TwoFactorAuth: React.FC<Props> = ({
  backHandler,
  phone,
  email,
  verificationSid,
  fromScreen,
  next2FaInSeconds,
  otpValidTill,
  afterOtpValidTimeExpireHandler,
  sendAttemptsCount,
  successOnVerifyOtpHandler,
  fromSignUp,
  verifyAttemptCount,
  message,
  updateRequestSid,
  updateDontAsk2Fa,
}) => {
  const history = useHistory();
  const {
    register,
    errors,
    handleSubmit,
    setError,
    clearErrors,
    setValue,
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onBlur",
    criteriaMode: "firstError",
    shouldFocusError: true,
    shouldUnregister: true,
  });

  const { dispatch } = useContext(Store);
  
  const [disableResendBtn, setdisableResendBtn] = useState<boolean>(true);
  const [otpSendAttemptCount, setotpSendAttemptCount] = useState<string>("");
  const [showTimer, setShowTimer] = useState<boolean>(false);
  
  const [dontAskMe, setDontAskMe] = useState(false);
  
  const [otpValidTillInterval, setOtpValidTillInterval] = useState<number>();
  const [maxAttemptMsg, setMaxAttemptMsg] = useState<string>();
  const [otpValue, setOtpValue] = useState<string>("");
  const [otpValid, setOtpValid] = useState<boolean | undefined>(undefined);
  const [sid, setSid] = useState(null);
  const [optResponse, setOptResponse] = useState<LoggedInResponseType>();
  const [mobileNumber, setMobileNumber] = useState<string | null>(null)

  useEffect(() => {
    if (verifyAttemptCount && verifyAttemptCount >= 5) {
      startTimerForNewCycle(otpValidTill);
      setMaxAttemptMsg(message);
    } else {
      if (fromScreen === "phone") {
        send2FaRequest();
      }
      if (fromScreen === "signIn") {
        timerForResendBtn(next2FaInSeconds, callbackToEnableResendBtn);
        if (sendAttemptsCount) {
          setotpSendAttemptCount(String(5 - sendAttemptsCount));
        }
      }
    }
  }, []);

  useEffect(() => {
    if (sid === null) return;
    !!updateRequestSid && updateRequestSid(sid ?? "");
  }, [sid]);

  useEffect(() => {
    !!updateDontAsk2Fa && updateDontAsk2Fa(dontAskMe);
  }, [dontAskMe]);

  const startTimerForNewCycle = (otpValidTill: string | undefined) => {
    let otpValidTime = Number(moment(otpValidTill).local().format("mm"));
    setOtpValidTillInterval((otpValidTime - new Date().getMinutes()) * 60);
    setShowTimer(true);
  };

  const send2FaRequest = async () => {
    clearErrors("otp");
    setValue("otp", "");
    setdisableResendBtn(true);
    let result = await LocalDB.getcaptchaCode(UserActions.send2FaRequest, {
      phoneNumber: phone,
      email: email,
      verificationSid: verificationSid,
    });
    if (result.message === "Frequent OTP attempt found.") {
      let userData = {
        userName: email,
        verificationSid: result.data.sid,
        phoneNumber: phone,
        otpValidTime: result.data.otp_valid_till,
        next2FaInSeconds: next2FaInSeconds || result.data.next2FaInSeconds,
      };
      dispatch({ type: UserActionsType.SetUserInfo, payload: userData });
      let totalsendCodeAttempt: number = result.data.send_code_attempts.length;
      setotpSendAttemptCount(String(5 - totalsendCodeAttempt));

      setSid(result.data.sid)

      if (totalsendCodeAttempt < 5) {
        timerForResendBtn(
          next2FaInSeconds || result.data.next2FaInSeconds,
          callbackToEnableResendBtn
        );
      }
      return;
    }

    if (result.status === "max_attempts_reached") {
      startTimerForNewCycle(result.data.twoFaResponse.otp_valid_till);
      setMaxAttemptMsg(result.message);
    } else {
      setSid(result.data.twoFaResponse.sid);
      let userData = {
        userName: email,
        verificationSid: result.data.twoFaResponse.sid,
        phoneNumber: phone,
        otpValidTime: result.data.twoFaResponse.otp_valid_till,
        next2FaInSeconds:
          next2FaInSeconds || result.data.twoFaResponse.next2FaInSeconds,
      };
      dispatch({ type: UserActionsType.SetUserInfo, payload: userData });
      let totalsendCodeAttempt: number =
        result.data.twoFaResponse.send_code_attempts.length;
      setotpSendAttemptCount(String(5 - totalsendCodeAttempt));
      if (totalsendCodeAttempt < 5) {
        timerForResendBtn(next2FaInSeconds, callbackToEnableResendBtn);
      }
    }
  };

  const validateOtpLength = (event: React.FormEvent<HTMLInputElement>) => {
    const value = event.currentTarget.value;

    if (isNaN(Number(value))) return;

    setOtpValue(value);

    if (value.length === 6) {
      verifyCode(value);
    } else {
      setOtpValid(undefined);
    }
  };

  const timerForResendBtn = (seconds:number | undefined, cb:Function) => {
    if(seconds){
      var remaningTime = seconds;
      window.setTimeout(function () {
        if (remaningTime > 0) {
          timerForResendBtn(remaningTime - 1, cb);
        } else {
          cb();
        }
      }, 1000);
    }
    
  };

  var callbackToEnableResendBtn = function () {
    console.log("------Timer end for resend button------");
    setdisableResendBtn(false);
  };

  const timerCompleteCallBack = () => {
    setShowTimer(false);
    afterOtpValidTimeExpireHandler();
  };

  const verifyCode = async (otpCode: string) => {
    let result: any = null;
    // Since we are using different end points for SignIn and SignUp for verification.
    const verifyMethod = fromSignUp
      ? UserActions.verify2FaSignUp
      : UserActions.verify2FaSignIn;

    result = await LocalDB.getcaptchaCode(verifyMethod, {
      otpCode: otpCode,
      // Get the latest VerficiationSide received from API,
      // on SignUp this needs to be matched.
      verificationSid: fromSignUp ? sid : verificationSid,
      email: email,
      phoneNumber: "+1" + phone,
      dontAsk2Fa: dontAskMe,
      mapPhoneNumber: fromScreen === "phone" ? true : false,
    });

    if (result) {
      if (result.code === "200" || result.status === "Success") {
        setOtpValid(true);
        setOptResponse(result.data);
        const { token, refreshToken, cookiePath } = result.data;
        LocalDB.storeCookiePath(cookiePath);
        LocalDB.storeAuthTokens(token, refreshToken);
      } else if (result.code === "400") {
        setOtpValid(false);
        setError("otp", {
          type: "server",
          message: "",
        });
        if (result?.data.status === "max_attempts_reached") {
          console.log('result.data.otp_valid_till', result.data.otp_valid_till)
          setMobileNumber(result.data.to.substring(2))
          startTimerForNewCycle(result.data.otp_valid_till);
          setMaxAttemptMsg(result.message);
        }
      }
    }
  };

  const onVerifyBtnClick = async () => {
    if (dontAskMe && !fromSignUp) {
      let result = await LocalDB.getcaptchaCode(UserActions.dontAsk2Fa, { dntAsk2fa: true });
      if (result && optResponse) {
        successOnVerifyOtpHandler(optResponse);
      }
    } else {
      if (optResponse) {
        successOnVerifyOtpHandler(optResponse);  
      }
    }
  };

  return (
    <div
      data-testid="signIn-form-step3"
      className="colaba__page-two-factor-auth"
    >
      <div className="row">
        <div className="col-md-6">
          <header
            data-testid="2fa-header"
            className="colaba__page-two-factor-auth-header  auth-user-header"
          >
            <h1 className="h1">Phone Verification And Consent To Contact</h1>
          </header>
          <div className="form-group">
            <label data-testid="encoded-number" className="form-label">
              Enter the code sent to{" "}
              {`(***) ***-${!!mobileNumber ? mobileNumber?.substr(mobileNumber?.length - 4) : phone.substr(phone.length - 4)}`}
            </label>
            <div className="input-group">
              <input
                autoFocus={true}
                readOnly={otpValid}
                data-testid="otp-input"
                maxLength={6}
                id="otp"
                name="otp"
                type="text"
                value={otpValue}
                className={`form-control ${errors.otp ? "error" : ""}`}
                ref={register({
                  required: "Required.",
                  pattern: {
                    value: /\d/,
                    message: "Invalid format",
                  },

                  maxLength: {
                    value: 6,
                    message: "Max length limit reached.",
                  },
                })}
                onFocus={() => { }}
                onChange={(e) => {
                  clearErrors("otp");
                  validateOtpLength(e);
                }}
              />
              {otpValid && <em data-testid="tick" className="input-tick"></em>}
              {otpValid === false && <em data-testid="cross" className="input-cross zmdi zmdi-close"></em>}
            </div>

            {errors.otp && (
              <span data-testid="otp-error" className="form-error">
                {errors.otp.message}
              </span>
            )}

            {showTimer && (
              <div data-testid="timer-div" className="colaba-c-timer">
                <Timer
                  interval={otpValidTillInterval || 0}
                  maxAttemptMsg={maxAttemptMsg || ""}
                  timerCompleteCallBack={timerCompleteCallBack}
                />
              </div>
            )}
          </div>

          {!showTimer && !otpValid && (
            <>
              <span className="text-dark">Didn't receive the code?</span>
              <div data-testid="send2fa-btn" className={`form-group extend`}>
                <a
                  className={disableResendBtn ? "text-disabled" : ""}
                  onClick={() => send2FaRequest()}
                  href="javascript:;"
                >{`Resend code (${otpSendAttemptCount} left)`}</a>
              </div>
            </>
          )}

          <div data-testid="dnt-ask-check" className="form-group">
            <InputCheckedBox
              name="test"
              label="Don't ask again on this computer."
              checked={dontAskMe}
              onChange={() => setDontAskMe(!dontAskMe)}
            >

            </InputCheckedBox>
          </div>

          <div className="form-group extend">
            <button
              data-testid="verify-btn"
              disabled={!otpValid}
              className="btn btn-primary btn-lg btn-block"
              onClick={handleSubmit(onVerifyBtnClick)}
              type="submit"
            >
              Verify
            </button>
          </div>
          {!otpValid && (
            <div className="d-flex align-items-center">
              <button
                data-testid="back-btn"
                className="btn btn-back mr-auto"
                onClick={backHandler}
              >
                <em className="zmdi zmdi-arrow-left"></em> Back
              </button>
              {fromSignUp ? (
                <span>
                  <label className="text-dark no-margin">
                    Already have an account?
                  </label>
                  <a
                    href="javascript:;"
                    onClick={() => {
                      history.push("/signin/");
                    }}
                  >
                    {" "}
                    Login{" "}
                  </a>
                </span>
              ) : (
                  <span>
                    <label className="text-dark no-margin">
                      Don't have an account?&nbsp;
                  </label>
                    <a
                      data-testid="signup-btn"
                      href="javascript:;"
                      onClick={() => {
                        history.push("/signup/");
                      }}
                    >
                      Sign Up
                  </a>
                  </span>
                )}
            </div>
          )}
        </div>

        <div className="col-md-5 offset-md-1">
          <div className="artwork">
            <ArtWorkPhoneVerification />
          </div>
        </div>
      </div>
    </div>
  );
};
