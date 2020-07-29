import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
// ../../../../../../Assets/images/editicon.svg
import EditIcon from '../../../../../../Assets/images/editicon.svg';

export const NeedListContent = () => {
    const [editTitleview, seteditTitleview] = useState<boolean>(false);
    const toggleRename = () => {
        seteditTitleview(!editTitleview);
    }
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
                                        value={"Financial Statement"}
                                        onBlur={() => toggleRename()}
                                        className="editable-TemplateTitle" />
                                </p>
                            </>
                            : <>
                                <p> Financial Statement  <span className="editicon" onClick={toggleRename} ><img src={EditIcon} alt="" /></span></p>
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
                    <textarea rows={6} className="editer">
                        Hi Richard Glenn Randall,
                        As we discussed, I’m adding addition |
                        To continue your application, we need some more information.

                </textarea>
                </div>

            </div>

            <div className="right-footer">
                <div className="btn-wrap">
                    <button className="btn btn-primary">Review Request</button>

                </div>
            </div>


        </section>
    )
}