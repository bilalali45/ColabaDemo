import React, { useState, useContext, useEffect } from 'react'
import { Store } from '../../../../../Store/Store';
import { TemplateActions } from '../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer';

const MyTemplate = "MCU Template";
const TenantTemplate = "Tenant Template";
const SystemTemplate = "System Template";

export const TemplateListContainer = () => {
    const [removeTemplate, setremoveTemplate] = useState<any>(false);


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

    const remove = () => {
        setremoveTemplate(true)
    }
    const undo = () => {
        setremoveTemplate(false)
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
                                <li>
                                    <div className="l-wrap">
                                        {!removeTemplate ?
                                            <div className="c-list">
                                                Income Template
                                <span className="BTNclose" onClick={remove}><i className="zmdi zmdi-close"></i></span>
                                            </div>
                                            : <>
                                                <div className="alert-cancel">
                                                    <span>Remove this template?</span>
                                                    <div className="l-remove-actions"><button onClick={undo}> No</button> <button>Yes</button></div>
                                                </div>
                                            </>
                                        }


                                    </div>
                                </li>
                                <li>
                                    <div className="l-wrap">
                                        {!removeTemplate ?
                                            <div className="c-list">
                                                My standard checklist
                                <span className="BTNclose" onClick={remove}><i className="zmdi zmdi-close"></i></span>
                                            </div>
                                            : <>
                                                <div className="alert-cancel">
                                                    <span>Remove this template?</span>
                                                    <div className="l-remove-actions"><button onClick={undo}> No</button> <button>Yes</button></div>
                                                </div>
                                            </>
                                        }


                                    </div>
                                </li>

                                <li>
                                    <div className="l-wrap">
                                        {!removeTemplate ?
                                            <div className="c-list">
                                                Assets Template
                                <span className="BTNclose" onClick={remove}><i className="zmdi zmdi-close"></i></span>
                                            </div>
                                            : <>
                                                <div className="alert-cancel">
                                                    <span>Remove this template?</span>
                                                    <div className="l-remove-actions"><button onClick={undo}> No</button> <button>Yes</button></div>
                                                </div>
                                            </>
                                        }


                                    </div>
                                </li>
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
                                <li>
                                    <div className="l-wrap">
                                        <div className="c-list">
                                            FHA Full Doc Refinance - W2
                                    </div>
                                    </div>
                                </li>
                                <li>
                                    <div className="l-wrap">
                                        <div className="c-list">
                                            VA Cash Out - W-2
                                    </div>
                                    </div>

                                </li>
                                <li>
                                    <div className="l-wrap">
                                        <div className="c-list">
                                            FHA Full Doc Refinance
                                    </div>
                                    </div>
                                </li>
                                <li>

                                    <div className="l-wrap">
                                        <div className="c-list">
                                            Conventional Refinance - SE
                                    </div>
                                    </div>
                                </li>
                                <li>
                                    <div className="l-wrap">
                                        <div className="c-list">
                                            VA Purchase - W-2
                                    </div>
                                    </div>

                                </li>
                                <li>
                                    <div className="l-wrap">
                                        <div className="c-list">
                                            Additional Questions
                                    </div>
                                    </div>

                                </li>
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
