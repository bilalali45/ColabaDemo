import React, { useState, useEffect } from 'react';


interface TooltipTitleProps {
    className?: string;
    title?: string;
    handlerClick?: () => void;
}

const TooltipTitle: React.FC<TooltipTitleProps> = ({ className = '', title = '', handlerClick }) => {

    const [state, setstate] = useState(false);

    useEffect(() => {        
        setTimeout(()=>setstate(!state),1000);        
    }, [])

    return (
        <div data-testid="tooltip" className={`c-tooltip-title-wrap ${className}`} onClick={handlerClick} >
            <div className="t-anim-wrap">
                <div className="c-tooltip-title">
                    {(state == false) ?
                         (
                            <>
                            <svg className="text-loader-zigzag" xmlns="http://www.w3.org/2000/svg" width="32.678" height="13.751" viewBox="0 0 32.678 13.751">
                            <g id="Group_1" data-name="Group 1" transform="translate(-54 -62)">
                                <rect id="Rectangle_357" data-name="Rectangle 357" width="3.127" height="14.073" opacity="1" rx="1.564" transform="translate(61.036 62) rotate(30)" className="fillprimary" fill="#4484f4">
                                    <animate attributeType="CSS" attributeName="opacity" from="1" to="1" dur="800ms" repeatCount="indefinite" fill="freeze" begin="0ms" />
                                </rect>
                                <rect id="Rectangle_358" data-name="Rectangle 358" width="3.127" height="14.073" opacity="0" rx="1.564" transform="translate(59.734 63.564) rotate(-30)" className="fillprimary" fill="#4484f4">
                                    <animate attributeType="CSS" attributeName="opacity" from="0.2" to="1" dur="800ms" repeatCount="indefinite" fill="freeze" begin="100ms" />
                                </rect>
                                <rect id="Rectangle_359" data-name="Rectangle 359" width="3.127" height="14.073" opacity="0" rx="1.564" transform="translate(72.503 62) rotate(30)" className="fillprimary" fill="#4484f4">
                                    <animate attributeType="CSS" attributeName="opacity" from="0.2" to="1" dur="800ms" repeatCount="indefinite" fill="freeze" begin="200ms" />
                                </rect>
                                <rect id="Rectangle_360" data-name="Rectangle 360" width="3.127" height="14.073" opacity="0" rx="1.564" transform="translate(71.2 63.564) rotate(-30)" className="fillprimary" fill="#4484f4">
                                    <animate attributeType="CSS" attributeName="opacity" from="0.2" to="1" dur="800ms" repeatCount="indefinite" fill="freeze" begin="300ms" />
                                </rect>
                                <rect id="Rectangle_361" data-name="Rectangle 361" width="3.127" height="14.073" opacity="0" rx="1.564" transform="translate(83.97 62) rotate(30)" className="fillprimary" fill="#4484f4">
                                    <animate attributeType="CSS" attributeName="opacity" from="0.2" to="1" dur="800ms" repeatCount="indefinite" fill="freeze" begin="400ms" />
                                </rect>
                            </g>
                        </svg>
                            </>
                        )
                        :
                        <span className="c-tooltip-title-text" title={title} dangerouslySetInnerHTML={{__html: title }}></span>
                    }</div>
            </div>
        </div>
    )
}

export default TooltipTitle;
