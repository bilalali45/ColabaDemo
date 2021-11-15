//
//  EthnicityModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/10/2021.
//

import Foundation
import SwiftyJSON

class EthnicityModel: NSObject{
    
    var id: Int = 0
    var name: String = ""
    var isSelected: Bool = false
    var ethnicityDetails = [EthnicityDetailModel]()
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
        let ethnicityDetailsArray = json["ethnicityDetails"].arrayValue
        for ethnicityDetailsJson in ethnicityDetailsArray{
            let model = EthnicityDetailModel()
            model.updateModelWithJSON(json: ethnicityDetailsJson)
            ethnicityDetails.append(model)
        }
    }
    
}

class EthnicityDetailModel: NSObject{
    
    var id: Int = 0
    var isOther: Bool = false
    var name: String = ""
    var otherPlaceHolder: String = ""
    var isSelected: Bool = false
    var otherName: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        isOther = json["isOther"].boolValue
        name = json["name"].stringValue
        otherPlaceHolder = json["otherPlaceHolder"].stringValue
    }

}

