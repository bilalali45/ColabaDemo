import PSPDFKit from 'pspdfkit';
import Instance from 'pspdfkit/dist/types/typescript/Instance';
import React, { useContext, useEffect, useState, RefObject } from 'react'
import { DocumentActionsType } from '../../../../../Store/reducers/documentsReducer';
import { ViewerActionsType } from '../../../../../Store/reducers/ViewerReducer';
import { Store } from '../../../../../Store/Store';
import { AnnotationActions } from '../../../../../Utilities/AnnotationActions';
import { Loader } from '../../../../../Utilities/Loader';
import { PSPDFKitViewer } from '../../../../../Utilities/PSPDFKitViewer';
import { Viewer } from '../../../../../Utilities/Viewer';

// TRIAL-553dIKgZHx9CKMmvcnQUWyoTpN2xhAAdyxBGSUG55hVPsfXIYR3jexSzJ_LmE3z7mcOQqvoX2s_pJDuwOy4vkWW2xkuQrxL4Ef0v7rXbV7w
const licenseKey = 'Xq2sbPLKcoMngmloCFRhq1HUgk0jQLLbOf6LosAo6oO8y2G9QoaX3w3aX0PWavM6WOVdQo49a7UbnVe1GG6vkS1oSYDJv4EsuCckA4sx6M1qwqn9NbaszHkR6dvE8F0UhxZUsvIIRKUQQ67XwqwCd5G5iBfiJG6NE6gZRu-zasYtvEoyQ1uufbWcWF6FoXV6P_1FOcHrXqToVEXUqYVdYoPtXT3o_gEhLIp3mkLmIWXA2sUuMYZKKAHie1Wqu1eD1mpL0EzxadBtTVAPjLL8xMgl3h0PRZppCtQswQVFCQQMYwMLmDXG7Mzc_v8SO7z_3-CpjubR71MiAaMiw-jRCS8NfVnpso5pCws5gB3uhgxb4x94ISus4h1I0kiN9n7rsihbeJwn16L0-wuxDhuRr-Yyhh2WYdcQz-BfX6XXTTEThzESMHyrWWSJ6KNSNJLq';

const baseUrl = `${window.location.protocol}//${window.location.host}/DocManager/`;


export const PDFViewer = () => {

    // const [viewerInstanceLocal, setViewerInstaneLocal] = useState<Instance | null>(null);

    // const getViewerInstanceLocal = (instance: Instance) => setViewerInstaneLocal(instance);

    const { state, dispatch } = useContext(Store)

    const viewer: any = state.viewer;
    const instance: any = viewer.instance;

    const { currentFile, isLoading }: any = state.viewer;
    const { currentDoc, isFileDirty }: any = state.documents;
    const { isFileChanged }: any = state.viewer;

    useEffect(() => {
        dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: false })
    }, []);

    useEffect(() => {
        if (instance) {
            instance.addEventListener("document.change", async () => {
                console.log('in here document change')
                if (!isFileChanged) {
                    await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
                }
            });
            
            instance.addEventListener("annotations.willChange", async () => {
                console.log('in here annotations change')
                Viewer.instance = instance
                if (!isFileChanged) {
                    await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
                    
                }
            });

            instance.addEventListener("annotations.delete", (deletedAnnotations:any) => {
                console.log("in delete annotations")
                Viewer.instance = instance
              });
        }

    }, [instance, currentDoc, currentFile])

    const getViewerInstance = (instance: Instance) => {

        dispatch({ type: ViewerActionsType.SetInstance, payload: instance })
        Viewer.instance = instance;

    }

    const getViewerContainerElement = (containerRef: RefObject<HTMLDivElement>) => {

        dispatch({ type: ViewerActionsType.SetContainerElement, payload: containerRef })
        Viewer.instance = instance;

    }

    const setAnnotations = async () => {
        if (currentDoc && currentFile) {
           await AnnotationActions?.getAnnoations(currentDoc, currentFile);
           dispatch({type: ViewerActionsType.SetIsFileChanged, payload: false});
        }
    }

    return (
        <div className="vc-wrap-right">
            {/* {isLoading && <Loader containerHeight={"153px"} />} */}

            {viewer?.currentFile && <PSPDFKitViewer
                isFileChanged={isFileChanged}
                baseUrl={baseUrl}
                licenseKey={licenseKey}
                file={viewer?.currentFile.src}
                retrieveViewerInstance={getViewerInstance}
                retrieveContainerElement={getViewerContainerElement}
                setAnnotations={setAnnotations}
            />}
        </div>
    )
}
