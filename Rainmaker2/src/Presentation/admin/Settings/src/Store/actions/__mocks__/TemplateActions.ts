import { Template } from "../../../Entities/Models/Template";
import { TemplateDocument } from "../../../Entities/Models/TemplateDocument";
import { CategoryDocument } from "../../../Entities/Models/CategoryDocument";
import { Document } from "../../../Entities/Models/Document";

let templateMock = [
    {
        "id": "5f85a58849fa941f146f94ab",
        "type": "MCU Template",
        "name": "Check List Templates",
        "docs": [
            {
                "typeId": "5f474242cca0a5d1c96f8b61",
                "docName": "Letter of Explanation"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            }
        ]
    },
    {
        "id": "5f8ecc0613122424cc8dfc41",
        "type": "MCU Template",
        "name": "New Template 1",
        "docs": [
            {
                "typeId": "5eec89fd6ecaea424796436a",
                "docName": "Tax Returns with Schedules (Personal - Two Years)"
            }
        ]
    },
    {
        "id": "5f8ed87113122424cc8dfd3a",
        "type": "MCU Template",
        "name": "ABCD",
        "docs": []
    },
    {
        "id": "5f8fd05313122424cc8dfeca",
        "type": "MCU Template",
        "name": "New Template 2",
        "docs": []
    },
    {
        "id": "5f8fd1ef13122424cc8dfed5",
        "type": "MCU Template",
        "name": "New Template 3",
        "docs": []
    },
    {
        "id": "5f8fd20113122424cc8dfed6",
        "type": "MCU Template",
        "name": "New Template 4",
        "docs": []
    },
    {
        "id": "5f6de5d0960da01efb77d01f",
        "type": "Tenant Template",
        "name": "Standard",
        "docs": [
            {
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": "5eec89fd6ecaea424796436a",
                "docName": "Tax Returns with Schedules (Personal - Two Years)"
            },
            {
                "typeId": "5f6ddac1960da01efb53ddd6",
                "docName": "Paystubs - Most Recent"
            },
            {
                "typeId": "5f473879cca0a5d1c9659cc3",
                "docName": "W-2s - Last Two years"
            },
            {
                "typeId": "5f473aa0cca0a5d1c96798dd",
                "docName": "HOA or Condo Association Fee Statements"
            },
            {
                "typeId": "5f47439dcca0a5d1c971083c",
                "docName": "Government Issued Identification"
            },
            {
                "typeId": "5f47457acca0a5d1c9731ae5",
                "docName": "Homeowner's Insurance Policy"
            },
            {
                "typeId": "5f6ddb0e960da01efb54bd6d",
                "docName": "Property Tax Statement"
            },
            {
                "typeId": "5f6ddb52960da01efb5580b0",
                "docName": "Homeowner's Association Certificate"
            }
        ]
    },
    {
        "id": "5f6de5df960da01efb7804e2",
        "type": "Tenant Template",
        "name": "Purpose: Purchase",
        "docs": [
            {
                "typeId": "5eec89bf6ecaea4247963f28",
                "docName": "Purchase Contract Deposit Check"
            },
            {
                "typeId": "5f4744d0cca0a5d1c9725ca9",
                "docName": "Purchase Contract"
            }
        ]
    },
    {
        "id": "5f6de5ef960da01efb783a61",
        "type": "Tenant Template",
        "name": "Purpose: Refi",
        "docs": [
            {
                "typeId": "5f473faecca0a5d1c96cba16",
                "docName": "Mortgage Statement"
            }
        ]
    },
    {
        "id": "5f6de5fe960da01efb786ea8",
        "type": "Tenant Template",
        "name": "Purpose: Cashout",
        "docs": [
            {
                "typeId": "5f4742adcca0a5d1c9700091",
                "docName": "Purpose of Cash Out"
            }
        ]
    },
    {
        "id": "5f6de60b960da01efb789909",
        "type": "Tenant Template",
        "name": "Immigration",
        "docs": [
            {
                "typeId": "5f4743dacca0a5d1c9714be4",
                "docName": "Permanent Resident Card"
            },
            {
                "typeId": "5f47441ecca0a5d1c9719720",
                "docName": "Work Visa - Work Permit"
            }
        ]
    },
    {
        "id": "5f6de618960da01efb78c75e",
        "type": "Tenant Template",
        "name": "Condo",
        "docs": [
            {
                "typeId": "5f474508cca0a5d1c9729a87",
                "docName": "Condo HO6 Interior Insurance"
            }
        ]
    },
    {
        "id": "5f6de624960da01efb78f0d3",
        "type": "Tenant Template",
        "name": "Rental",
        "docs": [
            {
                "typeId": "5f4738dccca0a5d1c965f813",
                "docName": "Rental Agreement - Real Estate Owned"
            },
            {
                "typeId": "5f473b2dcca0a5d1c9681ab2",
                "docName": "Rental Agreement"
            },
            {
                "typeId": "5f47457acca0a5d1c9731ae5",
                "docName": "Homeowner's Insurance Policy"
            }
        ]
    },
    {
        "id": "5f6de632960da01efb792214",
        "type": "Tenant Template",
        "name": "Self Employed",
        "docs": [
            {
                "typeId": "5eec894e6ecaea42479637b9",
                "docName": "Current P&L/Balance Sheet"
            },
            {
                "typeId": "5eec89e56ecaea42479641d4",
                "docName": "Tax Returns with Schedules (Business - Two Years)"
            }
        ]
    }
]

