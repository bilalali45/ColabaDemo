import React, {useEffect, useState, useRef, useContext} from 'react'
import { Tokens } from '../../../Entities/Models/Token'
import { ContentSubHeader } from '../../Shared/ContentHeader';
import { TokenPopup } from '../../Shared/TokenPopup';
import { Store } from '../../../Store/Store';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { disableBrowserPrompt } from '../../../Utils/helpers/Common'

type props = {
    showinsertToken?: boolean;
    selectedField: string;
  }

export const ReminderEmailSubHeader = ({ showinsertToken, selectedField }: props) => {
    const { state, dispatch } = useContext(Store);
    const emailTemplateManger: any = state.requestEmailTemplateManager;
    const tokens: Tokens[] = emailTemplateManger.tokens;
    const [insertTokenPopup, setInsertTokenPopup] = useState<boolean>(false);
    const insertTokenDropDown = useRef<HTMLDivElement>(null);
    const [tokenList, setTokenList] = useState<Tokens[]>();
    const emailReminderManager: any = state.emailReminderManager;
    const reminderEmailData = emailReminderManager.reminderEmailData;
    const selectedreminderEmail = emailReminderManager.selectedReminderEmail;
    const [number, setNumber] = useState<string>('');

    useEffect(()=>{
        document.addEventListener('click',(e:any)=>{
          if(!insertTokenDropDown.current?.contains(e.target)){
            setInsertTokenPopup(false);
            dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
          }
        });
        return ()=>{
         disableBrowserPrompt();
        }
    },[]);
    
    useEffect(() => {
        if(tokens && selectedField){
          let list: Tokens[] = setSelectedTokenList(selectedField, tokens);
          setTokenList(list)
        }
    
    }, [selectedField, tokens]);
    
    const checkReminderEmailNumber = () => {
      debugger
      let num = selectedreminderEmail?.index?.substring(1,4);
      if(String(num).length == 1 || String(num).length == 0){
        if(num) return '0'+num;
        else return "01";
      }else{
        return num;
      }
    }
    
    useEffect(() => {      
      setNumber(checkReminderEmailNumber());
    },[selectedreminderEmail])
    
    const setSelectedTokenList = (selectedField: string, tokens: Tokens[]) => {
        switch(selectedField){
          case "fromAddress":
            return tokens.filter(item => item.fromAddess === true);    
          case "ccAddress":
            return tokens.filter(item => item.ccAddess === true); 
          case "subjectLine":
            return tokens.filter(item => item.emailSubject === true); 
          default:
            return tokens.filter(item => item.emailBody === true); 
        }
    }
    
    const activeTokenPopup = () => {
        setInsertTokenPopup(!insertTokenPopup)
        dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
    }

    return (
        
       <>
         <ContentSubHeader title= {`Reminder Email - ${number ? number : ''}`} className="nlre-settings-header">  
            <div data-testid="add-emailTemplate" className="arrow-right float-right">
                    {showinsertToken &&
                    <div className="dropdown" ref={insertTokenDropDown}>
                        <button  className="settings-btn dropdown-toggle" data-testid="insertToken-btn" onClick={activeTokenPopup}>Insert Tokens <i className="zmdi zmdi-plus"></i></button>
                        {insertTokenPopup &&
                        <TokenPopup tokens = {tokenList} applyClass="arrow-right"/>
                        }            
                    </div>
                    }

            </div>
        </ContentSubHeader>
       </>
    )
}
