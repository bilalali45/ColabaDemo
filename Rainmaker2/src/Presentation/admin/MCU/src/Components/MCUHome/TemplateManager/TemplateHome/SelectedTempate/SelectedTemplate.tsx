import React from 'react'
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
            <li>Financial statements <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>
            <li>Profit and Loss Statement <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>
            <li>Form 1099 (Miscellaneous Income) <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>
            <li>Government-Issued ID <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>

            </ul>

            </div>


            {/* <TemplateItemsList/>
            <AddDocument/> */}
        </section>
    )
}
