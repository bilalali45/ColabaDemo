import React from 'react'
import { SelectedOtherIncomeType } from '../../../../../../../Entities/Models/types';
import { 
    OiFamilyIcon, OiInvestmentsIcon, OiHousingIcon, OiGovernmentIcon, OiMiscellaneousIcon, ToRightArrowIcon
} from '../../../../../../../Shared/Components/SVGs';

type props = {
    otherIncomeTypes: any[];
    OtherIncomeTypeChange: (data: SelectedOtherIncomeType) => void;
}

export const OtherIncomeWeb = ({otherIncomeTypes, OtherIncomeTypeChange}: props) => {

    const icons = {
        'Family': <OiFamilyIcon />,
        'Investments': <OiInvestmentsIcon />,
        'Housing': <OiHousingIcon />,
        'Government': <OiGovernmentIcon />,
        'Miscellaneous': <OiMiscellaneousIcon />,
        'Other': <OiMiscellaneousIcon />,
      }

    return (
      
        <div  className="compo-otherincome">
        <div data-testid="otherIncome-row" className="row">
            <div  className="col-sm-12">
                <div className="form-group"><h4>Select Your Income Type:</h4></div>
            </div>
            {otherIncomeTypes.map(type => 
            <div className="col-sm-4">
                <div className="oi-card-wrap">
                    <div data-testid="other-grp" className="oi-icon">{icons[type.incomeGroupName]}</div>
                    <div  className="oi-content">
                        <h5>{type.incomeGroupDisplayName}</h5>
                        <ul>
                            {
                              type.incomeTypes.map((item : SelectedOtherIncomeType) =>
                                <li data-testid="other-list" onClick= {() => OtherIncomeTypeChange(item)} ><span className="a-icon"><ToRightArrowIcon /></span> {item.name}</li>
                                )
                            }
                        </ul>
                    </div>
                </div>
            </div>
               ) }                                             
        </div>
    </div>
    )
}
