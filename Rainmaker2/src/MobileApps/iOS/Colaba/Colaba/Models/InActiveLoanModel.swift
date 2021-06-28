//
//  InActiveLoanModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/06/2021.
//

import Foundation
import SwiftyJSON
import RealmSwift

class InActiveLoanModel: Object{
    
    @objc dynamic var activityTime: String = ""
    @objc dynamic var cellNumber: String = ""
    @objc dynamic var coBorrowerCount: Int = 0
    @objc dynamic var documents: Int = 0
    @objc dynamic var email: String = ""
    @objc dynamic var firstName: String = ""
    @objc dynamic var lastName: String = ""
    @objc dynamic var loanApplicationId: Int = 0
    @objc dynamic var loanPurpose: String = ""
    @objc dynamic var milestone: String = ""
    @objc dynamic var loanAmount: Int = 0
    @objc dynamic var propertyValue: Int = 0
    @objc dynamic var city: String = ""
    @objc dynamic var countryId: Int = 0
    @objc dynamic var countryName: String = ""
    @objc dynamic var stateId: Int = 0
    @objc dynamic var stateName: String = ""
    @objc dynamic var street: String = ""
    @objc dynamic var unit: String = ""
    @objc dynamic var zipCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        
        activityTime = json["activityTime"].stringValue
        cellNumber = json["cellNumber"].stringValue
        coBorrowerCount = json["coBorrowerCount"].intValue
        documents = json["documents"].intValue
        email = json["email"].stringValue
        firstName = json["firstName"].stringValue
        lastName = json["lastName"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        loanPurpose = json["loanPurpose"].stringValue
        milestone = json["milestone"].stringValue
        loanAmount = json["detail"]["loanAmount"].intValue
        propertyValue = json["detail"]["propertyValue"].intValue
        city = json["detail"]["address"]["city"].stringValue
        countryId = json["detail"]["address"]["countryId"].intValue
        countryName = json["detail"]["address"]["countryName"].stringValue
        stateId = json["detail"]["address"]["stateId"].intValue
        stateName = json["detail"]["address"]["stateName"].stringValue
        street = json["detail"]["address"]["street"].stringValue
        unit = json["detail"]["address"]["unit"].stringValue
        zipCode = json["detail"]["address"]["zipCode"].stringValue
        
    }
    
    static func getAllInActiveLoans() -> [InActiveLoanModel]{
        let realm = try! Realm()
        return Array(realm.objects(InActiveLoanModel.self))
    }
}
