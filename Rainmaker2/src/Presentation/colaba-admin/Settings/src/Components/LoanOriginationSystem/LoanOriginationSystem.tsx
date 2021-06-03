import React, { useContext, useState } from 'react'

import { LoanOriginationSystemBody } from './_LoanOriginationSystem/LoanOriginationSystemBody'
import { LoanOriginationSystemHeader } from './_LoanOriginationSystem/LoanOriginationSystemHeader'
import {LoadOriginationMenu} from '../../Utils/helpers/Enums';

type Props = {
    backHandler?: Function
}

 const LoanOriginationSystem = ({backHandler}: Props) => {

     const [Nav, setNav] = useState(LoadOriginationMenu.Users);

     const changeNavHandler = (ele:number) => {
         setNav(ele);
     }

    return (
        <div className="settings-loadOrigination">
            <LoanOriginationSystemHeader />
            <LoanOriginationSystemBody
                navigation = {Nav}
                changeNav = {changeNavHandler}
            />
        </div>
    )
}

export default LoanOriginationSystem;
