import React, { useState } from 'react';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import { Toggler } from '../../../../../Shared/Toggler';
import { Template } from '../../../../../Entities/Models/Template';
import { MyTemplate, TenantTemplate, SystemTemplate } from '../../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
type headerProps = {
    toggleCallBack: Function;
    templateList: Template[];
    redirectToDocumentRequest: Function;
}

export const NeedListViewHeader = ({ toggleCallBack, templateList, redirectToDocumentRequest }: headerProps) => {
    const [toggle, setToggle] = useState(true);
    const [idArray, setIdArray] = useState<String[]>([]);

    const callBack = () => {
        toggleCallBack(toggle)
        setToggle(!toggle)
    }

    const MyTemplates = () => {
        if (!templateList) return '';
        return (
            <>
                <h3>My Templates</h3>
                <ul className="checklist">
                    {
                        templateList?.map((t: Template) => {
                            if (t?.type === MyTemplate) {
                                return <li><label><input onClick={(e) => chechIdInArray(t.id)} id={t.id} type="checkbox" /> {t.name}</label></li>
                            }
                        })
                    }
                </ul>
            </>
        );
    };

    const TemplatesByTenant = () => {
        if (!templateList) return '';
        return (
            <>
                <h3>Templates by Tenants</h3>
                <ul className="checklist">
                    {
                        templateList?.map((t: Template) => {
                            if (t?.type === TenantTemplate) {
                                return <li><label><input onClick={(e) => chechIdInArray(t.id)} id={t.id} type="checkbox" /> {t.name}</label></li>
                            }
                        })
                    }
                </ul>
            </>
        );
    }
    const StartListButton = () => {
        if (idArray.length > 0) {
            return <button onClick={() => {redirectToDocumentRequest(idArray)}} className="btn btn-primary btn-block">Continue with Template</button>
        } else {
            return <a href="">Start from new list</a>
        }
    }

    const chechIdInArray = (id: string) => {
        let isExist = idArray.includes(id);
        if (isExist) {
            let oldArray = [...idArray];
            const index = oldArray.indexOf(id);
            if (index > -1) {
                oldArray.splice(index, 1);
                setIdArray(oldArray);
            }
        } else {
            let newArray = [...idArray, id]
            setIdArray(newArray);
        }
    }

    console.log('idArray', idArray)
    return (
        <div className="need-list-view-header" id="NeedListViewHeader" data-component="NeedListViewHeader">
            <div className="need-list-view-header--left">
                <span className="h2">Needs List</span>
                <div className="btn-group">
                    <Dropdown>
                        <Dropdown.Toggle size="sm" variant="primary" className="mcu-dropdown-toggle no-caret" id="dropdown-basic" >
                            Add <span className="btn-icon-right"><span className="rotate-plus"></span></span>
                        </Dropdown.Toggle>

                        <Dropdown.Menu className="padding">
                            <h2>Select a need list Template</h2>
                            {MyTemplates()}

                            {TemplatesByTenant()}
                            {/* <h3>My Templates</h3>
                        <ul className="checklist">
                            <li><label><input type="checkbox" /> Income templates</label></li>
                            <li><label><input type="checkbox" /> My standard checklist</label></li>
                            <li><label><input type="checkbox" /> Assets template</label></li>
                        </ul> 

                        <h3>Templates by Tenants</h3>
                        <ul className="checklist">
                            <li><label><input type="checkbox" /> FHA Full Doc Refinance - W2</label></li>
                            <li><label><input type="checkbox" /> VA Cash Out - W-2</label></li>
                            <li><label><input type="checkbox" /> FHA Full Doc Refinance</label></li>
                            <li><label><input type="checkbox" /> Conventional Refinance - SE</label></li>
                            <li><label><input type="checkbox" /> VA Purchase - W-2</label></li>
                            <li><label><input type="checkbox" /> Additional Questions</label></li>
                            <li><label><input type="checkbox" /> Auto Loan</label></li>
                            <li><label><input type="checkbox" /> Construction Loan-Phase 1</label></li>
                        </ul>  */}

                            <div className="external-link">
                                {StartListButton()}
                            </div>
                        </Dropdown.Menu>
                    </Dropdown>
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
