//
//  LoanGoalModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/10/2021.
//

import Foundation
import SwiftyJSON

class LoanGoalModel: NSObject{
    
    var loanGoal: String = ""
    var id: Int = 0
    var loanPurposeId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        loanGoal = json["description"].stringValue
        id = json["id"].intValue
        loanPurposeId = json["loanPurposeId"].intValue
    }
}
