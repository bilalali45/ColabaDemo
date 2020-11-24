export class AssignedRoleEndpoints {
    static GET ={
        getAssignedRoles: () => `/api/Setting/RainMaker/GetUserRoles`
    }

    static POST = {
       updateAssignedRole: () => `/api/Setting/RainMaker/UpdateUserRoles`
    }
}