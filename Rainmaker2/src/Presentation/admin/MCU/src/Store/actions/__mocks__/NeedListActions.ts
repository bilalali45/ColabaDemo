import { LoanApplication } from '../../../Entities/Models/LoanApplication';



const LoanInfo = {
  "borrowers": ["Taruf Ali", "Co Borr Last Name"],
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

const NeedList = [
  {
    createdOn: "2020-09-22T12:14:35.165Z",
    docId: "5f69eaaba16049171422d549",
    docName: "Covid-19",
    files: [
      {
        byteProStatus: "Synchronized",
        clientName: "download.jpeg",
        fileUploadedOn: "2020-09-22T12:15:04.914Z",
        id: "5f69eac8a16049171422d54d",
        isRead: true,
        mcuName: "",
        status: null
      }, {
        byteProStatus: "Synchronized",
        clientName: "sampleabc.jpeg",
        fileUploadedOn: "2020-09-22T12:15:06.864Z",
        id: "5f69eacaa16049171422d550",
        isRead: true,
        mcuName: "",
        status: null,
      },
      {
        byteProStatus: "Synchronized",
        clientName: "download-copy-1.jpeg",
        fileUploadedOn: "2020-09-22T12:15:07.747Z",
        id: "5f69eacba16049171422d552",
        isRead: true,
        mcuName: "",
        status: null,
      }
    ],
    id: "5f58c63b603a2d64b4a27d79",
    requestId: "5f69eaaba16049171422d548",
    status: "Pending review",
    typeId: null,
    userName: "Taruf Ali"
  }
  , {
    createdOn: "2020-09-17T11:27:27.092Z",
    docId: "5f63481f37de0c1490a36b59",
    docName: "Mortgage Statement",
    files: [
      {
        byteProStatus: "Not synchronized",
        clientName: "sampleabc.jpeg",
        fileUploadedOn: "2020-09-17T11:28:01.008Z",
        id: "5f63484137de0c1490a36b69",
        isRead: true,
        mcuName: "",
        status: null,
      },
      {
        byteProStatus: "Not synchronized",
        clientName: "sampleabc-copy-1.jpeg",
        fileUploadedOn: "2020-09-17T11:28:01.929Z",
        id: "5f63484137de0c1490a36b6c",
        isRead: false,
        mcuName: "",
        status: null,
      }
    ],
    id: "5f58c63b603a2d64b4a27d79",
    requestId: "5f63481f37de0c1490a36b58",
    status: "Completed",
    typeId: "5f473faecca0a5d1c96cba16",
    userName: "Taruf Ali",
  },
  {
    createdOn: "2020-09-17T11:27:27.092Z",
    docId: "5f63481f37de0c1490a36b5d",
    docName: "HOA or Condo Association Fee Statements",
    files: [
      {
        byteProStatus: "Synchronized",
        clientName: "7mb.pdf",
        fileUploadedOn: "2020-09-17T14:01:01.079Z",
        id: "5f636c1d37de0c1490a36e25",
        isRead: true,
        mcuName: "",
        status: null,
      }, {
        byteProStatus: "Synchronized",
        clientName: "7mb-copy-1.pdf",
        fileUploadedOn: "2020-09-17T14:01:40.733Z",
        id: "5f636c4437de0c1490a36e29",
        isRead: false,
        mcuName: "",
        status: null,
      }],
    id: "5f58c63b603a2d64b4a27d79",
    requestId: "5f63481f37de0c1490a36b58",
    status: "Pending review",
    typeId: "5f473aa0cca0a5d1c96798dd",
    userName: "Taruf Ali"
  },
   {
    createdOn: "2020-09-17T11:27:27.092Z",
    docId: "5f63481f37de0c1490a36b61",
    docName: "Retirement Account Statements",
    files: [
      {
        byteProStatus: "Synchronized",
        clientName: "7mb.pdf",
        fileUploadedOn: "2020-09-17T14:16:58.949Z",
        id: "5f636fda37de0c1490a36e2d",
        isRead: true,
        mcuName: "",
        status: null,
      }, {
        byteProStatus: "Synchronized",
        clientName: "milestore text.png",
        fileUploadedOn: "2020-09-17T14:29:34.313Z",
        id: "5f6372ce37de0c1490a36e6c",
        isRead: false,
        mcuName: "",
        status: null,
      }
    ], 
        id: "5f58c63b603a2d64b4a27d79",
        requestId: "5f63481f37de0c1490a36b58",
        status: "Completed",
        typeId: "5eec89a26ecaea4247963d25",
        userName: "Taruf Ali"
      
  },
   {
    createdOn: "2020-09-17T11:27:27.092Z",
    docId: "5f63481f37de0c1490a36b65",
    docName: "Government Issued Identification",
    files: [
      {
        byteProStatus: "Not synchronized",
        clientName: "abcxyz.jpeg",
        fileUploadedOn: "2020-09-18T06:10:39.796Z",
        id: "5f644f5f37de0c1490a370c1",
        isRead: true,
        mcuName: "",
        status: null
      }, {
        byteProStatus: "Not synchronized",
        clientName: "abcxyz-copy-1.jpeg",
        fileUploadedOn: "2020-09-18T06:10:48.168Z",
        id: "5f644f6837de0c1490a370c4",
        isRead: false,
        mcuName: "",
        status: null
      }
    ],
    id: "5f58c63b603a2d64b4a27d79",
    requestId: "5f63481f37de0c1490a36b58",
    status: "Started",
    typeId: "5f47439dcca0a5d1c971083c",
    userName: "Taruf Ali"
  }
]


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
      if (status) {
        return Promise.resolve(NeedList.filter(needList=> needList.status !=="Completed"));
      }
      else {
        return Promise.resolve(NeedList);
      }

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
