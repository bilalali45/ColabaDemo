//
//  SearchResultTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/06/2021.
//

import UIKit

class SearchResultTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblApplicationStatus: UILabel!
    @IBOutlet weak var lblApplicationNumber: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        mainView.layer.cornerRadius = 8
        mainView.layer.borderWidth = 1
        mainView.layer.borderColor = Theme.getButtonBlueColor().cgColor
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
