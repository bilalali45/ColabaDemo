
import { APIResponse } from '../../../Entities/Models/APIResponse';
import { CurrentHomeAddressReqObj, DobSSNReqObj } from '../../../Entities/Models/types';

const countriesData = [
    {
        "id": 1,
        "name": "United States",
        "shortCode": "US"
    },
    {
        "id": 2,
        "name": "Andorra",
        "shortCode": "AD"
    },
    {
        "id": 3,
        "name": "United Arab Emirates",
        "shortCode": "AE"
    },
    {
        "id": 4,
        "name": "Afghanistan",
        "shortCode": "AF"
    },
    {
        "id": 5,
        "name": "Antigua and Barbuda",
        "shortCode": "AG"
    },
    {
        "id": 6,
        "name": "Anguilla",
        "shortCode": "AI"
    },
    {
        "id": 7,
        "name": "Albania",
        "shortCode": "AL"
    },
    {
        "id": 8,
        "name": "Armenia",
        "shortCode": "AM"
    },
    {
        "id": 9,
        "name": "Angola",
        "shortCode": "AO"
    },
    {
        "id": 10,
        "name": "Antarctica",
        "shortCode": "AQ"
    }]

const statesData = [
    {
        "id": 1,
        "countryId": 1,
        "name": "Alaska",
        "shortCode": "AK"
    },
    {
        "id": 2,
        "countryId": 1,
        "name": "Alabama",
        "shortCode": "AL"
    },
    {
        "id": 3,
        "countryId": 1,
        "name": "Arkansas",
        "shortCode": "AR"
    },
    {
        "id": 4,
        "countryId": 1,
        "name": "Arizona",
        "shortCode": "AZ"
    },
    {
        "id": 5,
        "countryId": 1,
        "name": "California",
        "shortCode": "CA"
    },
    {
        "id": 6,
        "countryId": 1,
        "name": "Colorado",
        "shortCode": "CO"
    },
    {
        "id": 7,
        "countryId": 1,
        "name": "Connecticut",
        "shortCode": "CT"
    },
    {
        "id": 8,
        "countryId": 1,
        "name": "District Of Columbia",
        "shortCode": "DC"
    },
    {
        "id": 9,
        "countryId": 1,
        "name": "Delaware",
        "shortCode": "DE"
    },
    {
        "id": 10,
        "countryId": 1,
        "name": "Florida",
        "shortCode": "FL"
    }
]

const homeOwnershipTypesData = [{ "id": 1, "name": "Own" }, { "id": 2, "name": "Rent" }, { "id": 3, "name": "Other" }]
const primaryBorrowerAddress = {
    city: "Lockbourne",
    countryId: 1,
    countryName: "United States",
    housingStatusId: 2,
    id: 14931,
    rent: 3456,
    stateId: 36,
    stateName: "Ohio",
    street: "6023 Alum Creek Dr",
    unit: "",
    zipCode: "43137"
}

export default class GettingToKnowYouActions {
    static async getAllCountries() {

        return new APIResponse(200, countriesData);

    }

    static async getStates() {

        return new APIResponse(200, statesData);

    }

    static async getHomeOwnershipTypes() {

        return new APIResponse(200, homeOwnershipTypesData);

    }

    static async addOrUpdatePrimaryBorrowerAddress(reqObj: CurrentHomeAddressReqObj) {
        return new APIResponse(200, "1002");
    }



    static async getBorrowerAddress(loanApplicationId: number, borrowerId: number) {

        return new APIResponse(200, primaryBorrowerAddress);
    }

    static async getDobSSN(loanApplicationId: number, borrowerId: number) {

        return new APIResponse(200, { "dobUtc": "1969-12-17T00:00:00", "ssn": "215454854" });
    }

    static async addOrUpdateDobSSN(reqObj: DobSSNReqObj) {

        return new APIResponse(200, 8002);

    }
};
