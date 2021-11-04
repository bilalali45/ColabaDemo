//
//  GiftDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/11/2021.
//

import Foundation
import SwiftyJSON

class GiftDetailModel: NSObject{
    
    var assetTypeId: Int = 0
    var borrowerId: Int = 0
    var descriptionField: String = ""
    var giftSourceId: Int = 0
    var id: Int = 0
    var isDeposited: Bool = false
    var loanApplicationId: Int = 0
    var value: Double = 0.0
    var valueDate: String = ""

    func updateModelWithJSON(json: JSON){
        assetTypeId = json["assetTypeId"].intValue
        borrowerId = json["borrowerId"].intValue
        descriptionField = json["description"].stringValue
        giftSourceId = json["giftSourceId"].intValue
        id = json["id"].intValue
        isDeposited = json["isDeposited"].boolValue
        loanApplicationId = json["loanApplicationId"].intValue
        value = json["value"].doubleValue
        valueDate = json["valueDate"].stringValue
    }
}
