//
//  NotificationViewController.swift
//  Colaba
//
//  Created by Murtaza on 11/06/2021.
//

import UIKit
import LoadingPlaceholderView

class NotificationViewController: BaseViewController {

    //MARK:- Outlets and properties
    @IBOutlet weak var tblViewNotification: UITableView!
    @IBOutlet weak var btnNewNotifications: UIButton!
    
    var notificationsArray = [NotificationModel]()
    var notificationsDict = [String: Any]()
    let loadingPlaceholderView = LoadingPlaceholderView()
    var lastNotificationId = -1
    var notificationsSeenIds = [Int]()
    var notificationsReadIds = [Int]()
    
    var totalRowsInColumn1 = 2
    var totalRowsInColumn2 = 10
    
    var undoIndexPath: IndexPath?
    var undoTimer: Timer?
    var undoTimerSeconds = 5
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewNotification.register(UINib(nibName: "NotificationsTableViewCell", bundle: nil), forCellReuseIdentifier: "NotificationsTableViewCell")
        tblViewNotification.coverableCellsIdentifiers = ["NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell", "NotificationsTableViewCell"]
        tblViewNotification.loadControl = UILoadControl(target: self, action: #selector(loadMoreNotifications))
        tblViewNotification.loadControl?.heightLimit = 60
        btnNewNotifications.layer.cornerRadius = 8
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(true)
        if (notificationsArray.count == 0){
            getNotifications()
        }
    }
    
    override func viewWillDisappear(_ animated: Bool) {
        super.viewWillDisappear(true)
        self.readNotifications()
    }
    
    //MARK:- Methods and Actions
    
    @objc func loadMoreNotifications(){
        if (self.notificationsArray.count % 20 == 0){
            self.lastNotificationId = self.notificationsArray.last!.id
            self.getNotifications()
        }
        else{
            tblViewNotification.loadControl?.endLoading()
        }
    }
    
    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        scrollView.loadControl?.update()
    }
    
    @objc func undoTimerStart(){
        if undoTimerSeconds != 0 {
            undoTimerSeconds -= 1
        }
        else{
            if let timer = undoTimer{
                timer.invalidate()
                undoTimerSeconds = 5
                if (self.undoIndexPath != nil){
                    if (self.undoIndexPath!.section == 0){
                        self.totalRowsInColumn1 = self.totalRowsInColumn1 - 1
                    }
                    else{
                        self.totalRowsInColumn2 = self.totalRowsInColumn2 - 1
                    }
                    self.tblViewNotification.deleteRows(at: [self.undoIndexPath!], with: .left)
                    self.undoIndexPath = nil
                }
            }
        }
    }
    
    @IBAction func btnNewNotificationsTapped(_ sender: UIButton) {
//        totalRowsInColumn1 = 2
//        totalRowsInColumn2 = 10
//        self.tblViewNotification.reloadData()
        if (notificationsArray.count > 0){
            self.tblViewNotification.scrollToRow(at: IndexPath(row: 0, section: 0), at: .top, animated: true)
        }
        
    }
    
    //MARK:- API's
    func getNotifications(){
        
        if (lastNotificationId == -1){
            notificationsArray = [NotificationModel(), NotificationModel(), NotificationModel(), NotificationModel(), NotificationModel(), NotificationModel(), NotificationModel()]
            self.tblViewNotification.reloadData()
            loadingPlaceholderView.cover(tblViewNotification, animated: true)
        }
        
        let extraData = "pageSize=20&lastId=\(lastNotificationId)&mediumId=1"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getNotificationsList, method: .get, params: nil, extraData: extraData) { (status, result, message) in
            
            DispatchQueue.main.async {
                
                self.loadingPlaceholderView.uncover(animated: true)
                self.tblViewNotification.loadControl?.endLoading()
                
                if (status == .success){
                    
                    if (self.lastNotificationId == -1){
                        self.notificationsArray.removeAll()
                        if (result.arrayValue.count > 0){
                            let notificationArray = result.arrayValue
                            for notification in notificationArray{
                                let model = NotificationModel()
                                model.updateModelWithJSON(json: notification)
                                self.notificationsArray.append(model)
                            }
                            self.notificationsSeenIds = self.notificationsArray.map{$0.id}
                            self.seenNotifications()
                        }
                        else{
                            self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(2)) { reason in
                                
                            }
                        }
                    }
                    else{
                        if (result.arrayValue.count > 0){
                            let notificationArray = result.arrayValue
                            for notification in notificationArray{
                                let model = NotificationModel()
                                model.updateModelWithJSON(json: notification)
                                self.notificationsArray.append(model)
                            }
                            self.notificationsSeenIds = self.notificationsArray.map{$0.id}
                            self.seenNotifications()
                        }
                    }
                    self.tblViewNotification.reloadData()
                }
                else{
                    self.showPopup(message: "No Notifications", popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
    }
    
    func seenNotifications(){
        
        let params = ["ids": notificationsSeenIds]
        APIRouter.sharedInstance.executeDashboardAPIs(type: .seenNotification, method: .put, params: params) { (status, result, message) in
            
            DispatchQueue.main.async {
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationRefreshCounter), object: nil, userInfo: nil)
            }
            
        }
    }
    
    func readNotifications(){
        
        let params = ["ids": notificationsReadIds]
        APIRouter.sharedInstance.executeDashboardAPIs(type: .readNotification, method: .put, params: params) { (status, result, message) in
            
            DispatchQueue.main.async {
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationRefreshCounter), object: nil, userInfo: nil)
                
            }
            
        }
    }
    
}

