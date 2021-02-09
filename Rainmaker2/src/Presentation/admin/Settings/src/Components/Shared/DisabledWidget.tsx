import React, {FunctionComponent} from 'react';
import { SVGReminderEmailsDisabled } from './SVG';

const DisabledWidget:React.FC<{text:string}> = ({text}) => {
    return (
        <div data-testid="DisabledWidget" className="disabled-entire-widget">
            <div className="disabled-entire-widget-wrap">
                <SVGReminderEmailsDisabled />
                <span className="disabled-entire-widget-text">{text}</span>
            </div>
        </div>
    )
}

export default DisabledWidget
