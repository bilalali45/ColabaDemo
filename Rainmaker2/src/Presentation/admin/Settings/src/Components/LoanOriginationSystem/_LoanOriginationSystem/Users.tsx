import React, { useContext, useEffect, useState } from 'react'
import { cloneDeep } from "lodash";
import ContentBody from "../../Shared/ContentBody";
import ContentFooter from "../../Shared/ContentFooter";
import TableSort from "../../Shared/SettingTable/TableSort";
import { LoanOfficersActions } from "../../../Store/actions/LoanOfficersActions";
import { LoanOfficerActionType } from '../../../Store/reducers/LoanOfficerReducer';
import { Store } from '../../../Store/Store';
import LoanOfficer from "../../../Entities/Models/LoanOfficer";

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
        const localOfficers = cloneDeep(data);
        setLoanOfficer(localOfficers);
        dispatch({ type: LoanOfficerActionType.SetLoanOfficerData, payload: data })   
    }
    const updateLOSusers = async () => {
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
        setLoanOfficer(localOfficers);
    }

    const handlerUpdate =() => {
        updateLOSusers();
        dispatch({ type: LoanOfficerActionType.SetLoanOfficerData, payload: loanOfficers })
        setFooter(false);
    }

    const handlerCancel =() => {
        const localOfficers = cloneDeep(loanOfficerData);
        setLoanOfficer(localOfficers);
        setFooter(false);
       }

    const renderRows = () => {
        return loanOfficers?.map((d:LoanOfficer) => {
            return (
                <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>{d?.userName}</td>
                      <td>
                          <input className={`settings__control`} type="text" value={d?.byteUserName}  id={String(d.userId)}  onFocus={handlerFocus} onChange={(e) => handlerChange(e)} />
                      </td>
                      <td></td>
                </tr>
            )
        })
    }

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
                            <th data-testid="th-templateName" scope="col"><TableSort>Name</TableSort></th>
                            <th scope="col">Byte User Name</th>
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
