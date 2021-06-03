import { APIResponse } from "../../../Entities/Models/APIResponse";
import { CurrentEmploymentDetails } from "../../../Entities/Models/Employment";

const borrowerInfo = {
    employmentInfo:{
    EmployerName: "Abacusoft",
    JobTitle: "Senior Software Engineer",
    StartDate: "2018-01-08T00:00:00",
    EndDate: "2018-08-31T00:00:00",
    YearsInProfession: 15,
    EmployerPhoneNumber: "1234567890",
    EmployedByFamilyOrParty: null,
    HasOwnershipInterest: true,
    OwnershipInterest: "21.00",
    IncomeInfoId: 66
  },
  employerAddress:{
    streetAddress: "Defence Phase VI",
    unitNo: "4th Floor",
    cityId: 1,
    cityName: "Thatta",
    countryId: 1,
    stateId: 1,
    stateName: "Sindh",
    zipCode: "73130"
  },
  wayOfIncome:{
    isPaidByMonthlySalary: false,
    hourlyRate: null,
    hoursPerWeek: null,
    employerAnnualSalary:""
  }, 
  employmentOtherIncomes:[{
    incomeTypeId:2,
    annualIncome:3232,
    name:null, 
    displayName:null
  }]}
  const otherIncome = [{"id":2,"name":"Bonus","displayName":"Bonus"},{"id":3,"name":"Commission","displayName":"Commission"},{"id":1,"name":"Overtime","displayName":"Overtime"}]
export default class EmploymentActions {

    static async getEmployerInfo(loanApplicationId: number, borrowerId: number) {
        return new APIResponse(200, borrowerInfo);
        
    }

    static async addOrUpdateCurrentEmployer(dataValues: CurrentEmploymentDetails) {
        return new APIResponse(200, true);

    }

    static async getEmploymentOtherDefaultIncomeTypes() {
        return new APIResponse(200, otherIncome);
    }
}