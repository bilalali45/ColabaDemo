//
//  SecondMortgageFollowupQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 03/09/2021.
//

import UIKit
import Material

protocol SecondMortgageFollowupQuestionsViewControllerDelegate: AnyObject {
    func saveSecondMortageObject(secondMortgage: [String: Any])
}

class SecondMortgageFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldMortgagePayment: ColabaTextField!
    @IBOutlet weak var txtfieldMortgageBalance: ColabaTextField!
    @IBOutlet weak var homeEquityStackView: UIStackView!
    @IBOutlet weak var switchHomeEquity: UISwitch!
    @IBOutlet weak var lblHomeEquity: UILabel!
    @IBOutlet weak var txtfieldCreditLimit: ColabaTextField!
    @IBOutlet weak var mortgageCombinedView: UIView!
    @IBOutlet weak var mortgageCombinedViewTopConstraint: NSLayoutConstraint! //140 or 50
    @IBOutlet weak var mortgageCombinedViewHeightConstarint: NSLayoutConstraint!
    @IBOutlet weak var lblMortgageCombinedQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyPurchaseView: UIView!
    @IBOutlet weak var propertyPurchaseViewTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblPropertyPurchaseQuestion: UILabel!
    @IBOutlet weak var yesStackView2: UIStackView!
    @IBOutlet weak var btnYes2: UIButton!
    @IBOutlet weak var lblYes2: UILabel!
    @IBOutlet weak var noStackView2: UIStackView!
    @IBOutlet weak var btnNo2: UIButton!
    @IBOutlet weak var lblNo2: UILabel!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isMortgageCombine: Int?
    var isMortgageTakenWhenPropertyPurchase: Int?
    
    var loanApplicationId = 0
    var borrowerPropertyId = 0
    
    var isForRealEstate = false
    var streetAddress = ""
    var mortgageDetail: SecondMortgageModel?
    weak var delegate: SecondMortgageFollowupQuestionsViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        if (isForRealEstate){
            lblUsername.text = streetAddress
            mortgageCombinedView.isHidden = true
            mortgageCombinedViewHeightConstarint.constant = 0
            propertyPurchaseViewTopConstraint.constant = 0
        }
        changeMortgageCombineStatus()
        changeMortgageTakenWhenPropertyPurchaseStatus()
