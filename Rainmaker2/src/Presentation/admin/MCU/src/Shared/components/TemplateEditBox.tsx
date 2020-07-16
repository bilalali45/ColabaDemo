import React, { useState, Fragment, useEffect } from 'react'



type TemplateEditBoxType = {
    checkIcon: string,
    editIcon: string,
    templateName: string,
    renameTemplate: Function
}

export const TemplateEditBox = ({renameTemplate, templateName, checkIcon, editIcon}: TemplateEditBoxType) => {

    const [newNameText, setNewNameText] = useState<string>(templateName);
    const [editTitleview, seteditTitleview] = useState<boolean>(false);

    useEffect(() => {
        setNewNameText(templateName);
    }, [templateName])

    const toggleRename = () => {
        seteditTitleview(!editTitleview)
    }

    return (
        <Fragment>
            {editTitleview ?
                <p className="editable">
                    <input
                        value={newNameText}
                        onChange={(e) => setNewNameText(e.target.value)}
                        onKeyDown={(e) => {
                            if (e.keyCode === 13) {
                                renameTemplate(e);
                                toggleRename();
                            }
                        }}
                        onBlur={(e) => {
                            renameTemplate(e);
                            toggleRename();
                        }}
                        className="editable-TemplateTitle" />
                    <span className="editsaveicon" onClick={toggleRename}><img src={checkIcon} alt="" /></span></p>
                : <>
                    <p> {templateName} <span className="editicon" onClick={toggleRename}><img src={editIcon} alt="" /></span></p>
                </>
            }
        </Fragment>
    )
}
