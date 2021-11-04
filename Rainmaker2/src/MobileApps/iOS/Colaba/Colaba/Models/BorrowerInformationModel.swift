//
//  BorrowerInformationModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 29/10/2021.
//

import Foundation
import SwiftyJSON

class BorrowerInformationModel: NSObject{
    
    var borrowerBasicDetails = BorrowerBasicDetail()
    var borrowerCitizenship =  BorrowerCitizenship()
    var borrowerId: Int = 0
    var currentAddress = BorrowerAddress()
    var loanApplicationId: Int = 0
    var maritalStatus = MaritalStatus()
    var militaryServiceDetails = MilitaryServiceDetail()
    var previousAddresses = [BorrowerAddress]()

    func updateModelWithJSON(json: JSON){
        
        let borrowerBasicDetailsJson = json["borrowerBasicDetails"]
        let borrowerBasicDetailModel = BorrowerBasicDetail()
        borrowerBasicDetailModel.updateModelWithJSON(json: borrowerBasicDetailsJson)
        self.borrowerBasicDetails = borrowerBasicDetailModel

        let borrowerCitizenshipJson = json["borrowerCitizenship"]
        let borrowerCitizenshipModel = BorrowerCitizenship()
        borrowerCitizenshipModel.updateModelWithJSON(json: borrowerCitizenshipJson)
        self.borrowerCitizenship = borrowerCitizenshipModel
        
        borrowerId = json["borrowerId"].intValue
        
        let currentAddressJson = json["currentAddress"]
        let currentAddressModel = BorrowerAddress()
        currentAddressModel.updateModelWithJSON(json: currentAddressJson)
        self.currentAddress = currentAddressModel
        
        loanApplicationId = json["loanApplicationId"].intValue
        
        let maritalStatusJson = json["maritalStatus"]
        let maritalStatusModel = MaritalStatus()
        maritalStatusModel.updateModelWithJSON(json: maritalStatusJson)
        self.maritalStatus = maritalStatusModel
        
        let militaryServiceDetailsJson = json["militaryServiceDetails"]
        let militaryServiceDetailModel = MilitaryServiceDetail()
        militaryServiceDetailModel.updateModelWithJSON(json: militaryServiceDetailsJson)
        self.militaryServiceDetails = militaryServiceDetailModel
       
        let previousAddressesArray = json["previousAddresses"].arrayValue
        for previousAddressesJson in previousAddressesArray{
            let model = BorrowerAddress()
            model.updateModelWithJSON(json: previousAddressesJson)
            previousAddresses.append(model)
        }
    }
}

class BorrowerBasicDetail : NSObject{

    var borrowerId: Int = 0
    var cellPhone: String = ""
    var emailAddress: String = ""
    var firstName: String = ""
    var homePhone: String = ""
    var lastName: String = ""
    var loanApplicationId: Int = 0
    var middleName: String = ""
    var ownTypeId: Int = 0
    var suffix: String = ""
    var workPhone: String = ""
    var workPhoneExt: String = ""

    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        cellPhone = json["cellPhone"].stringValue
        emailAddress = json["emailAddress"].stringValue
        firstName = json["firstName"].stringValue
        homePhone = json["homePhone"].stringValue
        lastName = json["lastName"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        middleName = json["middleName"].stringValue
        ownTypeId = json["ownTypeId"].intValue
        suffix = json["suffix"].stringValue
        workPhone = json["workPhone"].stringValue
        workPhoneExt = json["workPhoneExt"].stringValue
    }
    
}

class BorrowerAddress : NSObject{

    var addressModel = AddressModel()
    var borrowerId: Int = 0
    var fromDate: String = ""
    var toDate: String = ""
    var housingStatusId: Int = 0
    var id: Int = 0
    var isMailingAddressDifferent : Bool!
    var loanApplicationId: Int = 0
    var mailingAddressModel = AddressModel()
    var monthlyRent: Int = 0
    
