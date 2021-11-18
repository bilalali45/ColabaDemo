//
//  ChildSupportViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit

protocol ChildSupportViewControllerDelegate: AnyObject {
    func getChildSupportQuestionModel(question: [String: Any])
}

class ChildSupportViewController: BaseViewController {

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
    @IBOutlet weak var detailView2: UIView!
    @IBOutlet weak var lblDetailQuestion2: UILabel!
    @IBOutlet weak var lblAns2: UILabel!
    @IBOutlet weak var detailView3: UIView!
    @IBOutlet weak var lblDetailQuestion3: UILabel!
    @IBOutlet weak var lblAns3: UILabel!
    
    var isYes: Bool?
    var questionModel = GovernmentQuestionModel()
    weak var delegate: ChildSupportViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        detailView.isHidden = true
        detailView2.isHidden = true
        detailView3.isHidden = true
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
        
        detailView2.layer.cornerRadius = 6
        detailView2.layer.borderWidth = 1
        detailView2.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        detailView2.dropShadowToCollectionViewCell()
        detailView2.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(detailViewTapped)))
        
        detailView3.layer.cornerRadius = 6
        detailView3.layer.borderWidth = 1
        detailView3.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        detailView3.dropShadowToCollectionViewCell()
        detailView3.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(detailViewTapped)))
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
        let vc = Utility.getChildSupportFollowupQuestionsVC()
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.questionModel = questionModel
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
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
            if ansYes{
                detailView.isHidden = !(questionModel.answerData.count > 0)
                detailView2.isHidden = !(questionModel.answerData.count > 1)
                detailView3.isHidden = !(questionModel.answerData.count > 2)
                
                if questionModel.answerData.count > 0{
                    lblDetailQuestion.text = questionModel.answerData[0].liabilityName
                    lblAns.text = questionModel.answerData[0].monthlyPayment.withCommas().replacingOccurrences(of: ".00", with: "")
                }
                if questionModel.answerData.count > 1{
                    lblDetailQuestion2.text = questionModel.answerData[1].liabilityName
                    lblAns2.text = questionModel.answerData[1].monthlyPayment.withCommas().replacingOccurrences(of: ".00", with: "")
                }
                if questionModel.answerData.count > 2{
                    lblDetailQuestion3.text = questionModel.answerData[2].liabilityName
                    lblAns3.text = questionModel.answerData[2].monthlyPayment.withCommas().replacingOccurrences(of: ".00", with: "")
                }
            }
            else{
                detailView.isHidden = true
                detailView2.isHidden = true
                detailView3.isHidden = true
            }
        }
        saveQuestion()
    }
    
    @objc func detailViewTapped(){
        let vc = Utility.getChildSupportFollowupQuestionsVC()
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        vc.questionModel = questionModel
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    func saveQuestion(){
        
        var answerData: [Any] = []
        
        if let childSupport = questionModel.answerData.filter({$0.liabilityName.localizedCaseInsensitiveContains("Child Support")}).first{
            let data = ["liabilityTypeId": 1,
                        "liabilityName": childSupport.liabilityName,
                        "remainingMonth": childSupport.remainingMonth,
                        "monthlyPayment": childSupport.monthlyPayment,
                        "name": childSupport.name] as [String: Any]
            answerData.append(data)
        }
        
        if let alimony = questionModel.answerData.filter({$0.liabilityName.localizedCaseInsensitiveContains("Alimony")}).first{
            let data = ["liabilityTypeId": 8,
                        "liabilityName": alimony.liabilityName,
                        "remainingMonth": alimony.remainingMonth,
                        "monthlyPayment": alimony.monthlyPayment,
                        "name": alimony.name] as [String: Any]
            answerData.append(data)
        }
        
        if let separate = questionModel.answerData.filter({$0.liabilityName.localizedCaseInsensitiveContains("Separate Maintenance")}).first{
            let data = ["liabilityTypeId": 2,
                        "liabilityName": separate.liabilityName,
                        "remainingMonth": separate.remainingMonth,
                        "monthlyPayment": separate.monthlyPayment,
                        "name": separate.name] as [String: Any]
            answerData.append(data)
        }
        
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
                        "answerData": answerData.count == 0 ? NSNull() : answerData] as [String: Any]
        self.delegate?.getChildSupportQuestionModel(question: question)
    }
}

extension ChildSupportViewController: ChildSupportFollowupQuestionsViewControllerDelegate{
    func saveQuestion(childSupport: GovernmentQuestionModel) {
        questionModel = childSupport
        questionModel.answer = "Yes"
        changeStatus()
    }
}
