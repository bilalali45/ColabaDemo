//
//  BorrowerAddressTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import UIKit

class BorrowerAddressTableViewCell: UITableViewCell {

    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblFamilyType: UILabel!
    @IBOutlet weak var lblPropertyValue: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
