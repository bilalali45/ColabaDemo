//
//  GovernmentQuestionModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/10/2021.
//

import Foundation
import SwiftyJSON

class GovernmentQuestionModel: NSObject{
    
    var parentQuestionId: Int = 0
    var answer:String = ""
    var answerData = [AnswerData]()
    var answerDetail:String = ""
    var firstName:String = ""
    var headerText:String = ""
    var id:Int = 0
    var lastName:String = ""
    var ownTypeId:Int = 0
    var question:String = ""
    var questionSectionId:Int = 0
    var selectionOptionId: Int = 0
    var selectionOptionText: String = ""
    var isAffiliatedWithSeller: Bool = false
    var affiliationDescription: String = ""
    
    func updateModelWithJSON(json: JSON){
        parentQuestionId = json["parentQuestionId"].intValue
        answer = json["answer"].stringValue
        let answerDataArray = json["answerData"].arrayValue
        for answerDataJson in answerDataArray{
            let model = AnswerData()
            model.updateModelWithJSON(json: answerDataJson)
            answerData.append(model)
        }
        answerDetail = json["answerDetail"].stringValue
        firstName = json["firstName"].stringValue
        headerText = json["headerText"].stringValue
        id = json["id"].intValue
        lastName = json["lastName"].stringValue
        ownTypeId = json["ownTypeId"].intValue
        question = json["question"].stringValue
        questionSectionId = json["questionSectionId"].intValue
        selectionOptionId = json["selectionOptionId"].intValue
        selectionOptionText = json["answerData"]["selectionOptionText"].stringValue
        isAffiliatedWithSeller = json["answerData"]["IsAffiliatedWithSeller"].boolValue
        affiliationDescription = json["answerData"]["AffiliationDescription"].stringValue

    }
    
}

class AnswerData: NSObject{
    
    var liabilityName: String = ""
    var liabilityTypeId:Int = 0
    var monthlyPayment:Int = 0
    var name: String = ""
    var remainingMonth:Int = 0
    
    func updateModelWithJSON(json: JSON){
        liabilityName = json["liabilityName"].stringValue
        liabilityTypeId = json["liabilityTypeId"].intValue
        monthlyPayment = json["monthlyPayment"].intValue
        name = json["name"].stringValue
        remainingMonth = json["remainingMonth"].intValue
    }
}

