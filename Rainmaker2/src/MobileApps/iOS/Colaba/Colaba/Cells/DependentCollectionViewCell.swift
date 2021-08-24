//
//  DependentCollectionViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/08/2021.
//

import UIKit
import Material

protocol DependentCollectionViewCellDelegate: AnyObject {
    func deleteDependent(indexPath: IndexPath)
}

class DependentCollectionViewCell: UICollectionViewCell, UITextFieldDelegate {

    @IBOutlet weak var txtfieldAge: TextField!
    @IBOutlet weak var btnDelete: UIButton!
    weak var delegate: DependentCollectionViewCellDelegate?
    var indexPath = IndexPath()
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        txtfieldAge.dividerActiveColor = Theme.getButtonBlueColor()
        txtfieldAge.dividerColor = Theme.getSeparatorNormalColor()
        txtfieldAge.placeholderActiveColor = Theme.getAppGreyColor()
        txtfieldAge.delegate = self
        txtfieldAge.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        txtfieldAge.detailLabel.font = Theme.getRubikRegularFont(size: 12)
        txtfieldAge.detailColor = .red
        txtfieldAge.placeholderVerticalOffset = 8
        
    }
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        btnDelete.isHidden = false
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        btnDelete.isHidden = true
        if (txtfieldAge.text == ""){
            txtfieldAge.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
        }
        else{
            txtfieldAge.placeholderLabel.textColor = Theme.getAppGreyColor()
        }
    }

    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        self.delegate?.deleteDependent(indexPath: indexPath)
    }
}
