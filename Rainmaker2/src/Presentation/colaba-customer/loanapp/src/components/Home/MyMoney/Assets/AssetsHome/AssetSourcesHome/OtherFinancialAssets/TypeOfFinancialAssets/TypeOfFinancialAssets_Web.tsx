import React from 'react'
import { FinancialAssets } from '../../../../../../../../Entities/Models/types'
import IconRadioBox from '../../../../../../../../Shared/Components/IconRadioBox'
import { IconBonds, IconCertificateOfDeposit, IconMoneyMarket, IconMutualFunds, IconStockOptions, IconStocks2 } from '../../../../../../../../Shared/Components/SVGs'



type props = {
    financialAssetList: FinancialAssets[];
    onSelectHandler: Function;
    selectedItem: FinancialAssets;
}

export const TypeOfFinancialAssetsWeb = ({financialAssetList, onSelectHandler, selectedItem}: props) => {

    const RenderFinancialAssetList = () => {
        const icons = {
            'MutualFunds': <IconMutualFunds />,
            'Stocks': <IconStocks2 />,
            'MoneyMarket': <IconMoneyMarket />,
            'Stock Options': <IconStockOptions />,
            'Certificate of Deposit' : <IconCertificateOfDeposit/>,
            'Bonds': <IconBonds/>,
          }
          return (
              <>
              {financialAssetList && financialAssetList.length &&financialAssetList?.map((item: FinancialAssets) => {
                return(
                    <div className="col-md-6">
                    <IconRadioBox
                        id={item.id}
                        className= {selectedItem?.id === item.id ? "active" :''}
                        name="radio1"
                        checked={selectedItem?.id === item.id ?  true: false}
                        value={item.name}
                        title={item.name}
                        Icon={icons[item.name]}
                        handlerClick = {() => {
                            onSelectHandler(item)
                        }}
                    />
                </div>
                )
              })

              }
              </>
          )
    }

    return (
        <div>
             <div className="p-body">
             <div data-testid="subtitle" className="form-group">
                <h3 className="h3">What type of financial asset is this?</h3>
            </div>
            <div  className="row">
               {RenderFinancialAssetList()}                                                     
            </div>
            </div>
        </div>
    )
}
