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
    
    func updateModelWithJSON(json: JSON){
        optionId = json["id"].intValue
        optionName = json["name"].stringValue
    }
    
}
