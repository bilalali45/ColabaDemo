//
//  BorrowerLoanInfoTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import UIKit

class BorrowerLoanInfoTableViewCell: UITableViewCell {

    @IBOutlet weak var lblLoanPurpose: UILabel!
    @IBOutlet weak var lblLoanPayment: UILabel!
    @IBOutlet weak var lblDownPayment: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
