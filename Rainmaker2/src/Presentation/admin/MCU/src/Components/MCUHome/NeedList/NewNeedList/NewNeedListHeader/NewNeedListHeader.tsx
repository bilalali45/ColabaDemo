import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";

type NewNeedListHeaderType = {
    saveAsDraft: Function
}

export const NewNeedListHeader = ({saveAsDraft} : NewNeedListHeaderType) => {
    return (
        <section className="MTheader">
            <div className="addneedlist-actions">
                <button className="btn btn-sm btn-secondary">Close</button>
                <button onClick={() => saveAsDraft()} className="btn btn-sm btn-primary">Save as Close</button>
            </div>
        </section>
    )
}