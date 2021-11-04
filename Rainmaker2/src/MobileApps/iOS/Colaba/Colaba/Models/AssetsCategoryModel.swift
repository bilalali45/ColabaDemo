//
//  AssetsCategoryModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/11/2021.
//

import Foundation
import SwiftyJSON

class AssetsCategoryModel: NSObject{
    
    var assetCategoryId: Int = 0
    var displayName: String = ""
    var fieldsInfo: String = ""
    var id: Int = 0
    var name: String = ""
    var tenantAlternateName: String = ""

    func updateModelWithJSON(json: JSON){
        assetCategoryId = json["assetCategoryId"].intValue
        displayName = json["displayName"].stringValue
        fieldsInfo = json["fieldsInfo"].stringValue
        id = json["id"].intValue
        name = json["name"].stringValue
        tenantAlternateName = json["tenantAlternateName"].stringValue
    }
    
}
