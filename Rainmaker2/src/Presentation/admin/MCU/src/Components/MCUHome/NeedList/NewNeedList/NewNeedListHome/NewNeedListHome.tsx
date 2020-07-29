import React, { useEffect, useCallback, useState } from "react";
import { Http } from "rainsoft-js";
import Spinner from "react-bootstrap/Spinner";
import { NeedListRequest } from "./NeedListRequest/NeedListRequest";
import { NeedListContent } from "./NeedListContent/NeedListContent";

export const NewNeedListHome = () => {
    const [loaderVisible, setLoaderVisible] = useState<boolean>(false);

    return (
        <section className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                            <NeedListRequest
                                setLoaderVisible={setLoaderVisible}
                                loaderVisible={loaderVisible}
                            />

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                            <NeedListContent />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}