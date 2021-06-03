import ContentBody from "../../Shared/ContentBody";
import {RequestEmailTemplateActionsType} from "../../../Store/reducers/RequestEmailTemplateReducer";
import ContentFooter from "../../Shared/ContentFooter";
import TableSort from "../../Shared/SettingTable/TableSort";
import React, { useContext, useEffect, useState } from 'react'
import { LoanOfficersActions } from "../../../Store/actions/LoanOfficersActions";
import { LoanOfficerActionType } from '../../../Store/reducers/LoanOfficerReducer';
import { Store } from '../../../Store/Store';
import Loader, {WidgetLoader} from '../../Shared/Loader';
import Table from '../../Shared/SettingTable/Table';
import TableROW from '../../Shared/SettingTable/TableROW';
import TableTD from '../../Shared/SettingTable/TableTD';
import TableTH from '../../Shared/SettingTable/TableTH';
import LoanOfficer from "../../../Entities/Models/LoanOfficer";
import { identity,cloneDeep,orderBy } from "lodash";
import { sortList } from "../../../Utils/helpers/Sort";
import { SimpleSort } from "../../../Utils/helpers/Enums";

type Props = {
    
}

export const LOSUsers = ({  }: Props) => {
    const { state, dispatch } = useContext(Store);
    const loanOfficerManager: any = state.loanOfficerManager;
    const loanOfficerData = loanOfficerManager.loanOfficerData;
    const [footer, setFooter] = useState<boolean>(false);
    const [loanOfficers, setLoanOfficer] = useState<any>();
    
    const  fetchLOSSettings = async () => {
        
        let data = await LoanOfficersActions.fetchLoanOfficersSettings();
        //const local: any = JSON.parse(JSON.stringify(data))
        const localOfficers = cloneDeep(data);
        setLoanOfficer(localOfficers);
        dispatch({ type: LoanOfficerActionType.SetLoanOfficerData, payload: data })   
    }
    const updateLOSusers = async (loanOffices:LoanOfficer[]) => {

        let result = await LoanOfficersActions.updateLoanOfficersSettings(loanOfficers)
    }
    useEffect(() => {
        fetchLOSSettings();
    }, []);

    const handlerFocus =() => {
        setFooter(true);
    }
    const handlerChange = (e: any) => {
  
        const {id, value} = e.target;
        let users: any = loanOfficers?.map((item: any, index: number) => {
              
               if(item.userId.toString() === id){
                   item.byteUserName = value;
               }
               return item;
        });
        const localOfficers = cloneDeep(users);
        //const local: any = JSON.parse(JSON.stringify(users))
        setLoanOfficer(localOfficers);
    }
    const handlerUpdate =() => {
        updateLOSusers(loanOfficers);
        dispatch({ type: LoanOfficerActionType.SetLoanOfficerData, payload: loanOfficers })
        setFooter(false);
    }
    const handlerCancel =() => {
        const localOfficers = cloneDeep(loanOfficerData);
        //const local: any = JSON.parse(JSON.stringify(loanOfficerData))
        setLoanOfficer(localOfficers);
        setFooter(false);
       }
    const renderRows = () => {
        return loanOfficers?.map((d:LoanOfficer) => {

            return (
                <tr data-testid="input-rows">
                      <td className="dp"><figure><img src={`data:image/jpeg;base64,${d?.photo}`} /></figure></td>
                      <td>{d?.userName}</td>
                      <td>
                          <input data-testid="input-text" className={`settings__control`} type="text" value={d?.byteUserName}  id={String(d.userId)}  onFocus={handlerFocus} onChange={(e) => handlerChange(e)} />
                      </td>
                      <td></td>
                </tr>
            )
        })
    }
    const  arrowSortHandler = (sortOrder:any) => {
        const users = cloneDeep(loanOfficers);
      
        if(sortOrder == SimpleSort.Up){
            sortList(users,"userName",false);
        }
        else if(sortOrder == SimpleSort.Down){
            sortList(users,"userName",true);
        }
        setLoanOfficer(users);
    }

    if(!loanOfficers){
        return(<WidgetLoader reduceHeight="110px"/>)
    }else{
        return (
            <>
                <ContentBody className="loan-origination-system los-user-list">
                <table className="table table-striped request-email-templates-records">
                          <colgroup>
                            <col span={1} style={{ width: '1%' }} />
                            <col span={2} style={{ width: '10%' }} />
                            <col span={2} style={{ width: '15%' }} />
                            <col span={3} />
                        </colgroup>
                        <thead>
                              <tr>
                                  <th scope="col"></th>
                                  <th data-testid="th-templateName" scope="col"><TableSort callBackFunction = {arrowSortHandler}>Name</TableSort></th>
                                  <th data-testid="th-byteusername"scope="col">Byte User Name</th>
                                  <th scope="col"></th>
                              </tr>
                        </thead>
                        <tbody>
                        {renderRows()}
                        </tbody>
                  
                  </table>
                </ContentBody>
                {
                    footer &&
                    <ContentFooter>
                    <button data-testid= "save-btn" type="submit" className="settings-btn settings-btn-primary" onClick={() =>handlerUpdate()}>Save</button>
                    <button data-testid= "cancel-btn" className="settings-btn settings-btn-secondry"  onClick={() =>handlerCancel()}>Cancel</button>
                    </ContentFooter>
                }
               
            </>
        )
    }
  
}
