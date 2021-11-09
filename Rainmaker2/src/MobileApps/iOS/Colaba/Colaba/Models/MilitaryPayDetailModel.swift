//
//  MilitaryPayDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/11/2021.
//

import Foundation
import SwiftyJSON

class MilitaryPayDetailModel: NSObject{
    
    var address = AddressModel()
    var borrowerId: Int = 0
    var employerName: String = ""
    var id: Int = 0
    var jobTitle: String = ""
    var loanApplicationId: Int = 0
    var militaryEntitlements: Double = 0.0
    var monthlyBaseSalary: Double = 0.0
    var startDate: String = ""
    var yearsInProfession: Int = 0
    
    func updateModelWithJSON(json: JSON){
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        self.address = model
        borrowerId = json["borrowerId"].intValue
        employerName = json["employerName"].stringValue
        id = json["id"].intValue
        jobTitle = json["jobTitle"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        militaryEntitlements = json["militaryEntitlements"].doubleValue
        monthlyBaseSalary = json["monthlyBaseSalary"].doubleValue
        startDate = json["startDate"].stringValue
        yearsInProfession = json["yearsInProfession"].intValue
    }
    
}
