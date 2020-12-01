import  React ,{useState, useEffect} from "react";
import { ReactMultiEmail, isEmail  } from "react-multi-email";
import "react-multi-email/style.css";

type props = {
  handlerEmail: Function;
  tokens: string[];
  exisitngEmailValues?: string[] | null;
  className?: string;
  errorHandler?: any;
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
       if(isEmail(text)){
          return true;
       }else{
        props.errorHandler()
         return false;
       }    
   } 

    return (
      <>
        <ReactMultiEmail
          placeholder="Input your Email Address"
          emails={emails}
          className={props.className}
          onChange={handlerEmailChange}          
          validateEmail={(email:any) => {
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
      </>
    );
  }



