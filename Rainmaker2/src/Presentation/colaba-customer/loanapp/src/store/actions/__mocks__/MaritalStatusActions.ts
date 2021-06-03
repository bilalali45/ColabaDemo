import { Http } from "rainsoft-js";
import moment from "moment";
export default class MaritalStatusActions {
    static async getAllMaritalStatuses() {
        
        try {
            
            return [{ "id": 1, "name": "Married" }, { "id": 2, "name": "Separated" }, { "id": 3, "name": "Single" }, { "id": 4, "name": "Divorced" }, { "id": 5, "name": "Widowed" }, { "id": 6, "name": "Civil Union" }, { "id": 7, "name": "Domestic Partnership" }, { "id": 8, "name": "Registered Reciprocal Beneficiary Relationship" }];
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async getMaritalStatus(loanapplicationId: number, borrowerId: number) {
        
        try {
            
            return { "maritalStatus": 7, "relationshipWithPrimary": null };
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }
    static async addOrUpdateMaritalStatus(payload: MaritalStatusActions) {
        try {
            
            return "";
        } catch (error) {
            console.log(error);
            return error;
        }

    }
}