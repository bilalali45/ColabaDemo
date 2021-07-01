//
//  NotificationModel.swift
//  Colaba
//
//  Created by Murtaza on 01/07/2021.
//

import Foundation
import SwiftyJSON

class NotificationModel: NSObject{
    
    var id: Int = 0
    var status: String = ""
    var address: String = ""
    var city: String = ""
    var dateTime: String = ""
    var loanApplicationId: String = ""
    var name: String = ""
    var notificationType: String = ""
    var state: String = ""
    var unitNumber: String = ""
    var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        status = json["status"].stringValue
        address = json["payload"]["data"]["address"].stringValue
        city = json["payload"]["data"]["city"].stringValue
        dateTime = json["payload"]["data"]["dateTime"].stringValue
        loanApplicationId = json["payload"]["data"]["loanApplicationId"].stringValue
        name = json["payload"]["data"]["name"].stringValue
        notificationType = json["payload"]["data"]["notificationType"].stringValue
        state = json["payload"]["data"]["state"].stringValue
        unitNumber = json["payload"]["data"]["unitNumber"].stringValue
        zipCode = json["payload"]["data"]["zipCode"].stringValue
    }
    
}
