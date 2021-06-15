//
//  ExtensionOfView.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit

extension UIView{
    
    func addShadow(shadowColor: CGColor = UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.05).cgColor,
                   shadowOffset: CGSize = CGSize(width: 3.0, height: 6.0),
                   shadowOpacity: Float = 1,
                   shadowRadius: CGFloat = 5.0) {
        layer.shadowColor = shadowColor
        layer.shadowPath = UIBezierPath(rect: self.bounds).cgPath
        layer.shadowRadius = shadowRadius
        layer.shadowOffset = .zero
        layer.shadowOpacity = shadowOpacity
    }
    
    func roundButtonWithShadow(_ radius: CGFloat? = nil) {
        self.layer.cornerRadius = radius ?? self.frame.width / 2
        self.layer.shadowColor = UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.05).cgColor
        self.layer.shadowOffset = .zero
        self.layer.shadowRadius = 10.0
        self.layer.shadowOpacity = 1.0
        self.layer.masksToBounds = false
    }
    
    func roundAllCorners(radius: CGFloat) {
       self.clipsToBounds = true
       self.layer.cornerRadius = radius
       self.layer.maskedCorners = [.layerMinXMinYCorner, .layerMaxXMinYCorner, .layerMaxXMaxYCorner, .layerMinXMaxYCorner]
    }
    
    func roundOnlyTopCorners(radius: CGFloat) {
       self.clipsToBounds = true
       self.layer.cornerRadius = radius
       self.layer.maskedCorners = [.layerMinXMinYCorner, .layerMaxXMinYCorner] //[Top Left and Top Right]
    }
    
    func roundOnlyBottomCorners(radius: CGFloat) {
       self.clipsToBounds = true
       self.layer.cornerRadius = radius
       self.layer.maskedCorners = [.layerMinXMaxYCorner, .layerMaxXMaxYCorner] //[Bottom Left and Bottom Right]
    }
}
