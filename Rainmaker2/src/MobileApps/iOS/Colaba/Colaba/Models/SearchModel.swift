//
//  SearchModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/06/2021.
//

import Foundation
import SwiftyJSON

class SearchModel: NSObject {
    
    var activityTime: String = ""
    var cityName: String = ""
    var countryName: String = ""
    var firstName: String = ""
    var id: Int = 0
    var lastName: String = ""
    var loanNumber: String = ""
    var stateAbbreviation: String = ""
    var stateName: String = ""
    var status: String = ""
    var streetAddress: String = ""
    var tenantId: Int = 0
    var unitNumber: String = ""
    var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        activityTime = json["activityTime"].stringValue
        cityName = json["cityName"].stringValue
        countryName = json["countryName"].stringValue
        firstName = json["firstName"].stringValue
        id = json["id"].intValue
        lastName = json["lastName"].stringValue
        loanNumber = json["loanNumber"].stringValue
        stateAbbreviation = json["stateAbbreviation"].stringValue
        stateName = json["stateName"].stringValue
        status = json["status"].stringValue
        streetAddress = json["streetAddress"].stringValue
        tenantId = json["tenantId"].intValue
        unitNumber = json["unitNumber"].stringValue
        zipCode = json["zipCode"].stringValue
    }
    
}
