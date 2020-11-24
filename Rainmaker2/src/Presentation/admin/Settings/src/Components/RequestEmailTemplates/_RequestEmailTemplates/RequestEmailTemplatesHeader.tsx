import React, {useEffect, useState, useRef, useContext} from 'react'
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer'
import { Store } from '../../../Store/Store'
import ContentHeader from '../../Shared/ContentHeader'
import { TokenPopup } from '../../Shared/TokenPopup'


type props = {
  addEmailTemplateClick?: any;
  showEmailList?: boolean;
  showinsertToken?: boolean;
  insertTokenClick?: any;
}

export const RequestEmailTemplatesHeader = ({ addEmailTemplateClick, showEmailList, showinsertToken, insertTokenClick }: props) => {

  const [insertTokenPopup, setInsertTokenPopup] = useState<boolean>(false);
  const insertTokenDropDown = useRef<HTMLDivElement>(null);
  const { state, dispatch } = useContext(Store);
  
  useEffect(()=>{
    document.addEventListener('click',(e:any)=>{
      if(!insertTokenDropDown.current?.contains(e.target)){
        setInsertTokenPopup(false);
        dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
      }
    });
    return ()=>{

    }
  },[]);

  const activeTokenPopup = () => {
    setInsertTokenPopup(!insertTokenPopup)
    dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: null});
  }

  return (
    <ContentHeader title="Request Email Templates" tooltipType={9} className="request-email-templates-header">
      <div data-testid="add-emailTemplate" className="dropdown arrow-right float-right">
        {showEmailList &&
          <button
            onClick={e => addEmailTemplateClick(e)}
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
              <TokenPopup applyClass="arrow-right"/>
            }            
          </div>
        }

      </div>
    </ContentHeader>

  )
}
