//
//  BorrowerAssetsModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 15/10/2021.
//

import Foundation
import SwiftyJSON

class BorrowerAssetsModel: NSObject{
    
    var assetsTotal: Double = 0.0
    var borrowerAssets = [BorrowerAsset]()
    var borrowerId: Int = 0
    var borrowerName: String = ""
    var ownTypeDisplayName: String = ""
    var ownTypeId: Int = 0
    var ownTypeName: String = ""
    
    func updateModelWithJSON(json: JSON){
        
        assetsTotal = json["assetsTotal"].doubleValue
        let borrowerAssetsArray = json["borrowerAssets"].arrayValue
        for borrowerAssetsJson in borrowerAssetsArray{
            let model = BorrowerAsset()
            model.updateModelWithJSON(json: borrowerAssetsJson)
            borrowerAssets.append(model)
        }
        borrowerId = json["borrowerId"].intValue
        borrowerName = json["borrowerName"].stringValue
        ownTypeDisplayName = json["ownTypeDisplayName"].stringValue
        ownTypeId = json["ownTypeId"].intValue
        ownTypeName = json["ownTypeName"].stringValue
        
    }
    
}

class BorrowerAsset: NSObject{

    var assets = [Asset]()
    var assetsCategory: String = ""
    var assetsTotal: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        
        let assetsArray = json["assets"].arrayValue
        for assetsJson in assetsArray{
            let model = Asset()
            model.updateModelWithJSON(json: assetsJson)
            assets.append(model)
        }
        assetsCategory = json["assetsCategory"].stringValue
        assetsTotal = json["assetsTotal"].doubleValue
        
    }
}

class Asset: NSObject{

    var assetCategoryId: Int = 0
    var assetCategoryName: String = ""
    var assetId: Int = 0
    var assetName: String = ""
    var assetTypeID: Int = 0
    var assetTypeName: String = ""
    var assetValue: Double = 0.0
    var isDisabledByTenant: Bool = false
    var isEarnestMoney: Bool = false

    func updateModelWithJSON(json: JSON){
        
        assetCategoryId = json["assetCategoryId"].intValue
        assetCategoryName = json["assetCategoryName"].stringValue
        assetId = json["assetId"].intValue
        assetName = json["assetName"].stringValue
        assetTypeID = json["assetTypeID"].intValue
        assetTypeName = json["assetTypeName"].stringValue
        assetValue = json["assetValue"].doubleValue
        isDisabledByTenant = json["isDisabledByTenant"].boolValue
        isEarnestMoney = json["isEarnestMoney"].boolValue

    }
    
}
