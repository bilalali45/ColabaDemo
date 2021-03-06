import { Http } from 'rainsoft-js'
import { TrashItem } from '../../Models/TrashItem';
import { Endpoints } from '../endpoints/Endpoints';
import { AxiosResponse } from 'axios';
import { CategoryDocument } from '../../Models/CategoryDocument';
import { Document } from '../../Models/Document';
import { DocumentRequest } from '../../Models/DocumentRequest';
import { ApplicationEnv } from '../../Utilities/helpers/AppEnv';
import { FileUpload } from '../../Utilities/helpers/FileUpload';
import { DocumentFile } from '../../Models/DocumentFile';
import { LocalDB } from '../../Utilities/LocalDB';
import Axios from 'axios'
import { DocumentActionsType } from '../reducers/documentsReducer';
import { PDFThumbnails } from '../../Utilities/PDFThumbnails';
import { CurrentInView } from '../../Models/CurrentInView';
import { ViewerActionsType } from '../reducers/ViewerReducer';
import { SelectedFile } from '../../Models/SelectedFile';
import { debug } from 'console';
import { ViewerActions } from './ViewerActions';
import { Rename } from '../../Utilities/helpers/Rename';
import { ViewerTools } from '../../Utilities/ViewerTools';

export default class DocumentActions {
  static nonExistentFileId = "000000000000000000000000"
  static loanApplicationId = LocalDB.getLoanAppliationId();
  static documentViewCancelToken: any = Axios.CancelToken.source();
  static performNextAction:any = true
  static docsSearchTerm: string = '';

