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
    @IBOutlet weak var ethnicityTableView: UITableView!
    @IBOutlet weak var ethnicityTableViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var sexTableView: UITableView!
    
    var racesArray = [DemographicQuestionsModel]()
    var asianOptionArray = [DemographicQuestionsModel]()
    var nativeHawaiianOptionArray = [DemographicQuestionsModel]()
    var ethnicityArray = [DemographicQuestionsModel]()
    var hispanicOptionArray = [DemographicQuestionsModel]()
    var sexArray = [DemographicQuestionsModel]()
    
    var isAsianSelected = false
    var isNativeHawaiianSelected = false
    var ethnicitySelectedIndex: Int?
    var sexSelectedIndex: Int?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        raceTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        ethnicityTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        sexTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        
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
        
        let ethnicityModel1 = DemographicQuestionsModel()
        ethnicityModel1.question = "Hispanic or Latino"
        
        let ethnicityModel2 = DemographicQuestionsModel()
        ethnicityModel2.question = "Not Hispanic or Latino"
        
        let ethnicityModel3 = DemographicQuestionsModel()
        ethnicityModel3.question = "I do not wish to provide this information"
        
        self.ethnicityArray.append(ethnicityModel1)
        self.ethnicityArray.append(ethnicityModel2)
        self.ethnicityArray.append(ethnicityModel3)
        
        let hispanicModel1 = DemographicQuestionsModel()
        hispanicModel1.question = "Mexican"
        
        let hispanicModel2 = DemographicQuestionsModel()
        hispanicModel2.question = "Puerto Rican"
        
        let hispanicModel3 = DemographicQuestionsModel()
        hispanicModel3.question = "Cuban"
        
        let hispanicModel4 = DemographicQuestionsModel()
        hispanicModel4.question = "Other Hispanic or Latino"
        
        self.hispanicOptionArray.append(hispanicModel1)
        self.hispanicOptionArray.append(hispanicModel2)
        self.hispanicOptionArray.append(hispanicModel3)
        self.hispanicOptionArray.append(hispanicModel4)
        
        let sexModel1 = DemographicQuestionsModel()
        sexModel1.question = "Male"
        
        let sexModel2 = DemographicQuestionsModel()
        sexModel2.question = "Female"
        
        let sexModel3 = DemographicQuestionsModel()
        sexModel3.question = "I do not wish to provide this information"
        
        self.sexArray.append(sexModel1)
        self.sexArray.append(sexModel2)
        self.sexArray.append(sexModel3)
        
        self.raceTableView.reloadData()
        
    }
  
    //MARK:- Methods
    
    func setScreenHeight(){
        let raceTableViewHeight = raceTableView.contentSize.height
        let ethnicityTableViewHeight = ethnicityTableView.contentSize.height
        let sexTableViewHeight = sexTableView.contentSize.height
        
        self.raceTableViewHeightConstraint.constant = raceTableViewHeight
        self.ethnicityTableViewHeightConstraint.constant = ethnicityTableViewHeight
        
        self.mainViewHeightConstraint.constant = raceTableViewHeight + ethnicityTableViewHeight + sexTableViewHeight + 400
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
}

