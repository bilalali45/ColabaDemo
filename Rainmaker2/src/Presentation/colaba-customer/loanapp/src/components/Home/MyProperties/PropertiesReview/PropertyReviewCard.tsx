import React from 'react';

import { EditIcon } from '../../../../Shared/Components/SVGs';
import { GetReviewMyPropertyInfoSectionProto } from '../../../../Entities/Models/types';
import { StringServices } from '../../../../Utilities/helpers/StringServices';

export const PropertyReviewCard: React.FC<{
    heading: string,
    properties: GetReviewMyPropertyInfoSectionProto[],
    editProperty: Function
}> = ({ heading, properties, editProperty }) => {
    return (
        <>
            <div className="review-item-e-history p-r-list" data-testid="propertyViewCard">
                <div className="r-content">
                    <div className="eh-title">
                        <h4>{heading}</h4>
                    </div>



                    {(properties?.map((info: GetReviewMyPropertyInfoSectionProto) => {
                        return (
                            <>
                                <div className="otherinfo p-reviewinfo">
                                    <div className="p-review-wrap">
                                        <h5>{StringServices.generateAddress(info?.street, info?.unit, info?.city, info?.stateName, info?.zipCode, info?.countryName)}</h5>
                                        <p>{info?.propertyType}</p>
                                    </div>

                                    <div className="r-actions">
                                        <button data-testid="btn-edit" className="btn btn-edit" onClick={() => {
                                            editProperty(info?.id, info?.typeId)
                                        }}>
                                            <span className="icon">
                                                <EditIcon />
                                            </span>
                                            <span className="lbl">Edit</span>
                                        </button>
                                    </div>

                                </div>
                            </>
                        )
                    }))}
                </div>
            </div>
        </>)
};
