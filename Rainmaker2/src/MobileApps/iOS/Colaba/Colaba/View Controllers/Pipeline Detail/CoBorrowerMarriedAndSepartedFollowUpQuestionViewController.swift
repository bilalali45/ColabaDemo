//
//  CoBorrowerMarriedAndSepartedFollowUpQuestionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/11/2021.
//

import UIKit

protocol CoBorrowerMarriedAndSepartedFollowUpQuestionViewControllerDelegate: AnyObject {
    func saveMaritalStatusMarriedOrSeparated(status: MaritalStatus)
}

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
    
    var selectedMaritalStatus = MaritalStatus()
    var borrowerName = ""
    weak var delegate: CoBorrowerMarriedAndSepartedFollowUpQuestionViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        lblBorrowerName.text = borrowerName.uppercased()
        setTextFields()
        setMaritalStatusData()
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
    
    func setMaritalStatusData(){
        txtfieldSpouseFirstName.setTextField(text: selectedMaritalStatus.firstName)
        txtfieldSpouseMiddleName.setTextField(text: selectedMaritalStatus.middleName)
        txtfieldSpouseLastName.setTextField(text: selectedMaritalStatus.lastName)
    }
    
    func saveMaritalStatusDetail(){
        selectedMaritalStatus.firstName = txtfieldSpouseFirstName.text!
        selectedMaritalStatus.middleName = txtfieldSpouseMiddleName.text!
        selectedMaritalStatus.lastName = txtfieldSpouseLastName.text!
        self.delegate?.saveMaritalStatusMarriedOrSeparated(status: selectedMaritalStatus)
        self.dismissVC()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        saveMaritalStatusDetail()
    }
    
}
