import React from 'react';

interface LoaderProps {
    type?:string;
    className?:string;
}

const Loader:React.FC<LoaderProps> = ({type,className}) => {
    return (
        <>
        {!type &&
            <div className={className}>
                <div className="widget-loader">
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
        }
        {type == 'widget' &&
            <div className={className}>
                <div className="widget-loader">
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
        }
        {type == 'page' &&
            <div className={className}>
                <div className="page-loader">
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            </div>
        }
        </>
    )
}

export default Loader
