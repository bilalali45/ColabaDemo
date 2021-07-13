//
//  BorrowerOverviewTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import UIKit

class BorrowerOverviewTableViewCell: UITableViewCell {

    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var lblCoBorrowerName: UILabel!
    @IBOutlet weak var lblCoBorrowerHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblCoBorrowerTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblLoanNo: UILabel!
    @IBOutlet weak var lblLoanNoHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var lblLoanNoTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var iconByte: UIImageView!
    @IBOutlet weak var lblByte: UILabel!
    @IBOutlet weak var lblByteTopConstraint: NSLayoutConstraint!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
