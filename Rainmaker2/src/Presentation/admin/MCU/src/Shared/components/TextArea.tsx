import React, { useState, Fragment, useEffect, ChangeEvent } from 'react'

type TextAreaType = {
    textAreaValue?: string;
    focus?: boolean;
    onBlurHandler?: Function;
    onChangeHandler?: Function;
    isValid?: boolean;
    errorText?: string;
    placeholderValue?: string,
    maxLengthValue?: number
}

export const TextArea = ({ textAreaValue, placeholderValue, onBlurHandler = () => { }, onChangeHandler = () => { }, isValid, errorText, focus, maxLengthValue = 3000}: TextAreaType) => {


    const [isTextValid, setIsTextValid] = useState<boolean>(false);
   // const regex = /^[ A-Za-z0-9-,.!@#$%^&*()_+=`~{}|[\s]*$/i;
    const regex = /^[a-zA-Z0-9~`!@#\$%\^&\*\(\)_\-\+={\[\}\]\|\\:;"'<,>\.\?\/\s  ]*$/i;


    const checkIfValid = (e: ChangeEvent<HTMLTextAreaElement>) => {
        if (regex.test(e.target.value)) {
            setIsTextValid(false)
            onChangeHandler(e);
        } else {
            setIsTextValid(true);
        }
    }

    const textAreaStyle = {
        border: isTextValid ? "1px solid #f00" : "",
        outine: 'none'
    };


    return (
        <Fragment>
            <textarea
                style={textAreaStyle} 
                autoFocus={focus} 
                onBlur={(e) => onBlurHandler()} 
                value={textAreaValue} 
                onChange={(e) => {
                    checkIfValid(e);
                }} 
                name="" 
                id="" 
                className="form-control" 
                rows={20} 
                placeholder={placeholderValue} 
                maxLength={maxLengthValue}>
            </textarea>
            <div>
                <p style={{ color: "red" }} >{isTextValid ? errorText : ''}</p>
            </div>
        </Fragment>
    )
}