//
//  DemographicDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 29/09/2021.
//

import UIKit

enum DemographicType{
    case race
    case ethnicity
}

class DemographicDetailViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var tableViewQuestion: UITableView!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var type: DemographicType!
    var borrowerName = ""
    
    var raceModel = RaceModel()
    var ethnicityModel = EthnicityModel()
    var demographicDetail = DemographicModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        lblUsername.text = borrowerName.uppercased()
        tableViewQuestion.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        tableViewQuestion.contentInset = UIEdgeInsets(top: 12, left: 0, bottom: 0, right: 0)
        
        if (type == .race){
            lblQuestion.text = raceModel.name
        }
        else{
            lblQuestion.text = ethnicityModel.name
        }
        
        for raceDetail in raceModel.raceDetails{
            if let race = demographicDetail.race.filter({$0.raceId == raceModel.id}).first{
                for detail in race.raceDetails{
                    if detail.detailId == raceDetail.id{
                        raceDetail.isSelected = true
                    }
                }
            }
        }
        
        for ethnicityDetail in ethnicityModel.ethnicityDetails{
            if let ethnicity = demographicDetail.ethnicity.filter({$0.ethnicityId == ethnicityModel.id}).first{
                for detail in ethnicity.ethnicityDetails{
                    if detail.detailId == ethnicityDetail.id{
                        ethnicityDetail.isSelected = true
                    }
                }
            }
        }
        
        self.tableViewQuestion.reloadData()
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
        return type == .race ? raceModel.raceDetails.count : ethnicityModel.ethnicityDetails.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
        if (type == .race){
            let type = raceModel.raceDetails[indexPath.row]
            cell.lblHeading.text = ""
            cell.txtfieldOther.setTextField(placeholder: type.otherPlaceHolder, controller: self, validationType: .noValidation)
            cell.txtfieldOther.setIsValidateOnEndEditing(validate: false)
            cell.lblQuestion.text = type.name
            cell.btnCheckBox.setImage(UIImage(named: type.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
            cell.lblQuestion.font = type.isSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            cell.otherView.isHidden = !type.isOther
            if let detail = demographicDetail.race.filter({$0.raceId == raceModel.id}).first{
                if let raceDetail = detail.raceDetails.filter({$0.detailId == type.id}).first{
                    cell.txtfieldOther.setTextField(text: raceDetail.otherRace)
                }
            }
        }
        else{
            let type = ethnicityModel.ethnicityDetails[indexPath.row]
            cell.lblHeading.text = ""
            cell.txtfieldOther.setTextField(placeholder: type.otherPlaceHolder, controller: self, validationType: .noValidation)
            cell.txtfieldOther.setIsValidateOnEndEditing(validate: false)
            cell.lblQuestion.text = type.name
            cell.btnCheckBox.setImage(UIImage(named: type.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
            cell.lblQuestion.font = type.isSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            cell.otherView.isHidden = !type.isOther
            if let detail = demographicDetail.ethnicity.filter({$0.ethnicityId == ethnicityModel.id}).first{
                if let ethnicityDetail = detail.ethnicityDetails.filter({$0.detailId == type.id}).first{
                    cell.txtfieldOther.setTextField(text: ethnicityDetail.otherEthnicity)
                }
            }
        }
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if (type == .race){
            let selectedRace = raceModel.raceDetails[indexPath.row]
            selectedRace.isSelected = !selectedRace.isSelected
        }
        else{
            let selectedEthnicity = ethnicityModel.ethnicityDetails[indexPath.row]
            selectedEthnicity.isSelected = !selectedEthnicity.isSelected
        }
        self.tableViewQuestion.reloadData()
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (type == .race){
            let detail = raceModel.raceDetails[indexPath.row]
            return detail.isOther ? 120 : 40
//            if (indexPath.row == demographicTypeArray.count - 1){
//                return type.isQuestionSelected ? 120 : 40
//            }
//            else{
//                return 40
//            }
        }
        else{
            let detail = ethnicityModel.ethnicityDetails[indexPath.row]
            return detail.isOther ? 120 : 40
//            if (indexPath.row == demographicTypeArray.count - 1){
//                return type.isQuestionSelected ? 120 : 40
//            }
//            else{
//                return 40
//            }
        }
        
    }
}
