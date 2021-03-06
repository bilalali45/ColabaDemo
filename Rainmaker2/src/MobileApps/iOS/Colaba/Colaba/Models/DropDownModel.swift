//
//  DropDownModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/10/2021.
//

import Foundation
import SwiftyJSON

class DropDownModel: NSObject{
    
    var optionId: Int = 0
    var optionName: String = ""
    var isSelected: Bool = false
    
    func updateModelWithJSON(json: JSON, isForBorrowerInfo:Bool=false){
        optionId = json["id"].intValue
        optionName = isForBorrowerInfo ? json["description"].stringValue : json["name"].stringValue
    }
    
}
