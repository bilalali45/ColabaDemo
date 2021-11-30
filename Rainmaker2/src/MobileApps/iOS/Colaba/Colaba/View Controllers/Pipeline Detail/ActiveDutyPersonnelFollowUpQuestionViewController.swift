//
//  ActiveDutyPersonnelViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/08/2021.
//

import UIKit
import Material
import MonthYearPicker

protocol ActiveDutyPersonnelFollowUpQuestionViewControllerDelegate: AnyObject {
    func saveLastDateOfService(date: String)
}

class ActiveDutyPersonnelFollowUpQuestionViewController: BaseViewController {

    //MARK:- Outlets and Properties

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblTopHeading: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var separatorView: UIView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLastDate: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var borrowerName = ""
    var selectedMilitary = Detail()
    weak var delegate: ActiveDutyPersonnelFollowUpQuestionViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        lblBorrowerName.text = borrowerName.uppercased()
        txtfieldLastDate.setTextField(text: Utility.getMonthYear(selectedMilitary.expirationDateUtc))
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Last Date of Service Text Field
        txtfieldLastDate.setTextField(placeholder: "Last date of service / tour", controller: self, validationType: .required)
        txtfieldLastDate.type = .monthlyDatePicker
        if (selectedMilitary.expirationDateUtc == ""){
            _ = txtfieldLastDate.becomeFirstResponder()
        }

    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            self.delegate?.saveLastDateOfService(date: txtfieldLastDate.text!)
            self.dismissVC()
        }
    }
    
    func validate() -> Bool {
        return txtfieldLastDate.validate()
    }
}
