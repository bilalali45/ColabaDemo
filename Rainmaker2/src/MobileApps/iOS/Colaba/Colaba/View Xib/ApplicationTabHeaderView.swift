//
//  ApplicationTabHeaderView.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 20/07/2021.
//

import UIKit

class ApplicationTabHeaderView: UIView{
    
    @IBOutlet weak var icon: UIImageView!
    @IBOutlet weak var lblTitle: UILabel!
    
    override func awakeFromNib() {
        
    }
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
    }
        
    required init?(coder aDecoder: NSCoder) {
        super.init(coder: aDecoder)
        
    }
    
}