let categoryDocMock = [
    {
        "catId": "5ebabbfb3845be1cf1edce50",
        "catName": "Assets",
        "documents": [
            {
                "docTypeId": "5eb257a3e519051af2eeb624",
                "docType": "Bank Statements - Two Months",
                "docMessage": "Please provide 2 most recent months Bank Statement with sufficient funds for cash to close.",
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
                "docMessage": "Please provide a copy of Earnest Money Deposit Check or Proof of Wire Transfer.",
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
                "docMessage": "Please provide the most recent Mortgage Statement for the subject property/all the real estate owned.",
                "isCommonlyUsed": false
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
                "docMessage": "Please provide most recent 2 years Tax Returns Form 1040 with complete schedules",
                "isCommonlyUsed": true
            },
            {
                "docTypeId": "5f473879cca0a5d1c9659cc3",
                "docType": "W-2s - Last Two years",
                "docMessage": "Please provide most recent 2 years W-2's from current & prior employer (if applicable)",
                "isCommonlyUsed": true
            },
            {
                "docTypeId": "5f4738dccca0a5d1c965f813",
                "docType": "Rental Agreement - Real Estate Owned",
                "docMessage": "Please provide the current lease agreement for the investment property/properties.",
                "isCommonlyUsed": false
            },
            {
                "docTypeId": "5f6ddac1960da01efb53ddd6",
                "docType": "Paystubs - Most Recent",
                "docMessage": "Please provide most recent paystubs covering 30 days period.",
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
                "docMessage": "Please provide HOA dues statement/transaction history or a letter from the HOA association confirming dues and the frequency of the payment (annual, quarterly or monthly).",
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
                "docMessage": "Currently valid Lease or rental agreement",
                "isCommonlyUsed": false
            },
            {
                "docTypeId": "5f473b70cca0a5d1c9685829",
                "docType": "Bankruptcy Papers",
                "docMessage": "Bankruptcy documents - Chapter 7, 11 or 13 documents - All pages",
                "isCommonlyUsed": false
            },
            {
                "docTypeId": "5f6ddb0e960da01efb54bd6d",
                "docType": "Property Tax Statement",
                "docMessage": "Please provide recent property tax statement for the Subject property./all the real estate owned",
                "isCommonlyUsed": true
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
                "docMessage": "Please provide the copy of front and back side of your Green Card.",
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
                "docType": "Homeowner's Insurance Policy",
                "docMessage": "Please provide Updated Homeowners Insurance policy for the subject property/all the real estate owned",
                "isCommonlyUsed": false
            },
            {
                "docTypeId": "5f4745bacca0a5d1c97361bc",
                "docType": "Survey Affidavit",
                "docMessage": "Affidavit that structures have not been changed on the property since the previous survey.",
                "isCommonlyUsed": false
            },
            {
                "docTypeId": "5f4745fccca0a5d1c973abf9",
                "docType": "Property Survey",
                "docMessage": "Clear copy of the property survey",
                "isCommonlyUsed": true
            },
            {
                "docTypeId": "5f6ddb52960da01efb5580b0",
                "docType": "Homeowner's Association Certificate",
                "docMessage": "HOA certificate depicting annual HOA dues and current account status",
                "isCommonlyUsed": false
            }
        ]
    }
]

