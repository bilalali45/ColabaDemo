import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
// ../../../../../../Assets/images/editicon.svg
import EditIcon from '../../../../../../Assets/images/editicon.svg';
import { TemplateDocument } from "../../../../../../Entities/Models/TemplateDocument";
import { useHistory } from "react-router-dom";
import { TextArea } from "../../../../../../Shared/components/TextArea";
import { errorText } from "../../../../ReviewNeedListRequest/ReviewNeedListRequestHome/EmailContentReview/EmailContentReview";
import { isDocumentDraftType } from "../../../../../../Store/reducers/TemplatesReducer";

type NeedListContentType = {
    document: TemplateDocument | null;
    updateDocumentMessage: Function,
    toggleShowReview: Function
    isDraft: isDocumentDraftType,
    editcustomDocName: Function
}

export const NeedListContent = ({ document, updateDocumentMessage, toggleShowReview, isDraft, editcustomDocName }: NeedListContentType) => {
    const [docName, setDocName] = useState(document?.docName);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [doc, setDoc] = useState<TemplateDocument | null>(null);
    const [isValid, setIsValid] = useState<boolean>(false);
    const regex = /^[ A-Za-z0-9-,.!@#$%^&*()_+=`~{}\s]*$/i;
   
    useEffect(() => {
        setDoc(document);
    }, [doc?.docName])

    const toggleRename = () => {
        seteditTitleview(!editTitleview);
        setDocName(document?.docName);
    }

    if (!document) {
        return null;
    }


    const renderTitleInputText = () => {

        if (!document?.docName) {
            return null;
        }

        return (
            <div className="T-head">
                <div className="T-head-flex text-ellipsis">
                    <div>
                        {editTitleview ?
                            <>
                                <h3 className="editable">
                                    <input
                                        maxLength={255}
                                        autoFocus
                                        value={docName}
                                        onBlur={(e) => {
                                            setDocName(e.target.value);
                                            let updatedDoc = {
                                                ...document,
                                                docName: e.target.value
                                            }
                                            editcustomDocName(updatedDoc);
                                            toggleRename();
                                        }}
                                        onChange={(e) => {
                                            setDocName(e.target.value);
                                            let updatedDoc = {
                                                ...document,
                                                docName: e.target.value
                                            }
                                            editcustomDocName(updatedDoc);
                                        }}
                                        className="editable-TemplateTitle" />
                                </h3>
                            </>
                            : <>
                                {/* <h3><span className="text-ellipsis"> {document?.docName}</span></h3> */}
                                <h3 className="text-ellipsis" title={document?.docName}> {document?.docName}
                                    {document?.isCustom &&
                                        <span className="editicon" onClick={toggleRename} ><img src={EditIcon} alt="" /></span>
                                    }
                                </h3>
                            </>}
                    </div>
                </div>
            </div>
        )
    }

    if (!isDraft) {
        return (
            <div className="flex-center">
                <Spinner animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
            </div>
        )
    }

    // rows={6}
    return (
        <section className="veiw-SelectedTemplate">

            {renderTitleInputText()}


            <div className="mainbody">
                <p>Include a message to the borrower</p>
                <div className="editer-wrap">
                    <TextArea
                        placeholderValue={"Type your message"}
                        focus={true}
                        textAreaValue={document?.docMessage || ''}
                        onBlurHandler={() => { }}
                        errorText={errorText}
                        isValid={isValid}
                        onChangeHandler={(e: any) => updateDocumentMessage(e.target.value, document)}
                        maxLengthValue={500} />
                </div>

            </div>

            <div className="right-footer">
                <div className="btn-wrap">
                    <button onClick={(e) => toggleShowReview(e)} className="btn btn-primary">Review Request</button>

                </div>
            </div>


        </section>
    )
}