    func updateModelWithJSON(json: JSON){
        let addressModelJson = json["addressModel"]
        let model = AddressModel()
        model.updateModelWithJSON(json: addressModelJson)
        self.addressModel = model
        borrowerId = json["borrowerId"].intValue
        fromDate = json["fromDate"].stringValue
        toDate = json["toDate"].stringValue
        housingStatusId = json["housingStatusId"].intValue
        id = json["id"].intValue
        isMailingAddressDifferent = json["isMailingAddressDifferent"].boolValue
        loanApplicationId = json["loanApplicationId"].intValue
        let mailingAddressJson = json["mailingAddressModel"]
        let model2 = AddressModel()
        model2.updateModelWithJSON(json: mailingAddressJson)
        self.mailingAddressModel = model2
        monthlyRent = json["monthlyRent"].intValue

    }
}

class MaritalStatus : NSObject{

    var borrowerId: Int = 0
    var firstName: String = ""
    var isInRelationship: Bool = false
    var lastName: String = ""
    var loanApplicationId: Int = 0
    var maritalStatusId: Int = 0
    var middleName: String = ""
    var otherRelationshipExplanation: String = ""
    var relationFormedStateId: Int = 0
    var relationWithPrimaryId: Int = 0
    var relationshipTypeId: Int = 0
    var spouseBorrowerId: Int = 0
    var spouseLoanContactId: Int = 0
    var spouseMaritalStatusId: Int = 0

    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        firstName = json["firstName"].stringValue
        isInRelationship = json["isInRelationship"].boolValue
        lastName = json["lastName"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        maritalStatusId = json["maritalStatusId"].intValue
        middleName = json["middleName"].stringValue
        otherRelationshipExplanation = json["otherRelationshipExplanation"].stringValue
        relationFormedStateId = json["relationFormedStateId"].intValue
        relationWithPrimaryId = json["relationWithPrimaryId"].intValue
        relationshipTypeId = json["relationshipTypeId"].intValue
        spouseBorrowerId = json["spouseBorrowerId"].intValue
        spouseLoanContactId = json["spouseLoanContactId"].intValue
        spouseMaritalStatusId = json["spouseMaritalStatusId"].intValue
    }
    
}

class BorrowerCitizenship : NSObject{

    var borrowerId: Int = 0
    var dependentAges: String = ""
    var dependentCount: Int = 0
    var dobUtc: String = ""
    var loanApplicationId: Int = 0
    var residencyStatusExplanation: String = ""
    var residencyStatusId: Int = 0
    var residencyTypeId: Int = 0
    var ssn: String = ""

    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        dependentAges = json["dependentAges"].stringValue
        dependentCount = json["dependentCount"].intValue
        dobUtc = json["dobUtc"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        residencyStatusExplanation = json["residencyStatusExplanation"].stringValue
        residencyStatusId = json["residencyStatusId"].intValue
        residencyTypeId = json["residencyTypeId"].intValue
        ssn = json["ssn"].stringValue
    }
    
}

class MilitaryServiceDetail: NSObject{

    var details = [Detail]()
    var isVaEligible: Bool = false
    
    func updateModelWithJSON(json: JSON){
        let detailsArray = json["details"].arrayValue
        for detailsJson in detailsArray{
            let model = Detail()
            model.updateModelWithJSON(json: detailsJson)
            details.append(model)
        }
        isVaEligible = json["isVaEligible"].boolValue

    }
    
}

class Detail: NSObject{

    var expirationDateUtc: String = ""
    var militaryAffiliationId: Int = 0
    var reserveEverActivated: Bool = false

    func updateModelWithJSON(json: JSON){
        expirationDateUtc = json["expirationDateUtc"].stringValue
        militaryAffiliationId = json["militaryAffiliationId"].intValue
        reserveEverActivated = json["reserveEverActivated"].boolValue
    }
    
}
