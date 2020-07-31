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
    addTemplatesDocuments: Function;
    isDraft: string;
    viewSaveDraft: Function;
}

export const NeedListViewHeader = ({ toggleCallBack, templateList, addTemplatesDocuments, isDraft, viewSaveDraft }: headerProps) => {
    const [toggle, setToggle] = useState(true);


    const callBack = () => {
        toggleCallBack(toggle)
        setToggle(!toggle)
    }


    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left">
                <span className="h2">Needs List</span>
                <div className="btn-group">

                    {/* {displayAddButton()} */}
                    <NeedListSelect
                        showButton={false}
                        templateList={templateList}
                        addTemplatesDocuments={addTemplatesDocuments}
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
