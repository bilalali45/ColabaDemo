//
//  CoBorrowerMarriedAndSepartedFollowUpQuestionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/11/2021.
//

import UIKit

class CoBorrowerMarriedAndSepartedFollowUpQuestionViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldSpouseFirstName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseMiddleName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseLastName: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    
    func setTextFields() {
        ///First Name
        txtfieldSpouseFirstName.setTextField(placeholder: "Spouse's Legal First Name", controller: self, validationType: .noValidation)
        
        ///Middle Name
        txtfieldSpouseMiddleName.setTextField(placeholder: "Middle Name", controller: self, validationType: .noValidation)
        
        ///Last Name
        txtfieldSpouseLastName.setTextField(placeholder: "Spouse's Legal Last Name", controller: self, validationType: .noValidation)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
}
