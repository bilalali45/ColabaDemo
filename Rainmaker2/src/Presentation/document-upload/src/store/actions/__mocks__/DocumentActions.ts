import { DocumentRequest } from "../../../entities/Models/DocumentRequest";
import { Document } from "../../../entities/Models/Document";
import { FileUpload } from "../../../utils/helpers/FileUpload";

const seedData = [
    {
        "id": "5f3e259db821542b2841e37b",
        "requestId": "5f450c0860b6782ac07de6ed",
        "docId": "5f450c0860b6782ac07de6ee",
        "docName": "Alimony Income Verification",
        "status": "Pending review",
        "createdOn": "2020-08-25T13:03:04.772Z",
        "files": [
            {
                "id": "5f460e2f9863a30474056ef4",
                "clientName": "sample.pdf",
                "fileUploadedOn": "2020-08-26T07:24:31.978Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            }
        ],
        "typeId": "5eec89cd6ecaea4247964038",
        "userName": "Shehroz Riyaz"
    },
    {
        "id": "5f3e259db821542b2841e37b",
        "requestId": "5f45040260b6782ac07de6ad",
        "docId": "5f45040260b6782ac07de6b2",
        "docName": "Bank Statement",
        "status": "Pending review",
        "createdOn": "2020-08-25T12:28:50.165Z",
        "files": [
            {
                "id": "5f450ac160b6782ac07de6ea",
                "clientName": "nature.jpg",
                "fileUploadedOn": "2020-08-25T12:57:37.829Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f450c4560b6782ac07de6f7",
                "clientName": "nature-02.jpg",
                "fileUploadedOn": "2020-08-25T13:04:05.658Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f450cd160b6782ac07de6fa",
                "clientName": "sample.pdf",
                "fileUploadedOn": "2020-08-25T13:06:25.572Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f450cd460b6782ac07de6fc",
                "clientName": "turkey-30482991920-1366x550.jpg",
                "fileUploadedOn": "2020-08-25T13:06:28.794Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f450d9760b6782ac07de6fe",
                "clientName": "turkey-30482991920-02.jpg",
                "fileUploadedOn": "2020-08-25T13:09:43.658Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            }
        ],
        "typeId": "5eb257a3e519051af2eeb624",
        "userName": "Shehroz Riyaz"
    },
    {
        "id": "5f3e259db821542b2841e37b",
        "requestId": "5f44f6aa60b6782ac07de669",
        "docId": "5f44f6aa60b6782ac07de66a",
        "docName": "Salary Slip",
        "status": "Pending review",
        "createdOn": "2020-08-25T11:31:54.532Z",
        "files": [
            {
                "id": "5f44f76460b6782ac07de680",
                "clientName": "turkey-30482991920-1366x550.jpg",
                "fileUploadedOn": "2020-08-25T11:35:00.461Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f77d60b6782ac07de683",
                "clientName": "turkey-30482991920-02.jpg",
                "fileUploadedOn": "2020-08-25T11:35:25.016Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f7d360b6782ac07de685",
                "clientName": "turkey-30482991920-03.jpeg",
                "fileUploadedOn": "2020-08-25T11:36:51.766Z",
                "mcuName": "",
                "byteProStatus": "Not synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f81460b6782ac07de687",
                "clientName": "turkey-30482991920-04.jpeg",
                "fileUploadedOn": "2020-08-25T11:37:56.73Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f84b60b6782ac07de689",
                "clientName": "nature.jpg",
                "fileUploadedOn": "2020-08-25T11:38:51.61Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f85860b6782ac07de68b",
                "clientName": "turkey-30482991920-05.jpeg",
                "fileUploadedOn": "2020-08-25T11:39:04.479Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f91d60b6782ac07de68d",
                "clientName": "turkey-30482991920-06.jpeg",
                "fileUploadedOn": "2020-08-25T11:42:21.351Z",
                "mcuName": "",
                "byteProStatus": "Not synchronized",
                "isRead": false,
                "status": null
            },
            {
                "id": "5f44f91e60b6782ac07de68f",
                "clientName": "sample.pdf",
                "fileUploadedOn": "2020-08-25T11:42:22.308Z",
                "mcuName": "",
                "byteProStatus": "Not synchronized",
                "isRead": true,
                "status": null
            },
            {
                "id": "5f44f94360b6782ac07de691",
                "clientName": "sample-02.pdf",
                "fileUploadedOn": "2020-08-25T11:42:59.845Z",
                "mcuName": "",
                "byteProStatus": "Not synchronized",
                "isRead": false,
                "status": null
            },
            {
                "id": "5f45029360b6782ac07de6a6",
                "clientName": "sample-03.pdf",
                "fileUploadedOn": "2020-08-25T12:22:43.006Z",
                "mcuName": "",
                "byteProStatus": "Synchronized",
                "isRead": true,
                "status": null
            }
        ],
        "typeId": "5eb257a3e519051af2eeb624",
        "userName": "Shehroz Riyaz"
    },
]

export class DocumentActions {
    static async getPendingDocuments(loanApplicationId: string) {
        let d = seedData.map((d: any, i: number) => {
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

    }

    static async getSubmittedDocuments(loanApplicationId: string) {
        return [];
    }

    static async getSubmittedDocumentForView(params: any) {

    }

    static async finishDocument(loanApplicationId: string, data: {}) {
        return [];

    }
}
