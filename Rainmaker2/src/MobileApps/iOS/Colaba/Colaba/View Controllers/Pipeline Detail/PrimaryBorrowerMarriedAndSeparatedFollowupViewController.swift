//
//  PrimaryBorrowerMarriedAndSeparatedFollowupViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 26/11/2021.
//

import UIKit

class PrimaryBorrowerMarriedAndSeparatedFollowupViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var txtfieldCoBorrowerName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseFirstName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseMiddleName: ColabaTextField!
    @IBOutlet weak var txtfieldSpouseLastName: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var isMarried = 0 // 1 for yes 2 for no
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setTextFields()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
    }
    
    //MARK:- Methods and Actions
    
    func setTextFields() {
        ///Co Borrower Name
        txtfieldCoBorrowerName.setTextField(placeholder: "Which One?", controller: self, validationType: .noValidation)
        txtfieldCoBorrowerName.type = .dropdown
        txtfieldCoBorrowerName.setDropDownDataSource(["Test"])
        
        ///First Name
        txtfieldSpouseFirstName.setTextField(placeholder: "Spouse's Legal First Name", controller: self, validationType: .noValidation)
        
        ///Middle Name
        txtfieldSpouseMiddleName.setTextField(placeholder: "Middle Name", controller: self, validationType: .noValidation)
        
        ///Last Name
        txtfieldSpouseLastName.setTextField(placeholder: "Spouse's Legal Last Name", controller: self, validationType: .noValidation)
    }
    
    @objc func yesStackViewTapped(){
        isMarried = 1
        changeMarriedStatus()
    }
    
    @objc func noStackViewTapped(){
        isMarried = 2
        changeMarriedStatus()
    }
    
    func changeMarriedStatus(){
        btnYes.setImage(UIImage(named: isMarried == 1 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblYes.font = isMarried == 1 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnNo.setImage(UIImage(named: isMarried == 2 ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblNo.font = isMarried == 2 ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        txtfieldCoBorrowerName.isHidden = isMarried == 2
        txtfieldSpouseFirstName.isHidden = isMarried == 1
        txtfieldSpouseMiddleName.isHidden = isMarried == 1
        txtfieldSpouseLastName.isHidden = isMarried == 1
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.dismissVC()
    }
}
