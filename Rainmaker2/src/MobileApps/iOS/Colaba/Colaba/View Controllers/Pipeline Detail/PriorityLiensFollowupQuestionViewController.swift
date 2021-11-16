//
//  PriorityLiensFollowupQuestionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import MaterialComponents

enum DetailScreenType{
    case priorityLiens
    case familyOrBusinessAffilation
    case undisclosedMortgageApplication
    case undisclosedCreditApplication
    case debtCoSigner
    case outStandingJudgement
    case federalDebt
    case partyToLawsuit
    case titleConveyance
    case preForceClosure
    case forceClosedProperty
}

protocol GovernmentQuestionDetailControllerDelegate: AnyObject {
    func saveQuestionModel(question: GovernmentQuestionModel)
}

class PriorityLiensFollowupQuestionViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopTitle: UILabel!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtviewDetailContainerView: UIView!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var type: DetailScreenType!
    
    var txtViewDetail = MDCFilledTextArea()
    
    private let validation: Validation
    
    var questionModel = GovernmentQuestionModel()
    var borrowerName = ""
    weak var delegate: GovernmentQuestionDetailControllerDelegate?
    
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
        setupTextView()
        lblUsername.text = borrowerName.uppercased()
        txtViewDetail.textView.text = questionModel.answerDetail
        if (type == .priorityLiens){
            lblTopTitle.text = "Priority Liens"
        }
        else if (type == .familyOrBusinessAffilation){
            lblTopTitle.text = "Family or Business Affiliation"
        }
        else if (type == .undisclosedMortgageApplication){
            lblTopTitle.text = "Undisclosed Mortgage Applications"
        }
        else if (type == .undisclosedCreditApplication){
            lblTopTitle.text = "Undisclosed Credit Applications"
        }
        else if (type == .debtCoSigner){
            lblTopTitle.text = "Debt Co-signer or Guarantor"
        }
        else if (type == .outStandingJudgement){
            lblTopTitle.text = "Outstanding Judgements"
        }
        else if (type == .federalDebt){
            lblTopTitle.text = "Federal Debt Deliquency"
        }
        else if (type == .partyToLawsuit){
            lblTopTitle.text = "Party to Lawsuit"
        }
        else if (type == .titleConveyance){
            lblTopTitle.text = "Title Conveyance"
        }
        else if (type == .preForceClosure){
            lblTopTitle.text = "Pre-Foreclosure or Short Sale"
        }
        else if (type == .forceClosedProperty){
            lblTopTitle.text = "Foreclosured Property"
        }
    }

    //MARK:- Methods and Actions
    
    func setupTextView(){
        let estimatedFrame = txtviewDetailContainerView.frame
        txtViewDetail = MDCFilledTextArea(frame: estimatedFrame)
        txtViewDetail.label.text = "Details"
        txtViewDetail.textView.text = questionModel.answerDetail
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
        txtViewDetail.sizeToFit()
        mainView.addSubview(txtViewDetail)

    }
    
    func validate() -> Bool {

        if (txtViewDetail.textView.text == ""){
            return false
        }
        return true
    }
    
    func saveQuestion(){
        questionModel.answerDetail = txtViewDetail.textView.text!
        self.delegate?.saveQuestionModel(question: questionModel)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        
//        do{
//            let assetDescription = try validation.validateAssetDescription(txtViewDetail.textView.text)
//            DispatchQueue.main.async {
//                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
//                self.txtViewDetail.leadingAssistiveLabel.text = ""
//            }
//
//        }
//        catch{
//            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
//            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
//        }
        
        //if validate(){
            saveQuestion()
            self.dismissVC()
        //}
    }
}

extension PriorityLiensFollowupQuestionViewController: UITextViewDelegate{
    
    func textViewDidEndEditing(_ textView: UITextView) {
//        do{
//            let assetDescription = try validation.validateAssetDescription(txtViewDetail.textView.text)
//            DispatchQueue.main.async {
//                self.txtViewDetail.setUnderlineColor(Theme.getSeparatorNormalColor(), for: .normal)
//                self.txtViewDetail.leadingAssistiveLabel.text = ""
//            }
//
//        }
//        catch{
//            self.txtViewDetail.setUnderlineColor(Theme.getSeparatorErrorColor(), for: .normal)
//            self.txtViewDetail.leadingAssistiveLabel.text = error.localizedDescription
//        }
    }
    
}
