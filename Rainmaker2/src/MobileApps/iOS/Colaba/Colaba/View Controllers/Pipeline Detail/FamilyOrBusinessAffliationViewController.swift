//
//  FamilyOrBusinessAffliationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 25/10/2021.
//

import UIKit

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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        setQuestionData()
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
        if questionModel.answer == "Yes"{
            isYes = true
        }
        else if (questionModel.answer == "No"){
            isYes = false
        }
        lblDetailQuestion.text = questionModel.answerDetail
        detailView.isHidden = questionModel.answerDetail == ""
        changeStatus()
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .debtCoSigner
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
        isYes = false
        changeStatus()
    }
    
    func changeStatus(){
        if let ansYes = isYes{
            btnYes.setImage(UIImage(named: ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = ansYes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: !ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblNo.font = !ansYes ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            //detailView.isHidden = !ansYes
        }
    }
    
    @objc func detailViewTapped(){
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .familyOrBusinessAffilation
        vc.questionModel = questionModel
        vc.borrowerName = "\(questionModel.firstName) \(questionModel.lastName)"
        self.presentVC(vc: vc)
    }

}
