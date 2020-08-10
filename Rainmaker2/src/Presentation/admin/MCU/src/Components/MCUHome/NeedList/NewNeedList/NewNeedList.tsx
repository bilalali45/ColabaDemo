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
import { v4 } from "uuid";

export const NewNeedList = () => {

    const [currentDocument, setCurrentDocument] = useState<TemplateDocument | null>(null);
    const [allDocuments, setAllDocuments] = useState<TemplateDocument[]>([]);
    const [draftDocuments, setDraftDocuments] = useState<TemplateDocument[]>([]);
    const [customDocuments, setCustomDocuments] = useState<TemplateDocument[]>([]);
    const { state, dispatch } = useContext(Store);
    const [templateName, setTemplateName] = useState<string>('');
    const [showReview, setShowReview] = useState<boolean>(false);
    const [requestSent, setRequestSent] = useState<boolean>(false);


    const templateManager: any = state?.templateManager;
    const needListManager: any = state?.needListManager;
    const isDocumentDraft = templateManager?.isDocumentDraft;
    const emailBody = templateManager?.emailContent;
    const templateIds = needListManager?.templateIds || [];
    const categoryDocuments = templateManager?.categoryDocuments;
    const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments || [];
    const selectedIds: string[] = needListManager?.templateIds;
    const loanInfo: string[] = needListManager?.loanInfo;
    const isDraft: string = needListManager?.isDraft;
    const templates: Template[] = templateManager?.templates;
    const [showSendButton, setShowSendButton] = useState<boolean>(false);
    const emailContent: string = templateManager?.emailContent;
    const [documentHash, setDocumentHash] = useState<string>();

    const history = useHistory();
    const location = useLocation();

    useEffect(() => {
        console.log(isDocumentDraft, 'isDocumentDraft');
        if (!isDocumentDraft) {
            checkIsDocumentDraft(LocalDB.getLoanAppliationId());
        }


        if (!loanInfo) {
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


    }, [selectedTemplateDocuments?.length]);


    useEffect(() => {
        if (isDocumentDraft?.requestId && !selectedTemplateDocuments?.length) {
            fetchDraftDocuments();
        } else {
            if (selectedIds) {
                getDocumentsFromSelectedTemplates(selectedIds)
            }
        }

    }, [selectedIds?.length])

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


    const checkIsDocumentDraft = async (id: string) => {
        let res: any = await TemplateActions.isDocumentDraft(id);
        if (res.requestId) {
            fetchDraftDocuments();
        }
        dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: res });

        //  if(result?.requestId){
        //     setIsDraft('true')
        //  }else{
        //     setIsDraft('false')
        //  }
    }

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
        setRequestSent(true);
        let documents: any = await NewNeedListActions.getDocumentsFromSelectedTemplates(ids);
        documents = documents?.map((d: any) => {
            return {
                localId: v4(),
                typeId: d.typeId,
                docName: d.docName,
                docMessage: d.docMessage,
                docId: null,
                requestId: null
            }
        })
        console.log(documents, 'documents');
        let data = documents?.map((obj: TemplateDocument) => {
            return {
                ...obj,
                isRejected: false
            }
        }) || [];
        data = [...draftDocuments, ...customDocuments, ...data];
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
        setRequestSent(false);
    }

    const fetchDraftDocuments = async () => {
        setRequestSent(true);
        let documents: any = await NewNeedListActions.getDraft(LocalDB.getLoanAppliationId());
        const data = documents?.map((obj: any) => ({ ...obj, isRejected: false, localId: v4(), }))
        setDraftDocuments(data);
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: data })
        setRequestSent(false);
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
            localId: v4(),
            docId: null,
            requestId: null,
            typeId: doc.docTypeId,
            docName: doc?.docType,
            docMessage: doc?.docMessage,
        }
        let newDocs = [...allDocuments, newDoc];
        setCustomDocuments([...customDocuments, newDoc]);
        setAllDocuments(newDocs);
        dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: newDocs });
        setCurrentDocument(newDoc);

    }

    const saveAsDraft = async (toDraft: boolean) => {
        await NewNeedListActions.saveNeedList(LocalDB.getLoanAppliationId(), toDraft, emailContent || '', allDocuments)
        if (toDraft) {
            history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
        } else {
            setShowSendButton(true)
            setTimeout(() => {
                history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
            }, 1000)
        }

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
        setCustomDocuments([]);
        setDraftDocuments([]);
        let id = await NewNeedListActions.saveAsTemplate(templateName, allDocuments);
        dispatch({ type: TemplateActionsType.SetTemplates, payload: null });
        dispatch({ type: NeedListActionsType.SetTemplateIds, payload: [id] });
        setTemplateName('');
    }

    const removeDocumentFromList = async (localId: string) => {
        let prevDocs = [];
        let filter = (pre: TemplateDocument[]) => pre.filter((d: TemplateDocument) => d.localId !== localId);
        await setAllDocuments(filter);
        setCustomDocuments(filter);
        setDraftDocuments(filter);
        setTimeout(() => {
            if (allDocuments.length) {
                setCurrentDocument(allDocuments[0]);
            }
        }, 1);
    }

    const toggleShowReview = () => setShowReview(!showReview)
    const setHashHandler = (hash: string) => {
        setDocumentHash(hash)
    }

    // if (!allDocuments?.length) {
    //     return '';
    // }

    return (
        <main className="NeedListAddDoc-wrap">
            {/* <NewNeedListHeader
                saveAsDraft={saveAsDraft} /> */}
            <ReviewNeedListRequestHeader
                documentList={allDocuments}
                saveAsDraft={saveAsDraft}
                showReview={showReview}
                toggleShowReview={toggleShowReview} />
            {showReview ?
                <ReviewNeedListRequestHome
                    documentList={allDocuments}
                    saveAsDraft={saveAsDraft}
                    showSendButton={showSendButton}
                    documentHash={documentHash}
                    setHash={setHashHandler}
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
                    isDraft={isDocumentDraft}
                    viewSaveDraft={viewSaveDraftHandler}
                    saveAsTemplate={saveAsTemplate}
                    templateName={templateName}
                    changeTemplateName={changeTemplateName}
                    removeDocumentFromList={removeDocumentFromList}
                    toggleShowReview={toggleShowReview}
                    requestSent={requestSent} 
                    showSaveAsTemplateLink={customDocuments?.length > 0? true : false}/>}
        </main>
    )
}