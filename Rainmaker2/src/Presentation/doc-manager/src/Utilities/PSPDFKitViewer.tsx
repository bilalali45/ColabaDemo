import PSPDFKit from 'pspdfkit'
import Instance from 'pspdfkit/dist/types/typescript/Instance';
import React, { useContext, useEffect, useRef, useState } from 'react'
import { Store } from '../Store/Store';
import { ViewerTools } from './ViewerTools';


let licenseKey: any = window?.envConfig?.PSPDFKIT_LICENCE;
const baseUrl = `${window.location.protocol}//${window.location.host}/DocManager/`;

let _cachedInstance: any = null;

type PSPDFKitViewerType = {
    baseUrl: string;
    file: ArrayBuffer | null | any,
    retrieveViewerInstance: Function,
    retrieveContainerElement: Function,
    setAnnotations: Function,
    isFileChanged: boolean
}

export const PSPDFKitViewer: React.FC<PSPDFKitViewerType> = ({
    baseUrl,
    file,
    retrieveViewerInstance,
    retrieveContainerElement,
    setAnnotations,
    isFileChanged
}: PSPDFKitViewerType) => {

    const [viewerInstance, setviewerInstance] = useState<Instance | null>(null);
    const [saveBtnPressed, setSaveBtnPressed] = useState<Boolean>(false);

    const viewerRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        return () => {
            destroyViewer();
        }
    }, [])

    useEffect(() => {
        file && (async () => setviewerInstance(await getViewerInstance(file)))();
        retrieveContainerElement(viewerRef)
    }, [file]);


    const getViewerInstance = async (file: Blob) => {

        if(process.env.NODE_ENV === "test"){
            let instanceConfig: any = {
                document: await file,
                container: viewerRef?.current || '',
                licenseKey,
                baseUrl,
                theme: PSPDFKit.Theme.DARK,
                 styleSheets: [`${baseUrl}assets/css/pspdfkit-styles.css`]
                
            };
            try {
                let instance = await PSPDFKit.load(instanceConfig);
                _cachedInstance = instance;
                // ViewerTools.addEmptyPage(instance);
                retrieveViewerInstance(instance);
                setAnnotations();
                instance?.setViewState(state => state.set("zoom", 'FIT_TO_VIEWPORT'));
                return instance;
            } catch (error) {
                console.log(error);
    
            }
        }
        else{

        
        let instanceConfig: any = {
            document: await file.arrayBuffer(),
            container: viewerRef?.current || '',
            licenseKey,
            baseUrl,
            theme: PSPDFKit.Theme.DARK,
             styleSheets: [`${baseUrl}assets/css/pspdfkit-styles.css`]
            
        };
        try {
            let instance = await PSPDFKit.load(instanceConfig);
            ViewerTools.updateAnnotationDefaultValues(instance);
            _cachedInstance = instance;
            // ViewerTools.addEmptyPage(instance);
            retrieveViewerInstance(instance);
            setAnnotations();
            instance?.setViewState(state => state.set("zoom", 'FIT_TO_VIEWPORT'));
            return instance;
        } catch (error) {
            console.log(error);

        }
    }
        return null;
    }


    const destroyViewer = () => {
        if (_cachedInstance)
            PSPDFKit.unload(_cachedInstance);
    }

    return (
        <div className="viewer-container-ref" ref={viewerRef} style={{ width: '100%', height: '100%', position: 'absolute' }}>

        </div>
    )
}
