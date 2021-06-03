import React, { useContext, useEffect, useState } from 'react'
import { MyPropertyInfo } from '../../../../Entities/Models/types';
import { LocalDB } from '../../../../lib/LocalDB';
import MyPropertyActions from '../../../../store/actions/MyPropertyActions';
import { LoanApplicationActionsType } from '../../../../store/reducers/LoanApplicationReducer';
import { Store } from '../../../../store/store';
import { SectionTypeEnum } from '../../../../Utilities/Enumerations/MyPropertyEnums';
import { ErrorHandler } from '../../../../Utilities/helpers/ErrorHandler';
import { StringServices } from '../../../../Utilities/helpers/StringServices';
import { PropertyTypes } from '../PropertyTypes/PropertyTypes';

type CurrentResidenceProps = {
    setAddress: Function
   }

export const CurrentResidence = ({ setAddress }: CurrentResidenceProps) => {

    const [primaryAddress, setPrimaryAddress] = useState<string>();
    const [primaryBorrowerId, setPrimaryBorrowerId] = useState<number>();

    const { dispatch } = useContext(Store);


    useEffect(() => {
        if (!primaryAddress) {
            getPrimaryBorrowerAddressDetail();
        }
    }, []);

    const getPrimaryBorrowerAddressDetail = async () => {
        let loanApplicationId = Number(LocalDB.getLoanAppliationId());
        if (loanApplicationId) {
            var response = await MyPropertyActions.getPrimaryBorrowerAddressDetail(loanApplicationId)
            if (response) {
                if (ErrorHandler.successStatus.includes(response.statusCode)) {
                    if (response.data.address) {
                        var address = StringServices.addressGenerator({
                            countryId: response.data.address?.countryId,
                            countryName: response.data.address?.countryName,
                            stateId: response.data.address?.stateId,
                            stateName: response.data.address?.stateName,
                            cityName: response.data.address?.city,
                            streetAddress: response.data.address?.street,
                            zipCode: response.data.address?.zipCode,
                            unitNo: response.data.address?.unit
                        }, true);
                        setPrimaryAddress(address);
                    }
                    if (response.data) {
                        setPrimaryBorrowerId(response.data.borrowerId);
                    }
                } else {
                    //ErrorHandler.setError(dispatch, response);
                }
            }
        }
    };

    const runAfterSubmit = (response) => {

        if (ErrorHandler.successStatus.includes(response.statusCode)) {
            let data: MyPropertyInfo = {
                primaryPropertyTypeId: response.data as number
            };
            dispatch({
                type: LoanApplicationActionsType.SetMyPropertyInfo,
                payload: data,
            });
            setAddress(primaryAddress)
        } else {
            ErrorHandler.setError(dispatch, response);
        }
    }

    return (
        <PropertyTypes
            pageTitle="My Current Residence"
            title={'My current residence is a'}
            selectedTypeAction={'getBorrowerPrimaryPropertyType'}
            submitAction={'addOrUpdatePrimaryPropertyType'}
            runAfterSubmit={runAfterSubmit}
            address={primaryAddress}
            propertyTypeId={LocalDB.getMyPropertyTypeId()}
            primaryBorrowerId={primaryBorrowerId}
            setPropertyTypeId={LocalDB.setMyPropertyTypeId}
            sectionType={SectionTypeEnum.CurrentResidence}
            shouldAskForRentalIncome={true}
        />
    )
}
