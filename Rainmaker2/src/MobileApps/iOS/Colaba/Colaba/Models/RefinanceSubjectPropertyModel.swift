//
//  RefinanceSubjectPropertyModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/10/2021.
//

import Foundation
import SwiftyJSON

class RefinanceSubjectPropertyModel: NSObject{
   
    var address: AddressModel?
    var cashOutAmount: Double = 0.0
    var dateAcquired: String = ""
    var firstMortgage: FirstMortgageModel?
    var floodInsurance: Double = 0.0
    var hasFirstMortgage: Bool = false
    var hasSecondMortgage: Bool = false
    var hoaDues: Double = 0.0
    var homeOwnerInsurance: Double = 0.0
    var isMixedUseProperty: Bool = false
    var isSameAsPropertyAddress: Bool = false
    var loanAmount: Double = 0.0
    var loanApplicationId: Int = 0
    var loanGoalId: Int = 0
    var mixedUsePropertyExplanation: String = ""
    var propertyInfoId: Int = 0
    var propertyTax: Double = 0.0
    var propertyTypeId: Int = 0
    var propertyUsageId: Int = 0
    var propertyValue: Double = 0.0
    var rentalIncome: Double = 0.0
    var secondMortgage: SecondMortgageModel?
    
    func updateModelWithJSON(json: JSON){
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        address = model
        cashOutAmount = json["cashOutAmount"].doubleValue
        dateAcquired = json["dateAcquired"].stringValue
        if json["firstMortgageModel"] != JSON.null{
            let firstMortgageModelJson = json["firstMortgageModel"]
            let firstMortgageModel = FirstMortgageModel()
            firstMortgageModel.updateModelWithJSON(json: firstMortgageModelJson)
            firstMortgage = firstMortgageModel
        }
        floodInsurance = json["floodInsurance"].doubleValue
        hasFirstMortgage = json["hasFirstMortgage"].boolValue
        hasSecondMortgage = json["hasSecondMortgage"].boolValue
        hoaDues = json["hoaDues"].doubleValue
        homeOwnerInsurance = json["homeOwnerInsurance"].doubleValue
        isMixedUseProperty = json["isMixedUseProperty"].boolValue
        isSameAsPropertyAddress = json["isSameAsPropertyAddress"].boolValue
        loanAmount = json["loanAmount"].doubleValue
        loanApplicationId = json["loanApplicationId"].intValue
        loanGoalId = json["loanGoalId"].intValue
        mixedUsePropertyExplanation = json["mixedUsePropertyExplanation"].stringValue
        propertyInfoId = json["propertyInfoId"].intValue
        propertyTax = json["propertyTax"].doubleValue
        propertyTypeId = json["propertyTypeId"].intValue
        propertyUsageId = json["propertyUsageId"].intValue
        propertyValue = json["propertyValue"].doubleValue
        rentalIncome = json["rentalIncome"].doubleValue
        if json["secondMortgageModel"] != JSON.null{
            let secondMortgageModelJson = json["secondMortgageModel"]
            let secondMortgageModel = SecondMortgageModel()
            secondMortgageModel.updateModelWithJSON(json: secondMortgageModelJson)
            secondMortgage = secondMortgageModel
        }
    }
}

class FirstMortgageModel: NSObject{
    
    var id: Int = 0
    var propertyTax: Double = 0.0
    var homeOwnerInsurance: Double = 0.0
    var floodInsurance: Double = 0.0
    var loanApplicationId: Int = 0
    var firstMortgagePayment: Double = 0.0
    var floodInsuranceIncludeinPayment: Bool = false
    var helocCreditLimit: Double = 0.0
    var homeOwnerInsuranceIncludeinPayment: Bool = false
    var isHeloc: Bool = false
    var paidAtClosing: Bool = false
    var propertyTaxesIncludeinPayment: Bool = false
    var unpaidFirstMortgagePayment: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        propertyTax = json["propertyTax"].doubleValue
        homeOwnerInsurance = json["homeOwnerInsurance"].doubleValue
        floodInsurance = json["floodInsurance"].doubleValue
        loanApplicationId = json["loanApplicationId"].intValue
        firstMortgagePayment = json["firstMortgagePayment"].doubleValue
        floodInsuranceIncludeinPayment = json["floodInsuranceIncludeinPayment"].boolValue
        helocCreditLimit = json["helocCreditLimit"].doubleValue
        homeOwnerInsuranceIncludeinPayment = json["homeOwnerInsuranceIncludeinPayment"].boolValue
        isHeloc = json["isHeloc"].boolValue
        paidAtClosing = json["paidAtClosing"].boolValue
        propertyTaxesIncludeinPayment = json["propertyTaxesIncludeinPayment"].boolValue
        unpaidFirstMortgagePayment = json["unpaidFirstMortgagePayment"].doubleValue
    }
    
}

class SecondMortgageModel: NSObject{
    
    var id: Int = 0
    var loanApplicationId: Int = 0
    var combineWithNewFirstMortgage: Bool = false
    var helocCreditLimit: Double = 0.0
    var isHeloc: Bool = false
    var paidAtClosing: Bool = false
    var secondMortgagePayment: Double = 0.0
    var state: String = ""
    var unpaidSecondMortgagePayment: Double = 0.0
    var wasSmTaken: Bool = false
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        loanApplicationId = json["loanApplicationId"].intValue
        combineWithNewFirstMortgage = json["combineWithNewFirstMortgage"].boolValue
        helocCreditLimit = json["helocCreditLimit"].doubleValue
        isHeloc = json["isHeloc"].boolValue
        paidAtClosing = json["paidAtClosing"].boolValue
        secondMortgagePayment = json["secondMortgagePayment"].doubleValue
        state = json["state"].stringValue
        unpaidSecondMortgagePayment = json["unpaidSecondMortgagePayment"].doubleValue
        wasSmTaken = json["wasSmTaken"].boolValue

    }
    
}
