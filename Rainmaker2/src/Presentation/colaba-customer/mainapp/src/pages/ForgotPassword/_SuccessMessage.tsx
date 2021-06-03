import React from "react";
import _ from "lodash/fp";
import HeaderAuth from "../../components/HeaderAuth";
import { Link } from "react-router-dom";
import { ArtWorkPasswordAssistance } from "../../Shared/Components/Artwork";


const SuccessMessage = () => {


  return (
    <div data-testid="reset-password-success" className="colaba__page-almost-finished auth-user-p">
      <HeaderAuth className="colaba__page-almost-finished-header"></HeaderAuth>

      <div className="container colaba-form">
        <div className="row">
          <div className="col-md-5">
            <header className="colaba__page-almost-finished-header auth-user-p-header">
              <h1 className="h1" id="almost-finished-header" data-testid="almost-finished-header">Almost Finished</h1>
              <span className="tagline" id="almost-finished-tagline" data-testid="almost-finished-tagline">An email with instructions to create a new password has been sent to you.</span>
            </header>

            <div className="form-group extend">
              <Link id="login-link" to={`/signin`} data-testid="signin-btn" className="btn btn-primary btn-lg btn-block"> Go Back to Login</Link>
            </div>
          </div>
          <div className="col-md-5 offset-md-2">
            <div className="artwork">
              <ArtWorkPasswordAssistance />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SuccessMessage;
