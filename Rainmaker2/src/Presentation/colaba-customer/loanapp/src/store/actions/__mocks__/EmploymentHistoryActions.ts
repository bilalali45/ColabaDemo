import {APIResponse} from "../../../Entities/Models/APIResponse";
import {EmploymentHistoryDetails} from "../../../Entities/Models/EmploymentHistory";
const borrowerIncome ={"loanApplicationId":2,"borrowerId":1291,"borrowerName":"third third","ownType":{"ownTypeId":2,"name":"Secondary Contact","ownTypeDisplayName":"Secondary Contact"},"borrowerIncomes":[{"incomeInfoId":52,"employerName":"third previous","startDate":"2021-03-29T00:00:00","endDate":null,"isCurrentEmployment":true,"incomeType":{"incomeTypeId":1,"name":"EmploymentInfo","displayName":"EmploymentInfo"}},{"incomeInfoId":1020,"employerName":"previous","startDate":"2021-04-25T00:00:00","endDate":"2021-04-23T00:00:00","isCurrentEmployment":false,"incomeType":{"incomeTypeId":1,"name":"EmploymentInfo","displayName":"EmploymentInfo"}},{"incomeInfoId":1024,"employerName":"previous","startDate":"2021-04-22T00:00:00","endDate":"2021-04-20T00:00:00","isCurrentEmployment":false,"incomeType":{"incomeTypeId":1,"name":"EmploymentInfo","displayName":"EmploymentInfo"}}]}

const borrowerEmplymentHistory = {"loanApplicationId":2,"requiresHistory":true,"borrowerEmploymentHistory":[{"borrowerId":1291,"borrowerName":"third third","employmentHistory":[{"incomeInfoId":52,"employerName":"third previous","startDate":"2021-03-29T00:00:00","endDate":null,"isCurrentEmployment":true},{"incomeInfoId":1020,"employerName":"previous","startDate":"2021-04-25T00:00:00","endDate":"2021-04-23T00:00:00","isCurrentEmployment":false},{"incomeInfoId":1024,"employerName":"previous","startDate":"2021-04-22T00:00:00","endDate":"2021-04-20T00:00:00","isCurrentEmployment":false}]},{"borrowerId":1296,"borrowerName":"forth forth","employmentHistory":[{"incomeInfoId":1021,"employerName":"business","startDate":"2021-04-26T00:00:00","endDate":null,"isCurrentEmployment":true}]}]}
export default class EmploymentHistoryActions {

    static async getBorrowersEmploymentHistory(loanApplicationId: number) {
        return new APIResponse(200, borrowerEmplymentHistory );
    }

    static async GetBorrowerIncomes(loanApplicationId: number, borrowerId:number) {
        return new APIResponse(200, borrowerIncome );
    }

    static async addOrUpdatePreviousEmploymentDetail(dataValues: EmploymentHistoryDetails) {
        return new APIResponse(200, 1020 );

    }
}
