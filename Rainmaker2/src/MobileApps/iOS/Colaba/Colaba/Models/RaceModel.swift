//
//  RaceModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/10/2021.
//

import Foundation
import SwiftyJSON

class RaceModel: NSObject{
    
    var id: Int = 0
    var name: String = ""
    var isSelected: Bool = false
    var raceDetails = [RaceDetailModel]()
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        name = json["name"].stringValue
        let raceDetailsArray = json["raceDetails"].arrayValue
        for raceDetailsJson in raceDetailsArray{
            let model = RaceDetailModel()
            model.updateModelWithJSON(json: raceDetailsJson)
            raceDetails.append(model)
        }
    }
    
}

class RaceDetailModel: NSObject{
    
    var id: Int = 0
    var isOther: Bool = false
    var name: String = ""
    var otherPlaceHolder: String = ""
    var isSelected: Bool = false
    
    func updateModelWithJSON(json: JSON){
        id = json["id"].intValue
        isOther = json["isOther"].boolValue
        name = json["name"].stringValue
        otherPlaceHolder = json["otherPlaceHolder"].stringValue
    }

}
