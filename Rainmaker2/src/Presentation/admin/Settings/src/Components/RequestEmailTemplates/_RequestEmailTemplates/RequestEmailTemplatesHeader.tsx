import React, {useEffect, useState, useRef, useContext} from 'react'
import { Tokens } from '../../../Entities/Models/Token'
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer'
import { Store } from '../../../Store/Store'
import { disableBrowserPrompt } from '../../../Utils/helpers/Common'
import ContentHeader from '../../Shared/ContentHeader'
import { TokenPopup } from '../../Shared/TokenPopup'


type props = {
  addEmailTemplateClick?: any;
  showEmailList?: boolean;
  showinsertToken?: boolean;
  insertTokenClick?: any;
  selectedField: string;
}

export const RequestEmailTemplatesHeader = ({ addEmailTemplateClick, showEmailList, showinsertToken, insertTokenClick, selectedField }: props) => {
 
  const { state, dispatch } = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const tokens: Tokens[] = emailTemplateManger.tokens;
  const [insertTokenPopup, setInsertTokenPopup] = useState<boolean>(false);
  const insertTokenDropDown = useRef<HTMLDivElement>(null);
  const [tokenList, setTokenList] = useState<Tokens[]>();

  useEffect(()=>{
    document.addEventListener('click',(e:any)=>{
      if(!insertTokenDropDown.current?.contains(e.target)){
        setInsertTokenPopup(false);
        dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
      }
    });
    return ()=>{
     console.log('Request Email Template unmounting...')
     disableBrowserPrompt();
    }
  },[]);

  useEffect(() => {
    if(tokens && selectedField){
      let list: any = setSelectedTokenList(selectedField, tokens);
      setTokenList(list)
    }

  }, [selectedField, tokens]);

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
    <ContentHeader title="Request Email Templates" tooltipType={9} className="request-email-templates-header">
      <div data-testid="add-emailTemplate" className="dropdown arrow-right float-right">
        {showEmailList &&
          <button
            onClick={() => addEmailTemplateClick(true)}
            className="settings-btn dropdown-toggle"
            data-testid="addNewTemplate-btn">
            Add Email Template
                     <i className="zmdi zmdi-plus"></i>

          </button>
        }
        {showinsertToken && !showEmailList &&
          <div className="dropdown" ref={insertTokenDropDown}>
            <button  className="settings-btn dropdown-toggle" data-testid="insertToken-btn" onClick={activeTokenPopup}>Insert Tokens <i className="zmdi zmdi-plus"></i></button>
            {insertTokenPopup &&
              <TokenPopup tokens = {tokenList} applyClass="arrow-right"/>
            }            
          </div>
        }

      </div>
    </ContentHeader>

  )
}
