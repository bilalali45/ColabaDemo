import React, { useState, useContext, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateActions } from '../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';
import { Template } from '../../../../../Entities/Models/Template';
import { TemplateItem } from '../SelectedTempate/TemplateItem/TemplateItem';

const MyTemplate = "MCU Template";
const TenantTemplate = "Tenant Template";
const SystemTemplate = "System Template";

export const TemplateListContainer = () => {

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state.templateManager;
    const templates: Template[] = templateManager?.templates;
    const currentTemplate: Template = templateManager?.currentTemplate;


    useEffect(() => {
        if (!templates) {
            fetchTemplatesList();
        }
    }, []);


    const changeCurrentTemplate = async (template: Template) => {

        if (currentTemplate?.id === template.id) {
            return;
        }

        dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: template });
    }


    const fetchTemplatesList = async () => {
        let newTemplates: any = await TemplateActions.fetchTemplates('1');
        if (newTemplates) {
            dispatch({ type: TemplateActionsType.SetTemplates, payload: newTemplates });
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: newTemplates[0] });

        }
    }

    const removeTemplate = async (templateId: string) => {
        let isDeleted = await TemplateActions.deleteTemplate('1', templateId);
        if (isDeleted) {
            fetchTemplatesList();
        }
    }

    const TenantListItem = (t: any) => {
        return (
            <li onClick={() => changeCurrentTemplate(t)}>
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
                                            return <TemplateItem
                                                template={t}
                                                isSelected={currentTemplate?.name === t.name}
                                                changeTemplate={changeCurrentTemplate}
                                                removeTemlate={removeTemplate} />
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

                <div className="btn-add-new-Temp" onClick={() => {
                    dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: null });
                    dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null });

                }}>
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
