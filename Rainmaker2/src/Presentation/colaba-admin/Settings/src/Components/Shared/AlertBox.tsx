import React, { useState, useContext } from "react";
import { Store } from "../../Store/Store";
import { useHistory } from "react-router-dom";

type AlertBoxType = {
  hideAlert: Function;
  setshowAlert: Function;
  navigateUrl?: string;
};

export const AlertBox = ({
  hideAlert,
  navigateUrl,
  setshowAlert
}: AlertBoxType) => {
  const history = useHistory();

  const { state, dispatch } = useContext(Store);
  

  const yesHandler = () => {
    if (navigateUrl) {
        history.push(navigateUrl);       
      } 
      setshowAlert(false);
  };

  const noHandler = () => {
    hideAlert();
  };

  return (
    <div
      className="settings__alert-box"
      id="AlertBox"
      data-testid="alert-box"
      data-component="AlertBox"
    >
      <div className="settings__alert-box--modal">
        <section className="settings__alert-box--modal-body">
          <div className="settings__alert-box--modal-body-content">
            <p>Are you sure you want to close this</p>
            <p>request without saving?</p>
          </div>
        </section>
        <footer className="settings__alert-box--modal-footer">
          <div className="text-center">
            <button
              onClick={yesHandler}
              className="btn btn-secondary "
              data-testid="btnyes"
            >
              Yes
            </button>
            <button
              onClick={noHandler}
              className="btn btn-primary"
              data-testid="btnno"
            >
              No
            </button>
          </div>
        </footer>
      </div>
    </div>
  );
};
