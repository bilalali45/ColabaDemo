import React from 'react';

interface TableProps{
    tableClass?:any
}

const Table:React.FC<TableProps> = ({tableClass,children}) => {
    return (
        <div data-testid="table" className={`settings__table ${tableClass}`}>
            {children}
        </div>
    )
}

export default Table;
