import React from 'react';
import {useHistory} from 'react-router-dom';
import {LocalDB} from '../../../../Utils/LocalDB';

export const NeedListHeader = () => {
  const history = useHistory();

  const redirectToTemplate = () => {
    history.push(`/templateManager/${LocalDB.getLoanAppliationId()}`);
  };

  const redirectToMVC = () => {

    window.top.location.href = document.referrer
  };
  
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
      </div>
    </div>
  );
};
