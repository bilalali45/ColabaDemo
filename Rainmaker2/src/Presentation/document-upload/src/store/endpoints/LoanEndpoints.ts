export class LoanEndpoints {
  static GET = {
    officer: (loanApplicationId: string) =>
      `/api/Rainmaker/LoanApplication/GetLOInfo?loanApplicationId=${loanApplicationId}`,
    info: (loanApplicationId: string) =>
      `/api/Rainmaker/LoanApplication/GetLoanInfo?loanApplicationId=${Number(
        loanApplicationId
      )}`,
    getLOPhoto: (lOPhotoId?: string) =>
      `/api/Rainmaker/LoanApplication/GetPhoto?photo=${lOPhotoId}`,
    loanProgressStatus: (loanApplicationId: string) =>
      `/api/Documentmanagement/dashboard/GetDashboardStatus?loanApplicationId=${loanApplicationId}`,
    getFooter: () => `/api/Documentmanagement/dashboard/GetFooterText`,
  };

  static POST = {};

  static PUT = {};

  static DELETE = {
    // deleteLoanApplication: (id: any) => `/api/deleteLoanApplication/${id}`
  };
}
