import React from 'react'

interface ContentFooterProps {
    children?:any;
    className?:any
}

const ContentFooter:React.FC<ContentFooterProps> = ({children,className}:any) => {
    return (
        <div className={`settings__content-area--footer ${className ? className : ''}`}>
            {children}
        </div>
    )
}

export default ContentFooter;
