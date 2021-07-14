//
//  DocumentModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/07/2021.
//

import Foundation
import SwiftyJSON

class LoanDocumentModel: NSObject{
    
    var createdOn: String = ""
    var docId: String = ""
    var docName: String = ""
    var files: [DocumentFileModel] = []
    var id: String = ""
    var requestId: String = ""
    var status: String = ""
    var typeId: String = ""
    var userName: String = ""
    
    func updateModelWithJSON(json: JSON){
        createdOn = json["createdOn"].stringValue
        docId = json["docId"].stringValue
        docName = json["docName"].stringValue
        files.removeAll()
        let filesArray = json["files"].arrayValue
        for file in filesArray{
            let model = DocumentFileModel()
            model.updateModelWithJSON(json: file)
            files.append(model)
        }
        id = json["id"].stringValue
        requestId = json["requestId"].stringValue
        status = json["status"].stringValue
        typeId = json["typeId"].stringValue
        userName = json["userName"].stringValue

    }
    
}

class DocumentFileModel: NSObject {
    
    var byteProStatus: String = ""
    var clientName: String = ""
    var fileModifiedOn: String = ""
    var fileUploadedOn: String = ""
    var fileUploadedTimeStamp: Int = 0
    var fileId: String = ""
    var isRead: Bool = false
    var mcuName: String = ""
    var status: String = ""
    var userId: Int = 0
    var userName: String = ""
    
    func updateModelWithJSON(json: JSON){
        byteProStatus = json["byteProStatus"].stringValue
        clientName = json["clientName"].stringValue
        fileModifiedOn = json["fileModifiedOn"].stringValue
        fileUploadedOn = json["fileUploadedOn"].stringValue
        fileId = json["id"].stringValue
        isRead = json["isRead"].boolValue
        mcuName = json["mcuName"].stringValue
        status = json["status"].stringValue
        userId = json["userId"].intValue
        userName = json["userName"].stringValue
        fileUploadedTimeStamp = Utility.getDocumentFilesTimeStamp(json["fileUploadedOn"].stringValue)
    }
    
}
