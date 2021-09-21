//
//  ActiveDutyPersonnelViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import MonthYearPicker

class ActiveDutyPersonnelFollowUpQuestionViewController: BaseViewController {

    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLastDate: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Last Date of Service Text Field
        txtfieldLastDate.setTextField(placeholder: "Last date of service / tour")
        txtfieldLastDate.setDelegates(controller: self)
        txtfieldLastDate.setValidation(validationType: .required)
        txtfieldLastDate.type = .monthlyDatePicker
        _ = txtfieldLastDate.becomeFirstResponder()
        

    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldLastDate.text != ""){
                self.dismissVC()
            }
        }
    }
    
    func validate() -> Bool {
        return txtfieldLastDate.validate()
    }
}
