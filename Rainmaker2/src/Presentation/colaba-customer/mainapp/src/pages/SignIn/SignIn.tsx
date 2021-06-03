import React, { useState, useContext } from "react";
import { useHistory } from "react-router-dom";
import HeaderAuth from "../../components/HeaderAuth";
import { TwoFactorAuth } from "../../components/TwoFactorAuth";
import {
  LoggedInResponseType,
  LoginPropsType,
  LoginResponseType,
  PhoneProps,
} from "../../lib/types";
import { UserActions } from "../../Store/actions/UserActions";
import { Store } from "../../Store/Store";
import { SignStep1, SignStep2 } from "./_SignIn";
import { UserActionsType } from "../../Store/reducers/UserReducer";
import { LocalDB } from "../../lib/localStorage";
import { UnMaskPhone } from "../../../Utilities/helpers/PhoneMasking";
import { Tenant2FaSetting } from "../../../Utilities/Enum";

const SignIn: React.FC = () => {
  const { state, dispatch } = useContext(Store);
  const { userInfo }: any = state.user;
  const history = useHistory();
  const [step, setStep] = useState<number>(1);
  const [fromStep, setFromStep] = useState<number>(1);
  const [fromScreen, setfromScreen] = useState<string>("signIn");
  const [error, setError] = useState<string>("");
  const [showSkipLink, setShowSkipLink] = useState<boolean>(false);

  const onLoginBtnClick = async (data: LoginPropsType) => {
    setError("");
  
    let result = await LocalDB.getcaptchaCode(UserActions.signIn, data);
    
    if (result.code === "200" || result.status === "Success") {
      // Valid userName and Password
      let response = result.data;
      //If 2Fa is Off, direct login and redirect to DashBoard
      if (response.isLoggedIn) {
        loggedInHandler(response);
      } else {
        let userData = {
          userName: response.userName,
          verificationSid: response.verificationSid,
          phoneNumber:
            response.phoneNo != null ? UnMaskPhone(response.phoneNo) : "",
          otpValidTime: response.otp_valid_till,
          next2FaInSeconds: response.next2FaInSeconds,
          sendAttemptsCount: response.sendAttemptsCount?.length,
          verifyAttemptCount: response.verify_attempts_count,
          message: result.message,
        };
        dispatch({ type: UserActionsType.SetUserInfo, payload: userData });
        LocalDB.storeAuthTokens(response.token, "");
        //Check Tenant2FaSettings
        loginHelper(response);
        setError("");
      }
    } else if (result.code === "400") {
      // Invalid userName or password

      if (result.message) {
        setError(result.message);
      } else {
        setError("Please try after few moments");
      }
    }
  };

  const loginHelper = (data: LoginResponseType) => {
    //Required for All----- step 1
    if (data.tenant2FaStatus === Tenant2FaSetting.RequiredForAll) {
      //Check phone is present or not
      if (data.phoneNoMissing) {
        //Showing phone number screen with no skip link
        setStep(2);
        setFromStep(1);
        setShowSkipLink(false);
      } else {
        //Showing 2Fa screen
        setStep(3);
        setFromStep(1);
      }
    }
    //User Preferences ---Step 3
    if (data.tenant2FaStatus === Tenant2FaSetting.UserPrefrences) {
      if (data.userPreference) {
        if (data.phoneNoMissing) {
          //Showing phone number screen with no skip link
          setStep(2);
          setFromStep(1);
          setShowSkipLink(false);
        } else {
          //Showing 2Fa screen
          setStep(3);
          setFromStep(1);
        }
      } else if (
        data.userPreference === undefined ||
        data.userPreference === null
      ) {
        if (data.phoneNoMissing) {
          //Showing phone number screen with no skip link
          setStep(2);
          setFromStep(1);
          setShowSkipLink(true);
        } else {
          //Showing 2Fa screen
          setStep(3);
          setFromStep(1);
        }
      }
    }
  };

  const onAgreeAndContinueBtnClick = async (data: PhoneProps) => {
    setError("");
    let phoneNumber: string = UnMaskPhone(data.mobileNumber);
    if (phoneNumber.includes("_")) {
      setError("Invalid number");
      return;
    }
    setfromScreen("phone");
    if (phoneNumber) {
      let userData = {
        userName: userInfo.userName,
        phoneNumber: phoneNumber,
        verificationSid: userInfo.verificationSid,
        otpValidTime: userInfo.otpValidTime,
        next2FaInSeconds: userInfo.next2FaInSeconds,
        sendAttemptsCount: userInfo.sendAttemptsCount,
        verifyAttemptCount: userInfo.verifyAttemptCount,
        message: userInfo.mmessage,
      };
      dispatch({ type: UserActionsType.SetUserInfo, payload: userData });
      setStep(3);
      setFromStep(2);
    }
  };

  const onSkipThisStepBtnClick = async () => {
    let result = await LocalDB.getcaptchaCode(UserActions.skip2FaRequest, {
      skip2Fa: true,
    });
    if (result) {
      loggedInHandler(result.data);
    }
  };

  const loggedInHandler = (loggedInData: LoggedInResponseType) => {
    const { token, refreshToken, cookiePath } = loggedInData;
    LocalDB.storeCookiePath(cookiePath);
    LocalDB.storeAuthTokens(token, refreshToken);
    
    history.push("/homepage");
  };

  const afterOtpValidTimeExpire = () => {
    setStep(1);
    setFromStep(1);
    setfromScreen("signIn");
    let userData = {
      userName: null,
      phoneNumber: null,
      verificationSid: null,
      otpValidTime: null,
      next2FaInSeconds: null,
    };
    dispatch({ type: UserActionsType.SetUserInfo, payload: userData });
  };

  const verifyHandler = (loggedInData: LoggedInResponseType) => {  
      loggedInHandler(loggedInData);  
  }

  
  return (
    <div data-testid="colabaSignin" className="colaba__page-signin auth-user-p">
      <HeaderAuth />

      <form data-testid="signIn-form" className="colaba-form">
        <div className="container">
          {step === 1 && (
            <SignStep1 loginHandler={onLoginBtnClick} error={error} />
          )}

          {step === 2 && (
            <SignStep2
              agreeAndContinueHandler={onAgreeAndContinueBtnClick}
              backHandler={() => {
                setStep(1);
              }}
              skipThisStepHandler={onSkipThisStepBtnClick}
              showSkipLink={showSkipLink}
              error={error}
            />
          )}
          {step === 3 && (
            <TwoFactorAuth
              backHandler={() => {
                setStep(fromStep);
              }}
              phone={userInfo.phoneNumber}
              email={userInfo.userName}
              verificationSid={userInfo.verificationSid}
              fromScreen={fromScreen}
              next2FaInSeconds={userInfo.next2FaInSeconds}
              otpValidTill={userInfo.otpValidTime}
              afterOtpValidTimeExpireHandler={afterOtpValidTimeExpire}
              sendAttemptsCount={userInfo.sendAttemptsCount}
              successOnVerifyOtpHandler={verifyHandler}
              verifyAttemptCount={userInfo.verifyAttemptCount}
              message={userInfo.message}
            />
          )}
        </div>
      </form>
    </div>
  );
};

export default SignIn;
