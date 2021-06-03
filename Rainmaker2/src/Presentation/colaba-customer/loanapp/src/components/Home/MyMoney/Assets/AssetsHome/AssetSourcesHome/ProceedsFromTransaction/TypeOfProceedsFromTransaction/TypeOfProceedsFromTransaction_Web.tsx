import React from "react";
import {
    IconGiftFunds,
    IconCashGift,
    IconWhiteHouse
} from "../../../../../../../../Shared/Components/SVGs";

import IconRadioBox from "../../../../../../../../Shared/Components/IconRadioBox";
import { AssetTypesByCategory } from "../../../../../../../../Entities/Models/types";
import { NavigationHandler } from "../../../../../../../../Utilities/Navigation/NavigationHandler";
type props = {
    assetTypesByCategoryList: AssetTypesByCategory[],
    setSelectedPropertyType: Function
    selectedPropertyType: number
}
export const TypeOfProceedsFromTransaction_Web = ({ assetTypesByCategoryList, setSelectedPropertyType, selectedPropertyType }: props) => {

    const RenderAssetTypesByCategoryList = () => {
        const icons = {
            'Proceeds from a Loan': <IconCashGift />,
            'Proceeds from selling non-real estate assets': <IconWhiteHouse />,
            'Proceeds from selling real estate': <IconGiftFunds />
        }
        return (
            <React.Fragment>
                {
                    assetTypesByCategoryList?.map((item) => {
                        return (
                            <div data-testid="list-div" className="col-sm-6">
                                <IconRadioBox
                                    id={item.id}
                                    className={selectedPropertyType === item.id ? "active" : ''}
                                    name="radio1"
                                    checked={selectedPropertyType === item.id ? true : false}
                                    value={item.name}
                                    title={item.name}
                                    Icon={icons[item.name]}
                                    data-testid={`IconRadioBox-${item.id}`}
                                    handlerClick={(id) => {

                                        setSelectedPropertyType(id);
                                        //setPropertyTypeId(id);

                                        NavigationHandler.moveNext()
                                    }}
                                />
                            </div>
                        )
                    })
                }
            </React.Fragment>
        )
    }

    return (
        <React.Fragment>
            <div className="form-group">
                <h4>Which transaction are these proceeds from?</h4>
            </div>
            <div className="row">
                {
                    RenderAssetTypesByCategoryList()
                }
            </div>
        </React.Fragment>
    )
}