import React from 'react'

interface TableROWProps{

}

const TableROW: React.FC<TableROWProps> = ({children}) => {
    return (
        <div data-testid="table-row" className="settings__table--row">
            {children}
        </div>
    )
}

export default TableROW;