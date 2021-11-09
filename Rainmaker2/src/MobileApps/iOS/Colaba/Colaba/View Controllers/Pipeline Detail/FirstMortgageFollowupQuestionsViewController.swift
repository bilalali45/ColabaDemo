//
//  FirstMortgageFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/09/2021.
//

import UIKit
import Material

protocol FirstMortgageFollowupQuestionsViewControllerDelegate: AnyObject {
    func saveFirstMortageObject(firstMortgage: [String: Any])
}

class FirstMortgageFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: ColabaTextField!
    @IBOutlet weak var txtfieldMortgageBalance: ColabaTextField!
    @IBOutlet weak var accountPaymentsView: UIView!
    @IBOutlet weak var accountPaymentViewHeightConstraint: NSLayoutConstraint! // 337 or 248
    @IBOutlet weak var lblAccountPaymentQuestion: UILabel!
    @IBOutlet weak var annualFloodInsuranceStackView: UIStackView!
    @IBOutlet weak var btnAnnualFloodInsurance: UIButton!
    @IBOutlet weak var lblAnnualFloodInsurance: UILabel!
    @IBOutlet weak var annualPropertyTaxesStackView: UIStackView!
    @IBOutlet weak var btnAnnualTaxes: UIButton!
    @IBOutlet weak var lblAnnualTaxes: UILabel!
    @IBOutlet weak var annualHomeownerInsuranceStackView: UIStackView!
    @IBOutlet weak var btnAnnualHomeownerInsurance: UIButton!
    @IBOutlet weak var lblAnnualHomeownerInsurance: UILabel!
    @IBOutlet weak var homeEquityStackView: UIStackView!
    @IBOutlet weak var switchHomeEquity: UISwitch!
    @IBOutlet weak var lblHomeEquity: UILabel!
    @IBOutlet weak var txtfieldCreditLimit: ColabaTextField!
    @IBOutlet weak var mortgagePaidOffView: UIView!
    @IBOutlet weak var lblMortgagePaidOffQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isAnnualFloodInsurance = false
    var isAnnualPropertyTax = false
    var isAnnualHomeownerInsurance = false
    var isMortgagePaidOff:Int?
    
    var loanApplicationId = 0
    var borrowerPropertyId = 0
    
    var isForRealEstate = false
    var streetAddress = ""
    var mortgageDetail: FirstMortgageModel?
    weak var delegate: FirstMortgageFollowupQuestionsViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        if (isForRealEstate){
            lblUsername.text = streetAddress
        }
        btnAnnualFloodInsurance.isUserInteractionEnabled = false
        btnAnnualTaxes.isUserInteractionEnabled = false
        btnAnnualHomeownerInsurance.isUserInteractionEnabled = false
        changeAccountsIncluded()
        changeMortgagePaidOffStatus()
        if (isForRealEstate){
           getMortgageData()
        }
        else{
            setMortgageData()
        }
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(){

        annualFloodInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualFloodInsuranceStackViewTapped)))
        annualPropertyTaxesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualPropertyTaxStackViewTapped)))
        annualHomeownerInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualHomeownerInsuranceStackViewTapped)))
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        setTextFields()
    }
    
    func setTextFields() {
        ///First Mortgage Payment Text Field
        txtfieldMortgagePayment.setTextField(placeholder: "First Mortgage Payment", controller: self, validationType: .noValidation)
        txtfieldMortgagePayment.type = .amount
        
        ///Unpaid First Mortgage Balance Text Field
        txtfieldMortgageBalance.setTextField(placeholder: "Unpaid First Mortgage Balance", controller: self, validationType: .noValidation)
        txtfieldMortgageBalance.type = .amount
        
        ///Credit Limit Text Field
        txtfieldCreditLimit.setTextField(placeholder: "Credit Limit", controller: self, validationType: .noValidation)
        txtfieldCreditLimit.type = .amount
    }
    
    func setMortgageData(){
        if let firstMortgage = mortgageDetail{
            txtfieldMortgagePayment.setTextField(text: String(format: "%.0f", firstMortgage.firstMortgagePayment.rounded()))
            txtfieldMortgageBalance.setTextField(text: String(format: "%.0f", firstMortgage.unpaidFirstMortgagePayment.rounded()))
            isAnnualFloodInsurance = firstMortgage.floodInsuranceIncludeinPayment
            isAnnualPropertyTax = firstMortgage.propertyTaxesIncludeinPayment
            isAnnualHomeownerInsurance = firstMortgage.homeOwnerInsuranceIncludeinPayment
            switchHomeEquity.isOn = firstMortgage.isHeloc
            txtfieldCreditLimit.setTextField(text: String(format: "%.0f", firstMortgage.helocCreditLimit.rounded()))
            isMortgagePaidOff = firstMortgage.paidAtClosing == true ? 1 : 0
            changeMortgagePaidOffStatus()
            changeAccountsIncluded()
            changeHELOCStatus()
        }
    }
    
    @objc func annualFloodInsuranceStackViewTapped(){
        isAnnualFloodInsurance = !isAnnualFloodInsurance
        changeAccountsIncluded()
    }
    
    @objc func annualPropertyTaxStackViewTapped(){
        isAnnualPropertyTax = !isAnnualPropertyTax
        changeAccountsIncluded()
    }
    
    @objc func annualHomeownerInsuranceStackViewTapped(){
        isAnnualHomeownerInsurance = !isAnnualHomeownerInsurance
        changeAccountsIncluded()
    }
    
    func changeAccountsIncluded(){
        btnAnnualFloodInsurance.setImage(UIImage(named: isAnnualFloodInsurance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualFloodInsurance.font = isAnnualFloodInsurance ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnAnnualTaxes.setImage(UIImage(named: isAnnualPropertyTax ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualTaxes.font = isAnnualPropertyTax ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnAnnualHomeownerInsurance.setImage(UIImage(named: isAnnualHomeownerInsurance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualHomeownerInsurance.font = isAnnualHomeownerInsurance ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @objc func yesStackViewTapped(){
        isMortgagePaidOff = 1
        changeMortgagePaidOffStatus()
    }
    
    @objc func noStackViewTapped(){
        isMortgagePaidOff = 0
        changeMortgagePaidOffStatus()
    }
    
    func changeMortgagePaidOffStatus(){
        btnYes.setImage(UIImage(named: isMortgagePaidOff == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMortgagePaidOff == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isMortgagePaidOff == 0 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isMortgagePaidOff == 0 ?  Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    func changeHELOCStatus(){
        lblHomeEquity.font = switchHomeEquity.isOn ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCreditLimit.isHidden = !switchHomeEquity.isOn
        accountPaymentViewHeightConstraint.constant = switchHomeEquity.isOn ? 337 : 248
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    func saveFirstMortgageObject(){
        
        var paidAtClosing: Any = NSNull()
        var firstMortgagePayment: Any = NSNull()
        var unPaidFirstMortgagePayment: Any = NSNull()
        var creditLimit: Any = NSNull()
        
        if let isPaid = isMortgagePaidOff{
            paidAtClosing = isPaid == 1 ? true : false
        }
        
        if txtfieldMortgagePayment.text != ""{
            if let value = Double(cleanString(string: txtfieldMortgagePayment.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                firstMortgagePayment = value
            }
        }
        
        if txtfieldMortgageBalance.text != ""{
            if let value = Double(cleanString(string: txtfieldMortgageBalance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                unPaidFirstMortgagePayment = value
            }
        }
        
        if txtfieldCreditLimit.text != ""{
            if let value = Double(cleanString(string: txtfieldCreditLimit.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                creditLimit = value
            }
        }
        
        let firstMortgageDict = ["propertyTaxesIncludeinPayment": isAnnualPropertyTax,
                                 "homeOwnerInsuranceIncludeinPayment": isAnnualHomeownerInsurance,
                                 "floodInsuranceIncludeinPayment": isAnnualFloodInsurance,
                                 "paidAtClosing": paidAtClosing,
                                 "firstMortgagePayment": firstMortgagePayment,
                                 "unpaidFirstMortgagePayment": unPaidFirstMortgagePayment,
                                 "helocCreditLimit": creditLimit,
                                 "isHeloc": switchHomeEquity.isOn ? true : false] as [String: Any]
        self.delegate?.saveFirstMortageObject(firstMortgage: firstMortgageDict)
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func switchHomeEquityChanged(_ sender: UISwitch) {
        changeHELOCStatus()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        saveFirstMortgageObject()
        self.dismissVC()
    }
    
    //MARK:- API's
    
    func getMortgageData(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerPropertyId=\(borrowerPropertyId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getFirstMortgageDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let firstMortageModel = FirstMortgageModel()
                    firstMortageModel.updateModelWithJSON(json: result["data"])
                    self.mortgageDetail = firstMortageModel
                    self.setMortgageData()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
        
    }
}
