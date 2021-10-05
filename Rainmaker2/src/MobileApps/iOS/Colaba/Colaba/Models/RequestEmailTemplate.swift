//
//  RequestEmailTemplate.swift
//  Colaba
//
//  Created by Salman ~Khan on 05/10/2021.
//

import Foundation

struct RequestEmailTemplate: Codable {
    let title: String
    let description: String

    enum CodingKeys: String, CodingKey {
        case title
        case description
    }
}
