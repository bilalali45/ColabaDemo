//
//  RetirementAccountDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/11/2021.
//

import Foundation
import SwiftyJSON

class RetirementAccountDetailModel: NSObject{
    
    var accountNumber: String = ""
    var borrowerId: Int = 0
    var id: Int = 0
    var institutionName: String = ""
    var loanApplicationId: Int = 0
    var value: Double = 0.0

    func updateModelWithJSON(json: JSON){
        accountNumber = json["accountNumber"].stringValue
        borrowerId = json["borrowerId"].intValue
        id = json["id"].intValue
        institutionName = json["institutionName"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        value = json["value"].doubleValue
    }
    
}
