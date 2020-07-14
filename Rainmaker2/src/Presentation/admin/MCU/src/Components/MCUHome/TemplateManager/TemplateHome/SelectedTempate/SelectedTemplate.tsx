import React, { useEffect, useContext, useState } from 'react'
import { TemplateItemsList } from './TemplateItemsList/TemplateItemsList'
import { AddDocument } from '../../AddDocument/AddDocument'
import checkicon from '../../../../../Assets/images/checkicon.svg'
import EditIcon from '../../../../../Assets/images/editicon.svg'
import { TemplateActions } from '../../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer'
import { Store } from '../../../../../Store/Store'

export const SelectedTemplate = () => {

    const { state, dispatch } = useContext(Store);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);


    const templateManager: any = state.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templateDocuments = templateManager?.templateDocuments;

    useEffect(() => {
        setCurrentTemplate(currentTemplate)
    }, [templateDocuments?.length, currentTemplate?.id]);

    const setCurrentTemplate = async (template: any) => {
        const templateDocs = await TemplateActions.fetchTemplateDocuments(template?.id);
        if(templateDocs) {
            dispatch({type: TemplateActionsType.SetTemplateDocuments, payload: templateDocs});
            dispatch({type: TemplateActionsType.SetCurrentTemplate, payload: template});
        }
    }

    const renameTemplate = () => {
        
    }

    const toggleRename = () => {
        seteditTitleview(true)
    }
    const VeiwableTitle = () => {
        seteditTitleview(false)
    }

    const removeDoc = async (templateId: string, documentId: string) => {
        let isDeleted = await TemplateActions.deleteTemplateDocument('1', templateId, documentId);
        if (isDeleted) {
            setCurrentTemplate(currentTemplate);
        }
    }



    return (
        <section>
            <div className="T-head">
                {editTitleview ?
                    <p className="editable"> <input onChange={renameTemplate} value="My standard checklist" className="editable-TemplateTitle" />
                        <span className="editsaveicon" onClick={VeiwableTitle}><img src={checkicon} alt="" /></span></p>
                    : <>
                        <p> {currentTemplate?.name} <span className="editicon" onClick={toggleRename}><img src={EditIcon} alt="" /></span></p>
                    </>
                }

            </div>

            <div className="ST-content-Wrap">
                <ul>
                    {
                        templateDocuments?.map((td: any) => {
                            return (
                                <li>
                                    <p>{td.docName}
                                        <span className="BTNclose">
                                            <i className="zmdi zmdi-close" onClick={() => removeDoc(currentTemplate?.id, td.docId)}></i>
                                        </span>
                                    </p>
                                </li>
                            )
                        })
                    }

                </ul>

                {/* <div className="add-doc-link-wrap">
                    <a className="add-doc-link">
                        Add Document <i className="zmdi zmdi-plus"></i>
                    </a>
                </div> */}
                <AddDocument/> 
            </div>


            {/* <TemplateItemsList/>
            <AddDocument/> */}
        </section>
    )
}
