//
//  AddGiftFundsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit

class AddGiftFundsViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldGiftSource: ColabaTextField!
    @IBOutlet weak var giftTypeView: UIView!
    @IBOutlet weak var cashGiftStackView: UIStackView!
    @IBOutlet weak var btnCashGift: UIButton!
    @IBOutlet weak var lblCashGift: UILabel!
    @IBOutlet weak var giftOfEquityStackView: UIStackView!
    @IBOutlet weak var btnGiftOfEquity: UIButton!
    @IBOutlet weak var lblGiftOfEquity: UILabel!
    @IBOutlet weak var lblGiftTypeError: UILabel!
    @IBOutlet weak var txtfieldCashValue: ColabaTextField!
    @IBOutlet weak var giftDepositView: UIView!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldDate: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isCashGift = false
    var isGiftTypeSelect = false
    var isGiftDeposit = false
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerAssetId = 0
    var assetCategoryId = 0
    var loanPurposeId = 0
    var giftSourceArray = [DropDownModel]()
    var assetsCategoryArray = [AssetsCategoryModel]()
    var giftAssetDetail = GiftDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setViews()
        setTextFields()
        lblBorrowerName.text = borrowerName.uppercased()
        getAssetsCategories()
        getGiftSources()
        btnDelete.isHidden = isForAdd
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Gift Source Text Field
        txtfieldGiftSource.setTextField(placeholder: "Gift Source", controller: self, validationType: .required)
        txtfieldGiftSource.type = .dropdown
        
        ///Cash / Market Value Text Field
        txtfieldCashValue.setTextField(placeholder: "Cash Value", controller: self, validationType: .noValidation)
        txtfieldCashValue.type = .amount
        
        ///Date of Transfer / Expected Transfer Date Text Field
        txtfieldDate.setTextField(placeholder: "Date of Transfer", controller: self, validationType: .noValidation)
        txtfieldDate.type = .datePicker
        
    }
    
    func setGiftDetail(){
        if let giftSource = giftSourceArray.filter({$0.optionId == giftAssetDetail.giftSourceId}).first{
            txtfieldGiftSource.setTextField(text: giftSource.optionName)
            setTextFieldsAccordingToOptions(option: giftSource.optionName)
        }
        if let giftType = assetsCategoryArray.filter({$0.id == self.giftAssetDetail.assetTypeId}).first{
            isCashGift = giftType.name.localizedCaseInsensitiveContains("Cash Gift")
        }
        changeGiftType()
        txtfieldCashValue.setTextField(text: String(format: "%.0f", self.giftAssetDetail.value.rounded()))
        if (isCashGift){
            isGiftDeposit = self.giftAssetDetail.isDeposited
            changeGiftDepositType()
            txtfieldDate.setTextField(text: Utility.getDayMonthYear(self.giftAssetDetail.valueDate))
        }
    }
    
    func setTextFieldsAccordingToOptions(option: String){
        giftTypeView.isHidden = false
        lblGiftOfEquity.text = (option == "Relative" || option == "Unmarried Partner") ? "Gift Of Equity" : "Grant"
    }
    
    func setViews(){
        
        cashGiftStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(cashGiftStackViewTapped)))
        giftOfEquityStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(giftOfEquityStackViewTapped)))
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
 
    }
    
    @objc func cashGiftStackViewTapped(){
        isGiftTypeSelect = true
        lblGiftTypeError.isHidden = true
        isCashGift = true
        changeGiftType()
    }
    
    @objc func giftOfEquityStackViewTapped(){
        isGiftTypeSelect = true
        lblGiftTypeError.isHidden = true
        isCashGift = false
        changeGiftType()
        txtfieldDate.isHidden = true
    }
    
    func changeGiftType(){
        btnCashGift.setImage(UIImage(named: isCashGift ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblCashGift.font = isCashGift ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnGiftOfEquity.setImage(UIImage(named: !isCashGift ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblGiftOfEquity.font = !isCashGift ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCashValue.isHidden = false
        txtfieldCashValue.setTextField(placeholder: isCashGift ? "Cash Value" : "Market Value")
        giftDepositView.isHidden = !isCashGift
        
    }
    
    @objc func yesStackViewTapped(){
        isGiftDeposit = true
        changeGiftDepositType()
    }
    
    @objc func noStackViewTapped(){
        isGiftDeposit = false
        changeGiftDepositType()
    }
    
    func changeGiftDepositType(){
        btnYes.setImage(UIImage(named: isGiftDeposit ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isGiftDeposit ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: !isGiftDeposit ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = !isGiftDeposit ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldDate.isHidden = false
        txtfieldDate.setTextField(placeholder: isGiftDeposit ? "Date of Transfer" : "Expected Date of Transfer")
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnDeleteTapped(_ sender: UIButton) {
        let vc = Utility.getDeleteAddressPopupVC()
        vc.popupTitle = "Are you sure you want to remove this asset type?"
        vc.screenType = 4
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            addUpdateGiftFunds()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldGiftSource.validate()
//        if !txtfieldCashValue.isHidden {
//            isValidate = txtfieldCashValue.validate() && isValidate
//        }
//        if !txtfieldDate.isHidden {
//            isValidate = txtfieldDate.validate() && isValidate
//        }
        if (isForAdd && txtfieldGiftSource.text != ""){
            isValidate = isGiftTypeSelect
            lblGiftTypeError.isHidden = isGiftTypeSelect
        }
        return isValidate
    }
    
    //MARK:- API's
    
    func getAssetsCategories(){
        
        let extraData = "categoryId=\(assetCategoryId)&loanPurposeId=\(loanPurposeId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAssetsTypes, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = AssetsCategoryModel()
                        model.updateModelWithJSON(json: option)
                        self.assetsCategoryArray.append(model)
                    }
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func getGiftSources(){
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getGiftSources, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.giftSourceArray.append(model)
                    }
                    self.txtfieldGiftSource.setDropDownDataSource(self.giftSourceArray.map({$0.optionName}))
                    if (self.isForAdd){
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                    else{
                        self.getGiftAssetsDetail()
                    }
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func getGiftAssetsDetail(){
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)&borrowerAssetId=\(borrowerAssetId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getGiftAssetsDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = GiftDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.giftAssetDetail = model
                    self.setGiftDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateGiftFunds(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        var giftSourceId = 0
        var giftValue: Any = NSNull()
        var valueDate: Any = NSNull()
        var assetTypeId = 10
        
        if let giftSoruceType = giftSourceArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldGiftSource.text!)}).first{
            giftSourceId = giftSoruceType.optionId
        }
        
        if (isCashGift){
            assetTypeId = 10
        }
        else{
            if (lblGiftOfEquity.text!.localizedCaseInsensitiveContains("Gift of Equity")){
                assetTypeId = 26
            }
            else if (lblGiftOfEquity.text!.localizedCaseInsensitiveContains("Grant")){
                assetTypeId = 11
            }
        }
        
        if (txtfieldCashValue.text != ""){
            if let value = Double(cleanString(string: txtfieldCashValue.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                giftValue = value
            }
        }
        
        let giftDateComponent = txtfieldDate.text!.components(separatedBy: "/")
        if (giftDateComponent.count == 3){
            valueDate = "\(giftDateComponent[2])-\(giftDateComponent[0])-\(giftDateComponent[1])"
        }
        
        let params = ["Id": isForAdd ? NSNull() : giftAssetDetail.id,
                      "BorrowerId": borrowerId,
                      "AssetTypeId": assetTypeId,
                      "GiftSourceId": giftSourceId,
                      "Value": giftValue,
                      "valueDate": valueDate,
                      "Description": "",
                      "IsDeposited": isGiftDeposit,
                      "LoanApplicationId": loanApplicationId] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateGiftFunds, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
        }
        
    }

    func deleteAsset(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "AssetId=\(borrowerAssetId)&borrowerId=\(borrowerId)&loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteAsset, method: .delete, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.dismissVC()
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

extension AddGiftFundsViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteAsset()
    }
}

extension AddGiftFundsViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldGiftSource {
            setTextFieldsAccordingToOptions(option: option)
        }
    }
}
