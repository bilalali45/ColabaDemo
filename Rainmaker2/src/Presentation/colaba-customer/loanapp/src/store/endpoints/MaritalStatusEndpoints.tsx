export default class MaritalStatusEndpoints {
  static GET = {
    
    getAllMaritalstatus: () => ('/api/loanapplication/loan/getallmaritalstatus'),
    getMaritalStatus: (loanapplicationId: number, borrowerId: number) => (`/api/loanapplication/borrower/GetMaritalStatus?loanapplicationid=${loanapplicationId}&borrowerId=${borrowerId}`),
    isRelationAlreadyMapped: (loanapplicationId: number, borrowerId: number) => (`/api/loanapplication/borrower/IsRelationAlreadyMapped?loanapplicationId=${loanapplicationId}&borrowerId=${borrowerId}`),
  };
  static POST = {
    addOrUpdateMaritalStatus: () => ('/api/loanapplication/borrower/AddOrUpdateMaritalStatus'),
  };
  static PUT = {

  };
  static DELETE = {

  };
};
