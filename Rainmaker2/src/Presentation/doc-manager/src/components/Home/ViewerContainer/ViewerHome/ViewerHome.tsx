import React, { useContext, useEffect, useState, RefObject } from 'react'
import { PDFViewer } from './PDFViewer/PDFViewer'
import { ViewerThumbnails } from './ViewerThumbnails/ViewerThumbnails'
import { ViewerToolbar } from './ViewerToolbar/ViewerToolbar';
// import './../../../../App.css';    

export const ViewerHome = () => {
    return (
        <div className="vc-wrap">
            <ViewerThumbnails/>
            <PDFViewer/>
            <ViewerToolbar/>
        </div>
    )
}
