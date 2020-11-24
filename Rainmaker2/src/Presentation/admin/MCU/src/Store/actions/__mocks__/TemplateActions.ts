import axios, { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import { debug } from "console";

//const http = new Http();

const mockTemplates: any = [
  {
    "id": "5f48eba06b56461860b648f8",
    "type": "MCU Template",
    "name": "New Template 3",
    "docs": [
      {
        "typeId": "5eec89bf6ecaea4247963f28",
        "docName": "Purchase Contract Deposit Check"
      },
      {
        "typeId": "5f473faecca0a5d1c96cba16",
        "docName": "Mortgage Statement"
      },
    ]
  },
  {
    "id": "5f55fc62dce29d97a075a364",
    "type": "MCU Template",
    "name": "New Template 2",
    "docs": []
  },
  {
    "id": "5f575ad613f0d679882c6c83",
    "type": "MCU Template",
    "name": "Test Max Character lenght for the remplate name ne",
    "docs": [
      {
        "typeId": null,
        "docName": "this is the test max lenght for custom document na"
      },
    ]
  },
  {
    "id": "5f576d1913f0d679882c6d86",
    "type": "MCU Template",
    "name": "all small template name",
    "docs": [
      {
        "typeId": "5eb257a3e519051af2eeb624",
        "docName": "Bank Statements - Two Months"
      },
      {
        "typeId": null,
        "docName": "all small document name"
      },
    ]
  },
  {
    "id": "5f6de5d0960da01efb77d01f",
    "name": "Standard",
    "type": "Tenant Template",
    "docs": [
      {
        "docName": "Bank Statements - Two Months",
        "typeId": "5eb257a3e519051af2eeb624"
      }
    ]
  },
  {
    "id": "5f6de5df960da01efb7804e2",
    "name": "Purpose: Purchase",
    "type": "Tenant Template",
    "docs": [
      {
        "docName": "Purchase Contract Deposit Check",
        "typeId": "5eec89bf6ecaea4247963f28"
      },
      {
        "docName": "Purchase Contract",
        "typeId": "5f4744d0cca0a5d1c9725ca9"
      },
    ]
  }
];

const mockCategoryDocs = [
  {
    "catId": "5ebabbfb3845be1cf1edce50",
    "catName": "Assets",
    "documents": [
      {
        "docTypeId": "5eb257a3e519051af2eeb624",
        "docType": "Bank Statements - Two Months",
        "docMessage": "Checking & Savings account statements - most recent two months or most recent quarter",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5ebc18cba5d847268075ad4f",
        "docType": "Brokerage Statements - Two Months",
        "docMessage": "Brokerage account statements - most recent two month or most recent quarter",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5eec892f6ecaea42479635c3",
        "docType": "Co-Depositor Letter",
        "docMessage": "Co-Depositor Acknowledgement Letter signed by the account holder not on the loan",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5eec894e6ecaea42479637b9",
        "docType": "Current P&L/Balance Sheet",
        "docMessage": "Most recent signed Business Profit & Loss and Balance Sheet statement",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5eec89a26ecaea4247963d25",
        "docType": "Retirement Account Statements",
        "docMessage": "Most recent two month or quarterly IRA, 401K or other retirement account statement",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5eec89b06ecaea4247963e36",
        "docType": "Gift Letter/Source of Funds",
        "docMessage": "Gift letter signed by the donor and evidence of transfer of funds",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5eec89bf6ecaea4247963f28",
        "docType": "Purchase Contract Deposit Check",
        "docMessage": "Copy of cancelled check for the deposit on the purchase contract",
        "isCommonlyUsed": true
      }
    ]
  },
  {
    "catId": "5ebabbfb3845be1cf1edce51",
    "catName": "Other",
    "documents": [
      {
        "docTypeId": "5f473c08cca0a5d1c968e844",
        "docType": "Trust - Family Trust",
        "docMessage": "Revocable or Living family trust with all amendments",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f473f5ecca0a5d1c96c6346",
        "docType": "Divorce Decree",
        "docMessage": "Final divorce decree signed by the judge and filed with the court house",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f473faecca0a5d1c96cba16",
        "docType": "Mortgage Statement",
        "docMessage": "Most recent monthly mortgage statement for all properties owned",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f4741bfcca0a5d1c96efbea",
        "docType": "Homebuyer Education",
        "docMessage": "Evidence of completion of homebuyer's counseling program",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f4741f7cca0a5d1c96f3ae7",
        "docType": "Credit Explanation",
        "docMessage": "Credit report and other items - letter of explanation",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f474242cca0a5d1c96f8b61",
        "docType": "Letter of Explanation",
        "docMessage": "One or more letter of explanation",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f47427ccca0a5d1c96fcaf5",
        "docType": "Power of Attorney (POA)",
        "docMessage": "Signed and notarized Power of Attorney",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f4742adcca0a5d1c9700091",
        "docType": "Purpose of Cash Out",
        "docMessage": "Signed letter explaining for what purpose cash out funds will be used for",
        "isCommonlyUsed": false
      }
    ]
  },
  {
    "catId": "5f473440cca0a5d1c961b617",
    "catName": "Income",
    "documents": [
      {
        "docTypeId": "5eec89cd6ecaea4247964038",
        "docType": "Income - Miscellaneous",
        "docMessage": "Other income documentation",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5eec89e56ecaea42479641d4",
        "docType": "Tax Returns with Schedules (Business - Two Years)",
        "docMessage": "Most recent two years business tax returns filed with the IRS",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5eec89fd6ecaea424796436a",
        "docType": "Tax Returns with Schedules (Personal - Two Years)",
        "docMessage": "Most recent two years personal tax Returns filed with the IRS",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f473879cca0a5d1c9659cc3",
        "docType": "W-2s - Last Two years",
        "docMessage": "Most recent and prior year W-2 forms from all applicants in the workforce",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f4738dccca0a5d1c965f813",
        "docType": "Rental Agreement - Real Estate Owned",
        "docMessage": "Lease or rental agreement for the real estate already owned",
        "isCommonlyUsed": false
      }
    ]
  },
  {
    "catId": "5f473471cca0a5d1c961e383",
    "catName": "Liabilities",
    "documents": [
      {
        "docTypeId": "5f473aa0cca0a5d1c96798dd",
        "docType": "HOA or Condo Association Fee Statements",
        "docMessage": "Condo HOA fee statement depicting monthly/quarterly/annual dues and payments",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f473adecca0a5d1c967d21f",
        "docType": "Liabilities - Miscellaneous",
        "docMessage": "Other payments and liabilities including contingent liabilities",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f473b2dcca0a5d1c9681ab2",
        "docType": "Rental Agreement",
        "docMessage": "Lease or rental agreement",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f473b70cca0a5d1c9685829",
        "docType": "Bankruptcy Papers",
        "docMessage": "Bankruptcy documents - Chapter 7, 11 or 13 documents - All pages",
        "isCommonlyUsed": false
      }
    ]
  },
  {
    "catId": "5f47349dcca0a5d1c9620c2d",
    "catName": "Personal",
    "documents": [
      {
        "docTypeId": "5f47439dcca0a5d1c971083c",
        "docType": "Government Issued Identification",
        "docMessage": "State or military issued driver's license or identification card including spouse's/co-borrower's (if applicable)",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f4743dacca0a5d1c9714be4",
        "docType": "Permanent Resident Card",
        "docMessage": "Permanent Resident Card - Green Card (Front and Back)",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f47441ecca0a5d1c9719720",
        "docType": "Work Visa - Work Permit",
        "docMessage": "Work permit or work visa. EAD, H1, or L1 VISA valid for at least 90 days",
        "isCommonlyUsed": false
      }
    ]
  },
  {
    "catId": "5f4734b1cca0a5d1c9621ea3",
    "catName": "Property",
    "documents": [
      {
        "docTypeId": "5f4744d0cca0a5d1c9725ca9",
        "docType": "Purchase Contract",
        "docMessage": "Copy of signed purchase contract by all parties (Seller and Buyer)",
        "isCommonlyUsed": true
      },
      {
        "docTypeId": "5f474508cca0a5d1c9729a87",
        "docType": "Condo HO6 Interior Insurance",
        "docMessage": "Condo HO6 insurance policy providing interior coverage",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f474545cca0a5d1c972defd",
        "docType": "Flood Insurance Policy",
        "docMessage": "Existing or new flood Insurance policy/binder",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f47457acca0a5d1c9731ae5",
        "docType": "Homeowner's Insurance",
        "docMessage": "Existing or new homeowners insurance policy/binder",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f4745bacca0a5d1c97361bc",
        "docType": "Survey Affidavit",
        "docMessage": "Affidavit that structures have not been changed on the property since the previous survey",
        "isCommonlyUsed": false
      },
      {
        "docTypeId": "5f4745fccca0a5d1c973abf9",
        "docType": "Property Survey",
        "docMessage": "Clear copy of the property survey",
        "isCommonlyUsed": true
      }
    ]
  }
]

let mockTemplateDocs: any = [];

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();
export let mockIsDocumentDraft: any = { requestId: null }

export class TemplateActions {


  static async fetchTemplates() {
    return Promise.resolve(mockTemplates)
  }

  static async fetchCategoryDocuments() {
    return Promise.resolve(mockCategoryDocs);
  }

  static async fetchTemplateDocuments(id: string) {
    return Promise.resolve(mockTemplates);
  }

  static async fetchEmailTemplate() {

    const emailtemplate = "Hi {user},\nWe're busy reviewing your file, but to continue the process we need the following documents.\n{documents}\nPlease upload the above documents in two days to avoid any processing delays.\nThank you,"

    return Promise.resolve(emailtemplate)
  }

  static async insertTemplate(name: string) {
    mockTemplates.push({
      "id": "tytyt576d1913f0d679882c6d86",
      "type": "MCU Template",
      "name": name,
      "docs": []
    });
    return Promise.resolve(true);
  }

  static async renameTemplate(templateId: string, name: string) {

  }

  static async deleteTemplate(templateId: string) {

  }

  static async addDocument(templateId: string, docTypeOrName: string, type: string) {
    let doc = null; 0
    for (const catDoc of mockCategoryDocs) {
      doc = catDoc.documents.find(d => {
        if (d.docTypeId === docTypeOrName) {
          return d;
        }
      });
      if (doc) {
        break;
      }
    }

    mockTemplateDocs.push({ ...doc, docName: doc?.docType });
    return Promise.resolve(true);
  }

  static async deleteTemplateDocument(templateId: string, documentId: string) {

  }

  static async isDocumentDraft(loanApplicationId: string) {
    console.log('this.mockIsDocumentDraft ::::::::::::::::::::::::::::::::::::', mockIsDocumentDraft)
    return Promise.resolve(mockIsDocumentDraft);
  }
}
