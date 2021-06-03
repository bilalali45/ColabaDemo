import React, { useContext, useEffect, useState } from 'react'
import { Switch } from 'react-router'
import { ApplicationEnv } from '../../../lib/appEnv'
import { LocalDB } from '../../../lib/LocalDB'
import MyPropertyActions from '../../../store/actions/MyPropertyActions'
import { Store } from '../../../store/store'
import { AdditionalPropertyAddressCalculator } from '../../../Utilities/helpers/AdditionalPropertyAddressCalculator'
import { ErrorHandler } from '../../../Utilities/helpers/ErrorHandler'
import { StringServices } from '../../../Utilities/helpers/StringServices'
import { IsRouteAllowed } from '../../../Utilities/Navigation/navigation_settings/IsRouteAllowed'
import { AdditionalPropertyAddress } from './AdditionalPropertyAddress/AdditionalPropertyAddress'
import { AdditionalPropertyDetails } from './AdditionalPropertyDetails/AdditionalPropertyDetails'
import { AdditionalPropertyFirstMortgage } from './AdditionalPropertyFirstMortgage/AdditionalPropertyFirstMortgage'
import { AdditionalPropertyFirstMortgageDetails } from './AdditionalPropertyFirstMortgageDetails/AdditionalPropertyFirstMortgageDetails'
import { AdditionalPropertySecondMortgage } from './AdditionalPropertySecondMortgage/AdditionalPropertySecondMortgage'
import { AdditionalPropertySecondMortgageDetails } from './AdditionalPropertySecondMortgageDetails/AdditionalPropertySecondMortgageDetails'
import { AdditionalPropertyType } from './AdditionalPropertyType/AdditionalPropertyType'
import { AdditionalPropertyUsage } from './AdditionalPropertyUsage/AdditionalPropertyUsage'
import { AllProperties } from './AllProperties/AllProperties'
import { CurrentResidence } from './CurrentResidence/CurrentResidence'
import { CurrentResidenceDetails } from './CurrentResidenceDetails/CurrentResidenceDetails'
import { FirstCurrentResidenceMortgage } from './FirstCurrentResidenceMortgage/FirstCurrentResidenceMortgage'
import { FirstCurrentResidenceMortgageDetails } from './FirstCurrentResidenceMortgageDetails/FirstCurrentResidenceMortgageDetails'
import { PropertiesOwned } from './PropertiesOwned/PropertiesOwned'
import { PropertiesReview } from './PropertiesReview/PropertiesReview'
import { SecondCurrentResidenceMortgage } from './SecondCurrentResidenceMortgage/SecondCurrentResidenceMortgage'
import { SecondCurrentResidenceMortgageDetails } from './SecondCurrentResidenceMortgageDetails/SecondCurrentResidenceMortgageDetails'

const containerPath = `${ApplicationEnv.ApplicationBasePath}/MyProperties`;


export const MyProperties = () => {

    const [additionalPropertyAddress, setAdditionalPropertyAddress] = useState();
    const [primaryPropertyAddress, setPrimaryPropertyAddress] = useState<string>();
    const [editting, setEditting] = useState(false);
    const { dispatch } = useContext(Store);

    useEffect(() => {
        if (primaryPropertyAddress) {
            setPrimaryPropertyAddress(primaryPropertyAddress);
        } else {
            getPrimaryBorrowerAddressDetail();
        }

        if (!additionalPropertyAddress && LocalDB.getAddtionalPropertyTypeId()) {
            AdditionalPropertyAddressCalculator.getAdditionalPropertyAddress(setAdditionalPropertyAddress);
        }
    }, []);

    const getPrimaryBorrowerAddressDetail = async () => {
        // let loanApplicationId = LocalDB.getLoanAppliationId();
        // if (loanApplicationId) {
        var response = await MyPropertyActions.getPrimaryBorrowerAddressDetail(
            +LocalDB.getLoanAppliationId()
        );
        if (response) {
            if (ErrorHandler.successStatus.includes(response.statusCode)) {
                if (response.data.address) {
                    var propertyAddress = StringServices.addressGenerator({
                        countryId: response.data.address?.countryId,
                        countryName: response.data.address?.countryName,
                        stateId: response.data.address?.stateId,
                        stateName: response.data.address?.stateName,
                        cityName: response.data.address?.city,
                        streetAddress: response.data.address?.street,
                        zipCode: response.data.address?.zipCode,
                        unitNo: response.data.address?.unit,
                    }, true);

                    setPrimaryPropertyAddress(propertyAddress);
                }
            } else {
                ErrorHandler.setError(dispatch, response);
            }
            // }
        }
    };

    return (
        <div className="loanapp-p-MyNewMortgage fadein">
            {/* <button onClick={() => NavigationHandler.moveNext()}>Test Move Next</button> */}
            {/* <button onClick={() => NavigationHandler.moveBack()}>Test Move Back</button> */}
            <Switch>

                <IsRouteAllowed
                    path={`${containerPath}/CurrentResidence`}
                    component={CurrentResidence}
                    setAddress={setPrimaryPropertyAddress}
                />

                <IsRouteAllowed
                    path={`${containerPath}/CurrentResidenceDetails`}
                    component={CurrentResidenceDetails}
                    address={primaryPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/FirstCurrentResidenceMortgage`}
                    component={FirstCurrentResidenceMortgage}
                    address={primaryPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/FirstCurrentResidenceMortgageDetails`}
                    component={FirstCurrentResidenceMortgageDetails}
                    address={primaryPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/SecondCurrentResidenceMortgage`}
                    component={SecondCurrentResidenceMortgage}
                    address={primaryPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/SecondCurrentResidenceMortgageDetails`}
                    component={SecondCurrentResidenceMortgageDetails}
                    address={primaryPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/PropertiesOwned`}
                    component={PropertiesOwned}
                    editting={editting} />

                <IsRouteAllowed
                    path={`${containerPath}/AllProperties`}
                    component={AllProperties}
                    setEditting={setEditting} />


                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyType`}
                    component={AdditionalPropertyType}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyAddress`}
                    component={AdditionalPropertyAddress}
                    address={additionalPropertyAddress}
                    setAddress={setAdditionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyUsage`}
                    component={AdditionalPropertyUsage}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyDetails`}
                    component={AdditionalPropertyDetails}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyFirstMortgage`}
                    component={AdditionalPropertyFirstMortgage}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertyFirstMortgageDetails`}
                    component={AdditionalPropertyFirstMortgageDetails}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertySecondMortgage`}
                    component={AdditionalPropertySecondMortgage}
                    address={additionalPropertyAddress} />

                <IsRouteAllowed
                    path={`${containerPath}/AdditionalPropertySecondMortgageDetails`}
                    component={AdditionalPropertySecondMortgageDetails}
                    address={additionalPropertyAddress} />


                <IsRouteAllowed
                    path={`${containerPath}/PropertiesReview`}
                    component={PropertiesReview}
                />

            </Switch>
        </div>
    )
}