  static async getDocumentItems(dispatch: Function, importedFileIds:any) {
    let url = Endpoints.Document.GET.all(
      DocumentActions.loanApplicationId
    );
    try {
      let res: AxiosResponse<DocumentRequest[]> = await Http.get<DocumentRequest[]>(url);
      if (res) {

        let docItems = res.data;
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
      }
      let docs = res.data;
      if (docs.length === 1 && docs[0]?.files.length === 0) {
        dispatch({ type: ViewerActionsType.SetIsLoading, payload: false });
      }
      return res.data;
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



  static async getFileToView(id: string, requestId: string, docId: string, fileId: string, isFromCategory: boolean, isFromWorkbench: boolean, isFromTrash: boolean, dispatch: Function) {

    await DocumentActions.documentViewCancelToken.cancel();
    DocumentActions.documentViewCancelToken = Axios.CancelToken.source();
    let url = Endpoints.Document.GET.viewDocument(
      id,
      requestId,
      docId,
      fileId
    );

    let downloadProgress = 0;
    const authToken = LocalDB.getAuthToken();
    dispatch({
      type: ViewerActionsType.SetFileProgress,
      payload: 0,
    });
    try {
      const response = await Axios.get(Http.createUrl(Http.baseUrl, url), {
        cancelToken: DocumentActions.documentViewCancelToken.token,
        responseType: 'blob',
        headers: {
          Authorization: `Bearer ${authToken}`
        },
        onDownloadProgress: (e) => {
          let p = Math.floor((e.loaded / e.total) * 100);
          downloadProgress = p;
          if (p === 100) {
            dispatch({
              type: ViewerActionsType.SetIsLoading,
              payload: false
            })
            

          }

          dispatch({
            type: ViewerActionsType.SetFileProgress,
            payload: downloadProgress,
          });
        },
      });

      let fileData = {
        id,
        requestId,
        docId,
        fileId,
        isFromCategory,
        isFromWorkbench,
        isFromTrash
      }
      let file: any;
      if (!response.data.type.includes('pdf')) {
        file = await ViewerTools.convertImageToPDF(response.data, false, fileData, false);

      } else {

        file = response.data;
      }

      return file;
    }
    catch (error) {
      console.log(error)
    }

  }


  static async addDocCategory(locationId: string, requestData: any) {


    let url = Endpoints.Document.POST.docCategory();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          loanApplicationId: +locationId,
          request: requestData
        }
      );
      return true;
    } catch (error) {
      console.log(error);
    }


  }

  static async deleteDocCategory(id: string,
    requestId: string,
    docId: string) {


    let url = Endpoints.Document.POST.delDocCategory();
    try {
      let res: AxiosResponse = await Http.put(
        url,
        {
          id: id,
          requestId: requestId,
          docId: docId
        }
      );
      return res;
    } catch (error) {
      console.log(error);
    }


  }



  static async renameDoc(id: string,
    requestId: string,
    docId: string,
    fileId: string,
    newName: string) {


    let url = Endpoints.Document.POST.rename();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          requestId: requestId,
          docId: docId,
          fileId: fileId,
          newName: newName
        }
      );
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


    let url = Endpoints.Document.POST.reassignDoc();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fromRequestId: fromRequestId,
          fromDocId: fromDocId,
          fromFileId: fromFileId,
          toRequestId: toRequestId,
          toDocId: toDocId
        }
      );
      return true;
    } catch (error) {
      console.log(error);
    }


  }

  static async submitDocuments(
    documents: any,
    currentSelected: DocumentRequest,
    fileId: string,
    file: File,
    dispatchProgress: Function,
  ) {
    let selectedFile = new DocumentFile(
      fileId.toString(),
      FileUpload.removeSpecialChars(file.name),
      FileUpload.todayDate(),
      0,
      0,
      FileUpload.getDocLogo(file, "slash"),
      file,
      "pending",
      "",
      currentSelected.docId,
      "System Administrator"

    );

    selectedFile = await Rename.rename(currentSelected.files, selectedFile);
    selectedFile.uploadProgress = 0;
    let allDocs = documents.map((doc: any) => {
      if (doc.docId === currentSelected.docId) {
        doc = currentSelected
      }
      return doc
    })

    dispatchProgress({
      type: DocumentActionsType.SetDocumentItems,
      payload: allDocs,
    });
    try {

      if (await !FileUpload.isSizeAllowed(file)) {
        selectedFile.notAllowedReason = "FileSize";
        selectedFile.notAllowed = true;
        return selectedFile
      }
      if ((await FileUpload.isTypeAllowed(file)) === false) {
        selectedFile.notAllowedReason = "FileType";
        selectedFile.notAllowed = true;
        return selectedFile;

      }
      currentSelected.files = [selectedFile, ...currentSelected?.files]
      await Http.fetch(
        {
          method: Http.methods.POST,
          url: Http.createUrl(
            Http.baseUrl,
            Endpoints.Document.POST.filesAddedToCategory()
          ),
          // cancelToken: file.uploadReqCancelToken.token,
          data: DocumentActions.prepareFormData(currentSelected, selectedFile),
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            selectedFile.uploadProgress = p;
            if (p === 100) {
              selectedFile.uploadStatus = "done";

              dispatchProgress({
                type: ViewerActionsType.SetFileProgress,
                payload: 0,
              });
            }

            let allDocs = documents.map((doc: any) => {
              if (doc.docId === currentSelected.docId) {
                doc = currentSelected
              }
              return doc
            })

            dispatchProgress({
              type: DocumentActionsType.SetDocumentItems,
              payload: allDocs,
            });

          },
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`,
        }
      );
      return selectedFile;
    } catch (error) {
      console.log('error', error.response);
      let err = error.response.data;
      selectedFile.uploadStatus = 'failed';
      selectedFile.notAllowedReason = 'Failed';
      selectedFile.failedReason = err.Message ? err.Message : err;
      currentSelected.files = [...currentSelected?.files, selectedFile]

      dispatchProgress({
        type: DocumentActionsType.SetDocumentItems,
        payload: currentSelected
      });
      console.log("-------------->Upload errors------------>", error);
      return selectedFile;
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

    let url = Endpoints.WorkBench.GET.list(
      DocumentActions.loanApplicationId
    );
    try {
      let res: AxiosResponse<DocumentRequest[]> = await Http.get<DocumentRequest[]>(url);
      if (res) {

        let docItems = res.data;
        if (importedFileIds && importedFileIds.length) {
          importedFileIds.forEach(file => {

            docItems = docItems.filter((f: any) => f.fileId !== file.fromFileId)

          });
        }
        dispatch({ type: DocumentActionsType.SetWorkbenchItems, payload: docItems });
      }
      return res.data;
    } catch (error) {
      console.log(error);
    }

  }

  static async getTrashedDocuments(dispatch: Function, importedFileIds: any) {
    let url = Endpoints.Trash.GET.trash.list(
      DocumentActions.loanApplicationId
    );
    try {
      let res: AxiosResponse<TrashItem[]> = await Http.get<TrashItem[]>(url);

      if (res) {
        let docItems = res.data;
        if (importedFileIds && importedFileIds.length) {
          importedFileIds.forEach(file => {

            docItems = docItems.filter((f: any) => f.fileId !== file.fromFileId)
          });
        }

        dispatch({ type: DocumentActionsType.SetTrashedDoc, payload: docItems });
      }

      return res.data;
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

    let url = Endpoints.Document.POST.moveFromCategoryToWorkBench();
    return this.moveFileToWorkbenchLocal(url, body);

  }

  static async moveFileToWorkbenchLocal(url: any, body: any) {
    try {
      let res: AxiosResponse = await Http.post(
        url,
        body
      );
      return res;
    } catch (error) {
      console.log(error);
    }

  }

  static async moveFromWorkBenchToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
    let url = Endpoints.Document.POST.moveFromWorkBenchToCategory();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id,
          toRequestId,
          toDocId,
          fromFileId
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }


  static async moveFromTrashToCategory(id: string, toRequestId: string, toDocId: string, fromFileId: string) {
    let url = Endpoints.Document.POST.moveFromTrashToCategory();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id,
          toRequestId,
          toDocId,
          fromFileId
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async moveCatFileToTrash(id: string, requestId: string, docId: string, fileId: string, cancelCurrentFileViewRequest: boolean) {



    if (cancelCurrentFileViewRequest) {
      await DocumentActions.documentViewCancelToken.cancel();
    }

    let url = Endpoints.Trash.POST.moveCatFileToTrash();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fromRequestId: requestId,
          fromDocId: docId,
          fromFileId: fileId
        },
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
    return false;
  }

  static async moveWorkBenchFileToTrash(id: string, fileId: string, cancelCurrentFileViewRequest: boolean) {

    if (cancelCurrentFileViewRequest) {
      try {
        await DocumentActions.documentViewCancelToken.cancel();
      }
      catch (error) {
        console.log(error)
      }
    }

    let url = Endpoints.Trash.POST.moveWorkBenchFileToTrash();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fromFileId: fileId
        }
      );
      return true;
    } catch (error) {
      console.log(error);
    }
    return false;
  }

  static async moveTrashFileToWorkBench(id: string, fileId: string) {

    let url = Endpoints.Trash.POST.moveTrashFileToWorkBench();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fromFileId: fileId
        }
      );
      return true;
    } catch (error) {
      console.log(error);
    }
  }

  static async DeleteCategoryFile(fileData: any) {
    let { id, fromRequestId, fromDocId, fromFileId } = fileData;

    let url = Endpoints.Document.POST.DeleteCategoryFile();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          requestId: fromRequestId,
          docId: fromDocId,
          fileId: fromFileId,
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }
  static async DeleteTrashFile(id: string, fileId: string) {

    let url = Endpoints.Document.POST.DeleteTrashFile();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fileId: fileId
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async DeleteWorkbenchFile(id: string, fileId: string) {

    let url = Endpoints.Document.POST.DeleteWorkbenchFile();
    try {
      let res: AxiosResponse = await Http.post(
        url,
        {
          id: id,
          fileId: fileId
        }
      );
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  static async SaveCategoryDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    let selectedFile = new DocumentFile(
      document.fileId,
      FileUpload.removeSpecialChars(file.name),
      FileUpload.todayDate(),
      0,
      0,
      FileUpload.getDocLogo(file, "slash"),
      file,
      "pending",

    );

    try {
      let { id, requestId, docId, fileId } = document
      // const file = new Blob([buffer], { type: "application/pdf" });
      const formData = new FormData();
      formData.append('id', id)
      formData.append('requestId', requestId);
      formData.append('docId', docId);
      formData.append('fileId', fileId);
      formData.append('file', file);

      if (fileId === DocumentActions.nonExistentFileId) {
        let files = [selectedFile, ...currentDoc.files]
        currentDoc.files = files
      }
      let res = await Http.fetch(
        {
          method: Http.methods.POST,
          url: Http.createUrl(
            Http.baseUrl,
            Endpoints.Document.POST.saveCategoryDocument()
          ),
          // cancelToken: file.uploadReqCancelToken.token,
          data: formData,
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            selectedFile.uploadProgress = p;
            if (p === 100) {
              selectedFile.uploadStatus = "done";
              dispatchProgress({
                type: ViewerActionsType.SetIsSaving,
                payload: false,
              });

            }

            const docFiles = currentDoc?.files?.map((docFile: any) => {
              if (docFile.id === document?.fileId) {
                docFile = selectedFile
              }
              return docFile;
            })
            currentDoc.files = docFiles;
            dispatchProgress({
              type: DocumentActionsType.UpdateDocFile,
              payload: currentDoc
            });

            dispatchProgress({
              type: ViewerActionsType.SetFileProgress,
              payload: p,
            });
          },
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`,
        }
      );

      return res.data;
    } catch (error) {
      console.log('error', error.response);
      let err = error.response.data;
      selectedFile.uploadStatus = 'failed';
      selectedFile.notAllowedReason = 'Failed';
      selectedFile.failedReason = err.Message ? err.Message : err;
      console.log("-------------->Upload errors------------>", error);
    }
  }

  static async SaveTrashDocument(document: any, file: File, dispatchProgress: Function, currentDoc: any) {
    let selectedFile = new DocumentFile(
      "",
      FileUpload.removeSpecialChars(file.name),
      FileUpload.todayDate(),
      0,
      0,
      FileUpload.getDocLogo(file, "slash"),
      file,
      "pending",

    );
    try {

      // const file = new Blob([buffer], { type: "application/pdf" });
      const formData = new FormData();
      formData.append('id', document.id)
      formData.append('fileId', "000000000000000000000000");
      formData.append('file', file);


      let files = [selectedFile, ...currentDoc]
      dispatchProgress({
        type: DocumentActionsType.AddFileToTrash,
        payload: files
      });

      let res = await Http.fetch(
        {
          method: Http.methods.POST,
          url: Http.createUrl(
            Http.baseUrl,
            Endpoints.Document.POST.saveTrashDocument()
          ),
          // cancelToken: file.uploadReqCancelToken.token,
          data: formData,
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            selectedFile.uploadProgress = p;
            if (p === 100) {
              selectedFile.uploadStatus = "done";
              dispatchProgress({
                type: ViewerActionsType.SetIsSaving,
                payload: false,
              });

            }
            const docFiles = files?.map((docFile: any) => {
              if (docFile.fileId === DocumentActions.nonExistentFileId) {
                docFile = selectedFile
              }
              return docFile;
            })

            dispatchProgress({
              type: DocumentActionsType.AddFileToTrash,
              payload: docFiles
            });

            dispatchProgress({
              type: ViewerActionsType.SetFileProgress,
              payload: p,
            });
          },
        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`,
        }
      );

      return res.data;
    } catch (error) {
      console.log('error', error.response);

      selectedFile.uploadStatus = 'failed';
      selectedFile.notAllowedReason = 'Failed';
      selectedFile.failedReason = error.Message ? error.Message : error;
      dispatchProgress({
        type: DocumentActionsType.AddFileToTrash,
        payload: selectedFile
      });
      console.log("-------------->Upload errors------------>", error);
    }
  }

  static async SaveWorkbenchDocument(fileObj: any, file: File, dispatchProgress: Function, currentDoc: any) {
    let selectedFile = new DocumentFile(
      "",
      FileUpload.removeSpecialChars(file.name),
      FileUpload.todayDate(),
      0,
      0,
      FileUpload.getDocLogo(file, "slash"),
      file,
      "pending",

    );
    selectedFile = await Rename.rename(currentDoc, selectedFile);
    try {
      let { id, fileId } = fileObj
      const formData = new FormData();
      formData.append('id', id)
      formData.append('fileId', fileId);
      

      if (selectedFile.file) {
        formData.append("file", selectedFile.file, `${selectedFile.clientName}`);
      }

      let files: any = currentDoc;
      if (fileId === DocumentActions.nonExistentFileId) {
        files = [selectedFile, ...currentDoc]

      }
      let res = await Http.fetch(
        {
          method: Http.methods.POST,
          url: Http.createUrl(
            Http.baseUrl,
            Endpoints.Document.POST.saveWorkbenchDocument()
          ),

          // cancelToken: file.uploadReqCancelToken.token,
          data: formData,
          onUploadProgress: (e) => {
            let p = Math.floor((e.loaded / e.total) * 100);
            selectedFile.uploadProgress = p;
            if (p === 100) {
              selectedFile.uploadStatus = "done";
              dispatchProgress({
                type: ViewerActionsType.SetIsSaving,
                payload: false,
              });

            }

            const docFiles = files?.map((docFile: any) => {
              if (docFile.fileId === fileId) {
                docFile = selectedFile
              }
              return docFile;
            })
            dispatchProgress({
              type: DocumentActionsType.AddFileToWorkbench,
              payload: docFiles
            });
            dispatchProgress({
              type: ViewerActionsType.SetFileProgress,
              payload: p,
            });
          }

        },
        {
          Authorization: `Bearer ${LocalDB.getAuthToken()}`,
        }
      );

      return res.data;
    } catch (error) {
      console.log('error', error.response);
      selectedFile.uploadStatus = 'failed';
      selectedFile.notAllowedReason = 'Failed';
      selectedFile.failedReason = error.Message ? error.Message : error;
      dispatchProgress({
        type: DocumentActionsType.AddFileToWorkbench,
        payload: selectedFile
      });
      console.log("-------------->Upload errors------------>", error);
    }
  }


  static async fetchFile(path: any) {

    let f = await fetch(path).then(r => r.blob());
    return f;
  }

  static async getLoanApplicationDetail(loanApplicationId: string) {
    try {
      let result: any = await Http.get<any>(Endpoints.Document.GET.loanInfo(loanApplicationId));
      return result;
    } catch (error) {
      console.log(error);
    }
  }
 
  static async getLoanApplicationId(loanApplicationId: string) {
    try {
      let result: any = await Http.get<any>(Endpoints.Document.GET.loanApplicationId(loanApplicationId));
      return result;
    } catch (error) {
      console.log(error);
    }
  }


  static async syncFileToLos(fileData: any) {

    let url = Endpoints.Document.POST.syncToLOS();
    try {
      let res: any = await Http.post(url, fileData);
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
    let url = Endpoints.Document.GET.checkIsByteProAuto();
    try {
      let res: any = await Http.get(url);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }


}