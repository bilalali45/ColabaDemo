import { APIResponse } from "../../../Entities/Models/APIResponse";

const loInfoMock = {
    "name": "Aliya",
    "image": "https://apply.lendova.com:5003/colabacdn/lendova/lendova/aliya.jpeg",
    "phone": "(458) 697-4236",
    "email": "rainmaker.rainsoft@gmail.com",
    "url": "https://www.texastrust.com",
    "nmlsNo": "45612",
    "isLoanOfficer": true
}

const allLoanPurposeMock = [
    {
        "id": 1,
        "name": "Purchase",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 2,
        "name": "Refinance",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 3,
        "name": "Cash Out",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    }
]

const allLoanGoal = [
    {
        "id": 4,
        "name": "Property Under Contract",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 3,
        "name": "Pre-Approval",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 1,
        "name": "Researching Loan Options",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    },
    {
        "id": 2,
        "name": "Going to Open Houses",
        "image": "https://apply.lendova.com:5003/colabacdn/loangoal.svg"
    }
]

const loanGoal = {"loanGoal":4,"loanPurpose":1}


export default class GettingStartedActions {

   static async getLoInfo() {
      return new APIResponse(200, loInfoMock);
   }

   static async getAllLoanPurpose() {
    return new APIResponse(200,allLoanPurposeMock);
   }

   static async getAllLoanGoal(purposeId: number) {
    return new APIResponse(200, allLoanGoal);
   } 

   static async getLoanGoal(loanApplicationId: number) {
    return new APIResponse(200, loanGoal);
} 

   static async createLoanGoal(loanApplicationId: number, loanGoalId: number, loanPurposeId: number, state: string){
     
    return new APIResponse(200, 12);
   }
}
