import React from 'react'
import { NeedList } from './NeedList/NeedList'
import { TemplateManager } from './TemplateManager/TemplateManager'
import { Route, Switch } from 'react-router-dom';

export const MCUHome = () => {
    return (
        <section className="home-layout">
            <Switch>
                <Route path="/needList" component={NeedList} />
                <Route path="/templateManager" component={TemplateManager} />
            </Switch>
        </section>
    )
}
