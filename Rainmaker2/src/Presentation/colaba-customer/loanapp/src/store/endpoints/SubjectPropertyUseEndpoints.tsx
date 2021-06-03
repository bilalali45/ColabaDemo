export default class SubjectPropertyUseEndpoints {
    static GET = {
      getPropertyUsage: (loanApplicationId :number) => (`/api/loanapplication/property/getpropertyusage?loanApplicationId=${loanApplicationId}`),
      getAllpropertyUsages: () => (`/api/loanapplication/property/getAllpropertyusages`)
    };
    static POST = {
      addOrUpdatePropertyUsage: () => ('/api/loanapplication/property/addorupdatepropertyusage')
    };
    static PUT = {
  
    };
    static DELETE = {
  
    };
  };
  