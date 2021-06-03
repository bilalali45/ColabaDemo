import { APIResponse } from "../../../Entities/Models/APIResponse";
import { AssetPayloadType, FinancialSatementPayloadType, RetirementPayloadType } from "../../../Entities/Models/types";

const AssetCategoryMock = [
    {
        "id": 1,
        "name": "Bank Account",
        "displayName": "Bank Account",
        "tenantAlternateName": null
    },
    {
        "id": 2,
        "name": "Stocks, Bonds, Or Other Financial Assets",
        "displayName": "Stocks, Bonds, Or Other Financial Assets",
        "tenantAlternateName": null
    },
    {
        "id": 3,
        "name": "Retirement Account",
        "displayName": "Retirement Account",
        "tenantAlternateName": null
    },
    {
        "id": 4,
        "name": "Gift Funds",
        "displayName": "Gift Funds",
        "tenantAlternateName": null
    },
    {
        "id": 6,
        "name": "Proceeds from Transactions",
        "displayName": "Proceeds from Transactions",
        "tenantAlternateName": null
    },
    {
        "id": 7,
        "name": "Other",
        "displayName": "Other",
        "tenantAlternateName": null
    }
]

const AssetTypeMock = [
    {
        "id": 1,
        "assetCategoryId": 1,
        "name": "Checking Account",
        "displayName": "Checking Account",
        "tenantAlternateName": null,
        "fieldsInfo": null
    },
    {
        "id": 2,
        "assetCategoryId": 1,
        "name": "Savings Accoount",
        "displayName": "Savings Accoount",
        "tenantAlternateName": null,
        "fieldsInfo": null
    }
]

const AssetDetailMock = {
    "borrowerId": 6617,
    "borrowerName": "Jehangir Babul",
    "borrowerAssets": [
        {
            "borrowerAssetId": 1170,
            "assetType": "Checking Account",
            "assetTypeId": 1,
            "assetCategory": "Bank Account",
            "assetCategoryId": 1,
            "institutionName": "US Bank",
            "accountNumber": "123456789",
            "assetValue": 800000
        }
    ]
}

const RetirementMock = {
    "borrowerAssetId": 1170,
    "institutionName": "US Bank",
    "accountNumber": "123456789",
    "value": 800000

}

const FinancialAssetDetailMock = {
    "id": 1225,
    "assetTypeId": 6,
    "institutionName": "Abc Bank2",
    "accountNumber": "5555555",
    "balance": 2001,
    "loanApplicationId": 3300,
    "state": null
}

let mockAssetsHomeScreenData = {
    borrowers: [
        {
            "borrowerId": 14897,
            "borrowerName": "Quemby Kelly",
            "ownTypeId": 1,
            "ownTypeName": "Primary Contact",
            "ownTypeDisplayName": "Primary Contact",
            "borrowerAssets": [
                {
                    "assetName": "Eum magna dolorem at",
                    "assetValue": 123,
                    "assetId": 2342,
                    "assetTypeID": 1,
                    "assetCategoryId":1,
                    "assetCategoryName": "Bank Account",
                    "assetTypeName": "Savings Accoount"
                }
            ],
            "assetsValue": 123
        },
        {
            "borrowerId": 14899,
            "borrowerName": "Brianna Chan",
            "ownTypeId": 2,
            "ownTypeName": "Secondary Contact",
            "ownTypeDisplayName": "Secondary Contact",
            "borrowerAssets": [],
            "assetsValue": 0
        }
    ],
    totalAssetsValue: 0
}

const mockTransactionProceedsData = [{ "id": 12, "assetCategoryId": 6, "name": "Proceeds from a Loan", "displayName": "Proceeds from a Loan", "tenantAlternateName": null, "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}" }, { "id": 13, "assetCategoryId": 6, "name": "Proceeds from selling non-real estate assets", "displayName": "Proceeds from selling non-real estate assets", "tenantAlternateName": null, "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}" }, { "id": 14, "assetCategoryId": 6, "name": "Proceeds from selling real estate", "displayName": "Proceeds from selling real estate", "tenantAlternateName": null, "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}" }];

const FinancialAssetsMock = [
    {
        "id": 3,
        "name": "MutualFunds"
    },
    {
        "id": 4,
        "name": "Bonds"
    },
    {
        "id": 5,
        "name": "Stocks"
    },
    {
        "id": 6,
        "name": "Stock Options"
    },
    {
        "id": 7,
        "name": "MoneyMarket"
    },
    {
        "id": 8,
        "name": "Certificate of Deposit"
    }
]

const EarnestMoneyMock = {
    "loanApplicationId": 14909,
    "deposit": 15000,
    "state": null,
    "isEarnestMoneyProvided": true
}


export default class AssetsActions {

    static async GetMyMoneyHomeScreen(loanApplicationId: number) {        
        return new APIResponse(200, mockAssetsHomeScreenData);
    }

    static async GetAssetTypesByCategory(categoryId: number) {
        if (categoryId == 6)
            return new APIResponse(200, mockTransactionProceedsData);

        else
            return new APIResponse(200, AssetTypeMock);
    }

    static async GetEarnestMoneyDeposit(loanApplicationId: number) {
        return new APIResponse(200, EarnestMoneyMock)
    }

    static async UpdateEarnestMoneyDeposit(loanApplicationId: number, deposit: number, State: string, isEarnestMoneyProvided: boolean) {
        return new APIResponse(200, {});
    }

    static async GetAllAssetCategories() {
        return new APIResponse(200, AssetCategoryMock);
    }


    static async GetCollateralAssetTypes() {
        return new APIResponse(200, [{ "id": 1, "name": "House", "displayName": "House" }, { "id": 2, "name": "Automobile", "displayName": "Automobile" }, { "id": 3, "name": "Financial Account", "displayName": "Financial Account" }, { "id": 4, "name": "Other", "displayName": "Other" }]);
    }

    static async GetBorrowerAssetDetail(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {

        return new APIResponse(200, AssetDetailMock);

    }

    static async AddOrUpdateBorrowerAsset(assetInfo: AssetPayloadType) {
        return new APIResponse(200, {});
    }
    static async GetRetirementAccount(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {
        return new APIResponse(200, RetirementMock);
    }

    static async UpdateRetirementAccount(assetInfo: RetirementPayloadType) {
        return new APIResponse(200, {});
    }

    static async GetAllFinancialAsset() {
        return new APIResponse(200, FinancialAssetsMock);
    }
    static async GetFinancialAsset(loanApplicationId: number, borrowerId: number, borrowerAssetId: number) {
        return new APIResponse(200, FinancialAssetDetailMock);
    }

    static async AddOrUpdateFinancialAsset(assetInfo: FinancialSatementPayloadType) {
        return new APIResponse(200, {});
    }
}