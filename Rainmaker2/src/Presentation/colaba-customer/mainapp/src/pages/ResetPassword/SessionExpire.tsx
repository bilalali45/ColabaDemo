import React from "react";
import { Link } from "react-router-dom";
import HeaderAuth from "../../components/HeaderAuth";
import { ArtWorkCreateNewPassword } from "../../Shared/Components/Artwork";

const SessionExpire = () => {
 
    return (
        <div data-testid="reset-password" className="colaba__page-session-expired auth-user-p">
          <HeaderAuth className="colaba__page-session-expired-header"></HeaderAuth>
  
          <div className="container colaba-form">
  
            <div className="row">
              <div className="col-md-5">
  
                <header className="colaba__page-session-expired-header auth-user-p-header">
                  <h1 className="h1" id="rp-header" data-testid="session-expired-header">Session Expired</h1>
                  <span className="tagline">Sorry, your password reset link has expired.Please refresh to try again.</span>
                </header>

                <Link to="/forgotPassword" className="btn btn-primary btn-lg btn-block">Refresh</Link>
  
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
};


export default SessionExpire;
