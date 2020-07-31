import React, { useEffect, useCallback, useState, useContext } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
import { NewNeedListHeader } from './NewNeedListHeader/NewNeedListHeader'
import { NewNeedListHome } from './NewNeedListHome/NewNeedListHome'
import { TemplateDocument } from "../../../../Entities/Models/TemplateDocument";
import { Store } from "../../../../Store/Store";
import { TemplateActions } from "../../../../Store/actions/TemplateActions";
import { TemplateActionsType } from "../../../../Store/reducers/TemplatesReducer";
import { Document } from "../../../../Entities/Models/Document";
import { LocalDB } from "../../../../Utils/LocalDB";
import { NewNeedListActions } from "../../../../Store/actions/NewNeedListActions";
import { Template } from "../../../../Entities/Models/Template";
import { NeedListActionsType } from "../../../../Store/reducers/NeedListReducer";
import { useHistory, useLocation } from "react-router-dom";

export const NewNeedList = () => {

    const [currentDocument, setCurrentDocument] = useState<TemplateDocument | null>(null);
    const [allDocuments, setAllDocuments] = useState<TemplateDocument[]>([])
    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;
    const needListManager: any = state?.needListManager;
    const templateIds = needListManager?.templateIds || [];
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;
    const selectedIds: string[] = needListManager?.templateIds;
    const isDraft: string = needListManager?.isDraft;
    const templates: Template[] = templateManager?.templates;

    console.log('selectedIds', selectedIds)

    const history = useHistory();
    const location = useLocation();

    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }

        setAllDocuments(selectedTemplateDocuments || []);
        if (selectedTemplateDocuments?.length) {
            setCurrentDocument(selectedTemplateDocuments[0]);
        }
    }, [selectedTemplateDocuments?.length, templateIds?.length]);


    useEffect(() => {
        if (!isDraft) {
            let tenantId = LocalDB.getTenantId();
            getDocumentsFromSelectedTemplates(selectedIds, +tenantId)
        } else {
            fetchDraftDocuments();
        }
    }, [selectedIds?.length])

    // useEffect(() => {

    // }, [allDocuments?.length])

    const changeDocument = (d: TemplateDocument) => setCurrentDocument(d);


    const getDocumentsFromSelectedTemplates = async (ids: string[], tenantId: number) => {
        let documents: any = await NewNeedListActions.getDocumentsFromSelectedTemplates(ids, tenantId)
        const data = documents?.map((obj: TemplateDocument) => {
            return {
                ...obj,
                docMessage: allDocuments?.find((d: TemplateDocument) => d.docId === obj.docId)?.docMessage,
                isRejected: false
            }
        })
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
    }

    const fetchDraftDocuments = async () => {
        let documents: any = await NewNeedListActions.getDraft(LocalDB.getLoanAppliationId(), LocalDB.getTenantId())
        const data = documents?.map((obj: any) => ({ ...obj, isRejected: false }))
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
    }

    const updateDocumentMessage = (message: string, document: TemplateDocument) => {

        let documents: TemplateDocument[] = [];
        setAllDocuments((preDocs: TemplateDocument[]) => {
            documents = preDocs?.map((pd: TemplateDocument) => {
                if (pd?.docId === document?.docId) {
                    pd.docMessage = message;
                    return pd;
                }
                return pd;
            });
            return documents;
        });

    }

    const fetchCurrentCatDocs = async () => {
        let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();
        if (currentCatDocs) {
            dispatch({ type: TemplateActionsType.SetCategoryDocuments, payload: currentCatDocs });

            // setCurrentDocType(currentCatDocs[0]);
        }
    }

    const addDocumentToList = (doc: Document, type: string) => {
        let newDoc: TemplateDocument = {
            docId: doc?.docTypeId,
            docName: doc?.docType,
            docMessage: doc?.docMessage,
        }
        // dispatch({type: NeedListActionsType.SetTemplateIds, payload: [...allDocuments, newDoc]});
        let newDocs = [...allDocuments, newDoc];
        setAllDocuments(newDocs);
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: newDocs });
        setCurrentDocument(newDoc);
    }

    const saveAsDraft = () => {
        console.log(allDocuments);
    }

    const addTemplatesDocuments = (idArray: string[]) => {
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: idArray });
        console.log('idArray', idArray);
        if (!idArray) {
            idArray = [];
        }
        let newIds = [...idArray];
        selectedIds?.forEach(id => {
            if (!newIds.includes(id)) {
                newIds.push(id)
            }
        });
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: newIds })
        if (!location.pathname.includes('newNeedList')) {
            history.push('/newNeedList');
        }
    }

    const viewSaveDraftHandler = () => {
        dispatch({ type: NeedListActionsType.SetIsDraft, payload: true });
        history.push('/newNeedList');
    }

    // if (!allDocuments?.length) {
    //     return '';
    // }

    return (
        <main className="NeedListAddDoc-wrap">
            <NewNeedListHeader
                saveAsDraft={saveAsDraft} />
            <NewNeedListHome
                addDocumentToList={addDocumentToList}
                currentDocument={currentDocument}
                changeDocument={changeDocument}
                allDocuments={allDocuments}
                updateDocumentMessage={updateDocumentMessage}
                templateList={templates?.filter((td: Template) => !templateIds?.includes(td?.id))}
                addTemplatesDocuments={addTemplatesDocuments}
                isDraft={isDraft}
                viewSaveDraft={viewSaveDraftHandler} />
        </main>
    )
}