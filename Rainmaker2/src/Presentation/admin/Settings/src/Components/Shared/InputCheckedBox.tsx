import React, {Children, FunctionComponent, useRef, useEffect} from 'react'
import { idText } from 'typescript'

interface InputCheckedBoxProps {
    id?: any;
    className?: string;
    name: any;
    checked?:boolean;
    value?:string;
    testId?:string;
    onchange: Function
}

const InputCheckedBox:React.FC<InputCheckedBoxProps> = ({id, className, name, checked, value, testId, onchange, children}) => {

    const refInput = useRef<HTMLInputElement>(null);
    
    const makeClick = () => {
        refInput.current?.click();
    } 

    return (
        <label className="settings__input-checkbox">
            <input  ref={refInput} type="checkbox" data-testid={testId} onClick={() =>  onchange()} id={id} className={className} name={name} checked={checked} value={value}/>
            <span className={`settings__input-checkbox-type`}></span>
            <label className={`settings__input-checkbox-label`} onClick={makeClick}>{children}</label>
        </label>
    )
}

export default InputCheckedBox;
