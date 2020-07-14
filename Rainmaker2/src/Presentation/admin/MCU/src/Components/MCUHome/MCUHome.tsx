import React, { useEffect, useContext } from "react";
import { NeedList } from "./NeedList/NeedList";
import { TemplateManager } from "./TemplateManager/TemplateManager";
import { Route, Switch, Redirect } from "react-router-dom";
import { UserActions } from "../../Store/actions/UserActions";
import { Store } from "../../Store/Store";
import { UserActionsType } from "../../Store/reducers/UserReducer";

import { ReviewDocument } from "./ReviewDocument/ReviewDocument";

export const MCUHome = () => {
  const { state, dispatch } = useContext(Store);

  useEffect(() => {}, []);

    useEffect(() => {
    }, []);
  
    return (
        <section className="home-layout">
            <Switch>
                <Redirect exact from="/" to="/needList"/>
                <Route path="/needList" component={NeedList} />
                <Route path="/templateManager" component={TemplateManager} />
                <Route path="/ReviewDocument" component={ReviewDocument} />
            </Switch>
        </section>
    )
}
