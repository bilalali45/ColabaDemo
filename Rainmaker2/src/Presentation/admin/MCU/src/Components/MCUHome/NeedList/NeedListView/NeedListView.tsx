import React, { useEffect, useState } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'
import { LocalDB } from '../../../../Utils/LocalDB'

export const NeedListView = () => {

    const[needList, setNeedList] = useState<NeedList | null>();

useEffect(()=> {
    if(!needList){
        fetchNeedList(false)
    }
},[needList])

const fetchNeedList = async (status: boolean)=> {
    let applicationId = LocalDB.getLoanAppliationId();
    let tenentId = LocalDB.getTenantId();
    if(applicationId && tenentId){
        let res: NeedList | undefined = await NeedListActions.getNeedList(applicationId, tenentId, status) 
        if(res){
            setNeedList(res) 
        } 
    }  
}

const togglerHandler = (pending: boolean) => {
    fetchNeedList(!pending)
}

const deleteClickHandler = (id: string, docId: string) => {
    console.log('Click on delete Id&docId:', id, docId)
}


    return (
        <div className="need-list-view">
            <NeedListViewHeader
            toggleCallBack = {togglerHandler}
            />
            <NeedListTable
            needList = {needList}
            deleteDocument = {deleteClickHandler}
            />
        </div>
    )
}
