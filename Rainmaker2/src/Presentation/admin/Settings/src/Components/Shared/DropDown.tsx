import { isEmpty } from 'lodash';
import React, { FunctionComponent, useState, useEffect, useRef } from 'react';

interface DropDownProps {
    listData: any[];
    editable?: boolean;
    selectedValue: any[];
    disabled?: boolean;
    handlerSelect: Function;
    handlerInput?: Function;
    maxLength?:number;
    inputType?:string;
}

export const DropDown: React.FC<DropDownProps> = ({ listData, editable, selectedValue, disabled, handlerSelect, handlerInput, maxLength, inputType}) => {

    const [showDropdown, setShowDropdown] = useState<boolean>(false);
    const [enableInput, setEnableInput] = useState<boolean>(false);
    const dropdown = useRef<HTMLDivElement>(null);
    const input = useRef<HTMLInputElement>(null);
    
    const [inputVal, setInputVal] = useState<any>(selectedValue);

    useEffect(() => {
        

        const clickOutside = (e: any) => {
            if (!dropdown.current?.contains(e.target)) {
                setShowDropdown(false);
            }
        }
        document.addEventListener('click', clickOutside);

        return () => document.removeEventListener('click', clickOutside);

    }, []);


    const setSize = () =>{
        if(String(selectedValue[0]?.text).length < 4 || isEmpty(selectedValue[0]?.text)){
            return 4;
        }else{
            return String(selectedValue[0]?.text).length;
        }
    }

    const filterDay = () =>{
        if(String(inputVal[0]?.text).length == 1)
        {
            return "0"+String(inputVal[0]?.text);
        }
        else if(String(inputVal[0]?.text).length == 1 || isEmpty(inputVal[0]?.text))
        {
            return "00";
        }
        else{
            return String(inputVal[0]?.text);
        }
    }


    return (
        <div data-testid="dropDown" className={`settings__dropdown ${disabled ? 'disabled' : ''}`} ref={dropdown}>
            <div data-testid="dropDownClick" className="settings__dropdown-wrap" onClick={() => setShowDropdown(!showDropdown)}>
                {editable &&
                    <>
                        {enableInput &&
                            <input
                                min="1"
                                max={30}
                                maxLength={maxLength}
                                minLength={2}
                                ref={input}
                                autoFocus
                                className="settings__dropdown-input"
                                type={inputType?inputType:"text"}
                                value={selectedValue[0]?.text} 
                                size={setSize()}
                                onBlurCapture={(e) => {                                    
                                   handlerSelect([{ text: e.target.value, value: e.target.value }]);
                                    setTimeout(() => {                                    
                                        setEnableInput(!enableInput) 
                                    }, 20);                                   
                                }}
                                onChange={(e: any) => {                               
                                    handlerSelect([{ text: e.target.value, value: e.target.value }]);
                                    setInputVal([{ text: e.target.value, value: e.target.value }]);
                                }}
                                onKeyUp={(e:any)=>{
                                    if(e.key=='Enter' || e.which==13)
                                    {
                                        setEnableInput(!enableInput);
                                    }
                                }}
                            />
                        }
                        {!enableInput &&
                            <span className="settings__dropdown-text" onClick={(e) => { setTimeout(() => { setEnableInput(!enableInput) }, 20) }}>{ filterDay() }</span>
                        }
                    </>
                }

                {!editable &&
                    <span className="settings__dropdown-text">{filterDay()}</span>
                }

                <span className="settings__dropdown-arrow"><i className="zmdi zmdi-chevron-down"></i></span>
            </div>


            {showDropdown && !disabled &&
                <div data-testid="dropDownMenu" className="settings__dropdown-menu">
                    <ul>
                        {listData.map((item: any) => {
                            return (
                                <li key={item.value} onClick={() => setTimeout(() => { setShowDropdown(false) }, 20)}>
                                    <span data-testid="dropDownMenuItem" onClick={() => { 
                                            handlerSelect([{ text: item.text, value: item.value }]); 
                                            setInputVal([{ text: item.text, value: item.value }]);
                                        }}>{item.text}</span>
                                </li>
                            )
                        })}
                    </ul>
                </div>
            }

        </div>
    )
}