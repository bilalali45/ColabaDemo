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

export const Sync = "Synchronized";
export const SyncTxt = "Synced";
export const notSync = "Not synchronized";
export const notSyncTxt = "Not Synced";
export const SyncError = "Error";
export const SyncErrorTxt = "Sync failed";
export const ReadyToSync = "Ready to Sync";
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
  const currentTemplate: Template[] = templateManager?.currentTemplate;
  const isDraftStore: boolean = needListManager?.isDraft;
  const templateIds: boolean = needListManager?.templateIds;
  const isByteProAuto: boolean = needListManager?.isByteProAuto;
  const [deleteRequestSent, setDeleteRequestSent] = useState<boolean>(false);
  const [showConfirmBox, setShowConfirmBox] = useState<boolean>(false);
  const [synchronizing, setSynchronizing] = useState<boolean>(false);
  const [showFailedToSyncBox, setShowFailedToSyncBox] = useState<boolean>(false);
  const [syncSuccess, setsyncSuccess] = useState<boolean>(false);
  const [syncTitleClass, setSyncTitleClass] = useState<string>('not_Synced');

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
          let data = await updateNeedListArray(res)
          dispatch({
            type: NeedListActionsType.SetNeedListTableDATA,
            payload: data
          });
          return data;
        }
      
      }
    }
  };

  const updateNeedListArray = async (arr: NeedList[]) => {
     for(let i = 0; i < arr.length; i++){
        for(let k = 0; k < arr[i].files.length; k++){
          if(arr[i].files[k].byteProStatus === Sync){

            arr[i].files[k].byteProStatusText = SyncTxt;
            arr[i].files[k].byteProStatusClassName = "synced";

          }else if(arr[i].files[k].byteProStatus === notSync){

            arr[i].files[k].byteProStatusText = notSyncTxt
            arr[i].files[k].byteProStatusClassName = "not_Synced";

          }else{
            arr[i].files[k].byteProStatusText = SyncErrorTxt
            arr[i].files[k].byteProStatusClassName = "sync_error";
            arr[i].files[k].byteProStatus = SyncError;
          }
        }
     }
     return arr;
  }

  const updateSyncStatusToReady = async (arr: NeedList[], id?: string) => {
   
    for(let i = 0; i < arr.length; i++){
      for(let k = 0; k < arr[i].files.length; k++){
        if(id != undefined && id === arr[i].files[k].id && arr[i].files[k].byteProStatusText != SyncTxt){
          arr[i].files[k].byteProStatusText = ReadyToSync;
          arr[i].files[k].byteProStatus = ReadyToSync;
          arr[i].files[k].byteProStatusClassName = "readyto_Sync";
          break;
        }else if(id === undefined && arr[i].files[k].byteProStatus === notSync){
          arr[i].files[k].byteProStatusText = ReadyToSync;
          arr[i].files[k].byteProStatus = ReadyToSync;
          arr[i].files[k].byteProStatusClassName = "readyto_Sync";
        }
      }
   }
   return arr;
  }

  const updateSyncStatusToSynchronizing = async (synAgain: boolean) => {
    let arr = needListData;
    let chkVar = synAgain ? "sync failed" : "Ready to Sync";
    for(let i = 0; i < arr.length; i++){
      for(let k = 0; k < arr[i].files.length; k++){
      if( arr[i].files[k].byteProStatus === chkVar){
          arr[i].files[k].byteProStatusText = Synchronizing;
          arr[i].files[k].byteProStatus = Synchronizing;
          arr[i].files[k].byteProStatusClassName = "readyto_Sync";
        }
      }
   }
   return arr;
  }

  const FilesSyncToLosHandler = async () => {
   
    let data = await updateSyncStatusToReady(needListData)
    dispatch({
      type: NeedListActionsType.SetNeedListTableDATA,
      payload: data
    });
    setShowConfirmBox(true)
    setSyncTitleClass('readyto_Sync')
  }

  const FileSyncToLosHandler = async (id: string, txt: string) => {
   let data = await updateSyncStatusToReady(needListData, id)
   dispatch({
    type: NeedListActionsType.SetNeedListTableDATA,
    payload: data
  });
  if(txt != 'Synced'){
    setShowConfirmBox(true)
  }
   
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
   for(let i = 0; i < arr.length; i++){
     for(let k = 0; k < arr[i].files.length; k++){
     if( arr[i].files[k].byteProStatus === "Synchronizing"){
       let sync = await filePostToBytePro(
         loanApplicationId,
         arr[i].id,
         arr[i].requestId,
         arr[i].docId,
         arr[i].files[k].id
         )
         if(sync === 200){
          arr[i].files[k].byteProStatusText = 'Synced';
          arr[i].files[k].byteProStatus = 'Synchronized';
          arr[i].files[k].byteProStatusClassName = "synced";
         }else{
          arr[i].files[k].byteProStatusText = 'Sync failed';
          arr[i].files[k].byteProStatus = 'sync failed';
          arr[i].files[k].byteProStatusClassName = "sync_error";
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
    console.log('checkIsByteProAuto', res.syncToBytePro)
    let isAuto = res.syncToBytePro != 2 ? true : false;
    dispatch({type: NeedListActionsType.SetIsByteProAuto, payload: false})
  }

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
          console.log('sortArrow!!',sortArrow)
          console.log('sortStatusArrow!!',sortStatusArrow)
          console.log('docSort!!',docSort)
          console.log('statusSort!!',statusSort)
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
    {
    showFailedToSyncBox &&  <NeedListAlertBox showFailedToSyncBox = {showFailedToSyncBox} needList = {needListData} syncAgain = {syncAgain} handleClose = {handleClose} />
     } 
    </div>
  );
};
