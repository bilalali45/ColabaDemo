import React from 'react'
import IconRadioSnipit2 from '../../../../../../../Shared/Components/IconRadioSnipit2'
import { IconBankAccount, IconGiftFunds, IconOther, IconProceedsTransaction, IconRetirementAccount, IconStocks } from '../../../../../../../Shared/Components/SVGs';
import {AssetSourceType} from '../../../../../../../Entities/Models/types';

type props = {
    AssetSourceList: AssetSourceType[],
    selectType: Function
}

export const AssetSourceWeb = ({AssetSourceList, selectType}: props) => {
    

    const RenderAssetSourceList = () => {
        const icons = {
            'Bank Account': <IconBankAccount />,
            'Retirement Account': <IconRetirementAccount />,
            'Gift Funds': <IconGiftFunds />,
            'Stocks, Bonds, Or Other Financial Assets': <IconStocks />,
            'Proceeds from Transactions' : <IconProceedsTransaction/>,
            'Other': <IconOther/>,
            'Credits': <IconStocks/>
          }
          return (
            <>
            {
            AssetSourceList?.map((item: AssetSourceType) => {
              return(
                <div data-testid="list-div" className="col-sm-6">
                <IconRadioSnipit2
                    dataTestId = "radioBox"
                    id={item.id}
                    title={item.displayName}
                    icon={icons[item.name]}
                    handlerClick={() => { 
                        selectType(item);
                    }}
                    className=""
                />
            </div>
              )
            })
            }
            </>
        )
    
}
    return (
        <div className="row">
           
           {
                RenderAssetSourceList()
            }

        </div>
    )
}
