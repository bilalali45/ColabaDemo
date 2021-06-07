//
//  ExtensionOfView.swift
//  Colaba
//
//  Created by Murtaza on 18/05/2021.
//

import Foundation
import UIKit

extension UIView{
    
    func addShadow(shadowColor: CGColor = UIColor(red: 5/255, green: 5/255, blue: 5/255, alpha: 0.12).cgColor,
                   shadowOffset: CGSize = CGSize(width: 3.0, height: 6.0),
                   shadowOpacity: Float = 1,
                   shadowRadius: CGFloat = 5.0) {
        layer.shadowColor = shadowColor
//        layer.shadowOffset = shadowOffset
//        layer.masksToBounds = false
        layer.shadowPath = UIBezierPath(rect: self.bounds).cgPath
        layer.shadowRadius = shadowRadius
        layer.shadowOffset = .zero
        layer.shadowOpacity = shadowOpacity
    }
    
}
