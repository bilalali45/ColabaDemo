//
//  UndisclosedCreditApplicationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit

class UndisclosedCreditApplicationViewController: BaseViewController {

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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
    }
    
    func setupViews(){
        
        detailView.layer.cornerRadius = 6
        detailView.layer.borderWidth = 1
        detailView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        detailView.dropShadowToCollectionViewCell()
        detailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(detailViewTapped)))
    }
    
    //MARK:- Methods
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .undisclosedCreditApplication
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
            if let yes = isYes{
                detailView.isHidden = !yes
            }
            
        }
    }
    
    @objc func detailViewTapped(){
        let vc = Utility.getPriorityLiensFollowupQuestionViewController()
        vc.type = .undisclosedCreditApplication
        self.presentVC(vc: vc)
    }
    
}
