//
//  ApplicationStatusViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit

class ApplicationStatusViewController: UIViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var tblViewStatus: UITableView!
    
    var applicationStatus = [String]()
    var statusTime = [String]()
    
    var isShowDeniedStatus = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        applicationStatus = ["Application Started", "Application Submitted", "Processing", "Underwriting", "Approvals", "Closing", "Completed"]
        statusTime = ["05 Dec 2020, 15:23", "12 Jan 2021, 22:16", "15 Jan 2021, 18:21", "15 Jan 2021, 18:21", "", "", ""]
        tblViewStatus.register(UINib(nibName: "ApplicationStatusTableViewCell", bundle: nil), forCellReuseIdentifier: "ApplicationStatusTableViewCell")
        tblViewStatus.contentInset = UIEdgeInsets(top: 60, left: 0, bottom: 0, right: 0)
    }
    
    //MARK:- Methods and Actions

    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
}

extension ApplicationStatusViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return isShowDeniedStatus ? 4 : 7
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "ApplicationStatusTableViewCell", for: indexPath) as! ApplicationStatusTableViewCell
        
        cell.lblApplicationStatus.text = applicationStatus[indexPath.row]
        cell.lblTime.text = statusTime[indexPath.row]
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
            cell.dottedLine.isHidden = indexPath.row == 6
            cell.iconTick.isHidden = indexPath.row > 2
            cell.statusView.layer.borderColor = indexPath.row > 3 ? Theme.getSeparatorNormalColor().cgColor : Theme.getButtonBlueColor().cgColor
            cell.statusView.backgroundColor = indexPath.row > 3 ? .white : Theme.getButtonBlueColor()
            cell.dottedLine.backgroundColor = indexPath.row > 2 ? Theme.getSeparatorNormalColor() : Theme.getButtonBlueColor()
            cell.lblApplicationStatus.textColor = Theme.getAppBlackColor()
            if (indexPath.row > 2){
                cell.dottedLine.backgroundColor = .clear
                cell.dottedLine.createDottedLine(width: 2.0, color: Theme.getSeparatorNormalColor().cgColor)
            }
            else{
                cell.dottedLine.backgroundColor = Theme.getButtonBlueColor()
            }
        }
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        isShowDeniedStatus = !isShowDeniedStatus
        if (isShowDeniedStatus){
            applicationStatus = ["Application Started", "Application Submitted", "Processing", "Application Denied"]
        }
        else{
            applicationStatus = ["Application Started", "Application Submitted", "Processing", "Underwriting", "Approvals", "Closing", "Completed"]
        }
        self.tblViewStatus.reloadData()
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 75
    }
}
