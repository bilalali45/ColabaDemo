import React, { useEffect, useCallback, useState, useContext, ChangeEvent } from "react";
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
import { ReviewNeedListRequestHeader } from "../../ReviewNeedListRequest/ReviewNeedListRequestHeader/ReviewNeedListRequestHeader";
import { ReviewNeedListRequestHome } from "../../ReviewNeedListRequest/ReviewNeedListRequestHome/ReviewNeedListRequestHome";
import { LoanApplication } from "../../../../Entities/Models/LoanApplication";
import { NeedListActions } from "../../../../Store/actions/NeedListActions";

export const NewNeedList = () => {

    const [currentDocument, setCurrentDocument] = useState<TemplateDocument | null>(null);
    const [allDocuments, setAllDocuments] = useState<TemplateDocument[]>([])
    const { state, dispatch } = useContext(Store);
    const [templateName, setTemplateName] = useState<string>('');
    const [showReview, setShowReview] = useState<boolean>(false);


    const templateManager: any = state?.templateManager;
    const needListManager: any = state?.needListManager;
    const isDocumentDraft = templateManager?.isDocumentDraft;
    const templateIds = needListManager?.templateIds || [];
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments || [];
    const selectedIds: string[] = needListManager?.templateIds;
    const loanInfo: string[] = needListManager?.loanInfo;
    const isDraft: string = needListManager?.isDraft;
    const templates: Template[] = templateManager?.templates;
    const emailContent: string = templateManager?.emailContent;

    const history = useHistory();
    const location = useLocation();

    useEffect(() => {

        if(!loanInfo) {
            fetchLoanApplicationDetail();
        }

        return () => {
            clearOldData()
        }
    }, []);

    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }

        setAllDocuments(selectedTemplateDocuments);

        if (selectedTemplateDocuments?.length) {
            setCurrentDocument(selectedTemplateDocuments[0]);
        }


    }, [selectedTemplateDocuments?.length, templateIds?.length]);

    const clearOldData = () => {
        setCurrentDocument(null);
        setAllDocuments([]);
        setTemplateName('');
        dispatch({ type: TemplateActionsType.SetTemplates, payload: null })
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: null });
        dispatch({ type: TemplateActionsType.SetEmailContent, payload: null })
        dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: null })
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: null })
        dispatch({ type: TemplateActionsType.SetCurrentCategoryDocuments, payload: null })
        dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: null })
    }


    useEffect(() => {
        if (isDocumentDraft?.requestId) {
            fetchDraftDocuments();
        } else {
            getDocumentsFromSelectedTemplates(selectedIds)
        }

    }, [selectedIds?.length])

    // useEffect(() => {

    // }, [allDocuments?.length])

    const fetchLoanApplicationDetail = async () => {
        let applicationId = LocalDB.getLoanAppliationId();
        if (applicationId) {
          let res:
            | LoanApplication
            | undefined = await NeedListActions.getLoanApplicationDetail(
            applicationId
          );
          if (res) {
            dispatch({ type: NeedListActionsType.SetLoanInfo, payload: res });
            // setLoanInfo(res)
          }
        }
      };

    const changeDocument = (d: TemplateDocument) => setCurrentDocument(d);

    const changeTemplateName = (e: ChangeEvent<HTMLInputElement>) => setTemplateName(e.target.value);


    const getDocumentsFromSelectedTemplates = async (ids: string[]) => {
        let documents: any = await NewNeedListActions.getDocumentsFromSelectedTemplates(ids)
        const data = documents?.map((obj: TemplateDocument) => {
            return {
                ...obj,
                docMessage: allDocuments?.find((d: TemplateDocument) => d.docId === obj.docId)?.docMessage,
                isRejected: false
            }
        }) || [];
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
    }

    const fetchDraftDocuments = async () => {
        let documents: any = await NewNeedListActions.getDraft(LocalDB.getLoanAppliationId());
        const data = documents?.map((obj: any) => ({ ...obj, isRejected: false }))
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
    }

    const updateDocumentMessage = (message: string, document: TemplateDocument) => {

        let documents: TemplateDocument[] = [];
        setAllDocuments((preDocs: TemplateDocument[]) => {
            documents = preDocs?.map((pd: TemplateDocument) => {
                if (pd?.docName === document?.docName) {
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
        }
    }

    const addDocumentToList = (doc: Document, type: string) => {

        let newDoc: any = {
            docId: null,
            requestId: null,
            typeId: doc.docTypeId,
            docName: doc?.docType,
            docMessage: doc?.docMessage,
        }

        let newDocs = [...allDocuments, newDoc];
        setAllDocuments(newDocs);
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: newDocs });
        setCurrentDocument(newDoc);

    }

    const saveAsDraft = async (toDraft: boolean) => {
        await NewNeedListActions.saveNeedList(
            LocalDB.getLoanAppliationId(),
            toDraft,
            emailContent,
            allDocuments
        )
        history.push(`/needList/${LocalDB.getLoanAppliationId()}`);

    }

    const addTemplatesDocuments = (idArray: string[]) => {

        if (!idArray) {
            idArray = [];
        }
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: idArray });
        if (!location.pathname.includes('newNeedList')) {
            history.push(`/needList/${LocalDB.getLoanAppliationId()}`)
        }
    }

    const viewSaveDraftHandler = () => {
        dispatch({ type: NeedListActionsType.SetIsDraft, payload: true });
        history.push(`/needList/${LocalDB.getLoanAppliationId()}`)
    }

    const saveAsTemplate = async () => {
        let id = await NewNeedListActions.saveAsTemplate(templateName, allDocuments);
        dispatch({ type: TemplateActionsType.SetTemplates, payload: null });
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: [id] });
        setTemplateName('');
    }

    const removeDocumentFromList = async (docName: string) => {
        await setAllDocuments((pre: TemplateDocument[]) => pre.filter((d: TemplateDocument) => d.docName !== docName));
        setCurrentDocument(allDocuments[0]);
    }

    const toggleShowReview = () => setShowReview(!showReview)

    // if (!allDocuments?.length) {
    //     return '';
    // }

    return (
        <main className="NeedListAddDoc-wrap">
            {/* <NewNeedListHeader
                saveAsDraft={saveAsDraft} /> */}
            <ReviewNeedListRequestHeader
                saveAsDraft={saveAsDraft}
                showReview={showReview}
                toggleShowReview={toggleShowReview} />
            {showReview ?
                <ReviewNeedListRequestHome
                    documentList={allDocuments}
                    saveAsDraft={saveAsDraft}
                />
                :
                <NewNeedListHome
                    addDocumentToList={addDocumentToList}
                    currentDocument={currentDocument}
                    changeDocument={changeDocument}
                    allDocuments={allDocuments}
                    updateDocumentMessage={updateDocumentMessage}
                    templateList={templates?.filter((td: Template) => !templateIds?.includes(td?.id))}
                    addTemplatesDocuments={addTemplatesDocuments}
                    isDraft={isDraft}
                    viewSaveDraft={viewSaveDraftHandler}
                    saveAsTemplate={saveAsTemplate}
                    templateName={templateName}
                    changeTemplateName={changeTemplateName}
                    removeDocumentFromList={removeDocumentFromList}
                    toggleShowReview={toggleShowReview} />}
        </main>
    )
}