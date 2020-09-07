import React, {useEffect, useState, useContext} from 'react';
import {useHistory} from 'react-router-dom';
import {NeedListViewHeader} from './NeedListViewHeader/NeedListViewHeader';
import {NeedListTable} from './NeedListTable/NeedListTable';
import {NeedList} from '../../../../Entities/Models/NeedList';
import {NeedListActions} from '../../../../Store/actions/NeedListActions';
import {LocalDB} from '../../../../Utils/LocalDB';
import {Store} from '../../../../Store/Store';
import {NeedListActionsType} from '../../../../Store/reducers/NeedListReducer';
import {sortList} from '../../../../Utils/helpers/Sort';
import {Template} from '../../../../Entities/Models/Template';
import {TemplateActions} from '../../../../Store/actions/TemplateActions';
import {TemplateActionsType} from '../../../../Store/reducers/TemplatesReducer';
import { NeedListAlertBox } from "../NeedListView/NeedListAlertBox/NeedListAlertBox";
import { findIndex } from 'lodash';
import { NeedListDocuments } from '../../../../Entities/Models/NeedListDocuments';

export const Sync = "Synchronized";
export const SyncTxt = "Synced";
export const SyncClass = "synced";
export const notSync = "Not synchronized";
export const notSyncTxt = "Not Synced";
export const notSyncClass = "not_Synced";
export const SyncError = "Error";
export const SyncErrorTxt = "Sync failed";
export const SyncErrorClass = "sync_error";
export const ReadyToSync = "Ready to Sync";
export const ReadyToSyncClass = "readyto_Sync";
export const Synchronizing = "Synchronizing";

