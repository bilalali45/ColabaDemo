//
//  DemographicDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 29/09/2021.
//

import UIKit

enum DemographicType{
    case asian
    case hawaiian
    case hispanic
}

class DemographicDetailViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var tableViewQuestion: UITableView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var type: DemographicType!
    var demographicTypeArray = [DemographicQuestionsModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        tableViewQuestion.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        tableViewQuestion.contentInset = UIEdgeInsets(top: 12, left: 0, bottom: 0, right: 0)
        
        if (type == .asian){
            lblQuestion.text = "Asian"
        }
        else if (type == .hawaiian){
            lblQuestion.text = "Native Hawaiian or Other Pacific Islander"
        }
        else if (type == .hispanic){
            lblQuestion.text = "Hispanic or Latino"
        }
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.dismissVC()
    }

}

extension DemographicDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return demographicTypeArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
        let type = demographicTypeArray[indexPath.row]
        cell.lblHeading.text = ""
        cell.txtfieldOther.setTextField(placeholder: "Specify", controller: self, validationType: .noValidation)
        cell.txtfieldOther.setIsValidateOnEndEditing(validate: false)
        cell.lblQuestion.text = type.question
        cell.btnCheckBox.setImage(UIImage(named: type.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        cell.lblQuestion.font = type.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        
        if (indexPath.row == demographicTypeArray.count - 1){
            cell.otherView.isHidden = !type.isQuestionSelected
        }
        else{
            cell.otherView.isHidden = true
        }
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let type = demographicTypeArray[indexPath.row]
        type.isQuestionSelected = !type.isQuestionSelected
        self.tableViewQuestion.reloadData()
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        let type = demographicTypeArray[indexPath.row]
        if (indexPath.row == demographicTypeArray.count - 1){
            return type.isQuestionSelected ? 120 : 40
        }
        else{
            return 40
        }
    }
}
