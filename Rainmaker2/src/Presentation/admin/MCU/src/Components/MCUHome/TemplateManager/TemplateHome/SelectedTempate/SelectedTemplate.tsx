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
import { MyTemplate, TemplateListContainer } from '../TemplateListContainer/TemplateListContainer'
import { nameTest } from '../TemplateHome';
import Spinner from 'react-bootstrap/Spinner'
import { Loader } from "../../../../../Shared/components/loader";
import { trim } from 'lodash'

type SelectedTemplateType = {
    loaderVisible: boolean;
    setLoaderVisible: Function;
    listContainerElRef: any
}

export const SelectedTemplate = ({ loaderVisible, setLoaderVisible, listContainerElRef }: SelectedTemplateType) => {

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
    }, [editTitleview]);

    useEffect(() => {
        setNameExistsError('');
    }, [currentTemplate?.name]);

    useEffect(() => {
        if (currentTemplate) {
            seteditTitleview(false);
        }

        if (!currentTemplate) {
            seteditTitleview(false);
            // setNewNameText('');
        }
        setCurrentTemplateDocs(currentTemplate)
    }, [templateDocuments?.length, currentTemplate?.id]);

    useEffect(() => {
        let nameUsed = templates?.filter((t: Template) => t.name.toLowerCase().includes('new template') && !isNaN(parseInt(t.name.split(' ')[2])))?.length;

        let name = `New Template ${nameUsed === 0 ? '' : nameUsed}`.trimEnd();

        if (templates?.find((t: Template) => t?.name === name)) {
            setNewNameText(`New Template ${nameUsed + 1}`.trimEnd())
            return;
        }

        setNewNameText(name);
    }, [!currentTemplate])


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
        dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null });
        let insertedTemplate = await TemplateActions.insertTemplate('1', name);
        if (insertedTemplate) {

            let updatedTemplates: any = await TemplateActions.fetchTemplates('1');
            dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });

            let currentTemplate = updatedTemplates.find((t: Template) => t.name === name);
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: currentTemplate });
            if (listContainerElRef?.current) {
                listContainerElRef.current.scrollTo(0, listContainerElRef.current?.children[0]?.clientHeight + 40);
            }
        }
    }

    const renameTemplate = async (value: string) => {

        if (value === currentTemplate?.name) {
            toggleRename();
            return;
        }

        if (addRequestSent) {
            return;
        }

        if (!value?.trim()?.length) {
            setNameExistsError('Name cannot be empty');
            return;
        }
        
        if(value?.length > 255) {
            setNameExistsError('Name must be less than 256 chars');
            return;
        }

        if (templates.find((t: Template) => t.name === value && t.id !== currentTemplate?.id)) {
            setNameExistsError(`A template named "${value.toLowerCase()}" already exists`);
            return;
        };

        setAddRequestSent(true);
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
                                    <p title={td.docName}>{td?.docName}
                                        {
                                            ((currentTemplate?.type === MyTemplate)) &&
                                                addRequestSent && td.docId === removeDocName ?
                                                <span className="BTNloader">
                                                    <Spinner size="sm" animation="border" role="status">
                                                        <span className="sr-only">Loading...</span>
                                                    </Spinner>
                                                </span> : currentTemplate?.type === MyTemplate && <span title="Remove" className="BTNclose">
                                                    <i className="zmdi zmdi-close" onClick={() => removeDoc(currentTemplate?.id, td?.docId)}></i>
                                                </span>
                                        }
                                    </p>
                                </li>
                            )
                        })
                    }

                </ul>

            </div >
        )
    }

    const renderTitleInputText = () => {
        return (
            <div className="T-head">
                <div className="T-head-flex">
                    <div className="titleWrap">
                        {editTitleview || currentTemplate === null ?
                            <>
                                <p className="editable">
                                    <input
                                        autoFocus
                                        onFocus={(e: any) => {
                                            let target = e.target;
                                            setTimeout(() => {
                                                target?.select();
                                            }, 0);
                                        }}
                                        style={{ border: nameExistsError ? '1px solid red' : '' }}
                                        value={newNameText}
                                        onChange={({ target: { value } }: ChangeEvent<HTMLInputElement>) => {
                                            setNewNameText(value);

                                            if (!value?.length || value?.length > 255) {
                                                return;
                                            }
                                            if (!nameTest.test(value)) {
                                                return;
                                            }
                                            setAddRequestSent(false);
                                            setLoaderVisible(false);
                                            setNameExistsError('');
                                            setNewNameText(value);
                                        }}
                                        onKeyDown={(e: any) => {
                                            if (e.keyCode === 13) {
                                                renameTemplate(e.target.value);
                                                setNewNameText(e.target.value);
                                            }
                                        }}
                                        onBlur={() => renameTemplate(newNameText)}
                                        className="editable-TemplateTitle" />
                                    {addRequestSent ?
                                        <div className="rename-spinner">
                                            <Spinner size="sm" animation="border" role="status">
                                                <span className="sr-only">Loading...</span>
                                            </Spinner>
                                        </div> : ''}
                                    {/* <span className="editsaveicon" onClick={() => renameTemplate(newNameText)}><img src={checkicon} alt="" /></span> */}
                                    {nameExistsError && <span className={"error-name"}>{nameExistsError}</span>}
                                </p>
                            </>
                            : <>
                                <p className="title"> {currentTemplate?.name} {currentTemplate?.type === MyTemplate && <span title="Rename" className="editicon" onClick={toggleRename}><img src={EditIcon} alt="" /></span>}</p>
                            </>}
                    </div>
                    <div>
                        {
                            currentTemplate?.type === MyTemplate &&
                            <AddDocument
                                setLoaderVisible={setLoaderVisible}
                                popoverplacement="bottom-start"
                            />
                        }
                    </div>
                </div>
            </div>
        )
    }

    if (!templates) return <Loader containerHeight={"100%"} />;

    return (
        <section className="veiw-SelectedTemplate">

            {renderTitleInputText()}

            {(templates && !currentTemplate || templateDocuments?.length === 0) ?
                <NewTemplate
                    setLoaderVisible={setLoaderVisible} /> : currentTemplate && templateDocuments?.length ? renderDocumentList() : <Loader containerHeight={"100%"} />}

            {/* {loaderVisible ? <h2>...your request is in process please wait...</h2> : ''} */}
        </section>
    )
}
