//
//  AssetsHeadingTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/09/2021.
//

import UIKit

class AssetsHeadingTableViewCell: UITableViewCell {

    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblAmount: UILabel!
    @IBOutlet weak var imageArrow: UIImageView!
    @IBOutlet weak var counterView: UIView!
    @IBOutlet weak var lblCounter: UILabel!
    @IBOutlet weak var separatorView: UIView!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        counterView.layer.cornerRadius = counterView.frame.height / 2
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
