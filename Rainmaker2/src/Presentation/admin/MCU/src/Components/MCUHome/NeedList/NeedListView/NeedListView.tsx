import React, { useEffect, useState } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'
import { LocalDB } from '../../../../Utils/LocalDB'

export const NeedListView = () => {

    const[needList, setNeedList] = useState<NeedList | null>();
    const [toggle, setToggle] = useState(false);
    const [sortArrow, setSortArrow] = useState('asc')
    

useEffect(()=> {
    if(!needList){
        fetchNeedList(false, 'asc')
    }
},[needList])

const fetchNeedList = async (status: boolean, sortBy: string)=> {
    let applicationId = LocalDB.getLoanAppliationId();
    let tenentId = LocalDB.getTenantId();
    if(applicationId && tenentId){
        let res: NeedList | undefined = await NeedListActions.getNeedList(applicationId, tenentId, status) 
        if(res){
            if(sortBy){
                let sortedList = sortNeedList(res, sortBy);
                setNeedList(sortedList)
            }
            setNeedList(res)
        } 
    }  
}

const deleteNeedListDoc = async (id: string, requestId: string, docId: string)=>{
    let tenentId = LocalDB.getTenantId();
    if(id && requestId && docId && tenentId){
      let res = await NeedListActions.deleteNeedListDocument(id, requestId, docId, parseInt(tenentId))
      if(res === 200){
        fetchNeedList(toggle, sortArrow)
        setSortArrow(sortArrow) 
      }
    }
}

const togglerHandler = (pending: boolean) => {
   if(!pending){
    fetchNeedList(!pending, '')
    setToggle(!toggle)
    setSortArrow('desc') 
   }else{
    fetchNeedList(!pending, 'asc')
    setToggle(!toggle)
    setSortArrow('asc') 
   }  
}

const deleteClickHandler = (id: string, requestId: string, docId: string) => {
    deleteNeedListDoc(id, requestId, docId);
}

const sortDocumentTitleHandler = () => {
  if(sortArrow === 'asc'){
    let sortedList = sortNeedList(needList, 'desc');
    setNeedList(sortedList) 
    setSortArrow('desc') 
  }else{
    setSortArrow('asc')
    let sortedList = sortNeedList(needList, 'asc');
    setNeedList(sortedList) 
  }
}

const sortNeedList = (list: any, sortBy: string) => {
    if(sortBy === 'asc'){
        return list.sort(function(a: any, b: any){
            if(a.docName < b.docName) { return -1; }
            if(a.docName > b.docName) { return 1; }
            return 0;
          })
    }else{
        return list.sort(function(a: any, b: any){
            if(a.docName < b.docName) { return 1; }
            if(a.docName > b.docName) { return -1; }
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
            />
        </div>
    )
}
