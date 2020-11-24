import React from 'react';
import { SVGNothing } from './SVG';

type NothingProps = {
    heading:any,
    text:any
}

const Nothing:React.FC<NothingProps> = ({heading,text,children}) => {
    return(
        <div data-testid="new-template-container" className="settings__data-not-found">
            <SVGNothing />
            <h4 className="h4">{heading}</h4>
            <p>{text}</p>
            {children}
        </div>
    )
} 
export default Nothing;