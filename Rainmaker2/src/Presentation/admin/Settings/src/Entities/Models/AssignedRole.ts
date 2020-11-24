export class AssignedRole {
    public roleId?: string;
    public roleName?: string;
    public isRoleAssigned?: boolean;

    constructor(roleId?: string, roleName?: string, isRoleAssigned?: boolean){
        this.roleId = roleId;
        this.roleName = roleName;
        this.isRoleAssigned = isRoleAssigned;
    }

    updateRole(roleId?: string, isRoleAssigned?: boolean){
       if(this.roleId === roleId){
        this.isRoleAssigned = isRoleAssigned;
       }
       return this;
   }
}