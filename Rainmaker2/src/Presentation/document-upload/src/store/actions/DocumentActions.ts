import axios, { AxiosResponse } from "axios";
import { Http } from "rainsoft-js";
import { Auth } from "../../services/auth/Auth";
import { Endpoints } from "../endpoints/Endpoints";
import { DocumentRequest } from "../../entities/Models/DocumentRequest";
import { UploadedDocuments } from "../../entities/Models/UploadedDocuments";
import { Document } from "../../entities/Models/Document";
import { DocumentsEndpoints } from "../endpoints/DocumentsEndpoints";
import { FileUpload } from "../../utils/helpers/FileUpload";
import { CategoryDocument } from "../../entities/Models/CategoryDocument";
const mockData = [
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
  }
]
export class DocumentActions {
  static async getPendingDocuments(loanApplicationId: string) {
    try {
      let res: AxiosResponse<DocumentRequest[]> = await Http.get<
        DocumentRequest[]
      >(Endpoints.documents.GET.pendingDocuments(loanApplicationId));

      let d = res?.data?.map((d: DocumentRequest, i: number) => {
        let {
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files,
          isRejected,
        } = d;
        let doc = new DocumentRequest(
          id,
          requestId,
          docId,
          docName,
          docMessage,
          files,
          isRejected
        );

        if (doc.files === null || doc.files === undefined) {
          doc.files = [];
        }
        doc.files = doc.files.map((f: Document) => {
          return new Document(
            f.id,
            f.clientName,
            f.fileUploadedOn,
            f.size,
            f.order,
            FileUpload.getDocLogo(f, "dot"),
            "done"
          );
        });

        return doc;
      });
      return d;
    } catch (error) {
      console.log(error);
    }
  }

  static async getSubmittedDocuments(loanApplicationId: string) {
    try {
      let res: AxiosResponse<UploadedDocuments[]> = await Http.get<
        UploadedDocuments[]
      >(Endpoints.documents.GET.submittedDocuments(loanApplicationId));
      return res.data.map((r) => r);
    } catch (error) {
      console.log(error);
    }
  }

  static documentViewCancelToken: any = axios.CancelToken.source();
  static async getSubmittedDocumentForView(params: any) {
    DocumentActions.documentViewCancelToken.cancel();
    DocumentActions.documentViewCancelToken = axios.CancelToken.source();
    try {
      const accessToken = Auth.getAuth();
      const url = DocumentsEndpoints.GET.viewDocuments(
        params.id,
        params.requestId,
        params.docId,
        params.fileId
      );

      const response = await axios.get(Http.createUrl(Http.baseUrl, url), {
        cancelToken: DocumentActions.documentViewCancelToken.token,
        params: { ...params },
        responseType: "arraybuffer", //arraybuffer response type important to get the correct response back from server.
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });

      return response;
    } catch (error) {
      return Promise.reject(error);
    }
  }

  static async finishDocument(loanApplicationId: string, data: {}) {
    try {
      let doneRes = await Http.put(Endpoints.documents.PUT.finishDocument(), {
        ...data,
      });
      if (doneRes) {
        let remainingPendingDocs = await DocumentActions.getPendingDocuments(
          loanApplicationId
        );
        if (remainingPendingDocs) {
          return remainingPendingDocs;
        }
      }
    } catch (error) {}
  }

  static async fetchCategoryDocuments() {
    try {
     let res: any = await Http.get<any>(Endpoints.documents.GET.categoryDocuments());
     let mappedData = res.data.map((d: any) => {
            for (const item of d.documents) {
               item.files = []
            }
            return new CategoryDocument(d.catId, d.catName, d.documents)
     });
      return mappedData;
    } catch (error) {
      console.log('error', error);
    }
  }

}
