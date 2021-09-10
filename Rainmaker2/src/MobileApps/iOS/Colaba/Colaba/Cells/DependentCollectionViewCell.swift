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

class DependentCollectionViewCell: UICollectionViewCell, ColabaTextFieldDelegate {
    
    @IBOutlet weak var txtfieldAge: ColabaTextField!
    weak var delegate: DependentCollectionViewCellDelegate?
    var indexPath = IndexPath()
    
    override func awakeFromNib() {
        super.awakeFromNib()
        setTextFields()
    }
    
    func setTextFields() {
        ///Age Text Field
        txtfieldAge.setTextField(placeholder: "Email")
        txtfieldAge.setDelegates(collectionViewCell: self)
        txtfieldAge.setValidation(validationType: .required)
        txtfieldAge.setTextField(keyboardType: .numberPad)
        txtfieldAge.type = .delete
    }
    
    func textFieldEndEditing(_ textField: TextField) {
        print("End Editing Called")
    }
    
    func deleteButtonClicked() {
        self.delegate?.deleteDependent(indexPath: indexPath)
    }
}
