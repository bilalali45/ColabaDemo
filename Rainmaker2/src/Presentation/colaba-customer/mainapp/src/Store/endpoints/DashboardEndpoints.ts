export class DashboardEndpoints {
    static GET = {
        fetchLoggedInUserLoanApplications: ()=> ('/api/loanapplication/loan/GetDashboardLoanInfo')
    };
    static POST = {
        
    };
    static PUT = {
        setTenantSettings: () => (`/api/tenantconfig/tenant/putsetting`)
    };
    static DELETE = {

    };
};
