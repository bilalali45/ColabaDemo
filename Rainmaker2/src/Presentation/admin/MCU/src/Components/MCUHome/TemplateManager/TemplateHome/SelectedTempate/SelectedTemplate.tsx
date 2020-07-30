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
import { LocalDB } from '../../../../../Utils/LocalDB'
import { Document } from '../../../../../Entities/Models/Document'

type SelectedTemplateType = {
    loaderVisible: boolean;
    setLoaderVisible: Function;
    listContainerElRef: any
}

export const SelectedTemplate = ({ loaderVisible, setLoaderVisible, listContainerElRef }: SelectedTemplateType) => {

    const { state, dispatch } = useContext(Store);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [newNameText, setNewNameText] = useState<string>('');
    const [nameError, setNameError] = useState<string>()
    const [addRequestSent, setAddRequestSent] = useState<boolean>(false);
    const [removeDocName, setRemoveDocName] = useState<string>();
    // const [showSpecialCharsError, setShowSpecialCharsError] = useState<boolean>(error);


    const templateManager: any = state.templateManager;
    const currentTemplate = templateManager?.currentTemplate;
    const templates = templateManager?.templates;
    const templateDocuments = templateManager?.templateDocuments;
    const categoryDocuments = templateManager?.categoryDocuments;

    useEffect(() => {
        setNewNameText(currentTemplate?.name)
    }, [editTitleview]);

    useEffect(() => {
        setNameError('');
    }, [currentTemplate?.name]);

    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }

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

        if (templates?.find((t: Template) => t?.name?.trim() === name?.trim())) {
            setNewNameText(`New Template ${nameUsed + 1}`.trimEnd())
            return;
        }

        setNewNameText(name);
    }, [!currentTemplate]);


    useEffect(() => {
        if (!nameTest.test(newNameText)) {
            console.log('in here you know where ...');
            setNameError('Template name cannot contain any special characters');
        }

        if (!newNameText?.trim()?.length) {
            setNameError('');
        }
    }, [newNameText]);

    const fetchCurrentCatDocs = async () => {
        let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();
        if (currentCatDocs) {
            dispatch({ type: TemplateActionsType.SetCategoryDocuments, payload: currentCatDocs });

            // setCurrentDocType(currentCatDocs[0]);
        }
    }

    const addDocumentToList = async (doc: Document, type: string) => {
        
        try {
            let success = await TemplateActions.addDocument(LocalDB.getTenantId(), currentTemplate?.id, doc?.docTypeId || doc?.docType, type);
            if (success) {
                let docs = await TemplateActions.fetchTemplateDocuments(currentTemplate?.id);
                dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: docs });
            }
        } catch (error) {

        }
    }


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
        let insertedTemplate = await TemplateActions.insertTemplate(LocalDB.getTenantId(), name);
        if (insertedTemplate) {

            let updatedTemplates: any = await TemplateActions.fetchTemplates(LocalDB.getTenantId());
            dispatch({ type: TemplateActionsType.SetTemplates, payload: updatedTemplates });

            let currentTemplate = updatedTemplates.find((t: Template) => t.name === name);
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: currentTemplate });
            if (listContainerElRef?.current) {
                listContainerElRef.current.scrollTo(0, listContainerElRef.current?.children[0]?.clientHeight + 40);
            }
        }
    }

    const renameTemplate = async (value: string) => {

        if (!nameTest.test(value.trim())) {
            return;
        }

        if (value === currentTemplate?.name) {
            toggleRename();
            return;
        }

        if (addRequestSent) {
            return;
        }

        if (!value?.trim()?.length) {
            setNameError('Template name cannot be empty');
            return;
        }

        if (value?.length > 255) {
            setNameError('Name must be less than 256 chars');
            return;
        }

        if (templates.find((t: Template) => t.name.trim() === value.trim() && t.id !== currentTemplate?.id)) {
            setNameError(`Template name must be unique`);
            return;
        };

        setAddRequestSent(true);
        setLoaderVisible(true);
        console.log(value.trim().length);
        if (!currentTemplate) {
            await addNewTemplate(value.trim());
            toggleRename();
            setLoaderVisible(false);
            setAddRequestSent(false);
            return;
        }

        const renamed = await TemplateActions.renameTemplate(LocalDB.getTenantId(), currentTemplate?.id, value?.trim());
        if (renamed) {
            let updatedTemplates: any = await TemplateActions.fetchTemplates(LocalDB.getTenantId());
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
        let isDeleted = await TemplateActions.deleteTemplateDocument(LocalDB.getTenantId(), templateId, documentId);
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
                                        maxLength={255}
                                        autoFocus
                                        onFocus={(e: any) => {
                                            let target = e.target;
                                            setTimeout(() => {
                                                target?.select();
                                            }, 0);
                                        }}
                                        placeholder="New Template"
                                        className={`editable-TemplateTitle ${nameError ? 'error' : ''}`}
                                        value={newNameText}
                                        onChange={({ target: { value } }: ChangeEvent<HTMLInputElement>) => {

                                            setNewNameText(value);
                                            if (!value?.length || value?.length > 255) {
                                                return;
                                            }

                                            setAddRequestSent(false);
                                            setLoaderVisible(false);
                                            setNameError('');

                                            setNewNameText(value);
                                        }}
                                        onKeyDown={(e: any) => {
                                            if (e.keyCode === 13) {
                                                renameTemplate(e.target.value);
                                                setNewNameText(e.target.value);
                                            }
                                        }}
                                        onBlur={() => renameTemplate(newNameText)} />
                                    {addRequestSent ?
                                        <div className="rename-spinner">
                                            <Spinner size="sm" animation="border" role="status">
                                                <span className="sr-only">Loading...</span>
                                            </Spinner>
                                        </div> : ''}
                                    {/* <span className="editsaveicon" onClick={() => renameTemplate(newNameText)}><img src={checkicon} alt="" /></span> */}
                                    {nameError && <label className={"error"}>{nameError}</label>}
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
                                addDocumentToList={addDocumentToList}
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
