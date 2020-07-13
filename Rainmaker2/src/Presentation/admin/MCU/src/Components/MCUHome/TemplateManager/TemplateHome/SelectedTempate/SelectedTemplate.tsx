import React, { useEffect, useContext } from 'react'
import { TemplateItemsList } from './TemplateItemsList/TemplateItemsList'
import { AddDocument } from '../../AddDocument/AddDocument'
import EditIcon from '../../../../../Assets/images/editicon.svg'
import { TemplateActions } from '../../../../../Store/actions/TemplateActions'
import { TemplateActionsType } from '../../../../../Store/reducers/TemplatesReducer'
import { Store } from '../../../../../Store/Store'

export const SelectedTemplate = () => {

    const { state, dispatch } = useContext(Store);

    const templateManager: any = state.templateManager;
    const templateDocuments = templateManager?.templateDocuments;

    const removeDoc = async (templateId: string, documentId: string) => {
        let isDeleted = await TemplateActions.deleteTemplateDocument(templateId, '1', documentId);
        if(isDeleted) {
            
        }
    }

    return (
        <section>
            <div className="T-head">
                <p> My standard checklist <span className="editicon"><img src={EditIcon} alt="" /></span></p>
            </div>

            <div className="ST-content-Wrap">
                <ul>
                    {
                        templateDocuments?.map((td: any) => {
                            return (
                                <li>
                                    <p>{td.docName}
                                        <span className="BTNclose">
                                            <i className="zmdi zmdi-close" onClick={() => removeDoc(td.id, td.documentId)}></i>
                                        </span>
                                    </p>
                                </li>
                            )
                        })
                    }
                    {/* <li><p>Profit and Loss Statement <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>
                    <li><p>Form 1099 (Miscellaneous Income) <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>
                    <li><p>Government-Issued ID <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li> */}

                </ul>

                <div className="add-doc-link-wrap">
                    <a className="add-doc-link">
                        Add Document <i className="zmdi zmdi-plus"></i>
                    </a>
                </div>

            </div>


            {/* <TemplateItemsList/>
            <AddDocument/> */}
        </section>
    )
}
