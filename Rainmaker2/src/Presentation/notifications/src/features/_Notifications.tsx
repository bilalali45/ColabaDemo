import React from 'react';
import uploadedFile from './../assets/icons/uploaded-file.svg';
import calendar from './../assets/icons/calendar.svg';
import close from './../assets/icons/close.svg';

type NotificationType = {
  unSeen?: boolean,
}
export const Notification = ({unSeen }: NotificationType) => {
  return (
    <li className={`notification-list ${unSeen ? 'unSeenList' : ''}`} >
      <div className="n-wrap">
        <div className="n-icon"><img src={uploadedFile} alt="" /></div>
        <div className="n-content">
          <div className="n-cat" title={"Document Submission"}>Document Submission</div>
          <h4 className="n-title">
            Richard Glenn
          </h4>
          <p className="n-address">
            727 Ashleigh LN # 222 <br />
            Dallas, TX 76099
         </p>
          <div className="n-date">
            <img src={calendar} alt="" /> Jul. 25 2020 05:30 PM
          </div>
        </div>
      </div>
      <div className="n-close">
        <img src={close} alt="" title="Close" />
      </div>
    </li>
  );
};
