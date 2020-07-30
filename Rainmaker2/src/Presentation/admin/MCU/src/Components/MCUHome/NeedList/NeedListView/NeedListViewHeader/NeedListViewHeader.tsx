import React, { useState } from 'react';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import { Toggler } from '../../../../../Shared/Toggler';
import { Template } from '../../../../../Entities/Models/Template';
import { MyTemplate, TenantTemplate, SystemTemplate } from '../../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
import { Link } from 'react-router-dom';
import { NeedListSelect } from '../../NeedListSelect/NeedListSelect';
type headerProps = {
    toggleCallBack: Function;
    templateList: Template[];
    redirectToDocumentRequest: Function;
    isDraft: string;
    viewSaveDraft: Function;
}

export const NeedListViewHeader = ({ toggleCallBack, templateList, redirectToDocumentRequest, isDraft, viewSaveDraft }: headerProps) => {
    const [toggle, setToggle] = useState(true);


    const callBack = () => {
        toggleCallBack(toggle)
        setToggle(!toggle)
    }

    // const displayAddButton = () => {
    //   if(isDraft === '') return '';
    //    if(isDraft){
    //        return  <button onClick={() => viewSaveDraft()} className="btn btn-success btn-sm">View Save Draft</button>
    //    }else{
    //        return (
    //            <>
    //                  <Dropdown>
    //                     <Dropdown.Toggle size="sm" variant="primary" className="mcu-dropdown-toggle no-caret" id="dropdown-basic" >
    //                         Add <span className="btn-icon-right"><span className="rotate-plus"></span></span>
    //                     </Dropdown.Toggle>

    //                     <Dropdown.Menu className="padding">
    //                         <h2>Select a need list Template</h2>
    //                         {MyTemplates()}
    //                         {TemplatesByTenant()}                          
    //                         <div className="external-link">
    //                         {StartListButton()}
    //                         </div>
    //                     </Dropdown.Menu>
    //                 </Dropdown>
    //            </>
    //        )
    //    }
    // }

    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left">
                <span className="h2">Needs List</span>
                <div className="btn-group">

                    {/* {displayAddButton()} */}
                    <NeedListSelect
                        templateList={templateList}
                        redirectToDocumentRequest={redirectToDocumentRequest}
                        viewSaveDraft={viewSaveDraft}
                        isDraft={isDraft}
                    />
                </div>
            </div>
            <div className="need-list-view-header--right">
                <label><strong>All</strong></label>
                &nbsp;&nbsp;&nbsp;
                {/* <Toggler /> */}
                <label className="switch" >
                    <input type="checkbox" onChange={callBack} id="toggle" defaultChecked={toggle} />
                    <span className="slider round"></span>
                </label>
                &nbsp;&nbsp;&nbsp;
                <label><strong>Pending</strong></label>
            </div>
        </div>
    )
}
