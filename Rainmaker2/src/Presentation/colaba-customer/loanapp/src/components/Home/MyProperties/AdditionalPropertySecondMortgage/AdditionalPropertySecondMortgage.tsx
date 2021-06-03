import React, { useContext } from 'react'
import { LocalDB } from '../../../../lib/LocalDB';
import { Store } from '../../../../store/store';
import { PropertyMortgageSecondStep } from '../PropertyMortgage/PropertyMortgageSecondStep/PropertyMortgageSecondStep';


export const AdditionalPropertySecondMortgage = ({ address }) => {

    const { state } = useContext(Store);
    const { primaryBorrowerInfo }: any = state.loanManager;

    return <PropertyMortgageSecondStep
        propertyId={LocalDB.getAddtionalPropertyTypeId()}
        title={'My Properties'}
        address={address}
        animatedText={`${primaryBorrowerInfo?.name}, please tell us a bit more about the second mortgage on this property.`} />

}
