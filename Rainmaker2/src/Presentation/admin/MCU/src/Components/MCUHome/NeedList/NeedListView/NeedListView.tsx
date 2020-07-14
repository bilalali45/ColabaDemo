import React, { useEffect, useState } from 'react'
import { NeedListViewHeader } from './NeedListViewHeader/NeedListViewHeader'
import { NeedListTable } from './NeedListTable/NeedListTable'
import { NeedList } from '../../../../Entities/Models/NeedList'
import { NeedListActions } from '../../../../Store/actions/NeedListActions'

export const NeedListView = () => {

    const[needList, setNeedList] = useState<NeedList | null>();

useEffect(()=> {
    if(!needList){
        fetchNeedList()
    }
},[needList])

const fetchNeedList = async ()=> {
    let applicationId = '5976';
    let tenentId = '1';
    let status = false;
    let res: NeedList | undefined = await NeedListActions.getNeedList(applicationId, tenentId, status) 
    if(res){
        setNeedList(res) 
    } 
}

const togglerHandler = () => {
    
}

console.log('needList', needList)
    return (
        <div className="need-list-view">
            <NeedListViewHeader
            toggleCallBack = {togglerHandler}
            />
            <NeedListTable/>
        </div>
    )
}
