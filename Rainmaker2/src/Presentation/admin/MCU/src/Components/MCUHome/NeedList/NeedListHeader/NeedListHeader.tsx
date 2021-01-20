import React from 'react';
import {useHistory} from 'react-router-dom';
import {LocalDB} from '../../../../Utils/LocalDB';
import {DocManagerIcon} from "../../../../Shared/SVG";

export const NeedListHeader = () => {
  const history = useHistory();

  const redirectToTemplate = () => {
    history.push(`/templateManager/${LocalDB.getLoanAppliationId()}`);
  };

  const redirectTodDocManager = () => {
    window.location.href = `/DocManager/${LocalDB.getLoanAppliationId()}`;
  };

  const redirectToMVC = () => {
    const portalReferralUrl = LocalDB.getPortalReferralUrl();
    const rainmakerUrl = window?.envConfig?.RAIN_MAKER_URL;
    if (portalReferralUrl) {
      window.top.location.href = portalReferralUrl;
    } else {
      window.top.location.href = `${rainmakerUrl}/Admin/Dashboard`;
    }
  };

  return (
    <div className="need-list-header">
      <div className="need-list-header--left" data-testid="NeedListBackButton">
        <a href="#" className="btn btn-back" onClick={redirectToMVC}>
          <em className="zmdi zmdi-arrow-left"></em> Back
        </a>
      </div>
      <div className="need-list-header--right" data-testId="NeedListManageButton">
        {/* <button onClick={redirectToTemplate} className="btn btn-primary">
          <em className="icon-record"></em> Manage Document Template
        </button> */}
        <button onClick={redirectTodDocManager} className="btn btn-secondary">
          <DocManagerIcon/> DOC MANAGER
        </button>
        {/* <button disabled = {true} className="btn btn-primary" >
          <em className="icon-edit"></em> Post to Byte Pro
        </button> */}
      </div>
    </div>
  );
};
