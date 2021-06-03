import { RetirementIncomeInfo } from "../../../Entities/Models/types";
import { APIResponse } from "../../../Entities/Models/APIResponse";


export const incomeTypeIds = { socialSecurity: 6, pension: 7, ira: 8, otherRetirement: 9 };
export const incomeTypesMock = [{ "id": incomeTypeIds.socialSecurity, "name": "Social Security", "fieldsInfo": "{\"fieldsInfo\": [{\"name\": \"employerName\",\"caption\": \"Employer Name\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 0,\"Enabled\":false},{\"name\": \"monthlyBaseIncome\",\"caption\": \"Monthly Income\",\"datatype\": \"decimal\",\"displayOrder\": 1,\"Enabled\":true},{\"name\": \"description\",\"caption\": \"Description\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 2,\"Enabled\":false}\r\n]\r\n}" }, { "id": incomeTypeIds.pension, "name": "Pension", "fieldsInfo": "{\r\n\"fieldsInfo\": [{\"name\": \"employerName\",\"caption\": \"Emplyer Name\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 0,\"Enabled\":true},{\"name\": \"monthlyBaseIncome\",\"caption\": \"Monthly Income\",\"datatype\": \"decimal\",\"displayOrder\": 1,\"Enabled\":true},{\"name\": \"description\",\"caption\": \"Description\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 2,\"Enabled\":false}]}" }, { "id": incomeTypeIds.ira, "name": "Ira / 401K", "fieldsInfo": "{\r\n\"fieldsInfo\": [{\"name\": \"employerName\",\"caption\": \"Emplyer Name\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 0,\"Enabled\":false},{\"name\": \"monthlyBaseIncome\",\"caption\": \"Monthly Withdrawal\",\"datatype\": \"decimal\",\"displayOrder\": 1,\"Enabled\":true},{\"name\": \"description\",\"caption\": \"Description\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 2,\"Enabled\":false}\r\n]\r\n}" }, { "id": incomeTypeIds.otherRetirement, "name": "Other Retirement", "fieldsInfo": "{\r\n\"fieldsInfo\": [{\"name\": \"employerName\",\"caption\": \"Emplyer Name\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 0,\"Enabled\":false},{\"name\": \"monthlyBaseIncome\",\"caption\": \"Monthly Income\",\"datatype\": \"decimal\",\"displayOrder\": 1,\"Enabled\":true},{\"name\": \"description\",\"caption\": \"Description\",\"datatype\": \"string\",\"maxLength\": 150,\"displayOrder\": 2,\"Enabled\":true}]}" }];
export const incomeInfoMock = { incomeInfoId: 2061, borrowerId: 2287, incomeTypeId: 6, monthlyBaseIncome: 111288855552.06, employerName: "asad1", description: "aaaaaaa" };
export default class RetirementIncomeActions {

    static async GetRetirementIncomeTypes() {
        return new APIResponse(200, incomeTypesMock);
    }

    static async GetRetirementIncomeInfo(incomeInfoId: number, borrowerId: number) {
        return new APIResponse(200, incomeInfoMock);
    }

    static async AddOrUpdateRetirementIncomeInfo(retirementIncomeInfo: RetirementIncomeInfo) {
        return new APIResponse(200, {});
    }

}