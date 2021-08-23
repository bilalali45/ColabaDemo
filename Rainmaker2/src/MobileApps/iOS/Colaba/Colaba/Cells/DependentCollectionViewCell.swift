//
//  DependentCollectionViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/08/2021.
//

import UIKit
import Material

class DependentCollectionViewCell: UICollectionViewCell, UITextFieldDelegate {

    @IBOutlet weak var txtfieldAge: TextField!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        txtfieldAge.dividerActiveColor = Theme.getButtonBlueColor()
        txtfieldAge.dividerColor = Theme.getSeparatorNormalColor()
        txtfieldAge.placeholderActiveColor = Theme.getAppGreyColor()
        txtfieldAge.delegate = self
        txtfieldAge.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        txtfieldAge.detailLabel.font = Theme.getRubikRegularFont(size: 12)
        txtfieldAge.detailColor = .red
        
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        if (txtfieldAge.text == ""){
            txtfieldAge.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        }
        else{
            txtfieldAge.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
    }

}
