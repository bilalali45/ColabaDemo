import { CurrentInView } from "../../../Models/CurrentInView";
import { DocumentFile } from "../../../Models/DocumentFile";
import { DocumentRequest } from "../../../Models/DocumentRequest";
import { SelectedFile } from "../../../Models/SelectedFile";
import { ViewerTools } from "../../../Utilities/ViewerTools";
import { DocumentActionsType } from "../../reducers/documentsReducer";
import { ViewerActionsType } from "../../reducers/ViewerReducer";
import { ViewerActions } from "../ViewerActions";



export const documentItems:any[] = [{
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
},{
    "id":"5fce0fe0cfc6472d9870f8e3",
    "requestId":"600eb82803130cd04b0964c8",
    "docId":"600eb82803130cd04b0964c9",
    "docName":"Rental Agreement",
    "status":"Manually added",
    "createdOn":"2021-01-25T12:23:04.869Z",
    "files":[{
        "id":"600eb83103130cd04b0964cc",
        "clientName":"",
        "fileUploadedOn":"2021-01-25T12:23:13.61Z",
        "mcuName":"images.jpeg",
        "byteProStatus":"Not synchronized",
        "isRead":false,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    },{
        "id":"600eb83003130cd04b0964cb",
        "clientName":"",
        "fileUploadedOn":"2021-01-25T12:23:12.907Z",
        "mcuName":"images 2.jpeg",
        "byteProStatus":"Not synchronized",
        "isRead":false,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    },{
        "id":"600eb82f03130cd04b0964ca",
        "clientName":"",
        "fileUploadedOn":"2021-01-25T12:23:11.967Z",
        "mcuName":"images 1.jpeg",
        "byteProStatus":"Not synchronized",
        "isRead":false,
        "status":null,
        "userId":1,
        "userName":"System Administrator",
        "fileModifiedOn":null
    }],
    "typeId":"5f473b2dcca0a5d1c9681ab2",
    "userName":" Rt Tru"
}]

