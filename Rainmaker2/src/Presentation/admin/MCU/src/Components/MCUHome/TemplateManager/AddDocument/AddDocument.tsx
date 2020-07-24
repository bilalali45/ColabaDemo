import React, { useState, useContext, useEffect, useRef } from 'react'
import Popover from 'react-bootstrap/Popover'
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import { DocumentTypes } from './DocumentTypes/DocumentTypes'
import { CommonDocuments } from './SelectedDocumentType/CommonDocuments/CommonDocuments'
import { SelectedType } from './SelectedDocumentType/SelectedDocumentType'
import { Store } from '../../../../Store/Store'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer'
import { Document } from '../../../../Entities/Models/Document'
import { CategoryDocument } from '../../../../Entities/Models/CategoryDocument'
import Overlay from 'react-bootstrap/Overlay'

type AddDocumentType = {
    popoverplacement?: any;
    setLoaderVisible: Function;

}
export const AddDocument = ({ popoverplacement = "bottom", setLoaderVisible }: AddDocumentType) => {
    const [PopoverShowClass, setpopovershowClass] = useState("");
    const [target, setTarget] = useState(null);
    const [requestSent, setRequestSent] = useState<boolean>(false);
    const [show, setShow] = useState<boolean>(false);
    const mainContainerRef = useRef(null);
    const aRef = useRef(null);

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;

    const currentTemplate = templateManager?.currentTemplate;
    const templateDocuments = templateManager?.templateDocuments;
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const addDocumentBoxVisible = templateManager?.addDocumentBoxVisible;


    const handleClick = (event: any) => {
        let tag = event.target.tagName;

        dispatch({ type: TemplateActionsType.ToggleAddDocumentBox, payload: { value: !addDocumentBoxVisible?.value } })
        // if (tag === 'A') {
        // } else {
        //     dispatch({ type: TemplateActionsType.ToggleAddDocumentBox, payload: { value: false } })
        // }
        setShow(!show);

        setTarget(event.target);
    };

    const handleOnEntered = (event:any) => {
        setpopovershowClass(event);
    };
    
    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }
        if (mainContainerRef?.current) {
            setTarget(null);
        }
        if (aRef?.current) {
            setTarget(aRef?.current)
        }
    }, [aRef?.current, mainContainerRef?.current]);

    useEffect(() => {
        // setShow(addDocumentBoxVisible?.value)
    }, [addDocumentBoxVisible])

    const fetchCurrentCatDocs = async () => {
        let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();
        if (currentCatDocs) {
            dispatch({ type: TemplateActionsType.SetCategoryDocuments, payload: currentCatDocs });
            setCurrentDocType(currentCatDocs[0]);
        }
    }

    const setCurrentDocType = (curDoc: CategoryDocument) => {
        dispatch({ type: TemplateActionsType.SetCurrentCategoryDocuments, payload: curDoc });

    }

    const changeCurrentDocType = (curDocType: string) => {

        if (curDocType === 'all') {
            setCurrentDocType(extractAllDocs());
        // }
        //  else if (curDocType === 'other') {
        //     let currentDoc = {
        //         catId: 'other',
        //         catName: 'Other',
        //         documents: []
        //     };
        //     setCurrentDocType(currentDoc);
        } else {
            let currentDoc = categoryDocuments.find((c: CategoryDocument) => c.catId === curDocType);
            setCurrentDocType(currentDoc);
        }
    }

    const extractAllDocs = () => {
        let allDocs: Document[] = [];

        for (const doc of categoryDocuments) {
            allDocs = [...allDocs, ...doc.documents];
        }
        return {
            catId: 'all',
            catName: 'Commonly Used Documnets',
            documents: allDocs
        };
    }

    const addDocToTemplate = async (docName: string, type: string) => {
        if (!docName?.length || docName?.length > 254) {
            return;
        }
        if(requestSent) return;
        setRequestSent(true);
        setLoaderVisible(true);
        // if (templateDocuments.find((t: any) => t.docName?.toLowerCase() === docName?.toLowerCase())) {
        //     return;
        // }
        try {
            let success = await TemplateActions.addDocument('1', currentTemplate?.id, docName, type);
            if (success) {
                let docs = await TemplateActions.fetchTemplateDocuments(currentTemplate?.id);
                dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: docs });
            }
        } catch (error) {

        }
        setRequestSent(false);
        setLoaderVisible(false);
    }

    const hidePopup = () => {
        dispatch({ type: TemplateActionsType.ToggleAddDocumentBox, payload: { value: false } })
    }

    const renderPopOverContent = () => {
        return (
            <div className="popup-add-doc">
                <div className="popup-add-doc-row row">
                    <div className="col-sm-5 col-md-5  col-lg-4 popup-add-doc-row--left">
                        <DocumentTypes
                            currentCategoryDocuments={currentCategoryDocuments}
                            documentTypeList={categoryDocuments}
                            changeCurrentDocType={changeCurrentDocType} />
                    </div>
                    <div className="col-sm-7 col-md-7 col-lg-8 popup-add-doc-row--right">

                        <SelectedType
                            setVisible={hidePopup}
                            selectedCatDocs={currentCategoryDocuments}
                            addNewDoc={addDocToTemplate} />
                    </div>
                </div>
            </div>
        )
    }

    const renderPopOver = () => {
        return (
            <Popover id="popover-add-document" className={PopoverShowClass}> 
                <Popover.Content>
                    {renderPopOverContent()}
                </Popover.Content>
            </Popover>
        )
    }

    return (
        <div className="Compo-add-document" >

            <div className="add-doc-link-wrap" ref={mainContainerRef} >
                {/* <OverlayTrigger trigger="click" placement="auto" overlay={renderPopOver()}  > */}
                {/* <a ref={aRef} className="add-doc-link" onClick={(e) => { handleClick(e) }} >
                    Add Document <i className="zmdi zmdi-plus"></i>
                </a> */}

                <div ref={aRef}  className="btn-add-new-Temp"  onClick={(e) => { handleClick(e) }} >
                    <button className="btn btn-primary addnewTemplate-btn">
                        <span className="btn-text">Add Document</span>
                        <span className="btn-icon">
                            <i className="zmdi zmdi-plus"></i>
                        </span>

                    </button>
                </div>
                {/* </OverlayTrigger> */}
            </div>
            <Overlay show={show}
                target={target}
                placement={popoverplacement}
                container={mainContainerRef.current || aRef.current}
                onHide={handleClick}
                rootClose={true}
                rootCloseEvent={'click'}
                transition={false}
                
            >
                {renderPopOver()}
            </Overlay>
        </div>
    )
}
