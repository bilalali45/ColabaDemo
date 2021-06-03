export class GettingStartedEndpoints {
    static GET = {
        getLoInfo: () => '/api/loanapplication/loan/getloinfo',
        getAllLoanPurpose: () => `/api/loanapplication/loanpurpose/getAllloanpurpose`,
        getAllLoanGoal: (purposeId: number) => `/api/loanapplication/loangoal/getAllloangoal?purposeId=${purposeId}`,
        getLoanGoal: (loanApplicationId: number) => `/api/loanapplication/loangoal/getloangoal?loanApplicationId=${loanApplicationId}`
    };
    static POST = {
        createLoanGoal: () => `/api/loanapplication/loangoal/addorupdate`
    };
    static PUT = {
       
    };
    static DELETE = {

    };
};
