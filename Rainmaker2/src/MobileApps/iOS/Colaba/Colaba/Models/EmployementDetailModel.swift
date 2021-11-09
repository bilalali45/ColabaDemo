//
//  EmployementDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 04/11/2021.
//

import Foundation
import SwiftyJSON

class EmployementDetailModel: NSObject{
    
    var borrowerId: Int = 0
    var employerAddress = AddressModel()
    var employmentInfo = EmploymentInfo()
    var employmentOtherIncome = [EmploymentOtherIncome]()
    var errorMessage: String = ""
    var loanApplicationId: Int = 0
    var wayOfIncome = WayOfIncome ()
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        
        let employerAddressJson = json["employerAddress"]
        let addressModel = AddressModel()
        addressModel.updateModelWithJSON(json: employerAddressJson)
        self.employerAddress = addressModel
        
        let employmentInfoJson = json["employmentInfo"]
        let employmentInfoModel = EmploymentInfo()
        employmentInfoModel.updateModelWithJSON(json: employmentInfoJson)
        self.employmentInfo = employmentInfoModel
        
        let employmentOtherIncomeArray = json["employmentOtherIncome"].arrayValue
        for employmentOtherIncomeJson in employmentOtherIncomeArray{
            let model = EmploymentOtherIncome()
            model.updateModelWithJSON(json: employmentOtherIncomeJson)
            employmentOtherIncome.append(model)
        }
        
        errorMessage = json["errorMessage"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        
        let wayOfIncomeJson = json["wayOfIncome"]
        let incomeWayModel = WayOfIncome()
        incomeWayModel.updateModelWithJSON(json: wayOfIncomeJson)
        self.wayOfIncome = incomeWayModel
    }
    
}

class WayOfIncome: NSObject{

    var employerAnnualSalary: Double = 0.0
    var hourlyRate: Double = 0.0
    var hoursPerWeek: Int = 0
    var isPaidByMonthlySalary: Bool = false
    
    func updateModelWithJSON(json: JSON){
        employerAnnualSalary = json["employerAnnualSalary"].doubleValue
        hourlyRate = json["hourlyRate"].doubleValue
        hoursPerWeek = json["hoursPerWeek"].intValue
        isPaidByMonthlySalary = json["isPaidByMonthlySalary"].boolValue
    }

}

class EmploymentOtherIncome: NSObject{

    var annualIncome: Double = 0.0
    var displayName: String = ""
    var incomeTypeId: Int = 0
    var name: String = ""

    func updateModelWithJSON(json: JSON){
        annualIncome = json["annualIncome"].doubleValue
        displayName = json["displayName"].stringValue
        incomeTypeId = json["incomeTypeId"].intValue
        name = json["name"].stringValue
    }
    
}

class EmploymentInfo: NSObject{

    var borrowerId: Int = 0
    var employedByFamilyOrParty: Bool = false
    var employerName: String = ""
    var employerPhoneNumber: String = ""
    var employmentCategory: String = ""
    var endDate: String = ""
    var hasOwnershipInterest: Bool = false
    var incomeInfoId: Int = 0
    var isCurrentIncome: Bool = false
    var jobTitle: String = ""
    var ownershipInterest: Int = 0
    var startDate : String!
    var yearsInProfession: Int = 0
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        employedByFamilyOrParty = json["employedByFamilyOrParty"].boolValue
        employerName = json["employerName"].stringValue
        employerPhoneNumber = json["employerPhoneNumber"].stringValue
        employmentCategory = json["employmentCategory"].stringValue
        endDate = json["endDate"].stringValue
        hasOwnershipInterest = json["hasOwnershipInterest"].boolValue
        incomeInfoId = json["incomeInfoId"].intValue
        isCurrentIncome = json["isCurrentIncome"].boolValue
        jobTitle = json["jobTitle"].stringValue
        ownershipInterest = json["ownershipInterest"].intValue
        startDate = json["startDate"].stringValue
        yearsInProfession = json["yearsInProfession"].intValue
    }

}
