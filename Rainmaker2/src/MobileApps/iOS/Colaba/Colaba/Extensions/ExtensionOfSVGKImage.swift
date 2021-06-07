//
//  ExtensionOfSVGKKit.swift
//  Colaba
//
//  Created by Murtaza on 20/05/2021.
//

import Foundation
import UIKit
import SVGKit

extension SVGKImageView {
 func fillTintColor(colorImage: UIColor, iconID: String) {
        if self.image != nil && self.image.caLayerTree != nil {
            print(self.image.caLayerTree.sublayers)
            guard let sublayers = self.image.caLayerTree.sublayers else { return }
            fillRecursively(sublayers: sublayers, color: colorImage, iconID: iconID, hasFoundLayer: true)
        }
    }

     private func fillRecursively(sublayers: [CALayer], color: UIColor, iconID: String, hasFoundLayer: Bool) {
        for layer in sublayers {
            if let l = layer as? CAShapeLayer {

                print(l.name)
                //IF you want to color the specific shapelayer by id else remove the l.name  == "myID"  validation
                if let layerName = l.name, layerName == iconID {
                    self.colorThatImageWIthColor(color: color, layer: l)
                    //print("Colouring FInished")
                }
                
            } else {
                if layer.name == iconID {
                    if let innerSublayer = layer.sublayers as? [CAShapeLayer] {
                        fillRecursively(sublayers: innerSublayer, color: color, iconID: iconID, hasFoundLayer: true )
                        //print("FOund")
                    }
                } else {
                    if let l = layer as? CALayer, let sub = l.sublayers {
                        fillRecursively(sublayers: sub, color: color, iconID: iconID, hasFoundLayer: false)
                    }
                }
            }

        }
    }

    func colorThatImageWIthColor(color: UIColor, layer: CAShapeLayer) {
        if layer.strokeColor != nil {
            layer.strokeColor = color.cgColor
        }
        if layer.fillColor != nil {
            layer.fillColor = color.cgColor
        }
    }

}
