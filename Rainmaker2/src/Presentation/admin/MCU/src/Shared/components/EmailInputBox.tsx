import React, { useState, useEffect } from "react";
import { ReactMultiEmail, isEmail } from "react-multi-email";
import "react-multi-email/style.css";

type props = {
  handlerEmail: Function;
  tokens: string[];
  exisitngEmailValues?: string[] | null;
  className?: string;
  errorHandler?: any;
  id?: any;
  setInputError: Function
  triggerInputValidation: Function
  clearInputError: Function
}

export const EmailInputBox = (props: props) => {
  const [emailsArray, setEmailArray] = useState<string[]>([]);
  const [isEmailValid, setisEmailValid] = useState<boolean>();
  const existingEmailValues = props.exisitngEmailValues;


  useEffect(() => {
    const emailCoontainer = document?.getElementsByClassName(`${props.className} react-multi-email`);
    const inputElement = emailCoontainer[0]?.children[0];
    const updateInputSize = () => {
      let l = inputElement.getAttribute('value')?.length;
      inputElement?.setAttribute('size', String(l));
    }

    const keyDownListner = (event: any) => {
      props.clearInputError(props.id);
      if (event.code == "Backspace" && event.target.value.length == 1) {
        setisEmailValid(true);
        props.triggerInputValidation(props.id, true);
      }else if(event.code == "Backspace" && window?.getSelection()?.toString() == event.target.value){
        setisEmailValid(true);
        props.triggerInputValidation(props.id, true);
      }
      updateInputSize();
    }
    inputElement.addEventListener('keydown', keyDownListner);
    inputElement.addEventListener('paste', async () => { setTimeout(() => { updateInputSize() }, 100) });

    const fromEmailListener = (event: any) => {
      props.clearInputError(props.id);
      if(window?.getSelection()?.toString() != event.target.value){
      if (props.id == "fromEmail" && event.target.value.length > 1) {
        if (emailCoontainer[0].childElementCount == 2) {
          setisEmailValid(false);
          props.setInputError(props.id, "Only one email is allowed in from address.")
          return;
        }
      }
    }
    }
    inputElement.addEventListener('keydown', fromEmailListener);

    return () => {
      inputElement.removeEventListener("keydown", keyDownListner);
      inputElement.removeEventListener('paste', async () => { setTimeout(() => { updateInputSize() }, 100) });
    };
  }, []);

  useEffect(() => {
    if (existingEmailValues) {
      setEmailArray(existingEmailValues)
    }
  }, [existingEmailValues]);

  const handlerEmailChange = (content: string[]) => {
    setEmailArray(content)
    props.handlerEmail(content);
    props.triggerInputValidation(props.id, true)
  }

  const handlerEmailValidate = (text: string) => {
    if (isEmail(text)) {
      if(isEmailValid === false){
        return false;
       }
       setisEmailValid(true);
      return true;
    } else {
      setisEmailValid(false)
      props.setInputError(props.id);
      return false;
    }
  }
  const removeEmailHandler = () => {
    setisEmailValid(true);
    props.triggerInputValidation(props.id, true);
  }

  return (
    <>
      <ReactMultiEmail
        emails={emailsArray}
        className={props.className}
        onChange={handlerEmailChange}
        validateEmail={(email: any) => {
          return handlerEmailValidate(email);
        }}
        getLabel={(email: string,index: number,removeEmail: (index: number) => void) => {
          return (
            <div data-tag key={index}>
              {email}
              <span data-tag-handle onClick={
                () => {
                  removeEmail(index)
                  removeEmailHandler()
                }           
                }>
                ×
                </span>
            </div>
          );
        }}
      />
    </>
  );
}



