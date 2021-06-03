import React from 'react';
import { SVGNoReminder } from './SVG';

interface HaveNoDataReminderProps{
    heading?:string;
    text?:string;
}
//settings__have-no-data
const HaveNoDataReminder:React.FC<HaveNoDataReminderProps> = ({children, heading, text}) => {
    return (
        <div data-testid="noReminder" className="settings__have-no-data">
            <div className="settings__no-data-reminder-wrap">
                <SVGNoReminder/>
                <h4 className="h4">{heading}</h4>
                <p>{text}</p>
                {children}
            </div>            
        </div>
    )
}

export default HaveNoDataReminder;
