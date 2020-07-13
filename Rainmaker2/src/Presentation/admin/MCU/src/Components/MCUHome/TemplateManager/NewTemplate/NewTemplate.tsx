import React from 'react'
import checkicon from '../../../../Assets/images/checkicon.svg'
import emptyIcon from '../../../../Assets/images/empty-icon.svg'
export const NewTemplate = () => {
    return (
        <section className="add-newTemp-wrap">
            <div className="T-head">
                <p className="editable"> <input value="My standard checklist" className="editable-TemplateTitle" /> 
                 <span className="editsaveicon"><img src={checkicon} alt="" /></span></p>
            </div>
            <div className="empty-wrap">

<div className="c-wrap">
<div className="icon-wrap">
    <img src={emptyIcon} alt="" />
</div>
<div className="content">
    <p><b>Nothing</b> 
    <br />
    Your template is empty
    </p>

    
    <div className="add-doc-link-wrap">
                <a className="add-doc-link">
                 Add Document <i className="zmdi zmdi-plus"></i>
                </a>
            </div>
</div>
</div>

            </div>
            
        </section>
    )
}
