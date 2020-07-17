import React, { useEffect, useState } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'
import { LocalDB } from '../../../../Utils/LocalDB'

export const NeedListView = () => {

    const[needList, setNeedList] = useState<NeedList | null>();
    const [toggle, setToggle] = useState(false);
    const [sortArrow, setSortArrow] = useState('desc')
    const [sortStatusArrow, setStatusSortArrow] = useState('desc')
    const [lastFilter, setLastFilter] = useState(0);

useEffect(()=> {
    if(!needList){
        fetchNeedList(true).then((data)=> {
            setNeedList(data)
        })
    }
},[needList])

const fetchNeedList = async (status: boolean)=> {
    if(LocalDB.getLoanAppliationId() && LocalDB.getTenantId()){
        let res: NeedList | undefined = await NeedListActions.getNeedList(LocalDB.getLoanAppliationId(), LocalDB.getTenantId(), status) 
        if(res){
            return res
        } 
    }  
}

const deleteNeedListDoc = async (id: string, requestId: string, docId: string) => {
    let tenentId = LocalDB.getTenantId();
    if(id && requestId && docId && tenentId){
      let res = await NeedListActions.deleteNeedListDocument(id, requestId, docId, parseInt(tenentId))
      if(res === 200){
          if(lastFilter === 1){
            fetchNeedList(toggle).then((data) => {
                let sortedList = sortNeedList(data, sortArrow, 'docName');
                setNeedList(sortedList)
            }) 
          }else if(lastFilter === 2){
            fetchNeedList(toggle).then((data) => {
                let sortedList = sortNeedList(data, sortStatusArrow, 'status'); 
                setNeedList(sortedList)
            })  
          }else{
            fetchNeedList(toggle).then((data) => {
                setNeedList(data)
            }) 
          }      
      }
    }
}

const togglerHandler = (pending: boolean) => {
   if(!pending){
    fetchNeedList(!pending).then((data) => {
        setNeedList(data)
        setToggle(!toggle)
        setSortArrow('desc') 
        setStatusSortArrow('desc')
    })  
   }else{
    fetchNeedList(!pending).then((data) => {
        setNeedList(data)
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
  if(sortArrow === 'asc'){
    let sortedList = sortNeedList(needList, 'desc', 'docName');
    setNeedList(sortedList) 
    setSortArrow('desc') 
  }else{
    setSortArrow('asc')
    let sortedList = sortNeedList(needList, 'asc', 'docName');
    setNeedList(sortedList) 
  }
  setLastFilter(1);
}

const sortStatusTitleHandler = () => {
    if(sortStatusArrow === 'asc'){
        let sortedList = sortNeedList(needList, 'desc', 'status');
        setNeedList(sortedList) 
        setStatusSortArrow('desc') 
      }else{
        setStatusSortArrow('asc')
        let sortedList = sortNeedList(needList, 'asc', 'status');
        setNeedList(sortedList) 
      }
      setLastFilter(2);
}

const sortNeedList = (list: any, sortBy: string, fieldName: string) => {
    if(sortBy === 'asc'){
        return list.sort(function(a: any, b: any){
            if(a[fieldName] < b[fieldName]) { return -1; }
            if(a[fieldName] > b[fieldName]) { return 1; }
            return 0;
          })
    }else{
        return list.sort(function(a: any, b: any){
            if(a[fieldName] < b[fieldName]) { return 1; }
            if(a[fieldName] > b[fieldName]) { return -1; }
            return 0;
          })
    }  
}

    return (
        <div className="need-list-view">
            <NeedListViewHeader
            toggleCallBack = {togglerHandler}
            />
            <NeedListTable
            needList = {needList}
            deleteDocument = {deleteClickHandler}
            sortDocumentTitle ={sortDocumentTitleHandler}
            documentTitleArrow = {sortArrow}
            sortStatusTitle = {sortStatusTitleHandler}
            statusTitleArrow = {sortStatusArrow}
            />
        </div>
    )
}
