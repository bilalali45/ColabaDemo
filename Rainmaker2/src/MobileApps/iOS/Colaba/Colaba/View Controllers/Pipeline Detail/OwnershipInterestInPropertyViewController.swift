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
    
    var isYes: Bool?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyTypeView.isHidden = true
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        propertyTypeView.layer.cornerRadius = 6
        propertyTypeView.layer.borderWidth = 1
        propertyTypeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        propertyTypeView.dropShadowToCollectionViewCell()
        propertyTypeView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyTypeViewTapped)))
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getOwnershipInterestInPropertyFollowupQuestionVC()
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
            propertyTypeView.isHidden = !yes
        }
    }
    
    @objc func propertyTypeViewTapped(){
        let vc = Utility.getOwnershipInterestInPropertyFollowupQuestionVC()
        self.presentVC(vc: vc)
    }
}
