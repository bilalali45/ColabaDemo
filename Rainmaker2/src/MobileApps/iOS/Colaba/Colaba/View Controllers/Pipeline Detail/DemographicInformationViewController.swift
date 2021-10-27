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
    
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerName = ""
    
    var allRacesArray = [RaceModel]()
    var allEthnicityArray = [EthnicityModel]()
    var allGenderArray = [DropDownModel]()
    var demographicDetail = DemographicModel()
//    var racesArray = [DemographicQuestionsModel]()
//    var asianOptionArray = [DemographicQuestionsModel]()
//    var nativeHawaiianOptionArray = [DemographicQuestionsModel]()
//    var ethnicityArray = [DemographicQuestionsModel]()
//    var hispanicOptionArray = [DemographicQuestionsModel]()
//    var sexArray = [DemographicQuestionsModel]()
    
    var isAsianSelected = false
    var isNativeHawaiianSelected = false
    var ethnicitySelectedIndex: Int?
    var sexSelectedIndex: Int?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        raceTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        ethnicityTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        sexTableView.register(UINib(nibName: "DemographicQuestionsTableViewCell", bundle: nil), forCellReuseIdentifier: "DemographicQuestionsTableViewCell")
        getRaces()
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        setDemographicData()
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
    
    func setDemographicData(){
        
        for race in allRacesArray{
            race.isSelected = demographicDetail.race.filter({$0.raceId == race.id}).count > 0
        }
        for ethnicity in allEthnicityArray{
            ethnicity.isSelected = demographicDetail.ethnicity.filter({$0.ethnicityId == ethnicity.id}).count > 0
        }
        for gender in allGenderArray{
            gender.isSelected = demographicDetail.genderId == gender.optionId
        }
        raceTableView.reloadData()
        ethnicityTableView.reloadData()
        sexTableView.reloadData()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    //MARK:- API's
    
    func getRaces(){
        
        allRacesArray.removeAll()
        allEthnicityArray.removeAll()
        allGenderArray.removeAll()
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllRaceList, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let racesArray = result.arrayValue
                    for race in racesArray{
                        let model = RaceModel()
                        model.updateModelWithJSON(json: race)
                        self.allRacesArray.append(model)
                    }
                    self.getEthnicities()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
        
    }
    
    func getEthnicities(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllEthnicityList, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let ethnicityArray = result.arrayValue
                    for ethnicity in ethnicityArray{
                        let model = EthnicityModel()
                        model.updateModelWithJSON(json: ethnicity)
                        self.allEthnicityArray.append(model)
                    }
                    self.getGender()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func getGender(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllGenderList, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let genderArray = result.arrayValue
                    for gender in genderArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: gender)
                        self.allGenderArray.append(model)
                    }
                    self.getDemographicInformation()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func getDemographicInformation(){
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(self.borrowerId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getDemographicInformation, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    
                    let model = DemographicModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.demographicDetail = model
                    self.setDemographicData()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
}

extension DemographicInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        
        if (tableView == raceTableView){
            return allRacesArray.count
        }
        else if (tableView == ethnicityTableView){
            return allEthnicityArray.count
        }
        else{
            return allGenderArray.count
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
            let race = allRacesArray[indexPath.section]
            cell.lblQuestion.text = race.name
            cell.btnCheckBox.setImage(UIImage(named: race.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
            cell.lblQuestion.font = race.isSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            cell.otherView.isHidden = true
            cell.stackViewLeadingConstraint.constant = 20
            
            if let selectedRace = demographicDetail.race.filter({$0.raceId == race.id}).first{
                cell.otherDetailView.isHidden = selectedRace.raceDetails.count == 0
                cell.otherDetailViewHeightConstraint.constant = selectedRace.raceDetails.filter({$0.isOther == true}).count > 0 ? 84 : 58
                let subRace = selectedRace.raceDetails.filter({$0.isOther == false})
                cell.lblDetail.text = subRace.map({$0.name}).joined(separator: ", ")
                if let otherRace = selectedRace.raceDetails.filter({$0.isOther == true}).first{
                    let otherAsian = race.name.localizedCaseInsensitiveContains("Asian") ? "Other Asian: \(otherRace.otherRace)" : "Other Pacific Islander: \(otherRace.otherRace)"
                    let attributedOtherAsian = NSMutableAttributedString(string: otherAsian)
                    let range = otherAsian.range(of: otherRace.otherRace)
                    attributedOtherAsian.addAttribute(NSMutableAttributedString.Key.foregroundColor, value: Theme.getAppGreyColor(), range: otherAsian.nsRange(from: range!))
                    cell.lblOther.attributedText = attributedOtherAsian
                }
                else{
                    cell.lblOther.text = ""
                }

            }
            else{
                cell.otherDetailView.isHidden = true
            }
            cell.layoutIfNeeded()
            return cell
        }
        else if (tableView == ethnicityTableView){
            let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
            cell.indexPath = indexPath
            cell.delegate = self
         
            let ethnicity = allEthnicityArray[indexPath.section]
            cell.lblQuestion.text = ethnicity.name
            cell.btnCheckBox.setImage(UIImage(named: ethnicity.isSelected ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            cell.lblQuestion.font = ethnicity.isSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            cell.otherView.isHidden = true
            cell.lblHeading.text = ""
            cell.stackViewLeadingConstraint.constant = 20
            
            if let selectedEthnicity = demographicDetail.ethnicity.filter({$0.ethnicityId == ethnicity.id}).first{
                cell.otherDetailView.isHidden = selectedEthnicity.ethnicityDetails.count == 0
                cell.otherDetailViewHeightConstraint.constant = selectedEthnicity.ethnicityDetails.filter({$0.isOther == true}).count > 0 ? 84 : 58
                let subEthnicity = selectedEthnicity.ethnicityDetails.filter({$0.isOther == false})
                cell.lblDetail.text = subEthnicity.map({$0.name}).joined(separator: ", ")
                if let otherEthnicity = selectedEthnicity.ethnicityDetails.filter({$0.isOther == true}).first{
                    let otherHispanic =  "Other Hispanic or Latino: \(otherEthnicity.otherEthnicity)"
                    let attributedOtherHispanic = NSMutableAttributedString(string: otherHispanic)
                    let range = otherHispanic.range(of: otherEthnicity.otherEthnicity)
                    attributedOtherHispanic.addAttribute(NSMutableAttributedString.Key.foregroundColor, value: Theme.getAppGreyColor(), range: otherHispanic.nsRange(from: range!))
                    cell.lblOther.attributedText = attributedOtherHispanic
                }
                else{
                    cell.lblOther.text = ""
                }

            }
            else{
                cell.otherDetailView.isHidden = true
            }
            
            
            cell.layoutIfNeeded()
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "DemographicQuestionsTableViewCell", for: indexPath) as! DemographicQuestionsTableViewCell
            let gender = allGenderArray[indexPath.section]
            cell.lblHeading.text = ""
            cell.lblQuestion.text = gender.optionName
            cell.btnCheckBox.setImage(UIImage(named: gender.isSelected ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            cell.lblQuestion.font = gender.isSelected ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            return cell
        }
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {

        if (tableView == raceTableView){
            let selectedRace = allRacesArray[indexPath.section]
            for race in allRacesArray{
                if (selectedRace.id == race.id){
                    selectedRace.isSelected = !selectedRace.isSelected
                }
            }
            
            if (selectedRace.name.localizedCaseInsensitiveContains("I do not wish to provide this information")){
                for race in allRacesArray{
                    if (race.id != selectedRace.id){
                        race.isSelected = false
                    }
                }
            }
            
            if selectedRace.raceDetails.count > 0 && selectedRace.isSelected{
                let vc = Utility.getDemographicDetailVC()
                vc.type = .race
                vc.raceModel = selectedRace
                vc.demographicDetail = self.demographicDetail
                vc.borrowerName = self.borrowerName
                self.presentVC(vc: vc)
            }
            
            raceTableView.reloadData()
        }
        else if (tableView == ethnicityTableView){
            let selectedEthnicity = allEthnicityArray[indexPath.section]
            for ethnicity in allEthnicityArray{
                ethnicity.isSelected = ethnicity.id == selectedEthnicity.id
            }
            if selectedEthnicity.ethnicityDetails.count > 0{
                let vc = Utility.getDemographicDetailVC()
                vc.type = .ethnicity
                vc.ethnicityModel = selectedEthnicity
                vc.demographicDetail = self.demographicDetail
                vc.borrowerName = self.borrowerName
                self.presentVC(vc: vc)
            }
            ethnicityTableView.reloadData()
        }
        else{
            let selectedGender = allGenderArray[indexPath.section]
            for gender in allGenderArray{
                gender.isSelected = gender.optionId == selectedGender.optionId
            }
            sexTableView.reloadData()
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
            let race = allRacesArray[indexPath.section]
            if let selectedRace = demographicDetail.race.filter({$0.raceId == race.id}).first{
                if (selectedRace.raceDetails.count == 0){
                    return 40
                }
                else{
                    return selectedRace.raceDetails.filter({$0.isOther == true}).count > 0 ? 140 : 110
                }
            }
            return 40
        }
        else if (tableView == ethnicityTableView){
            let ethnicity = allEthnicityArray[indexPath.section]
            if let selectedEthnicity = demographicDetail.ethnicity.filter({$0.ethnicityId == ethnicity.id}).first{
                if (selectedEthnicity.ethnicityDetails.count == 0){
                    return 40
                }
                else{
                    return selectedEthnicity.ethnicityDetails.filter({$0.isOther == true}).count > 0 ? 140 : 110
                }
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
            let selectedEthnicity = allEthnicityArray[indexPath.section]
            vc.type = .ethnicity
            vc.ethnicityModel = selectedEthnicity
            vc.demographicDetail = self.demographicDetail
            vc.borrowerName = self.borrowerName
            self.presentVC(vc: vc)
        }
        else{
            let selectedRace = allRacesArray[indexPath.section]
            vc.type = .race
            vc.raceModel = selectedRace
            vc.demographicDetail = self.demographicDetail
            vc.borrowerName = self.borrowerName
            self.presentVC(vc: vc)
        }
        
    }
    
}
