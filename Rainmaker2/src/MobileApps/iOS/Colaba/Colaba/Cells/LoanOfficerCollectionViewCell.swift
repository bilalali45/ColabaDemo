//
//  LoanOfficerCollectionViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/09/2021.
//

import UIKit

class LoanOfficerCollectionViewCell: UICollectionViewCell {

    @IBOutlet weak var selectedView: UIView!
    @IBOutlet weak var userImageView: UIImageView!
    @IBOutlet weak var seeMoreImage: UIImageView!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var lblTenant: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        selectedView.layer.borderWidth = 2
        selectedView.layer.cornerRadius = selectedView.frame.height / 2
        selectedView.layer.borderColor = Theme.getButtonBlueColor().cgColor
        userImageView.layer.cornerRadius = userImageView.frame.height / 2
    }

}
