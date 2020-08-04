import React, { useState, useEffect, useContext, ChangeEvent } from "react";
import Dropdown from "react-bootstrap/Dropdown";
import { Template } from "../../../../Entities/Models/Template";
import {
  MyTemplate,
  TenantTemplate,
} from "../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer";
import { Link } from "react-router-dom";
import { TemplateDocument } from "../../../../Entities/Models/TemplateDocument";
import { Store } from "../../../../Store/Store";
import { TemplateActionsType } from "../../../../Store/reducers/TemplatesReducer";
import { TemplateActions } from "../../../../Store/actions/TemplateActions";
import { LocalDB } from "../../../../Utils/LocalDB";
import { NeedListActionsType } from "../../../../Store/reducers/NeedListReducer";

type NeedListSelectType = {
  templateList: Template[];
  addTemplatesDocuments: Function;
  viewSaveDraft: Function;
  showButton: boolean;
};

export const NeedListSelect = ({
  // templateList,
  addTemplatesDocuments,
  viewSaveDraft,
  showButton = false,
}: NeedListSelectType) => {
  const [idArray, setIdArray] = useState<String[]>([]);
  const [templateList, setTemplateList] = useState<Template[]>([]);

  const { state, dispatch } = useContext(Store);

  const templateManager: any = state?.templateManager;
  const templates: Template[] = templateManager?.templates;

  const needListManager: any = state?.needListManager;
  const selectedIds: string[] = needListManager?.templateIds || [];

  useEffect(() => {
    console.log("in here you know sdf", templates);
    if (!templates) {
      fetchTemplatesList();
    }
  }, [!templates]);

  useEffect(() => {
    setIdArray(selectedIds || []);
  }, [selectedIds?.length]);

  const [show, setShow] = useState<boolean>(false);

  useEffect(() => {
    setTemplateList(templates);
  }, [templates?.length]);

  const fetchTemplatesList = async () => {
    let newTemplates: any = await TemplateActions.fetchTemplates();
    if (newTemplates) {
      dispatch({
        type: TemplateActionsType.SetTemplates,
        payload: newTemplates,
      });
    }
  };

  const updateIdsList = (
    { target: { checked } }: ChangeEvent<HTMLInputElement>,
    id: string
  ) => {
    if (checked) {
      setIdArray([...idArray, ...selectedIds, id]);
    } else {
      setIdArray((pre) => pre?.filter((idOld) => idOld !== id));
    }

    // let isExist = idArray.includes(id);
    // if (isExist) {
    //   let oldArray = [...idArray];
    //   const index = oldArray.indexOf(id);
    //   if (index > -1) {
    //     oldArray.splice(index, 1);
    //     setIdArray(oldArray);
    //   }
    // } else {
    //   let newArray = [...idArray, id]
    //   setIdArray(newArray);
    // }
  };

  const MyTemplates = () => {
    if (!templateList) return "";
    return (
      <>
        <h3>My Templates</h3>
        <ul className="checklist">
          {templates?.map((t: Template) => {
            console.log(idArray.includes(t?.id));

            if (t?.type === MyTemplate) {
              return (
                <li>
                  <label>
                    <input
                      checked={idArray.includes(t?.id)}
                      onChange={(e) => {
                        updateIdsList(e, t.id);
                      }}
                      id={t.id}
                      type="checkbox"
                    />{" "}
                    {t.name}
                  </label>
                </li>
              );
            }
          })}
        </ul>
      </>
    );
  };

  const TemplatesByTenant = () => {
    if (!templateList) return "";
    return (
      <>
        <h3>Templates by Tenants</h3>
        <ul className="checklist">
          {templates?.map((t: Template) => {
            if (t?.type === TenantTemplate) {
              return (
                <li>
                  <label>
                    <input
                      checked={idArray.includes(t?.id)}
                      onChange={(e) => {
                        updateIdsList(e, t.id);
                      }}
                      id={t.id}
                      type="checkbox"
                    />{" "}
                    {t.name}
                  </label>
                </li>
              );
            }
          })}
        </ul>
      </>
    );
  };
  const StartListButton = () => {
    // return <button onClick={() => {

    //   setShow(false);
    //   addTemplatesDocuments(idArray);
    // }} className="btn btn-primary btn-block">Add Selected</button>

    if (!showButton) {
      return (
        <button
          onClick={() => {
            setShow(false);
            addTemplatesDocuments(idArray);
          }}
          className="btn btn-primary btn-block"
        >
          Add Selected
        </button>
      );
    } else {
      if (idArray.length > 0) {
        return (
          <button
            onClick={() => {
              setShow(false);
              addTemplatesDocuments(idArray);
            }}
            className="btn btn-primary btn-block"
          >
            Continue with Template
          </button>
        );
      } else {
        return <Link to="/newNeedList">Start from new list</Link>;
      }
    }
  };

  const displayAddButton = () => {
    return (
      <>
        <Dropdown onToggle={() => setShow(!show)} show={show}>
          <Dropdown.Toggle
            size="sm"
            variant="primary"
            className="mcu-dropdown-toggle no-caret"
            id="dropdown-basic"
          >
            {showButton ? (
              <>
                Add{" "}
                <span className="btn-icon-right">
                  <span className="rotate-plus"></span>
                </span>
              </>
            ) : (
              <span className="btn-text">Add from template</span>
            )}
          </Dropdown.Toggle>

          <Dropdown.Menu className="padding" show={show}>
            <h2>Select a need list Template</h2>
            {MyTemplates()}
            {TemplatesByTenant()}
            <div className="external-link">{StartListButton()}</div>
          </Dropdown.Menu>
        </Dropdown>
      </>
    );
  };
  // if(!templateList || !templateList?.length) {
  //   return <div></div>;
  // }

  return displayAddButton();
};
