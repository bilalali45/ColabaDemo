//
//  CountriesModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/10/2021.
//

import Foundation
import SwiftyJSON

class CountriesModel: NSObject{
    
    var id: Int = 0
    var name: String = ""
    var shortCode: String = ""
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
        shortCode = json["shortCode"].stringValue
    }

}
