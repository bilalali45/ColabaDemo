import React, { useState, useEffect, useContext, ChangeEvent, useRef } from "react";
import Dropdown from "react-bootstrap/Dropdown";
import { Template } from "../../../../Entities/Models/Template";
import { MyTemplate, TenantTemplate } from "../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer";
import { Link, useLocation } from "react-router-dom";
import { TemplateDocument } from "../../../../Entities/Models/TemplateDocument";
import { Store } from "../../../../Store/Store";
import { TemplateActionsType } from "../../../../Store/reducers/TemplatesReducer";
import { TemplateActions } from "../../../../Store/actions/TemplateActions";
import { LocalDB } from "../../../../Utils/LocalDB";
import { NeedListActionsType } from "../../../../Store/reducers/NeedListReducer";

type NeedListSelectType = {
  templateList: Template[],
  addTemplatesDocuments: Function,
  viewSaveDraft: Function,
  showButton: boolean,
  fetchTemplateDocs: Function
}

export const NeedListSelect = ({
  // templateList,
  addTemplatesDocuments,
  viewSaveDraft,
  showButton = false,
  fetchTemplateDocs }: NeedListSelectType) => {

  const [idArray, setIdArray] = useState<String[]>([]);
  const [templateList, setTemplateList] = useState<Template[]>([]);

  const { state, dispatch } = useContext(Store);

  const location = useLocation();

  let myTemplateContainerRef = useRef<HTMLUListElement>(null);
  let tenantTemplateContainerRef = useRef<HTMLUListElement>(null);

  const templateManager: any = state?.templateManager;
  const templates: Template[] = templateManager?.templates;

  const needListManager: any = state?.needListManager;
  const selectedIds: string[] = needListManager?.templateIds || [];

  const [show, setShow] = useState<boolean>(false);
    

  useEffect(() => {
    if (!templates) {
      fetchTemplatesList();
    }

    
  }, [!templates])

  useEffect(() => {
    setIdArray(selectedIds || []);
  }, [selectedIds?.length]);

  useEffect(() => {
    if(myTemplateContainerRef?.current && tenantTemplateContainerRef?.current) {
      myTemplateContainerRef?.current.scrollTo(0, 0);
      tenantTemplateContainerRef?.current.scrollTo(0, 0);
    }
  }, [show === true]);


  useEffect(() => {
    setTemplateList(templates);
  }, [templates?.length])

  const fetchTemplatesList = async () => {
    let newTemplates: any = await TemplateActions.fetchTemplates();
    if (newTemplates) {
      dispatch({ type: TemplateActionsType.SetTemplates, payload: newTemplates });
    }
  }

  const updateIdsList = ({ target: { checked } }: ChangeEvent<HTMLInputElement>, id: string) => {

    if (checked) {
      setIdArray([...idArray, ...selectedIds, id]);
    } else {
      setIdArray((pre: any) => pre?.filter((idOld: any) => idOld !== id));
    }

  }

  const MyTemplates = (templateList: Template[]) => {
    if (!templateList || templateList.length === 0) return null;
    return (
      <>
        <h3>My Templates</h3>

        <ul className="checklist" ref={myTemplateContainerRef}>
          {
            templateList?.map((t: Template) => {

              if (t?.type === MyTemplate) {

                return <li key={t?.id}><label className="text-ellipsis"><input autoFocus checked={idArray.includes(t?.id)} onChange={(e) => {
                  updateIdsList(e, t?.id);
                }} id={t.id} type="checkbox" /> {t?.name}</label></li>
              }
            })
          }
        </ul>
      </>
    );
  };

  const TemplatesByTenant = (templateList: Template[]) => {
    if (!templateList) return null;
    return (
      <>
        <h3>Templates by Tenants</h3>
        <ul className="checklist" ref={tenantTemplateContainerRef}>
          {
            templateList?.map((t: Template) => {
              if (t?.type === TenantTemplate) {
                return <li key={t?.id}><label className="text-ellipsis"><input checked={idArray.includes(t?.id)} onChange={(e) => {
                  updateIdsList(e, t.id);
                }} id={t.id} type="checkbox" /> {t.name}</label></li>
              }
            })
          }
        </ul>
      </>
    );
  }
  const StartListButton = () => {

    if (!showButton) {
      return <button onClick={() => {

        fetchTemplateDocs(idArray);
        addTemplatesDocuments(idArray);
        setShow(false);
      }} className="btn btn-primary btn-block">Add Selected</button>
    } else {

      if (idArray.length > 0) {
        return <button onClick={() => {
          setShow(false);
          addTemplatesDocuments(idArray);
        }} className="btn btn-primary btn-block"><span className="btn-text">Continue with Template</span><span className="btn-icon"><i className="zmdi zmdi-plus"></i></span></button>



      } else {
        return <Link to={`/newNeedList/${LocalDB.getLoanAppliationId()}`} >Start from new list</Link>
      }
    }

  }


  const displayAddButton = () => {
    return (
      <>
        <Dropdown onToggle={() => setShow(!show)} show={show}>
          {showButton ?
            <Dropdown.Toggle size="sm" variant="primary" className="mcu-dropdown-toggle no-caret" id="dropdown-basic"  >
              Add <span className="btn-icon-right"><em className="zmdi zmdi-plus"></em></span>
            </Dropdown.Toggle> :

            <Dropdown.Toggle size="sm" style={{ background: 'none', border: 'none', color: '#2C9EF5', outline: 'none' }} className="mcu-dropdown-toggle no-caret" id="dropdown-basic"  >
              <span className="btn-text">Add from template</span>
            </Dropdown.Toggle>}

          <Dropdown.Menu className="padding" show={show}>
            <h2>Select a need list Template</h2>
            {MyTemplates(templates?.filter((t: Template) => t.type === MyTemplate))}
            {TemplatesByTenant(templates?.filter((t: Template) => t.type === TenantTemplate))}
            <div className="external-link">
              {StartListButton()}
            </div>
          </Dropdown.Menu>
        </Dropdown>
      </>
    )
  }


  return displayAddButton();
};
