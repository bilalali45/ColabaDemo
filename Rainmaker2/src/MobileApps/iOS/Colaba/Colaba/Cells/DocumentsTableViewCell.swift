//
//  DocumentsTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit

class DocumentsTableViewCell: UITableViewCell {

    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var lblTime: UILabel!
    @IBOutlet weak var iconStatus: UIImageView!
    @IBOutlet weak var lblStatus: UILabel!
    @IBOutlet weak var viewAttatchment1: UIView!
    @IBOutlet weak var iconAttatchment1: UIImageView!
    @IBOutlet weak var lblAttatchment1: UILabel!
    @IBOutlet weak var viewAttatchment2: UIView!
    @IBOutlet weak var iconAttachment2: UIImageView!
    @IBOutlet weak var lblAttachment2: UILabel!
    @IBOutlet weak var viewOtherAttatchment: UIView!
    @IBOutlet weak var lblOtherAttatchment: UILabel!
    @IBOutlet weak var iconNoAttatchment: UIImageView!
    @IBOutlet weak var lblNoAttatchment: UILabel!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        setupAttachmentsView(attatchmentViews: [viewAttatchment1, viewAttatchment2, viewOtherAttatchment])
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    func setupAttachmentsView(attatchmentViews: [UIView]){
        for attachmentView in attatchmentViews{
            attachmentView.layer.cornerRadius = attachmentView.frame.height / 2
            attachmentView.layer.borderWidth = 1
            attachmentView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        }
    }
    
}
