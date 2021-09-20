//
//  DemographicInformationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/09/2021.
//

import UIKit

class DemographicInformationViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var raceTableView: UITableView!
    @IBOutlet weak var raceTableViewHeightConstraint: NSLayoutConstraint!
    
    var racesArray = [DemographicQuestionsModel]()//["American Indian or Alaska Native", "Asian", "Black or African American", "Native Hawaiian or Other Pacific Islander", "White", "I do not wish to provide this information"]
    var asianOptionArray = [DemographicQuestionsModel]()//["Asian Indian", "Chinese", "Filipino", "Japanese", "Korean", "Vietnamese", "Other Asian"]
    var nativeHawaiianOptionArray = [DemographicQuestionsModel]()//["Native Hawaiian", "Guamanian or Chamorro", "Samoan", "Other Pacific Islander"]
    
    var isAsianSelected = false
    var isNativeHawaiianSelected = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        raceTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        
        let model1 = DemographicQuestionsModel()
        model1.question = "American Indian or Alaska Native"
        
        let model2 = DemographicQuestionsModel()
        model2.question = "Asian"
        
        let model3 = DemographicQuestionsModel()
        model3.question = "Black or African American"
        
        let model4 = DemographicQuestionsModel()
        model4.question = "Native Hawaiian or Other Pacific Islander"
        
        let model5 = DemographicQuestionsModel()
        model5.question = "White"
        
        let model6 = DemographicQuestionsModel()
        model6.question = "I do not wish to provide this information"
        
        self.racesArray.append(model1)
        self.racesArray.append(model2)
        self.racesArray.append(model3)
        self.racesArray.append(model4)
        self.racesArray.append(model5)
        self.racesArray.append(model6)
        
        let asianOptionModel1 = DemographicQuestionsModel()
        asianOptionModel1.question = "Asian Indian"
        
        let asianOptionModel2 = DemographicQuestionsModel()
        asianOptionModel2.question = "Chinese"
        
        let asianOptionModel3 = DemographicQuestionsModel()
        asianOptionModel3.question = "Filipino"
        
        let asianOptionModel4 = DemographicQuestionsModel()
        asianOptionModel4.question = "Japanese"
        
        let asianOptionModel5 = DemographicQuestionsModel()
        asianOptionModel5.question = "Korean"
        
        let asianOptionModel6 = DemographicQuestionsModel()
        asianOptionModel6.question = "Vietnamese"
        
        let asianOptionModel7 = DemographicQuestionsModel()
        asianOptionModel7.question = "Other Asian"
        
        self.asianOptionArray.append(asianOptionModel1)
        self.asianOptionArray.append(asianOptionModel2)
        self.asianOptionArray.append(asianOptionModel3)
        self.asianOptionArray.append(asianOptionModel4)
        self.asianOptionArray.append(asianOptionModel5)
        self.asianOptionArray.append(asianOptionModel6)
        self.asianOptionArray.append(asianOptionModel7)
        
        let nativeQuestionModel1 = DemographicQuestionsModel()
        nativeQuestionModel1.question = "Native Hawaiian"
        
        let nativeQuestionModel2 = DemographicQuestionsModel()
        nativeQuestionModel2.question = "Guamanian or Chamorro"
        
        let nativeQuestionModel3 = DemographicQuestionsModel()
        nativeQuestionModel3.question = "Samoan"
        
        let nativeQuestionModel4 = DemographicQuestionsModel()
        nativeQuestionModel4.question = "Other Pacific Islander"
        
        self.nativeHawaiianOptionArray.append(nativeQuestionModel1)
        self.nativeHawaiianOptionArray.append(nativeQuestionModel2)
        self.nativeHawaiianOptionArray.append(nativeQuestionModel3)
        self.nativeHawaiianOptionArray.append(nativeQuestionModel4)
        
        self.raceTableView.reloadData()
        
    }
  
    //MARK:- Methods
    
    func setScreenHeight(){
        let raceTableViewHeight = raceTableView.contentSize.height
        self.raceTableViewHeightConstraint.constant = raceTableViewHeight
        self.mainViewHeightConstraint.constant = raceTableViewHeight + 100
        
        UIView.animate(withDuration: 0.3) {
            self.view.layoutIfNeeded()
        }
    }
}

extension DemographicInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return racesArray.count
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (section == 1){
            return isAsianSelected ? asianOptionArray.count + 1 : 1
        }
        else if (section == 3){
            return isNativeHawaiianSelected ? nativeHawaiianOptionArray.count + 1 : 1
        }
        return 1
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
        
        if (indexPath.row == 0){
            let race = racesArray[indexPath.section]
            cell.lblQuestion.text = race.question
            cell.btnCheckBox.setImage(UIImage(named: race.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
            cell.lblQuestion.font = race.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            cell.otherView.isHidden = true
            cell.stackViewLeadingConstraint.constant = 20
        }
        else{
            let race = indexPath.section == 1 ? asianOptionArray[indexPath.row - 1] : nativeHawaiianOptionArray[indexPath.row - 1]
            cell.lblQuestion.text = race.question
            cell.btnCheckBox.setImage(UIImage(named: race.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
            cell.lblQuestion.font = race.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            
            if (indexPath.section == 1 && indexPath.row == 7){
                cell.otherView.isHidden = !race.isQuestionSelected
            }
            else if (indexPath.section == 3 && indexPath.row == 4){
                cell.otherView.isHidden = !race.isQuestionSelected
            }
            else{
                cell.otherView.isHidden = true
            }
            cell.stackViewLeadingConstraint.constant = 54
        }
        cell.layoutIfNeeded()
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        var race = DemographicQuestionsModel()
        if (indexPath.row == 0){
            race = racesArray[indexPath.section]
            if (indexPath.section == 1){
                isAsianSelected = !isAsianSelected
            }
            else if (indexPath.section == 3){
                isNativeHawaiianSelected = !isNativeHawaiianSelected
            }
        }
        else{
            if (indexPath.section == 1){
                race = asianOptionArray[indexPath.row - 1]
            }
            else if (indexPath.section == 3){
                race = nativeHawaiianOptionArray[indexPath.row - 1]
            }
        }
        race.isQuestionSelected = !race.isQuestionSelected
        self.raceTableView.reloadData()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 0
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        if (indexPath.section == 1 && indexPath.row == 7){
            let race = asianOptionArray[indexPath.row - 1]
            return race.isQuestionSelected ? 100 : 40
        }
        else if (indexPath.section == 3 && indexPath.row == 4){
            let race = nativeHawaiianOptionArray[indexPath.row - 1]
            return race.isQuestionSelected ? 100 : 40
        }
        return 40
    }
}
