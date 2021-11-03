//
//  OtherAssetsDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/11/2021.
//

import Foundation
import SwiftyJSON

class OtherAssetsDetailModel: NSObject{
    
    var accountNumber: String = ""
    var assetDescription: String = ""
    var assetId: Int = 0
    var assetTypeId: Int = 0
    var assetTypeName: String = ""
    var assetValue: Double = 0.0
    var institutionName: String = ""
    
    func updateModelWithJSON(json: JSON){
        accountNumber = json["accountNumber"].stringValue
        assetDescription = json["assetDescription"].stringValue
        assetId = json["assetId"].intValue
        assetTypeId = json["assetTypeId"].intValue
        assetTypeName = json["assetTypeName"].stringValue
        assetValue = json["assetValue"].doubleValue
        institutionName = json["institutionName"].stringValue
    }
    
}
