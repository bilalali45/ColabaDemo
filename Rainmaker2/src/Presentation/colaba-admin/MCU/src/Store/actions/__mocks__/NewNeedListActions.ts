
import { Http } from 'rainsoft-js';
import { debug } from 'console';
import { AxiosResponse } from 'axios';
import { TemplateDocument } from '../../../Entities/Models/TemplateDocument';
import { mockIsDocumentDraft, TemplateActions } from './TemplateActions';

export const mockNeedList = [
  {
    "id": "5f7ebe991cfe5b44d8338665",
    "name": "New Template",
    "docs": [
      {
        "typeId": "5f473b70cca0a5d1c9685829",
        "docName": "Bankruptcy Papers",
        "docMessage": "Bankruptcy documents - Chapter 7, 11 or 13 documents - All pages"
      },
      {
        "typeId": "5f6ddb0e960da01efb54bd6d",
        "docName": "Property Tax Statement",
        "docMessage": "Please provide recent property tax statement for the Subject property./all the real estate owned"
      },
      {
        "typeId": "5eec89e56ecaea42479641d4",
        "docName": "Tax Returns with Schedules (Business - Two Years)", 
        "docMessage": "Most recent two years business tax returns filed with the IRS"
      },
      {
        "typeId": "5f473aa0cca0a5d1c96798dd",
        "docName": "HOA or Condo Association Fee Statements",
        "docMessage": "Please provide HOA dues statement/transaction history or a letter from the HOA association confirming dues and the frequency of the payment (annual, quarterly or monthly)."
      }
    ]
  }
];

let mockSavedDraft : any = null;

export type DocumentsWithTemplateDetails = {
  id: string;
  name: string;
  docs: TemplateDocument[];
};

export class NewNeedListActions {
  static async getDocumentsFromSelectedTemplates(ids: string[]) {
    return Promise.resolve([]);
  }

  static async getDraft(loanApplicationId: string) {
    return Promise.resolve(mockSavedDraft);

  }

  static async saveNeedList(
    loanApplicationId: string,
    isDraft: boolean,
    emailText: string,
    documents: any[]
  ) {

    mockSavedDraft = [];
    mockSavedDraft.push(documents);
    mockIsDocumentDraft.requestId = "5f84049c49fa941f146f93f0" ;
    console.log('mockIsDocumentDraft', mockIsDocumentDraft);
    return Promise.resolve();
  }

  static async saveAsTemplate(name: string, documents: TemplateDocument[]) {

  }
}
