//
//  ApplicationStatusModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/12/2021.
//

import UIKit
import SwiftyJSON

class ApplicationStatusModel: NSObject {

    var createdDate: String = ""
    var descriptionField: String = ""
    var id: Int = 0
    var isCurrent: Bool = false
    var milestoneType: Int = 0
    var name: String = ""

    func updateModelWithJSON(json: JSON){
        createdDate = json["createdDate"].stringValue
        descriptionField = json["description"].stringValue
        id = json["id"].intValue
        isCurrent = json["isCurrent"].boolValue
        milestoneType = json["milestoneType"].intValue
        name = json["name"].stringValue
    }
    
}
