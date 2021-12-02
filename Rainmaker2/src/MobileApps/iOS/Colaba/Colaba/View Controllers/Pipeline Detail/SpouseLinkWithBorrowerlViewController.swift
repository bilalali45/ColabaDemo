//
//  SpouseLinkWithBorrowerlViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/11/2021.
//

import UIKit

protocol SpouseLinkWithBorrowerlViewControllerDelegate: AnyObject {
    func savePrimaryBorrowerMartialStatus(status: MaritalStatus)
}

class SpouseLinkWithBorrowerlViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldCoBorrowerName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseFirstName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseMiddleName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseLastName: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var inRelationWithCoBorrower = 0 // 1 for yes 2 for no
    var selectedMaritalStatus = MaritalStatus()
    var borrowerName = ""
    var isForSeparated = false
    var totalBorrowers = [BorrowerInfoModel]()
    var isForSecondaryBorrower = false
    var selectedPrimaryBorrowerId = 0
    weak var delegate: SpouseLinkWithBorrowerlViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        lblTopHeading.text = isForSeparated ? "Separated"  :"Married"
        lblBorrowerName.text = borrowerName.uppercased()
        setTextFields()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        setMaritalStatus()
    }
    
    //MARK:- Methods and Actions
    
    func setTextFields() {
        ///Co Borrower Name
        txtfieldCoBorrowerName.setTextField(placeholder: "Which One?", controller: self, validationType: .required)
        txtfieldCoBorrowerName.setIsValidateOnEndEditing(validate: true)
        txtfieldCoBorrowerName.type = .dropdown
        
        ///First Name
        txtfieldSpouseFirstName.setTextField(placeholder: "Spouse's Legal First Name", controller: self, validationType: .noValidation)
        
        ///Middle Name
        txtfieldSpouseMiddleName.setTextField(placeholder: "Middle Name", controller: self, validationType: .noValidation)
        
        ///Last Name
        txtfieldSpouseLastName.setTextField(placeholder: "Spouse's Legal Last Name", controller: self, validationType: .noValidation)
    }
    
    func setMaritalStatus(){
        if (isForSecondaryBorrower){
            if let primaryBorrower = totalBorrowers.filter({$0.borrowerId == selectedMaritalStatus.spouseBorrowerId}).first{
                lblQuestion.text = isForSeparated ? "Is \(primaryBorrower.firstName) your legal spouse" : "Are you married with \(primaryBorrower.firstName)"
                selectedPrimaryBorrowerId = primaryBorrower.borrowerId
                inRelationWithCoBorrower = 1
                txtfieldSpouseFirstName.setTextField(text: primaryBorrower.firstName)
                txtfieldSpouseMiddleName.setTextField(text: primaryBorrower.middleName)
                txtfieldSpouseLastName.setTextField(text: primaryBorrower.lastName)
                txtfieldSpouseFirstName.isEnabled = false
                txtfieldSpouseMiddleName.isEnabled = false
                txtfieldSpouseLastName.isEnabled = false
                changeRelationStatus()
            }
        }
        else{
            lblQuestion.text = isForSeparated ? "Is Co-borrower your legal spouse?" : "Are you married to a co-borrower?"
            let coBorrowers = totalBorrowers.filter({$0.borrowerId != selectedMaritalStatus.borrowerId})
            txtfieldCoBorrowerName.setDropDownDataSource(coBorrowers.map({$0.borrowerFullName}))
            if let selectedCoBorrower = coBorrowers.filter({$0.borrowerId == selectedMaritalStatus.spouseBorrowerId}).first{
                txtfieldCoBorrowerName.setTextField(text: selectedCoBorrower.borrowerFullName)
            }
            txtfieldSpouseFirstName.setTextField(text: selectedMaritalStatus.firstName)
            txtfieldSpouseMiddleName.setTextField(text: selectedMaritalStatus.middleName)
            txtfieldSpouseLastName.setTextField(text: selectedMaritalStatus.lastName)
            if (selectedMaritalStatus.spouseBorrowerId > 0){
                inRelationWithCoBorrower = 1
            }
            else if (selectedMaritalStatus.firstName != "" || selectedMaritalStatus.middleName != "" || selectedMaritalStatus.lastName != ""){
                inRelationWithCoBorrower = 2
            }
            changeRelationStatus()
        }
    }
    
    @objc func yesStackViewTapped(){
        inRelationWithCoBorrower = 1
        changeRelationStatus()
    }
    
    @objc func noStackViewTapped(){
        inRelationWithCoBorrower = 2
        changeRelationStatus()
    }
    
    func changeRelationStatus(){
        btnYes.setImage(UIImage(named: inRelationWithCoBorrower == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = inRelationWithCoBorrower == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: inRelationWithCoBorrower == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = inRelationWithCoBorrower == 2 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        if (isForSecondaryBorrower){
            txtfieldCoBorrowerName.isHidden = true
            txtfieldSpouseFirstName.isHidden = false
            txtfieldSpouseMiddleName.isHidden = false
            txtfieldSpouseLastName.isHidden = false
            if (inRelationWithCoBorrower == 1){
                if let primaryBorrower = totalBorrowers.filter({$0.borrowerId == selectedMaritalStatus.spouseBorrowerId}).first{
                    txtfieldSpouseFirstName.setTextField(text: primaryBorrower.firstName)
                    txtfieldSpouseMiddleName.setTextField(text: primaryBorrower.middleName)
                    txtfieldSpouseLastName.setTextField(text: primaryBorrower.lastName)
                    txtfieldSpouseFirstName.isEnabled = false
                    txtfieldSpouseMiddleName.isEnabled = false
                    txtfieldSpouseLastName.isEnabled = false
                }
            }
            else if (inRelationWithCoBorrower == 2){
                txtfieldSpouseFirstName.setTextField(text: "")
                txtfieldSpouseMiddleName.setTextField(text: "")
                txtfieldSpouseLastName.setTextField(text: "")
                txtfieldSpouseFirstName.isEnabled = true
                txtfieldSpouseMiddleName.isEnabled = true
                txtfieldSpouseLastName.isEnabled = true
            }
        }
        else{
            if (inRelationWithCoBorrower == 1){
                txtfieldCoBorrowerName.isHidden = false
                txtfieldSpouseFirstName.isHidden = true
                txtfieldSpouseMiddleName.isHidden = true
                txtfieldSpouseLastName.isHidden = true
            }
            else if (inRelationWithCoBorrower == 2){
                txtfieldCoBorrowerName.isHidden = true
                txtfieldSpouseFirstName.isHidden = false
                txtfieldSpouseMiddleName.isHidden = false
                txtfieldSpouseLastName.isHidden = false
            }
        }
        
    }
    
    func validate() -> Bool{
        if (inRelationWithCoBorrower == 1 && !txtfieldCoBorrowerName.isHidden){
            return txtfieldCoBorrowerName.validate()
        }
        else{
            return true
        }
    }
    
    func saveMaritalStatus(){
        
        if (isForSecondaryBorrower){
            if (inRelationWithCoBorrower == 1){
                selectedMaritalStatus.spouseBorrowerId = selectedPrimaryBorrowerId
                selectedMaritalStatus.firstName = ""
                selectedMaritalStatus.middleName = ""
                selectedMaritalStatus.lastName = ""
            }
            else if (inRelationWithCoBorrower == 2){
                selectedMaritalStatus.spouseBorrowerId = 0
                selectedMaritalStatus.firstName = txtfieldSpouseFirstName.text!
                selectedMaritalStatus.middleName = txtfieldSpouseMiddleName.text!
                selectedMaritalStatus.lastName = txtfieldSpouseLastName.text!
            }
        }
        else{
            if (inRelationWithCoBorrower == 1){
                if let selectedBorrower = totalBorrowers.filter({$0.borrowerFullName == txtfieldCoBorrowerName.text!}).first{
                    selectedMaritalStatus.spouseBorrowerId = selectedBorrower.borrowerId
                }
                selectedMaritalStatus.firstName = ""
                selectedMaritalStatus.middleName = ""
                selectedMaritalStatus.lastName = ""
                
            }
            else if (inRelationWithCoBorrower == 2){
                selectedMaritalStatus.spouseBorrowerId = 0
                selectedMaritalStatus.firstName = txtfieldSpouseFirstName.text!
                selectedMaritalStatus.middleName = txtfieldSpouseMiddleName.text!
                selectedMaritalStatus.lastName = txtfieldSpouseLastName.text!
            }
        }
        self.delegate?.savePrimaryBorrowerMartialStatus(status: selectedMaritalStatus)
        self.dismissVC()
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        if (validate()){
            saveMaritalStatus()
        }
    }
}
