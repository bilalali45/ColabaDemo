import React, {useEffect, useState, useContext} from 'react';
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
import {useHistory} from 'react-router-dom';

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
  const [deleteRequestSent, setDeleteRequestSent] = useState<boolean>(false);

  useEffect(() => {
    fetchNeedList(true, true);
    checkIsDocumentDraft(LocalDB.getLoanAppliationId());
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
        let res: NeedList | undefined = await NeedListActions.getNeedList(
          LocalDB.getLoanAppliationId(),
          status
        );
        dispatch({
          type: NeedListActionsType.SetNeedListTableDATA,
          payload: res
        });
        if (res) {
          return res;
        }
      }
    }
  };
  const checkIsDocumentDraft = async (id: string) => {
    let res: any = await TemplateActions.isDocumentDraft(id);
    dispatch({type: TemplateActionsType.SetIsDocumentDraft, payload: res});
  };

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
      />
    </div>
  );
};