extension DemographicInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        
        if (tableView == raceTableView){
            return racesArray.count
        }
        else if (tableView == ethnicityTableView){
            return ethnicityArray.count
        }
        else{
            return sexArray.count
        }
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (tableView == raceTableView){
            if (section == 1){
                return 1//isAsianSelected ? asianOptionArray.count + 1 : 1
            }
            else if (section == 3){
                return 1//isNativeHawaiianSelected ? nativeHawaiianOptionArray.count + 1 : 1
            }
            return 1
        }
        else if (tableView == ethnicityTableView){
            if (section == 0){
                return 1//ethnicitySelectedIndex == section ? hispanicOptionArray.count + 1 : 1
            }
            else{
                return 1
            }
        }
        else{
            return 1
        }
        
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (tableView == raceTableView){
            let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
            cell.lblHeading.text = ""
            cell.indexPath = indexPath
            cell.delegate = self
            if (indexPath.row == 0){
                let race = racesArray[indexPath.section]
                cell.lblQuestion.text = race.question
                cell.btnCheckBox.setImage(UIImage(named: race.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.lblQuestion.font = race.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
                cell.otherView.isHidden = true
                cell.stackViewLeadingConstraint.constant = 20
                if (indexPath.section == 1){
                    cell.otherDetailView.isHidden = !race.isQuestionSelected
                    cell.otherDetailViewHeightConstraint.constant = 84
                    cell.lblDetail.text = "Asian Indian"
                    let otherAsian = "Other Asian: Thai"
                    let attributedOtherAsian = NSMutableAttributedString(string: otherAsian)
                    let range = otherAsian.range(of: "Thai")
                    attributedOtherAsian.addAttribute(NSMutableAttributedString.Key.foregroundColor, value: Theme.getAppGreyColor(), range: otherAsian.nsRange(from: range!))
                    cell.lblOther.attributedText = attributedOtherAsian
                }
                else if (indexPath.section == 3){
                    cell.otherDetailView.isHidden = !race.isQuestionSelected
                    cell.otherDetailViewHeightConstraint.constant = 58
                    cell.lblDetail.text = ""
                    cell.lblOther.text = "Other Pacific Islander: Fijian"
                    let otherHawaiian = "Other Pacific Islander: Fijian"
                    let attributedOtherHawaiian = NSMutableAttributedString(string: otherHawaiian)
                    let range = otherHawaiian.range(of: "Fijian")
                    attributedOtherHawaiian.addAttribute(NSMutableAttributedString.Key.foregroundColor, value: Theme.getAppGreyColor(), range: otherHawaiian.nsRange(from: range!))
                    cell.lblOther.attributedText = attributedOtherHawaiian
                }
                else{
                    cell.otherDetailView.isHidden = true
                }
            }
            else{
                let race = indexPath.section == 1 ? asianOptionArray[indexPath.row - 1] : nativeHawaiianOptionArray[indexPath.row - 1]
                cell.lblQuestion.text = race.question
                cell.btnCheckBox.setImage(UIImage(named: race.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.lblQuestion.font = race.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
                cell.txtfieldOther.placeholder = "For example, Pakistani, Laotian, Thai, etc."
                
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
        else if (tableView == ethnicityTableView){
            let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
            cell.indexPath = indexPath
            cell.delegate = self
            if (indexPath.row == 0){
                let ethnicity = ethnicityArray[indexPath.section]
                cell.lblQuestion.text = ethnicity.question
                cell.btnCheckBox.setImage(UIImage(named: ethnicitySelectedIndex == indexPath.section ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
                cell.lblQuestion.font = ethnicitySelectedIndex == indexPath.section ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
                cell.otherView.isHidden = true
                cell.lblHeading.text = ""
                cell.stackViewLeadingConstraint.constant = 20
                if (indexPath.section == 0){
                    cell.otherDetailView.isHidden = ethnicitySelectedIndex != indexPath.section
                }
                else{
                    cell.otherDetailView.isHidden = true
                }
                cell.otherDetailViewHeightConstraint.constant = 58
                cell.lblDetail.text = ""
                let otherHispanic = "Other Hispanic or Latino: Argentinean"
                let attributedOtherHispanic = NSMutableAttributedString(string: otherHispanic)
                let range = otherHispanic.range(of: "Argentinean")
                attributedOtherHispanic.addAttribute(NSMutableAttributedString.Key.foregroundColor, value: Theme.getAppGreyColor(), range: otherHispanic.nsRange(from: range!))
                cell.lblOther.attributedText = attributedOtherHispanic
            }
            else{
                let ethnicity = hispanicOptionArray[indexPath.row - 1]
                cell.lblQuestion.text = ethnicity.question
                cell.btnCheckBox.setImage(UIImage(named: ethnicity.isQuestionSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.lblQuestion.font = ethnicity.isQuestionSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
                cell.txtfieldOther.placeholder = "For example, Argentinean, Colombian, Dominican, etc."
                cell.lblHeading.text = indexPath.row == 1 ? "Select all that apply." : ""
                
                if (indexPath.section == 0 && indexPath.row == 4){
                    cell.otherView.isHidden = !ethnicity.isQuestionSelected
                }
                else{
                    cell.otherView.isHidden = true
                }
                cell.stackViewLeadingConstraint.constant = 54
            }
            cell.layoutIfNeeded()
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
            let sex = sexArray[indexPath.section]
            cell.lblHeading.text = ""
            cell.lblQuestion.text = sex.question
            cell.btnCheckBox.setImage(UIImage(named: sexSelectedIndex == indexPath.section ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            cell.lblQuestion.font = sexSelectedIndex == indexPath.section ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            return cell
        }
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let vc = Utility.getDemographicDetailVC()
        
        if (tableView == raceTableView){
            var race = DemographicQuestionsModel()
            if (indexPath.row == 0){
                race = racesArray[indexPath.section]
                if (indexPath.section == 1){
                    isAsianSelected = !isAsianSelected
                    if (isAsianSelected){
                        vc.type = .asian
                        vc.demographicTypeArray = self.asianOptionArray
                        self.presentVC(vc: vc)
                    }
                }
                else if (indexPath.section == 3){
                    isNativeHawaiianSelected = !isNativeHawaiianSelected
                    if (isNativeHawaiianSelected){
                        vc.type = .hawaiian
                        vc.demographicTypeArray = self.nativeHawaiianOptionArray
                        self.presentVC(vc: vc)
                    }
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
            
            if (indexPath.section == 5){
                for race in racesArray{
                    if (race.question != "I do not wish to provide this information"){
                        race.isQuestionSelected = false
                    }
                }
                for asian in asianOptionArray{
                    asian.isQuestionSelected = false
                }
                for native in nativeHawaiianOptionArray{
                    native.isQuestionSelected = false
                }
                isAsianSelected = false
                isNativeHawaiianSelected = false
            }
            else{
                for race in racesArray{
                    if (race.question == "I do not wish to provide this information"){
                        race.isQuestionSelected = false
                    }
                }
            }
            
            self.raceTableView.reloadData()
        }
        else if (tableView == ethnicityTableView){
            
            var ethnicity = DemographicQuestionsModel()
            if (indexPath.row == 0){
                ethnicity = ethnicityArray[indexPath.section]
                ethnicitySelectedIndex = indexPath.section
            }
            else{
                ethnicity = hispanicOptionArray[indexPath.row - 1]
                ethnicity.isQuestionSelected = !ethnicity.isQuestionSelected
            }
            self.ethnicityTableView.reloadData()
            if (ethnicitySelectedIndex == 0){
                vc.type = .hispanic
                vc.demographicTypeArray = self.hispanicOptionArray
                self.presentVC(vc: vc)
            }
        }
        else{
            sexSelectedIndex = indexPath.section
            self.sexTableView.reloadData()
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 0
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (tableView == raceTableView){
//            if (indexPath.section == 1 && indexPath.row == 7){
//                let race = asianOptionArray[indexPath.row - 1]
//                return race.isQuestionSelected ? 100 : 40
//            }
//            else if (indexPath.section == 3 && indexPath.row == 4){
//                let race = nativeHawaiianOptionArray[indexPath.row - 1]
//                return race.isQuestionSelected ? 100 : 40
//            }
//            return 40
            let race = racesArray[indexPath.section]
            if (indexPath.section == 1){
                return race.isQuestionSelected ? 140 : 40
            }
            else if (indexPath.section == 3){
                return race.isQuestionSelected ? 110 : 40
            }
            return 40
        }
        else if (tableView == ethnicityTableView){
//            if (indexPath.section == 0 && indexPath.row == 1){
//                return 56
//            }
//            else if (indexPath.section == 0 && indexPath.row == 4){
//                let ethnicity = hispanicOptionArray[indexPath.row - 1]
//                return ethnicity.isQuestionSelected ? 100 : 40
//            }
//            return 40
            if (indexPath.section == 0 && ethnicitySelectedIndex == 0){
                return 110
            }
            return 40
        }
        else{
            return 40
        }
    }
}

extension DemographicInformationViewController: DemographicQuestionsTableViewCellDelegate{
    
    func otherDetailViewTapped(indexPath: IndexPath) {
        
        let vc = Utility.getDemographicDetailVC()
        
        if (indexPath.section == 0){
            vc.type = .hispanic
            vc.demographicTypeArray = self.hispanicOptionArray
            self.presentVC(vc: vc)
        }
        else if (indexPath.section == 1){
            vc.type = .asian
            vc.demographicTypeArray = self.hispanicOptionArray
            self.presentVC(vc: vc)
        }
        else if (indexPath.section == 3){
            vc.type = .hawaiian
            vc.demographicTypeArray = self.hispanicOptionArray
            self.presentVC(vc: vc)
        }
        
    }
    
}
