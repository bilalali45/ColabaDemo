//
//  DocumentTypeTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 04/10/2021.
//

import UIKit

class DocumentTypeTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var stackView: UIStackView!
    @IBOutlet weak var lblTitle: UILabel!
    @IBOutlet weak var lblDetail: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        mainView.layer.cornerRadius = 6
        mainView.layer.borderWidth = 1
        mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        mainView.dropShadowToCollectionViewCell()
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
