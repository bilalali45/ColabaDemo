import { Country, State } from "../../Entities/Models/types";
import { LocalDB } from "../../lib/LocalDB";
import MyPropertyActions from "../../store/actions/MyPropertyActions";

export class AdditionalPropertyAddressCalculator {
    static async getAdditionalPropertyAddress(setAddress) {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let propertyId = LocalDB.getAddtionalPropertyTypeId();
        try {
            let res = await MyPropertyActions.getBorrowerAdditionalPropertyAddress(loanApplicationId, propertyId);
            let addressAsString = this.createAddressString(res.data);
            setAddress(addressAsString);
            return res?.data;
        } catch (error) {
            console.log('error >>>>>', error);
        }
    }


    static extractAddress(data, countries, states) {
        console.log(data);
        let { street_address, unit, city, zip_code, country } = data;

        if (!street_address) {
            return null;
        }

        if (!country.length) country = "United States";
        let stateEle = document.querySelector("#state") as HTMLSelectElement;
        let countryEle = document.querySelector("#country") as HTMLSelectElement;
        let stateObj = states && states?.filter((s: State) => s.name === stateEle.value)[0];
        let countryObj = countries && countries?.filter((c: Country) => c.name === countryEle.value)[0];

        return {
            city: city,
            countryId: countryObj ? countryObj.id : null,
            stateId: stateObj ? stateObj.id : null,
            stateName: stateObj ? stateObj.name : "",
            street: street_address,
            unit: unit,
            zipCode: zip_code,
            countryName: country
        }
    }

    static createAddressString({ street, city, stateName, zipCode }) {
        return `${street}, ${city}, ${stateName}, ${zipCode}`
    }

}