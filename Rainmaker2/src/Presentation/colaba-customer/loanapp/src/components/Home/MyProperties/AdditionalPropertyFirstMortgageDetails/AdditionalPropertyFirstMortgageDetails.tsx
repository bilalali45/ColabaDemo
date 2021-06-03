import React from 'react'
import { PropertyMortgageFirstStepDetails } from '../PropertyMortgage/PropertyMortgageFirstStepDetails/PropertyMortgageFirstStepDetails';
import { LocalDB } from '../../../../lib/LocalDB';
type Props = {
  address:string
}

export const AdditionalPropertyFirstMortgageDetails = ({address}: Props) => {

    return <PropertyMortgageFirstStepDetails
        propertyId={LocalDB.getAddtionalPropertyTypeId()}
        title={'My Properties'}
        address={address}
        animatedText={'Tell us about additional property - Mortgage.'} />

}
