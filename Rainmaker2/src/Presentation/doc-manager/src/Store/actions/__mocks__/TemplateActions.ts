
const categoryDocuments = [{
    "catId": "5ebabbfb3845be1cf1edce50",
    "catName": "Assets",
    "documents": [{
        "docTypeId": "5eb257a3e519051af2eeb624",
        "docType": "Bank Statements - Two Months",
        "docMessage": "Please provide 2 most recent months Bank Statement with sufficient funds for cash to close.",
        "isCommonlyUsed": true
    }, {
        "docTypeId": "5ebc18cba5d847268075ad4f",
        "docType": "Brokerage Statements - Two Months",
        "docMessage": "Brokerage account statements - most recent two month or most recent quarter",
        "isCommonlyUsed": false
    }, {
        "docTypeId": "5eec892f6ecaea42479635c3",
        "docType": "Co-Depositor Letter",
        "docMessage": "Co-Depositor Acknowledgement Letter signed by the account holder not on the loan",
        "isCommonlyUsed": false
    }, {
        "docTypeId": "5eec894e6ecaea42479637b9",
        "docType": "Current P&L/Balance Sheet",
        "docMessage": "Most recent signed Business Profit & Loss and Balance Sheet statement",
        "isCommonlyUsed": false
    }, {
        "docTypeId": "5eec89a26ecaea4247963d25",
        "docType": "Retirement Account Statements",
        "docMessage": "Most recent two month or quarterly IRA, 401K or other retirement account statement",
        "isCommonlyUsed": true
    }, {
        "docTypeId": "5eec89b06ecaea4247963e36",
        "docType": "Gift Letter/Source of Funds",
        "docMessage": "Gift letter signed by the donor and evidence of transfer of funds",
        "isCommonlyUsed": false
    }]
}]

export class TemplateActions {

    static async fetchCategoryDocuments() {
        return categoryDocuments;
    }

    static async fetchTemplateDocuments(id: string) {
    }

    static async addDocument(
        templateId: string,
        docTypeOrName: string,
        type: string
      ) {
      }

      static async deleteTemplateDocument(templateId: string, documentId: string) {
      }
}