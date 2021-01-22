import { DocumentFile } from "../../../Models/DocumentFile";
import { DocumentRequest } from "../../../Models/DocumentRequest";
import { DocumentActionsType } from "../../reducers/documentsReducer";




const documentItems = [{
    "id":"5feb1f0ec20bc413c03d39d5",
    "requestId":"6000191632088251cb1c705c",
    "docId":"6000191632088251cb1c705d",
    "docName":"Bank Statements - Two Months",
    "status":"Manually added",
    "createdOn":"2021-01-14T10:12:38.581Z",
    "files":[{
        "id":"60001b9932088251cb1c7061",
        "clientName":"",
        "fileUploadedOn":"2021-01-14T10:23:21.757Z",
        "mcuName":"images 1.jpeg",
        "byteProStatus":"Not synchronized",
        "isRead":true,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    },{
        "id":"60001e4032088251cb1c7062",
        "clientName":"",
        "fileUploadedOn":"2021-01-14T10:34:40.419Z",
        "mcuName":"Portrait-family-1-600-xxxq87.png",
        "byteProStatus":"Not synchronized",
        "isRead":false,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    }],
    "typeId":"5eb257a3e519051af2eeb624",
    "userName":"Doc Doc"
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "requestId":"5ff3e45d323122326cdbc7e3",
    "docId":"5ff3e45d323122326cdbc7e5",
    "docName":"Bank Statements - Two Months",
    "status":"Borrower to do",
    "createdOn":"2021-01-05T04:00:29.274Z",
    "files":[{
        "id":"5ff3e4c17c1915f70fd205fe",
        "clientName":"",
        "fileUploadedOn":"2021-01-05T04:02:09.894Z",
        "mcuName":"downloadrsadfdadsaadsadfd.pdf",
        "byteProStatus":"Not synchronized",
        "isRead":true,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":"2021-01-05T07:09:03.854Z"
    },{
        "id":"5ff411686e0945cf8729b9c9",
        "clientName":"",
        "fileUploadedOn":"2021-01-05T07:12:40.561Z",
        "mcuName":"dummy.pdf",
        "byteProStatus":"Not synchronized",
        "isRead":true,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    }],
    "typeId":"5eb257a3e519051af2eeb624",
    "userName":"Doc Doc"
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "requestId":"5ff3e45d323122326cdbc7e3",
    "docId":"5ff3e45d323122326cdbc7ed",
    "docName":"Tax Returns with Schedules (Personal - Two Years)",
    "status":"Borrower to do",
    "createdOn":"2021-01-05T04:00:29.274Z",
    "files":[{
        "id":"5ff3ef797c1915f70fd2060b",
        "clientName":"",
        "fileUploadedOn":"2021-01-05T04:47:53.091Z",
        "mcuName":"download.png 50202194752.pdf",
        "byteProStatus":"Not synchronized",
        "isRead":false,
        "status":null,
        "userId":6741,
        "userName":"Ali Momin",
        "fileModifiedOn":"2021-01-05T04:47:53.091Z"
    }],
    "typeId":"5eec89fd6ecaea424796436a",
    "userName":"Doc Doc"
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "requestId":"5ff2ff79323122326cdbc6dd",
    "docId":"5ff2ff7a323122326cdbc6e7",
    "docName":"W-2s - Last Two years",
    "status":"Borrower to do",
    "createdOn":"2021-01-04T11:43:53.978Z",
    "files":[],
    "typeId":"5f473879cca0a5d1c9659cc3",
    "userName":"Doc Doc"
}]

const trashedDocuments =[{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"5ff3ea8e7c1915f70fd20604",
    "fileUploadedOn":"2021-01-05T04:26:54.04Z",
    "mcuName":"Portrait-family-1-600-xxxq87.png",
    "userId":6741,
    "userName":"Ali Momin",
    "fileModifiedOn":null
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"5ff3073c7c1915f70fd205fa",
    "fileUploadedOn":"2021-01-04T12:17:00.23Z",
    "mcuName":"download 1-copy-1.png",
    "userId":6741,
    "userName":"Ali Momin",
    "fileModifiedOn":null
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"5ff301c77c1915f70fd205f7",
    "fileUploadedOn":"2021-01-04T11:53:43.058Z",
    "mcuName":"download 1.pdf",
    "userId":6741,
    "userName":"Ali Momin",
    "fileModifiedOn":"2021-01-04T11:53:55.669Z"
}]

const workbenchItems = [{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"60001fd232088251cb1c7066",
    "fileUploadedOn":"2021-01-14T10:41:22.657Z",
    "mcuName":"f609e7b380524c72ee79ecc47cad7e11.jpeg",
    "userId":1,
    "userName":"System Administrator"
    ,"fileModifiedOn":null
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"5ff3ed717c1915f70fd20609",
    "fileUploadedOn":"2021-01-05T04:39:13.716Z",
    "mcuName":"50202193912.pdf",
    "userId":6741,
    "userName":"Ali Momin",
    "fileModifiedOn":"2021-01-05T04:39:13.716Z"
},{
    "id":"5feb1f0ec20bc413c03d39d5",
    "fileId":"5ff3ec747c1915f70fd20606",
    "fileUploadedOn":"2021-01-05T04:35:00.994Z",
    "mcuName":"5020219350.pdf",
    "userId":6741,
    "userName":"Ali Momin",
    "fileModifiedOn":"2021-01-05T04:35:00.994Z"
}]

