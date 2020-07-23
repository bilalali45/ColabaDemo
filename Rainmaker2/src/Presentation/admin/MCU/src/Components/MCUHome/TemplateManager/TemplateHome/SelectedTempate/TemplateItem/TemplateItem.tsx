import React, { useState } from 'react'
import { Template } from '../../../../../../Entities/Models/Template'
import { TemplateActionsType } from '../../../../../../Store/reducers/TemplatesReducer'
import Spinner from 'react-bootstrap/Spinner'

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
    const [deleteRequestSent, setDeleteRequestSent] = useState<boolean>(false);

    const toggleDeleteBox = () => setDeleteBoxVisible(!deleteBoxVisible);

    return (
        <li onClick={() => changeTemplate(template)}>
            <div className="l-wrap ">
                {!deleteBoxVisible ?
                    <div className={`c-list ${isSelected ? 'active' : ''}`}>
                        {template.name}
                        {!deleteRequestSent ?
                            <span className="BTNclose" onClick={toggleDeleteBox}><i className="zmdi zmdi-close"></i></span>
                            :
                            <span className="BTNclose">
                                <Spinner size="sm" animation="border" role="status">
                                    <span className="sr-only">Loading...</span>
                                </Spinner>
                            </span>
                        }

                    </div>
                    : isSelected && <>
                        <div className="alert-cancel">
                            <span>Remove this template?</span>
                            <div className="l-remove-actions">
                                <button className="lbtn btn-no" onClick={toggleDeleteBox}> No</button>
                                <button className="lbtn btn-yes" onClick={async () => {
                                    setDeleteRequestSent(true)
                                    toggleDeleteBox();
                                    await removeTemlate(template?.id);
                                    // setDeleteRequestSent(false)
                                }}>Yes</button></div>
                        </div>
                    </>
                }
            </div>
        </li>
    )
}
