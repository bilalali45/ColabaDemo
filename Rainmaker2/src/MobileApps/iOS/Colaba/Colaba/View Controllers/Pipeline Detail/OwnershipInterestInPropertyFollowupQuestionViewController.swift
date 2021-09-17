//
//  OwnershipInterestInPropertyFollowupQuestionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import MaterialComponents

class OwnershipInterestInPropertyFollowupQuestionViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var txtfieldHoldTitle: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
    }
    
    //MARK:- Methods and Actions
    
    func setupTextfields(){
        
        txtfieldPropertyType.setTextField(placeholder: "What type of property did you own?")
        txtfieldPropertyType.setDelegates(controller: self)
        txtfieldPropertyType.setValidation(validationType: .required)
        txtfieldPropertyType.setTextField(keyboardType: .asciiCapable)
        txtfieldPropertyType.setIsValidateOnEndEditing(validate: true)
        txtfieldPropertyType.setValidation(validationType: .required)
        txtfieldPropertyType.type = .dropdown //kOccupancyTypeArray
        
        txtfieldHoldTitle.setTextField(placeholder: "How did you hold title to the property?")
        txtfieldHoldTitle.setDelegates(controller: self)
        txtfieldHoldTitle.setValidation(validationType: .required)
        txtfieldHoldTitle.setTextField(keyboardType: .asciiCapable)
        txtfieldHoldTitle.setIsValidateOnEndEditing(validate: true)
        txtfieldHoldTitle.setValidation(validationType: .required)
        txtfieldHoldTitle.type = .dropdown //kHoldTitleArray
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
    }
    
    func validate() -> Bool {

        if (!txtfieldPropertyType.validate()) {
            return false
        }
        if (!txtfieldHoldTitle.validate()){
            return false
        }
        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldPropertyType.validate()
        txtfieldHoldTitle.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
    
}
