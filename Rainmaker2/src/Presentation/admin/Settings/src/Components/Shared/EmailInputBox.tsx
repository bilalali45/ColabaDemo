import  React ,{useState, useEffect, useRef} from "react";
import { ReactMultiEmail, isEmail  } from "react-multi-email";
import "react-multi-email/style.css";
import { Tokens } from "../../Entities/Models/Token";

type props = {
  handlerEmail: Function;
  tokens: Tokens[];
  handlerClick: Function;
  exisitngEmailValues?: string[] | null;
  className?:any;
  dataTestId: string;
  id?:any;
}

export const EmailInputBox = (props: props) => {
  const [emails, setEmailArray] = useState<string[]>([]);
  const existingEmailValues = props.exisitngEmailValues;
  

  useEffect(() => {
    if (existingEmailValues){
      setEmailArray(existingEmailValues)
    }
  }, [existingEmailValues]);

  const handlerEmailChange = (content: string[]) => {
    setEmailArray(content)
    props.handlerEmail(content);
   }
   
  const  handlerEmailValidate = (text: string) => {
       if(isEmail(text)){ // check valid email address
          return true;
       } else if(checkTokenIsValid(text)){ //check valid token
         return true;
       } else{
         return false;
       }    
   } 

  const checkTokenIsValid = (text: string) => {
    let tokens: Tokens[]; 
    let result: boolean = false

    if(props.id === "defaultFromAddress"){
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

  
    return (
      <>
      <div  data-testid={props.dataTestId}  onFocus={ClickHandler} onClick={ClickHandler} className={`settings__multi-pills-control ${props.className?props.className:''}`}>
        <ReactMultiEmail
          className = {props.dataTestId}
          emails={emails}
          onChange={handlerEmailChange}          
          validateEmail={email => {
            return handlerEmailValidate(email); 
          }}
          getLabel={(
            email: string,
            index: number,
            removeEmail: (index: number) => void
          ) => {
            return (
              <div data-tag key={index}>
                {email}
                <span data-tag-handle onClick={() => removeEmail(index)}>
                  ×
                </span>
              </div>
            );
          }}
        />
        </div>
      </>
    );
  }



