import React, { useEffect, useContext, useState, ChangeEvent, Fragment, KeyboardEvent } from 'react'
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
import { nameTest } from '../TemplateHome';
import Spinner from 'react-bootstrap/Spinner'
import { Loader } from "../../../../../Shared/components/loader";

type SelectedTemplateType = {
    loaderVisible: boolean;
    setLoaderVisible: Function;
}

export const SelectedTemplate = ({ loaderVisible, setLoaderVisible }: SelectedTemplateType) => {

    const { state, dispatch } = useContext(Store);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [newNameText, setNewNameText] = useState<string>('');
    const [nameExistsError, setNameExistsError] = useState<string>()
    const [addRequestSent, setAddRequestSent] = useState<boolean>(false);
    const [removeDocName, setRemoveDocName] = useState<string>();


    const templateManager: any = state.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templates = templateManager?.templates;
    const templateDocuments = templateManager?.templateDocuments;

    useEffect(() => {
        setNewNameText(currentTemplate?.name)
    }, [editTitleview])

    useEffect(() => {
        if (currentTemplate) {
            seteditTitleview(false);
        }

        if (!currentTemplate) {
            seteditTitleview(false);
            setNewNameText('');
        }

        setCurrentTemplateDocs(currentTemplate)
    }, [templateDocuments?.length, currentTemplate?.id]);


    const setCurrentTemplateDocs = async (template: any) => {
        if (!currentTemplate) return '';
        setLoaderVisible(!loaderVisible);
        const templateDocs = await TemplateActions.fetchTemplateDocuments(template?.id);
        if (templateDocs) {
            dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: templateDocs });
        }
        setLoaderVisible(false);
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

    const renameTemplate = async (value: string) => {
        if (addRequestSent) return;
        setAddRequestSent(true);
        if (!value?.length || value?.length > 255 || !value.trim().length) {
            return;
        }
        if (templates.find((t: Template) => t.name === value && t.id !== currentTemplate?.id)) {
            setNameExistsError(`A template named "${value.toLowerCase()}" already exists`);
            return;
        };
        setLoaderVisible(true);

        if (!currentTemplate) {
            await addNewTemplate(value);
            toggleRename();
            setLoaderVisible(false);
            setAddRequestSent(false);
            return;
        }

        const renamed = await TemplateActions.renameTemplate('1', currentTemplate?.id, value);
        if (renamed) {
            let updatedTemplates: any = await TemplateActions.fetchTemplates('1');
            if (updatedTemplates) {
                dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });
                dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: updatedTemplates.find((ut: Template) => ut.id === currentTemplate.id) });

            }
        }
        toggleRename();
        setLoaderVisible(false);
        setAddRequestSent(false);

    }

    const toggleRename = () => {
        seteditTitleview(!editTitleview);
    }

    const removeDoc = async (templateId: string, documentId: string) => {
        setAddRequestSent(true);
        setLoaderVisible(true);
        setRemoveDocName(documentId);
        let isDeleted = await TemplateActions.deleteTemplateDocument('1', templateId, documentId);
        if (isDeleted === 200) {
            await setCurrentTemplateDocs(currentTemplate);
        }
        setLoaderVisible(false);
        setAddRequestSent(false);
    }

    const renderDocumentList = () => {
        return (
            <div className="ST-content-Wrap">
                <ul className="ul-ST-content">
                    {
                        templateDocuments?.map((td: TemplateDocument) => {
                            return (
                                <li key={td.docId}>
                                    <p>{td.docName}
                                        {
                                            ((currentTemplate?.type === MyTemplate)) &&
                                                addRequestSent && td.docId === removeDocName ?
                                                <span className="BTNloader"> 
                                                    <Spinner size="sm" animation="border" role="status">
                                                        <span className="sr-only">Loading...</span>
                                                    </Spinner>
                                                </span> : <span className="BTNclose">
                                                    <i className="zmdi zmdi-close" onClick={() => removeDoc(currentTemplate?.id, td?.docId)}></i>
                                                </span>
                                        }
                                    </p>
                                </li>
                            )
                        })
                    }

                </ul>
                {
                    currentTemplate?.type === MyTemplate &&
                    <AddDocument
                        setLoaderVisible={setLoaderVisible}
                        popoverplacement="right"
                    />
                }
            </div >
        )
    }

    const renderTitleInputText = () => {
        return (
            <div className="T-head">
                {editTitleview || currentTemplate === null ?
                    <>
                        <p className="editable">
                            <input
                                style={{ border: nameExistsError ? '1px solid red' : '' }}
                                autoFocus
                                value={newNameText}
                                onChange={({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
                                    // console.log(letterNumber.test(e.target.value));
                                    if (!nameTest.test(value)) {
                                        return;
                                    }
                                    setAddRequestSent(false);
                                    setLoaderVisible(false);
                                    setNameExistsError('');
                                    setNewNameText(value)
                                }}
                                onKeyDown={(e: any) => {
                                    if (e.keyCode === 13) {
                                        renameTemplate(e.target.value);
                                    }
                                }}
                                onBlur={() => renameTemplate(newNameText)}
                                className="editable-TemplateTitle" />
                            <br />
                            {/* <span className="editsaveicon" onClick={() => renameTemplate(newNameText)}><img src={checkicon} alt="" /></span> */}
                            {nameExistsError && <p className={"text-danger"}>{nameExistsError}</p>}
                        </p>
                    </>
                    : <>
                        <p> {currentTemplate?.name} {currentTemplate?.type === MyTemplate && <span className="editicon" onClick={toggleRename}><img src={EditIcon} alt="" /></span>}</p>
                    </>}
            </div>
        )
    }

    if (!templates) return <Loader containerHeight={"100%"} />;

    return (
        <section className="veiw-SelectedTemplate">

            {renderTitleInputText()}

            {(templates && !currentTemplate || templateDocuments?.length === 0) &&
                <NewTemplate
                    setLoaderVisible={setLoaderVisible} />}

            {templateDocuments?.length ? renderDocumentList() : <Loader containerHeight={"100%"} />}


            {/* {loaderVisible ? <h2>...your request is in process please wait...</h2> : ''} */}
        </section>
    )
}