//        if (isForRealEstate && loanApplicationId > 0){
//            getMortgageData()
//        }
//        else{
//            setMortgageData()
//        }
        setMortgageData()
    }
    
    //MARK:- Methods and Actions
    
    func setMaterialTextFieldsAndViews(){
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        yesStackView.layer.cornerRadius = 8
        yesStackView.layer.borderWidth = 1
        yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        noStackView.layer.cornerRadius = 8
        noStackView.layer.borderWidth = 1
        noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor

        
        yesStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackView2Tapped)))
        yesStackView2.layer.cornerRadius = 8
        yesStackView2.layer.borderWidth = 1
        yesStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        noStackView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackView2Tapped)))
        noStackView2.layer.cornerRadius = 8
        noStackView2.layer.borderWidth = 1
        noStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        setTextFields()
    }
    
    func setTextFields() {
        ///Second Mortgage Payment Text Field
        txtfieldMortgagePayment.setTextField(placeholder: "Second Mortgage Payment", controller: self, validationType: .noValidation)
        txtfieldMortgagePayment.type = .amount
        
        ///Unpaid Second Mortgage Balance Text Field
        txtfieldMortgageBalance.setTextField(placeholder: "Unpaid Second Mortgage Balance", controller: self, validationType: .noValidation)
        txtfieldMortgageBalance.type = .amount
        
        ///Credit Limit Text Field
        txtfieldCreditLimit.setTextField(placeholder: "Credit Limit", controller: self, validationType: .noValidation)
        txtfieldCreditLimit.type = .amount
    }
    
    func setMortgageData(){
        if let secondMortage = mortgageDetail{
            txtfieldMortgagePayment.setTextField(text: String(format: "%.0f", secondMortage.secondMortgagePayment.rounded()))
            txtfieldMortgageBalance.setTextField(text: String(format: "%.0f", secondMortage.unpaidSecondMortgagePayment.rounded()))
            switchHomeEquity.isOn = secondMortage.isHeloc
            txtfieldCreditLimit.setTextField(text: String(format: "%.0f", secondMortage.helocCreditLimit.rounded()))
            isMortgageCombine = secondMortage.combineWithNewFirstMortgage == true ? 1 : 0
            isMortgageTakenWhenPropertyPurchase = secondMortage.paidAtClosing == true ? 1 : 0
            changeHELOCStatus()
            changeMortgageCombineStatus()
            changeMortgageTakenWhenPropertyPurchaseStatus()
        }
    }
    
    @objc func yesStackViewTapped(){
        isMortgageCombine = 1
        changeMortgageCombineStatus()
    }
    
    @objc func noStackViewTapped(){
        isMortgageCombine = 0
        changeMortgageCombineStatus()
    }
    
    func changeMortgageCombineStatus(){
        btnYes.setImage(UIImage(named: isMortgageCombine == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblYes.font = isMortgageCombine == 1 ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblYes.textColor = isMortgageCombine == 1 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnNo.setImage(UIImage(named: isMortgageCombine == 0 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblNo.font = isMortgageCombine == 0 ?  Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblNo.textColor = isMortgageCombine == 0 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        
        if (isMortgageCombine == 1){
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            yesStackView.dropShadowToCollectionViewCell()
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            noStackView.removeShadow()
        }
        else if (isMortgageCombine == 0){
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            noStackView.dropShadowToCollectionViewCell()
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            yesStackView.removeShadow()
        }
    }
    
    @objc func yesStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = 1
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    @objc func noStackView2Tapped(){
        isMortgageTakenWhenPropertyPurchase = 0
        changeMortgageTakenWhenPropertyPurchaseStatus()
    }
    
    func changeMortgageTakenWhenPropertyPurchaseStatus(){
        btnYes2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblYes2.font = isMortgageTakenWhenPropertyPurchase == 1 ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblYes2.textColor = isMortgageTakenWhenPropertyPurchase == 1 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnNo2.setImage(UIImage(named: isMortgageTakenWhenPropertyPurchase == 0 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblNo2.font = isMortgageTakenWhenPropertyPurchase == 0 ?  Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblNo2.textColor = isMortgageTakenWhenPropertyPurchase == 0 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        
        if (isMortgageTakenWhenPropertyPurchase == 1){
            yesStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            yesStackView2.dropShadowToCollectionViewCell()
            noStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            noStackView2.removeShadow()
        }
        else if (isMortgageTakenWhenPropertyPurchase == 0){
            noStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            noStackView2.dropShadowToCollectionViewCell()
            yesStackView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            yesStackView2.removeShadow()
        }
        
    }
    
    func changeHELOCStatus(){
        lblHomeEquity.font = switchHomeEquity.isOn ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldCreditLimit.isHidden = !switchHomeEquity.isOn
        mortgageCombinedViewTopConstraint.constant = switchHomeEquity.isOn ? 140 : 50
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    func saveSecondMortgageObject(){
        
        var secondMortgagePayment: Any = NSNull()
        var unPaidSecondMortgagePayment: Any = NSNull()
        var creditLimit: Any = NSNull()
        var mortgageCombine: Any = NSNull()
        var mortgagePaidOff: Any = NSNull()
        
        if txtfieldMortgagePayment.text != ""{
            if let value = Double(cleanString(string: txtfieldMortgagePayment.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                secondMortgagePayment = value
            }
        }
        
        if txtfieldMortgageBalance.text != ""{
            if let value = Double(cleanString(string: txtfieldMortgageBalance.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                unPaidSecondMortgagePayment = value
            }
        }
        
        if txtfieldCreditLimit.text != ""{
            if let value = Double(cleanString(string: txtfieldCreditLimit.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                creditLimit = value
            }
        }
        
        if let isCombine = isMortgageCombine{
            mortgageCombine = isCombine == 1 ? true : false
        }
        
        if let isMortgagePaid = isMortgageTakenWhenPropertyPurchase{
            mortgagePaidOff = isMortgagePaid == 1 ? true : false
        }
        
        let secondMortgageDict =  isForRealEstate ? ["secondMortgagePayment": secondMortgagePayment,
                                                     "unpaidSecondMortgagePayment": unPaidSecondMortgagePayment,
                                                     "helocCreditLimit": creditLimit,
                                                     "isHeloc": switchHomeEquity.isOn ? true : false,
                                                     "state": NSNull(),
                                                     "paidAtClosing": mortgagePaidOff] as [String: Any] :
                                  ["secondMortgagePayment": secondMortgagePayment,
                                  "unpaidSecondMortgagePayment": unPaidSecondMortgagePayment,
                                  "helocCreditLimit": creditLimit,
                                  "isHeloc": switchHomeEquity.isOn ? true : false,
                                  "combineWithNewFirstMortgage": mortgageCombine,
                                  "wasSmTaken": mortgagePaidOff] as [String: Any]
        
        self.delegate?.saveSecondMortageObject(secondMortgage: secondMortgageDict)
    }
    
    //MARK:- API's
    
    func getMortgageData(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerPropertyId=\(borrowerPropertyId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getSecondMortgageDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let secondMortageModel = SecondMortgageModel()
                    secondMortageModel.updateModelWithJSON(json: result["data"])
                    self.mortgageDetail = secondMortageModel
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
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func switchHomeEquityChanged(_ sender: UISwitch) {
        changeHELOCStatus()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        saveSecondMortgageObject()
        self.dismissVC()
    }
}
