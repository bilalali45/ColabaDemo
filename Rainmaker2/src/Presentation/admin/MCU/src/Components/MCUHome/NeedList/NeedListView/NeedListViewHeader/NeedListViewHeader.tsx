import React, { useEffect, useState, useRef, useContext } from 'react';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import { Toggler } from '../../../../../Shared/Toggler';
import { Template } from '../../../../../Entities/Models/Template';
import { MyTemplate, TenantTemplate, SystemTemplate } from '../../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
import { Link } from 'react-router-dom';
import { NeedListSelect } from '../../NeedListSelect/NeedListSelect';
import { isDocumentDraftType } from '../../../../../Store/reducers/TemplatesReducer';
import { DashboardSetting } from '../../../../../Entities/Models/DashboardSetting';
import { NeedListActions } from '../../../../../Store/actions/NeedListActions';
import { Store } from '../../../../../Store/Store';
import { NeedListActionsType } from '../../../../../Store/reducers/NeedListReducer';
type headerProps = {
    toggleCallBack: Function;
    templateList: Template[];
    addTemplatesDocuments: Function;
    isDocumentDraft: isDocumentDraftType;
    viewSaveDraft: Function;
    fetchDashBoardSettings:Function;
}

export const NeedListViewHeader = ({ toggleCallBack, templateList, addTemplatesDocuments, isDocumentDraft, viewSaveDraft, fetchDashBoardSettings }: headerProps) => {
    const {state, dispatch} = useContext(Store);

    const needListManager: any = state?.needListManager;

    const CheckBoxClickHandler = () => { 
        dispatch({type: NeedListActionsType.SetNeedListFilter, payload: !needListManager?.needListFilter});
        toggleCallBack(!needListManager?.needListFilter)
    }

    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left" data-testid="need-view-test-header">
                <span className="h2">Needs List</span>
                <div className="btn-group">
                    {!isDocumentDraft ? null : isDocumentDraft?.requestId ?
                        <button onClick={() => viewSaveDraft()} className="btn btn-secondry btn-sm">View Saved Draft</button>
                        :
                        <NeedListSelect
                            fetchTemplateDocs={() => {}}
                            showButton={true}
                            templateList={templateList}
                            addTemplatesDocuments={addTemplatesDocuments}
                            viewSaveDraft={viewSaveDraft}
                            dropType="down"
                        />
                    }
                </div>
            </div>
            <div className="need-list-view-header--right" data-testid="needListSwitchLabel">
                <label><strong>All</strong></label>
                &nbsp;&nbsp;&nbsp;
                {/* <Toggler /> */}
                <label className="switch" >
                    <input type="checkbox" onClick={CheckBoxClickHandler} id="toggle" defaultChecked={needListManager?.needListFilter} data-testid="needListSwitch"/>
                    <span className="slider round"></span>
                </label>
                &nbsp;&nbsp;&nbsp;
                <label><strong>Pending</strong></label>
            </div>
        </div>
    )
}
