export default class MilitaryEndpoints {
    static GET = {
      getMilitaryAffiliationsList: () => ('/api/loanapplication/va/GetMilitaryAffiliationsList'),
      getBorrowerVaDetail: (borrowerId :number) => (`/api/loanapplication/va/GetBorrowerVaDetail?BorrowerId=${borrowerId}`)
    };
    static POST = {
      AddOrUpdateBorrowerVaStatus: () => ('/api/loanapplication/va/AddOrUpdateBorrowerVaStatus')
    };
    static PUT = {
  
    };
    static DELETE = {
  
    };
  };
  