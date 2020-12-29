import React, { useContext, useEffect, useState,useRef } from 'react'
import { DocumentsHeader } from './DocumentsHeader/DocumentsHeader'
import { DocumentsTableView } from './DocumentTableView/DocumentsTableView'
import { WorkbenchView } from './WorkbenchView/WorkbenchView'
import Split from 'react-split'

export const DocumentsContainer = () => {
let splitWrap: any = window.document.getElementsByClassName('splitWrap');
const splitOnDragStart = () =>{
    splitWrap[0].classList.add("DragStart")
}
const splitOnDragEnd = () =>{
    splitWrap[0].classList.remove("DragStart")
}
const [splitsizes,setSplitSizes] = useState([55, 45]);

    return (
        <div className="c-DocContainer">
            <DocumentsHeader/>
            <Split
    sizes={splitsizes}
    minSize={50}
    expandToMin={false}
    gutterSize={20}
    gutterAlign="center"
    snapOffset={30}
    dragInterval={1}
    direction="vertical"
    cursor="row-resize"
    className={'splitWrap'}
    onDragStart={splitOnDragStart}
    onDragEnd={splitOnDragEnd}
    
    
>
            <DocumentsTableView/>
            <WorkbenchView
            setSplitSizes={setSplitSizes}
            />
         </Split>   
        </div>
    )
}
