//
//  OwnershipInterestInPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

class OwnershipInterestInPropertyViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyTypeView: UIView!
    @IBOutlet weak var lblPropertyQuestion: UILabel!
    @IBOutlet weak var lblPropertyType: UILabel!
    @IBOutlet weak var propertyTitleView: UIView!
    @IBOutlet weak var lblPropertyTitleQuestion: UILabel!
    @IBOutlet weak var lblPropertyTitleAnswer: UILabel!
    
    var isYes: Bool?
    var questionModel = GovernmentQuestionModel()
    var subQuestions: [GovernmentQuestionModel]?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyTypeView.isHidden = true
        propertyTitleView.isHidden = true
        setQuestionData()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        setQuestionData()
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        propertyTypeView.layer.cornerRadius = 6
        propertyTypeView.layer.borderWidth = 1
        propertyTypeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        propertyTypeView.dropShadowToCollectionViewCell()
        propertyTypeView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyTypeViewTapped)))
        
        propertyTitleView.layer.cornerRadius = 6
        propertyTitleView.layer.borderWidth = 1
        propertyTitleView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        propertyTitleView.dropShadowToCollectionViewCell()
        propertyTitleView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyTypeViewTapped)))
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
        let vc = Utility.getOwnershipInterestInPropertyFollowupQuestionVC()
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
        isYes = false
        changeStatus()
    }
    
    func changeStatus(){
        if let yes = isYes{
            btnYes.setImage(UIImage(named: yes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = yes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: !yes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblNo.font = !yes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            if let questions = subQuestions{
                propertyTypeView.isHidden = !yes
                propertyTitleView.isHidden = !yes
                lblPropertyQuestion.text = questions.filter({$0.question.localizedCaseInsensitiveContains("What type of property did you own?")}).first?.question
                lblPropertyType.text = questions.filter({$0.question.localizedCaseInsensitiveContains("What type of property did you own?")}).first?.answer
                lblPropertyTitleQuestion.text = questions.filter({$0.question.localizedCaseInsensitiveContains("How did you hold title to the property?")}).first?.question
                lblPropertyTitleAnswer.text = questions.filter({$0.question.localizedCaseInsensitiveContains("How did you hold title to the property?")}).first?.answer
            }
        }
    }
    
    @objc func propertyTypeViewTapped(){
        let vc = Utility.getOwnershipInterestInPropertyFollowupQuestionVC()
        vc.questions = subQuestions
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        self.presentVC(vc: vc)
    }
}
