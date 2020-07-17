import React, { useEffect, useContext, useState, ChangeEvent } from 'react'
import { TemplateItemsList } from './TemplateItemsList/TemplateItemsList'
import { AddDocument } from '../../AddDocument/AddDocument'
import checkicon from '../../../../../Assets/images/checkicon.svg'
import EditIcon from '../../../../../Assets/images/editicon.svg'
import { TemplateActions } from '../../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer'
import { Store } from '../../../../../Store/Store'
import { NewTemplate } from '../../NewTemplate/NewTemplate'
import { TemplateEditBox } from '../../../../../Shared/components/TemplateEditBox'
import { Template } from '../../../../../Entities/Models/Template'
import { TemplateDocument } from '../../../../../Entities/Models/TemplateDocument'
import { MyTemplate } from '../TemplateListContainer/TemplateListContainer'


export const SelectedTemplate = () => {

    const { state, dispatch } = useContext(Store);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [newNameText, setNewNameText] = useState<string>('');
    const [addingNew, setAddingNew] = useState<boolean>(false);

    const templateManager: any = state.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templateDocuments = templateManager?.templateDocuments;

    useEffect(() => {
        setNewNameText(currentTemplate?.name)
    }, [editTitleview])

    useEffect(() => {
        if (currentTemplate) {
            seteditTitleview(false);
        }

        if(!currentTemplate) {
            seteditTitleview(false);
            setNewNameText('');
        }

        setCurrentTemplateDocs(currentTemplate)
    }, [templateDocuments?.length, currentTemplate?.id]);


    const setCurrentTemplateDocs = async (template: any) => {
        const templateDocs = await TemplateActions.fetchTemplateDocuments(template?.id);
        if (templateDocs) {
            dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: templateDocs });
        }
    }

    const addNewTemplate = async (name: string) => {

        let insertedTemplate = await TemplateActions.insertTemplate('1', name);

        if (insertedTemplate) {

            let updatedTemplates: any = await TemplateActions.fetchTemplates('1');
            dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });

            let currentTemplate = updatedTemplates.find((t: Template) => t.name === name);
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: currentTemplate });
        }
    }

    const renameTemplate = async ({ target: { value } }: any) => {

        if (!currentTemplate) {
            await addNewTemplate(value);
            toggleRename();

            return;
        }

        const renamed = await TemplateActions.renameTemplate('1', currentTemplate?.id, value);
        if (renamed) {
            let updatedTemplates: any = await TemplateActions.fetchTemplates('1');
            if (updatedTemplates) {
                dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });
                dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: updatedTemplates.find((up: Template) => up.id === currentTemplate.id) });

            }
        }
        toggleRename();
    }

    const toggleRename = () => {
        seteditTitleview(!editTitleview);
        setAddingNew(false);
    }

    const removeDoc = async (templateId: string, documentId: string) => {
        let isDeleted = await TemplateActions.deleteTemplateDocument('1', templateId, documentId);
        // debugger
        if (isDeleted === 200) {
            await setCurrentTemplateDocs(currentTemplate);
        }
    }

    return (
        <section>
            <div className="T-head">
                {editTitleview || currentTemplate === null ?
                    <p className="editable">
                        <input
                            value={newNameText}
                            onChange={(e) => setNewNameText(e.target.value)}
                            onKeyDown={(e) => {
                                if (e.keyCode === 13) {
                                    renameTemplate(e);
                                }
                            }}
                            // onBlur={renameTemplate}
                            className="editable-TemplateTitle" />
                        <span className="editsaveicon" onClick={toggleRename}><img src={checkicon} alt="" /></span></p>
                    : <>
                        <p> {currentTemplate?.name} {currentTemplate?.type === MyTemplate && <span className="editicon" onClick={toggleRename}><img src={EditIcon} alt="" /></span>}</p>
                    </>
                }
                {/* 
                <TemplateEditBox
                   templateName={currentTemplate?.name} 
                   renameTemplate={renameTemplate}
                   checkIcon={checkicon}
                   editIcon={EditIcon}/> */}

            </div>

            {templateDocuments?.length ? <div className="ST-content-Wrap">
                <ul>
                    {
                        templateDocuments?.map((td: TemplateDocument) => {
                            return (
                                <li>
                                    <p>{td.docName}
                                        <span className="BTNclose">
                                            {
                                                currentTemplate?.type === MyTemplate &&
                                                <i className="zmdi zmdi-close" onClick={() => removeDoc(currentTemplate?.id, td.docId)}></i>
                                            }
                                        </span>
                                    </p>
                                </li>
                            )
                        })
                    }

                </ul>
                {currentTemplate?.type === MyTemplate &&
                    <AddDocument popoverplacement="right" />
                }
            </div> :
                <NewTemplate />
            }


            {/* <TemplateItemsList/> */}

        </section>
    )
}
