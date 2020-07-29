import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
import { NewNeedListHeader } from './NewNeedListHeader/NewNeedListHeader'
import { NewNeedListHome } from './NewNeedListHome/NewNeedListHome'

export const NewNeedList = () => {
    return (
        <main className="NeedListAddDoc-wrap">
            <NewNeedListHeader />
            <NewNeedListHome />
        </main>
    )
}