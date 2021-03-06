import React, {
  useState,
  useEffect,
  useContext,
  ChangeEvent,
  useRef
} from 'react';
import Dropdown from 'react-bootstrap/Dropdown';
import {Template} from '../../../../Entities/Models/Template';
import {
  MyTemplate,
  TenantTemplate,
  SystemTemplate
} from '../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
import {Link, useLocation} from 'react-router-dom';
import {TemplateDocument} from '../../../../Entities/Models/TemplateDocument';
import {Store} from '../../../../Store/Store';
import {TemplateActionsType} from '../../../../Store/reducers/TemplatesReducer';
import {TemplateActions} from '../../../../Store/actions/TemplateActions';
import {LocalDB} from '../../../../Utils/LocalDB';
import {NeedListActionsType} from '../../../../Store/reducers/NeedListReducer';
import Overlay from 'react-bootstrap/Overlay';
import Popover from 'react-bootstrap/Popover';
import { Error } from '../../../../Entities/Models/Error';

type NeedListSelectType = {
  templateList: Template[];
  addTemplatesDocuments: Function;
  viewSaveDraft: Function;
  showButton: boolean;
  fetchTemplateDocs: Function;
  dropType?:any;
};

export const NeedListSelect = ({
  // templateList,
  addTemplatesDocuments,
  viewSaveDraft,
  showButton = false,
  fetchTemplateDocs,
  dropType
}: NeedListSelectType) => {
  const [idArray, setIdArray] = useState<String[]>([]);
  const [templateList, setTemplateList] = useState<Template[]>([]);
  const {state, dispatch} = useContext(Store);
  const location = useLocation();

  let myTemplateContainerRef = useRef<HTMLUListElement>(null);
  let tenantTemplateContainerRef = useRef<HTMLUListElement>(null);

  const templateManager: any = state?.templateManager;
  const templates: Template[] = templateManager?.templates;

  const needListManager: any = state?.needListManager;
  const selectedIds: string[] = needListManager?.templateIds || [];

  const [show, setShow] = useState<boolean>(false);
  const [showPopover, setShowPopover] = useState<boolean>(false);
  const [target, setTarget] = useState(null);
  const ref = useRef(null);

  const [docs, setDocs] = useState<any[]>([]);
  const [templateName, setTemplateName] = useState<string>('');

  const displayPopover = (event: any, docs: any[], name: string) => {
    setTemplateName(name);
    setDocs(docs);
    setShowPopover(true);
    setTarget(event.target);
  };

  const hidePopover = (event: any) => {
    setShowPopover(false);
    setTarget(event.target);
  };

  useEffect(() => {
    if (!templates) {
      fetchTemplatesList();
    }
  }, [!templates]);

  useEffect(() => {
    setIdArray(selectedIds || []);
  }, [selectedIds?.length]);

  useEffect(() => {
    if (
      myTemplateContainerRef?.current &&
      tenantTemplateContainerRef?.current
    ) {
       myTemplateContainerRef?.current.scrollTo(0, 0);
       tenantTemplateContainerRef?.current.scrollTo(0, 0);
    }
  }, [show === true, location.pathname, templates]);

  useEffect(() => {
    setTemplateList(templates);
  }, [templates?.length]);

  const fetchTemplatesList = async () => {
    let res = await TemplateActions.fetchTemplates();
    if(res){
      if(Error.successStatus.includes(res.status)){
        let newTemplates: any = res.data
        if (newTemplates) {
          dispatch({type: TemplateActionsType.SetTemplates, payload: newTemplates});
        }
      }
      else{
          Error.setError(dispatch, res)
      }
  }
   
  };

  const updateIdsList = (
    {target: {checked}}: ChangeEvent<HTMLInputElement>,
    id: string
  ) => {
    if (checked) {
      setIdArray([...idArray, ...selectedIds, id]);
    } else {
      setIdArray((pre: any) => pre?.filter((idOld: any) => idOld !== id));
    }
  };

  const MyTemplates = (templateList: Template[]) => {
    if (!templateList || templateList.length === 0) return null;
    return (
      <>
        <div data-testid="templates">
          <h3>My Templates</h3>

          <ul className="checklist" ref={myTemplateContainerRef} 
          onScroll={(e) => {
                      hidePopover(e);
                    }}
                    >
            {templateList?.map((t: Template) => {
              if (t?.type === MyTemplate) {
                return (
                  <li
                  data-testid="my-template"
                    key={t?.id}
                    onMouseEnter={(e:any) => {
                       displayPopover(e, t.docs, t.name);
                    }}
                    onMouseMove={(e:any) => {
                      displayPopover(e, t.docs, t.name);
                   }}
                    onMouseLeave={(e) => {
                      hidePopover(e);
                    }}
                  >
                    <label className="text-ellipsis">
                      <input
                        autoFocus
                        checked={idArray.includes(t?.id)}
                        onChange={(e) => {
                          updateIdsList(e, t?.id);
                        }}
                        id={t.id}
                        type="checkbox"
                      />{' '}
                      {t?.name}
                    </label>
                  </li>
                );
              }
            })}
          </ul>
          {renderDocsPopover()}
        </div>
      </>
    );
  };
  const renderDocsPopover = () => {
    return (
      <Overlay
        show={showPopover}
        target={target}
        placement="right"
        container={ref.current}
      >
        <Popover id="popover-contained" className="addneedlist-popover" data-testid="popup-temp">
          <Popover.Title as="h3" className="addneedlist-popover-title" >
            {templateName}
          </Popover.Title>
          <Popover.Content>
            {docs.length > 0 ? (
              docs.map((d: any) => {
                return (
                  <span className="addneedlist-popover--list">{d.docName}</span>
                );
              })
            ) : (
              <span className="addneedlist-popover--list">
                Nothing in template.
              </span>
            )}
          </Popover.Content>
        </Popover>
      </Overlay>
    );
  };
  const TemplatesByTenant = (templateList: Template[]) => {
    if (!templateList || templateList.length === 0) return null;
    return (
      <div data-testid="templatesByTenant">
        <h3>System Templates</h3>
        <ul className="checklist" ref={tenantTemplateContainerRef}           onScroll={(e) => {
                      hidePopover(e);
                    }}>
          {templateList?.map((t: Template) => {
            if (t?.type === TenantTemplate || t?.type === SystemTemplate) {
              return (
                <li data-testid="tenant-template"
                  key={t?.id}
                  onMouseEnter={(e:any) => {
                    displayPopover(e, t.docs, t.name);
                  }}
                  onMouseMove={(e:any) => {
                    displayPopover(e, t.docs, t.name);
                 }}
                  onMouseLeave={(e) => {
                    hidePopover(e);
                  }}
                >
                  <label className="text-ellipsis">
                    <input
                      checked={idArray.includes(t?.id)}
                      onChange={(e) => {
                        updateIdsList(e, t.id);
                      }}
                      id={t.id}
                      type="checkbox"
                    />{' '}
                    {t.name}
                  </label>
                </li>
              );
            }
          })}
        </ul>
        {renderDocsPopover()}
      </div>
    );
  };
  const StartListButton = () => {
    if (!showButton) {
      return (
        <button
          onClick={() => {
            fetchTemplateDocs(idArray);
            addTemplatesDocuments(idArray);
            setShow(false);
          }}
          className="btn btn-primary btn-block"
        >
          <span className="btn-text d-text">Add Selected</span>
        </button>
      ); //zedit
    } else {
      if (idArray.length > 0) {
        return (
          <button data-testid="continue-with-temp-btn"
            onClick={() => {
              setShow(false);
              addTemplatesDocuments(idArray);
            }}
            className="btn btn-primary btn-block"
          >
            <span className="btn-text d-text">Continue with Template</span>
            <span className="btn-icon d-icon">
              <i className="zmdi zmdi-plus"></i>
            </span>
          </button>
        );
      } else {
        return (
          <Link to={`/newNeedList/${LocalDB.getLoanAppliationId()}`}>
            Start from new list
          </Link>
        );
      }
    }
  };

  const displayAddButton = () => {

    return (
      <>
        <Dropdown onToggle={() => setShow(!show)} show={show} drop={dropType} data-testid="addTemplate" className="dropdown">
          {showButton ?
            <Dropdown.Toggle data-testid="addTemplateBtn"  size="sm" variant="primary" className="mcu-dropdown-toggle no-caret" id="dropdown-basic">
              Add <span className="btn-icon-right"><em className="zmdi zmdi-plus"></em></span>
            </Dropdown.Toggle> :

            <Dropdown.Toggle data-testid="add-from-temp-btn" size="sm" style={{ background: 'none', border: 'none', outline: 'none' }} className="mcu-dropdown-toggle no-caret" id="dropdown-basic"  >
              <span className="btn-text">Add from template</span>
            </Dropdown.Toggle>
          }

          {show && <Dropdown.Menu className="padding" show={show} data-testid="addTemplateDropDown" ref={ref}>
            <h2>Select a needs list Template</h2>
            {MyTemplates(
              templates?.filter((t: Template) => t.type === MyTemplate)
            )}
            {TemplatesByTenant(
              templates?.filter(
                (t: Template) =>
                  t.type === TenantTemplate || t.type === SystemTemplate
              )
            )}
            <div className="external-link">{StartListButton()}</div>
          </Dropdown.Menu>}
        </Dropdown>
      </>
    )
  }

  return displayAddButton()
};
