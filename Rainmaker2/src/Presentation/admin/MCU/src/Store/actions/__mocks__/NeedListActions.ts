import {LoanApplication} from '../../../Entities/Models/LoanApplication';



const LoanInfo = {
  "borrowers": ["Taruf Ali","Co Borr Last Name"],
  "cityName": "Houston",
  "countryName": "",
  "countyName": " Harris County",
  "expectedClosingDate": "2020-08-29T00:00:00",
  "expirationDate": "",
  "loanAmount": 45000,
  "loanNumber": "50020000155",
  "loanProgram": "",
  "loanPurpose": "Purchase a home",
  "lockDate": "2020-08-18T05:44:40.357Z",
  "lockStatus": "Float",
  "popertyValue": 55000,
  "propertyType": "Single Family Detached",
  "rate": "",
  "stateName": "TX",
  "status": "Application Submitted",
  "streetAddress": "New27AUG",
  "unitNumber": "2708",
  "zipCode": "77023",
}
export class NeedListActions {
  static async getLoanApplicationDetail(loanApplicationId: string) {
    try {
      return LoanInfo;
    } catch (error) {
      console.log(error);
    }
  }

  static async getNeedList(loanApplicationId: string, status: boolean) {
    try {
      return [];
    } catch (error) {
      console.log(error);
    }
  }

  static async deleteNeedListDocument(
    id: string,
    requestId: string,
    docId: string
  ) {
    try {
      return '200';
    } catch (error) {
      console.log(error);
    }
  }

  static async checkIsByteProAuto() {
    try {
      return new Object();
    } catch (error) {
      console.log(error);
    }
  }

  static async fileSyncToLos(
    LoanApplicationId: number,
    DocumentLoanApplicationId: string,
    RequestId: string,
    DocumentId: string,
    FileId: string
  ) {
    try {
      return '200';
    } catch (error) {
      console.log(error);
    }
  }
}
