//
//  PriorityLiensViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

class PriorityLiensViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var priorityLiensView: UIView!
    @IBOutlet weak var lblPriorityQuestion: UILabel!
    @IBOutlet weak var lblAns: UILabel!
    
    var isYes = true
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        priorityLiensView.layer.cornerRadius = 6
        priorityLiensView.layer.borderWidth = 1
        priorityLiensView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        priorityLiensView.dropShadowToCollectionViewCell()
        priorityLiensView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(priorityLiensViewTapped)))
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        self.presentVC(vc: vc)
    }
    
    @objc func noStackViewTapped(){
        isYes = false
        changeStatus()
    }
    
    func changeStatus(){
        btnYes.setImage(UIImage(named: isYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isYes ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnNo.setImage(UIImage(named: !isYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = !isYes ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        priorityLiensView.isHidden = !isYes
    }
    
    @objc func priorityLiensViewTapped(){
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        self.presentVC(vc: vc)
    }
}
