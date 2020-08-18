import React from "react";
import { useHistory } from "react-router-dom";
import { LocalDB } from "../../../../Utils/LocalDB";
import {SignalRHub} from '../../../../Utils/helpers/SignalR';

export const NeedListHeader = () => {
  const history = useHistory();

  const redirectToTemplate = () => {
    history.push(`/templateManager/${LocalDB.getLoanAppliationId()}`);
  };

  const redirectToMVC = () => {
    const loanApplicationId = LocalDB.getLoanAppliationId();
    //window.open("/Admin/Loanapplication", "_self");
    window.top.location.href =
      "/Admin/Loanapplication/Edit/" + loanApplicationId;
  };

  const stopSignalR = () => {
    console.log('stopping signalR')
    SignalRHub.hubStop();
  }

  return (
    <div className="need-list-header">
      <div className="need-list-header--left">
        <a href="#" className="btn btn-back" onClick={redirectToMVC}>
          <em className="zmdi zmdi-arrow-left"></em> Back
        </a>
      </div>
      <div className="need-list-header--right">
        <button onClick={redirectToTemplate} className="btn btn-secondry">
          <em className="icon-record"></em> Manage Template
        </button>
        <button className="btn btn-primary" onClick={stopSignalR} >
          <em className="icon-edit"></em> Post to Byte Pro
        </button>
      </div>
    </div>
  );
};
