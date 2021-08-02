//
//  LoanApplicationModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/08/2021.
//

import Foundation
import SwiftyJSON

class LoanApplicationModel: NSObject{
    
    var borrowerInformation: [BorrowerModel] = []
    var firstName: String = ""
    var lastName: String = ""
    var city: String = ""
    var countryId: Int = 0
    var countryName: String = ""
    var stateId: Int = 0
    var stateName: String = ""
    var street: String = ""
    var unit: String = ""
    var zipCode: String = ""
    var propertyInfoId: Int = 0
    var propertyTypeId: Int = 0
    var propertyTypeName: String = ""
    var propertyUsageId: Int = 0
    var propertyUsageName: String = ""
    var deposit: Int = 0
    var depositPercent: Double = 0.0
    var loanAmount: Int = 0
    var loanPurposeDescription: String = ""
    var loanPurposeId: Int = 0
    var totalAsset: Double = 0.0
    var totalMonthyIncome: Double = 0.0
    var realEstateOwned: [RealEstateOwned] = []
    var governmentQuestions: [GovernmentQuestions] = []
    
    func updateModelWithJSON(json: JSON){
        
        let subjectProperty = json["subjectProperty"]
        let loanInformation = json["loanInformation"]
        let assetsAndIncome = json["assetAndIncome"]
        
        firstName = json["borrowerInformation"]["firstName"].stringValue
        lastName = json["borrowerInformation"]["lastName"].stringValue
        
        propertyInfoId = subjectProperty["propertyInfoId"].intValue
        propertyTypeId = subjectProperty["propertyTypeId"].intValue
        propertyTypeName = subjectProperty["propertyTypeName"].stringValue
        propertyUsageId = subjectProperty["propertyUsageId"].intValue
        propertyUsageName = subjectProperty["propertyUsageName"].stringValue
        let address = subjectProperty["address"]
        city = address["city"].stringValue
        countryId = address["countryId"].intValue
        countryName = address["countryName"].stringValue
        stateId = address["stateId"].intValue
        stateName = address["stateName"].stringValue
        street = address["street"].stringValue
        unit = address["unit"].stringValue
        zipCode = address["zipCode"].stringValue
        deposit = loanInformation["deposit"].intValue
        depositPercent = loanInformation["depositPercent"].doubleValue
        loanAmount = loanInformation["loanAmount"].intValue
        loanPurposeDescription = loanInformation["loanPurposeDescription"].stringValue
        loanPurposeId = loanInformation["loanPurposeId"].intValue
        totalAsset = assetsAndIncome["totalAsset"].doubleValue
        totalMonthyIncome = assetsAndIncome["totalMonthyIncome"].doubleValue
    }
    
}

class RealEstateOwned: NSObject{
    
}

class GovernmentQuestions: NSObject{
    
}
