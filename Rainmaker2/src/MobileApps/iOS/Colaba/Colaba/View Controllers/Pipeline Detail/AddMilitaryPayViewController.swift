//
//  AddMilitaryPayViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 14/09/2021.
//

import UIKit

class AddMilitaryPayViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldEmployerName: ColabaTextField!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var addAddressView: UIView!
    @IBOutlet weak var txtfieldJobTitle: ColabaTextField!
    @IBOutlet weak var txtfieldProfessionYears: ColabaTextField!
    @IBOutlet weak var txtfieldStartDate: ColabaTextField!
    @IBOutlet weak var txtfieldMonthlyBaseSalary: ColabaTextField!
    @IBOutlet weak var txtfieldMilitaryEntitlements: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var incomeInfoId = 0
    var militaryDetail = MilitaryPayDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextFields()
        lblUsername.text = borrowerName.uppercased()
        if (!isForAdd){
            getMilitaryPayDetail()
        }
    }
        
    //MARK:- Methods and Actions
    
    func setupTextFields(){
        /// Text Field
        txtfieldEmployerName.setTextField(placeholder: "Employer Name", controller: self, validationType: .required)
        
        /// Text Field
        txtfieldJobTitle.setTextField(placeholder: "Job Title", controller: self, validationType: .required)
        
        /// Text Field
        txtfieldProfessionYears.setTextField(placeholder: "Years in Profession", controller: self, validationType: .required, keyboardType: .numberPad)
        
        /// Text Field
        txtfieldStartDate.setTextField(placeholder: "Start Date (MM/DD/YYYY)", controller: self, validationType: .required)
        txtfieldStartDate.type = .datePicker
        
        /// Text Field
        txtfieldMonthlyBaseSalary.setTextField(placeholder: "Monthly Base Salary", controller: self, validationType: .required)
        txtfieldMonthlyBaseSalary.type = .amount
        
        /// Text Field
        txtfieldMilitaryEntitlements.setTextField(placeholder: "Military Entitlements", controller: self, validationType: .required)
        txtfieldMilitaryEntitlements.type = .amount
        
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        addressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
    }
    
    func setMilitaryPayDetail(){
        txtfieldEmployerName.setTextField(text: militaryDetail.employerName)
        let address = militaryDetail.address
        lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
        txtfieldJobTitle.setTextField(text: militaryDetail.jobTitle)
        txtfieldProfessionYears.setTextField(text: "\(militaryDetail.yearsInProfession)")
        txtfieldStartDate.setTextField(text: Utility.getDayMonthYear(militaryDetail.startDate))
        txtfieldMonthlyBaseSalary.setTextField(text: String(format: "%.0f", militaryDetail.monthlyBaseSalary))
        txtfieldMilitaryEntitlements.setTextField(text: String(format: "%.0f", militaryDetail.militaryEntitlements))
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getCurrentEmployerAddressVC()
        vc.topTitle = "Service Location Address"
        vc.searchTextFieldPlaceholder = "Search Service Location Address"
        vc.borrowerFullName = self.borrowerName
        if (!isForAdd){
            vc.selectedAddress = militaryDetail.address
        }
        self.pushToVC(vc: vc)
    }
    
    func validate() -> Bool {

        if (!txtfieldEmployerName.validate()) {
            return false
        }
        else if (!txtfieldJobTitle.validate()){
            return false
        }
        else if (!txtfieldProfessionYears.validate()){
            return false
        }
        else if (!txtfieldStartDate.validate()) {
            return false
        }
        else if (!txtfieldMonthlyBaseSalary.validate()){
            return false
        }
        else if (!txtfieldMilitaryEntitlements.validate()){
            return false
        }
        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldEmployerName.validate()
        txtfieldJobTitle.validate()
        txtfieldProfessionYears.validate()
        txtfieldStartDate.validate()
        txtfieldMonthlyBaseSalary.validate()
        txtfieldMilitaryEntitlements.validate()
        
        if validate(){
            self.dismissVC()
        }
    }
    
    //MARK:- API's
    
    func getMilitaryPayDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerid=\(borrowerId)&incomeInfoId=\(incomeInfoId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getMilitaryPayDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = MilitaryPayDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.militaryDetail = model
                    self.setMilitaryPayDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
}
