import React, { useContext, useEffect } from 'react'
import { DocumentTypes } from './DocumentTypes/DocumentTypes'
import { CommonDocuments } from './SelectedDocumentType/CommonDocuments/CommonDocuments'
import { SelectedType } from './SelectedDocumentType/SelectedDocumentType'
import { Store } from '../../../../Store/Store'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer'

export const AddDocument = () => {

    const {state, dispatch} = useContext(Store);

    const templateManager : any = state.templateManager;
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const currentTemplate = templateManager?.currentTemplate;

    useEffect(() => {
        if(!categoryDocuments) {
            fetchCatDocs();
        }
    }, [])

    const fetchCatDocs = async () => {
        let catDocs : any = await TemplateActions.fetchCategoryDocuments();
        if(catDocs) {
            dispatch({type: TemplateActionsType.SetCategoryDocuments, payload: catDocs});
            dispatch({type: TemplateActionsType.SetCurrentCategoryDocuments, payload: catDocs[0]});
        }
    }

    const addNewDoc = async (typeId: string) => {
        let res = await TemplateActions.addDocument('1', currentTemplate?.id, typeId, 'typeId')
    }

    return (
        <div>
            <h1>AddDocument</h1>
            <DocumentTypes
               documentTypeList={categoryDocuments} />
            <SelectedType
                documentList={currentCategoryDocuments?.documents}
                addNewDoc={addNewDoc}/>
        </div>
    )
}
