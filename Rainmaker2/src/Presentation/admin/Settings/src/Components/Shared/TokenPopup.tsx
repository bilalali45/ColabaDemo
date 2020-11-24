import React, { useContext } from 'react';
import { isTypeNode } from 'typescript';
import { Tokens } from '../../Entities/Models/Token';
import { RequestEmailTemplateActions} from '../../Store/actions/RequestEmailTemplateActions';
import { RequestEmailTemplateActionsType } from '../../Store/reducers/RequestEmailTemplateReducer';

import { Store } from '../../Store/Store';
import InfoDisplay from './../Shared/InfoDisplay';
import Loader, { WidgetLoader } from './Loader';


interface TokenPopup{
    applyClass?: string;
}

export const TokenPopup:React.FC<TokenPopup> = ({applyClass}) => {

    const { state, dispatch } = useContext(Store);
    const emailTemplateManger: any = state.requestEmailTemplateManager;
    const tokens: Tokens[] = emailTemplateManger.tokens;

    const getTokens = async () => {
        let tokens = await RequestEmailTemplateActions.fetchTokens();
        if(tokens){           
            dispatch({type: RequestEmailTemplateActionsType.SetTokens, payload: tokens});
        }
     }
     
    if(!tokens){
        getTokens();
    }
 
    if(!tokens){
        return <Loader size="xs"/>
    }

    const handlerClick = (token: Tokens) => {
      dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: token});
    }

    const renderTableRows = () => {
        return tokens.map((item: Tokens, index: number) => {
        return(
            <>
               <tr data-testid= "tb-tr" key={index} onClick={() => handlerClick(item)} >
                 <td data-testid= "tb-td-detail" >
                   <h4>{item.name}</h4>
                   <p>{item.description}</p>
                 </td>
                 <td data-testid="tb-td-symbol" >
                 <h5>{item.symbol}</h5>
                 </td>
                </tr>
            </>
        )
    });
}

    return (
        <div data-testid="token-popup" className={`settings__tokenpopup ${applyClass}`}>
          <div className="settings__tokenpopup-wrap">
              <header data-testid= "popup-header" className="settings__tokenpopup--header">
                <h4 className="settings__tokenpopup--title">Token List <InfoDisplay tooltipType={7}/></h4>
              </header>
              <section className="settings__tokenpopup--body">
                <table className="table table-striped">
                <colgroup>
                    <col span={1} style={{ width: '50%' }} />
                    <col span={2} style={{ width: '50%' }} />
                </colgroup>
                    <thead>
                        <tr data-testid="tb-heads" >
                            <th>Token Details </th>
                            <th>Symbol </th>
                        </tr>
                    </thead>
                    <tbody>
                           {renderTableRows()}                     
                    </tbody>
                </table>
              </section>
          </div>
        </div>
    )
}
