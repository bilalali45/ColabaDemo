//
//  ColabaButton.swift
//  Colaba
//
//  Created by Salman ~Khan on 21/09/2021.
//

import UIKit

class ColabaButton: UIButton {

    //MARK: Initializers
    public override init(frame: CGRect) {
        super.init(frame: frame)
        self.setupButton()
    }

    //initWithCode to init view from xib or storyboard
    public required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        self.setupButton()
    }
    
    func setupButton() {
        setButtonBorder(width: 1, color: Theme.getButtonBlueColor().withAlphaComponent(0.3))
        self.makeRoundedView()
        self.applyShadow(color: UIColor.white.withAlphaComponent(0.20), offset: .zero, opacity: 1.0, radius: 8.0)
        self.setBackgroundColor(color: Theme.getButtonBlueColor())
        self.setButton(image: UIImage(named: "SaveIcon")!)
    }
    
    public func setButtonBorder(width : CGFloat, color: UIColor) {
        layer.borderWidth = width
        layer.borderColor = color.cgColor
    }
    
    public func setBackgroundColor(color : UIColor) {
        self.backgroundColor = color
    }
    
    public func setButton(image: UIImage) {
        self.setImage(image, for: .normal)
    }
}
