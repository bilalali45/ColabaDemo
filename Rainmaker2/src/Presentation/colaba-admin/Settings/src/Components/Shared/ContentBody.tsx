import React,{FunctionComponent} from 'react'

interface Props{
    className?:string
}

const ContentBody: React.FC<Props> = ({className, children}:any) => {
    return (
        <div data-testid="contentBody" className={`settings__content-area--body ${className ? className : ''}`} >
            {children}
        </div>
    )
}

export default ContentBody
