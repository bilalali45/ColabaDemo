import React from 'react';
import {useHistory} from 'react-router-dom';
import {LocalDB} from '../../../../Utils/LocalDB';

export const NeedListHeader = () => {
  const history = useHistory();

  const redirectToTemplate = () => {
    history.push(`/templateManager/${LocalDB.getLoanAppliationId()}`);
  };

  const redirectToMVC = () => {
    const portalReferralUrl = LocalDB.getPortalReferralUrl();

    console.log('portalReferralUrl', portalReferralUrl);

    if (portalReferralUrl) {
      window.top.location.href = portalReferralUrl;
    } else {
      window.top.location.href = '/Admin/Dashboard';
    }
  };

  return (
    <div className="need-list-header">
      <div className="need-list-header--left">
        <a href="#" className="btn btn-back" onClick={redirectToMVC}>
          <em className="zmdi zmdi-arrow-left"></em> Back
        </a>
      </div>
      <div className="need-list-header--right">
        <button onClick={redirectToTemplate} className="btn btn-primary">
          <em className="icon-record"></em> Manage Document Template
        </button>
        {/* <button disabled = {true} className="btn btn-primary" >
          <em className="icon-edit"></em> Post to Byte Pro
        </button> */}
      </div>
    </div>
  );
};
