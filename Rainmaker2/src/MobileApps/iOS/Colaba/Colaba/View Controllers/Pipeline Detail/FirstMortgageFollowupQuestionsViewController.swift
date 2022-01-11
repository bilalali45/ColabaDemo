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
    @IBOutlet weak var accountPaymentViewHeightConstraint: NSLayoutConstraint! // 382 or 292
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
//        if (isForRealEstate && loanApplicationId > 0){
//           getMortgageData()
//        }
//        else{
//            setMortgageData()
//        }
        setMortgageData()
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(){

        annualFloodInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualFloodInsuranceStackViewTapped)))
        annualFloodInsuranceStackView.layer.cornerRadius = 8
        annualFloodInsuranceStackView.layer.borderWidth = 1
        annualFloodInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        annualPropertyTaxesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualPropertyTaxStackViewTapped)))
        annualPropertyTaxesStackView.layer.cornerRadius = 8
        annualPropertyTaxesStackView.layer.borderWidth = 1
        annualPropertyTaxesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        annualHomeownerInsuranceStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(annualHomeownerInsuranceStackViewTapped)))
        annualHomeownerInsuranceStackView.layer.cornerRadius = 8
        annualHomeownerInsuranceStackView.layer.borderWidth = 1
        annualHomeownerInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        yesStackView.layer.cornerRadius = 8
        yesStackView.layer.borderWidth = 1
        yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        noStackView.layer.cornerRadius = 8
        noStackView.layer.borderWidth = 1
        noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
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
        lblAnnualFloodInsurance.font = isAnnualFloodInsurance ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblAnnualFloodInsurance.textColor = isAnnualFloodInsurance ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnAnnualTaxes.setImage(UIImage(named: isAnnualPropertyTax ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualTaxes.font = isAnnualPropertyTax ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblAnnualTaxes.textColor = isAnnualPropertyTax ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnAnnualHomeownerInsurance.setImage(UIImage(named: isAnnualHomeownerInsurance ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblAnnualHomeownerInsurance.font = isAnnualHomeownerInsurance ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblAnnualHomeownerInsurance.textColor = isAnnualHomeownerInsurance ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        
        if (isAnnualFloodInsurance){
            annualFloodInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            annualFloodInsuranceStackView.dropShadowToCollectionViewCell()
        }
        else{
            annualFloodInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            annualFloodInsuranceStackView.removeShadow()
        }
        
        if (isAnnualPropertyTax){
            annualPropertyTaxesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            annualPropertyTaxesStackView.dropShadowToCollectionViewCell()
        }
        else{
            annualPropertyTaxesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            annualPropertyTaxesStackView.removeShadow()
        }
        
        if (isAnnualHomeownerInsurance){
            annualHomeownerInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            annualHomeownerInsuranceStackView.dropShadowToCollectionViewCell()
        }
        else{
            annualHomeownerInsuranceStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            annualHomeownerInsuranceStackView.removeShadow()
        }
        
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
        btnYes.setImage(UIImage(named: isMortgagePaidOff == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblYes.font = isMortgagePaidOff == 1 ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblYes.textColor = isMortgagePaidOff == 1 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnNo.setImage(UIImage(named: isMortgagePaidOff == 0 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblNo.font = isMortgagePaidOff == 0 ?  Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblNo.textColor = isMortgagePaidOff == 0 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        
        if (isMortgagePaidOff == 1){
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            yesStackView.dropShadowToCollectionViewCell()
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            noStackView.removeShadow()
        }
        else if (isMortgagePaidOff == 0){
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            noStackView.dropShadowToCollectionViewCell()
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            yesStackView.removeShadow()
        }
    }
    
    func changeHELOCStatus(){
        lblHomeEquity.font = switchHomeEquity.isOn ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCreditLimit.isHidden = !switchHomeEquity.isOn
        accountPaymentViewHeightConstraint.constant = switchHomeEquity.isOn ? 382 : 292
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
