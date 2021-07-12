//
//  BorrowerApplicationStatusButtonTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import UIKit

class BorrowerApplicationStatusButtonTableViewCell: UITableViewCell {

    @IBOutlet weak var applicationStatusView: UIView!
    @IBOutlet weak var lblApplicationStatus: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        applicationStatusView.layer.cornerRadius = 8
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