export const NeedListView = () => {

  const [toggle, setToggle] = useState(true);
  const [sortArrow, setSortArrow] = useState('desc');
  const [sortStatusArrow, setStatusSortArrow] = useState('desc');
  const [docSort, setDocSort] = useState(false);
  const [statusSort, setStatusSort] = useState(false);

  const {state, dispatch} = useContext(Store);
  const history = useHistory();

  const needListManager: any = state?.needListManager;
  const needListData = needListManager?.needList;
  const templateManager: any = state.templateManager;
  const templates: Template[] = templateManager?.templates;
  const isDocumentDraft = templateManager?.isDocumentDraft;
  const isByteProAuto: boolean = needListManager?.isByteProAuto;
  const [deleteRequestSent, setDeleteRequestSent] = useState<boolean>(false);
  const [showConfirmBox, setShowConfirmBox] = useState<boolean>(false);
  const [synchronizing, setSynchronizing] = useState<boolean>(false);
  const [showFailedToSyncBox, setShowFailedToSyncBox] = useState<boolean>(false);
  const [syncSuccess, setsyncSuccess] = useState<boolean>(false);
  const [syncTitleClass, setSyncTitleClass] = useState<string>(notSyncClass);

  var isError = false;

  useEffect(() => {
    fetchNeedList(true, true);
    checkIsDocumentDraft(LocalDB.getLoanAppliationId());
    checkIsByteProAuto();
  }, []);

  useEffect(() => {
    if (templates && templates?.length) {
      dispatch({
        type: TemplateActionsType.SetCurrentTemplate,
        payload: templates[0]
      });
    }
  }, [templates?.length]);

  const fetchNeedList = async (status: boolean, fetchNew: boolean) => {
    if (LocalDB.getLoanAppliationId()) {
      if (fetchNew) {
        let res: NeedList[] | undefined = await NeedListActions.getNeedList(
          LocalDB.getLoanAppliationId(),
          status
        );
        if(res){
          let data = await updateNeedListArray(res);
          dispatch({
            type: NeedListActionsType.SetNeedListTableDATA,
            payload: data.map((d, i) => {
              return {
                ...d,
                index: i,
                files: d.files.map((f, i) => {
                  return {
                    ...f,
                    index: i
                  }
                })
              }
            })
          });
          return data;
        }
      
      }
    }
  };

  const updateNeedListArray = async (arr: NeedList[]) => {
    for (const doc of arr) {
      for (const file of doc.files) {
        if(file.byteProStatus === Sync){
          file.byteProStatusText = SyncTxt;
          file.byteProStatusClassName = SyncClass;
        }else if(file.byteProStatus === notSync){
          file.byteProStatusText = notSyncTxt
          file.byteProStatusClassName = notSyncClass;
        }else{
          file.byteProStatusText = SyncErrorTxt
          file.byteProStatusClassName = SyncErrorClass;
          file.byteProStatus = SyncError;
        }
      }
    }
     return arr;
  }

  const syncAll = (arr: NeedList[]) => {
    for (const doc of arr) {
      for (const file of doc.files) {
        syncSingle(file)
      }
    }
    return arr;
  }

  const syncSingle = async (file: any) => {
    updateSyncStatusToReady(file);
  }

  const updateSyncStatusToReady = async (file: NeedListDocuments) => {
    if(file.byteProStatusText != SyncTxt || file.byteProStatus === notSync){
      file.byteProStatusText = ReadyToSync;
      file.byteProStatus = ReadyToSync;
      file.byteProStatusClassName = ReadyToSyncClass;
    }
    return file;
  }
  
  const updateSyncStatusToNotSync = async (arr: NeedList[]) => {
     for (const doc of arr) {
      for (const file of doc.files) {
        if(file.byteProStatus === ReadyToSync){
          file.byteProStatusText = notSyncTxt;
          file.byteProStatus = notSync;
          file.byteProStatusClassName = notSyncClass;
        }
      }
     }
     return arr;
  }

  const updateSyncStatusToSynchronizing = async (synAgain: boolean) => {
    let chkVar = synAgain ? "sync failed" : "Ready to Sync";
    for (const doc of needListData) {
      for (const file of doc.files) {
        if(file.byteProStatus === chkVar){
          file.byteProStatusText = Synchronizing;
          file.byteProStatus = Synchronizing;
          file.byteProStatusClassName = ReadyToSyncClass;
        }
      }
     }
   return needListData;
  }

  const FilesSyncToLosHandler = async (className: string) => {
    if(className === ReadyToSyncClass){
      let data = await updateSyncStatusToNotSync(needListData);
      dispatch({
        type: NeedListActionsType.SetNeedListTableDATA,
        payload: data
      });
      setSyncTitleClass(notSyncClass)
      setShowConfirmBox(false)
      return;
    }
   
    let data = await syncAll(needListData)
    dispatch({
      type: NeedListActionsType.SetNeedListTableDATA,
      payload: data
    });
    setShowConfirmBox(true)
    setSyncTitleClass(ReadyToSyncClass)
  }

  const FileSyncToLosHandler = async (file : NeedListDocuments) => {
    let txt = file.byteProStatusText;
    let id = file.id;
    if(txt === SyncTxt) return;
    if(txt === ReadyToSync){
      for (const doc of needListData) {
        for (const file of doc.files) {
          if(id === file.id){
            file.byteProStatusText = notSyncTxt;
            file.byteProStatus = notSync;
            file.byteProStatusClassName = notSyncClass;
        }
      }
    }
     dispatch({
      type: NeedListActionsType.SetNeedListTableDATA,
      payload: needListData
    });

     let isSelected = checkIsAnyItemSelected();
    if( isSelected === 0){
      setsyncSuccess(false)
      setShowConfirmBox(false)
      setSyncTitleClass(notSyncClass)
      return;
    }
     return ;
    }

   await syncSingle(file)
  //  let data = await updateSyncStatusToReady(needListData, id, docIndex, fileIndex)
   dispatch({
    type: NeedListActionsType.SetNeedListTableDATA,
    payload: needListData
  });
  if(txt != SyncTxt){
    setsyncSuccess(false)
    setShowConfirmBox(true)
  }
  }

  const checkIsAnyItemSelected = () => {
    let count = 0;
    for (const doc of needListData) {
      for (const file of doc.files) {
        if(file.byteProStatus === ReadyToSync){
                  count++;
                  return count;
                }
      }
    }
   return count;
  }

  const postToByteProHandler = async (synAgain: boolean) => { 
    let loanApplicationId = parseInt(LocalDB.getLoanAppliationId());
    let data = await updateSyncStatusToSynchronizing(synAgain);
    dispatch({
    type: NeedListActionsType.SetNeedListTableDATA,
    payload: data
    });
    setSynchronizing(true);
   
   let arr = needListData;

   for (const doc of needListData) {
     for (const file of doc.files) {
      if(file.byteProStatus === Synchronizing){
        let sync = await filePostToBytePro(
          loanApplicationId,
          doc.id,
          doc.requestId,
          doc.docId,
          file.id
          )
          if(sync === 200){
            file.byteProStatusText = SyncTxt;
            file.byteProStatus = Sync;
            file.byteProStatusClassName = SyncClass;
          }else{
            file.byteProStatusText = SyncErrorTxt;
            file.byteProStatus = 'sync failed';
            file.byteProStatusClassName = SyncErrorClass;
            isError = true;
          }
          dispatch({
           type: NeedListActionsType.SetNeedListTableDATA,
           payload: arr
           });
        }
     }
   }

  setSynchronizing(false);
  setShowConfirmBox(false);
  if(isError) {
    setShowFailedToSyncBox(true);
    setSyncTitleClass('sync_error')
  }else{
    setsyncSuccess(true)
    setSyncTitleClass('synced')
   }
  }

  const filePostToBytePro = async (
    LoanApplicationId: number,
    DocumentLoanApplicationId: string,
    RequestId: string,
    DocumentId: string,
    FileId: string
    ) => {
    let res = await NeedListActions.fileSyncToLos(
      LoanApplicationId,
      DocumentLoanApplicationId,
      RequestId,
      DocumentId,
      FileId
      )
      return res;
  }

  const checkIsDocumentDraft = async (id: string) => {
    let res: any = await TemplateActions.isDocumentDraft(id);
    dispatch({type: TemplateActionsType.SetIsDocumentDraft, payload: res});
  };

  const checkIsByteProAuto = async () => {
    let res: any = await NeedListActions.checkIsByteProAuto();
    let isAuto = res.syncToBytePro != 2 ? true : false;
    dispatch({type: NeedListActionsType.SetIsByteProAuto, payload: false})
  }
//new comment
  const deleteNeedListDoc = async (
    id: string,
    requestId: string,
    docId: string
  ) => {
    if (id && requestId && docId) {
      let res = await NeedListActions.deleteNeedListDocument(
        id,
        requestId,
        docId
      );
      if (res === 200) {
        fetchNeedList(toggle, true).then((data) => {
          let sortedList ;
          if(docSort){
             sortedList = sortList(
              data,
              "docName",
              sortArrow === 'asc' ? true : false
            );
          } else if(statusSort){
             sortedList = sortList(
              data,
              "status",
              sortStatusArrow === 'asc' ? true : false
            );
          }else{
            sortedList = data;
          }
          dispatch({
            type: NeedListActionsType.SetNeedListTableDATA,
            payload: sortedList
          });
          setDeleteRequestSent(false);
        });
      }
    }
  };

  const togglerHandler = (pending: boolean) => {
    setShowConfirmBox(false)
    if (pending) {
      fetchNeedList(pending, true).then((data) => {
        dispatch({
          type: NeedListActionsType.SetNeedListTableDATA,
          payload: data
        });
        setToggle(true);
        setDocSort(false)
        setStatusSort(false)
        setSortArrow('desc')
        setStatusSortArrow('desc')
      });
    } else {
      fetchNeedList(pending, true).then((data) => {
        dispatch({
          type: NeedListActionsType.SetNeedListTableDATA,
          payload: data
        });
        setToggle(false);
        setDocSort(false)
        setStatusSort(false)
        setSortArrow('desc')
        setStatusSortArrow('desc')
      });
    }
  };

  const sortDocumentTitleHandler = () => {
    setDocSort(true);
    setStatusSort(false)
    setStatusSortArrow('desc')
    if (sortArrow === 'asc') {
      let sortedList = sortList(
        needListData,
        "docName",
        false
      );
    
      dispatch({
        type: NeedListActionsType.SetNeedListTableDATA,
        payload: sortedList
      });
      setSortArrow('desc');
    } else {
      setSortArrow('asc');
      let sortedList = sortList(
        needListData,
        "docName",
        true
      );
      dispatch({
        type: NeedListActionsType.SetNeedListTableDATA,
        payload: sortedList
      });
    }
  };

  const sortStatusTitleHandler = () => {
    setStatusSort(true);
    setDocSort(false)
    setSortArrow('desc')
    if (sortStatusArrow === 'asc') {
      let sortedList = sortList(
        needListData,
        "status",
        false
      );
      dispatch({
        type: NeedListActionsType.SetNeedListTableDATA,
        payload: sortedList
      });
      setStatusSortArrow('desc');
    } else {
      setStatusSortArrow('asc');
      let sortedList = sortList(
        needListData,
        "status",
        true
      );
      dispatch({
        type: NeedListActionsType.SetNeedListTableDATA,
        payload: sortedList
      });
    }
  };

  const addTemplatesDocuments = (idArray: string[]) => {
    dispatch({type: NeedListActionsType.SetTemplateIds, payload: idArray});
    history.push(`/newNeedList/${LocalDB.getLoanAppliationId()}`);
  };

  const viewSaveDraftHandler = () => {
    dispatch({type: NeedListActionsType.SetIsDraft, payload: true});
    history.push(`/newNeedList/${LocalDB.getLoanAppliationId()}`);
  };

  const deleteClickHandler = (id: string, requestId: string, docId: string) => {
    setDeleteRequestSent(true);
    deleteNeedListDoc(id, requestId, docId);
  };

  const syncAgain = () => {
    postToByteProHandler(true);
    setShowFailedToSyncBox(false);
    setShowConfirmBox(true)
  }

  const handleClose = () => {
    setShowFailedToSyncBox(false)
    updateNeedListArray(needListData)
  }

  const closeSyncCompletedBoxhandler = () => {
    setsyncSuccess(false);
    setShowConfirmBox(false);
  }

  return (
    <div className="need-list-view">
      <NeedListViewHeader
        toggleCallBack={togglerHandler}
        templateList={templates}
        addTemplatesDocuments={addTemplatesDocuments}
        isDocumentDraft={isDocumentDraft}
        viewSaveDraft={viewSaveDraftHandler}
      />
      <NeedListTable
        needList={needListData}
        deleteDocument={deleteClickHandler}
        sortDocumentTitle={sortDocumentTitleHandler}
        documentTitleArrow={sortArrow}
        sortStatusTitle={sortStatusTitleHandler}
        statusTitleArrow={sortStatusArrow}
        documentSortClick={docSort}
        statusSortClick={statusSort}
        deleteRequestSent={deleteRequestSent}
        isByteProAuto = {isByteProAuto}
        FilesSyncToLos = {FilesSyncToLosHandler}
        showConfirmBox = {showConfirmBox}
        FileSyncToLos = {FileSyncToLosHandler}
        postToBytePro = {postToByteProHandler}
        synchronizing = {synchronizing}
        syncSuccess = {syncSuccess}
        closeSyncCompletedBox = {closeSyncCompletedBoxhandler}
        syncTitleClass = {syncTitleClass}
      />
    {showFailedToSyncBox &&  <NeedListAlertBox showFailedToSyncBox = {showFailedToSyncBox} needList = {needListData} syncAgain = {syncAgain} handleClose = {handleClose} />
     } 
    </div>
  );
};
