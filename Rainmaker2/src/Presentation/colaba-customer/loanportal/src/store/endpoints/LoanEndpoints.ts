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
      `/api/milestone/milestone/GetMilestoneForloancenter?loanApplicationId=${loanApplicationId}`,
    getFooter: (loanApplicationId: string) => `/api/Documentmanagement/dashboard/GetFooterText?loanApplicationId=${loanApplicationId}`,
    getCompanyLogoSrc: (loanApplicationId: string) => `/api/Rainmaker/LoanApplication/GetBanner?loanApplicationId=${loanApplicationId}`,
    getCompanyFavIconSrc: (loanApplicationId: string) => `/api/Rainmaker/LoanApplication/GetFavIcon?loanApplicationId=${loanApplicationId}`
  
  };

  static POST = {};

  static PUT = {};

  static DELETE = {
    // deleteLoanApplication: (id: any) => `/api/deleteLoanApplication/${id}`
  };
}
