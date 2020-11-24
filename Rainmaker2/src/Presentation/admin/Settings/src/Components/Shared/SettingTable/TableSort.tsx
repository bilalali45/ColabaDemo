import React, { FunctionComponent,useState, useEffect } from 'react'



type TableSortProps = {
    order?: any,
    className?:any,
}

const TableSort: React.FC<TableSortProps> = ({ order, className, children }) => {
    let ovalue = order ? order : null;
    const [sortOrder, setSortOrder] = useState<any>(ovalue);

    const makeSort = () => {
        switch(sortOrder) {
            case null:
                setSortOrder('up');
                break;
            case 'up':
                setSortOrder('down');
                break;
            case 'down':
                setSortOrder('up');
                break;
            default:
                setSortOrder(null);
                break;
        }
    }

    return (
        <span onClick={makeSort} className={`clickable ${className?className:''}`}>
        {children}
        {sortOrder != null &&
            <div className="settings__table-sorter">
                {sortOrder == 'up' && <i className="zmdi zmdi-long-arrow-up"></i>}
                {sortOrder == 'down' && <i className="zmdi zmdi-long-arrow-down"></i>}
            </div>
        }        
        </span>
    )
}

export default TableSort;

/*===============================
How to impliment?

<thead>
    <tr>
    <th scope="col"><TableSort order={`up`}>Template Name </TableSort></th>
    </tr>
</thead>

================================*/
