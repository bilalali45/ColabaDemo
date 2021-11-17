//
//  BankruptcyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit

protocol BankruptcyViewControllerDelegate: AnyObject {
    func getBankruptcyQuestionModel(question: [String: Any], subQuestion: [String: Any])
}

class BankruptcyViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var typeView: UIView!
    @IBOutlet weak var lblBankruptcyQuestion: UILabel!
    @IBOutlet weak var lblBankruptcyType: UILabel!
    
    var isYes: Bool?
    var questionModel = GovernmentQuestionModel()
    var subQuestionModel: GovernmentQuestionModel?
    weak var delegate: BankruptcyViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        typeView.isHidden = true
        setQuestionData()
        saveQuestion()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        setQuestionData()
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        typeView.layer.cornerRadius = 6
        typeView.layer.borderWidth = 1
        typeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        typeView.dropShadowToCollectionViewCell()
        typeView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(typeViewTapped)))
    }
    
    func setQuestionData(){
        lblQuestion.text = questionModel.question
        if questionModel.answer == "Yes"{
            isYes = true
        }
        else if (questionModel.answer == "No"){
            isYes = false
        }
        changeStatus()
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getBankruptcyFollowupVC()
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
            if let typeQuestion = subQuestionModel{
                if (yes && typeQuestion.childSupportTypes.count > 0){
                    typeView.isHidden = false
                }
                else{
                    typeView.isHidden = true
                }
                lblBankruptcyQuestion.text = typeQuestion.question
                lblBankruptcyType.text = typeQuestion.childSupportTypes.joined(separator: ", ")
            }
        }
        saveQuestion()
    }
    
    @objc func typeViewTapped(){
        let vc = Utility.getBankruptcyFollowupVC()
        vc.questionModel = subQuestionModel
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    func saveQuestion(){
        let mainQuestion = ["id": questionModel.id,
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
                            "answerData": NSNull()] as [String: Any]
        
        var subQuestion: [String: Any] = [:]
        
        if let typeQuestion = subQuestionModel{
            var bankruptcyTypes: [Any] = []
            
            if (typeQuestion.childSupportTypes.contains("Chapter 7")){
                let type = ["1": "Chapter 7"]
                bankruptcyTypes.append(type)
            }
            if (typeQuestion.childSupportTypes.contains("Chapter 11")){
                let type = ["2": "Chapter 11"]
                bankruptcyTypes.append(type)
            }
            if (typeQuestion.childSupportTypes.contains("Chapter 12")){
                let type = ["3": "Chapter 12"]
                bankruptcyTypes.append(type)
            }
            if (typeQuestion.childSupportTypes.contains("Chapter 13")){
                let type = ["4": "Chapter 13"]
                bankruptcyTypes.append(type)
            }
            
            subQuestion = ["id": typeQuestion.id,
                           "parentQuestionId": typeQuestion.parentQuestionId,
                           "headerText": typeQuestion.headerText,
                           "questionSectionId": typeQuestion.questionSectionId,
                           "ownTypeId": typeQuestion.ownTypeId,
                           "firstName": typeQuestion.firstName,
                           "lastName": typeQuestion.lastName,
                           "question": typeQuestion.question,
                           "answer": bankruptcyTypes.count == 0 ? NSNull() : "Yes",
                           "answerDetail": typeQuestion.answerDetail,
                           "selectionOptionId": NSNull(),
                           "answerData": bankruptcyTypes.count == 0 ? NSNull() : bankruptcyTypes]
            
            self.delegate?.getBankruptcyQuestionModel(question: mainQuestion, subQuestion: subQuestion)
        }
        
    }
}

extension BankruptcyViewController: BankruptcyFollowupViewControllerDelegate{
    func saveQuestion(bankruptcyTypeQuestion: GovernmentQuestionModel) {
        subQuestionModel = bankruptcyTypeQuestion
        questionModel.answer = "Yes"
        changeStatus()
    }
}
