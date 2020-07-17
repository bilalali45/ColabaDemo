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
    popoverplacement?:any,
}
export const AddDocument = ({ popoverplacement="bottom" }: AddDocumentType) => {
    const [popshow, setshow] = useState(true);
    const [show, setShow] = useState(false);
    const [target, setTarget] = useState(null);
    const ref = useRef(null);

    const handleClick = (event: any) => {
        setShow(!show);
        setTarget(event.target);
    };

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;

    const currentTemplate = templateManager?.currentTemplate;
    const templateDocuments = templateManager?.templateDocuments;
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;

    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }
    }, []);

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
        } else if (curDocType === 'other') {
            let currentDoc = {
                catId: 'other',
                catName: 'Other',
                documents: []
            };
            setCurrentDocType(currentDoc);
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

    const showpopover = () => {
        setshow(!popshow)
    }

    const addDocToTemplate = async (docName: string, type: string) => {
        if(templateDocuments.find((t: any) => t.docName?.toLowerCase() === docName?.toLowerCase())) {
            return;
        }
        try {
            let success = await TemplateActions.addDocument('1', currentTemplate?.id, docName, type);
            if (success) {
                let docs = await TemplateActions.fetchTemplateDocuments(currentTemplate?.id);
                dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: docs });
            }
        } catch (error) {

        }
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
                            setVisible={setShow}
                            selectedCatDocs={currentCategoryDocuments}
                            addNewDoc={addDocToTemplate} />
                    </div>
                </div>
            </div>
        )
    }

    const renderPopOver = () => {
        return (
            <Popover id="popover-add-document">
                <Popover.Content>
                    {renderPopOverContent()}
                </Popover.Content>
            </Popover>
        )
    }


    return (
        <div className="Compo-add-document" ref={ref}>

            <div className="add-doc-link-wrap">
                {/* <OverlayTrigger trigger="click" placement="auto" overlay={renderPopOver()}  > */}
                <a className="add-doc-link" onClick={(e)=>{handleClick(e)}} >
                    Add Document <i className="zmdi zmdi-plus"></i>
                </a>
                {/* </OverlayTrigger> */}
            </div>
            <Overlay show={show}
                target={target}
                placement={popoverplacement}
                container={ref.current}
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
