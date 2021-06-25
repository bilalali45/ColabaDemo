//
//  NotificationsTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/06/2021.
//

import UIKit

protocol NotificationsTableViewCellDelegate: AnyObject {
    func undoTapped(indexPath: IndexPath)
}

class NotificationsTableViewCell: UITableViewCell {

    @IBOutlet weak var readIcon: UIView!
    @IBOutlet weak var notificationIcon: UIImageView!
    @IBOutlet weak var lblNotificationDetail: UILabel!
    @IBOutlet weak var lblTime: UILabel!
    @IBOutlet weak var removedNotificationView: UIView!
    @IBOutlet weak var btnUndo: UIButton!
    
    var indexPath = IndexPath()
    weak var delegate: NotificationsTableViewCellDelegate?
    
    override func awakeFromNib() {
        super.awakeFromNib()
        readIcon.layer.cornerRadius = readIcon.frame.height / 2
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    @IBAction func btnUndoTapped(_ sender: UIButton){
        self.delegate?.undoTapped(indexPath: indexPath)
    }
}
