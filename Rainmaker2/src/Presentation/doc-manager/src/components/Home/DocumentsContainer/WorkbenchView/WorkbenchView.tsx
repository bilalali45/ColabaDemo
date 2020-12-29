import React, { useContext, useEffect, useState,useRef } from 'react'
import { WorkbenchTable } from './WorkbenchTable/WorkbenchTable'
import {
    MinusIcon,
    PlusIcon,
  } from "../../../../shared/Components/Assets/SVG";
type WorkbenchViewType = {
    setSplitSizes?:Function
}


export const WorkbenchView = ({
    setSplitSizes
  }: WorkbenchViewType) => {

    const [show, setShow] = useState(true);
    const handleClick = () => {
        setShow(!show);

        let gutterDiv: any = window.document.getElementsByClassName('gutter');
        
        if (show) {
            setSplitSizes([93,7])
            gutterDiv[0]?.classList.add('hide');
        }
        else {
            setSplitSizes([55,45])
            gutterDiv[0]?.classList.remove('hide');
        }


        
      };
    // useEffect(()=>{
      
    //     setSplitSizes([50,50])
        
    //   },[])

    return (
        <div className="c-WorkbenchView">
            <div className={`wb-head ${!show?"wbcollaps":""}`} onClick={handleClick}>
                <h2>{show ? <MinusIcon /> : <PlusIcon />} Uncategorized Doc</h2>
            </div>
            { show ? <WorkbenchTable/> : null }
        </div>
    )
}
