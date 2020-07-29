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

  return (
    <section className="home-layout">
      <Switch>
        <Redirect exact from="/" to="/needList" />
        <Authorized path="/needList" component={NeedList} />
        <Authorized path="/newNeedList" component={NewNeedList} />
        <Authorized path="/templateManager" component={TemplateManager} />
        <Authorized path="/ReviewDocument" component={ReviewDocument} />
      </Switch>
    </section>
  );
};
