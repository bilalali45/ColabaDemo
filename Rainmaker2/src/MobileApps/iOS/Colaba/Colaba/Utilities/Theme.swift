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
    
    static func getAppGreyColor() -> UIColor{
        return UIColor(named: "AppGreyColor")!
    }
    
    static func getAppBlackColor() -> UIColor{
        return UIColor(named: "AppBlackColor")!
    }
    
    static func getDashboardBackgroundColor() -> UIColor{
        return UIColor(named: "DashboardBackgroundColor")!
    }
    
    static func getSortIconDefaultColor() -> UIColor{
        return UIColor(named: "SortIconDefaultColor")!
    }
    
    static func getRubikRegularFont(size: CGFloat) -> UIFont{
        return UIFont(name: "Rubik-Regular", size: size)!
    }
    
    static func getRubikMediumFont(size: CGFloat) -> UIFont{
        return UIFont(name: "Rubik-Medium", size: size)!
    }
    
    static func getRubikBoldFont(size: CGFloat) -> UIFont{
        return UIFont(name: "Rubik-Bold", size: size)!
    }
    
    static func getSearchBarBorderColor() -> UIColor{
        return UIColor(named: "SearchBarBorderColor")!
    }
    
    static func getTopTabsColor() -> UIColor{
        return UIColor(named: "TopTabsColor")!
    }
    
    static func getCustomColorSVGImage(imageNamed: String, imageFrame: CGRect, customColor: UIColor, imageIconId: String) -> UIImage{
        let svgImage = SVGKImage(named: imageNamed)
        let svgIMGV = SVGKFastImageView(frame: imageFrame)
        svgIMGV.image = svgImage
        svgIMGV.fillTintColor(colorImage: customColor, iconID: imageIconId)
        return svgIMGV.image.uiImage
    }
}
