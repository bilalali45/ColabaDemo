import React from 'react'

interface TableTHProps {
    valign?:any,
    align?:any,
    className?:any
}

const TableTH:React.FC<TableTHProps> = ({valign,align,className,children}) => {
    
    const checkAlign = () =>{        
        switch (align){
            case 'left':
                return 'left';
                break;
            case 'right':
                return 'right';
                break;
            case 'center':
                return 'center';
                break;
            case 'default':
                return 'start';
                break;
        }
    }

    const checkValign = () => {
        switch (valign){
            case 'top':
                return 'top';
                break;
            case 'middle':
                return 'middle';
                break;
            case 'bottom':
                return 'bottom';
                break;
            case 'default':
                return 'initial';
                break;
        }
    }

    const Style:any = {}

    if(align){
        Style.textAlign = checkAlign();
    }

    if(valign){
        Style.verticalAlign = checkValign();
    }

    return (
        <div className={`settings__table--th ${className != undefined ? className : ''}`} style={Style}>
            {children}
        </div>
    )
}

export default TableTH;