//
//  DemographicQuestionsTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/09/2021.
//

import UIKit

class DemographicQuestionsTableViewCell: UITableViewCell {

    @IBOutlet weak var stackViewQuestion: UIStackView!
    @IBOutlet weak var btnCheckBox: UIButton!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var stackViewLeadingConstraint: NSLayoutConstraint! //20 or 54
    @IBOutlet weak var lblHeading: UILabel!
    @IBOutlet weak var otherView: UIView!
    @IBOutlet weak var txtfieldOther: UITextField!
    
    var isQuestionSelected = false
    
    override func awakeFromNib() {
        super.awakeFromNib()
        otherView.layer.cornerRadius = 6
        otherView.layer.borderWidth = 1
        otherView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        otherView.dropShadowToCollectionViewCell()
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
