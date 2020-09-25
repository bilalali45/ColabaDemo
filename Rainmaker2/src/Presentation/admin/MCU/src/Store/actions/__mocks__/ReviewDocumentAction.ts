

const mockFiles = [
    {
        "id": "5f58c63b603a2d64b4a27d79",
        "docId": "5f63481f37de0c1490a36b65",
        "docName": "Government Issued Identification",
        "typeId": "5f47439dcca0a5d1c971083c",
        "requestId": "5f63481f37de0c1490a36b58",
        "files": [
            {
                "fileId": "5f644f5f37de0c1490a370c1",
                "clientName": "abcxyz.jpeg",
                "mcuName": "",
                "fileUploadedOn": "2020-09-18T06:10:39.796Z",
                "isRead": true
            },
            {
                "fileId": "5f644f6837de0c1490a370c4",
                "clientName": "abcxyz-copy-1.jpeg",
                "mcuName": "",
                "fileUploadedOn": "2020-09-18T06:10:48.168Z",
                "isRead": true
            }
        ],
        "userName": "Taruf Ali"
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
        return Promise.resolve(mockFiles);  
    }
}