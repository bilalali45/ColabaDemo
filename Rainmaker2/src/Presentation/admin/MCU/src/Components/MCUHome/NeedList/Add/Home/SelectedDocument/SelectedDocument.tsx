import React, { useEffect, useContext, useState, ChangeEvent, Fragment, KeyboardEvent } from 'react'
import Spinner from 'react-bootstrap/Spinner'
import EditIcon from '../../../../../../Assets/images/editicon.svg'

export const SelectedDocument = () => {
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

                
        </section>
    )
}