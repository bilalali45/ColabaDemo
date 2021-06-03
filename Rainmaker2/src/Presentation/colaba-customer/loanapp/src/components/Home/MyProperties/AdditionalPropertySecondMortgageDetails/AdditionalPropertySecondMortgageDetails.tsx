import React from 'react'
import { LocalDB } from '../../../../lib/LocalDB'
import { PropertyMortgageSecondStepDetails } from '../PropertyMortgage/PropertyMortgageSecondStepDetails/PropertyMortgageSecondStepDetails'

type AdditionalPropertySecondMortgageDetailsProps = {
  address:string
}

export const AdditionalPropertySecondMortgageDetails = ({address}:AdditionalPropertySecondMortgageDetailsProps) => {

    return <PropertyMortgageSecondStepDetails
        propertyId={LocalDB.getAddtionalPropertyTypeId()}
        title={'My Properties'}
        address={address}
        animatedText={'Onwards to second mortgages (if you have any)'} />

}
