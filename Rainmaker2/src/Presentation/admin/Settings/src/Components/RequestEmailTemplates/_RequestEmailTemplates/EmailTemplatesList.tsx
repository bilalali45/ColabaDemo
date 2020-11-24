import React, { useContext, useEffect,useState, useRef } from 'react';
import { RequestEmailTemplate } from '../../../Entities/Models/RequestEmailTemplate';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { RequestEmailTemplateActionsType, RequestEmailTemplateType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { Store } from '../../../Store/Store';
import Loader, { WidgetLoader } from '../../Shared/Loader';
import Table from '../../Shared/SettingTable/Table';
import InfoDisplay from '../../Shared/InfoDisplay';
import { truncate } from '../../../Utils/helpers/TruncateString';



type props = {
  addEmailTemplateClick? : any;
  insertTokenClick: Function;
  }

export const EmailTemplatesList = ({addEmailTemplateClick, insertTokenClick}: props) => {

  const { state, dispatch } = useContext(Store);
  const emailTemplateManger: any = state.requestEmailTemplateManager;
  const emailTemplates: RequestEmailTemplate[] = emailTemplateManger.requestEmailTemplateData;
  const removeTemplate = useRef<HTMLDivElement>(null);

  useEffect(() => {    
    const handler = (e:any) => {
      if(!removeTemplate.current?.contains(e.target)){
        document.querySelector<HTMLElement>('#removeAlertNo')?.click();
        
      }
    }
    document.addEventListener('click',handler);
    document.querySelectorAll('.settings-btn-sort').forEach((item:any)=>{
      item.addEventListener('click',handler);
    })
    return () => {document.removeEventListener('click',handler)}
  })

  const getEmailTemplates = async () => {   
      let result: any = await RequestEmailTemplateActions.fetchEmailTemplates();
      if(result){
      let sortedList = sortList(result, true);
        dispatch({type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: sortedList});
      }
  }
  
  const updateEmailTemplate = async (model: RequestEmailTemplate[]) => {
   let result : any = await RequestEmailTemplateActions.updateEmailTemplateSort(model);
  }

  const deleteEmailTemplate = async (id?: number) => {
    let sortedListWithLoader = await updateEmailTemplateListWithLoader(true, id);
    dispatch({type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: sortedListWithLoader});
    let result : any = await RequestEmailTemplateActions.deleteEmailTemplate(id);
    if(result){
      getEmailTemplates();
    }
  }

   useEffect(() => {   
      getEmailTemplates(); 
      dispatch({
            type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate,
            payload: null
      }); 
      insertTokenClick(false);   
   },[])

   const setSelectedEmailTemplateInStore = (emailTemplate: RequestEmailTemplate) => {
     
    dispatch({type: RequestEmailTemplateActionsType.SetSelectedEmailTemplate, payload: emailTemplate});
    addEmailTemplateClick();
   }

const updateEmailTemplateList = (fieldName: string, value?: boolean, id?: number) => {
  let updatedList: any = emailTemplates.map((item: any, index: number) => {
       if(item.id === id){
        item[fieldName] = value;
       }else if(id != undefined){
        item[fieldName] = !value;
       }else{
        item[fieldName] = value;
       }
       return item;
  })
  let sortedList = sortList(updatedList, true);
  dispatch({type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: sortedList});
}

const updateEmailTemplateListWithLoader = async  ( showLoader?: boolean, id?: number) => {
  let updatedList: any = emailTemplates.map((item: any, index: number) => {
      if(showLoader){
        if(item.id === id){
          item.showDelete = false;
          item.deleteReqSent = true;
        }
      }else{
        item.showDelete = false;
        item.deleteReqSent = false;
      }
      return item;
  })
  return sortList(updatedList, true);
}

const sortList = (list: any, isAsc: boolean) => {
  if(isAsc){
    list.sort(function(a: { sortOrder: number; },b: { sortOrder: number; }){
      return a.sortOrder - b.sortOrder;
    })
    return list;
  }else{
    list.sort(function(a: { sortOrder: number; },b: { sortOrder: number; }){
      return b.sortOrder - a.sortOrder;
    })
    return list;
  }
    
}

const arrayMove = (arr: any, oldIndex: number, newIndex: number) => {
    if (newIndex >= arr.length) {
      var k = newIndex - arr.length + 1;
      while (k--) {
          arr.push(undefined);
      }
  }
  arr.splice(newIndex, 0, arr.splice(oldIndex, 1)[0]);
  return arr; 
}

const swapElementInList = (isUpward: boolean, item: RequestEmailTemplate, index: number) => {
  let updatedList: RequestEmailTemplate[];
  if(isUpward){
    updatedList = arrayMove(emailTemplates, index, index-1)
  }else{
    updatedList = arrayMove(emailTemplates, index, index+1)
  }
  let sortUpdated = updatedList.map((item: RequestEmailTemplate, index: number) => {
        item.sortOrder = index+1;
        return item;
  });
  let sortedList = sortList(sortUpdated, true);
  dispatch({type: RequestEmailTemplateActionsType.SetRequestEmailTemplateData, payload: sortedList});
  updateEmailTemplate(sortedList);
}

const renderCrossButton = (item: RequestEmailTemplate) => {
  return(
    <>  
    { item.deleteReqSent 
      ?
      <Loader size="xs" />
      :
       <button onClick={ e => updateEmailTemplateList('showDelete', true, item.id)} className="settings-btn settings-btn-delete">
        <i className="zmdi zmdi-close"></i>
       </button>
    }
       
    </>
  )
}
const renderRemoveButton = (item: RequestEmailTemplate) => {
  return(
    <>
    <div data-testid="remove-button-dv" className="settings__list-alert" ref={removeTemplate} id="removeAlert">
                <span className="settings__list-alert--text">Remove this email template?</span>
                <div className="settings__list-alert--options">
                  <button data-testid="delete-no" className="settings-btn settings-btn-sm settings-btn-secondry" onClick={() => {updateEmailTemplateList('showDelete', false)}} id="removeAlertNo">No</button>
                  <button data-testid="delete-yes" className="settings-btn settings-btn-sm settings-btn-primary" onClick={async () => {deleteEmailTemplate(item.id)}}>Yes</button>
                </div>
    </div>
    </>
  )
}

const renderTableRows = () => {
  return emailTemplates.map((item: RequestEmailTemplate, index: number) => {
    return (
      <>      <tr key={index}>   
                     
              <td data-testid="td-template-name" onDoubleClick={() => setSelectedEmailTemplateInStore(item)} >{truncate(item.templateName, 56)}</td>
              <td data-testid="td-description" onDoubleClick={() => setSelectedEmailTemplateInStore(item)}>{truncate(item.templateDescription, 56)}</td>
              <td data-testid="td-subject" onDoubleClick={() => setSelectedEmailTemplateInStore(item)}>{truncate(item.subject, 56)}</td>
              <td data-testid="td-sort-order">
                <button onClick={() => swapElementInList(true, item, index)} className={`settings-btn settings-btn-sort ${index === 0 ? 'settings-btn-disabled': ''}`}><i className="zmdi zmdi-chevron-up"></i></button>
                <button onClick={() => swapElementInList(false, item, index)} className={`settings-btn settings-btn-sort ${index === emailTemplates.length -1 ? 'settings-btn-disabled': '' }`}><i className="zmdi zmdi-chevron-down"></i></button>
              </td>
              <td data-testid="td-delete">
                { item.showDelete 
                ? 
                renderRemoveButton(item)
                : 
                renderCrossButton(item)
                }                
              </td>
              </tr>     
            
      </>
    ) 
  });  
}


if(!emailTemplates)
  return <WidgetLoader reduceHeight=""/>


  return (
    <>     
      <table data-testid={`request-email-templates-records`} className="table table-striped request-email-templates-records">
        <colgroup>
          <col span={1} style={{ width: '38%' }} />
          <col span={2} style={{ width: '25%' }} />
          <col span={3} style={{ width: '25%' }} />
          <col span={4} style={{ width: '10%' }} />
          <col span={5} style={{ width: '15%' }} />
        </colgroup>
        <thead>
          <tr>
            <th data-testid="thead-template-name" scope="col">Template Name</th>
            <th data-testid="thead-description" scope="col" >Description</th>
            <th data-testid="thead-subject" scope="col" >Subject  </th>
            <th data-testid="thead-sort-order" scope="col">Sort Order <InfoDisplay tooltipType={6} /></th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody data-testid="templates-records-body">
          {renderTableRows()}
        </tbody>
      </table>
    </>
  )
}
