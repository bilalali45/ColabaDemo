import React from 'react';

import {
    EditIcon,
    ReviewHomeIcon
} from '../../../../Shared/Components/SVGs';

import {MortgagePropertyInfoProto} from '../../../../Entities/Models/types';

import { StringServices } from '../../../../Utilities/helpers/StringServices';

export const NewMortgagePropertyCard: React.FC<{
    editMortgage: Function
    propertyDetails: MortgagePropertyInfoProto
}> = ({ editMortgage, propertyDetails}) => {

    return (

        <>
            <div className="review-item">
                <div className="r-icon">
                    <ReviewHomeIcon />
                </div>
                <div className="r-content">
                    <div className="title">
                        <h4>Purchase Property</h4>
                    </div>
                    <div className="email">
                        {propertyDetails?.propertyTypeName}
                    </div>
                    <div className="email">
                        {propertyDetails?.propertyUsageName}
                    </div>
                    <div className="otherinfo">
                        {StringServices.addressGenerator(propertyDetails?.addressInfo,true)}
                    </div>
                </div>
                <div className="r-actions">
                    <button className="btn btn-edit" onClick={() => { editMortgage() }}>
                                    <span className="icon">
                                        <EditIcon />
                                    </span>
                        <span className="lbl" >Edit</span>
                    </button>
                </div>
            </div>
           
        </>
    )
};
