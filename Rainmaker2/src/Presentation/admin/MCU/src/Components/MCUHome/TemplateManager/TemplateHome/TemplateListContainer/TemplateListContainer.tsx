import { toTitleCase } from 'rainsoft-js'
import React, { useState, useContext, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateActions } from '../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';
import { Template } from '../../../../../Entities/Models/Template';
import { TemplateItem } from '../SelectedTempate/TemplateItem/TemplateItem';
import { clear } from 'console';
import { Loader } from "../../../../../Shared/components/loader";
export const MyTemplate = "MCU Template";
export const TenantTemplate = "Tenant Template";
export const SystemTemplate = "System Template";


type TemplateListContainerType = {
    setLoaderVisible: Function,
    listContainerElRef: any
}

export const TemplateListContainer = ({ setLoaderVisible, listContainerElRef }: TemplateListContainerType) => {

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state.templateManager;
    const templates: Template[] = templateManager?.templates;
    const currentTemplate: Template = templateManager?.currentTemplate;


    useEffect(() => {
        if (!templates) {
            fetchTemplatesList();
        }
        return () => {
            fetchTemplatesList();
        }
    }, []);

    const clearOld = () => {
        dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: null });
        dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null });
    }


    const changeCurrentTemplate = async (template: Template) => {
        dispatch({ type: TemplateActionsType.ToggleAddDocumentBox, payload: { value: false } })

        if (currentTemplate?.id === template.id) {
            return;
        }
        clearOld();
        dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: template });
    }


    const fetchTemplatesList = async () => {
        setLoaderVisible(true);
        let newTemplates: any = await TemplateActions.fetchTemplates('1');
        if (newTemplates) {
            dispatch({ type: TemplateActionsType.SetTemplates, payload: newTemplates });
            dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: newTemplates[0] });
        }
        setLoaderVisible(false);
        if (listContainerElRef?.current) {
            listContainerElRef.current.scrollTo(0, 0);
        }
    }

    const removeTemplate = async (templateId: string) => {
        setLoaderVisible(true);
        let isDeleted = await TemplateActions.deleteTemplate('1', templateId);
        if (isDeleted) {
            fetchTemplatesList();
        }
        setLoaderVisible(false);
    }

    const TenantListItem = (t: any) => {
        return (
            <li key={t.name} onClick={() => changeCurrentTemplate(t)}>
                <div className="l-wrap">
                    <div title={t.name} className={`c-list ${currentTemplate?.id === t.id ? 'active' : ''}`}>
                        {toTitleCase(t.name)}
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

                        <div ref={listContainerElRef} className="list-wrap my-temp-list">
                            <ul>
                                {
                                    templates?.map((t: any) => {
                                        if (t?.type === MyTemplate) {
                                            return <TemplateItem
                                                key={t.name}
                                                template={t}
                                                isSelected={currentTemplate?.id === t.id}
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

                        <div className="list-wrap tenant-temp-list">

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

    if (!templates) return <Loader containerHeight={"100%"} />;

    return (
        <div className="TL-container">

            <div className="head-TLC">

                <h4>Templates</h4>

                <div className="btn-add-new-Temp" onClick={() => {
                    clearOld();

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
