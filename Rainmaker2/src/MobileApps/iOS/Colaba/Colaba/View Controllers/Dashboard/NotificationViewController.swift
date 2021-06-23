//
//  NotificationViewController.swift
//  Colaba
//
//  Created by Murtaza on 11/06/2021.
//

import UIKit

class NotificationViewController: BaseViewController {

    //MARK:- Outlets and properties
    @IBOutlet weak var tblViewNotification: UITableView!
    @IBOutlet weak var btnNewNotifications: UIButton!
    
    var totalRowsInColumn1 = 2
    var totalRowsInColumn2 = 10
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewNotification.register(UINib(nibName: "NotificationsTableViewCell", bundle: nil), forCellReuseIdentifier: "NotificationsTableViewCell")
        btnNewNotifications.layer.cornerRadius = 8
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnNewNotificationsTapped(_ sender: UIButton) {
        totalRowsInColumn1 = 2
        totalRowsInColumn2 = 10
        self.tblViewNotification.reloadData()
        self.tblViewNotification.scrollToRow(at: IndexPath(row: 0, section: 0), at: .top, animated: true)
    }
    
}

extension NotificationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 2
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return section == 0 ? totalRowsInColumn1 : totalRowsInColumn2
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "NotificationsTableViewCell", for: indexPath) as! NotificationsTableViewCell
        
        let notificationText = "Richard Glenn has submitted\ndocuments"
        let rangeOfUsername = notificationText.range(of: "Richard Glenn")
        let rangeOfNotificationText = notificationText.range(of: " has submitted\ndocuments")
        
        let attributedNotificationText = NSMutableAttributedString(string: notificationText)
        attributedNotificationText.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor()], range: notificationText.nsRange(from: rangeOfUsername!))
        attributedNotificationText.addAttributes([NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 14)], range: notificationText.nsRange(from: rangeOfUsername!))
        attributedNotificationText.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getButtonGreyTextColor()], range: notificationText.nsRange(from: rangeOfNotificationText!))
        attributedNotificationText.addAttributes([NSAttributedString.Key.font : Theme.getRubikRegularFont(size: 14)], range: notificationText.nsRange(from: rangeOfNotificationText!))
        
        cell.lblNotificationDetail.attributedText = attributedNotificationText
        
        if (indexPath.section == 0){
            cell.notificationIcon.image = UIImage(named: "NotificationDocumentIcon")
            cell.readIcon.isHidden = false
        }
        else{
            if (indexPath.row == 0 || indexPath.row == 1){
                cell.notificationIcon.image = UIImage(named: "NotificationDocumentIcon")
                cell.readIcon.isHidden = false
            }
            else{
                cell.notificationIcon.image = UIImage(named: "NotificationDocumentReadIcon")
                cell.readIcon.isHidden = true
            }
        }
        return cell
    }
    
    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
        return 60
    }
    
    func tableView(_ tableView: UITableView, willDisplayHeaderView view: UIView, forSection section: Int) {
        if let headerView = view as? UITableViewHeaderFooterView {
            headerView.contentView.backgroundColor = Theme.getDashboardBackgroundColor()
            headerView.backgroundView?.backgroundColor = .clear
            headerView.textLabel?.font = Theme.getRubikMediumFont(size: 14)
            headerView.textLabel?.textColor = Theme.getButtonGreyTextColor()
        }
    }
    
    func tableView(_ tableView: UITableView, titleForHeaderInSection section: Int) -> String? {
        return section == 0 ? "Today" : "Yesterday"
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteAction = UIContextualAction(style: .normal, title: "") { action, actionView, bool in
            if (indexPath.section == 0){
                self.totalRowsInColumn1 = self.totalRowsInColumn1 - 1
            }
            else{
                self.totalRowsInColumn2 = self.totalRowsInColumn2 - 1
            }
            self.tblViewNotification.deleteRows(at: [indexPath], with: .left)
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: "SwipeToDeleteIcon")
        return UISwipeActionsConfiguration(actions: [deleteAction])
    }
}
