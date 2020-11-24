import React,{FunctionComponent} from 'react'

interface Props{
    className?:any
}

const ContentBody: React.FC<Props> = ({className, children}:any) => {
    return (
        <div className={`${className ? className : ''} settings__content-area--body`}>
            {children}
        </div>
    )
}

export default ContentBody
