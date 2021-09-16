//
//  UndisclosedBorrowerFundsFollowupQuestionsViewControllerViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import MaterialComponents

class UndisclosedBorrowerFundsFollowupQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var txtfieldAmountBorrowed: ColabaTextField!
    @IBOutlet weak var txtviewDetailContainerView: UIView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var txtViewDetail = MDCFilledTextArea()
    
    private let validation: Validation
    
    init(validation: Validation) {
        self.validation = validation
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder: NSCoder) {
        self.validation = Validation()
        super.init(coder: coder)
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
    }
    
    //MARK:- Methods and Actions
    
    func setupTextfields(){
        txtfieldAmountBorrowed.setTextField(placeholder: "Amount Borrowed")
        txtfieldAmountBorrowed.setDelegates(controller: self)
        txtfieldAmountBorrowed.setValidation(validationType: .required)
        txtfieldAmountBorrowed.setTextField(keyboardType: .numberPad)
        txtfieldAmountBorrowed.setIsValidateOnEndEditing(validate: true)
        txtfieldAmountBorrowed.setValidation(validationType: .required)
        txtfieldAmountBorrowed.type = .amount
        
        let estimatedFrame = txtviewDetailContainerView.frame
        txtViewDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewDetail.label.text = "Details"
        txtViewDetail.textView.text = ""
        txtViewDetail.leadingAssistiveLabel.text = ""
        txtViewDetail.setFilledBackgroundColor(.clear, for: .normal)
        txtViewDetail.setFilledBackgroundColor(.clear, for: .disabled)
        txtViewDetail.setFilledBackgroundColor(.clear, for: .editing)
        txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
        txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .disabled)
        txtViewDetail.setUnderlineColor(Theme.getButtonBlueColor(), for: .editing)
        txtViewDetail.leadingEdgePaddingOverride = 0
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .normal)
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .disabled)
        txtViewDetail.setFloatingLabel(Theme.getAppGreyColor(), for: .editing)
        txtViewDetail.label.font = Theme.getRubikRegularFont(size: 13)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .normal)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .editing)
        txtViewDetail.setNormalLabel(Theme.getButtonGreyTextColor(), for: .disabled)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .normal)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .editing)
        txtViewDetail.setTextColor(Theme.getAppBlackColor(), for: .disabled)
        txtViewDetail.textView.font = Theme.getRubikRegularFont(size: 15)
        txtViewDetail.leadingAssistiveLabel.font = Theme.getRubikRegularFont(size: 12)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .normal)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .editing)
        txtViewDetail.setLeadingAssistiveLabel(.red, for: .disabled)
        txtViewDetail.textView.textColor = .black
        txtViewDetail.textView.delegate = self
        mainView.addSubview(txtViewDetail)
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
    }
    
    func validate() -> Bool {

        if (!txtfieldAmountBorrowed.validate()) {
            return false
        }
        if (txtViewDetail.textView.text == ""){
            return false
        }
        return true
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldAmountBorrowed.validate()
        do{
            let assetDescription = try validation.validateAssetDescription(txtViewDetail.textView.text)
            DispatchQueue.main.async {
                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
        
        if validate(){
            self.dismissVC()
        }
    }
    
}

extension UndisclosedBorrowerFundsFollowupQuestionsViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
        do{
            let assetDescription = try validation.validateAssetDescription(txtViewDetail.textView.text)
            DispatchQueue.main.async {
                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
                self.txtViewDetail.leadingAssistiveLabel.text = ""
            }

        }
        catch{
            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
        }
    }
    
}
