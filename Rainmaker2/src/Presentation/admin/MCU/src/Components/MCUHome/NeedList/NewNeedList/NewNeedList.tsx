import React, {
  useEffect,
  useCallback,
  useState,
  useContext,
  ChangeEvent
} from 'react';
import { Http } from 'rainsoft-js';
import Spinner from 'react-bootstrap/Spinner';
import { NewNeedListHeader } from './NewNeedListHeader/NewNeedListHeader';
import { NewNeedListHome } from './NewNeedListHome/NewNeedListHome';
import { TemplateDocument } from '../../../../Entities/Models/TemplateDocument';
import { Store } from '../../../../Store/Store';
import { TemplateActions } from '../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer';
import { Document } from '../../../../Entities/Models/Document';
import { LocalDB } from '../../../../Utils/LocalDB';
import {
  NewNeedListActions,
  DocumentsWithTemplateDetails
} from '../../../../Store/actions/NewNeedListActions';
import { Template } from '../../../../Entities/Models/Template';
import { NeedListActionsType } from '../../../../Store/reducers/NeedListReducer';
import { useHistory, useLocation } from 'react-router-dom';
import { ReviewNeedListRequestHeader } from '../../ReviewNeedListRequest/ReviewNeedListRequestHeader/ReviewNeedListRequestHeader';
import { ReviewNeedListRequestHome } from '../../ReviewNeedListRequest/ReviewNeedListRequestHome/ReviewNeedListRequestHome';
import { LoanApplication } from '../../../../Entities/Models/LoanApplication';
import { NeedListActions } from '../../../../Store/actions/NeedListActions';
import { v4 } from 'uuid';
import { template } from 'lodash';
import {
  enableBrowserPrompt,
  disableBrowserPrompt
} from '../../../../Utils/helpers/Common';

