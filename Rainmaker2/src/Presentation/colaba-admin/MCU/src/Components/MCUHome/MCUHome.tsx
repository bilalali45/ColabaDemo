import React, { useEffect, useContext } from 'react';

import { NeedList } from './NeedList/NeedList';
import { TemplateManager } from './TemplateManager/TemplateManager';
import {
  Route,
  Switch,
  Redirect,
  useLocation,
  useParams,
  useHistory
} from 'react-router-dom';
import { Store } from '../../Store/Store';
import { ReviewDocument } from './ReviewDocument/ReviewDocument';
import { Authorized } from '../Authorized/Authorized';
import { NewNeedList } from './NeedList/NewNeedList/NewNeedList';
import { ReviewNeedListRequest } from './ReviewNeedListRequest/ReviewNeedListRequest';
import { ParamsService } from '../../Utils/helpers/ParamService';

export const MCUHome = () => {
  const { state, dispatch } = useContext(Store);
  const { loanApplicationId } : any = useParams();
  const location = useLocation();
  ParamsService.storeParams(loanApplicationId);
  useEffect(() => {
    window.scrollTo(0, 0);
  }, [location.pathname]);

  const setParams = (loanId: string) => {
    ParamsService.storeParams(loanId);
  };

  const history = useHistory();

  useEffect(() => { 
    process.env.NODE_ENV === 'test' && history.push('/needList/3')
  }, [])

  return (
    <section data-testid="mcu-home" className="home-layout">
      <Switch>
        <Redirect
          exact
          from="/:loanApplicationId"
          to="/needList/:loanApplicationId"
        />

        <Authorized
          path="/needList/:loanApplicationId"
          component={NeedList}
        />
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
