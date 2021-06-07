//
//  Theme.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit
import SVGKit

struct Theme {
    
    static func getButtonBlueColor() -> UIColor{
        return UIColor(named: "ButtonBlueColor")!
    }
    
    static func getButtonGreyColor() -> UIColor{
        return UIColor(named: "ButtonGreyColor")!
    }
    
    static func getButtonGreyTextColor() -> UIColor{
        return UIColor(named: "ButtonGreyTextColor")!
    }
    
    static func getSeparatorNormalColor() -> UIColor{
        return UIColor(named: "SeparatorNormalColor")!
    }
    
    static func getSeparatorErrorColor() -> UIColor{
        return UIColor(named: "SeparatorErrorColor")!
    }
    
    static func getCustomColorSVGImage(imageNamed: String, imageFrame: CGRect, customColor: UIColor, imageIconId: String) -> UIImage{
        let svgImage = SVGKImage(named: imageNamed)
        let svgIMGV = SVGKFastImageView(frame: imageFrame)
        svgIMGV.image = svgImage
        svgIMGV.fillTintColor(colorImage: customColor, iconID: imageIconId)
        return svgIMGV.image.uiImage
    }
}
