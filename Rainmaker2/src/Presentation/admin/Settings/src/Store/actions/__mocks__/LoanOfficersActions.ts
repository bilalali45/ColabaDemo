import LoanOfficer from "../../../Entities/Models/LoanOfficer";

const mockOriginalData = [
    {
        "userId": 8,
        "userName": "minaz.karim",
        "byteUserName": "Minaz Karim",
        "fullName": "Minaz Karim",
        "photo" :"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"
    },
    {
        "userId": 21,
        "userName": "Tanner.Holland",
        "byteUserName": "Tanner Holland 12",
        "fullName": "Tanner Holland",
        "photo" :"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"
    }
]

export class  LoanOfficersActions {
    static async fetchLoanOfficersSettings() {
        let mappedData = mockOriginalData.map((d:any) => {
             return new LoanOfficer(d.userId, d.userName, d.byteUserName, d.fullName,d.photo);
       });
           return Promise.resolve(mappedData);
    }

    static async updateLoanOfficersSettings(loanOfficers: LoanOfficer[]){
        return Promise.resolve(true);
     }
}
