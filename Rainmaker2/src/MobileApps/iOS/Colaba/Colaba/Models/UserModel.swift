//
//  UserModel.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 31/05/2021.
//

import Foundation
import RealmSwift
import SwiftyJSON

class UserModel: Object{
    
    @objc dynamic var refreshToken: String = ""
    @objc dynamic var refreshTokenValidTo: String = ""
    @objc dynamic var token: String = ""
    @objc dynamic var tokenType: Int = 0
    @objc dynamic var tokenTypeName: String = ""
    @objc dynamic var userName: String = ""
    @objc dynamic var userProfileId: Int = 0
    @objc dynamic var validFrom: String = ""
    @objc dynamic var validTo: String = ""
    
    func updateModelWithJSON(json: JSON){
        refreshToken = json["refreshToken"].stringValue
        refreshTokenValidTo = json["refreshTokenValidTo"].stringValue
        token = json["token"].stringValue
        tokenType = json["tokenType"].intValue
        tokenTypeName = json["tokenTypeName"].stringValue
        userName = json["userName"].stringValue
        userProfileId = json["userProfileId"].intValue
        validFrom = json["validFrom"].stringValue
        validTo = json["validTo"].stringValue

    }

    static func getCurrentUser() -> UserModel?{
        let realm = try! Realm()
        if let user = realm.objects(UserModel.self).first{
            return user
        }
        return nil
    }
}

