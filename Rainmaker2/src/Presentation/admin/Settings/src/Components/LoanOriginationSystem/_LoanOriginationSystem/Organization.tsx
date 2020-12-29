import React, { useContext, useEffect, useState } from 'react'
import { cloneDeep } from "lodash";
import { OrganizationsActions } from '../../../Store/actions/OrganizationActions';
import ContentBody from "../../Shared/ContentBody";
import ContentFooter from "../../Shared/ContentFooter";
import TableSort from "../../Shared/SettingTable/TableSort";
import { Store } from '../../../Store/Store';
import { OrganizationActionsType } from '../../../Store/reducers/OrganizationReducer';
import Organization from '../../../Entities/Models/Organization';
import { LocalDB } from '../../../Utils/LocalDB';
import { Role } from '../../../Store/Navigation';
import { sortList } from '../../../Utils/helpers/Sort';
import { SimpleSort } from '../../../Utils/helpers/Enums';


type Props = {
  backHandler?: Function
}

export const LOSOrganization = ({ backHandler }: Props) => {
    const { state, dispatch } = useContext(Store);
    const organizationManager: any = state.organizationManager;
    const organizationData = organizationManager.organizationData;
    const [footer, setFooter] = useState<boolean>(false);
    const [organizations, setOrganization] = useState<any>();
    const role = LocalDB.getUserRole();

    const  fetchOrganizationSettings = async () => {
        let data = await OrganizationsActions.fetchOrganizationSettings();
        const organizations = cloneDeep(data);
        setOrganization(organizations);
        dispatch({ type: OrganizationActionsType.SetOrganizationData, payload: data })   
    }
    const updateOrganizations = async () => {
        let result = await OrganizationsActions.updateOrganizationSettings(organizations)
    }
    const handlerUpdate =() => {
        updateOrganizations();
        dispatch({ type: OrganizationActionsType.SetOrganizationData, payload: organizations })
        setFooter(false);
    }
    const handlerFocus =() => {
        setFooter(true);
    }
    const handlerChange = (e: any) => {
        const {id, value} = e.target;
        let organization: any = organizations?.map((item: Organization, index: number) => {            
               if(item.id.toString() === id){
                   item.byteOrganizationCode = value;
               }
               return item;
        });
        const localOrganizations = cloneDeep(organization);
        setOrganization(localOrganizations);
    }
    const handlerCancel =() => {
        const organizations = cloneDeep(organizationData);
        setOrganization(organizations);
        setFooter(false);
       }

    useEffect(() => {
        fetchOrganizationSettings();
    }, []);
    const  arrowSortHandler = (sortOrder:any) => {
        const organization = cloneDeep(organizations);
        if(sortOrder == SimpleSort.Down){
            sortList(organization,"name",false);
        }
        else if(sortOrder == SimpleSort.Up){
            sortList(organization,"name",true);
        }
        setOrganization(organization);
    }
    const renderRows = () => {
        return organizations?.map((org:Organization) => {
            return (
                <tr data-testid="input-rows">
                <td className="dp"><figure><img src={`data:image/jpeg;base64,${org?.photo}`}/></figure></td>
                <td>{org.name}</td>
                <td>
                {role === Role.SYSTEM_ROLE 
                 ?
                    <input className={`settings__control`} type="text" value={org?.byteOrganizationCode}  id={String(org.id)}  onFocus={handlerFocus} onChange={(e) => handlerChange(e)} />
                  :
                  org?.byteOrganizationCode
                }
                
                </td>
                <td></td>
            </tr>
            )
        })
    }
  return (
    <>
      <ContentBody className="loan-origination-system los-organization-list">
          <table className="table table-striped request-email-templates-records">
              <colgroup>
                  <col span={1} style={{ width: '2%' }} />
                  <col span={2} style={{ width: '30%' }} />
                  <col span={2} style={{ width: '15%' }} />
                  <col span={3} />
              </colgroup>
              <thead>
              <tr>
                  <th scope="col"></th>
                  <th data-testid="th-templateName" scope="col"><TableSort callBackFunction = {arrowSortHandler}>Name</TableSort></th>
                  <th data-testid="th-byteOrgCode"  scope="col">Byte Organization Code</th>
                  <th scope="col"></th>
              </tr>
              </thead>
              <tbody>
              {renderRows()}
              {/* <tr>
                  <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                  <td>Texas Trust Home Loans</td>
                  <td>302039</td>
                  <td></td>
              </tr>
              <tr>
                  <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                  <td>AHC Lending</td>
                  <td>100390</td>
                  <td></td>
              </tr> */}
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
