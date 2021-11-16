//
//  UndisclosedBorrowerFundsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

protocol UndisclosedBorrowerFundsViewControllerDelegate: AnyObject {
    func getQuestionModel(question: [String: Any], subQuestions: [String: Any])
}

class UndisclosedBorrowerFundsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var amountView: UIView!
    @IBOutlet weak var lblAmountQuestion: UILabel!
    @IBOutlet weak var lblAmount: UILabel!
    
    var isYes: Bool?
    var questionModel = GovernmentQuestionModel()
    var subQuestionModel: GovernmentQuestionModel?
    weak var delegate: UndisclosedBorrowerFundsViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        amountView.isHidden = true
        setQuestionData()
        saveQuestion()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        setQuestionData()
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        amountView.layer.cornerRadius = 6
        amountView.layer.borderWidth = 1
        amountView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        amountView.dropShadowToCollectionViewCell()
        amountView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(amountViewTapped)))
    }
    
    func setQuestionData(){
        lblQuestion.text = questionModel.question
        if questionModel.answer == "Yes"{
            isYes = true
        }
        else if questionModel.answer == "No"{
            isYes = false
        }
        changeStatus()
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        let vc = Utility.getUndisclosedBorrowerFundsFollowupQuestionsVC()
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.questionModel = subQuestionModel
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
        isYes = false
        questionModel.answer = "No"
        changeStatus()
    }
    
    func changeStatus(){
        if let yes = isYes{
            btnYes.setImage(UIImage(named: yes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = yes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: !yes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblNo.font = !yes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            if let amountQuestion = subQuestionModel{
                amountView.isHidden = !yes
                lblAmountQuestion.text = amountQuestion.question
                if let amount = Int(amountQuestion.answer){
                    lblAmount.text = amount.withCommas().replacingOccurrences(of: ".00", with: "")
                }
            }
        }
        saveQuestion()
    }
    
    @objc func amountViewTapped(){
        let vc = Utility.getUndisclosedBorrowerFundsFollowupQuestionsVC()
        vc.questionModel = subQuestionModel
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
                        "answerDetail": questionModel.answerDetail,
                        "selectionOptionId": NSNull(),
                        "answerData":NSNull()] as [String: Any]
        
        if let subQuestion = subQuestionModel{
            let question2 = ["id": subQuestion.id,
                             "parentQuestionId": subQuestion.parentQuestionId,
                             "headerText": subQuestion.headerText,
                             "questionSectionId": subQuestion.questionSectionId,
                             "ownTypeId": subQuestion.ownTypeId,
                             "firstName": subQuestion.firstName,
                             "lastName": subQuestion.lastName,
                             "question": subQuestion.question,
                             "answer": subQuestion.answer,
                             "answerDetail": subQuestion.answerDetail,
                             "selectionOptionId": NSNull(),
                             "answerData": NSNull()] as [String: Any]
            
            self.delegate?.getQuestionModel(question: question, subQuestions: question2)
        }
    }
}

extension UndisclosedBorrowerFundsViewController: UndisclosedBorrowerFundsFollowupQuestionsViewControllerDelegate{
    func saveUndisclosedFollowupQuestion(question: GovernmentQuestionModel) {
        subQuestionModel = question
        questionModel.answer = "Yes"
        changeStatus()
    }
}
