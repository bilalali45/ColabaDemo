//
//  ApplicationStatusTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit

class ApplicationStatusTableViewCell: UITableViewCell {

    @IBOutlet weak var statusView: UIView!
    @IBOutlet weak var dottedLine: UIView!
    @IBOutlet weak var lblApplicationStatus: UILabel!
    @IBOutlet weak var lblTime: UILabel!
    @IBOutlet weak var iconTick: UIImageView!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        statusView.layer.cornerRadius = statusView.frame.height / 2
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
