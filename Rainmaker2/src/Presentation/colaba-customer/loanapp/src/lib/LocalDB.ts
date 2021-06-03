import { ApplicationEnv } from '../lib/appEnv';
import { UrlQueryManager } from '../Utilities/Navigation/UrlQueryManager';

export class LocalDB {

    static getLoanAppliationId() {
        return (
            UrlQueryManager.getQuery("loanapplicationid")
        );
    }

    static setLoanAppliationId(loanApplicationId: string | undefined) {
        UrlQueryManager.addQuery(
            'loanapplicationid',
            loanApplicationId
        );
    }

    static getBorrowerId() {
        return (
            UrlQueryManager.getQuery('borrowerid')
        )
    }

    static setBorrowerId(borrowerId: string) {
        UrlQueryManager.addQuery(
            'borrowerid',
            borrowerId
        );
    }

    static getIncomeId() {
        return (
            UrlQueryManager.getQuery('incomeid')
        );
    }

    static getMyPropertyTypeId() {
        return (
            UrlQueryManager.getQuery('mypropertytypeid')
        );
    }
    static getAddtionalPropertyTypeId() {
        return (
            UrlQueryManager.getQuery('additionalPropertyTypeId')
        );
    }

    static setIncomeId(borrowerId: string | null) {
        UrlQueryManager.addQuery(
            'incomeid',
            borrowerId
        );
    }

    static setMyPropertyTypeId(myPropertyTypeId: number) {
        UrlQueryManager.addQuery(
            'mypropertytypeid',
            myPropertyTypeId
        );
    }

    static setAdditionalPropertyTypeId(propertyTypeId: number | null) {
        UrlQueryManager.addQuery(
            'additionalPropertyTypeId',
            propertyTypeId
        );
    }

    static setAssetId(assetId: string) {
        UrlQueryManager.addQuery(
            'assetid',
            assetId
        );
    }

    static getAssetId() {
        return (
            UrlQueryManager.getQuery('assetid')
        );
    }


    static getLoanPurposeId() {
        return (
            UrlQueryManager.getQuery('loanpurposeid')
        );
    }

    static setLoanPurposeId(loanPurposeId: string) {
        UrlQueryManager.addQuery(
            'loanpurposeid',
            loanPurposeId
        );
    }

    static getLoanGoalId() {
        return (
            UrlQueryManager.getQuery('loangoalid')
        );
    }
    static setLoanGoalId(loanGoalId: string) {
        UrlQueryManager.addQuery(
            'loangoalid',
            loanGoalId
        );
    }

    static setLOImageUrl(url: string) {
        UrlQueryManager.addQuery(
            'loimageurl',
            url
        );
    }

    static getLOImageUrl() {
        return (
            UrlQueryManager.getQuery('loimageurl')
        );
    }



    static clearLoanApplicationFromStorage() {
        UrlQueryManager.deleteQuery("loanapplicationid");
    }

    static clearLoanPurposeFromStorage() {
        UrlQueryManager.deleteQuery("loanpurposeid");
    }

    static clearLoanGoalFromStorage() {
        UrlQueryManager.deleteQuery("loangoalid");
    }

    static clearBorrowerFromStorage() {
        UrlQueryManager.deleteQuery("borrowerid");
    }

    static clearIncomeFromStorage() {
        UrlQueryManager.deleteQuery("incomeid");
    }

    static clearAssetFromStorage() {
        UrlQueryManager.deleteQuery("assetid");
    }

    static clearLoImageUrlFromStorage() {
        UrlQueryManager.deleteQuery("loimageurl");
    }


    static clearSessionStorage() {
        UrlQueryManager.deleteAllQueries();
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

    //#endregion
}
