export class RetirementIncomeEndpoints {
  static GET = {
    GetRetirementIncomeTypes: () => ('/api/loanapplication/income/GetRetirementIncomeTypes'),
    GetRetirementIncomeInfo: () => ('/api/loanapplication/income/GetRetirementIncomeInfo')
  };
  static POST = {
    AddOrUpdateRetirementIncomeInfo: () => ('/api/loanapplication/income/AddOrUpdateRetirementIncomeInfo')
  };
  static PUT = {

  };
  static DELETE = {

  };
};
