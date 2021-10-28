//
//  DemographicModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/10/2021.
//

import Foundation
import SwiftyJSON

class DemographicModel: NSObject{
    
    var borrowerId: Int = 0
    var ethnicity = [DemographicEthnicity]()
    var genderId: Int = 0
    var loanApplicationId: Int = 0
    var race = [DemographicRace]()
    var state: String = ""
    
    func updateModelWithJSON(json: JSON){
        borrowerId = json["borrowerId"].intValue
        let ethnicityArray = json["ethnicity"].arrayValue
        for ethnicityJson in ethnicityArray{
            let model = DemographicEthnicity()
            model.updateModelWithJSON(json: ethnicityJson)
            ethnicity.append(model)
        }
        genderId = json["genderId"].intValue
        loanApplicationId = json["loanApplicationId"].intValue
        let raceArray = json["race"].arrayValue
        for raceJson in raceArray{
            let model = DemographicRace()
            model.updateModelWithJSON(json: raceJson)
            race.append(model)
        }
        state = json["state"].stringValue
    }
    
}

class DemographicRace: NSObject {
    
    var raceDetails = [DemographicRaceDetail]()
    var raceId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        let raceDetailsArray = json["raceDetails"].arrayValue
        for raceDetailsJson in raceDetailsArray{
            let model = DemographicRaceDetail()
            model.updateModelWithJSON(json: raceDetailsJson)
            raceDetails.append(model)
        }
        raceId = json["raceId"].intValue
    }
    
}

class DemographicRaceDetail: NSObject{
    
    var detailId: Int = 0
    var name: String = ""
    var isOther: Bool = false
    var otherRace: String = ""
    
    func updateModelWithJSON(json: JSON){
        detailId = json["detailId"].intValue
        name = json["name"].stringValue
        isOther = json["isOther"].boolValue
        otherRace = json["otherRace"].stringValue
    }
    
}

class DemographicEthnicity: NSObject{
    
    var ethnicityDetails = [DemographicEthnicityDetail]()
    var ethnicityId: Int = 0
    
    func updateModelWithJSON(json: JSON){
        let ethnicityDetailsArray = json["ethnicityDetails"].arrayValue
        for ethnicityDetailsJson in ethnicityDetailsArray{
            let model = DemographicEthnicityDetail()
            model.updateModelWithJSON(json: ethnicityDetailsJson)
            ethnicityDetails.append(model)
        }
        ethnicityId = json["ethnicityId"].intValue
    }
    
}

class DemographicEthnicityDetail: NSObject{
    
    var detailId: Int = 0
    var name: String = ""
    var isOther: Bool = false
    var otherEthnicity: String = ""
    
    func updateModelWithJSON(json: JSON){
        detailId = json["detailId"].intValue
        name = json["name"].stringValue
        isOther = json["isOther"].boolValue
        otherEthnicity = json["otherEthnicity"].stringValue
    }
    
}
