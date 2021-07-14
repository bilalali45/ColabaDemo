//
//  LoanInfoModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/07/2021.
//

import Foundation
import SwiftyJSON

class LoanInfoModel: NSObject{
    
    var borrowers: [BorrowerModel] = []
    var cellPhone: String = ""
    var downPayment: Int = 0
    var email: String = ""
    var loanAmount: Int = 0
    var loanNumber: String = ""
    var loanPurpose: String = ""
    var milestone: String = ""
    var milestoneId: Int = 0
    var postedOn: String = ""
    var propertyType: String = ""
    var propertyValue: Int = 0
    var city: String = ""
    var countryId: Int = 0
    var countryName: String = ""
    var stateId: Int = 0
    var stateName: String = ""
    var street: String = ""
    var unit: String = ""
    var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        
        borrowers.removeAll()
        let borrowersArray = json["borrowers"].arrayValue
        for borrower in borrowersArray{
            let model = BorrowerModel()
            model.updateModelWithJSON(json: borrower)
            borrowers.append(model)
        }
        cellPhone = json["cellPhone"].stringValue
        downPayment = json["downPayment"].intValue
        email = json["email"].stringValue
        loanAmount = json["loanAmount"].intValue
        loanNumber = json["loanNumber"].stringValue
        loanPurpose = json["loanPurpose"].stringValue
        milestone = json["milestone"].stringValue
        milestoneId = json["milestoneId"].intValue
        postedOn = json["postedOn"].stringValue
        propertyType = json["propertyType"].stringValue
        propertyValue = json["propertyValue"].intValue
        city = json["address"]["city"].stringValue
        countryId = json["address"]["countryId"].intValue
        countryName = json["address"]["countryName"].stringValue
        stateId = json["address"]["stateId"].intValue
        stateName = json["address"]["stateName"].stringValue
        street = json["address"]["street"].stringValue
        unit = json["address"]["unit"].stringValue
        zipCode = json["address"]["zipCode"].stringValue
    }
    
}

class BorrowerModel: NSObject{
    
    var firstName: String = ""
    var lastName: String = ""
    var ownTypeId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        firstName = json["firstName"].stringValue
        lastName = json["lastName"].stringValue
        ownTypeId = json["ownTypeId"].intValue
    }
    
}
