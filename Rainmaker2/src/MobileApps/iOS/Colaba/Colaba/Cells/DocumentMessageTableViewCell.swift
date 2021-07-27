//
//  DocumentMessageTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/07/2021.
//

import UIKit

class DocumentMessageTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblMessage: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
