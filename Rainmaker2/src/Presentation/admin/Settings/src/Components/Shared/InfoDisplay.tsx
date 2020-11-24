import React, {Children, FunctionComponent, useState, useEffect, useRef} from 'react'
import { SVGInfo } from './SVG'
import { ToolTipData } from './ToolTipData'


interface InfoDisplayProps{
    setInnerHTML?:React.ReactNode,
    handleHover?: (e:any) => any,
    handleHoverOut?: (e:any) => any,
    checkState?:any,
    className?:any,
    testId?:any
}

const InfoDisplayMarkup:FunctionComponent<InfoDisplayProps> = ({setInnerHTML,handleHover,handleHoverOut,checkState,className,testId,children}) => {

    const [position, setPosition] = useState<any>({x:0,y:0});
    const [getOpacity, setOpacity] = useState<any>(0);
    let tooltipIcon = useRef<any>();
    let tooltipDropDown = useRef<any>();

    let styles:any = {opacity: getOpacity, position:'fixed',top: position.y, left:position.x}; //,

    let checkStyle = async() => {
        let ddWidth = tooltipDropDown.current?.getBoundingClientRect()?.width;

        setPosition({
            ...position,
            x:tooltipIcon.current?.getBoundingClientRect().right - ddWidth,
            y:tooltipIcon.current?.getBoundingClientRect().y + 20
        });
        await setOpacity(1);
    }

    return (
        <span  className={`info-display ${className ? className : ''}`} onMouseLeave={e=>setOpacity(0)} onMouseOver={()=>{checkStyle()}}>
            <span data-testid="toolTip" className="info-display--icon" ref={tooltipIcon} onMouseLeave={handleHoverOut} onMouseEnter={handleHover}>{children}</span>
            {checkState == true &&
                <div data-testid={`infoDropdown`} ref={tooltipDropDown} style={styles} className="info-display--dropdown">
                    {setInnerHTML}
                </div>
            }            
        </span>
    )
}

interface infoDisplayProps {
    tooltipType:any,
    testId?:any,
}

const InfoDisplay = ({tooltipType,testId}:any) => {

    const [tooltip, setTooltip] = useState(false);
    
    const checkTooltip = (e:any,type:any) => {
        e.preventDefault();
        e.stopPropagation();
        setTooltip(type);
    }

    useEffect(()=>{
       document.addEventListener('click', ()=> setTooltip(false));      
       return (()=>{
           document.removeEventListener('click',()=>{});
       })
    },[]);

    return(
        <InfoDisplayMarkup testId={testId} checkState={tooltip} handleHover={e=>{checkTooltip(e,true)}} handleHoverOut={e=>checkTooltip(e,false)} setInnerHTML={<ToolTipData type={tooltipType} />}><SVGInfo/></InfoDisplayMarkup>
    )
}

export default InfoDisplay;
