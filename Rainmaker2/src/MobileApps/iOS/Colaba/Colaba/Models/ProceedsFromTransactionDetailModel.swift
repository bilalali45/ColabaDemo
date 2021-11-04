//
//  ProceedsFromTransactionDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 04/11/2021.
//

import Foundation
import SwiftyJSON

class ProceedsFromTransactionDetailModel: NSObject{
    
    var assetTypeCategoryName: String = ""
    var assetTypeId: Int = 0
    var asstTypeName: String = ""
    var borrowerId: Int = 0
    var collateralAssetName: String = ""
    var collateralAssetOtherDescription: String = ""
    var collateralAssetTypeId: Int = 0
    var descriptionField: String = ""
    var id: Int = 0
    var securedByCollateral: Bool = false
    var value: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        assetTypeCategoryName = json["assetTypeCategoryName"].stringValue
        assetTypeId = json["assetTypeId"].intValue
        asstTypeName = json["asstTypeName"].stringValue
        borrowerId = json["borrowerId"].intValue
        collateralAssetName = json["collateralAssetName"].stringValue
        collateralAssetOtherDescription = json["collateralAssetOtherDescription"].stringValue
        collateralAssetTypeId = json["collateralAssetTypeId"].intValue
        descriptionField = json["description"].stringValue
        id = json["id"].intValue
        securedByCollateral = json["securedByCollateral"].boolValue
        value = json["value"].doubleValue
    }
    
}
