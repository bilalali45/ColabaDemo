import React, { useEffect, useState, useContext } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'
import { LocalDB } from '../../../../Utils/LocalDB'
import { Store } from '../../../../Store/Store'
import { NeedListActionsType } from '../../../../Store/reducers/NeedListReducer';
import { sortList } from '../../../../Utils/helpers/Sort'
import { Template } from '../../../../Entities/Models/Template'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer'

export const NeedListView = () => {

    const [toggle, setToggle] = useState(false);
    const [sortArrow, setSortArrow] = useState('desc')
    const [sortStatusArrow, setStatusSortArrow] = useState('desc')
    const [docSort, setDocSort] = useState(false);
    const [statusSort, setStatusSort] = useState(false);

    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const needListData = needListManager?.needList;
    const templateManager: any = state.templateManager;
    const templates: Template[] = templateManager?.templates;

    useEffect(() => {
        fetchNeedList(true, true);
        if (!templates) {
            fetchTemplatesList();
        }
    }, [])

    const fetchNeedList = async (status: boolean, fetchNew: boolean) => {
        if (LocalDB.getLoanAppliationId() && LocalDB.getTenantId()) {
            if (fetchNew) {
                let res: NeedList | undefined = await NeedListActions.getNeedList(LocalDB.getLoanAppliationId(), LocalDB.getTenantId(), status);
                dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: res })
                if (res) {
                    return res
                }
            }
        }
    }

    const fetchTemplatesList = async () => {
        let newTemplates: any = await TemplateActions.fetchTemplates(LocalDB.getTenantId());
        if (newTemplates) {
            dispatch({ type: TemplateActionsType.SetTemplates, payload: newTemplates });
        }
    }

    const deleteNeedListDoc = async (id: string, requestId: string, docId: string) => {
        let tenentId = LocalDB.getTenantId();
        if (id && requestId && docId && tenentId) {
            let res = await NeedListActions.deleteNeedListDocument(id, requestId, docId, parseInt(tenentId))
            if (res === 200) {
                fetchNeedList(toggle, true).then((data) => {
                    let sortedList = sortList(data, docSort, sortArrow === 'asc' ? true : false, statusSort, sortStatusArrow === 'asc' ? true : false);
                    dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })                     
                    })
            }
        }
    }

    const togglerHandler = (pending: boolean) => {
        if (!pending) {
            fetchNeedList(!pending, true).then((data) => {
                let sortedList = sortList(data, docSort, sortArrow === 'asc' ? true : false, statusSort, sortStatusArrow === 'asc' ? true : false);
                dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
                setToggle(!toggle)              
            })
        } else {
            fetchNeedList(!pending, true).then((data) => {
                let sortedList = sortList(data, docSort, sortArrow === 'asc' ? true : false, statusSort, sortStatusArrow === 'asc' ? true : false);
                dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
                setToggle(!toggle)            
            })
        }
    }

    const deleteClickHandler = (id: string, requestId: string, docId: string) => {
        deleteNeedListDoc(id, requestId, docId);
    }

    const sortDocumentTitleHandler = () => {
        setDocSort(true);
        if (sortArrow === 'asc') {         
            let sortedList = sortList(needListData, true, false, statusSort, sortStatusArrow === 'asc' ? true : false);
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
            setSortArrow('desc')
        } else {
            setSortArrow('asc')       
            let sortedList = sortList(needListData, true, true, statusSort, sortStatusArrow === 'asc' ? true : false);
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
        }    
    }

    const sortStatusTitleHandler = () => {
        setStatusSort(true);
        if (sortStatusArrow === 'asc') {          
            let sortedList = sortList(needListData, docSort, sortArrow === 'asc' ? true : false, true, false);
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
            setStatusSortArrow('desc')
        } else {
            setStatusSortArrow('asc')         
            let sortedList = sortList(needListData, docSort, sortArrow === 'asc' ? true : false, true, true);
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
        }     
    }

    

    return (
        <div className="need-list-view">
            <NeedListViewHeader
                toggleCallBack={togglerHandler}
                templateList = {templates}
            />
            <NeedListTable
                needList={needListData}
                deleteDocument={deleteClickHandler}
                sortDocumentTitle={sortDocumentTitleHandler}
                documentTitleArrow={sortArrow}
                sortStatusTitle={sortStatusTitleHandler}
                statusTitleArrow={sortStatusArrow}
                documentSortClick = {docSort}
                statusSortClick = {statusSort}
            />
        </div>
    )
}
