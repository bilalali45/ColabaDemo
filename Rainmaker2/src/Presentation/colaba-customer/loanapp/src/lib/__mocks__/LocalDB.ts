import { ApplicationEnv } from "../appEnv";
import { UrlQueryManager } from '../../Utilities/Navigation/UrlQueryManager';

export class LocalDB {
    // ---- Please set these properties by calling set functions from .test. Refer to CurrentResidence.test.tsx ----
    static myPropertyTypeId: number = null; 
    static loanApplicationId: number = null;
    static borrowerId: number = null;
    static additionalPropertyTypeId: number = null;
    static loanGoalId: string = "1";
    // ---- Please set these properties by calling set functions from .test. Refer to CurrentResidence.test.tsx ----

    static getLoanAppliationId() {
        return this.loanApplicationId;
    }

    static setLoanAppliationId(loanApplicationId: number) {
        this.loanApplicationId = loanApplicationId;
    }

    static getBorrowerId() {
        return this.borrowerId
    }

    static setBorrowerId(borrowerId: number) {
        this.borrowerId = borrowerId;
    }

    static getIncomeId() {
        return 32
    }

    static getMyPropertyTypeId() {
        return this.myPropertyTypeId
    }

    static setIncomeId(borrowerId: string) {

    }

    static setMyPropertyTypeId(myPropertyTypeId: number) {
        this.myPropertyTypeId = myPropertyTypeId;
    }

    static setAdditionalPropertyTypeId(propertyTypeId: number) {
        this.additionalPropertyTypeId = propertyTypeId;
    }

    static getAddtionalPropertyTypeId() {
        return this.additionalPropertyTypeId;
    }

    static setAssetId(assetId: string) {
    }

    static getAssetId() {
        return 1
    }


    static getLoanPurposeId() {
        return 1
    }

    static setLoanPurposeId(loanPurposeId: string) {

    }

    static getLoanGoalId() {
        return this.loanGoalId
    }
    static setLoanGoalId(loanGoalId: string) {
        this.loanGoalId = loanGoalId;
    }

    static setLOImageUrl(url: string) {

    }

    static getLOImageUrl() {
        return ""
    }

    static clearLoanApplicationFromStorage() {
        // UrlQueryManager.deleteQuery("loanapplicationid");
    }

    static clearLoanPurposeFromStorage() {
        // UrlQueryManager.deleteQuery("loanpurposeid");
    }

    static clearLoanGoalFromStorage() {
        // UrlQueryManager.deleteQuery("loangoalid");
    }

    static clearBorrowerFromStorage() {
        // UrlQueryManager.deleteQuery("borrowerid");
    }


    static clearIncomeFromStorage() {
        // UrlQueryManager.deleteQuery("incomeid");
    }

    static clearAssetFromStorage() {
        // UrlQueryManager.deleteQuery("assetid");
    }

    static clearLoImageUrlFromStorage() {
        // UrlQueryManager.deleteQuery("loimageurl");
    }

    static clearSessionStorage() {
        // UrlQueryManager.deleteQuery("loanapplicationid");
        // UrlQueryManager.deleteQuery("borrowerid");
        // UrlQueryManager.deleteQuery("loangoalid");
        // UrlQueryManager.deleteQuery("loanpurposeid");
    }


    //#endregion

    //#region Local DB Post Methods



    public static storeItem(name: string, data: string) {
        localStorage.setItem(name, this.encodeString(data));
    }
    //#endregion

    //#region Encode Decode
    public static encodeString(value: string) {
        // Encode the String
        //const currentDate = Date.toString();
        const string = value + '|' + ApplicationEnv.Encode_Key;
        return btoa(unescape(encodeURIComponent(string)));
    }

    public static decodeString(value?: string | null) {
        // Decode the String
        if (!value) {
            return '';
        }
        try {
            const decodedString = atob(value);
            return decodedString.split('|')[0];
        } catch {
            return null;
        }
    }

}