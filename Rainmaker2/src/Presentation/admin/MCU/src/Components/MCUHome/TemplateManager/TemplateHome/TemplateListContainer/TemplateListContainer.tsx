import React, { useState, useContext, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateActions } from '../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';

const MyTemplate = "MCU Template";
const TenantTemplate = "Tenant Template";
const SystemTemplate = "System Template";

export const TemplateListContainer = () => {
    const [toRemoveTemplate, setToRemoveTemplate] = useState<any>(false);
    const [toRemoveTemplate1, setToRemoveTemplate1] = useState<any>(false);

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state.templateManager;
    const templates = templateManager?.templates;


    useEffect(() => {
        if (!templates) {
            fetchTemplatesList();
        }
    }, []);

    const fetchTemplatesList = async () => {
        let newTemplates = await TemplateActions.fetchTemplates('1');
        if (newTemplates) {
            dispatch({ type: TemplateActionsType.FetchTemplates, payload: newTemplates });
        }
    }

    const removeTemplate = async (templateId: string) => {
        let isDeleted = await TemplateActions.deleteTemplate(templateId, '1');
        if(isDeleted) {
            
        }
    }


    const remove = () => {
        setToRemoveTemplate(true)
    }
    const undo = () => {
        setToRemoveTemplate(false)
    }

    const remove1 = () => {
        setToRemoveTemplate(true)
    }
    const undo1 = () => {
        setToRemoveTemplate(false)
    }

    const MyTemplateListItem = (t: any) => {
        return (
            <li onClick={() => changeCurrentTemplate(t.id)}>
                <div className="l-wrap">
                    {!toRemoveTemplate1 ?
                        <div className="c-list">
                            {t.name}
                                <span className="BTNclose" onClick={remove1}><i className="zmdi zmdi-close"></i></span>
                        </div>
                        : <>
                            <div className="alert-cancel">
                                <span>Remove this template?</span>
                                <div className="l-remove-actions">
                                    <button className="lbtn btn-no" onClick={undo1}> No</button>
                                    <button className="lbtn btn-yes" onClick={() => removeTemplate(t.id)}>Yes</button></div>
                            </div>
                        </>
                    }


                </div>
            </li>
        )
    }

    const changeCurrentTemplate = async (id: any) => {
        const templateDocs = await TemplateActions.fetchTemplateDocuments(id);
        if(templateDocs) {
            dispatch({type: TemplateActionsType.FetchTemplateDocuments, payload: templateDocs})
        }
    }

    const TenantListItem = (t: any) => {
        return (
            <li onClick={() => changeCurrentTemplate(t.id)}>
                <div className="l-wrap">
                    <div className="c-list">
                        {t.name}
                    </div>
                </div>
            </li>
        )
    }

    const MyTemplates = () => {
        return (
            <>
                <div className="m-template">
                    <div className="MT-groupList">
                        <div className="title-wrap">
                            <h4>My Templates</h4>
                        </div>

                        <div className="list-wrap my-temp-list">
                            <ul>
                                {
                                    templates?.map((t: any) => {
                                        if (t?.type === MyTemplate) {
                                            return MyTemplateListItem(t)
                                        }
                                    })
                                }
                            </ul>
                        </div>
                    </div>
                </div>
            </>
        );
    };


    const TemplatesByTenant = () => {
        return (
            <>
                <div className="template-by-tenant">
                    <div className="MT-groupList">
                        <div className="title-wrap">
                            <h4>Templates by Tenant</h4>
                        </div>

                        <div className="list-wrap my-temp-list">

                            <ul>
                                {
                                    templates?.map((t: any) => {
                                        if (t?.type === TenantTemplate) {
                                            return TenantListItem(t)
                                        }
                                    })
                                }
                            </ul>
                        </div>
                    </div>
                </div>
            </>
        );
    };

    return (
        <div className="TL-container">

            <div className="head-TLC">

                <h4>Templates</h4>

                <div className="btn-add-new-Temp">
                    <button className="btn btn-primary addnewTemplate-btn">
                        <span className="btn-text">Add new template</span>
                        <span className="btn-icon">
                            <i className="zmdi zmdi-plus"></i>
                        </span>

                    </button>
                </div>
            </div>





            <div className="listWrap-templates">
                {/* My Templates */}
                {MyTemplates()}

                {TemplatesByTenant()}


            </div>


        </div>
    )
}
