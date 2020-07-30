import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
// ../../../../../../Assets/images/editicon.svg
import EditIcon from '../../../../../../Assets/images/editicon.svg';
import { TemplateDocument } from "../../../../../../Entities/Models/TemplateDocument";
import { useHistory } from "react-router-dom";

type NeedListContentType = {
    document: TemplateDocument | null;
    updateDocumentMessage: Function
}

export const NeedListContent = ({document, updateDocumentMessage} : NeedListContentType) => {
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const toggleRename = () => {
        seteditTitleview(!editTitleview);
    }

    const history = useHistory();

    const renderTitleInputText = () => {
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
                                <p> {document?.docName}  <span className="editicon" onClick={toggleRename} ><img src={EditIcon} alt="" /></span></p>
                            </>}
                    </div>
                </div>
            </div>
        )
    }

    return (
        <section className="veiw-SelectedTemplate">

            {renderTitleInputText()}


            <div className="mainbody">
                <p>If you’d like, you can customize this email.</p>
                <div className="editer-wrap">
                    <textarea rows={6} className="editer" value={document?.docMessage} onChange={(e) => updateDocumentMessage(e.target.value, document)}></textarea>
                </div>

            </div>

            <div className="right-footer">
                <div className="btn-wrap">
                    <button onClick={() => history.push('/ReviewNeedListRequest')} className="btn btn-primary">Review Request</button>

                </div>
            </div>


        </section>
    )
}