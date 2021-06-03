import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";

interface IncomePopUpProps {
    className?: string;
    title?: string;
    handlerCancel?: any;
    id?: number;
    closePath: string
    setShowPopup?: any,
}

const IncomeModal: React.FC<IncomePopUpProps> = ({
    className = "",
    title = "",
    closePath,
    //handlerCancel,
    children
}) => {

    const [, setFrom] = useState<string | undefined>('')
    const [isIncomeSourceHome, setIsIncomeSourceHome] = useState(false);

    const location = useLocation();

    useEffect(() => {
        setFrom(NavigationHandler.getPreviousStepPath());
    }, [])

    useEffect(() => {
        let p = location.pathname;
        setIsIncomeSourceHome(p.split('/')[p.split('/')?.length - 1] === 'IncomeSources');
    }, [location.pathname]);

    const closeWizard = () => {
        NavigationHandler.closeWizard(closePath)
    }


    return (
        <>
            <div data-testid="pop-modal" className={`income-popup animate__animated animate__slideInDown ${className}`}>
                <div className="income-popup-head">
                    {!isIncomeSourceHome && <span className="icon-back" onClick={() => {
                        if (isIncomeSourceHome) {
                            closeWizard();
                        } else {
                            NavigationHandler.moveBack();
                        }
                    }}>
                        <i className="zmdi zmdi-arrow-left"></i>
                    </span>}
                    <h4 data-testid="popup-title" dangerouslySetInnerHTML={{ __html: title }}></h4>

                    <div data-testid="popup-close" className="icon-close" onClick={closeWizard}>
                        <i className="zmdi zmdi-close"></i>
                    </div>

                </div>
                <div className="income-popup-body">
                    {children}
                </div>
            </div>
            <div className="income-popup-wrap"></div>
        </>
    );
};

export default IncomeModal;
