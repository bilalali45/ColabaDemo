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

export const NewNeedList = () => {

    const [currentDocument, setCurrentDocument] = useState<TemplateDocument | null>(null);
    const [allDocuments, setAllDocuments] = useState<TemplateDocument[]>([])
    const { state, dispatch } = useContext(Store);

    const templateManager: any = state?.templateManager;
    const categoryDocuments = templateManager?.categoryDocuments;
    const selectedTemplateDocuments: TemplateDocument[] = templateManager?.selectedTemplateDocuments;

    useEffect(() => {
        if (!categoryDocuments) {
            fetchCurrentCatDocs();
        }

        setAllDocuments(selectedTemplateDocuments)
    }, [])

    const changeDocument = (d: TemplateDocument) => setCurrentDocument(d);

    const updateDocumentMessage = (message: string, document: TemplateDocument) => {
        setAllDocuments((preDocs: TemplateDocument[]) => {
            return preDocs?.map((pd: TemplateDocument) => {
                if (pd.docId === document.docId) {
                    pd.docMessage = message;
                    return pd;
                }
                return pd;
            });
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
        debugger
        let newDoc: TemplateDocument = {
            docId: doc?.docTypeId,
            docName: doc?.docType,
            docMessage: doc?.docMessage,
        }
        setAllDocuments([...allDocuments, newDoc]);
    }

    const saveAsDraft = () => {
        console.log(allDocuments);
    }

    return (
        <main className="NeedListAddDoc-wrap">
            <NewNeedListHeader 
                saveAsDraft={saveAsDraft}/>
            <NewNeedListHome 
               addDocumentToList={addDocumentToList}
               currentDocument={currentDocument}
               changeDocument={changeDocument}
               allDocuments={allDocuments}
               updateDocumentMessage={updateDocumentMessage}  />
        </main>
    )
}