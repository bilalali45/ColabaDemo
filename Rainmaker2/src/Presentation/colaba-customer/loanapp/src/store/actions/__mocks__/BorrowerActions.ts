import { APIResponse } from "../../../Entities/Models/APIResponse";

const AllConsentMock = {
    "consentHash": "c1207ee82c65da9ff5d875e947ee8319b5f394c639fc684ad10dc6b32037422e",
    "consentList": [
        {
            "id": 10,
            "name": "Personal Information",
            "description": "<p>4Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris sit amet semper nunc. Vestibulum laoreet ex tortor, ac ullamcorper ipsum sodales at. Suspendisse placerat laoreet lorem, ac scelerisque nibh rhoncus in. Maecenas dictum venenatis dolor eget lobortis. Nunc pretium, augue at sodales imperdiet, urna nulla gravida tellus, faucibus lacinia nibh nulla eu eros. Sed vel feugiat arcu, vel congue magna. Pellentesque pharetra eros tincidunt orci porta auctor.</p>\r\n"
        },
        {
            "id": 11,
            "name": "Legal Information",
            "description": "<p>5Curabitur eget lacus ac sem consectetur bibendum non sit amet sapien. Ut blandit aliquam volutpat. Etiam felis nunc, mollis a aliquet dignissim, pharetra sed massa. Nulla gravida in dui et ornare. Pellentesque finibus erat ipsum, vitae tincidunt quam dictum at. Vivamus fermentum at diam nec varius. Donec porttitor interdum varius. Proin quis euismod nibh. Sed fermentum velit eget quam vehicula sagittis.</p>\r\n"
        },
        {
            "id": 12,
            "name": "Assets Information",
            "description": "<p>6Nullam ornare auctor convallis. Curabitur at ipsum sapien. Fusce ut lectus tempus lectus porta auctor in vitae est. Cras posuere et dui sit amet semper. Quisque interdum neque sed ligula lacinia faucibus. Nullam sed tellus sem. Proin elementum nisi ac sem blandit, et lacinia metus ornare. Mauris dolor nibh, mollis sed consectetur eu, rhoncus quis ex.</p>\r\n"
        }
    ]
}

const AcceptedConsentMock = {
    "isAccepted": false,
    "acceptedConsentList": null
}

const borrowerFirstReviewData = { "borrowerReviews": [{ "borrowerAddress": { "countryId": 1, "countryName": "United States", "stateId": 35, "stateName": "New York", "countyId": null, "countyName": null, "cityId": null, "cityName": "Flushing", "streetAddress": " DFG Rd", "zipCode": "11371", "unitNo": "" }, "borrowerId": 5475, "firstName": "khalid", "middleName": "", "lastName": "siddiqui", "emailAddress": "danish@gmail.com", "homePhone": "4353453453", "workPhone": "4353453453", "workPhoneExt": "", "isVaEligible": false, "maritalStatusId": 9, "cellPhone": "", "ownTypeId": 1, "ownTypeName": null }] }
export default class BorrowerActions {
    static AllBorrower = [
        { "id": 7002, "firstName": "first", "lastName": "first", "ownTypeId": 1 },
        { "id": 7003, "firstName": "second", "lastName": "second", "ownTypeId": 2 }
    ]

    static BorrowerInfo = {
        cellPhone: "2222222222",
        email: "sukagu@mailinator.com",
        firstName: "Perry",
        homePhone: "2222222222",
        id: 14931,
        lastName: "Goodman",
        middleName: "Tana Stein",
        ownTypeId: 1,
        suffix: "Dr",
        workExt: "",
        workPhone: "2222222222"
    }

    static async getAllBorrower(loanApplicationId: number) {
        return new APIResponse(200, this.AllBorrower);
    }

    static async getBorrowersForFirstReview(loanApplicationId: number) {

        return new APIResponse(200, borrowerFirstReviewData)
    }

    static async getBorrowerInfo(loanApplicationId: number, borrowerId: number) {
        return new APIResponse(200, this.BorrowerInfo);
    }

    static async getAllConsentTypes() {
        return new APIResponse(200, AllConsentMock)
    }

    static async getBorrowerAcceptedConsents(loanApplicationId: number, borrowerId: number) {
        return new APIResponse(200, AcceptedConsentMock)
    }

    static async addOrUpdateBorrowerConsents(loanApplicationId: number, borrowerId: number, isAccepted: boolean, State: string, consentHash: string) {
        return new APIResponse(200, true)
    }

    static async populatePrimaryBorrower() {
        return new APIResponse(200, null);
    }

    //      static async addOrUpdateBorrowerInfo(borrowerBasicInfo: BorrowerBasicInfo) {
    //         let url = Endpoints.Borrower.POST.addOrUpdateBorrowerInfo()
    //         try {
    //           let response = await Http.post(url, borrowerBasicInfo)
    //           return response.data;
    //         } catch (error) {
    //           console.log(error);
    //           return error.response.data;
    //         }
    //      }

    //      static async getReviewBorrowerInfoSection(loanApplicationId: Number) {
    //       let url = Endpoints.Borrower.GET.getReviewBorrowerInfoSection(loanApplicationId)
    //       try {
    //           let response: any = await Http.get(url, {}, false);
    //           const dataPayload :GetReviewBorrowerInfoSectionPayload = response.data;
    //           return dataPayload
    //       } catch (error) {
    //           console.log(error);
    //           return undefined;
    //       }
    //   }

    //   static async deleteSecondaryBorrower(loanApplicationId: number, borrowerId :number) {
    //       let url = Endpoints.Borrower.DELETE.deleteSecondaryBorrower();
    //       try {
    //           let response: any = await Http.delete(url,{"loanApplicationId":loanApplicationId,"borrowerId": borrowerId},{},false);
    //           const dataPayload :GetReviewBorrowerInfoSectionPayload = response.data;
    //           return dataPayload
    //       } catch (error) {
    //           console.log(error);
    //           return undefined;
    //       }
    //   }







}
