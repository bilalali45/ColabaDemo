import React, { useState } from 'react'
import { Template } from '../../../../../../Entities/Models/Template'
import { TemplateActionsType } from '../../../../../../Store/reducers/TemplatesReducer'

type TemplateItemType = {
    template: Template,
    isSelected: boolean,
    changeTemplate: Function,
    removeTemlate: Function,
}

export const TemplateItem = ({
    template,
    isSelected,
    changeTemplate,
    removeTemlate }: TemplateItemType) => {

    const [deleteBoxVisible, setDeleteBoxVisible] = useState<boolean>(false);

    const toggleDeleteBox = () => setDeleteBoxVisible(!deleteBoxVisible);

    return (
        <li onClick={() => changeTemplate(template)}>
            <div className="l-wrap ">
                {!deleteBoxVisible ?
                    <div className={`c-list ${isSelected ? 'active' : ''}`}>
                        {template.name}
                        <span className="BTNclose" onClick={toggleDeleteBox}><i className="zmdi zmdi-close"></i></span>
                    </div>
                    : isSelected && <>
                        <div className="alert-cancel">
                            <span>Remove this template?</span>
                            <div className="l-remove-actions">
                                <button className="lbtn btn-no" onClick={toggleDeleteBox}> No</button>
                                <button className="lbtn btn-yes" onClick={() => {
                                    toggleDeleteBox();
                                    removeTemlate(template?.id);
                                }}>Yes</button></div>
                        </div>
                    </>
                }
            </div>
        </li>
    )
}
