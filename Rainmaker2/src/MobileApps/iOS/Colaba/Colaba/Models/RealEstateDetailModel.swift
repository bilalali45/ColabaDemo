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
    var hasFirstMortgage: Bool = false
    var hasSecondMortgage: Bool = false
    var homeOwnerDues: Double = 0.0
    var occupancyTypeId: Int = 0
    var propertyInfoId: Int = 0
    var borrowerPropertyId: Int = 0
    var propertyStatus: Int = 0
    var propertyTypeId: Int = 0
    var propertyValue: Double = 0.0
    var rentalIncome: Double = 0.0
    var firstMortgage: FirstMortgageModel?
    var secondMortgage: SecondMortgageModel?
    
    func updateModelWithJSON(json: JSON){
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        address = model
        annualFloodInsurance = json["floodInsurance"].doubleValue
        annualHomeInsurance = json["homeOwnerInsurance"].doubleValue
        annualPropertyTax = json["propertyTax"].doubleValue
        hasFirstMortgage = json["hasFirstMortgage"].boolValue
        hasSecondMortgage = json["hasSecondMortgage"].boolValue
        homeOwnerDues = json["hoaDues"].doubleValue
        occupancyTypeId = json["occupancyTypeId"].intValue
        propertyInfoId = json["propertyInfoId"].intValue
        borrowerPropertyId = json["borrowerPropertyId"].intValue
        propertyStatus = json["propertyStatus"].intValue
        propertyTypeId = json["propertyTypeId"].intValue
        propertyValue = json["appraisedPropertyValue"].doubleValue
        rentalIncome = json["rentalIncome"].doubleValue
        if json["firstMortgageModel"] != JSON.null{
            let firstMortgageModelJson = json["firstMortgageModel"]
            let firstMortgageModel = FirstMortgageModel()
            firstMortgageModel.updateModelWithJSON(json: firstMortgageModelJson)
            firstMortgage = firstMortgageModel
        }
        if json["secondMortgageModel"] != JSON.null{
            let secondMortgageModelJson = json["secondMortgageModel"]
            let secondMortgageModel = SecondMortgageModel()
            secondMortgageModel.updateModelWithJSON(json: secondMortgageModelJson)
            secondMortgage = secondMortgageModel
        }
    }
    
}
