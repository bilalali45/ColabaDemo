//
//  DemographicQuestionsTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/09/2021.
//

import UIKit

protocol DemographicQuestionsTableViewCellDelegate: Any {
    func otherDetailViewTapped(indexPath: IndexPath)
}

class DemographicQuestionsTableViewCell: UITableViewCell {

    @IBOutlet weak var stackViewQuestion: UIStackView!
    @IBOutlet weak var btnCheckBox: UIButton!
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var stackViewLeadingConstraint: NSLayoutConstraint! //20 or 54
    @IBOutlet weak var lblHeading: UILabel!
    @IBOutlet weak var otherView: UIView!
    @IBOutlet weak var txtfieldOther: ColabaTextField!
    @IBOutlet weak var otherDetailView: UIView!
    @IBOutlet weak var otherDetailViewHeightConstraint: NSLayoutConstraint! //58 or 84
    @IBOutlet weak var lblDetail: UILabel!
    @IBOutlet weak var lblOther: UILabel!
    
    var isQuestionSelected = false
    var indexPath = IndexPath()
    var delegate: DemographicQuestionsTableViewCellDelegate?
    
    override func awakeFromNib() {
        super.awakeFromNib()
//        otherView.layer.cornerRadius = 6
//        otherView.layer.borderWidth = 1
//        otherView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
//        otherView.dropShadowToCollectionViewCell()
        otherDetailView.layer.cornerRadius = 6
        otherDetailView.layer.borderWidth = 1
        otherDetailView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        otherDetailView.dropShadowToCollectionViewCell()
        otherDetailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(otherDetailViewTapped)))
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    @objc func otherDetailViewTapped(){
        self.delegate?.otherDetailViewTapped(indexPath: indexPath)
    }
    
}
