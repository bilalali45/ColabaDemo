import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
// ../../../../../../Assets/images/editicon.svg
import EditIcon from '../../../../../../Assets/images/editicon.svg';
import { TemplateDocument } from "../../../../../../Entities/Models/TemplateDocument";
import { useHistory } from "react-router-dom";
import { TextArea } from "../../../../../../Shared/components/TextArea";
import { errorText } from "../../../../ReviewNeedListRequest/ReviewNeedListRequestHome/EmailContentReview/EmailContentReview";

type NeedListContentType = {
    document: TemplateDocument | null;
    updateDocumentMessage: Function,
    toggleShowReview: Function
}

export const NeedListContent = ({ document, updateDocumentMessage, toggleShowReview }: NeedListContentType) => {
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const [doc, setDoc] = useState<TemplateDocument | null>(null);
    const [isValid, setIsValid] = useState<boolean>(false);
    const regex = /^[ A-Za-z0-9-,.!@#$%^&*()_+=`~{}\s]*$/i;
    console.log(document, 'document');
    useEffect(() => {
        setDoc(document);
    }, [doc?.docName])

    const toggleRename = () => {
        seteditTitleview(!editTitleview);
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
                <div className="T-head-flex">
                    <div>
                        {editTitleview ?
                            <>
                                <p className="editable">
                                    <input
                                        autoFocus
                                        value={document?.docName}
                                        onBlur={() => toggleRename()}
                                        className="editable-TemplateTitle" />
                                </p>
                            </>
                            : <>
                                <p> {document?.docName} </p>
                                {/* <p> {document?.docName}  <span className="editicon" onClick={toggleRename} ><img src={EditIcon} alt="" /></span></p> */}
                            </>}
                    </div>
                </div>
            </div>
        )
    }
    // rows={6}
    return (
        <section className="veiw-SelectedTemplate">

            {renderTitleInputText()}


            <div className="mainbody">
                <p>Document request message.</p>
                <div className="editer-wrap">
                    <TextArea
                        focus={true}
                        textAreaValue={document?.docMessage || ''}
                        onBlurHandler={() => { }}
                        errorText={errorText}
                        isValid={isValid}
                        onChangeHandler={(e: any) => updateDocumentMessage(e.target.value, document)} />
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