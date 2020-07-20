import React, { useContext, useState } from 'react'
import checkicon from '../../../../Assets/images/checkicon.svg'
import emptyIcon from '../../../../Assets/images/empty-icon.svg'
import { Store } from '../../../../Store/Store'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer'
import { Template } from '../../../../Entities/Models/Template'
import { AddDocument } from '../AddDocument/AddDocument'

type NewTemplateType = {
    setLoaderVisible: Function
}

export const NewTemplate = ({ setLoaderVisible }: NewTemplateType) => {

    const { state, dispatch } = useContext(Store);
    const [templateName, setTemplateName] = useState('');
    const [saved, setSaved] = useState<boolean>(false);

    const templateManager: any = state?.templateManager;
    const currentTemplate: any = templateManager?.currentTemplate;

    return (
        <section className="add-newTemp-wrap">

            <div className="empty-wrap">

                <div className="c-wrap">
                    <div className="icon-wrap">
                        <img src={emptyIcon} alt="" />
                    </div>
                    {currentTemplate ? <div className="content">
                        <p><b>Nothing</b>
                            <br />Your template is empty</p>
                        <AddDocument
                            setLoaderVisible={setLoaderVisible}
                            popoverplacement="left" />
                    </div> :
                        <p>Add documents after template is created </p>}
                </div>

            </div>

        </section>
    )
}