let docs = [
    {
        "docId": "5f85c8a649fa941f146f94e8",
        "docName": "Letter of Explanation",
        "typeId": "5f474242cca0a5d1c96f8b61"
    },
    {
        "docId": "5f86c8e149fa941f146f95e4",
        "docName": "Retirement Account Statements",
        "typeId": "5eec89a26ecaea4247963d25"
    },
    {
        "docId": "5f86d31b49fa941f146f960c",
        "docName": "Rental Agreement",
        "typeId": "5f473b2dcca0a5d1c9681ab2"
    },
    {
        "docId": "5f86d32c49fa941f146f960d",
        "docName": "W-2s - Last Two years",
        "typeId": "5f473879cca0a5d1c9659cc3"
    },
    {
        "docId": "5f87fc8149fa941f146f9644",
        "docName": "HOA or Condo Association Fee Statements",
        "typeId": "5f473aa0cca0a5d1c96798dd"
    },
    {
        "docId": "5f87fca649fa941f146f9645",
        "docName": "Bank Statements - Two Months",
        "typeId": "5eb257a3e519051af2eeb624"
    },
    {
        "docId": "5f880a4049fa941f146f966d",
        "docName": "Current P&L/Balance Sheet",
        "typeId": "5eec894e6ecaea42479637b9"
    },
    {
        "docId": "5f883d3749fa941f146f9716",
        "docName": "Work Visa - Work Permit",
        "typeId": "5f47441ecca0a5d1c9719720"
    },
    {
        "docId": "5f883d3b49fa941f146f9717",
        "docName": "Permanent Resident Card",
        "typeId": "5f4743dacca0a5d1c9714be4"
    },
    {
        "docId": "5f8ed6fd13122424cc8dfd10",
        "docName": "Brokerage Statements - Two Months",
        "typeId": "5ebc18cba5d847268075ad4f"
    }
]

let mockTemplateDocs: any = [];

export class TemplateActions {
    
    static async fetchTemplates() {      
        try {
            let mappedData = templateMock.map((data: any) => {
              return new Template(data.id, data.type, data.name, data.docs)
            });
         return Promise.resolve(mappedData);
        } catch (error) {
          console.log(error);
        }
      }

      static async fetchCategoryDocuments() {     
            let mappedData = categoryDocMock.map((item: CategoryDocument) => {
                 return new CategoryDocument(item.catId, item.catName, 
                  item.documents.map((i: Document) => {
                      return new Document(i.docTypeId, i.docType, i.docMessage)
                  })  )
            });
                                
            return Promise.resolve(mappedData);
        
      }
    
      static async fetchTemplateDocuments(id: string) {           
        try {
             let mappedData = docs.map((data: any) => {
               return new TemplateDocument(data.docId, data.docName,data.typeId);
             });
            return Promise.resolve(mappedData);
        } catch (error) {
          console.log(error);
        }
      }

      static async insertTemplate(name: string, isMcuTemplate: boolean) {
        templateMock.push({
            "id": "tytyt576d1913f0d679882c6d86",
            "type": "MCU Template",
            "name": name,
            "docs": []
        });
        return Promise.resolve(name);
      }

      static async addDocument(templateId: string, docTypeOrName: string, type: string) {
        let doc = null;0
        for (const catDoc of categoryDocMock) {
            doc = catDoc.documents.find(d => {
                if(d.docTypeId === docTypeOrName) {
                    return d;
                }
            });
            if (doc) {
                break;
            }
        }

        mockTemplateDocs.push({...doc, docName: doc?.docType});
        return Promise.resolve(true);
    }

}