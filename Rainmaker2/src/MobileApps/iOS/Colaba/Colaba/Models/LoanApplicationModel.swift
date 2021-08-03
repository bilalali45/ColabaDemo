//
//  LoanApplicationModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/08/2021.
//

import Foundation
import SwiftyJSON

class LoanApplicationModel: NSObject{
    
    var borrowersInformation: [BorrowerModel] = []
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
    var realEstatesOwned: [RealEstateOwned] = []
    var governmentQuestions: [GovernmentQuestions] = []
    
    func updateModelWithJSON(json: JSON){
        
        let borrowerInformation = json["borrowersInformation"].arrayValue
        let subjectProperty = json["subjectProperty"]
        let loanInformation = json["loanInformation"]
        let assetsAndIncome = json["assetAndIncome"]
        let realEstateOwned = json["realStateOwns"].arrayValue
        let questions = json["borrowerQuestionsModel"].arrayValue
        
        borrowersInformation.removeAll()
        for borrower in borrowerInformation{
            let model = BorrowerModel()
            model.updateModelWithJSON(json: borrower)
            borrowersInformation.append(model)
        }
        
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
        
        realEstatesOwned.removeAll()
        for realEstate in realEstateOwned{
            let model = RealEstateOwned()
            model.updateModelWithJSON(json: realEstate)
            realEstatesOwned.append(model)
        }
        
        governmentQuestions.removeAll()
        for question in questions{
            let model = GovernmentQuestions()
            model.updateModelWithJSON(json: question)
            governmentQuestions.append(model)
        }
        
    }
    
}

class RealEstateOwned: NSObject{
    
    var borrowerId: Int = 0
    var propertyInfoId: Int = 0
    var propertyTypeId: Int = 0
    var propertyTypeName: String = ""
    var city: String = ""
    var countryId: Int = 0
    var countryName: String = ""
    var stateId: Int = 0
    var stateName: String = ""
    var street: String = ""
    var unit: String = ""
    var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        propertyInfoId = json["propertyInfoId"].intValue
        propertyTypeId = json["propertyTypeId"].intValue
        propertyTypeName = json["propertyTypeName"].stringValue
        let address = json["address"]
        city = address["city"].stringValue
        countryId = address["countryId"].intValue
        countryName = address["countryName"].stringValue
        stateId = address["stateId"].intValue
        stateName = address["stateName"].stringValue
        street = address["street"].stringValue
        unit = address["unit"].stringValue
        zipCode = address["zipCode"].stringValue
    }
}

class GovernmentQuestions: NSObject{
    
    var questionHeader:String = ""
    var questionId: Int = 0
    var questionText: String = ""
    var questionResponses: [QuestionResponse] = []
    
    func updateModelWithJSON(json: JSON){
        
        let questionDetail = json["questionDetail"]
        let questionsResponses = json["questionResponses"].arrayValue
        
        questionHeader = questionDetail["questionHeader"].stringValue
        questionId = questionDetail["questionId"].intValue
        questionText = questionDetail["questionText"].stringValue
        
        for questionResponse in questionsResponses{
            let model = QuestionResponse()
            model.updateModelWithJSON(json: questionResponse)
            questionResponses.append(model)
        }
        
    }
    
}

class QuestionResponse: NSObject{
    
    var borrowerFirstName: String = ""
    var borrowerId: Int = 0
    var borrowerLastName: String = ""
    var questionId: Int = 0
    var questionResponseText: String = ""
    
    func updateModelWithJSON(json: JSON){
        borrowerFirstName = json["borrowerFirstName"].stringValue
        borrowerId = json["borrowerId"].intValue
        borrowerLastName = json["borrowerLastName"].stringValue
        questionId = json["questionId"].intValue
        questionResponseText = json["questionResponseText"].stringValue
    }
    
}
