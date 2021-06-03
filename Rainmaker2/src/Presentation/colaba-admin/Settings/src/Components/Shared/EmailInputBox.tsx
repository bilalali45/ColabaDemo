import  React ,{useState, useEffect, useRef} from "react";
import { ReactMultiEmail, isEmail  } from "react-multi-email";
import "react-multi-email/style.css";
import { Tokens } from "../../Entities/Models/Token";

type props = {
  handlerEmail: Function;
  tokens: Tokens[];
  handlerClick: Function;
  exisitngEmailValues?: string[] | null;
  className?:string;
  dataTestId: string;
  id?:any;
  setInputError: Function
  triggerInputValidation: Function
  clearInputError: Function
}

export const EmailInputBox = (props: props) => {
  const [emailsArray, setEmailArray] = useState<string[]>([]);
  const [isEmailValid, setisEmailValid] = useState<boolean>();
  const existingEmailValues = props.exisitngEmailValues;

  useEffect(() => {

      const emailCoontainer  = document?.getElementsByClassName(`${props.dataTestId} react-multi-email`); 
      const inputElement =  emailCoontainer[0]?.children[0];

      const updateInputSize = () => {
        let l:any =  inputElement.getAttribute('value')?.length;
        let size = (l >= 2) ? l : 2;
        inputElement?.setAttribute('size',String(size));
        inputElement?.setAttribute('data-testid','EmailInputBox_Input');
      }

      updateInputSize();

      const keyDownListner = (event: any) => {
        props.clearInputError(props.id);
        if(event.code == "Backspace" && event.target.value.length == 1){ 
          setisEmailValid(true);
          props.triggerInputValidation(props.id, true);
        }else if(event.code == "Backspace" && window?.getSelection()?.toString() == event.target.value){
          setisEmailValid(true);
          props.triggerInputValidation(props.id, true);
        }
        updateInputSize();
      }
      inputElement.addEventListener('keydown', keyDownListner);
      inputElement.addEventListener('paste', async()=>{ setTimeout( ()=>{ updateInputSize() },100)});

      const fromEmailListener = (event: any) => {
        props.clearInputError(props.id);
        if(window?.getSelection()?.toString() != event.target.value){
          if(props.id == "fromEmail" && event.target.value.length > 1){
            if(emailCoontainer[0].childElementCount == 2){
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
        inputElement.removeEventListener('paste', async()=>{setTimeout( ()=>{ updateInputSize() },100)});
      };
  },[]);

  useEffect(() => {
    if (existingEmailValues){
      setEmailArray(existingEmailValues);
    }
  }, [existingEmailValues]);

  const handlerEmailChange = (content: string[]) => {
    setEmailArray(content)
    props.handlerEmail(content);
    props.triggerInputValidation(props.id, true)
   }
   
  const  handlerEmailValidate = (text: string) => {
    if(isEmail(text)){ // check valid email address
         if(isEmailValid === false){
          return false;
         }
         setisEmailValid(true);
          return true;
       } else if(checkTokenIsValid(text)){ //check valid token
         return true;
       } else{
         setisEmailValid(false)
         props.setInputError(props.id);
         return false;
       }    
   } 

  const checkTokenIsValid = (text: string) => {
    let tokens: Tokens[]; 
    let result: boolean = false
    if(props.id === "fromEmail"){
      tokens = props.tokens.filter(item => item.fromAddess === true); 
    }else{
      tokens = props.tokens.filter(item => item.ccAddess === true);
    }     
     result = tokens.some(item => item.symbol === text);
     return result;
   }

  const ClickHandler = () => {
    props.handlerClick();
  }

  const emailBlur = () => {
    props.triggerInputValidation(props.id, isEmailValid);
  }

  const [inputText, setInputText] = useState<any>('');

  const doubleClickHandler = (event: any,i:any) => {
    let parentDiv = event.target.parentElement; //nodeName
    let pillsTxt = event.target.textContent;
    let deleteSpan = event.target.childNodes[1];
    let elementLength = parentDiv.children.length;
    let input = parentDiv.children[elementLength-1];
    
    input.focus();
    deleteSpan.click();
    setTimeout(()=>{
      input.value = pillsTxt.substring(0,(pillsTxt.length-1))      
    },20)
    console.log('doubleClick on pill', emailsArray[i], parentDiv.childNodes);
    //...emailsArray,emailsArray[i]
    //setEmailArray([...emailsArray,emailsArray[i]=input.value]);
  }

  const removeEmailHandler = () => {
    setisEmailValid(true);
  props.triggerInputValidation(props.id, true);
  }
    return (
      <>
      <div data-testid={props.dataTestId} onBlur={emailBlur}  onFocus={ClickHandler} onClick={ClickHandler} className={`settings__multi-pills-control ${props.className?props.className:''}`}>
        <div data-testid="EmailInputBox">
        <ReactMultiEmail
          className = {props.dataTestId}
          emails={emailsArray}
          onChange={handlerEmailChange}       //handlerEmailChange
          validateEmail={email => {
            return handlerEmailValidate(email); 
          }}
          
          getLabel={(
            email: string,
            index: number,
            removeEmail: (index: number) => void
          ) => {
            return (
              <div 
              // onDoubleClick={(e:any)=> doubleClickHandler(e ,index)} 
              data-tag key={index}>
                {email}
                <span 
                data-testid="EmailInputBox_TagCross"
                data-tag-handle onClick={
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
        </div></div>
      </>
    );
  }



