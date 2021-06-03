import React, { Fragment, useCallback, useContext, useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import { decodeString } from "../../../Utilities/helpers/CommonFunc";
import { ErrorHandler } from "../../../Utilities/helpers/ErrorHandler";
import HeaderAuth from "../../components/HeaderAuth";
import { LocalDB } from "../../lib/localStorage";
import { formData } from "../../lib/types";
import { ArtWorkCreateNewPassword, ArtWorkResetPassword } from "../../Shared/Components/Artwork";
import { UserActions } from "../../Store/actions/UserActions";
import { Store } from "../../Store/Store";
import SessionExpire from "./SessionExpire";
import ResetPasswordForm from "./_ResetPassword";


const ResetPassword = () => {
  
  const [showSuccessMsg, setShowSuccessMessage] = useState(false)
  const [sessoinExpire, setSessionExpire] = useState<boolean>(false)
  const {dispatch} = useContext(Store);
 
  const location = useLocation();
  const search = location?.search;
  let keys = decodeString(new URLSearchParams(search).get('key'));


  useEffect(()=>{
    checkSessionValidity()
  },[])

  const checkSessionValidity = async() => {
    if (keys) {
      let res = await LocalDB.getcaptchaCode(UserActions.getSessionValidityForResetPassword, { userId: + keys ? keys[0]: "", key: keys[1] });
      if(res && res != undefined){
        if(ErrorHandler.successStatus.includes(res.statusCode)){
          if(res?.data.data && res?.data.message === "The link has been expired")
          setSessionExpire(true);
        }
    
        else{
          ErrorHandler.setError(dispatch, res);
        }
      }  
    }
  }
  const onSubmitHandler = useCallback(async (data: formData) => {
    if (keys && keys.length > 0) {
      let res = await LocalDB.getcaptchaCode(UserActions.forgotPasswordResponse,{userId:+ keys ? keys[0] : "", key:keys[1], newPassword: data.password}); 
      if(res){
        if(ErrorHandler.successStatus.includes(res.statusCode)){
          setShowSuccessMessage(true);
        }
    
        else{
          ErrorHandler.setError(dispatch, res);
        }
      }
     }
  }, [])

  const showSuccessMessage = () => {
    return (
      <div data-testid="reset-password-success" className="colaba__page-reset-password auth-user-p">
        <HeaderAuth className="colaba__page-reset-password-header"></HeaderAuth>

        <div className="container colaba-form text-center box-center">
          <header className="colaba__page-reset-password-header auth-user-p-header">
            <h1 className="h1" id="rp-success-header" data-testid="reset-pass-success-header">Password Reset</h1>
            <span className="tagline" id="rp-success-tagline" data-testid="reset-pass-success-tagline">Your password has been reset successfully.</span>
          </header>

          <div className="artwork password-reset">
            <ArtWorkResetPassword />
          </div>

          <div className="form-group extend">
          <Link id="login-link" to={`/signin`} data-testid="signin-btn" className="btn btn-primary btn-lg btn-block"> Login</Link>
          </div>

          {renderFooter()}
        </div>
      </div>
    )
  }

  const resetPasswordForm = () => {
    return (
      <div data-testid="reset-password" className="colaba__page-reset-password auth-user-p">
        <HeaderAuth className="colaba__page-reset-password-header"></HeaderAuth>

        <div className="container colaba-form">

          <div className="row">
            <div className="col-md-5">

              <header className="colaba__page-reset-password-header  auth-user-p-header">
                <h1 className="h1" id="rp-header" data-testid="reset-pass-header">Create a New Password</h1>
                {/* <span className="tagline">Enter your email to reset your password.</span> */}
              </header>

              <ResetPasswordForm
                submitBtnText={"RESET PASSWORD"}
                onSubmitHandler={onSubmitHandler}
              />

              {renderFooter()}
            </div>

            <div className="col-md-5 offset-md-2">
              <div className="artwork">
                <ArtWorkCreateNewPassword />
              </div>
            </div>
          </div>

        </div>
      </div>
    )
  }

  const renderFooter = () => {
    return (
      <Fragment>
        <label id="need-acc-lbl" className="text-dark">Need an account? </label> 
        {/* <a href="javascript:;" onClick={() => { history.push("/signup/"); }}>
          Sign Up
              </a> */}
        <Link id="signup-link" to={`/signup`} data-testid="signup-btn"> Sign Up</Link>
      </Fragment>

    )
  }


  return (
    sessoinExpire? <SessionExpire/> :  showSuccessMsg ? showSuccessMessage() : resetPasswordForm()
  );

  // return (
  //    showSuccessMessage()
  // );
};


export default ResetPassword;
