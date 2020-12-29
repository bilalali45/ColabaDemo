import PSPDFKit from "pspdfkit";
import Instance from "pspdfkit/dist/types/typescript/Instance";
import React, { useContext, useEffect, useState } from "react";
import { Store } from "../../../../../Store/Store";
import { ViewerTools } from "../../../../../Utilities/ViewerTools";

export const ViewerToolbar = () => {
  const { state, dispatch } = useContext(Store);

  const viewer: any = state.viewer;
  const instance: any = viewer.instance;
  const { currentFile, isFileChanged }: any = state.viewer;
  const { currentDoc, workbenchItems }: any = state.documents;


  

  useEffect(() => {
    if(currentDoc && currentFile){
       generateToolbarData()
    }
  }, [instance, currentDoc, currentFile, isFileChanged]);

    const generateToolbarData = async () => {
      
      
      if(currentFile.isWorkBenchFile){
        let{id, fileId, isWorkBenchFile, name } = currentFile
        let annotationObj = {
          id, 
          requestId:"000000000000000000000000", 
          docId:"000000000000000000000000", 
          fileId, 
          isFromWorkbench:true,
          name
        }
        await ViewerTools.generateToolBarData(annotationObj, isFileChanged, dispatch, workbenchItems, currentFile );
        
      }
      else{
        let{fileId, name } = currentFile
        let {id, requestId, docId} = currentDoc;
        let annotationObj = {
          id, 
          requestId, 
          docId, 
          fileId, 
          isFromCategory:true,
          name
        }
        await ViewerTools.generateToolBarData(annotationObj, isFileChanged, dispatch, currentDoc, currentFile );

      }
      
    }
  return (
    <div className="Zoom-toolbar">
      <button id="zoom-in" onClick={()=>ViewerTools.zoomIn()} className="zoom-in zoom-button">
      <i className="zmdi zmdi-plus"></i>
      </button>
      <button id="zoom-out" onClick={()=>ViewerTools.zoomOut()} className="zoom-out zoom-button">
      <i className="zmdi zmdi-minus"></i>
      </button>
      <button id="zoom-reset" onClick={()=>ViewerTools.fitToScreen()} className="zoom-reset zoom-button">
      <svg xmlns="http://www.w3.org/2000/svg" width="14.66" height="14.66" viewBox="0 0 14.66 14.66">
  <path id="Fit_To_Width-595b40b65ba036ed117d1c56" data-name="Fit To Width-595b40b65ba036ed117d1c56" d="M4,4V9.5H5.222V6.1L8.448,9.326l.878-.878L6.1,5.222H9.5V4H4Zm9.162,0V5.222h3.4L13.334,8.448l.878.878L17.438,6.1V9.5H18.66V4h-5.5ZM4,13.162v5.5H9.5V17.438H6.1l3.226-3.226-.878-.878L5.222,16.56v-3.4Zm13.438,0v3.4l-3.226-3.226-.878.878,3.226,3.226h-3.4V18.66h5.5v-5.5Z" transform="translate(-4 -4)" fill="#7e829e"/>
</svg>

      </button>
    </div>
  );
};