extension NotificationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return section == 0 ? notificationsArray.count : totalRowsInColumn2
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "NotificationsTableViewCell", for: indexPath) as! NotificationsTableViewCell
        cell.indexPath = indexPath
        cell.delegate = self
        cell.removedNotificationView.isHidden = !(undoIndexPath == indexPath)
        
        let notification = notificationsArray[indexPath.row]
        
        var notificationText = ""
        if (notification.notificationType == "Document Submission"){
            notificationText = "\(notification.name) has submitted\ndocuments"
        }
        else{
            notificationText = ""
        }
        
        if (notificationText != ""){
            let rangeOfUsername = notificationText.range(of: "\(notification.name)")
            let remainingNotificationPart = notificationText.replacingOccurrences(of: "\(notification.name)", with: "")
            let rangeOfNotificationText = notificationText.range(of: remainingNotificationPart)
            
            let attributedNotificationText = NSMutableAttributedString(string: notificationText)
            attributedNotificationText.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getAppBlackColor()], range: notificationText.nsRange(from: rangeOfUsername!))
            attributedNotificationText.addAttributes([NSAttributedString.Key.font : Theme.getRubikMediumFont(size: 14)], range: notificationText.nsRange(from: rangeOfUsername!))
            attributedNotificationText.addAttributes([NSAttributedString.Key.foregroundColor : Theme.getButtonGreyTextColor()], range: notificationText.nsRange(from: rangeOfNotificationText!))
            attributedNotificationText.addAttributes([NSAttributedString.Key.font : Theme.getRubikRegularFont(size: 14)], range: notificationText.nsRange(from: rangeOfNotificationText!))
            
            cell.lblNotificationDetail.attributedText = attributedNotificationText
            cell.lblTime.text = Utility.timeAgoSince(notification.dateTime).replacingOccurrences(of: " ago", with: "")
            cell.readIcon.isHidden = notification.status == "Read"
            cell.notificationIcon.image = UIImage(named: notification.status == "Read" ? "NotificationDocumentReadIcon" : "NotificationDocumentIcon")
        }
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if (!notificationsReadIds.contains(notificationsArray[indexPath.row].id)){
            notificationsReadIds.append(notificationsArray[indexPath.row].id)
        }
        notificationsArray[indexPath.row].status = "Read"
        self.tblViewNotification.reloadData()
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
        if (indexPath == undoIndexPath){
            return false
        }
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteAction = UIContextualAction(style: .normal, title: "") { action, actionView, bool in
            
            if (self.undoIndexPath != nil){
                if (self.undoIndexPath!.section == 0){
                    self.totalRowsInColumn1 = self.totalRowsInColumn1 - 1
                }
                else{
                    self.totalRowsInColumn2 = self.totalRowsInColumn2 - 1
                }
                self.tblViewNotification.deleteRows(at: [self.undoIndexPath!], with: .left)
                self.undoIndexPath = nil
            }
            
            self.undoIndexPath = indexPath
            self.undoTimer?.invalidate()
            self.undoTimerSeconds = 5
            self.undoTimer = Timer.scheduledTimer(timeInterval: 1.0, target: self, selector: #selector(self.undoTimerStart), userInfo: nil, repeats: true)
            self.tblViewNotification.reloadData()
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: "SwipeToDeleteIcon")
        return UISwipeActionsConfiguration(actions: [deleteAction])
    }
}

extension NotificationViewController: NotificationsTableViewCellDelegate{
    
    func undoTapped(indexPath: IndexPath) {
        self.undoIndexPath = nil
        self.tblViewNotification.reloadData()
    }
    
}
