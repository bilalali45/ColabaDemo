import React, { useState, useEffect, useContext, ChangeEvent } from "react";
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
  showButton: boolean
}

export const NeedListSelect = ({
  // templateList,
  addTemplatesDocuments,
  viewSaveDraft,
  showButton = false }: NeedListSelectType) => {

  const [idArray, setIdArray] = useState<String[]>([]);
  const [templateList, setTemplateList] = useState<Template[]>([]);

  const { state, dispatch } = useContext(Store);

  const location = useLocation();

  const templateManager: any = state?.templateManager;
  const templates: Template[] = templateManager?.templates;

  const needListManager: any = state?.needListManager;
  const selectedIds: string[] = needListManager?.templateIds || [];



  useEffect(() => {
    if (!templates) {
      fetchTemplatesList();
    }
  }, [!templates])

  useEffect(() => {
    setIdArray(selectedIds || []);
  }, [selectedIds?.length])


  const [show, setShow] = useState<boolean>(false);

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

  const MyTemplates = () => {
    if (!templateList) return '';
    return (
      <>
        <h3>My Templates</h3>
        <ul className="checklist">
          {
            templates?.map((t: Template) => {

              if (t?.type === MyTemplate) {
                return <li key={t?.id}><label className="text-ellipsis"><input checked={idArray.includes(t?.id)} onChange={(e) => {
                  updateIdsList(e, t?.id);
                }} id={t.id} type="checkbox" /> {t?.name}</label></li>
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
            templates?.map((t: Template) => {
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

    // return <button onClick={() => {

    //   setShow(false);
    //   addTemplatesDocuments(idArray);
    // }} className="btn btn-primary btn-block">Add Selected</button>

    if (!showButton) {
      return <button onClick={() => {

        setShow(false);
        addTemplatesDocuments(idArray);
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
              Add <span className="btn-icon-right"><span className="rotate-plus"></span></span>
            </Dropdown.Toggle> :

            <Dropdown.Toggle size="sm" style={{ background: 'none', border: 'none', color: '#2C9EF5', outline: 'none' }} className="mcu-dropdown-toggle no-caret" id="dropdown-basic"  >
              <span className="btn-text">Add from template</span>
            </Dropdown.Toggle>}

          <Dropdown.Menu className="padding" show={show}>
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
