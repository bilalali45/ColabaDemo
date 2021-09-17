//
//  TitleConveyanceViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 17/09/2021.
//

import UIKit

class TitleConveyanceViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var lblQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    
    var isYes: Bool?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
    }
    
    //MARK:- Methods
    
    @objc func yesStackViewTapped(){
        isYes = true
        changeStatus()
    }
    
    @objc func noStackViewTapped(){
        isYes = false
        changeStatus()
    }
    
    func changeStatus(){
        if let ansYes = isYes{
            btnYes.setImage(UIImage(named: ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = ansYes ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            btnNo.setImage(UIImage(named: !ansYes ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblNo.font = !ansYes ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        }
    }
    
}
