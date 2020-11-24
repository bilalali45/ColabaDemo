import  React ,{useState, useEffect} from "react";
import { ReactMultiEmail, isEmail  } from "react-multi-email";
import "react-multi-email/style.css";

type props = {
  handlerEmail: Function;
  tokens: string[];
  handlerClick: Function;
  exisitngEmailValues?: string[] | null;
  className?:any;
  dataTestId: string;
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
     let tokens: string[] = props.tokens;
     let result: boolean = false
     result = tokens.some(item => item === text);
     return result;
   }

  const ClickHandler = () => {
    props.handlerClick();
  }
  
    return (
      <>
      <div data-testid={props.dataTestId}  onFocus={ClickHandler} onClick={ClickHandler} className={`settings__multi-pills-control ${props.className?props.className:''}`}>
        <ReactMultiEmail
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


