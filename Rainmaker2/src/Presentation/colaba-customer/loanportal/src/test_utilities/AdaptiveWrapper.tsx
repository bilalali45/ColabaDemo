import React, { useEffect } from "react";

export const AdaptiveWrapper = ({ children }) => {
    useEffect(() => {
        global.innerWidth = 767;
        global.dispatchEvent(new Event('resize'));
    }, [])
    return <>{children}</>
}
