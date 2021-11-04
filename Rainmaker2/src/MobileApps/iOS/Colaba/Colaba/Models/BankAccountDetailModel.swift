//
//  BankAccountDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/11/2021.
//

import Foundation
import SwiftyJSON

class BankAccountDetailModel: NSObject{
    
    var accountNumber: String = ""
    var assetTypeId: Int = 0
    var balance: Double = 0.0
    var borrowerId: Int = 0
    var id: Int = 0
    var institutionName: String = ""
    var loanApplicationId: Int = 0

    func updateModelWithJSON(json: JSON){
        accountNumber = json["accountNumber"].stringValue
        assetTypeId = json["assetTypeId"].intValue
        balance = json["balance"].doubleValue
        borrowerId = json["borrowerId"].intValue
        id = json["id"].intValue
        institutionName = json["institutionName"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
    }
    
}
