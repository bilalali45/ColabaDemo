//
//  RealEstateDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/10/2021.
//

import Foundation
import SwiftyJSON

class RealEstateDetailModel: NSObject{
    
    var address: AddressModel?
    var annualFloodInsurance: Double = 0.0
    var annualHomeInsurance: Double = 0.0
    var annualPropertyTax: Double = 0.0
    var firstMortgageBalance: Double = 0.0
    var firstMortgagePayment: Double = 0.0
    var hasFirstMortgage: Bool = false
    var hasSecondMortgage: Bool = false
    var homeOwnerDues: Double = 0.0
    var occupancyTypeId: Int = 0
    var propertyInfoId: Int = 0
    var propertyStatus: String = ""
    var propertyTypeId: Int = 0
    var propertyValue: Double = 0.0
    var rentalIncome: Double = 0.0
    var secondMortgageBalance: Double = 0.0
    var secondMortgagePayment: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        address = model
        annualFloodInsurance = json["annualFloodInsurance"].doubleValue
        annualHomeInsurance = json["annualHomeInsurance"].doubleValue
        annualPropertyTax = json["annualPropertyTax"].doubleValue
        firstMortgageBalance = json["firstMortgageBalance"].doubleValue
        firstMortgagePayment = json["firstMortgagePayment"].doubleValue
        hasFirstMortgage = json["hasFirstMortgage"].boolValue
        hasSecondMortgage = json["hasSecondMortgage"].boolValue
        homeOwnerDues = json["homeOwnerDues"].doubleValue
        occupancyTypeId = json["occupancyTypeId"].intValue
        propertyInfoId = json["propertyInfoId"].intValue
        propertyStatus = json["propertyStatus"].stringValue
        propertyTypeId = json["propertyTypeId"].intValue
        propertyValue = json["propertyValue"].doubleValue
        rentalIncome = json["rentalIncome"].doubleValue
        secondMortgageBalance = json["secondMortgageBalance"].doubleValue
        secondMortgagePayment = json["secondMortgagePayment"].doubleValue
    }
    
}
