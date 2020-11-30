import React, { useContext, useEffect, useState, useRef } from 'react';
import { Template } from '../../../Entities/Models/Template';
import { TemplateActions } from '../../../Store/actions/TemplateActions';
import { Role } from '../../../Store/Navigation';
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer';
import { Store } from '../../../Store/Store';
import { LocalDB } from '../../../Utils/LocalDB';
import ContentHeader from '../../Shared/ContentHeader';
import { MyTemplate, SystemTemplate, TenantTemplate } from '../ManageDocumentTemplate';

interface Props {

}

  const ManageDocumentTemplateHeader: React.FC<Props> = ({ }) => {

  const { state, dispatch } = useContext(Store);

  const templateManager: any = state.templateManager;
  const currentTemplate = templateManager?.currentTemplate;
  const templates = templateManager?.templates;
  const templateDocuments = templateManager?.templateDocuments;
  const categoryDocuments = templateManager?.categoryDocuments;

  const [disbaleAddButton, setdisbaleAddButton] = useState(false);
  const [showDropDown, setShowDropDown] = useState(false);
  const DropDownNode = useRef<HTMLDivElement>(null);
  const role = LocalDB.getUserRole();

  useEffect(() => {
      let handler = (event:any) => {    
          if ( !DropDownNode?.current?.contains(event.target)){
            setShowDropDown(false);
          }
      }    
      document.addEventListener("mousedown", handler); 
      return () => {
          document.removeEventListener("mousedown", ()=>{ })
      }
  });
  

  useEffect (() => {
    fetchTemplatesList();
     return () => {
      clearAll();
     }     
   },[])
   
   const clearAll = () => {
    dispatch({ type: TemplateActionsType.SetTemplates, payload: []});
    dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: {}});
    dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: null});
    //dispatch({ type: TemplateActionsType.SetCategoryDocuments, payload: []});
    dispatch({ type: TemplateActionsType.SetCurrentCategoryDocuments, payload: {}});
    dispatch({ type: TemplateActionsType.SetSelectedTemplateDocuments, payload: []});
   }

   const fetchTemplatesList = async () => {
    let newTemplates: any = await TemplateActions.fetchTemplates();
    if (newTemplates) {
      newTemplates.forEach((item: Template, index: any) => {
            if (index === 0) {
                item.open = true;
            }else{
                item.open = false;
            }
            item.confirmDelete = false;
        });

        dispatch({
            type: TemplateActionsType.SetTemplates,
            payload: newTemplates
        });
        dispatch({
            type: TemplateActionsType.SetCurrentTemplate,
            payload: newTemplates[0]
        });
    }
};

  const clearOldAndUpdate = (newTemp: any) => {
    dispatch({ type: TemplateActionsType.SetCurrentTemplate, payload: null });
    dispatch({ type: TemplateActionsType.SetTemplateDocuments, payload: [] });
    dispatch({ type: TemplateActionsType.SetTemplates, payload: [newTemp, ...templates]});
    setdisbaleAddButton(true);
  };

  const addNewTemplate = (role: number) => {
    closeAllOpenTemplate();
    let newTemp = new Template();
    newTemp.type = role === 1 ? TenantTemplate : MyTemplate;
    newTemp.name = 'New Template';
    newTemp.docs = [];
    newTemp.edit = true;
    newTemp.isNew = true;
    newTemp.open = true;
    clearOldAndUpdate(newTemp);
    setShowDropDown(false);
  }

  const addNewTemplateHandler = () => {
    if (role === Role.MCU_ROLE) {
      addNewTemplate(Role.MCU_ROLE);
    } else {
      setShowDropDown(true);
    }
  }

  const renderDropDown = () => {
    return (
      <>
        <div ref={DropDownNode} className="dropdown-menu" aria-labelledby="dropdownMenuButton">
          <ul>
            <li><a onClick={e => addNewTemplate(Role.MCU_ROLE)} href='javascript:;'>As Personal Template</a></li>
            <li><a onClick={e => addNewTemplate(Role.ADMIN_ROLE)} href='javascript:;'>As System Template</a></li>
          </ul>
        </div>
      </>
    )
  }

  useEffect(() => {
    if(templates){
      let newlyCreated = templates.find((ele: { NewlyCreated: boolean; })  => ele.NewlyCreated === false)
      if(newlyCreated){
       setdisbaleAddButton(true)
      }else{
       setdisbaleAddButton(false)
      }
    } 
  }, [currentTemplate])

  const closeAllOpenTemplate = () => {
    let list = templates;
    list.forEach((item: Template, index: any) => {
      item.open = false;
     });

     dispatch({
      type: TemplateActionsType.SetTemplates,
      payload: list
  });
  }

  return (
    <>
      <ContentHeader title="Document Templates" tooltipType={5} className="manage-doc-temp-header">
        <div className="dropdown arrow-right float-right">
          <button disabled={disbaleAddButton}
            onClick={(e: any) => addNewTemplateHandler()}
            className="settings-btn dropdown-toggle"
            data-testid="addNewTemplate-btn">
            Add New Template
                     <i className="zmdi zmdi-plus"></i>

          </button>
          {showDropDown &&
            renderDropDown()
          }
        </div>
      </ContentHeader>
    </>
  )
}

export default ManageDocumentTemplateHeader;
