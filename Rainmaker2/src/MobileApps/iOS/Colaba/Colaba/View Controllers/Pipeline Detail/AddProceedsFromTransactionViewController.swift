//
//  AddProceedsFromTransactionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/09/2021.
//

import UIKit
import MaterialComponents

class AddProceedsFromTransactionViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var btnDelete: UIButton!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldTransactionType: ColabaTextField!
    @IBOutlet weak var txtfieldExpectedProceeds: ColabaTextField!
    @IBOutlet weak var loanSecureView: UIView!
    @IBOutlet weak var loanSecureViewTopConstraint: NSLayoutConstraint! // 40 or 0
    @IBOutlet weak var loanSecureViewHeightConstraint: NSLayoutConstraint! // 140 or 0
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldAssetsType: ColabaTextField!
    @IBOutlet weak var txtfieldAssetsTypeTopConstraint: NSLayoutConstraint! // 20 or 0
    @IBOutlet weak var txtFieldAssetsTypeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var assetsDescriptionTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isLoanSecureByAnAsset = false
    var txtViewAssetsDescription = MDCFilledTextArea()
    var borrowerName = ""
    var isForAdd = false
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerAssetId = 0
    var assetCategoryId = 0
    var assetTypeId = 0
    var loanPurposeId = 0
    var assetsCategoryArray = [AssetsCategoryModel]()
    var proceedsFromTransactionDetail = ProceedsFromTransactionDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        lblBorrowerName.text = borrowerName.uppercased()
        getAssetsCategories()
        btnDelete.isHidden = isForAdd
    }
   
    //MARK:- Methods and Actions
    func setMaterialTextFieldsAndViews(){
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        let estimatedFrame = assetsDescriptionTextViewContainer.frame
        txtViewAssetsDescription = MDCFilledTextArea(frame: estimatedFrame)
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.label.text = "Asset Description"
        txtViewAssetsDescription.textView.text = ""
        txtViewAssetsDescription.leadingAssistiveLabel.text = ""
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .normal)
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewAssetsDescription.setFilledBackgroundColor(.clear, for: .editing)
        txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewAssetsDescription.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewAssetsDescription.leadingEdgePaddingOverride = 0
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewAssetsDescription.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewAssetsDescription.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewAssetsDescription.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewAssetsDescription.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewAssetsDescription.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewAssetsDescription.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewAssetsDescription.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewAssetsDescription.textView.textColor = .black
        txtViewAssetsDescription.textView.delegate = self
        mainView.addSubview(txtViewAssetsDescription)
        
        setTextFields()
    }
    
    func setTextFields() {

        ///Transaction Type  Text Field
        txtfieldTransactionType.setTextField(placeholder: "Transaction Type", controller: self, validationType: .required)
        txtfieldTransactionType.type = .dropdown
        
        ///Expected Proceeds Text Field
        txtfieldExpectedProceeds.setTextField(placeholder: "Expected Proceeds", controller: self, validationType: .required)
        txtfieldExpectedProceeds.type = .amount
        
        ///Assets Type Text Field
        txtfieldAssetsType.setTextField(placeholder: "Which Asset?", controller: self, validationType: .required)
        txtfieldAssetsType.type = .dropdown
        txtfieldAssetsType.setDropDownDataSource(kAssetsTypeArray)
    }
    
    func setProceedsFromTransaction(){
        txtfieldExpectedProceeds.setTextField(text: String(format: "%.0f", self.proceedsFromTransactionDetail.value.rounded()))
        if (assetTypeId == 12){ //Proceeds from loan work
            isLoanSecureByAnAsset = self.proceedsFromTransactionDetail.securedByCollateral
            txtViewAssetsDescription.textView.text = self.proceedsFromTransactionDetail.collateralAssetOtherDescription
            txtViewAssetsDescription.sizeToFit()
            txtfieldAssetsType.setTextField(text: self.proceedsFromTransactionDetail.collateralAssetName)
            setAssetTypeAccordingToOption(option: self.proceedsFromTransactionDetail.collateralAssetName)
            changeLoanSecureStatus()
        }
        else{
            txtViewAssetsDescription.textView.text = self.proceedsFromTransactionDetail.descriptionField
            txtViewAssetsDescription.sizeToFit()
        }
    }
    
    func setTextFieldAccordingToTransactionType(option: String){
        txtfieldExpectedProceeds.isHidden = false
        if (option.localizedCaseInsensitiveContains("Proceeds From A Loan")){
            loanSecureView.isHidden = false
            loanSecureViewTopConstraint.constant = 40
            loanSecureViewHeightConstraint.constant = 140
            txtfieldAssetsType.isHidden = true
            txtfieldAssetsTypeTopConstraint.constant = 0
            txtFieldAssetsTypeHeightConstraint.constant = 0
            assetsDescriptionTextViewContainer.isHidden = true
            txtViewAssetsDescription.isHidden = true
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) { [weak self] in
                self?.txtViewAssetsDescription.frame = self?.assetsDescriptionTextViewContainer.frame ?? CGRect(x: 0, y: 0, width: 0, height: 0)
            }
        }
        else{
            loanSecureView.isHidden = true
            loanSecureViewTopConstraint.constant = 0
            loanSecureViewHeightConstraint.constant = 0
            txtfieldAssetsType.isHidden = true
            txtfieldAssetsTypeTopConstraint.constant = 0
            txtFieldAssetsTypeHeightConstraint.constant = 0
            assetsDescriptionTextViewContainer.isHidden = false
            txtViewAssetsDescription.isHidden = false
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) { [weak self] in
                self?.txtViewAssetsDescription.frame = self?.assetsDescriptionTextViewContainer.frame ?? CGRect(x: 0, y: 0, width: 0, height: 0)
            }
        }
        
        setScreenHeight()
    }
    
    func setAssetTypeAccordingToOption(option: String){
        assetsDescriptionTextViewContainer.isHidden = option != "Other"
        txtViewAssetsDescription.isHidden = option != "Other"
        txtViewAssetsDescription.frame = assetsDescriptionTextViewContainer.frame
        setScreenHeight()
    }
    
    func setScreenHeight(){
        UIView.animate(withDuration: 0.0) {
            self.view.layoutSubviews()
        }
    }
    
    @objc func yesStackViewTapped(){
        isLoanSecureByAnAsset = true
        txtfieldAssetsType.text = ""
        assetsDescriptionTextViewContainer.isHidden = true
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.textView.text = ""
        changeLoanSecureStatus()
    }
    
    @objc func noStackViewTapped(){
        isLoanSecureByAnAsset = false
        txtfieldAssetsType.text = ""
        assetsDescriptionTextViewContainer.isHidden = true
        txtViewAssetsDescription.isHidden = true
        txtViewAssetsDescription.textView.text = ""
        changeLoanSecureStatus()
    }
    
    func changeLoanSecureStatus(){
        btnYes.setImage(UIImage(named: isLoanSecureByAnAsset ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isLoanSecureByAnAsset ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNo.setImage(UIImage(named: !isLoanSecureByAnAsset ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = !isLoanSecureByAnAsset ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        txtfieldAssetsType.isHidden = !isLoanSecureByAnAsset
        txtfieldAssetsTypeTopConstraint.constant = isLoanSecureByAnAsset ? 20 : 0
        txtFieldAssetsTypeHeightConstraint.constant = isLoanSecureByAnAsset ? 39 : 0
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
            addUpdateProceedsFromTransaction()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldTransactionType.validate()
        if isLoanSecureByAnAsset && !txtfieldAssetsType.isHidden{
            isValidate = txtfieldAssetsType.validate() && isValidate
        }
        if !txtViewAssetsDescription.isHidden{
            isValidate = validateTextView() && isValidate
        }
//
        isValidate = txtfieldExpectedProceeds.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getAssetsCategories(){
        Utility.showOrHideLoader(shouldShow: true)
        
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
                    self.txtfieldTransactionType.setDropDownDataSource(self.assetsCategoryArray.map({$0.name}))
                    if (self.isForAdd){
                        Utility.showOrHideLoader(shouldShow: false)
                    }
                    else{
                        if let selectedTransactionType = self.assetsCategoryArray.filter({$0.id == self.assetTypeId}).first{
                            self.txtfieldTransactionType.setTextField(text: selectedTransactionType.name)
                            self.setTextFieldAccordingToTransactionType(option: selectedTransactionType.name)
                            if (selectedTransactionType.id == 12){
                                self.getProceedFromTransactionDetail(endPoint: .getProceedsFromLoan)
                            }
                            else if (selectedTransactionType.id == 13){
                                self.getProceedFromTransactionDetail(endPoint: .getProceedsFromNonRealEstateDetail)
                            }
                            else if (selectedTransactionType.id == 14){
                                self.getProceedFromTransactionDetail(endPoint: .getProceedsFromRealEstateDetail)
                            }
                        }
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
    
    func getProceedFromTransactionDetail(endPoint: EndPoint){
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)&AssetTypeId=\(assetTypeId)&borrowerAssetId=\(borrowerAssetId)"
        
        APIRouter.sharedInstance.executeAPI(type: endPoint, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let model = ProceedsFromTransactionDetailModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.proceedsFromTransactionDetail = model
                    self.setProceedsFromTransaction()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
    
    func addUpdateProceedsFromTransaction(){
       
        var endPoint: EndPoint!
        var params: [String : Any] = [:]
        
        var expectedProceeds: Any = NSNull()
        var detail: Any = NSNull()
        var assetName: Any = NSNull()
        
        if (txtfieldExpectedProceeds.text != ""){
            if let value = Double(cleanString(string: txtfieldExpectedProceeds.text!, replaceCharacters: ["$  |  ",".00", ","], replaceWith: "")){
                expectedProceeds = value
            }
        }
        if (txtViewAssetsDescription.textView.text != ""){
            detail = txtViewAssetsDescription.textView.text!
        }
        if (txtfieldAssetsType.text != ""){
            assetName = txtfieldAssetsType.text!
        }
        
        var assetCategory = 0
        
        if let selectedAssetCategory = assetsCategoryArray.filter({$0.name.localizedCaseInsensitiveContains(txtfieldTransactionType.text!)}).first{
            assetCategory = selectedAssetCategory.id
        }
        
        if (assetCategory == 13 || assetCategory == 14){
            endPoint = assetCategory == 13 ? .addUpdateProceedFromNonRealState : .addUpdateProceedFromRealState
            params = ["BorrowerAssetId": isForAdd ? 0 : proceedsFromTransactionDetail.id,
                      "LoanApplicationId": loanApplicationId,
                      "BorrowerId": borrowerId,
                      "AssetTypeId": assetCategory,
                      "AssetCategoryId": 6,
                      "Description": detail,
                      "AssetValue": expectedProceeds]
        }
        else if (assetCategory == 12){
            if (isLoanSecureByAnAsset && txtfieldAssetsType.text!.contains("Other")){
                endPoint = .addUpdateProceedFromLoanOther
                params = ["BorrowerAssetId": isForAdd ? 0 : proceedsFromTransactionDetail.id,
                          "LoanApplicationId": loanApplicationId,
                          "BorrowerId":borrowerId,
                          "AssetTypeId":12,
                          "AssetCategoryId":6,
                          "AssetValue":expectedProceeds,
                          "ColletralAssetTypeId":4,
                          "CollateralAssetDescription": detail]
            }
            else{
                
                var colletralAssetTypeId = 0
                
                if (txtfieldAssetsType.text!.localizedCaseInsensitiveContains("House")){
                    colletralAssetTypeId = 1
                }
                else if (txtfieldAssetsType.text!.localizedCaseInsensitiveContains("Automobile")){
                    colletralAssetTypeId = 2
                }
                else if (txtfieldAssetsType.text!.localizedCaseInsensitiveContains("Financial Account")){
                    colletralAssetTypeId = 3
                }
                
                endPoint = .addUpdateProceedsFromLoan
                params = ["BorrowerAssetId": isForAdd ? 0 : proceedsFromTransactionDetail.id,
                          "AssetTypeId": 12,
                          "AssetCategoryId": 6,
                          "AssetValue": expectedProceeds,
                          "ColletralAssetTypeId": isLoanSecureByAnAsset ? colletralAssetTypeId : NSNull(),
                          "SecuredByColletral": isLoanSecureByAnAsset,
                          "CollateralAssetDescription": NSNull(),
                          "LoanApplicationId": loanApplicationId,
                          "BorrowerId": borrowerId]
            }
        }
        
        APIRouter.sharedInstance.executeAPI(type: endPoint!, method: .post, params: params) { status, result, message in
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

extension AddProceedsFromTransactionViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        deleteAsset()
    }
}

extension AddProceedsFromTransactionViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        _ = validateTextView()
    }
    
    func validateTextView() -> Bool {
        do{
            let response = try txtViewAssetsDescription.textView.text.validate(type: .required)
            DispatchQueue.main.async {
                self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewAssetsDescription.leadingAssistiveLabel.text = ""
            }
            return response
        }
        catch{
            self.txtViewAssetsDescription.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewAssetsDescription.leadingAssistiveLabel.text = error.localizedDescription
            return false
        }
    }
}

extension AddProceedsFromTransactionViewController : ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldTransactionType {
            setTextFieldAccordingToTransactionType(option: option)
        }
        
        if textField == txtfieldAssetsType {
            setAssetTypeAccordingToOption(option: option)
        }
    }
}
