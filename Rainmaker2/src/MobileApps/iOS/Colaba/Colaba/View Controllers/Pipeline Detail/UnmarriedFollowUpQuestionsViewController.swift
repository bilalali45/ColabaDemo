//
//  UnmarriedFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/08/2021.
//

import UIKit
import MaterialComponents

protocol UnmarriedFollowUpQuestionsViewControllerDelegate: AnyObject {
    func saveUnmarriedStatus(status: MaritalStatus)
}

class UnmarriedFollowUpQuestionsViewController: BaseViewController {

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
    @IBOutlet weak var txtfieldTypeOfRelation: ColabaTextField!
    @IBOutlet weak var txtfieldState: ColabaTextField!
    @IBOutlet weak var relationshipDetailTextViewContainer: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isNonLegalSpouse = 0 // 1 for yes 2 for no
    var txtViewRelationshipDetail = MDCFilledTextArea()
    var relationshipTypeArray = [DropDownModel]()
    var statesArray = [StatesModel]()
    var borrowerName = ""
    var selectedMaritalStatus = MaritalStatus()
    weak var delegate: UnmarriedFollowUpQuestionsViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setMaterialTextFieldsAndViews()
        isNonLegalSpouse = selectedMaritalStatus.isInRelationship == true ? 1 : 2
        changeNonLegalSpouseStatus()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        lblBorrowerName.text = borrowerName.uppercased()
        getStatesDropDown()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
    }

    //MARK:- Methods and Actions
    func setMaterialTextFieldsAndViews(){
        
        let estimatedFrame = relationshipDetailTextViewContainer.frame
        txtViewRelationshipDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewRelationshipDetail.isHidden = true
        txtViewRelationshipDetail.label.text = "Relationship Details"
        txtViewRelationshipDetail.textView.text = ""
        txtViewRelationshipDetail.leadingAssistiveLabel.text = ""
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .normal)
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewRelationshipDetail.setFilledBackgroundColor(.clear, for: .editing)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewRelationshipDetail.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewRelationshipDetail.leadingEdgePaddingOverride = 0
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewRelationshipDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewRelationshipDetail.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewRelationshipDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewRelationshipDetail.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewRelationshipDetail.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewRelationshipDetail.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewRelationshipDetail.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewRelationshipDetail.textView.textColor = .black
        txtViewRelationshipDetail.textView.delegate = self
        mainView.addSubview(txtViewRelationshipDetail)
        
        setTextFields()
    }
    
    func setTextFields() {
        ///Type of Relationship Text Field
        txtfieldTypeOfRelation.setTextField(placeholder: "Type of Relationship", controller: self, validationType: .required)
        txtfieldTypeOfRelation.type = .dropdown
        txtfieldTypeOfRelation.setDropDownDataSource(relationshipTypeArray.map({$0.optionName}))

        ///State Text Field
        txtfieldState.setTextField(placeholder: "In what state was this relationship formed", controller: self, validationType: .required)
        txtfieldState.type = .editableDropdown
    }
    
    @objc func yesStackViewTapped(){
        isNonLegalSpouse = 1
        changeNonLegalSpouseStatus()
    }
    
    @objc func noStackViewTapped(){
        isNonLegalSpouse = 2
        changeNonLegalSpouseStatus()
    }
    
    func changeNonLegalSpouseStatus(){
        btnYes.setImage(UIImage(named: isNonLegalSpouse == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isNonLegalSpouse == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isNonLegalSpouse == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isNonLegalSpouse == 2 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        txtfieldTypeOfRelation.isHidden = isNonLegalSpouse != 1
        txtfieldState.isHidden = isNonLegalSpouse != 1
        
        if let selectedRelationshipType = relationshipTypeArray.filter({$0.optionId == selectedMaritalStatus.relationshipTypeId}).first{
            txtfieldTypeOfRelation.setTextField(text: selectedRelationshipType.optionName)
        }
        if let selectedState = statesArray.filter({$0.id == selectedMaritalStatus.relationFormedStateId}).first{
            txtfieldState.setTextField(text: selectedState.name)
        }
        txtViewRelationshipDetail.textView.text = selectedMaritalStatus.otherRelationshipExplanation
        
        if (isNonLegalSpouse == 1){
            relationshipDetailTextViewContainer.isHidden = txtfieldTypeOfRelation.text != "Other"
            txtViewRelationshipDetail.isHidden = txtfieldTypeOfRelation.text != "Other"
        }
        else{
            relationshipDetailTextViewContainer.isHidden = true
            txtViewRelationshipDetail.isHidden = true
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        
        if isNonLegalSpouse == 1 {
            if validate() {
                saveUnmarriedAbbendum()
            }
        }
        else{
            self.saveUnmarriedAbbendum()
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldTypeOfRelation.validate()
        isValidate = txtfieldState.validate() && isValidate
        if (txtfieldTypeOfRelation.text == "Other") {
            isValidate = validateTextView() && isValidate
        }
        return isValidate
    }
    
    func saveUnmarriedAbbendum(){
        selectedMaritalStatus.isInRelationship = isNonLegalSpouse == 1
        selectedMaritalStatus.maritalStatusId = 9
        
        if (isNonLegalSpouse == 1){
            
            var relationshipTypeId = 0
            var stateId = 0
            
            if let selectedRelationship = relationshipTypeArray.filter({$0.optionName == txtfieldTypeOfRelation.text!}).first{
                relationshipTypeId = selectedRelationship.optionId
            }
            
            if let selectedState = statesArray.filter({$0.name == txtfieldState.text!}).first{
                stateId = selectedState.id
            }
            selectedMaritalStatus.relationshipTypeId = relationshipTypeId
            selectedMaritalStatus.relationFormedStateId = stateId
            selectedMaritalStatus.otherRelationshipExplanation = txtViewRelationshipDetail.textView.text!
            
        }
        self.delegate?.saveUnmarriedStatus(status: selectedMaritalStatus)
        self.dismissVC()
    }
    
    //MARK:- API's
    
    func getStatesDropDown(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllStates, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let statesArray = result.arrayValue
                    for state in statesArray{
                        let model = StatesModel()
                        model.updateModelWithJSON(json: state)
                        self.statesArray.append(model)
                    }
                    self.txtfieldState.setDropDownDataSource(self.statesArray.map{$0.name})
                    if let selectedState = self.statesArray.filter({$0.id == self.selectedMaritalStatus.relationFormedStateId}).first{
                        self.txtfieldState.setTextField(text: selectedState.name)
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
}

extension UnmarriedFollowUpQuestionsViewController : ColabaTextFieldDelegate {

    func selectedOption(option: String, atIndex: Int, textField : ColabaTextField) {
        if textField == txtfieldTypeOfRelation {
            relationshipDetailTextViewContainer.isHidden = (option != "Other")
            txtViewRelationshipDetail.isHidden = (option != "Other")
        }
    }
}

extension UnmarriedFollowUpQuestionsViewController: UITextViewDelegate{
    func textViewDidEndEditing(_ textView: UITextView) {
        _ = validateTextView()
    }
    
    func validateTextView() -> Bool {
        do{
            let response = try txtViewRelationshipDetail.textView.text.validate(type: .required)
            DispatchQueue.main.async {
                self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewRelationshipDetail.leadingAssistiveLabel.text = ""
            }
            return response
        }
        catch{
            self.txtViewRelationshipDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewRelationshipDetail.leadingAssistiveLabel.text = error.localizedDescription
            return false
        }
    }
}
