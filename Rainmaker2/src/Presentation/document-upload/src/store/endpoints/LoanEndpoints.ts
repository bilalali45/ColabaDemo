export class LoanEndpoints {
  static GET = {
    officer: (loanApplicationId: string) =>
      `/api/Rainmaker/LoanApplication/GetLOInfo?loanApplicationId=${loanApplicationId}`,
    info: (loanApplicationId: string) =>
      `/api/Rainmaker/LoanApplication/GetLoanInfo?loanApplicationId=${Number(
        loanApplicationId
      )}`,
    getLOPhoto: (lOPhotoId?: string, loanApplicationId?: string) =>
      `/api/Rainmaker/LoanApplication/GetPhoto?photo=${lOPhotoId}&loanApplicationId=${loanApplicationId}`,
    loanProgressStatus: (loanApplicationId: string) =>
      `/api/Documentmanagement/dashboard/GetDashboardStatus?loanApplicationId=${loanApplicationId}`,
    getFooter: (loanApplicationId: string) => `/api/Documentmanagement/dashboard/GetFooterText?loanApplicationId=${loanApplicationId}`,
  };

  static POST = {};

  static PUT = {};

  static DELETE = {
    // deleteLoanApplication: (id: any) => `/api/deleteLoanApplication/${id}`
  };
}
