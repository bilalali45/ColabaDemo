//
//  DocumentTemplateModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/12/2021.
//

import UIKit
import SwiftyJSON

class DocumentTemplateModel: NSObject {

    var docs = [Doc]()
    var id: String = ""
    var name: String = ""
    var type: String = ""
    var isSelected: Bool = false
    
    func updateModelWithJSON(json: JSON){
        let docsArray = json["docs"].arrayValue
        for docsJson in docsArray{
            let model = Doc()
            model.updateModelWithJSON(json: docsJson)
            docs.append(model)
        }
        id = json["id"].stringValue
        name = json["name"].stringValue
        type = json["type"].stringValue
    }
    
}

class Doc : NSObject{

    var docTypeId: String = ""
    var docType: String = ""
    var docMessage: String = ""
    var isCommonlyUsed: Bool = false
    var isSelected: Bool = false
    
    func updateModelWithJSON(json: JSON){
        docTypeId = json["docTypeId"].stringValue
        docType = json["docType"].stringValue
        docMessage = json["docMessage"].stringValue
        isCommonlyUsed = json["isCommonlyUsed"].boolValue
    }

}
