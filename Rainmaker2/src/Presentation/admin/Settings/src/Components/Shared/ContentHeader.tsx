import React from 'react';
import InfoDisplay from './InfoDisplay';

interface Props{
    title?:string,
    tooltipType?:number,
    className?:string,
    backLinkText?: string,
    backLink?: Function
}

const ContentHeader:React.FC<Props> = ({title,tooltipType,className,children,backLinkText,backLink}:any) => {
    return (
        <header data-testid="contentHeader" className={`settings__content-area--header ${className ? className : ''}`}>
           { title &&
            <div data-testid="header-title-text" className="settings__content-area--header-title">             
             <h2 data-testid="header-toolTip" className="h2">{title}                            
                {tooltipType && 
                <InfoDisplay testId={`header-toolTip`} tooltipType={tooltipType}/>
                }                    
                </h2>
            </div>
           }
           { backLinkText &&
             <button data-testid= "contentHeader-backBtn" onClick={backLink} className="settings-btn settings-btn-back">
                 <i className="zmdi zmdi-arrow-left"></i> {backLinkText}
             </button>
           }
            {children}
        </header>
    )
}

export default ContentHeader;


interface ContentSubHeaderProps{
    title?:string,
    tooltipType?:number,
    className?:string,
    backLinkText?: string,
    backLink?: Function
}

export const ContentSubHeader:React.FC<ContentSubHeaderProps> = ({title,tooltipType,className,children,backLinkText,backLink}:any) => {
    return (
        <header data-testid= "contentSubHeader" className={`settings__content-area--subheader ${className ? className : ''}`}>
           { title &&
            <div className="settings__content-area--subheader-title">             
             <h2 data-testid="header-toolTip" className="h2">{title}                            
                {tooltipType && 
                <InfoDisplay tooltipType={tooltipType}/>
                }                    
                </h2>
            </div>
           }
           { backLinkText &&
             <button data-testid= "subHeader-backBtn" onClick={() => backLink()} className="settings-btn settings-btn-back">
                 <i className="zmdi zmdi-arrow-left"></i> {backLinkText}
             </button>
           }
            {children}
        </header>
    )
}
