import React,{useContext, useState} from 'react';
import { useAccordionToggle } from 'react-bootstrap/AccordionToggle';

const ContextAwareToggle = ({ idNum, children, eventKey, callback }: any) => {
    
    const decoratedOnClick = useAccordionToggle(eventKey, () => callback && callback(eventKey))
 
    return (
        <button    
        id={idNum}    
        type="button"
        onClick={decoratedOnClick} 
        className={`settings__accordion-signable-header`} 
        >
        <span className="settings__accordion-signable-toggle-btn"></span> {children} 
        </button>
    );
}

export default ContextAwareToggle
