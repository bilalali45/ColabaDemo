import React from 'react';
import uploadedFile from './../assets/icons/uploaded-file.svg';
import calendar from './../assets/icons/calendar.svg';
import close from './../assets/icons/close.svg';
import { SVGDocument, SVGClose, SVGCalender } from '../SVGIcons';


type NotificationType = {
  unSeen?: boolean,
}
export const Notification = ({ unSeen }: NotificationType) => {
  return (
    <li className={`notification-list ${unSeen ? 'unSeenList' : ''}`} >

      <div className="notification-list-item">
        <div className="n-wrap">
          <div className="n-icon"><SVGDocument /></div>
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
              <SVGCalender /> Jul. 25 2020 05:30 PM
          </div>
          </div>
        </div>
        <div className="n-close">
          <SVGClose />
        </div>
      </div>

      <div className="notification-list-item-remove">
        <span className="n-alert-text">This notification has been removed.</span>
        <button className="btn-undo">Undo</button>
      </div>

    </li>
  );
};
