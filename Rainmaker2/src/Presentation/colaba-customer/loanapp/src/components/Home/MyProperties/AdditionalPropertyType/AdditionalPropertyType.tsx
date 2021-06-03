import React, { useContext, useState } from 'react'
import { LocalDB } from '../../../../lib/LocalDB';
import { Store } from '../../../../store/store';
import { SectionTypeEnum } from '../../../../Utilities/Enumerations/MyPropertyEnums';
import { PropertyTypes } from '../PropertyTypes/PropertyTypes';


export const AdditionalPropertyType = () => {

    const [address] = useState<string>();

    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    const onValueChanges = () => {

    }

    return (
        <PropertyTypes
            pageTitle="My Properties"
            title={'This property is a'}
            selectedTypeAction={'getBorrowerAdditionalPropertyType'}
            submitAction={'addOrUpdateAdditionalPropertyType'}
            runAfterSubmit={() => { }}
            address={address}
            primaryBorrowerId={+primaryBorrowerInfo.id}
            propertyTypeId={LocalDB.getAddtionalPropertyTypeId()}
            setPropertyTypeId={LocalDB.setAdditionalPropertyTypeId}
            sectionType={SectionTypeEnum.AdditionalProperty}
            onValueChanges={onValueChanges}
        />
    )
}
