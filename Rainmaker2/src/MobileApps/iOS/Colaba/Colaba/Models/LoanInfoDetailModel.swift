//
//  LoanInfoDetailModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/10/2021.
//

import Foundation
import SwiftyJSON

class LoanInfoDetailModel: NSObject {
    
    var cashOutAmount: Double = 0.0
    var downPayment: Double = 0.0
    var expectedClosingDate: String = ""
    var loanApplicationId: Int = 0
    var loanGoalId: Int = 0
    var loanGoalName: String = ""
    var loanPayment: Double = 0.0
    var loanPurposeDescription: String = ""
    var loanPurposeId: Int = 0
    var propertyValue: Double = 0.0
    
    func updateModelWithJSON(json: JSON){
        cashOutAmount = json["cashOutAmount"].doubleValue
        downPayment = json["downPayment"].doubleValue
        expectedClosingDate = json["expectedClosingDate"].stringValue
        loanApplicationId = json["loanApplicationId"].intValue
        loanGoalId = json["loanGoalId"].intValue
        loanGoalName = json["loanGoalName"].stringValue
        loanPayment = json["loanPayment"].doubleValue
        loanPurposeDescription = json["loanPurposeDescription"].stringValue
        loanPurposeId = json["loanPurposeId"].intValue
        propertyValue = json["propertyValue"].doubleValue
    }
    
}
