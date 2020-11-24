import React, { useEffect } from "react";

export const AdaptiveWrapper = ({ children }) => {
    useEffect(() => {
        global.innerWidth = 767;
        global.dispatchEvent(new Event('resize'));
        console.log('in here resize -------------------------------', window.innerHeight, window.innerWidth);
    }, [])
    return <>{children}</>
}
