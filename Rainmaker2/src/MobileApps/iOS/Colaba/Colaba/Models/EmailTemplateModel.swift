//
//  EmailTemplateModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/12/2021.
//

import UIKit
import SwiftyJSON

class EmailTemplateModel: NSObject {

    var ccAddress: String = ""
    var emailBody: String = ""
    var fromAddress: String = ""
    var id: String = ""
    var sortOrder: Int = 0
    var subject: String = ""
    var templateDescription: String = ""
    var templateName: String = ""
    var tenantId: Int = 0
    var toAddress: String = ""
    
    func updateModelWithJSON(json: JSON){
        ccAddress = json["ccAddress"].stringValue
        emailBody = json["emailBody"].stringValue
        fromAddress = json["fromAddress"].stringValue
        id = json["id"].stringValue
        sortOrder = json["sortOrder"].intValue
        subject = json["subject"].stringValue
        templateDescription = json["templateDescription"].stringValue
        templateName = json["templateName"].stringValue
        tenantId = json["tenantId"].intValue
        toAddress = json["toAddress"].stringValue
    }
    
}
