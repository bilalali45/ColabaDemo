import React, { useState, Fragment, useEffect } from 'react'

type TextAreaType = {
    textAreaValue: string;
    onBlurHandler: Function;
    onChangeHandler: Function;
}

export const TextArea = ({textAreaValue, onBlurHandler, onChangeHandler}: TextAreaType) => {
    return (
        <>
        <textarea onBlur={(e) => onBlurHandler()} value={textAreaValue} onChange = {(e) => {onChangeHandler(e)}} name="" id="" className="form-control" rows={20}>
        </textarea>
        </>
    )
}