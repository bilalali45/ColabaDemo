import { AssignedRole } from "../../../Entities/Models/AssignedRole";

const mock = [
    {
        "roleId": 2,
        "roleName": "Executives",
        "isRoleAssigned": true
    },
    {
        "roleId": 3,
        "roleName": "Staff Employee",
        "isRoleAssigned": true
    },
    {
        "roleId": 4,
        "roleName": "Administrator",
        "isRoleAssigned": false
    },
    {
        "roleId": 5,
        "roleName": "Manager - Production",
        "isRoleAssigned": false
    },
    {
        "roleId": 6,
        "roleName": "Operations Manager",
        "isRoleAssigned": false
    },
    {
        "roleId": 7,
        "roleName": "Manager - Loan Sales",
        "isRoleAssigned": false
    },
    {
        "roleId": 8,
        "roleName": "Loan Sales",
        "isRoleAssigned": false
    },
    {
        "roleId": 9,
        "roleName": "Site Content Manager",
        "isRoleAssigned": false
    },
    {
        "roleId": 10,
        "roleName": "System Role",
        "isRoleAssigned": false
    },
    {
        "roleId": 11,
        "roleName": "SEO",
        "isRoleAssigned": false
    },
    {
        "roleId": 1011,
        "roleName": "Loan Officer",
        "isRoleAssigned": false
    },
    {
        "roleId": 1012,
        "roleName": "Loan Coordinator",
        "isRoleAssigned": false
    },
    {
        "roleId": 1013,
        "roleName": "Pre Processor",
        "isRoleAssigned": false
    },
    {
        "roleId": 1014,
        "roleName": "Loan Processor",
        "isRoleAssigned": false
    },
    {
        "roleId": 1015,
        "roleName": "SupportTeam",
        "isRoleAssigned": true
    }
]

export class AssignedRoleActions {

    static async fetchUserRoles() {     
        let mappedData = mock.map((d:any) => {
            return new AssignedRole(d.roleId, d.roleName, d.isRoleAssigned);
      });
          return Promise.resolve(mappedData);
    }

    static async updateUserRoles(assignedRolesModel: AssignedRole){
        return Promise.resolve(true);
    }
}