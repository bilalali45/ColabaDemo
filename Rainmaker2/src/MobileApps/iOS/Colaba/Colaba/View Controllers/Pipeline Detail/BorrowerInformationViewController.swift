//
//  BorrowerInformationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/08/2021.
//

import UIKit
import Material

class BorrowerInformationViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblBorrowerType: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var txtfieldLegalFirstName: TextField!
    @IBOutlet weak var txtfieldMiddleName: TextField!
    @IBOutlet weak var txtfieldLegalLastName: TextField!
    @IBOutlet weak var txtfieldSuffix: TextField!
    @IBOutlet weak var txtfieldEmail: TextField!
    @IBOutlet weak var txtfieldHomeNumber: TextField!
    @IBOutlet weak var txtfieldWorkNumber: TextField!
    @IBOutlet weak var txtfieldExtensionNumber: TextField!
    @IBOutlet weak var txtfieldCellNumber: TextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setMaterialTextFields(textfields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber])
        //txtfieldEmail.detail = "Please enter email"
        
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFields(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
            
        }
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
   
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
}

extension BorrowerInformationViewController: UITextFieldDelegate{
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber])
    }
}
