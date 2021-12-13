//
//  SpouseBasicDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/11/2021.
//

import UIKit

protocol SpouseBasicDetailViewControllerDelegate: AnyObject {
    func saveMaritalStatusMarriedOrSeparated(status: MaritalStatus)
}

class SpouseBasicDetailViewController: BaseViewController {

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
    weak var delegate: SpouseBasicDetailViewControllerDelegate?
    var isForSeparated = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        lblTopHeading.text = isForSeparated ? "Separated"  :"Married"
        lblBorrowerName.text = borrowerName.uppercased()
        setTextFields()
        setMaritalStatusData()
    }
    
    //MARK:- Methods and Actions
    
    func setTextFields() {
        ///First Name
        txtfieldSpouseFirstName.setTextField(placeholder: isForSeparated ? "Legal Spouse First Name" : "Spouse First Name", controller: self, validationType: .noValidation)
        
        ///Middle Name
        txtfieldSpouseMiddleName.setTextField(placeholder: isForSeparated ? "Legal Spouse Middle Name" : "Spouse Middle Name", controller: self, validationType: .noValidation)
        
        ///Last Name
        txtfieldSpouseLastName.setTextField(placeholder: isForSeparated ? "Legal Spouse Last Name" : "Spouse Last Name", controller: self, validationType: .noValidation)
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
        selectedMaritalStatus.spouseBorrowerId = 0
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
