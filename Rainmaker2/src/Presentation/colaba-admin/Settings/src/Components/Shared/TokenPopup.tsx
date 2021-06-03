import React, { useContext, useEffect, useState } from 'react';
import { Token } from 'typescript';
import { Tokens } from '../../Entities/Models/Token';
import { RequestEmailTemplateActions} from '../../Store/actions/RequestEmailTemplateActions';
import { RequestEmailTemplateActionsType } from '../../Store/reducers/RequestEmailTemplateReducer';

import { Store } from '../../Store/Store';
import InfoDisplay from './../Shared/InfoDisplay';
import Loader, { WidgetLoader } from './Loader';
import {SVGSearch} from '../Shared/SVG';

interface TokenPopup{
    applyClass?: string;
    tokens: Tokens[] | undefined;
}

export const TokenPopup:React.FC<TokenPopup> = ({applyClass, tokens}) => {
    
    const { state, dispatch } = useContext(Store);
    const [tokenList, setTokenList] = useState<any>();

    useEffect(() => {
        if(tokens){
            setTokenList(tokens)
        }     
    }, [tokens])

    const handlerClick = (token: Tokens) => {
      dispatch({type: RequestEmailTemplateActionsType.SetSelectedToken, payload: token});
    }

    const tokenSearchHandler = (event: any) => {
     let value = event.target.value;
     let searchedList : Tokens[] | undefined = tokens?.filter((item : Tokens) => item.name?.toLowerCase().includes(value.toLowerCase()) || item.symbol?.toLowerCase().includes(value.toLowerCase()));
     setTokenList(searchedList);
    }

    if(!tokens || !tokenList){
        return <Loader size="xs"/>
    }

    const renderTableRows = () => {
        return tokenList.map((item: Tokens, index: number) => {
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
                <div className="settings__tokenpopup--search">
                    <input className="settings__tokenpopup--search-input" type="search" onChange={tokenSearchHandler} placeholder={`Search Tokens…`} />
                    <button className="settings__tokenpopup--search-btn">
                        <SVGSearch/>
                    </button>
                </div>
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
