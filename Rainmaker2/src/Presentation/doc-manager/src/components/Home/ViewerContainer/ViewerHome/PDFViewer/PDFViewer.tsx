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
        dispatch({ type: ViewerActionsType.SetSaveFile, payload: false })
        dispatch({ type: ViewerActionsType.SetDiscardFile, payload: false })
        dispatch({ type: DocumentActionsType.SetImportedFileIds, payload: [] })
    }, []);

    useEffect(() => {
        if (instance) {
            instance.addEventListener("document.change", async () => {
                Viewer.instance = instance
                if (!isFileChanged) {
                    await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
                    
                }
            });
            
            instance.addEventListener("annotations.willChange", async () => {
                Viewer.instance = instance
                if (!isFileChanged) {
                    await dispatch({ type: ViewerActionsType.SetIsFileChanged, payload: true });
                    
                }
            });


            instance.addEventListener('annotations.willChange', event => {
                const annotation = event.annotations.get(0)
                if (event.reason === PSPDFKit.AnnotationsWillChangeReason.DELETE_START) {
                  // We need to wrap the logic in a setTimeOut() because modal will get actually rendered on the next tick
                  setTimeout(function() {
                    
                  // The button is in the context of the PSPDFKit iframe
                  const button = instance.contentDocument.getElementsByClassName(
                    'PSPDFKit-Confirm-Dialog-Button-Confirm',
                  )[0]
                  if(button)
                  button.click();
                  }, 0)
                }
              })
            instance.addEventListener("annotations.delete", (deletedAnnotations:any) => {
                Viewer.instance = instance
              });
        }

    }, [instance, currentDoc, currentFile, isFileChanged, !isFileChanged])

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
                file={viewer?.currentFile.src}
                retrieveViewerInstance={getViewerInstance}
                retrieveContainerElement={getViewerContainerElement}
                setAnnotations={setAnnotations}
            />}
        </div>
    )
}
