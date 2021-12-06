//
//  ApplicationStatusViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit

class ApplicationStatusViewController: BaseViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var tblViewStatus: UITableView!
    
    var loanApplicationId = 0
    
    var milestones = [ApplicationStatusModel]()
    
    var isShowDeniedStatus = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewStatus.register(UINib(nibName: "ApplicationStatusTableViewCell", bundle: nil), forCellReuseIdentifier: "ApplicationStatusTableViewCell")
        tblViewStatus.contentInset = UIEdgeInsets(top: 60, left: 0, bottom: 0, right: 0)
        getApplicationStatus()
    }
    
    //MARK:- Methods and Actions

    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    //MARK:- API's
    
    func getApplicationStatus(){
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=1047"
        
        APIRouter.sharedInstance.executeAPI(type: .getApplicationStatus, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let applicationMilestones = result["data"].arrayValue
                    for milestone in applicationMilestones{
                        let model = ApplicationStatusModel()
                        model.updateModelWithJSON(json: milestone)
                        self.milestones.append(model)
                    }
                    self.tblViewStatus.reloadData()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}

extension ApplicationStatusViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return milestones.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ApplicationStatusTableViewCell", for: indexPath) as! ApplicationStatusTableViewCell
        
        let milestone = milestones[indexPath.row]
        cell.lblApplicationStatus.text = milestone.name
        cell.lblTime.text = Utility.getApplicationStatusDate(milestone.createdDate)
        cell.statusView.layer.borderWidth = 2
        
        if (isShowDeniedStatus){
            cell.iconTick.isHidden = false
            cell.iconTick.image = UIImage(named: indexPath.row == 3 ? "Cross-Icon" : "TickIcon")
            cell.dottedLine.isHidden = indexPath.row == 3
            cell.statusView.layer.borderColor = indexPath.row > 2 ? Theme.getSeparatorErrorColor().cgColor : Theme.getButtonBlueColor().cgColor
            cell.statusView.backgroundColor = indexPath.row > 2 ? .white : Theme.getButtonBlueColor()
            cell.lblApplicationStatus.textColor = indexPath.row > 2 ? Theme.getSeparatorErrorColor() : Theme.getAppBlackColor()
        }
        else{
            cell.dottedLine.isHidden = indexPath.row == milestones.count - 1
            cell.iconTick.isHidden = !milestone.isCurrent
            cell.statusView.layer.borderColor = milestone.isCurrent ? Theme.getButtonBlueColor().cgColor : Theme.getSeparatorNormalColor().cgColor
            cell.statusView.backgroundColor = milestone.isCurrent ? Theme.getButtonBlueColor() : .white
            cell.dottedLine.backgroundColor = milestone.isCurrent ?  Theme.getButtonBlueColor() : Theme.getSeparatorNormalColor()
            cell.lblApplicationStatus.textColor = Theme.getAppBlackColor()
            if (milestone.isCurrent){
                cell.dottedLine.backgroundColor = Theme.getButtonBlueColor()
            }
            else{
                cell.dottedLine.backgroundColor = .clear
                cell.dottedLine.createDottedLine(width: 2.0, color: Theme.getSeparatorNormalColor().cgColor)
            }
        }
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
//        isShowDeniedStatus = !isShowDeniedStatus
//        if (isShowDeniedStatus){
//            applicationStatus = ["Application Started", "Application Submitted", "Processing", "Application Denied"]
//        }
//        else{
//            applicationStatus = ["Application Started", "Application Submitted", "Processing", "Underwriting", "Approvals", "Closing", "Completed"]
//        }
//        self.tblViewStatus.reloadData()
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 75
    }
}
