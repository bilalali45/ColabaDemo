import React, { useEffect, useRef} from 'react';

// export interface clickOutSideProps {
//     callback:()=>void;
//     className?:any;
//     //when?:any;
// }

export const clickOutSide = (callback:any)  =>  {
    const domNode:any = useRef<any>(null);   

    useEffect(()=>{
        const handler = (e:any) =>{
            if(!domNode.current?.contains(e.target)){
                callback();
            }
        }       
        document.addEventListener("mousedown", handler);
        return () => document.removeEventListener("mousedown", handler);      
    });

    return domNode;
   
}