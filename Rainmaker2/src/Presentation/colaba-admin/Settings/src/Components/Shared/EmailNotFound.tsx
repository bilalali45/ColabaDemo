import React from 'react';
import { SVGEmailNotFound } from './SVG';

type EmailNotFoundProps = {
    heading?:any,
    text?:any
}

const EmailNotFound:React.FC<EmailNotFoundProps> = ({heading,text,children}) => {
    return (
        <div data-testid="new-template-container" className="settings__email-not-found">
            <SVGEmailNotFound/>
            <h4 className="h4">{heading}</h4>
            <p>{text}</p>
            {children}
        </div>
    )
}

export default EmailNotFound;


