import React, {Children, FunctionComponent, useState, useEffect, useRef} from 'react'
import { SVGInfo } from '../SVG'
import { ToolTipData } from './ToolTipData'


interface InfoDisplayProps{
    setInnerHTML?:React.ReactNode,
    //handleClick?: (e: any) => any,
    handleHover?: (e:any) => any,
    handleHoverOut?: (e:any) => any,
    checkState?:any,
    className?:any
}

const InfoDisplayMarkup:FunctionComponent<InfoDisplayProps> = ({setInnerHTML,handleHover,handleHoverOut,checkState,className,children}) => {

    const [position, setPosition] = useState<any>({x:0,y:0});
    const [getOpacity, setOpacity] = useState<any>(0);
    let tooltipIcon = useRef<any>();
    let tooltipDropDown = useRef<any>();

    const displayDropdown = () => {
        setTimeout(() => {
            setOpacity(1)
        }, 100);
    }


    useEffect(()=>{
        let ddWidth = tooltipDropDown.current?.getBoundingClientRect()?.width;

        setPosition({
            ...position,
            x:tooltipIcon.current?.getBoundingClientRect().right - ddWidth,
            y:tooltipIcon.current?.getBoundingClientRect().y + 20
        })
    },[checkState])


    return (
        <span data-testid="toolTip" className={`info-display ${className ? className : ''}`} onMouseLeave={e=>setOpacity(0)} onMouseOver={()=>{displayDropdown()}}>
            <span className="info-display--icon" ref={tooltipIcon} onMouseLeave={handleHoverOut} onMouseOver={handleHover}>{children}</span>
            {checkState == true &&
                <div ref={tooltipDropDown} data-testid="toolTip-dropdown" style={{position:'fixed',top: position.y, left:position.x, opacity: getOpacity}} className="info-display--dropdown">
                    {setInnerHTML}
                </div>
            }            
        </span>
    )
}

interface infoDisplayProps {
    tooltipType:any
}

const InfoDisplay = ({tooltipType}:any) => {

    const [tooltip, setTooltip] = useState(false);
    
    const checkTooltip = (e:any,type:any) => {
        e.preventDefault();
        e.stopPropagation();
        setTooltip(type);
    }

    useEffect(()=>{
       document.body.addEventListener('click', ()=> setTooltip(false));      
    },[]);

    return(
        <InfoDisplayMarkup checkState={tooltip} handleHover={e=>{checkTooltip(e,true)}} handleHoverOut={e=>checkTooltip(e,false)} setInnerHTML={<ToolTipData type={tooltipType} />}><SVGInfo/></InfoDisplayMarkup>
    )
}

export default InfoDisplay;
