//
//  DocumentCategoryModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/12/2021.
//

import UIKit
import SwiftyJSON

class DocumentCategoryModel: NSObject {

    var catId: String = ""
    var catName: String = ""
    var documents = [Doc]()
    
    func updateModelWithJSON(json: JSON){
        catId = json["catId"].stringValue
        catName = json["catName"].stringValue
        let docsArray = json["documents"].arrayValue
        for docsJson in docsArray{
            let model = Doc()
            model.updateModelWithJSON(json: docsJson, isForDocList: true)
            documents.append(model)
        }
    }
    
}
