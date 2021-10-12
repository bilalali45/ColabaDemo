//
//  CountiesModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/10/2021.
//

import Foundation
import SwiftyJSON

class CountiesModel: NSObject{
    
    var descriptionField: String = ""
    var id: Int = 0
    var name: String = ""
    var stateId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        descriptionField = json["description"].stringValue
        id = json["id"].intValue
        name = json["name"].stringValue
        stateId = json["stateId"].intValue

    }
}