const loanAppDetails = {
    "loanPurpose":"Purchase a home",
    "propertyType":"Single Family Detached",
    "stateName":"TX",
    "countyName":"Harris County",
    "cityName":"Houston",
    "streetAddress":"abc ",
    "zipCode":"77001",
    "loanAmount":120000.0,
    "countryName":null,
    "unitNumber":null,
    "status":"Application Submitted",
    "borrowers":["Doc Doc"],
    "loanNumber":"90020000106",
    "expectedClosingDate":null,
    "popertyValue":150000.0,
    "rate":null,
    "loanProgram":null,
    "lockStatus":"Float",
    "lockDate":"2020-12-29T12:14:20.54Z",
    "expirationDate":null}

export default class DocumentActions {
    static async getDocumentItems(dispatch: Function, importedFileIds: any) {
        dispatch({ type: DocumentActionsType.SearchDocumentItems, payload: documentItems });
        return documentItems;
    }

    static filterDocumentItems = (dispatch: Function, documentsList: any, term: string) => {
        dispatch({ type: DocumentActionsType.SearchDocumentItems, payload: documentItems });
        return documentItems;
    }

    static getCurrentDocumentItems = async (dispatch: Function, isFirstLoad: boolean, importedFileIds: any) => {
        
        return documentItems[0];
    }

    static getCurrentWorkbenchItem = async (dispatch: Function, importedFileIds: any) => {
        return workbenchItems[0];
    }

    static async getFileToView(id: string, requestId: string, docId: string, fileId: string, isFromCategory: boolean, isFromWorkbench: boolean, isFromTrash: boolean, dispatch: Function) {
    }

    static async addDocCategory(locationId: string, requestData: any) {
        return true;
    }

    static async deleteDocCategory(id: string,
        requestId: string,
        docId: string) {
        }

    static async renameDoc(id: string,
        requestId: string,
        docId: string,
        fileId: string,
        newName: string) {}

    static async reassignDoc(
        id: string,
        fromRequestId: string,
        fromDocId: string,
        fromFileId: string,
        toRequestId: string,
        toDocId: string,) {
        }
        
    static async submitDocuments(
        documents: any,
        currentSelected: DocumentRequest,
        fileId: string,
        file: File,
        dispatchProgress: Function,
        ) {
        }

    static prepareFormData(currentSelected: DocumentRequest, file: DocumentFile) {
    }

    static async getWorkBenchItems(dispatch: Function, importedFileIds: any) {
        dispatch({ type: DocumentActionsType.SetWorkbenchItems, payload: workbenchItems });
        return workbenchItems;
    }

    static async getTrashedDocuments(dispatch: Function, importedFileIds: any) {
        dispatch({ type: DocumentActionsType.SetTrashedDoc, payload: trashedDocuments });
        return trashedDocuments;
    }

    static async moveFileToWorkbench(body: any, isFromThumbnail: boolean) {
    }

    static async moveFromWorkBenchToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
    }

    static async moveFromTrashToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
    }

    static async moveCatFileToTrash(id: string, requestId: string, docId: string, fileId: string, cancelCurrentFileViewRequest: boolean) {
    }

    static async moveWorkBenchFileToTrash(id: string, fileId: string, cancelCurrentFileViewRequest: boolean) {

    }

    static async moveTrashFileToWorkBench(id: string, fileId: string) {
        return true;
    }

    static async DeleteCategoryFile(fileData: any) {
    }

    static async DeleteTrashFile(id: string, fileId: string) {
    }

    static async DeleteWorkbenchFile(id: string, fileId: string) {
    }

    static async SaveCategoryDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async SaveTrashDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async SaveWorkbenchDocument(fileObj: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async getLoanApplicationDetail(loanApplicationId: string) {
        return loanAppDetails;
    }

    static async syncFileToLos(fileData: any) {
    }

    static getFileName(file: any) {
        if (file?.mcuName) return file?.mcuName;
        return file?.clientName;
      };

    static async viewFile(document: any, file: any, dispatch: Function) {
    }

    static showFileBeingDragged(e: any, file: any) {
    }

    static async checkIsByteProAuto() {
        return {
            "id":"5f2acfc0c32e68366c0e7311",
            "tenantId":1,
            "emailTemplate":"Hi {user},\r\n\r\nWe're busy reviewing your file, but to continue the process we need the following documents.\r\n\r\n{documents}\r\n\r\nPlease upload the above documents in two days to avoid any processing delays.\r\n\r\nThank you,",
            "syncToBytePro":2,
            "autoSyncToBytePro":0
        }
    }


}