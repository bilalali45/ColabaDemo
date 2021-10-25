//
//  UndisclosedBorrowerFundsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        amountView.isHidden = true
        setQuestionData()
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
        else{
            isYes = false
        }
        changeStatus()
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        let vc = Utility.getUndisclosedBorrowerFundsFollowupQuestionsVC()
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
            if let amountQuestion = subQuestionModel{
                amountView.isHidden = !yes
                lblAmountQuestion.text = amountQuestion.question
                if let amount = Int(amountQuestion.answer){
                    lblAmount.text = amount.withCommas().replacingOccurrences(of: ".00", with: "")
                }
                
            }
            
        }
    }
    
    @objc func amountViewTapped(){
        let vc = Utility.getUndisclosedBorrowerFundsFollowupQuestionsVC()
        vc.questionModel = subQuestionModel
        self.presentVC(vc: vc)
    }
}
