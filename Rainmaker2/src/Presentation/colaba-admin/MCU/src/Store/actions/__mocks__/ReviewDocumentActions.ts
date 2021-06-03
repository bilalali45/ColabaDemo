

const mockFiles = [
    {
        "id": "5f58c63b603a2d64b4a27d79",
        "docId": "5f69eaaba16049171422d549",
        "docName": "Covid-19",
        "typeId": "5f47439dcca0a5d1c971083a",
        "requestId": "5f63481f37de0c1490a36b59",
        "files": [
            {
                "fileId": "5f69eac8a16049171422d54d",
                "clientName": "download.jpeg",
                "mcuName": "",
                "fileUploadedOn": "2020-09-22T12:15:04.914Z",
                "isRead": true
            },
            {
                "fileId": "5f69eacaa16049171422d550",
                "clientName": "sampleabc.jpeg",
                "mcuName": "",
                "fileUploadedOn": "2020-09-22T12:15:06.864Z",
                "isRead": true
            },
            {
                "fileId": "5f69eacba16049171422d552",
                "clientName": "download-copy-1.jpeg",
                "mcuName": "",
                "fileUploadedOn": "2020-09-22T12:15:07.747Z",
                "isRead": true
            }
        ],
        "userName": "Taruf Ali"
    }
]

const activityLogMock = [
    {
        "id": "5f6c7f1f6fb05c3c90655963",
        "userId": 1,
        "userName": "System Administrator",
        "typeId": "5f473aa0cca0a5d1c96798dd",
        "docId": "5f63481f37de0c1490a36b5d",
        "activity": "Re-requested By",
        "dateTime": "2020-09-24T11:12:31.774Z",
        "loanId": "5f58c63b603a2d64b4a27d79",
        "message": "Hi Taruf Ali, please submit the HOA or Condo Association Fee Statements again.",
        "log": [
            {
                "_id": "5f6c7f1f6fb05c3c90655964",
                "dateTime": "2020-09-24T11:12:31.775Z",
                "activity": "Status Changed : Borrower to do"
            }
        ]
    },
    {
        "id": "5f63481f37de0c1490a36b5e",
        "userId": 1,
        "userName": "System Administrator",
        "typeId": "5f473aa0cca0a5d1c96798dd",
        "docId": "5f63481f37de0c1490a36b5d",
        "activity": "Requested By",
        "dateTime": "2020-09-17T11:27:27.097Z",
        "loanId": "5f58c63b603a2d64b4a27d79",
        "message": "Condo HOA fee statement depicting monthly/quarterly/annual dues and payments",
        "log": [
            {
                "_id": "5f63481f37de0c1490a36b5f",
                "dateTime": "2020-09-17T11:27:27.098Z",
                "activity": "Status Changed : Borrower to do"
            },
            {
                "_id": "5f636c1d37de0c1490a36e26",
                "dateTime": "2020-09-17T14:01:01.083Z",
                "activity": "File Submission : 7mb.pdf"
            },
            {
                "_id": "5f636c1d37de0c1490a36e27",
                "dateTime": "2020-09-17T14:01:01.087Z",
                "activity": "Status Changed : Started"
            },
            {
                "_id": "5f636c4437de0c1490a36e28",
                "dateTime": "2020-09-17T14:01:40.509Z",
                "activity": "Status Changed : Pending review"
            },
            {
                "_id": "5f636c4437de0c1490a36e2a",
                "dateTime": "2020-09-17T14:01:40.738Z",
                "activity": "File Submission : 7mb-copy-1.pdf"
            },
            {
                "_id": "5f636c4437de0c1490a36e2b",
                "dateTime": "2020-09-17T14:01:40.741Z",
                "activity": "Status Changed : Started"
            },
            {
                "_id": "5f63728837de0c1490a36e66",
                "dateTime": "2020-09-17T14:28:24.794Z",
                "activity": "Status Changed : Pending review"
            },
            {
                "_id": "5f6c71886fb05c3c90655914",
                "dateTime": "2020-09-24T10:14:32.305Z",
                "activity": "Rejected By : System Administrator"
            }
        ]
    }
]

export class ReviewDocumentActions {
    static async getDocumentForView(id: string, requestId: string, docId: string,fileId: string){  
          var blob = new Blob(["\x01\x02\x03\x04"]);
          var req;
          var arrayPromise = new Promise(function(resolve) {
              var reader = new FileReader();
          
              reader.onloadend = function() {
                  resolve(reader.result);
              };
          
              reader.readAsArrayBuffer(blob);
          });
          var array = await arrayPromise;
          req = { headers: { 'content-type': 'image/jpeg' }, data:  array}
        return req;
       }

    static async requestDocumentFiles(id: string, requestId: string, docId: string){     
        return mockFiles;  
    }

    static async getActivityLogs(id: string, docId: string, requestId: string){
        return activityLogMock;
    }

    static async acceptDocument(id: string, requestId: string, docId: string){
       
   }

   static async rejectDocument(loanApplicationId: number, id: string, requestId: string, docId: string){
   }
}