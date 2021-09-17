//
//  BankruptcyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit

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
    
    var isYes = true
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
    }
    
    //MARK:- Methods
    
    func setupViews(){
        
        typeView.layer.cornerRadius = 6
        typeView.layer.borderWidth = 1
        typeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        typeView.dropShadowToCollectionViewCell()
        typeView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(typeViewTapped)))
    }
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
        let vc = Utility.getBankruptcyFollowupVC()
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
        typeView.isHidden = !isYes
    }
    
    @objc func typeViewTapped(){
        let vc = Utility.getBankruptcyFollowupVC()
        self.presentVC(vc: vc)
    }
}
