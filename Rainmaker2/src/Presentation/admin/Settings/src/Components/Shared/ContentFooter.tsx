import React from 'react'

interface ContentFooterProps {
    className?:string
}

const ContentFooter:React.FC<ContentFooterProps> = ({children,className}:any) => {
    return (
        <div data-testid="contentFooter" className={`settings__content-area--footer ${className ? className : ''}`}>
            {children}
        </div>
    )
}

export default ContentFooter;
