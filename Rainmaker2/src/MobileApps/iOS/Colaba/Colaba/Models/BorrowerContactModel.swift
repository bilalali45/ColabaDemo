//
//  BorrowerContactModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/12/2021.
//

import UIKit
import SwiftyJSON

class BorrowerContactModel: NSObject {

    var contactId: Int = 0
    var emailAddress: String = ""
    var firstName: String = ""
    var lastName: String = ""
    var midleName: String = ""
    var mobileNumber: String = ""
    
    func updateModelWithJSON(json: JSON){
        contactId = json["contactId"].intValue
        emailAddress = json["emailAddress"].stringValue
        firstName = json["firstName"].stringValue
        lastName = json["lastName"].stringValue
        midleName = json["midleName"].stringValue
        mobileNumber = json["mobileNumber"].stringValue
    }
    
}
