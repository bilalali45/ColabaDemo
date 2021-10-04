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
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
    }
    
    //MARK:- Methods and Actions
    
    func setupTextfields(){
        
        txtfieldPropertyType.setTextField(placeholder: "What type of property did you own?", controller: self, validationType: .required)
        txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.setDropDownDataSource(kOccupancyTypeArray)
        
        txtfieldHoldTitle.setTextField(placeholder: "How did you hold title to the property?", controller: self, validationType: .required)
        txtfieldHoldTitle.type = .dropdown
        txtfieldHoldTitle.setDropDownDataSource(kHoldTitleArray)
        

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
