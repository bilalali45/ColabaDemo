import React, { useState } from "react";
import Dropdown from "react-bootstrap/Dropdown";
import { Template } from "../../../../Entities/Models/Template";
import { MyTemplate, TenantTemplate } from "../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer";
import { Link } from "react-router-dom";

type NeedListSelectType = {
  templateList: Template[],
  redirectToDocumentRequest: Function,
  viewSaveDraft: Function,
  isDraft: string,
  showButton: boolean
}

export const NeedListSelect = ({
  templateList,
  redirectToDocumentRequest,
  viewSaveDraft,
  isDraft,
  showButton = false }: NeedListSelectType) => {

  const [idArray, setIdArray] = useState<String[]>([]);

  const updateIdsList = (id: string) => {
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

  const MyTemplates = () => {
    if (!templateList) return '';
    return (
      <>
        <h3>My Templates</h3>
        <ul className="checklist">
          {
            templateList?.map((t: Template) => {
              if (t?.type === MyTemplate) {
                return <li><label><input onClick={(e) => updateIdsList(t.id)} id={t.id} type="checkbox" /> {t.name}</label></li>
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
                return <li><label><input onClick={(e) => updateIdsList(t.id)} id={t.id} type="checkbox" /> {t.name}</label></li>
              }
            })
          }
        </ul>
      </>
    );
  }
  const StartListButton = () => {
    if (idArray.length > 0) {
      if (showButton) {
        return <button onClick={() => { redirectToDocumentRequest(idArray) }} className="btn btn-primary btn-block">Continue with Template</button>
      } else {
        return <button onClick={() => { redirectToDocumentRequest(idArray) }} className="btn btn-primary btn-block">Add Selected</button>
      }
    } else {
      if (showButton) {
        return <Link to="/newNeedList" >Start from new list</Link>
      }
    }
  }

  // <button onClick={() => viewSaveDraft()} className="btn btn-success btn-sm">View Save Draft</button>

  const displayAddButton = () => {
    return (
      <>
        <Dropdown>
          <Dropdown.Toggle size="sm" className="mcu-dropdown-toggle no-caret" id="dropdown-basic" style={{pointerEvents: "none"}} >
            {
              showButton ?
                <>
                  Add <span className="btn-icon-right"><span className="rotate-plus"></span></span>
                </>
                : <span className="btn-text">Add from template</span>
            }
          </Dropdown.Toggle>

          <Dropdown.Menu className="padding">
            <h2>Select a need list Template</h2>
            {MyTemplates()}
            {TemplatesByTenant()}
            <div className="external-link">
              {StartListButton()}
            </div>
          </Dropdown.Menu>
        </Dropdown>
      </>
    )
  }
  // if(!templateList || !templateList?.length) {
  //   return <div></div>;
  // }

  return displayAddButton();
};
