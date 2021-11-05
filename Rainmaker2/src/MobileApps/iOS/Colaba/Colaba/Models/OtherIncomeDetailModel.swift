//
//  OtherIncomeDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/11/2021.
//

import Foundation
import SwiftyJSON

class OtherIncomeDetailModel: NSObject{
    
    var annualBaseIncome: Double = 0.0
    var descriptionField: String = ""
    var fieldInfo: String = ""
    var incomeGroupId: Int = 0
    var incomeGroupName: String = ""
    var incomeInfoId: Int = 0
    var incomeTypeId: Int = 0
    var incomeTypeName: String = ""
    var monthlyBaseIncome: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        annualBaseIncome = json["annualBaseIncome"].doubleValue
        descriptionField = json["description"].stringValue
        fieldInfo = json["fieldInfo"].stringValue
        incomeGroupId = json["incomeGroupId"].intValue
        incomeGroupName = json["incomeGroupName"].stringValue
        incomeInfoId = json["incomeInfoId"].intValue
        incomeTypeId = json["incomeTypeId"].intValue
        incomeTypeName = json["incomeTypeName"].stringValue
        monthlyBaseIncome = json["monthlyBaseIncome"].doubleValue
    }
    
}