export const NewNeedList = () => {
  const [
    currentDocument,
    setCurrentDocument
  ] = useState<TemplateDocument | null>(null);
  const [allDocuments, setAllDocuments] = useState<TemplateDocument[]>([]);
  const [draftDocuments, setDraftDocuments] = useState<TemplateDocument[]>([]);
  const [customDocuments, setCustomDocuments] = useState<TemplateDocument[]>([]);
  const { state, dispatch } = useContext(Store);
  const [templateName, setTemplateName] = useState<string>('');
  const [showReview, setShowReview] = useState<boolean>(false);
  const [requestSent, setRequestSent] = useState<boolean>(false);
  const [currentDocumentIndex, setCurrentDocumentIndex] = useState<number>(0);

  const templateManager: any = state?.templateManager;
  const needListManager: any = state?.needListManager;
  const isDocumentDraft = templateManager?.isDocumentDraft;
  const emailBody = templateManager?.emailContent;
  const templateIds = needListManager?.templateIds || [];
  const loanData = needListManager?.loanInfo;
  const categoryDocuments = templateManager?.categoryDocuments;
  const currentCategoryDocuments = templateManager?.currentCategoryDocuments;
  const selectedTemplateDocuments: TemplateDocument[] =
    templateManager?.selectedTemplateDocuments || [];
  const selectedIds: string[] = needListManager?.templateIds;
  const loanInfo: string[] = needListManager?.loanInfo;
  const isDraft: string = needListManager?.isDraft;
  const templates: Template[] = templateManager?.templates;
  const [showSendButton, setShowSendButton] = useState<boolean>(true);
  const emailContent: string = templateManager?.emailContent;
  const [documentHash, setDocumentHash] = useState<string>();
  const [emailTemplate, setEmailTemplate] = useState<string>();
  const [documentsName, setDocumentName] = useState<string>();
  const [defaultEmail, setDefaultEmail] = useState<string>();
  const borrowername = loanData?.borrowers[0];
  const history = useHistory();
  const location = useLocation();

  useEffect(() => {
    if (!isDocumentDraft) {
      checkIsDocumentDraft(LocalDB.getLoanAppliationId());
    }
    if (!loanInfo) {
      fetchLoanApplicationDetail();
    }
    setTimeout(() => {
      getEmailTemplate();
    }, 1000);
    return () => {
      clearOldData();
    };
  }, []);

  useEffect(() => {
    getDocumentsName();
  }, [allDocuments?.length]);

  useEffect(() => {
    setDeafultText();
  }, [documentsName]);

  useEffect(() => {
    if (allDocuments) {
      setDeafultText();
    }
  }, [emailTemplate || documentsName]);

  useEffect(() => {
    if (!categoryDocuments) {
      fetchCurrentCatDocs();
    }

    setAllDocuments(selectedTemplateDocuments);

    if (selectedTemplateDocuments?.length && currentDocument === null) {
      setCurrentDocument(selectedTemplateDocuments[0]);
    }
  }, [selectedTemplateDocuments?.length]);

  useEffect(() => {
    if (isDocumentDraft?.requestId && !selectedTemplateDocuments?.length) {
      fetchDraftDocuments();
    }
  }, []);

  useEffect(() => {
    if (!selectedTemplateDocuments?.length && selectedIds) {
      fetchTemplateDocs(selectedIds);
    }
  }, []);

  const clearOldData = () => {
    setCurrentDocument(null);
    setAllDocuments([]);
    setTemplateName('');
    dispatch({ type: TemplateActionsType.SetTemplates, payload: null });
    dispatch({ type: NeedListActionsType.SetTemplateIds, payload: null });
    dispatch({ type: TemplateActionsType.SetEmailContent, payload: null });
    dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: null });
    dispatch({
      type: TemplateActionsType.SetSelectedTemplateDocuments,
      payload: null
    });
    dispatch({
      type: TemplateActionsType.SetCurrentCategoryDocuments,
      payload: null
    });
    dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: null });
  };

  const fetchTemplateDocs = (idArray: string[]) => {
    if (idArray) {
      getDocumentsFromSelectedTemplates(idArray);
    }
  };

  const checkIsDocumentDraft = async (id: string) => {
    let res: any = await TemplateActions.isDocumentDraft(id);
    if (res?.requestId) {
      fetchDraftDocuments();
    }
    dispatch({ type: TemplateActionsType.SetIsDocumentDraft, payload: res });

  };

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

  const changeDocument = (d: TemplateDocument) => {
     
    let index = allDocuments.findIndex((doc) => doc.localId === d?.localId);
    if(index === allDocuments.length - 1) {
      index = 0;
    }
    setCurrentDocumentIndex(index);
    setCurrentDocument(d);
  };

  const changeTemplateName = (e: ChangeEvent<HTMLInputElement>) =>
    setTemplateName(e.target.value);

  const getDocumentsFromSelectedTemplates = async (ids: string[]) => {
    setRequestSent(true);
    let allTemplateDocs: any[] = [];
    let documentsWithTemplate:
      | DocumentsWithTemplateDetails[]
      | undefined = await NewNeedListActions.getDocumentsFromSelectedTemplates(
        ids
      );
    if (documentsWithTemplate) {
      for (const template of documentsWithTemplate) {
        let docs = template?.docs || [];
        for (const d of docs) {
          let exists = allTemplateDocs?.find(
            (pd: TemplateDocument) =>
              pd.docName?.toLowerCase() === d.docName?.toLowerCase()
          );

          if (!exists) {
            allTemplateDocs.push({
              localId: v4(),
              typeId: d.typeId,
              docName: d.docName,
              docMessage: d.docMessage,
              docId: null,
              requestId: null,
              templateId: template?.id,
              isRejected: false
            });
          }
        }
      }
    }

    let data: any = [...draftDocuments, ...customDocuments, ...allTemplateDocs];
    setAllDocuments(data);

    dispatch({
      type: TemplateActionsType.SetSelectedTemplateDocuments,
      payload: data
    });
    setRequestSent(false);
  };

  const fetchDraftDocuments = async () => {
    setRequestSent(true);
    let documents: any = await NewNeedListActions.getDraft(
      LocalDB.getLoanAppliationId()
    );
    const data = documents?.map((obj: any) => ({
      ...obj,
      isRejected: false,
      localId: v4()
    }));
    setDraftDocuments(data);
    dispatch({
      type: TemplateActionsType.SetSelectedTemplateDocuments,
      payload: data
    });
    setRequestSent(false);
  };

  const updateDocumentMessage = (
    message: string,
    document: TemplateDocument
  ) => {
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
    enableBrowserPrompt();
  };

  const fetchCurrentCatDocs = async () => {
    let currentCatDocs: any = await TemplateActions.fetchCategoryDocuments();
    if (currentCatDocs) {
      dispatch({
        type: TemplateActionsType.SetCategoryDocuments,
        payload: currentCatDocs
      });
    }
  };

  const addDocumentToList = (doc: any, type: string) => {
    let newDoc: any = {
      localId: v4(),
      docId: null,
      requestId: null,
      typeId: doc.docTypeId,
      docName: doc?.docType,
      isCustom: doc?.isCustom,
      docMessage: doc?.docMessage
    };

    let newDocs = [...allDocuments, newDoc];
    setCustomDocuments([...customDocuments, newDoc]);
    setAllDocuments(newDocs);
    dispatch({
      type: TemplateActionsType.SetSelectedTemplateDocuments,
      payload: newDocs
    });
    dispatch({
      type: TemplateActionsType.SetIsDocumentDraft,
      payload: { requestId: null }
    });
    setCurrentDocument(newDoc);
    enableBrowserPrompt();
  };

  const saveAsDraft = async (toDraft: boolean) => {
    let body =
      toDraft === false ? emailContent?.replace(/\n/g, '<br />') : emailContent;
    await NewNeedListActions.saveNeedList(
      LocalDB.getLoanAppliationId(),
      toDraft,
      body || '',
      allDocuments
    );
    if (toDraft) {
      history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
    } else {
      setShowSendButton(false);
      setTimeout(() => {
        history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
      }, 1000);
    }
    disableBrowserPrompt();
  };

  const addTemplatesDocuments = (idArray: string[]) => {
    if (!idArray) {
      idArray = [];
    }
    dispatch({ type: NeedListActionsType.SetTemplateIds, payload: idArray });
    if (!location.pathname.includes('newNeedList')) {
      history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
    }
    dispatch({
      type: TemplateActionsType.SetIsDocumentDraft,
      payload: { requestId: null }
    });
    enableBrowserPrompt();
  };

  const editcustomDocName = (doc: TemplateDocument) => {
    setAllDocuments((pre: TemplateDocument[]) => {
      setCurrentDocument(null);
      return pre?.map((pt: TemplateDocument) => {
        if (pt?.localId === doc?.localId) {
          return doc;
        }
        return pt;
      });
    });
    setCurrentDocument(doc);
    enableBrowserPrompt();
  };

  const viewSaveDraftHandler = () => {
    dispatch({ type: NeedListActionsType.SetIsDraft, payload: true });
    history.push(`/needList/${LocalDB.getLoanAppliationId()}`);
  };

  const saveAsTemplate = async () => {
    setCustomDocuments([]);
    setDraftDocuments([]);
    let id = await NewNeedListActions.saveAsTemplate(
      templateName,
      allDocuments
    );
    dispatch({ type: TemplateActionsType.SetTemplates, payload: null });
    dispatch({ type: NeedListActionsType.SetTemplateIds, payload: [id] });
    setTemplateName('');
    enableBrowserPrompt();
  };

  const removeDocumentFromList = async (doc: TemplateDocument) => {
    let prevDocs = [];
    let filter = (pre: TemplateDocument[]) => {
      let updatedDocList = pre.filter((d: TemplateDocument) => d.localId !== doc?.localId);
      if (updatedDocList?.length) {
        setCurrentDocument(updatedDocList[currentDocumentIndex]);
      }
      return updatedDocList;
    };
    if (selectedIds?.length) {
      let updatedTemplateIds = selectedIds?.filter(
        (id: any) => id !== doc?.templateId
      );
      dispatch({
        type: NeedListActionsType.SetTemplateIds,
        payload: updatedTemplateIds
      });
    }
    // debugger
    await setCustomDocuments(filter);
    await setDraftDocuments(filter);
    await setAllDocuments(filter);
    dispatch({
      type: TemplateActionsType.SetIsDocumentDraft,
      payload: { requestId: null }
    });
    enableBrowserPrompt();
  };

  const toggleShowReview = () => setShowReview(!showReview);
  const setHashHandler = (hash: string) => {
    setDocumentHash(hash);
  };

  const getEmailTemplate = async () => {
    let res: any = await TemplateActions.fetchEmailTemplate();
    setEmailTemplate(res);
  };

  const getDocumentsName = () => {
    if (!allDocuments) return;
    let names: string = '';

    for (let i = 0; i < allDocuments.length; i++) {
      names += '-' + allDocuments[i].docName;
      if (i != allDocuments.length - 1) names = names + ',';
    }
    setDocumentName(names);
  };

  const setDeafultText = () => {
    let str: string = '';
    let payload = LocalDB.getUserPayload();
    let mcuName = payload?.FirstName + ' ' + payload?.LastName;
    let documentNames = documentsName
      ? documentsName?.split(',').join(' \r\n')
      : '';
    if (emailTemplate != undefined) {
      str = emailTemplate
        .replace('{user}', borrowername)
        .replace('{documents}', documentNames)
        // .replace('{mcu}', mcuName); because we will provide Business Unit Name from BE while emailing
      enableBrowserPrompt();
      setDefaultEmail(str);
      dispatch({
        type: TemplateActionsType.SetEmailContent,
        payload: str
      });
    }
  };

  return (
    <main className="NeedListAddDoc-wrap">
      {/* <NewNeedListHeader
                saveAsDraft={saveAsDraft} /> */}
      <ReviewNeedListRequestHeader
        documentList={allDocuments}
        saveAsDraft={saveAsDraft}
        showReview={showReview}
        toggleShowReview={toggleShowReview}
      />
      {showReview ? (
        <ReviewNeedListRequestHome
          documentList={allDocuments}
          saveAsDraft={saveAsDraft}
          showSendButton={showSendButton}
          documentHash={documentHash}
          setHash={setHashHandler}
          defaultEmail={defaultEmail}
        />
      ) : (
          <NewNeedListHome
            addDocumentToList={addDocumentToList}
            currentDocument={currentDocument}
            changeDocument={changeDocument}
            allDocuments={allDocuments}
            updateDocumentMessage={updateDocumentMessage}
            templateList={templates?.filter(
              (td: Template) => !templateIds?.includes(td?.id)
            )}
            addTemplatesDocuments={addTemplatesDocuments}
            isDraft={isDocumentDraft}
            viewSaveDraft={viewSaveDraftHandler}
            saveAsTemplate={saveAsTemplate}
            templateName={templateName}
            setTemplateName={setTemplateName}
            changeTemplateName={changeTemplateName}
            removeDocumentFromList={removeDocumentFromList}
            toggleShowReview={toggleShowReview}
            requestSent={requestSent}
            showSaveAsTemplateLink={Boolean(
              customDocuments?.length || selectedIds?.length > 1
            )}
            fetchTemplateDocs={fetchTemplateDocs}
            editcustomDocName={editcustomDocName}
          />
        )}
    </main>
  );
};
