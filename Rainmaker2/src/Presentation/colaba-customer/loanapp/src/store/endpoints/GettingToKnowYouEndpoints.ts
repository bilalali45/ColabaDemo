export class GettingToKnowYouEndpoints {
    static GET = {
        getAllCountries: ()=> ('/api/loanapplication/address/getAllcountry'),
        getAllStates: ()=> (`/api/loanapplication/address/getallstate`),
        getTenantStates: ()=> (`/api/loanapplication/address/gettenantstate`),
        getHomeOwnershipTypes: () => (`/api/loanapplication/address/getallownershiptype`),
        getBorrowerAddress: (loanApplicationId:number, borrowerId:number) =>(`/api/loanapplication/borrower/getborroweraddress?loanapplicationid=${loanApplicationId}&borrowerId=${borrowerId}`),
        getSearchByZipCode: (searchKey:number) => (`/api/loanapplication/address/getsearchbyzipcode?searchKey=${searchKey}`),
        getSearchByStateCountyCity:(searchKey:string) => (`/api/loanapplication/address/GetSearchByStateCountyCity?searchKey=${searchKey}`),
        getZipCodeByStateCountryCity: (cityId:number, countryId:number, stateId: number) => (`/api/loanapplication/address/GetZipCodeByStateCountyCity?cityId=${cityId}&countyId=${countryId}&stateId=${stateId}`),
        getZipCodeByStateCountyCityName: (cityName:string,stateName:string,countyName:string,zipCode:string) => (`/api/loanapplication/address/GetZipCodeByStateCountyCityName?cityName=${cityName}&stateName=${stateName}&countyName=${countyName}&zipCode=${zipCode}`)

    };
    static POST = {
        addOrUpdatePrimaryBorrowerAddress: ()=> (`/api/loanapplication/borrower/AddOrUpdateAddress`),
        addOrUpdateDobSsn: ()=> (`/api/loanapplication/borrower/AddOrUpdateDobSsn`)
    };
    static PUT = {
       
    };
    static DELETE = {

    };
};
