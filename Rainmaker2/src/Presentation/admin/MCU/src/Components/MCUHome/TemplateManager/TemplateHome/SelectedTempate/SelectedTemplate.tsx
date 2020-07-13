import React, { useEffect, useContext } from 'react'
import { TemplateItemsList } from './TemplateItemsList/TemplateItemsList'
import { AddDocument } from '../../AddDocument/AddDocument'
import EditIcon from '../../../../../Assets/images/editicon.svg'

export const SelectedTemplate = () => {

    return (
        <section>
            <div className="T-head">
                <p> My standard checklist <span className="editicon"><img src={EditIcon} alt="" /></span></p>
            </div>

            <div className="ST-content-Wrap">
            <ul>
            <li><p>Financial statements <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>
            <li><p>Profit and Loss Statement <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>
            <li><p>Form 1099 (Miscellaneous Income) <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>
            <li><p>Government-Issued ID <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></p></li>

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
