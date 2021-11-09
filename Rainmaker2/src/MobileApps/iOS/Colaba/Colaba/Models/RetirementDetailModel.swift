//
//  RetirementDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/11/2021.
//

import Foundation
import SwiftyJSON

class RetirementDetailModel: NSObject{
    
    var borrowerId: Int = 0
    var descriptionField: String = ""
    var employerName: String = ""
    var incomeInfoId: Int = 0
    var incomeTypeId: Int = 0
    var monthlyBaseIncome: Double = 0.0

    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        descriptionField = json["description"].stringValue
        employerName = json["employerName"].stringValue
        incomeInfoId = json["incomeInfoId"].intValue
        incomeTypeId = json["incomeTypeId"].intValue
        monthlyBaseIncome = json["monthlyBaseIncome"].doubleValue
    }
    
}
