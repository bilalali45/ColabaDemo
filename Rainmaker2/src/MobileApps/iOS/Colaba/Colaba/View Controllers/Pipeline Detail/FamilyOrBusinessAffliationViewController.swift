//
//  FamilyOrBusinessAffliationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 25/10/2021.
//

import UIKit

protocol FamilyOrBusinessAffliationViewControllerDelegate: AnyObject {
    func getFamilyOrBusinessQuestionModel(question: [String: Any])
}

class FamilyOrBusinessAffliationViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var detailView: UIView!
    @IBOutlet weak var lblDetailQuestion: UILabel!
    @IBOutlet weak var lblAns: UILabel!
    
    var isYes: Bool?
    var questionModel = GovernmentQuestionModel()
    weak var delegate: FamilyOrBusinessAffliationViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        setQuestionData()
        saveQuestion()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        setQuestionData()
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        detailView.layer.cornerRadius = 6
        detailView.layer.borderWidth = 1
        detailView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        detailView.dropShadowToCollectionViewCell()
        detailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(detailViewTapped)))
    }
    
    func setQuestionData(){
        lblQuestion.text = questionModel.question
        if questionModel.isAffiliatedWithSeller{
            isYes = true
        }
        else{
            isYes = false
        }
        lblAns.text = questionModel.answerDetail
        detailView.isHidden = questionModel.answerDetail == ""
        changeStatus()
    }
    
    @objc func yesStackViewTapped(){
        questionModel.isAffiliatedWithSeller = true
        questionModel.answer = "Yes"
        isYes = true
        changeStatus()
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .debtCoSigner
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.questionModel = questionModel
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
        questionModel.isAffiliatedWithSeller = false
        isYes = false
        questionModel.answer = "No"
        changeStatus()
    }
    
    func changeStatus(){
        if let ansYes = isYes{
            btnYes.setImage(UIImage(named: ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = ansYes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: !ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblNo.font = !ansYes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            detailView.isHidden = questionModel.answerDetail == ""
            lblAns.text = questionModel.answerDetail
        }
        saveQuestion()
    }
    
    @objc func detailViewTapped(){
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .familyOrBusinessAffilation
        vc.questionModel = questionModel
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    func saveQuestion(){
        let question = ["id": questionModel.id,
                        "parentQuestionId": NSNull(),
                        "headerText": questionModel.headerText,
                        "questionSectionId": questionModel.questionSectionId,
                        "ownTypeId": questionModel.ownTypeId,
                        "firstName": questionModel.firstName,
                        "lastName": questionModel.lastName,
                        "question": questionModel.question,
                        "answer": questionModel.answer,
                        "answerDetail":questionModel.answerDetail,
                        "selectionOptionId": NSNull(),
                        "answerData": [
                            "IsAffiliatedWithSeller": isYes == nil ? false : isYes!,
                            "AffiliationDescription": questionModel.answerDetail]] as [String: Any]
        self.delegate?.getFamilyOrBusinessQuestionModel(question: question)
    }

}

extension FamilyOrBusinessAffliationViewController: GovernmentQuestionDetailControllerDelegate{
    func saveQuestionModel(question: GovernmentQuestionModel) {
        questionModel = question
        changeStatus()
    }
}
