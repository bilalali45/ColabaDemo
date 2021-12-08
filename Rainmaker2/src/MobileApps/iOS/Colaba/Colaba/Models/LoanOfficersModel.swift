//
//  LoanOfficersModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/12/2021.
//

import UIKit
import SwiftyJSON

class LoanOfficersModel: NSObject {
    
    var mcus = [Mcu]()
    var roleId: Int = 0
    var roleName: String = ""
    
    func updateModelWithJSON(json: JSON){
        let mcusArray = json["mcus"].arrayValue
        for mcusJson in mcusArray{
            let model = Mcu()
            model.updateModelWithJSON(json: mcusJson)
            mcus.append(model)
        }
        roleId = json["roleId"].intValue
        roleName = json["roleName"].stringValue
    }
}

class Mcu: NSObject{

    var branchId: Int = 0
    var branchName: String = ""
    var employeeId: Int = 0
    var fullName: String = ""
    var profileimageurl: String = ""
    var tenantCode: String = ""
    var userId: Int = 0
    var userName: String = ""
    
    func updateModelWithJSON(json: JSON){
        branchId = json["branchId"].intValue
        branchName = json["branchName"].stringValue
        employeeId = json["employeeId"].intValue
        fullName = json["fullName"].stringValue
        profileimageurl = json["profileimageurl"].stringValue
        tenantCode = json["tenantCode"].stringValue
        userId = json["userId"].intValue
        userName = json["userName"].stringValue
    }
    
}
