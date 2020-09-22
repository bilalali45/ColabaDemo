import React, {useEffect, useContext} from 'react';

import {NeedList} from './NeedList/NeedList';
import {AddNeedList} from './NeedList/Add/AddNeedList';
import {TemplateManager} from './TemplateManager/TemplateManager';
import {
  Route,
  Switch,
  Redirect,
  useLocation,
  useParams
} from 'react-router-dom';
import {Store} from '../../Store/Store';
import {ReviewDocument} from './ReviewDocument/ReviewDocument';
import {Authorized} from '../Authorized/Authorized';
import {NewNeedList} from './NeedList/NewNeedList/NewNeedList';
import {ReviewNeedListRequest} from './ReviewNeedListRequest/ReviewNeedListRequest';
import {ParamsService} from '../../Utils/helpers/ParamService';

export const MCUHome = () => {
  const {state, dispatch} = useContext(Store);
  const {loanApplicationId} = useParams();
  const location = useLocation();
  ParamsService.storeParams(loanApplicationId);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  const setParams = (loanId: string) => {
    ParamsService.storeParams(loanId);
  };
  return (
    <section className="home-layout">
      <Switch>
        <Redirect
          exact
          from="/:loanApplicationId"
          to="/needList/:loanApplicationId"
        />
        {process.env.NODE_ENV === 'test' ? (
          <Authorized path="/" component={NeedList} />
        ) : (
          <Authorized
            path="/needList/:loanApplicationId"
            component={NeedList}
          />
        )}
        <Authorized
          path="/newNeedList/:loanApplicationId"
          component={NewNeedList}
        />
        <Authorized
          path="/templateManager/:loanApplicationId"
          component={TemplateManager}
        />
        <Authorized
          path="/ReviewDocument/:loanApplicationId"
          component={ReviewDocument}
        />
        <Authorized
          path="/ReviewNeedListRequest/:loanApplicationId"
          component={ReviewNeedListRequest}
        />
      </Switch>
    </section>
  );
};
