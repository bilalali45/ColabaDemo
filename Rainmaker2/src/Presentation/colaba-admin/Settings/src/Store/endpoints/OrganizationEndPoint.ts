export class OrganizationEndpoints {
    
    static GET = {
        getOrganizationSetting: () => `/api/rainmaker/setting/GetBusinessUnits`
    }

    static POST ={
        updateOrganizationSettings: () => `/api/rainmaker/setting/UpdateByteOrganizationCode`
    }
}