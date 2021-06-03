export class MyNewPropertyAddressEndpoints {
    static GET = {
        getPropertyAddress: (loanApplicationId:number) =>(`/api/loanapplication/property/getpropertyaddress?loanApplicationId=${loanApplicationId}`),

    };
    static POST = {
        addOrUpdatePropertyAddress: ()=> (`/api/loanapplication/property/addorupdatepropertyaddress`),
    };
    static PUT = {
       
    };
    static DELETE = {

    };
};
