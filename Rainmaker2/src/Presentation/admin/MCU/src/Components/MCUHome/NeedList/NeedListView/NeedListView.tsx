import React, { useEffect, useState, useContext } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'
import { LocalDB } from '../../../../Utils/LocalDB'
import { Store } from '../../../../Store/Store'
import { NeedListActionsType } from '../../../../Store/reducers/NeedListReducer';
import { sortList } from '../../../../Utils/helpers/Sort'

export const NeedListView = () => {

   // const [needList, setNeedList] = useState<NeedList | null>();
    const [toggle, setToggle] = useState(false);
    const [sortArrow, setSortArrow] = useState('desc')
    const [sortStatusArrow, setStatusSortArrow] = useState('desc')
    const [lastFilter, setLastFilter] = useState(0);
    const { state, dispatch } = useContext(Store);

    const needListManager: any = state?.needListManager;
    const needListData = needListManager?.needList;

    useEffect(() => {
        fetchNeedList(true, true);
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

    const deleteNeedListDoc = async (id: string, requestId: string, docId: string) => {
        let tenentId = LocalDB.getTenantId();
        if (id && requestId && docId && tenentId) {
            let res = await NeedListActions.deleteNeedListDocument(id, requestId, docId, parseInt(tenentId))
            if (res === 200) {
                if (lastFilter === 1) {
                    fetchNeedList(toggle, true).then((data) => {
                        let sortedList = sortList(data, sortArrow, 'docName');
                        dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })                     
                    })
                } else if (lastFilter === 2) {
                    fetchNeedList(toggle, true).then((data) => {
                        let sortedList = sortList(data, sortStatusArrow, 'status');
                        dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })                      
                    })
                } else {
                    fetchNeedList(toggle, true).then((data) => {
                        dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: data })
                    })
                }
            }
        }
    }

    const togglerHandler = (pending: boolean) => {
        if (!pending) {
            fetchNeedList(!pending, true).then((data) => {
                dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: data })
                setToggle(!toggle)
                setSortArrow('desc')
                setStatusSortArrow('desc')
            })
        } else {
            fetchNeedList(!pending, true).then((data) => {
                dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: data })
                setToggle(!toggle)
                setSortArrow('desc')
                setStatusSortArrow('desc')
            })
        }
    }

    const deleteClickHandler = (id: string, requestId: string, docId: string) => {
        deleteNeedListDoc(id, requestId, docId);
    }

    const sortDocumentTitleHandler = () => {
        if (sortArrow === 'asc') {
            let sortedList = sortList(needListData, 'desc', 'docName');
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
            setSortArrow('desc')
        } else {
            setSortArrow('asc')
            let sortedList = sortList(needListData, 'asc', 'docName');
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
        }
        setLastFilter(1);
    }

    const sortStatusTitleHandler = () => {
        if (sortStatusArrow === 'asc') {
            let sortedList = sortList(needListData, 'desc', 'status');
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
            setStatusSortArrow('desc')
        } else {
            setStatusSortArrow('asc')
            let sortedList = sortList(needListData, 'asc', 'status');
            dispatch({ type: NeedListActionsType.SetNeedListTableDATA, payload: sortedList })
        }
        setLastFilter(2);
    }

    

    return (
        <div className="need-list-view">
            <NeedListViewHeader
                toggleCallBack={togglerHandler}
            />
            <NeedListTable
                needList={needListData}
                deleteDocument={deleteClickHandler}
                sortDocumentTitle={sortDocumentTitleHandler}
                documentTitleArrow={sortArrow}
                sortStatusTitle={sortStatusTitleHandler}
                statusTitleArrow={sortStatusArrow}
            />
        </div>
    )
}
