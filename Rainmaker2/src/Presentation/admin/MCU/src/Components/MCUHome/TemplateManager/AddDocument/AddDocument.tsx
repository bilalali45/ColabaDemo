import React, { useState, useContext, useEffect } from 'react'
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

export const AddDocument = () => {
    const [popshow, setshow] = useState(false);

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;

    const currentTemplate = templateManager?.currentTemplate;
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
        try {
            let success = await TemplateActions.addDocument('1', currentTemplate?.id, docName, type);
            if(success) {
               let docs = await TemplateActions.fetchTemplateDocuments(currentTemplate?.id);
               dispatch({type: TemplateActionsType.SetTemplateDocuments, payload: docs});
            }
        } catch (error) {
            
        }
    }

    const renderPopOverContent = () => {
        return (
            <div className="popup-add-doc">
                <div className="popup-add-doc-row row">
                    <div className="col-sm-4 popup-add-doc-row--left">
                        <DocumentTypes
                            currentCategoryDocuments={currentCategoryDocuments}
                            documentTypeList={categoryDocuments}
                            changeCurrentDocType={changeCurrentDocType} />
                    </div>
                    <div className="col-sm-8 popup-add-doc-row--right">

                        <SelectedType
                            selectedCatDocs={currentCategoryDocuments}
                            addNewDoc={addDocToTemplate} />
                    </div>
                </div>
            </div>
        )
    }

    const renderPopOver = () => {
        return (
            <Popover id="popover-basic">
                <Popover.Content>
                    {renderPopOverContent()}
                </Popover.Content>
            </Popover>
        )
    }


    return (
        <div className="Compo-add-document">

            <div className="add-doc-link-wrap">
                <OverlayTrigger trigger="click" placement="auto" overlay={renderPopOver()} >
                    <a className="add-doc-link">
                        Add Document <i className="zmdi zmdi-plus"></i>
                    </a>
                </OverlayTrigger>
            </div>
        </div>
    )
}
