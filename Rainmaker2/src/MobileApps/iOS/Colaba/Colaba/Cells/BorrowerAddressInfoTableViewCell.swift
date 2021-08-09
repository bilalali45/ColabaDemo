//
//  BorrowerAddressInfoTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/08/2021.
//

import UIKit

class BorrowerAddressInfoTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var addressIcon: UIImageView!
    @IBOutlet weak var lblHeading: UILabel!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblAddressTopConstraint: NSLayoutConstraint! // 51 or 16
    @IBOutlet weak var lblDate: UILabel!
    @IBOutlet weak var lblRent: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
