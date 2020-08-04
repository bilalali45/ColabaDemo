import React, { useState, Fragment, useEffect } from 'react'

type TextAreaType = {
    textAreaValue: string;
    focus: boolean;
    onBlurHandler: Function;
    onChangeHandler: Function;
    isValid: boolean;
    errorText: string;  
}

export const TextArea = ({textAreaValue, onBlurHandler, onChangeHandler, isValid, errorText, focus}: TextAreaType) => {

   const textAreaStyle = {
    border: isValid ?  "1px solid #f00" : ""
   };
   

    return (
        <Fragment>
        <textarea style={textAreaStyle} autoFocus={focus} onBlur={(e) => onBlurHandler()} value={textAreaValue} onChange = {(e) => {onChangeHandler(e)}} name="" id="" className="form-control" rows={20}>
        </textarea>
        <div>
       <p style={{color:"red"}} >{isValid ? errorText : ''}</p>
        </div>
        </Fragment>
    )
}