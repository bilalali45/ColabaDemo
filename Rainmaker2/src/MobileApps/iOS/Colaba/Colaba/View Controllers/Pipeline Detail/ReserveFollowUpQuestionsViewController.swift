//
//  ReserveFollowUpQuestionsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit

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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        isActive = selectedMilitary.reserveEverActivated == true ? 1 : 2
        lblQuestion.text = "Was \(borrowerName) ever activated during their tour of duty?"
        lblBorrowerName.text = borrowerName.uppercased()
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
        btnYes.setImage(UIImage(named: isActive == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isActive == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isActive == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isActive == 2 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        self.dismissVC()
    }
}
