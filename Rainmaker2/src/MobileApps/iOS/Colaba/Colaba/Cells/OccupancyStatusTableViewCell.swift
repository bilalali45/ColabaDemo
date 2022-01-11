//
//  OccupancyStatusTableViewCell.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 09/11/2021.
//

import UIKit

class OccupancyStatusTableViewCell: UITableViewCell {

    @IBOutlet weak var occupancyStatusView: UIView!
    @IBOutlet weak var lblCoBorrowerName: UILabel!
    @IBOutlet weak var occupyingStackView: UIStackView!
    @IBOutlet weak var btnOccupying: UIButton!
    @IBOutlet weak var lblOccupying: UILabel!
    @IBOutlet weak var nonOccupyingStackView: UIStackView!
    @IBOutlet weak var btnNonOccupying: UIButton!
    @IBOutlet weak var lblNonOccupying: UILabel!
    
    var borrower = CoBorrowerOccupancyModel()
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        occupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(occupyingStackViewTapped)))
        occupyingStackView.layer.cornerRadius = 8
        occupyingStackView.layer.borderWidth = 1
        occupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        nonOccupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonOccupyingStackViewTapped)))
        nonOccupyingStackView.layer.cornerRadius = 8
        nonOccupyingStackView.layer.borderWidth = 1
        nonOccupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
    }
    
    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
    func setData(){
        lblCoBorrowerName.text = borrower.borrowerFullName
        if (borrower.willLiveInSubjectProperty){
            occupyingStackViewTapped()
        }
        else{
            nonOccupyingStackViewTapped()
        }
    }
    
    func updateOccupancyStatus(willOccupy: Bool){
        let params = ["Id": borrower.borrowerId,
                      "willLiveInSubjectProperty": willOccupy] as [String: Any]
        
        APIRouter.sharedInstance.executeAPI(type: .updateCoBorrowerOccupancyStatus, method: .post, params: params) { status, result, message in
            
        }
    }
    
    @objc func occupyingStackViewTapped(){
        btnOccupying.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
        lblOccupying.font = Theme.getRubikSemiBoldFont(size: 14)
        lblOccupying.textColor = Theme.getAppBlackColor()
        btnNonOccupying.setImage(UIImage(named: "radioUnslected"), for: .normal)
        lblNonOccupying.font = Theme.getRubikRegularFont(size: 14)
        lblNonOccupying.textColor = Theme.getAppGreyColor()
        occupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        occupyingStackView.dropShadowToCollectionViewCell()
        nonOccupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        nonOccupyingStackView.removeShadow()
        updateOccupancyStatus(willOccupy: true)
    }
    
    @objc func nonOccupyingStackViewTapped(){
        btnNonOccupying.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
        lblNonOccupying.font = Theme.getRubikSemiBoldFont(size: 14)
        lblNonOccupying.textColor = Theme.getAppBlackColor()
        btnOccupying.setImage(UIImage(named: "radioUnslected"), for: .normal)
        lblOccupying.font = Theme.getRubikRegularFont(size: 14)
        lblOccupying.textColor = Theme.getAppGreyColor()
        nonOccupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        nonOccupyingStackView.dropShadowToCollectionViewCell()
        occupyingStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        occupyingStackView.removeShadow()
        updateOccupancyStatus(willOccupy: false)
    }
}
