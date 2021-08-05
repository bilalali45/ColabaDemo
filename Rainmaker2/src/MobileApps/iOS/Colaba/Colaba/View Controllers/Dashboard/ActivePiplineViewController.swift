//
//  ActivePiplineViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 28/06/2021.
//

import UIKit
import LoadingPlaceholderView
import RealmSwift

class ActivePipelineViewController: BaseViewController {

    //MARK:- Outlets and properties
    @IBOutlet weak var assignToMeSwitch: UISwitch!
    @IBOutlet weak var btnFilters: UIButton!
    @IBOutlet weak var tblView: UITableView!
    
    var expandableCellsIndex = [Int]()
    let loadingPlaceholderView = LoadingPlaceholderView()
    var pipeLineArray = [ActiveLoanModel]()
    
    var dateForPage1 = ""
    var pageNumber = 1
    var isAssignToMe = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        pipeLineArray = ActiveLoanModel.getAllActiveLoans()
        tblView.register(UINib(nibName: "PipelineTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineTableViewCell")
        tblView.register(UINib(nibName: "PipelineDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "PipelineDetailTableViewCell")
        tblView.coverableCellsIdentifiers = ["PipelineDetailTableViewCell", "PipelineDetailTableViewCell", "PipelineDetailTableViewCell", "PipelineDetailTableViewCell"]
        tblView.loadControl = UILoadControl(target: self, action: #selector(loadMoreResult))
        tblView.loadControl?.heightLimit = 60
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        isAssignToMe = UserDefaults.standard.bool(forKey: kIsAssignToMe)
        assignToMeSwitch.setOn(isAssignToMe, animated: true)
        refreshLoanData()
    }
    
    //MARK:- Methods and Actions
    
    @objc func refreshLoanData(){
        self.pageNumber = 1
        self.dateForPage1 = Utility.getDate()
        self.expandableCellsIndex.removeAll()
        self.getPipelineData()
    }
    
    @objc func loadMoreResult(){
        if (self.pipeLineArray.count % 20 == 0){
            self.pageNumber = self.pageNumber + 1
            self.getPipelineData()
        }
        else{
            tblView.loadControl?.endLoading()
        }
    }
    
    @objc func showFiltersPopup(){
        let vc = Utility.getFiltersVC()
        vc.delegate = self
        self.present(vc, animated: false, completion: nil)
    }
    
    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        scrollView.loadControl?.update()
    }
    
    @IBAction func btnFilterTapped(_ sender: UIButton) {
        showFiltersPopup()
    }
    
    @IBAction func assignToMeSwitchChanged(_ sender: UISwitch) {
        UserDefaults.standard.setValue(sender.isOn ? true : false, forKey: kIsAssignToMe)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationAssignToMeSwitchChanged), object: nil, userInfo: nil)
        refreshLoanData()
    }
    
    //MARK:- API's
    
    func getPipelineData(){
        
        if (pageNumber == 1){
            self.loadingPlaceholderView.cover(self.tblView, animated: true)
        }
        
        let extraData = "dateTime=\(dateForPage1)&pageNumber=\(pageNumber)&pageSize=20&loanFilter=1&orderBy=\(sortingFilter)&assignedToMe=\(assignToMeSwitch.isOn ? true : false)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getPipelineList, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                self.tblView.loadControl?.endLoading()
                if (status == .success){
                    
                    if (self.pageNumber == 1){
                        self.pipeLineArray.removeAll()
                        let realm = try! Realm()
                        realm.beginWrite()
                        realm.delete(realm.objects(ActiveLoanModel.self))
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = ActiveLoanModel()
                                model.updateModelWithJSON(json: loan)
                                realm.add(model)
                            }
                        }
                        else{
                            self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(2)) { reason in
                                
                            }
                        }
                        try! realm.commitWrite()
                        self.pipeLineArray = Array(realm.objects(ActiveLoanModel.self))
                        self.tblView.reloadData()
                        if (self.pipeLineArray.count > 0){
                            self.tblView.scrollToRow(at: IndexPath(row: 0, section: 0), at: .top, animated: true)
                        }
                    }
                    else{
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = ActiveLoanModel()
                                model.updateModelWithJSON(json: loan)
                                self.pipeLineArray.append(model)
                            }
                            self.tblView.reloadData()
                        }
                    }
                    
                }
                else if (status == .internetError){
                    self.loadingPlaceholderView.uncover(animated: true)
                    self.tblView.reloadData()
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(3)) { reason in
                        self.loadingPlaceholderView.uncover(animated: true)
                    }
                }
                else{
                    self.pipeLineArray.removeAll()
                    self.tblView.reloadData()
                    self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
}

extension ActivePipelineViewController: UITableViewDataSource, UITableViewDelegate{
    
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
            
            cell.mainViewHeightConstraint.constant = loanData.documents == 0 ? 115 : 137
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
            cell.lblPropertyValue.text = loanData.propertyValue.withCommas().replacingOccurrences(of: ".00", with: "")
            cell.lblLoanAmount.text = loanData.loanAmount.withCommas().replacingOccurrences(of: ".00", with: "")
            return cell
        }
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let loanApplication = pipeLineArray[indexPath.section]
        let vc = Utility.getLoanDetailVC()
        vc.loanApplicationId = loanApplication.loanApplicationId
        vc.borrowerName = "\(loanApplication.firstName) \(loanApplication.lastName)"
        vc.loanPurpose = loanApplication.loanPurpose
        vc.phoneNumber = loanApplication.cellNumber
        vc.email = loanApplication.email
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        let loanData = pipeLineArray[indexPath.section]
        
        if (loanData.documents == 0){
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 120 : 135
            }
            else{
                return UITableView.automaticDimension
            }
        }
        else{
            if (indexPath.row == 0){
                return expandableCellsIndex.contains(indexPath.section) ? 141 : 160
            }
            else{
                return UITableView.automaticDimension
            }
        }
        
    }
}

extension ActivePipelineViewController: PipelineTableViewCellDelegate{
    
    func btnOptionsTapped(indexPath: IndexPath) {
        let vc = Utility.getPipelineMoreVC()
        let loanData = pipeLineArray[indexPath.section]
        vc.userFullName = "\(loanData.firstName) \(loanData.lastName)"
        vc.coBorrowers = loanData.coBorrowerCount
        vc.phoneNumber = loanData.cellNumber
        vc.email = loanData.email
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

extension ActivePipelineViewController: FiltersViewControllerDelegate{
    func getOrderby(orderBy: Int) {
        sortingFilter = orderBy
        refreshLoanData()
    }
}
