import React from 'react'

interface TableTDProps{
    valign?:string,
    align?:string,
    className?:string
}

const TableTD:React.FC<TableTDProps> = ({valign,align,className,children}) => {

    const checkAlign = () =>{
        // switch (align){
        //     case 'left':
        //         return 'left';
        //         break;
        //     case 'right':
        //         return 'right';
        //         break;
        //     case 'center':
        //         return 'center';
        //         break;
        //     case 'default':
        //         return 'start';
        //         break;
        // }
        return (align?align:'start')
    }

    const checkValign = () => {
        // switch (valign){
        //     case 'top':
        //         return 'top';
        //         break;
        //     case 'middle':
        //         return 'middle';
        //         break;
        //     case 'bottom':
        //         return 'bottom';
        //         break;
        //     case 'default':
        //         return 'initial';
        //         break;
        // }
        return (valign ? valign : 'initial')
    }

    const Style:any = {}

    if(align){
        Style.textAlign = checkAlign();
    }

    if(valign){
        Style.verticalAlign = checkValign();
    }

    return (
        <div data-testid="TableTD" className={`settings__table--td ${className != undefined ? className : ''}`} style={Style}>
            {children}
        </div>
    )
}

export default TableTD;