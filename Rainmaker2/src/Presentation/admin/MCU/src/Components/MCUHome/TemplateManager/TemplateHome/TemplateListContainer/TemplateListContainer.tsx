import React, { useContext, useEffect } from 'react'
import { Store, InitialStateType } from '../../../../../Store/Store'
import { TemplateActions } from '../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';

const MyTemplate = "MCU Template";
const TenantTemplate = "Tenant Template";
const SystemTemplate = "System Template";

export const TemplateListContainer = () => {

    const {state, dispatch} = useContext(Store);

    const templateManager : any = state.templateManager;
    const templates = templateManager?.templates;
    

    useEffect(() => {   
        if(!templates) {
            fetchTemplatesList();
        }
    }, []);

    const fetchTemplatesList = async () => {
        let newTemplates = await TemplateActions.fetchTemplates('1');
        if(newTemplates) {
            dispatch({type: TemplateActionsType.FetchTemplates, payload: newTemplates});
        }
    }

    return (
        <div className="TL-container">

            <div className="head-TLC">
                <h4>Templates</h4>
                <div className="btn-add-new-Temp">
                    <button className="btn">
                        Add new template
              <i className="zmdi zmdi-plus"></i>
                    </button>
                </div>
            </div>

            <div className="list-my-templates">
                <div className="title-wrap">
                    <h4>My Templates</h4>
                </div>

                <div className="my-temp-list">
                    <ul>
                        <li>Income Template</li>
                        <li>My standard checklist</li>
                        <li>Assets Template</li>
                    </ul>
                </div>

                <div className="template-by-tenant">
                    <div className="title-wrap">
                        <h4>Templates by Tenant</h4>
                    </div>
                <ul>
                
                        <li>FHA Full Doc Refinance - W2</li>
                        <li>VA Cash Out - W-2</li>
                        <li>FHA Full Doc Refinance</li>
                        <li>Conventional Refinance - SE</li>
                        <li>VA Purchase - W-2</li>
                        <li>Additional Questions</li>
                    </ul>  
                </div>
            </div>
        </div>
    )
}
