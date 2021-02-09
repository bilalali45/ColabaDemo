import React, { FunctionComponent,useState, useEffect } from 'react'
import { SimpleSort } from '../../../Utils/helpers/Enums';



type TableSortProps = {
    order?: any,
    className?:string,
    callBackFunction: Function
}


const TableSort: React.FC<TableSortProps> = ({ order, className, callBackFunction,children }) => {
    let ovalue = order ? order : null;
    const [sortOrder, setSortOrder] = useState<any>(ovalue);
    useEffect(() => {        
        // action on update of sortOrder
        callBackFunction(sortOrder);
    }, [sortOrder]);
    const makeSort = () => {
        switch(sortOrder) {
            case null:
                setSortOrder(SimpleSort.Up);
                break;
            case SimpleSort.Up:
                setSortOrder(SimpleSort.Down);
                break;
            case SimpleSort.Down:
                setSortOrder(SimpleSort.Up);
                break;
            default:
                setSortOrder(null);
                break;
        }
    }

    return (
        <span onClick={() => {
            makeSort();
        }
        } className={`clickable ${className?className:''}`}
        data-testid="TableSort"
        >
        {children}
        {sortOrder != null &&
            <div className="settings__table-sorter">
                {sortOrder == 1 && <i className="zmdi zmdi-long-arrow-up"></i>}
                {sortOrder == 2 && <i className="zmdi zmdi-long-arrow-down"></i>}
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
