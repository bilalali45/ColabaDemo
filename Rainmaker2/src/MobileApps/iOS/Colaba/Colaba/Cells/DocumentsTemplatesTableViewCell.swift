//
//  DocumentsTemplatesTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit

protocol DocumentsTemplatesTableViewCellDelegate: AnyObject {
    func templateSelect(indexPath: IndexPath)
    func infoTapped(indexPath: IndexPath)
}

class DocumentsTemplatesTableViewCell: UITableViewCell {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var stackViewTemplateName: UIStackView!
    @IBOutlet weak var btnCheckbox: UIButton!
    @IBOutlet weak var lblTemplateName: UILabel!
    @IBOutlet weak var btnInfo: UIButton!
    
    var indexPath = IndexPath()
    weak var delegate: DocumentsTemplatesTableViewCellDelegate?
    
    override func awakeFromNib() {
        super.awakeFromNib()
        stackViewTemplateName.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(stackViewTemplateTapped)))
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    //MARK:- Methods and Actions
    
    @objc func stackViewTemplateTapped(){
//        isSelectedTemplate = !isSelectedTemplate
//        btnCheckbox.setImage(UIImage(named: isSelectedTemplate ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
//        lblTemplateName.font = isSelectedTemplate ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        self.delegate?.templateSelect(indexPath: indexPath)
    }
    
    @IBAction func btnInfoTapped(_ sender: UIButton) {
        self.delegate?.infoTapped(indexPath: indexPath)
    }
    
    
}
