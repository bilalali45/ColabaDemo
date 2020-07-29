import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";

export const NewNeedListHeader = () => {
    return (
        <section className="MTheader">
            <div className="addneedlist-actions">
                <button className="btn btn-sm btn-secondary">Close</button>
                <button className="btn btn-sm btn-primary">Save as Close</button>
            </div>
        </section>
    )
}