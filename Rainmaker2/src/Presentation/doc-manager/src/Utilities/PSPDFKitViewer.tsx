import PSPDFKit from 'pspdfkit'
import Instance from 'pspdfkit/dist/types/typescript/Instance';
import React, { useContext, useEffect, useRef, useState } from 'react'
import { Store } from '../Store/Store';


let baseUrl: any = '';
let licenseKey: any = '';

let _cachedInstance: any = null;

type PSPDFKitViewerType = {
    baseUrl: string;
    licenseKey: string,
    file: ArrayBuffer | null | any,
    retrieveViewerInstance: Function,
    retrieveContainerElement: Function,
    setAnnotations: Function,
    isFileChanged: boolean
}

export const PSPDFKitViewer: React.FC<PSPDFKitViewerType> = ({
    baseUrl,
    licenseKey,
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
            console.log('in here unmount!!!');
            destroyViewer();
        }
    }, [])

    useEffect(() => {
        file && (async () => setviewerInstance(await getViewerInstance(file)))();
        retrieveContainerElement(viewerRef)
    }, [file]);


    const getViewerInstance = async (file: ArrayBuffer) => {

        let instanceConfig: any = {
            document: file,
            container: viewerRef?.current || '',
            licenseKey,
            baseUrl,
            theme: PSPDFKit.Theme.DARK,
            styleSheets: ['http://localhost:3002/DocManager/assets/css/pspdfkit-styles.css']
        };
        try {
            let instance = await PSPDFKit.load(instanceConfig);
            _cachedInstance = instance;
            retrieveViewerInstance(instance);
            setAnnotations();
            instance?.setViewState(state => state.set("zoom", 'FIT_TO_VIEWPORT'));
            return instance;
        } catch (error) {
            console.log(error);

        }
        return null;
    }

    // const retrieveViewerInstance = () => viewerInstance;

    const destroyViewer = () => {
        console.log('in unmount');
        if (_cachedInstance)
            PSPDFKit.unload(_cachedInstance);
    }

    return (
        <div className="viewer-container-ref" ref={viewerRef} style={{ width: '100%', height: '100%', position: 'absolute' }}>

        </div>
    )
}
