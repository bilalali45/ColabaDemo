//
//  SelfIndependenDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/11/2021.
//

import Foundation
import SwiftyJSON

class SelfIndependenDetailModel: NSObject{
    
    var address = AddressModel()
    var annualIncome: Double = 0.0
    var borrowerId: Int = 0
    var businessName: String = ""
    var businessPhone: String = ""
    var id: Int = 0
    var jobTitle: String = ""
    var loanApplicationId: Int = 0
    var startDate: String = ""
    
    func updateModelWithJSON(json: JSON){
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        self.address = model
        annualIncome = json["annualIncome"].doubleValue
        borrowerId = json["borrowerId"].intValue
        businessName = json["businessName"].stringValue
        businessPhone = json["businessPhone"].stringValue
        id = json["id"].intValue
        jobTitle = json["jobTitle"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        startDate = json["startDate"].stringValue
    }
}
