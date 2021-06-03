import Instance from 'pspdfkit/dist/types/typescript/Instance';
import React, { useContext, useState } from 'react'
import { Store } from '../../../Store/Store';
import { ViewerHeader } from './ViewerHeader/ViewerHeader'
import { PDFViewer } from './ViewerHome/PDFViewer/PDFViewer';
import { ViewerHome } from './ViewerHome/ViewerHome'

export const ViewerContainer = () => {

    const [viewerInstanceLocal, setViewerInstaneLocal] = useState<Instance | null>(null);
    

    return (
        <div className="c-ViewerContainer">
            <div className="vc-head">
            <ViewerHeader/>
            </div>
        
            
            <ViewerHome/>
        </div>
    )
}
