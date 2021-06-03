import React from 'react'
import { PropertyMortgageFirstStep } from '../PropertyMortgage/PropertyMortgageFirstStep/PropertyMortgageFirstStep';
import { LocalDB } from '../../../../lib/LocalDB';

export const AdditionalPropertyFirstMortgage = ({ address }) => {

    return <PropertyMortgageFirstStep
        propertyId={LocalDB.getAddtionalPropertyTypeId()}
        title={'My Properties'}
        address={address}
        animatedText={'Tell us about additional property - Mortgage.'} />
}
