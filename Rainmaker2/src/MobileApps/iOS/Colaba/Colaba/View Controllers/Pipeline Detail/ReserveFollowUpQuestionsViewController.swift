//
//  ReserveFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit

protocol ReserveFollowUpQuestionsViewControllerDelegate: AnyObject {
    func saveReserveNationalGuard(status: String)
}

class ReserveFollowUpQuestionsViewController: BaseViewController {

    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isActive = 0 // 1 for yes 2 for no
    var selectedMilitary = Detail()
    var borrowerName = ""
    weak var delegate: ReserveFollowUpQuestionsViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        yesStackView.layer.cornerRadius = 8
        yesStackView.layer.borderWidth = 1
        yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        noStackView.layer.cornerRadius = 8
        noStackView.layer.borderWidth = 1
        noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        isActive = selectedMilitary.reserveEverActivated == true ? 1 : 2
        if (borrowerName == " "){
            lblQuestion.text = "Was he ever activated during their tour of duty?"
        }
        else{
            lblQuestion.text = "Was \(borrowerName) ever activated during their tour of duty?"
        }
        
        lblBorrowerName.text = borrowerName.uppercased()
        isActive = selectedMilitary.reserveEverActivated == true ? 1 : 2
        changeActiveStatus()
    }

    //MARK:- Methods and Actions
    
    @objc func yesStackViewTapped(){
        isActive = 1
        changeActiveStatus()
    }
    
    @objc func noStackViewTapped(){
        isActive = 2
        changeActiveStatus()
    }
    
    func changeActiveStatus(){
        btnYes.setImage(UIImage(named: isActive == 1 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblYes.font = isActive == 1 ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblYes.textColor = isActive == 1 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnNo.setImage(UIImage(named: isActive == 2 ? "RadioButtonSelected" : "radioUnslected"), for: .normal)
        lblNo.font = isActive == 2 ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblNo.textColor = isActive == 2 ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        
        if (isActive == 1){
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            yesStackView.dropShadowToCollectionViewCell()
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            noStackView.removeShadow()
        }
        else{
            noStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            noStackView.dropShadowToCollectionViewCell()
            yesStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            yesStackView.removeShadow()
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.delegate?.saveReserveNationalGuard(status: isActive == 1 ? "Yes" : "No")
        self.dismissVC()
    }
}
