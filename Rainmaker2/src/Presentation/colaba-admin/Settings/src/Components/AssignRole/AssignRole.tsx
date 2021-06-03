import { stat } from 'fs';
import React, { useContext, useEffect, useState } from 'react';
import { Store } from '../../Store/Store';
import { AssignedRole } from '../../Entities/Models/AssignedRole';
import { AssignedRoleActions } from '../../Store/actions/AssignedRoleActions';
import { AssignedRoleActionsType } from '../../Store/reducers/AssignedRoleReducer';
import Loader, {WidgetLoader} from '../Shared/Loader';
import { LocalDB } from '../../Utils/LocalDB';
import { Role } from '../../Store/Navigation';
import InputCheckedBox from '../Shared/InputCheckedBox';


const AssignRole = () => {
    
    const { state, dispatch } = useContext(Store);

    const assignedRoleManager: any = state.assignedRolesManager;
    const assignedRoles = assignedRoleManager?.assignedRoles;
    
    const [userRole, setUserRole] = useState();

    const getUserAssignedRoles = async () => {
     let res: AssignedRole[] | undefined = await AssignedRoleActions.fetchUserRoles();
     if(res){
        let sortedList = sortList(res, true);
        dispatch({ type: AssignedRoleActionsType.SET_ASSIGNED_ROLES, payload: sortedList })
     }
    }

    const sortList = (list: any, isAsc: boolean) => {
         if(isAsc){
        return list.sort(function(a: any, b: any){
            if(a.roleName < b.roleName) { return -1; }
            if(a.roleName > b.roleName) { return 1; }
            return 0;
        })
         }         
      }
      

    const updateAssignedRole = async (roleId?: string, roleName?: string, isRoleAssigned?: boolean) => {
        
        let data: AssignedRole = await assignedRoles.map((d: AssignedRole) => {
            if(d.roleId === roleId){
                d.updateRole(roleId, !isRoleAssigned)
            }
            return d;
            
        });  
       dispatch({ type: AssignedRoleActionsType.SET_ASSIGNED_ROLES, payload: data })

        let res = await AssignedRoleActions.updateUserRoles(data);
    
    }

    const getAndSetUserRole = () => {
        let role: any = LocalDB.getUserRole();
        setUserRole(role);
    };

    useEffect(() => {
        getAndSetUserRole();
      }, []);

    useEffect(() => {
        getUserAssignedRoles();
    }, [])

    if(!assignedRoles){
        return <WidgetLoader reduceHeight=""/>
    }

    const renderAssignedRolesList = () => {
      return (
          <>
           <div data-testid="assigned-roleDv" className="settings__assigned-role" data-component="AssignRole">
            <h4 className="h4">Team Roles</h4>

            <div className="settings__assigned-role--list">
                <ul>  
                    {
                      assignedRoles.map((d: AssignedRole)=> {
                      return <li data-testid="assigned-role-li" className={d.isRoleAssigned ? 'assigned' : 'unassigned'}><label>{d.roleName}</label></li>
                                                            })  
                    }                 
                </ul>
            </div>
        </div>
          </>
      )
    }

    const renderAssignedRolesWithCheckBox = () => {
        return (
            <>
            <div data-testid="assigned-roleDv" className="settings__assigned-role" data-component="AssignRole">
            <h4 className="h4">Team Roles Settings</h4>

            <div className="settings__assigned-role--list">
                <ul>
                    {
                      assignedRoles.map((d: AssignedRole, index:any)=> {                     
                        return(<li key={index} data-testid="assigned-role-li" className={d.isRoleAssigned ? 'assigned' : 'unassigned'}>
                            <InputCheckedBox data-testid="assignedRole-input" onchange={(e:any) => updateAssignedRole(d.roleId, d.roleName, d.isRoleAssigned)} checked={d.isRoleAssigned === true} name={d.roleName} id={d.roleId}>{d.roleName}</InputCheckedBox>
                            </li>) 
                      })     
                    }                
                </ul>
            </div>
        </div>
            </>
        )
    }

    return (
        <>
       {
        userRole === Role.MCU_ROLE ? renderAssignedRolesList() : renderAssignedRolesWithCheckBox()
       }
       </>
    )
}

export default AssignRole;
