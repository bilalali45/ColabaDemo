//
//  InActivePipelineViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/06/2021.
//

import UIKit
import LoadingPlaceholderView
import RealmSwift

class InActivePipelineViewController: BaseViewController {

    //MARK:- Outlets and properties
    @IBOutlet weak var assignToMeSwitch: UISwitch!
    @IBOutlet weak var btnFilters: UIButton!
    @IBOutlet weak var tblView: UITableView!
    
    var expandableCellsIndex = [Int]()
    let loadingPlaceholderView = LoadingPlaceholderView()
    var pipeLineArray = [InActiveLoanModel]()
    
    var dateForPage1 = ""
    var pageNumber = 1
    var orderBy = 0 //0=MostActionsPending, 1=MostRecentActivity, 2=PrimaryBorrowerLastName(A to Z), 3=PrimaryBorrowerLastName(Z to A)
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        pipeLineArray = InActiveLoanModel.getAllInActiveLoans()
        tblView.register(UINib(nibName: "PipelineTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineTableViewCell")
        tblView.register(UINib(nibName: "PipelineDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineDetailTableViewCell")
        tblView.coverableCellsIdentifiers = ["PipelineDetailTableViewCell", "PipelineDetailTableViewCell", "PipelineDetailTableViewCell", "PipelineDetailTableViewCell"]
        dateForPage1 = Utility.getDate()
        getPipelineData()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(true)
    }
    
    //MARK:- Methods and Actions
    @IBAction func btnFilterTapped(_ sender: UIButton) {
        let vc = Utility.getFiltersVC()
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @IBAction func assignToMeSwitchChanged(_ sender: UISwitch) {
        self.pageNumber = 1
        self.dateForPage1 = Utility.getDate()
        self.getPipelineData()
    }
    
    //MARK:- API's
    
    func getPipelineData(){
        
        if (pipeLineArray.count == 0){
            self.loadingPlaceholderView.cover(self.tblView, animated: true)
        }
        
        let extraData = "dateTime=\(dateForPage1)&pageNumber=\(pageNumber)&pageSize=20&loanFilter=2&orderBy=\(orderBy)&assignedToMe=\(assignToMeSwitch.isOn ? true : false)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getPipelineList, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                if (status == .success){
                    
                    if (self.pageNumber == 1){
                        self.pipeLineArray.removeAll()
                        let realm = try! Realm()
                        realm.beginWrite()
                        realm.delete(realm.objects(InActiveLoanModel.self))
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = InActiveLoanModel()
                                model.updateModelWithJSON(json: loan)
                                realm.add(model)
                            }
                        }
                        else{
                            self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(5)) { reason in
                                
                            }
                        }
                        try! realm.commitWrite()
                        self.pipeLineArray = Array(realm.objects(InActiveLoanModel.self))
                        self.tblView.reloadData()
                        if (self.pipeLineArray.count > 0){
                            self.tblView.scrollToRow(at: IndexPath(row: 0, section: 0), at: .top, animated: true)
                        }
                    }
                    else{
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = InActiveLoanModel()
                                model.updateModelWithJSON(json: loan)
                                self.pipeLineArray.append(model)
                            }
                            self.tblView.reloadData()
                        }
                    }
                    
                }
                else{
                    self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
}

extension InActivePipelineViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return pipeLineArray.count
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if expandableCellsIndex.contains(section){
            return 2
        }
        else{
            return 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let loanData = pipeLineArray[indexPath.section]
        
        if (indexPath.row == 0){
            let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineTableViewCell", for: indexPath) as! PipelineTableViewCell
            
            cell.mainViewHeightConstraint.constant = loanData.documents == 0 ? 123 : 145
            cell.btnArrowBottomConstraint.constant = loanData.documents == 0 ? 15 : 11
            cell.updateConstraintsIfNeeded()
            cell.layoutSubviews()
            cell.mainView.layer.cornerRadius = 8
            cell.mainView.dropShadow()
            cell.indexPath = indexPath
            cell.delegate = self
            cell.lblUsername.text = "\(loanData.firstName) \(loanData.lastName)"
            cell.lblMoreUsers.text = loanData.coBorrowerCount == 0 ? "" : "+\(loanData.coBorrowerCount)"
            cell.lblTime.text = Utility.timeAgoSince(loanData.activityTime)
            cell.typeIcon.image = UIImage(named: loanData.loanPurpose == "Purchase" ? "PurchaseIcon" : "RefinanceIcon")
            cell.lblType.text = loanData.loanPurpose
            cell.lblStatus.text = loanData.milestone
            cell.lblDocuments.text = loanData.documents == 0 ? "" : "\(loanData.documents) Documents to Review"
            cell.lblDocuments.isHidden = loanData.documents == 0
            cell.bntArrow.setImage(UIImage(named: expandableCellsIndex.contains(indexPath.section) ? "ArrowUp" : "ArrowDown"), for: .normal)
            cell.emptyView.isHidden = !expandableCellsIndex.contains(indexPath.section)
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "PipelineDetailTableViewCell", for: indexPath) as! PipelineDetailTableViewCell
            cell.mainView.layer.cornerRadius = 8
            cell.mainView.dropShadow()
            cell.lblPropertyAddress.text = "\(loanData.street) \(loanData.unit) \(loanData.city) \(loanData.stateName) \(loanData.zipCode) \(loanData.countryName)"
            cell.lblPropertyValue.text = "$ \(loanData.propertyValue)"
            cell.lblLoanAmount.text = "$ \(loanData.loanAmount)"
            return cell
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        let loanData = pipeLineArray[indexPath.section]
        
        if (loanData.documents == 0){
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 140 : 145
            }
            else{
                return 165
            }
        }
        else{
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 165 : 170
            }
            else{
                return 165
            }
        }
        
    }
}

extension InActivePipelineViewController: PipelineTableViewCellDelegate{
    
    func btnOptionsTapped(indexPath: IndexPath) {
        let vc = Utility.getPipelineMoreVC()
        self.presentVC(vc: vc)
    }
    
    func btnArrowTapped(indexPath: IndexPath) {
        
        if (expandableCellsIndex.contains(indexPath.section)){
            if let index = expandableCellsIndex.firstIndex(of: indexPath.section){
                expandableCellsIndex.remove(at: index)
            }
        }
        else{
            expandableCellsIndex.append(indexPath.section)
        }
        self.tblView.reloadData()
    }
}

extension InActivePipelineViewController: FiltersViewControllerDelegate{
    func getOrderby(orderBy: Int) {
        self.orderBy = orderBy
        self.pageNumber = 1
        self.dateForPage1 = Utility.getDate()
        self.getPipelineData()
    }
}
