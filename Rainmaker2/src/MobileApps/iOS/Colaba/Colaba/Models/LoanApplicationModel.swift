//
//  LoanApplicationModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 02/08/2021.
//

import Foundation
import SwiftyJSON

class LoanApplicationModel: NSObject{
    
    var borrowersInformation: [BorrowerInfoModel] = []
    var city: String = ""
    var countryId: Int = 0
    var countryName: String = ""
    var stateId: Int = 0
    var stateName: String = ""
    var countyId: Int = 0
    var countyName: String = ""
    var street: String = ""
    var unit: String = ""
    var zipCode: String = ""
    var propertyInfoId: Int = 0
    var propertyTypeId: Int = 0
    var propertyTypeName: String = ""
    var propertyUsageId: Int = 0
    var propertyUsageName: String = ""
    var downPayment: Int = 0
    var downPaymentPercentage: Double = 0.0
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
            let model = BorrowerInfoModel()
            model.updateModelWithJSON(json: borrower)
            borrowersInformation.append(model)
        }
        
        propertyInfoId = subjectProperty["propertyInfoId"].intValue
        propertyTypeId = subjectProperty["propertyTypeId"].intValue
        propertyTypeName = subjectProperty["propertyTypeName"].stringValue
        propertyUsageId = subjectProperty["propertyUsageId"].intValue
        propertyUsageName = subjectProperty["propertyUsageDescription"].stringValue
        let address = subjectProperty["address"]
        city = address["city"].stringValue
        countryId = address["countryId"].intValue
        countryName = address["countryName"].stringValue
        stateId = address["stateId"].intValue
        stateName = address["stateName"].stringValue
        countyId = address["countyId"].intValue
        countyName = address["countyName"].stringValue
        street = address["street"].stringValue
        unit = address["unit"].stringValue
        zipCode = address["zipCode"].stringValue
        
        downPayment = loanInformation["downPayment"].intValue
        downPaymentPercentage = loanInformation["downPaymentPercent"].doubleValue
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
        
        governmentQuestions = governmentQuestions.sorted { question1, question2 in
            return question1.questionResponses.count < question2.questionResponses.count
        }
    }
    
}

class BorrowerInfoModel: NSObject{
    
    var borrowerId: Int = 0
    var ethnicities: [Ethnicity] = []
    var firstName:String = ""
    var genderId : Int!
    var genderName:String = ""
    var lastName:String = ""
    var ownTypeName:String = ""
    var ownTypeId: Int = 0
    var races: [Race] = []
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        ethnicities.removeAll()
        
        let ethnicitiesArray = json["ethnicities"].arrayValue
        for ethnicity in ethnicitiesArray{
            let model = Ethnicity()
            model.updateModelWithJSON(json: ethnicity)
            ethnicities.append(model)
        }
        
        firstName = json["firstName"].stringValue
        genderId = json["genderId"].intValue
        genderName = json["genderName"].stringValue
        lastName = json["lastName"].stringValue
        ownTypeName = json["ownTypeName"].stringValue
        ownTypeId = json["owntypeId"].intValue
        
        races.removeAll()
        let racesArray = json["races"].arrayValue
        for race in racesArray{
            let model = Race()
            model.updateModelWithJSON(json: race)
            races.append(model)
        }
    }
    
}

class Race: NSObject {
    
    var id: Int = 0
    var name: String = ""
    var raceDetails: [RaceDetail] = []
    var raceNameAndDetail: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
        
        let raceDetail = json["raceDetails"].arrayValue
        for detail in raceDetail{
            let model = RaceDetail()
            model.updateModelWithJSON(json: detail)
            raceDetails.append(model)
        }
        
        if (raceDetails.count > 0){
            raceNameAndDetail = "\(name)/\(raceDetails.first!.name)"
        }
        else{
            raceNameAndDetail = name
        }
    }
    
}

class RaceDetail: NSObject{
    var id: Int = 0
    var name: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
    }
}

class Ethnicity: NSObject {
    
    var id: Int = 0
    var name: String = ""
    var ethnicityDetails: [EthnicityDetail] = []
    var ethnicityNameAndDetail: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
        
        let raceDetail = json["ethnicityDetails"].arrayValue
        for detail in raceDetail{
            let model = EthnicityDetail()
            model.updateModelWithJSON(json: detail)
            ethnicityDetails.append(model)
        }
        
        if (ethnicityDetails.count > 0){
            ethnicityNameAndDetail = "\(name)/\(ethnicityDetails.first!.name)"
        }
        else{
            ethnicityNameAndDetail = name
        }
    }
    
}

class EthnicityDetail: NSObject{
    var id: Int = 0
    var name: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
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
    var countyId: Int = 0
    var countyName: String = ""
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
        countyId = address["countyId"].intValue
        countyName = address["countyName"].stringValue
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
