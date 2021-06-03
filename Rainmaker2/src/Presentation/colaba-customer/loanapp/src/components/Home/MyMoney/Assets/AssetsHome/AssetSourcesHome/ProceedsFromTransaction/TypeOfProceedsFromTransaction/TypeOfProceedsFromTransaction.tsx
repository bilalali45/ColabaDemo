import React from 'react'

import {TypeOfProceedsFromTransaction_Web} from "./TypeOfProceedsFromTransaction_Web";


export const TypeOfProceedsFromTransaction = (props) => {

    return (
        <div data-testid="incomesources-screen">
            <TypeOfProceedsFromTransaction_Web {...props} />
        </div>
    )
}
