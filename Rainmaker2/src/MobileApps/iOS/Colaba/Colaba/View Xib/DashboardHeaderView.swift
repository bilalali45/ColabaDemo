//
//  DashboardHeaderView.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 07/07/2021.
//

import UIKit

class DashboardHeaderView: UIView {

    @IBOutlet weak var switchAssignToMe: UISwitch!
    @IBOutlet weak var btnFilters: UIButton!
    var isAssignToMe = false
    
    override func awakeFromNib() {
        NotificationCenter.default.addObserver(self, selector: #selector(setAssignToMeSwitch), name: NSNotification.Name(rawValue: kNotificationAssignToMeSwitchChanged), object: nil)
        setAssignToMeSwitch()
    }
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
    }
        
    required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        
    }
    
    @objc func setAssignToMeSwitch(){
        isAssignToMe = UserDefaults.standard.bool(forKey: kIsAssignToMe)
        switchAssignToMe.setOn(isAssignToMe, animated: true)
    }
    
    @IBAction func switchAssignToMeChanged(_ sender: UISwitch){
        
    }
    
    @IBAction func btnFiltersTapped(_ sender: UIButton){
        
    }
}
