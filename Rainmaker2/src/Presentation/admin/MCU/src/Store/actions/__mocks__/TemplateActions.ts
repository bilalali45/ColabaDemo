import axios, { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import { debug } from "console";

const http = new Http();

const mockTemplates = [
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
            {
                "typeId": "5eec89bf6ecaea4247963f28",
                "docName": "Purchase Contract Deposit Check"
            },
            {
                "typeId": "5eec89fd6ecaea424796436a",
                "docName": "Tax Returns with Schedules (Personal - Two Years)"
            },
            {
                "typeId": "5f473aa0cca0a5d1c96798dd",
                "docName": "HOA or Condo Association Fee Statements"
            },
            {
                "typeId": "5f473879cca0a5d1c9659cc3",
                "docName": "W-2s - Last Two years"
            },
            {
                "typeId": "5eec894e6ecaea42479637b9",
                "docName": "Current P&L/Balance Sheet"
            }
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
            {
                "typeId": null,
                "docName": "this is the test max lenght for custom document na"
            },
            {
                "typeId": "5f47427ccca0a5d1c96fcaf5",
                "docName": "Power of Attorney (POA)"
            },
            {
                "typeId": "5f4741bfcca0a5d1c96efbea",
                "docName": "Homebuyer Education"
            }
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
            {
                "typeId": null,
                "docName": "ALL CAPS DOCUMENT NAME"
            },
            {
                "typeId": null,
                "docName": "Init Caps Document Name"
            }
        ]
    },
    {
        "id": "5f576d5713f0d679882c6d8f",
        "type": "MCU Template",
        "name": "ALL CAPS TEMPLATE NAME",
        "docs": [
            {
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            }
        ]
    },
    {
        "id": "5f588200603a2d64b4a279d6",
        "type": "MCU Template",
        "name": "mn,mdnvfd",
        "docs": [
            {
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5eec89e56ecaea42479641d4",
                "docName": "Tax Returns with Schedules (Business - Two Years)"
            }
        ]
    },
    {
        "id": "5f63432537de0c1490a36b0f",
        "type": "MCU Template",
        "name": "New Template 22",
        "docs": [
            {
                "typeId": "5f473faecca0a5d1c96cba16",
                "docName": "Mortgage Statement"
            },
            {
                "typeId": "5f473aa0cca0a5d1c96798dd",
                "docName": "HOA or Condo Association Fee Statements"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            }
        ]
    },
    {
        "id": "5f6343a237de0c1490a36b17",
        "type": "MCU Template",
        "name": "new temp 101",
        "docs": [
            {
                "typeId": "5f473faecca0a5d1c96cba16",
                "docName": "Mortgage Statement"
            },
            {
                "typeId": "5f473aa0cca0a5d1c96798dd",
                "docName": "HOA or Condo Association Fee Statements"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": "5f47439dcca0a5d1c971083c",
                "docName": "Government Issued Identification"
            }
        ]
    },
    {
        "id": "5f686912a16049171422d231",
        "type": "MCU Template",
        "name": "ereretet",
        "docs": [
            {
                "typeId": "5eec89bf6ecaea4247963f28",
                "docName": "Purchase Contract Deposit Check"
            },
            {
                "typeId": "5eec89fd6ecaea424796436a",
                "docName": "Tax Returns with Schedules (Personal - Two Years)"
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
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": "5f473faecca0a5d1c96cba16",
                "docName": "Mortgage Statement"
            },
            {
                "typeId": "5eec89e56ecaea42479641d4",
                "docName": "Tax Returns with Schedules (Business - Two Years)"
            }
        ]
    },
    {
        "id": "5f69d0efa16049171422d530",
        "type": "MCU Template",
        "name": "New Template 4",
        "docs": []
    },
    {
        "id": "5f69dd26a16049171422d531",
        "type": "MCU Template",
        "name": "New Template 5",
        "docs": []
    },
    {
        "id": "5f69e099a16049171422d533",
        "type": "MCU Template",
        "name": "jgjhg",
        "docs": []
    },
    {
        "id": "5f69e0aba16049171422d534",
        "type": "MCU Template",
        "name": "New Template 6",
        "docs": []
    },
    {
        "id": "5f69e0b7a16049171422d535",
        "type": "MCU Template",
        "name": "New Template 7",
        "docs": []
    },
    {
        "id": "5f6b0f326fb05c3c906557dd",
        "type": "MCU Template",
        "name": "New Template 8",
        "docs": []
    },
    {
        "id": "5f6c2a806fb05c3c9065587a",
        "type": "MCU Template",
        "name": "New Template 9",
        "docs": []
    },
    {
        "id": "5f55ff29dce29d97a075a377",
        "type": "Tenant Template",
        "name": "d",
        "docs": [
            {
                "typeId": "5eec89bf6ecaea4247963f28",
                "docName": "Purchase Contract Deposit Check"
            },
            {
                "typeId": "5eec89fd6ecaea424796436a",
                "docName": "Tax Returns with Schedules (Personal - Two Years)"
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
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": "5f473faecca0a5d1c96cba16",
                "docName": "Mortgage Statement"
            },
            {
                "typeId": "5eec89e56ecaea42479641d4",
                "docName": "Tax Returns with Schedules (Business - Two Years)"
            }
        ]
    },
    {
        "id": "5f48e2466b56461860b6488f",
        "type": "System Template",
        "name": "Asghar's Template",
        "docs": [
            {
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5f473c08cca0a5d1c968e844",
                "docName": "Trust - Family Trust"
            },
            {
                "typeId": "5f4741bfcca0a5d1c96efbea",
                "docName": "Homebuyer Education"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": "5f473b2dcca0a5d1c9681ab2",
                "docName": "Rental Agreement"
            },
            {
                "typeId": "5f47457acca0a5d1c9731ae5",
                "docName": "Homeowner's Insurance"
            },
            {
                "typeId": "5f47441ecca0a5d1c9719720",
                "docName": "Work Visa - Work Permit"
            },
            {
                "typeId": null,
                "docName": "lenght for custom document nathis is the test maxs"
            },
            {
                "typeId": "5eec892f6ecaea42479635c3",
                "docName": "Co-Depositor Letter"
            },
            {
                "typeId": "5eec894e6ecaea42479637b9",
                "docName": "Current P&L/Balance Sheet"
            }
        ]
    },
    {
        "id": "5f4f526d8e8a8f440cbe0b30",
        "type": "System Template",
        "name": "sasas@@@@@",
        "docs": [
            {
                "typeId": "5eb257a3e519051af2eeb624",
                "docName": "Bank Statements - Two Months"
            },
            {
                "typeId": "5eec892f6ecaea42479635c3",
                "docName": "Co-Depositor Letter"
            },
            {
                "typeId": "5eec89a26ecaea4247963d25",
                "docName": "Retirement Account Statements"
            },
            {
                "typeId": null,
                "docName": "fd"
            }
        ]
    }
];

let fetchTemplateDocumentCancelToken = axios.CancelToken.source();

export class TemplateActions {
    static async fetchTemplates() {
       return Promise.resolve([])
    }

    static async fetchCategoryDocuments() {

    }

    static async fetchTemplateDocuments(id: string) {
       
    }

    static async fetchEmailTemplate() {
       
    }

    static async insertTemplate(name: string) {
        console.log('in here you know where ================== ==================', name);
    }

    static async renameTemplate(templateId: string, name: string) {
        
    }

    static async deleteTemplate(templateId: string) {
        
    }

    static async addDocument(templateId: string, docTypeOrName: string, type: string) {
      
    }

    static async deleteTemplateDocument(templateId: string, documentId: string) {
        
    }

    static async isDocumentDraft(loanApplicationId: string) {
        
    }
}
