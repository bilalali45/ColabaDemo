export class MyNewMortgageEndpoints {
    static GET = {
        getAllpropertytypes: () => `/api/loanapplication/property/getAllpropertytypes`,
        getpropertytype: (loanApplicationId: number) => `/api/loanapplication/property/getpropertytype?loanApplicationId=${loanApplicationId}`,
        getSubjectPropertyLoanAmountDetail: (loanApplicationId: number) => `/api/loanapplication/subjectproperty/GetSubjectPropertyLoanAmountDetail?LoanApplicationId=${loanApplicationId}`,
        getPropertyIdentifiedFlag: (loanApplicationId: number) => `/api/loanapplication/subjectproperty/GetPropertyIdentifiedFlag?LoanApplicationId=${loanApplicationId}`
    };
    static POST = {
        addorupdatepropertytype: () => `/api/loanapplication/property/addorupdatepropertytype`,
        addOrUpdateLoanAmountDetail: () => `/api/loanapplication/subjectproperty/AddOrUpdateLoanAmountDetail`,
        updatePropertyIdentified: () => `/api/loanapplication/subjectproperty/UpdatePropertyIdentified`
    };
    static PUT = {
       
    };
    static DELETE = {
        
    };
};

