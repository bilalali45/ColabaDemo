//
//  SubjectPropertyModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/10/2021.
//

import Foundation
import SwiftyJSON

class SubjectPropertyModel: NSObject{
    
    var address: AddressModel?
    var appraisedPropertyValue: Double = 0.0
    var floodInsurance: Double = 0.0
    var homeOwnerInsurance: Double = 0.0
    var isMixedUseProperty: Bool = false
    var loanApplicationId: Int = 0
    var mixedUsePropertyExplanation: String = ""
    var occupancyTypeId: Int = 0
    var propertyTax: Double = 0.0
    var propertyTypeId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        
        let addressJson = json["address"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressJson)
        address = model
        appraisedPropertyValue = json["appraisedPropertyValue"].doubleValue
        floodInsurance = json["floodInsurance"].doubleValue
        homeOwnerInsurance = json["homeOwnerInsurance"].doubleValue
        isMixedUseProperty = json["isMixedUseProperty"].boolValue
        loanApplicationId = json["loanApplicationId"].intValue
        mixedUsePropertyExplanation = json["mixedUsePropertyExplanation"].stringValue
        occupancyTypeId = json["occupancyTypeId"].intValue
        propertyTax = json["propertyTax"].doubleValue
        propertyTypeId = json["propertyTypeId"].intValue
    }
}

class AddressModel: NSObject{
    
    var city: String = ""
    var countryId: Int = 0
    var countryName: String = ""
    var countyId: Int = 0
    var countyName: String = ""
    var stateId: Int = 0
    var stateName: String = ""
    var street: String = ""
    var unit: String = ""
    var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        city = json["city"].stringValue
        countryId = json["countryId"].intValue
        countryName = json["countryName"].stringValue
        countyId = json["countyId"].intValue
        countyName = json["countyName"].stringValue
        stateId = json["stateId"].intValue
        stateName = json["stateName"].stringValue
        street = json["street"].stringValue
        unit = json["unit"].stringValue
        zipCode = json["zipCode"].stringValue
    }
    
}

class CoBorrowerOccupancyModel: NSObject{
    
    var borrowerId: Int = 0
    var borrowerFirstName: String = ""
    var borrowerLastName: String = ""
    var borrowerFullName: String = ""
    var willLiveInSubjectProperty: Bool = false
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        borrowerFirstName = json["borrowerFirstName"].stringValue
        borrowerLastName = json["borrowerLastName"].stringValue
        borrowerFullName = "\(borrowerFirstName) \(borrowerLastName)"
        willLiveInSubjectProperty = json["willLiveInSubjectProperty"].boolValue
    }
    
}