export const trashedDocuments =[{
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

export const workbenchItems = [{
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

    



    static nonExistentFileId = "000000000000000000000000"
    static loanApplicationId = 2515;
    static documentViewCancelToken: any ="";
    static performNextAction:any = true
    static docsSearchTerm: string = '';
  
    static async getDocumentItems(dispatch: Function, importedFileIds:any) {
     
      try {
        
  
          let docItems = documentItems;
          if (importedFileIds && importedFileIds.length) {
            importedFileIds.forEach(file => {
  
              docItems = docItems.map((doc: any) => {
                doc.files = doc.files.filter((f: any) => f.id !== file.fromFileId)
                return doc
              })
            });
          }
          let items = [];
  
          for (const item of docItems) {
            let newItem = { ...item };
            let cachedFiles = newItem.files;
            newItem.files = [];
            for (const file of cachedFiles) {
              newItem.files.push({ ...file });
            }
            items.push(newItem);
          }
          this.filterDocumentItems(dispatch, items, this.docsSearchTerm)
          dispatch({ type: DocumentActionsType.SearchDocumentItems, payload: docItems });
        
        let docs = documentItems;;
        if (docs.length === 1 && docs[0]?.files.length === 0) {
          dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
        }
        return documentItems;;
      } catch (error) {
        console.log(error);
      }
    }
  
    static filterDocumentItems = (dispatch: Function, documentsList: any, term: string) => {
      
      let items = [];
  
      for (const item of documentsList) {
        let newItem = { ...item };
        let cachedFiles = newItem.files;
        newItem.files = [];
        for (const file of cachedFiles) {
          newItem.files.push({ ...file });
        }
        items.push(newItem);
      }
  
      if (term && term != "") {
        const result = items.filter(doc => {
          if (doc.docName.toLowerCase().includes(term.toLowerCase())) {
            return doc;
          } else {
            const filter = f => {
              let name = f?.mcuName || f?.clientName;
              return name?.toLowerCase().includes(term.toLowerCase());
            }
            doc.files = doc.files.filter(filter);
            if (doc.files.length) {
              return doc;
            }
          }
        })
          dispatch({ type: DocumentActionsType.DocSearchTerm, payload: term });
          dispatch({ type: DocumentActionsType.SetDocumentItems, payload: result });
        } else {
          dispatch({ type: DocumentActionsType.DocSearchTerm, payload: "" });
          dispatch({ type: DocumentActionsType.SetDocumentItems, payload: documentsList });
      }
    }
  
  
    static getCurrentDocumentItems = async (dispatch: Function, isFirstLoad: boolean, importedFileIds: any) => {
      let docs: any = await DocumentActions.getDocumentItems(dispatch, importedFileIds);
      let foundFirstFileDoc: any = null;
      let foundFirstFile: any = null;
      ViewerActions.resetInstance(dispatch)
      dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
  
      if (docs && docs.length > 0) {
  
        for (const doc of docs) {
          if (doc?.files?.length) {
            dispatch({ type: DocumentActionsType.SetCurrentDoc, payload: doc });
            dispatch({ type: ViewerActionsType.SetIsLoading, payload: true });
            foundFirstFileDoc = doc;
            foundFirstFile = doc?.files[0];
  
            await DocumentActions.viewFile(foundFirstFileDoc, foundFirstFile, dispatch);
            break;
          } else
            if (!isFirstLoad) {
              ViewerActions.resetInstance(dispatch)
              dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
            }
  
        }
  
  
      }
  
    }
  
  
    static getCurrentWorkbenchItem = async (dispatch: Function, importedFileIds: any) => {
      let files: any = await DocumentActions.getWorkBenchItems(dispatch, importedFileIds);
      let foundFirstFile: any = null;
      if (files?.length > 0) {
        foundFirstFile = files[0];
  
        let selectedFileData = new SelectedFile(foundFirstFile.id, DocumentActions.getFileName(foundFirstFile), foundFirstFile.fileId)
  
        dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData });
        ViewerTools.currentFileName = selectedFileData.name
        let f = await DocumentActions.getFileToView(
          foundFirstFile?.id,
          DocumentActions.nonExistentFileId,
          DocumentActions.nonExistentFileId,
          foundFirstFile.fileId,
          false,
          true,
          false,
          dispatch
        );
  
  
        let currentFile = new CurrentInView(foundFirstFile?.id, f, DocumentActions.getFileName(foundFirstFile), true, foundFirstFile.fileId);
        dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
  
      }
      else {
  
        ViewerActions.resetInstance(dispatch)
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
      }
  
    }
  
  
    static async submitDocuments(
        documents: any,
        currentSelected: DocumentRequest,
        fileId: string,
        file: File,
        dispatchProgress: Function,
        ) {
        }

    
    

    static async SaveCategoryDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async SaveTrashDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async SaveWorkbenchDocument(fileObj: any, file: File, dispatchProgress: Function, currentDoc: any) {
    }

    static async getFileToView(id: string, requestId: string, docId: string, fileId: string, isFromCategory: boolean, isFromWorkbench: boolean, isFromTrash: boolean, dispatch: Function) {
  
      
    //   DocumentActions.documentViewCancelToken = Axios.CancelToken.source();
    //   let url = Endpoints.Document.GET.viewDocument(
    //     id,
    //     requestId,
    //     docId,
    //     fileId
    //   );
  
    //   let downloadProgress = 0;
    //   const authToken = LocalDB.getAuthToken();
    //   dispatch({
    //     type: ViewerActionsType.SetFileProgress,
    //     payload: 0,
    //   });
    //   try {
    //     const response = await Axios.get(Http.createUrl(Http.baseUrl, url), {
    //       cancelToken: DocumentActions.documentViewCancelToken.token,
    //       responseType: 'blob',
    //       headers: {
    //         Authorization: `Bearer ${authToken}`
    //       },
    //       onDownloadProgress: (e) => {
    //         let p = Math.floor((e.loaded / e.total) * 100);
    //         downloadProgress = p;
    //         if (p === 100) {
    //           dispatch({
    //             type: ViewerActionsType.SetIsLoading,
    //             payload: false
    //           })
              
  
    //         }
  
    //         dispatch({
    //           type: ViewerActionsType.SetFileProgress,
    //           payload: downloadProgress,
    //         });
    //       },
    //     });
  
    //     let fileData = {
    //       id,
    //       requestId,
    //       docId,
    //       fileId,
    //       isFromCategory,
    //       isFromWorkbench,
    //       isFromTrash
    //     }
    //     let file: any;
    //     if (!response.data.type.includes('pdf')) {
    //       file = await ViewerTools.convertImageToPDF(response.data, false, fileData, false);
  
    //     } else {
  
    //       file = response.data;
    //     }
  
             return "";
    //   }
    //   catch (error) {
    //     console.log(error)
    //   }
  
    }
  
  
    static async addDocCategory(locationId: string, requestData: any) {

      try {
       
        return true;
      } catch (error) {
        console.log(error);
      }
  
  
    }
  
    static async deleteDocCategory(id: string,
      requestId: string,
      docId: string) {
  
  
      
      try {
    
        return true;
      } catch (error) {
        console.log(error);
      }
  
  
    }
  
  
  
    static async renameDoc(id: string,
      requestId: string,
      docId: string,
      fileId: string,
      newName: string) {
  
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
  
  
    }
  
    static async reassignDoc(
      id: string,
      fromRequestId: string,
      fromDocId: string,
      fromFileId: string,
      toRequestId: string,
      toDocId: string,) {
  
  
     
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
  
  
    }

  
    static prepareFormData(currentSelected: DocumentRequest, file: DocumentFile) {
      const data = new FormData();
  
      data.append('id', currentSelected.id.toString())
      data.append('requestId', currentSelected.requestId.toString())
      data.append('docId', currentSelected.docId.toString())
  
      if (file.file) {
        data.append("files", file.file, `${file.clientName}`);
      }
  
  
      return data;
    }
  
    static async getWorkBenchItems(dispatch: Function, importedFileIds: any) {
  
      try {
          let docItems = workbenchItems;
          if (importedFileIds && importedFileIds.length) {
            importedFileIds.forEach(file => {
  
              docItems = docItems.filter((f: any) => f.fileId !== file.fromFileId)
  
            });
          }
          dispatch({ type: DocumentActionsType.SetWorkbenchItems, payload: docItems });
        
        return workbenchItems;
      } catch (error) {
        console.log(error);
      }
  
    }
  
    static async getTrashedDocuments(dispatch: Function, importedFileIds: any) {
      
      try {
        
          let docItems = trashedDocuments;
          if (importedFileIds && importedFileIds.length) {
            importedFileIds.forEach(file => {
  
              docItems = docItems.filter((f: any) => f.fileId !== file.fromFileId)
            });
          }
  
          dispatch({ type: DocumentActionsType.SetTrashedDoc, payload: docItems });
        
  
        return trashedDocuments;
      } catch (error) {
        console.log(error);
      }
    }
  
  
    static moveFileToDoc() {
  
    }
  
    static async moveFileToWorkbench(body: any, isFromThumbnail: boolean) {
  
      /*if (isFromThumbnail) {
        let url = Endpoints.Document.POST.moveFromCategoryToWorkBench();
        return this.moveFileToWorkbenchLocal(url, body);
  
      } else {
        let url = Endpoints.Document.POST.moveFromCategoryToWorkBench();
        return this.moveFileToWorkbenchLocal(url, body);
      }*/
  
      return this.moveFileToWorkbenchLocal("", body);
  
    }
  
    static async moveFileToWorkbenchLocal(url: any, body: any) {
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
  
    }
  
    static async moveFromWorkBenchToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
      
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
    }
  
  
    static async moveFromTrashToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
      
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
    }
  
    static async moveCatFileToTrash(id: string, requestId: string, docId: string, fileId: string, cancelCurrentFileViewRequest: boolean) {

     
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
      return false;
    }
  
    static async moveWorkBenchFileToTrash(id: string, fileId: string, cancelCurrentFileViewRequest: boolean) {
      
      try {
       
        return true;
      } catch (error) {
        console.log(error);
      }
      return false;
    }
  
    static async moveTrashFileToWorkBench(id: string, fileId: string) {
  
     
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
    }
  
    static async DeleteCategoryFile(fileData: any) {
      
      try {
       
        return true;
      } catch (error) {
        console.log(error);
      }
    }
    static async DeleteTrashFile(id: string, fileId: string) {
  
     
      try {
       
        return true;
      } catch (error) {
        console.log(error);
      }
    }
  
    static async DeleteWorkbenchFile(id: string, fileId: string) {
  
     
      try {
        
        return true;
      } catch (error) {
        console.log(error);
      }
    }
  
    // static async SaveCategoryDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    //   let selectedFile = new DocumentFile(
    //     document.fileId,
    //     FileUpload.removeSpecialChars(file.name),
    //     FileUpload.todayDate(),
    //     0,
    //     0,
    //     FileUpload.getDocLogo(file, "slash"),
    //     file,
    //     "pending",
  
    //   );
  
    //   try {
    //     let { id, requestId, docId, fileId } = document
    //     // const file = new Blob([buffer], { type: "application/pdf" });
    //     const formData = new FormData();
    //     formData.append('id', id)
    //     formData.append('requestId', requestId);
    //     formData.append('docId', docId);
    //     formData.append('fileId', fileId);
    //     formData.append('file', file);
  
    //     if (fileId === DocumentActions.nonExistentFileId) {
    //       let files = [selectedFile, ...currentDoc.files]
    //       currentDoc.files = files
    //     }
    //     let res = await Http.fetch(
    //       {
    //         method: Http.methods.POST,
    //         url: Http.createUrl(
    //           Http.baseUrl,
    //           Endpoints.Document.POST.saveCategoryDocument()
    //         ),
    //         // cancelToken: file.uploadReqCancelToken.token,
    //         data: formData,
    //         onUploadProgress: (e) => {
    //           let p = Math.floor((e.loaded / e.total) * 100);
    //           selectedFile.uploadProgress = p;
    //           if (p === 100) {
    //             selectedFile.uploadStatus = "done";
    //             dispatchProgress({
    //               type: ViewerActionsType.SetIsSaving,
    //               payload: false,
    //             });
  
    //           }
  
    //           const docFiles = currentDoc?.files?.map((docFile: any) => {
    //             if (docFile.id === document?.fileId) {
    //               docFile = selectedFile
    //             }
    //             return docFile;
    //           })
    //           currentDoc.files = docFiles;
    //           dispatchProgress({
    //             type: DocumentActionsType.UpdateDocFile,
    //             payload: currentDoc
    //           });
  
    //           dispatchProgress({
    //             type: ViewerActionsType.SetFileProgress,
    //             payload: p,
    //           });
    //         },
    //       },
    //       {
    //         Authorization: `Bearer ${LocalDB.getAuthToken()}`,
    //       }
    //     );
  
    //     return res.data;
    //   } catch (error) {
    //     console.log('error', error.response);
    //     let err = error.response.data;
    //     selectedFile.uploadStatus = 'failed';
    //     selectedFile.notAllowedReason = 'Failed';
    //     selectedFile.failedReason = err.Message ? err.Message : err;
    //     console.log("-------------->Upload errors------------>", error);
    //   }
    // }
  
    // static async SaveTrashDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    //   let selectedFile = new DocumentFile(
    //     "",
    //     FileUpload.removeSpecialChars(file.name),
    //     FileUpload.todayDate(),
    //     0,
    //     0,
    //     FileUpload.getDocLogo(file, "slash"),
    //     file,
    //     "pending",
  
    //   );
    //   try {
  
    //     // const file = new Blob([buffer], { type: "application/pdf" });
    //     const formData = new FormData();
    //     formData.append('id', document.id)
    //     formData.append('fileId', "000000000000000000000000");
    //     formData.append('file', file);
  
  
    //     let files = [selectedFile, ...currentDoc]
    //     dispatchProgress({
    //       type: DocumentActionsType.AddFileToTrash,
    //       payload: files
    //     });
  
    //     let res = await Http.fetch(
    //       {
    //         method: Http.methods.POST,
    //         url: Http.createUrl(
    //           Http.baseUrl,
    //           Endpoints.Document.POST.saveTrashDocument()
    //         ),
    //         // cancelToken: file.uploadReqCancelToken.token,
    //         data: formData,
    //         onUploadProgress: (e) => {
    //           let p = Math.floor((e.loaded / e.total) * 100);
    //           selectedFile.uploadProgress = p;
    //           if (p === 100) {
    //             selectedFile.uploadStatus = "done";
    //             dispatchProgress({
    //               type: ViewerActionsType.SetIsSaving,
    //               payload: false,
    //             });
  
    //           }
    //           const docFiles = files?.map((docFile: any) => {
    //             if (docFile.fileId === DocumentActions.nonExistentFileId) {
    //               docFile = selectedFile
    //             }
    //             return docFile;
    //           })
  
    //           dispatchProgress({
    //             type: DocumentActionsType.AddFileToTrash,
    //             payload: docFiles
    //           });
  
    //           dispatchProgress({
    //             type: ViewerActionsType.SetFileProgress,
    //             payload: p,
    //           });
    //         },
    //       },
    //       {
    //         Authorization: `Bearer ${LocalDB.getAuthToken()}`,
    //       }
    //     );
  
    //     return res.data;
    //   } catch (error) {
    //     console.log('error', error.response);
  
    //     selectedFile.uploadStatus = 'failed';
    //     selectedFile.notAllowedReason = 'Failed';
    //     selectedFile.failedReason = error.Message ? error.Message : error;
    //     dispatchProgress({
    //       type: DocumentActionsType.AddFileToTrash,
    //       payload: selectedFile
    //     });
    //     console.log("-------------->Upload errors------------>", error);
    //   }
    // }
  
    // static async SaveWorkbenchDocument(fileObj: any, file: File, dispatchProgress: Function, currentDoc: any) {
    //   let selectedFile = new DocumentFile(
    //     "",
    //     FileUpload.removeSpecialChars(file.name),
    //     FileUpload.todayDate(),
    //     0,
    //     0,
    //     FileUpload.getDocLogo(file, "slash"),
    //     file,
    //     "pending",
  
    //   );
    //   selectedFile = await Rename.rename(currentDoc, selectedFile);
    //   try {
    //     let { id, fileId } = fileObj
    //     const formData = new FormData();
    //     formData.append('id', id)
    //     formData.append('fileId', fileId);
        
  
    //     if (selectedFile.file) {
    //       formData.append("file", selectedFile.file, `${selectedFile.clientName}`);
    //     }
  
    //     let files: any = currentDoc;
    //     if (fileId === DocumentActions.nonExistentFileId) {
    //       files = [selectedFile, ...currentDoc]
  
    //     }
    //     let res = await Http.fetch(
    //       {
    //         method: Http.methods.POST,
    //         url: Http.createUrl(
    //           Http.baseUrl,
    //           Endpoints.Document.POST.saveWorkbenchDocument()
    //         ),
  
    //         // cancelToken: file.uploadReqCancelToken.token,
    //         data: formData,
    //         onUploadProgress: (e) => {
    //           let p = Math.floor((e.loaded / e.total) * 100);
    //           selectedFile.uploadProgress = p;
    //           if (p === 100) {
    //             selectedFile.uploadStatus = "done";
    //             dispatchProgress({
    //               type: ViewerActionsType.SetIsSaving,
    //               payload: false,
    //             });
  
    //           }
  
    //           const docFiles = files?.map((docFile: any) => {
    //             if (docFile.fileId === fileId) {
    //               docFile = selectedFile
    //             }
    //             return docFile;
    //           })
    //           dispatchProgress({
    //             type: DocumentActionsType.AddFileToWorkbench,
    //             payload: docFiles
    //           });
    //           dispatchProgress({
    //             type: ViewerActionsType.SetFileProgress,
    //             payload: p,
    //           });
    //         }
  
    //       },
    //       {
    //         Authorization: `Bearer ${LocalDB.getAuthToken()}`,
    //       }
    //     );
  
    //     return res.data;
    //   } catch (error) {
    //     console.log('error', error.response);
    //     selectedFile.uploadStatus = 'failed';
    //     selectedFile.notAllowedReason = 'Failed';
    //     selectedFile.failedReason = error.Message ? error.Message : error;
    //     dispatchProgress({
    //       type: DocumentActionsType.AddFileToWorkbench,
    //       payload: selectedFile
    //     });
    //     console.log("-------------->Upload errors------------>", error);
    //   }
    // }
  
  
    // static async fetchFile(path: any) {
  
    //   let f = await fetch(path).then(r => r.blob());
    //   return f;
    // }
  
    static async getLoanApplicationDetail(loanApplicationId: string) {
        return loanAppDetails;
    }
   
    static async getLoanApplicationId(loanApplicationId: string) {
        return "5fce0fe0cfc6472d9870f8e3";
    }
  
  
    static async syncFileToLos(fileData: any) {

      try {
        
        return true;
      } catch (error) {
        console.log('error', '-------------------------------------', error);
        return Promise.reject(error);
      }
    }
  
    static getFileName(file: any) {
      if (file?.mcuName) return file?.mcuName;
      return file?.clientName;
    };
  
    static async viewFile(document: any, file: any, dispatch: Function) {
        let selectedFileData = new SelectedFile(document.id,this.getFileName(file), file.id )
        dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData});
        ViewerTools.currentFileName = selectedFileData.name
        let f = await DocumentActions.getFileToView(
          document.id,
          document.requestId,
          document.docId,
          file.id, 
          true, 
          false, 
          false,
          dispatch
        );
          let currentFile = new CurrentInView(document.id, f, this.getFileName(file), false, file.id);
          dispatch({ type: ViewerActionsType.SetCurrentFile, payload: currentFile });
          selectedFileData = new SelectedFile(document.id,this.getFileName(file), file.id )
          dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: selectedFileData});
          ViewerTools.currentFileName = selectedFileData.name
          // dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
          dispatch({
            type: ViewerActionsType.SetFileProgress,
            payload: 0,
          });
      
    }
  
    static showFileBeingDragged(e: any, file: any) {
      let fileItemDragView: any = window.document.createElement('div');
      fileItemDragView.id = 'fileBeingDragged';
      fileItemDragView.className = 'fileBeingDragged';
  
      let fileName = file.mcuName || file?.clientName;
      let by = `<span class="mb-lbl">${file.fileModifiedOn ? "Modified By:" : "Uploaded By:"}</span>  <span class="mb-name">${file.userName ? file.userName : "Borrower"}</span>`;
      //fileItemDragView = document.getElementById('fileBeingDragged');
      // fileItemDragView.innerHTML = '<p style="color: white; font-weight: bold; font-size: 20px;">'+fileName+'</p><p style="color: white;">'+by+'</p>';
      fileItemDragView.innerHTML = '<div class="l-icon"><svg xmlns="http://www.w3.org/2000/svg" width="15" height="20" viewBox="0 0 15 20"><path id="Path_633" data-name="Path 633" d="M14.453-13.672A1.808,1.808,0,0,1,15-12.344V.625a1.808,1.808,0,0,1-.547,1.328,1.808,1.808,0,0,1-1.328.547H1.875A1.808,1.808,0,0,1,.547,1.953,1.808,1.808,0,0,1,0,.625v-16.25a1.808,1.808,0,0,1,.547-1.328A1.808,1.808,0,0,1,1.875-17.5H9.844a1.808,1.808,0,0,1,1.328.547ZM12.969-12.5,10-15.469V-12.5ZM1.875.625h11.25v-11.25H9.063A.9.9,0,0,1,8.4-10.9a.9.9,0,0,1-.273-.664v-4.062H1.875Z" transform="translate(0 17.5)" fill="#7e829e"></path></svg></div><div class="d-name"><div><p>' + fileName + '</p><div class="modify-info">' + by + '</div></div></div>';
      window.document.body.appendChild(fileItemDragView);
      e.dataTransfer.setDragImage(fileItemDragView, 5, 5);
  
  
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

