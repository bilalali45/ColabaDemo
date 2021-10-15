//
//  BorrowerIncomeModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/10/2021.
//

import Foundation
import SwiftyJSON

class BorrowerIncomeModel: NSObject{
    
    var borrowerId: Int = 0
    var borrowerIncomes = [BorrowerIncome]()
    var borrowerName: String = ""
    var monthlyIncome: Double = 0.0
    var ownTypeDisplayName: String = ""
    var ownTypeId: Int = 0
    var ownTypeName: String = ""
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        let borrowerIncomesArray = json["borrowerIncomes"].arrayValue
        for borrowerIncomesJson in borrowerIncomesArray{
            let model = BorrowerIncome()
            model.updateModelWithJSON(json: borrowerIncomesJson)
            borrowerIncomes.append(model)
        }
        borrowerName = json["borrowerName"].stringValue
        monthlyIncome = json["monthlyIncome"].doubleValue
        ownTypeDisplayName = json["ownTypeDisplayName"].stringValue
        ownTypeId = json["ownTypeId"].intValue
        ownTypeName = json["ownTypeName"].stringValue
    }
    
}

class BorrowerIncome: NSObject{

    var incomeCategory: String = ""
    var incomeCategoryTotal: Double = 0.0
    var incomes = [Income]()
    
    func updateModelWithJSON(json: JSON){
        incomeCategory = json["incomeCategory"].stringValue
        incomeCategoryTotal = json["incomeCategoryTotal"].doubleValue
        let incomesArray = json["incomes"].arrayValue
        for incomesJson in incomesArray{
            let model = Income()
            model.updateModelWithJSON(json: incomesJson)
            incomes.append(model)
        }
    }
    
}

class Income : NSObject{

    var employmentCategory = EmploymentCategory()
    var endDate: String = ""
    var incomeId: Int = 0
    var incomeName: String = ""
    var incomeTypeDisplayName: String = ""
    var incomeTypeId: Int = 0
    var incomeValue: Double = 0.0
    var isCurrentIncome : Bool!
    var isDisabledByTenant : Bool!
    var jobTitle: String = ""
    var startDate: String = ""

    func updateModelWithJSON(json: JSON){
        let employmentCategoryJson = json["employmentCategory"]
        let model = EmploymentCategory()
        model.updateModelWithJSON(json: employmentCategoryJson)
        employmentCategory = model
        endDate = json["endDate"].stringValue
        incomeId = json["incomeId"].intValue
        incomeName = json["incomeName"].stringValue
        incomeTypeDisplayName = json["incomeTypeDisplayName"].stringValue
        incomeTypeId = json["incomeTypeId"].intValue
        incomeValue = json["incomeValue"].doubleValue
        isCurrentIncome = json["isCurrentIncome"].boolValue
        isDisabledByTenant = json["isDisabledByTenant"].boolValue
        jobTitle = json["jobTitle"].stringValue
        startDate = json["startDate"].stringValue
    }
    
}

class EmploymentCategory: NSObject{

    var categoryDisplayName: String = ""
    var categoryId: Int = 0
    var categoryName: String = ""

    func updateModelWithJSON(json: JSON){
        categoryDisplayName = json["categoryDisplayName"].stringValue
        categoryId = json["categoryId"].intValue
        categoryName = json["categoryName"].stringValue
    }
    
}
