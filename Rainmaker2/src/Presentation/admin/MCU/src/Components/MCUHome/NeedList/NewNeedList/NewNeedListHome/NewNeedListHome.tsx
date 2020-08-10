import React, { useEffect, useCallback, useState, useContext } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
import { NeedListRequest } from "./NeedListRequest/NeedListRequest";
import { NeedListContent } from "./NeedListContent/NeedListContent";
import { Store } from "../../../../../Store/Store";
import { TemplateDocument } from "../../../../../Entities/Models/TemplateDocument";
import { Template } from "../../../../../Entities/Models/Template";
import { TemplateActions } from "../../../../../Store/actions/TemplateActions";
import { TemplateActionsType, isDocumentDraftType } from "../../../../../Store/reducers/TemplatesReducer";
import { Document } from "../../../../../Entities/Models/Document";

type NewNeedListHomeType = {
    currentDocument: TemplateDocument | null,
    allDocuments: TemplateDocument[],
    addDocumentToList: Function,
    changeDocument: Function,
    updateDocumentMessage: Function,
    templateList: Template[],
    addTemplatesDocuments: Function,
    isDraft: isDocumentDraftType,
    viewSaveDraft: Function,
    saveAsTemplate: Function,
    templateName: string,
    changeTemplateName: Function,
    removeDocumentFromList: Function,
    toggleShowReview: Function,
    requestSent: boolean,
    showSaveAsTemplateLink: boolean
}

export const NewNeedListHome = ({
    addDocumentToList,
    currentDocument,
    changeDocument,
    allDocuments,
    updateDocumentMessage,
    templateList,
    addTemplatesDocuments,
    isDraft,
    viewSaveDraft,
    saveAsTemplate,
    changeTemplateName,
    templateName,
    removeDocumentFromList,
    toggleShowReview,
    requestSent,
    showSaveAsTemplateLink
}: NewNeedListHomeType) => {
    const [loaderVisible, setLoaderVisible] = useState<boolean>(false);

    return (
        <section className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                            <NeedListRequest
                                requestSent={requestSent}
                                addDocumentToList={addDocumentToList}
                                currentDocument={currentDocument}
                                changeDocument={changeDocument}
                                documentList={allDocuments}
                                setLoaderVisible={setLoaderVisible}
                                loaderVisible={loaderVisible}
                                templateList={templateList}
                                addTemplatesDocuments={addTemplatesDocuments}
                                viewSaveDraft={viewSaveDraft}
                                isDraft={isDraft}
                                saveAsTemplate={saveAsTemplate}
                                templateName={templateName}
                                changeTemplateName={changeTemplateName}
                                removeDocumentFromList={removeDocumentFromList}
                                showSaveAsTemplateLink={showSaveAsTemplateLink}
                            />

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                            <NeedListContent
                                isDraft={isDraft}
                                updateDocumentMessage={updateDocumentMessage}
                                document={!allDocuments?.length ? null : currentDocument}
                                toggleShowReview={toggleShowReview} />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}