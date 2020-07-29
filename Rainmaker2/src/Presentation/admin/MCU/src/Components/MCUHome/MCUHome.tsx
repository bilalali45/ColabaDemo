import React, { useEffect, useContext } from "react";

import { NeedList } from "./NeedList/NeedList";
import { AddNeedList } from "./NeedList/Add/AddNeedList";
import { TemplateManager } from "./TemplateManager/TemplateManager";
import { Route, Switch, Redirect } from "react-router-dom";
import { Store } from "../../Store/Store";
import { ReviewDocument } from "./ReviewDocument/ReviewDocument";
import { Authorized } from "../Authorized/Authorized";
import { NewNeedList } from "./NeedList/NewNeedList/NewNeedList";


export const MCUHome = () => {
  const { state, dispatch } = useContext(Store);

  useEffect(() => {}, []);

  useEffect(() => {}, []);

  return (
    <section className="home-layout">
      <Switch>
        <Redirect exact from="/" to="/needList" />
        <Authorized path="/needList" component={NeedList} />
        <Authorized path="/add-needList" component={AddNeedList} />
        <Authorized path="/templateManager" component={TemplateManager} />
        <Authorized path="/ReviewDocument" component={ReviewDocument} />

        <Authorized path="/NewNeedList" component={NewNeedList} />
      </Switch>
    </section>
  );
};
