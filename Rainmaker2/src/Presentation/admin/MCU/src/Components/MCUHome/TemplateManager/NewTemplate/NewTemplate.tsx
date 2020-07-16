import React, { useContext, useState } from 'react'
import checkicon from '../../../../Assets/images/checkicon.svg'
import emptyIcon from '../../../../Assets/images/empty-icon.svg'
import { Store } from '../../../../Store/Store'
import { TemplateActions } from '../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../Store/reducers/TemplatesReducer'
import { Template } from '../../../../Entities/Models/Template'
import { AddDocument } from '../AddDocument/AddDocument'
export const NewTemplate = () => {

    const {state, dispatch} = useContext(Store);
    const [templateName, setTemplateName] = useState('');

    const templateManager : any = state?.templateManager;

    const addNewTemplate = async (name: string) => {

        let insertedTemplate = await TemplateActions.insertTemplate('1', name);
        
        if(insertedTemplate) {
            
            let updatedTemplates : any = await TemplateActions.fetchTemplates('1');
            dispatch({type: TemplateActionsType.SetTemplates, payload: updatedTemplates});

            let currentTemplate = updatedTemplates.find((t: Template) => t.name === name);
            dispatch({type: TemplateActionsType.SetCurrentTemplate, payload: currentTemplate});
        }
    }

    return (
        <section className="add-newTemp-wrap">
            <div className="T-head">
                <p className="editable"> <input value={templateName} onChange={(e) => {
                    setTemplateName(e.target.value);
                }} onBlur={(e) => {
                    addNewTemplate(e.target.value);
                } } className="editable-TemplateTitle" />
                    <span className="editsaveicon"><img src={checkicon} alt="" /></span></p>
            </div>
            <div className="empty-wrap">

                <div className="c-wrap">
                    <div className="icon-wrap">
                        <img src={emptyIcon} alt="" />
                    </div>
                    <div className="content">
                        <p><b>Nothing</b>
                            <br />Your template is empty</p>
                            <AddDocument/> 
                    </div>
                </div>

            </div>

        </section>
    )
}
