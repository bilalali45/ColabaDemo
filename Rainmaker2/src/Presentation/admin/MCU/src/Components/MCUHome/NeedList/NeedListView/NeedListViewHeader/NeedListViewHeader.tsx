import React, { useState } from 'react';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import { Toggler } from '../../../../../Shared/Toggler';
import { Template } from '../../../../../Entities/Models/Template';
import { MyTemplate, TenantTemplate, SystemTemplate } from '../../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
import { Link } from 'react-router-dom';
import { NeedListSelect } from '../../NeedListSelect/NeedListSelect';
import { isDocumentDraftType } from '../../../../../Store/reducers/TemplatesReducer';
type headerProps = {
    toggleCallBack: Function;
    templateList: Template[];
    addTemplatesDocuments: Function;
    isDocumentDraft: isDocumentDraftType;
    viewSaveDraft: Function;
}

export const NeedListViewHeader = ({ toggleCallBack, templateList, addTemplatesDocuments, isDocumentDraft, viewSaveDraft }: headerProps) => {
    const [toggle, setToggle] = useState(true);


    const callBack = () => {
        toggleCallBack(!toggle)
        setToggle(!toggle)
    }


    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left">
                <span className="h2">Needs List</span>
                <div className="btn-group">

                    {/* {displayAddButton()} */}
                    {!isDocumentDraft ? null : isDocumentDraft?.requestId ?
                        <button onClick={() => viewSaveDraft()} className="btn btn-secondry btn-sm">View Saved Draft</button>
                        :
                        <NeedListSelect
                            fetchTemplateDocs={() => {}}
                            showButton={true}
                            templateList={templateList}
                            addTemplatesDocuments={addTemplatesDocuments}
                            viewSaveDraft={viewSaveDraft}
                        />
                    }
                </div>
            </div>
            <div className="need-list-view-header--right" data-testid="needListSwitchLabel">
                <label><strong>All</strong></label>
                &nbsp;&nbsp;&nbsp;
                {/* <Toggler /> */}
                <label className="switch" >
                    <input type="checkbox" onChange={callBack} id="toggle" defaultChecked={toggle} data-testid="needListSwitch"/>
                    <span className="slider round"></span>
                </label>
                &nbsp;&nbsp;&nbsp;
                <label><strong>Pending</strong></label>
            </div>
        </div>
    )
}
