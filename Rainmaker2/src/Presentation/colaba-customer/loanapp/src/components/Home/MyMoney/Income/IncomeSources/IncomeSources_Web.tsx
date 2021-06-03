import React from 'react';
import { IncomeSourceType } from '../../../../../Entities/Models/types';
import IconRadioSnipit2 from '../../../../../Shared/Components/IconRadioSnipit2';
import { IconEmployment, IconSelfEmployment, IconBusiness, IconMilitaryPay, IconRetirement, IconRental, IconOther } from '../../../../../Shared/Components/SVGs';

type props = {
    IncomeSourceList: IncomeSourceType[],
    selectType: Function
}

export const IncomeSourcesWeb = ({IncomeSourceList, selectType}: props) => {
    
    const RenderIncomeSourceList = () => {
        const icons = {
            'Employment': <IconEmployment />,
            'Self Employment / Independent Contractor': <IconSelfEmployment />,
            'Business': <IconBusiness />,
            'Military Pay': <IconMilitaryPay />,
            'Retirement' : <IconRetirement/>,
            'Rental': <IconRental/>,
            'Other': <IconOther/>
          }
        
        return (
            <>
            {
            IncomeSourceList?.map((item: IncomeSourceType) => {
              return(
                <div data-testid="list-div" className="col-sm-6">
                <IconRadioSnipit2

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
        <div  className="row">
           
            {
                RenderIncomeSourceList()
            }

        </div>
    )
}
