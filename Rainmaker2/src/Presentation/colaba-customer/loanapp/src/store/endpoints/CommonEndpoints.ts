export class CommonEndpoints {
    static GET = {
        getallmaritalstatus: () => ('/api/loanapplication/loan/getallmaritalstatus'),
        getAllloanpurpose: () => ('/api/loanapplication/loanpurpose/getAllloanpurpose'),

    };
    static POST = {
        saveNavigation: () => `/api/loanapplication/loan/UpdateState`
    };
    static PUT = {

    };
    static DELETE = {

    };
};

