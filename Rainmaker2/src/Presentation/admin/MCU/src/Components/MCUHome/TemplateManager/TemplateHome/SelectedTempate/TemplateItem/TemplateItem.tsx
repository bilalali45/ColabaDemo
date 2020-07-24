import React, { useState, useEffect } from 'react'
import { Template } from '../../../../../../Entities/Models/Template'
import { TemplateActionsType } from '../../../../../../Store/reducers/TemplatesReducer'
import Spinner from 'react-bootstrap/Spinner'
import { toTitleCase} from 'rainsoft-js'

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

    useEffect(() => {
        setDeleteBoxVisible(false);
    }, [!isSelected])

    return (
        <li onClick={() => changeTemplate(template)}>
            <div className="l-wrap ">
                {deleteBoxVisible && isSelected ?
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
                    :
                    <div title={template.name}  className={`c-list ${isSelected ? 'active' : ''}`}>
                        <p >{toTitleCase(template.name)}</p>
                        {!deleteRequestSent ?
                            isSelected && <span className="BTNclose" title={"Remove"} onClick={toggleDeleteBox}><i className="zmdi zmdi-close"></i></span>
                            :
                            <span className="btnloader">
                                <Spinner size="sm" animation="border" role="status">
                                    <span className="sr-only">Loading...</span>
                                </Spinner>
                            </span>
                        }

                    </div>
                }
            </div>
        </li>
    )
}
