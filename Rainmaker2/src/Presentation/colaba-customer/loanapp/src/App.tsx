import './assets/scss/loanapp.scss';
import React, { useEffect } from 'react';

import { BrowserRouter as Router } from 'react-router-dom';
import { StoreProvider } from './store/store';
import { LocalDB } from './lib/LocalDB';
import { InitApp } from './components/InitApp/InitApp';

const App = () => {
  const basePath = process.env.NODE_ENV == "production" ? window.location.pathname.toLowerCase().split('/app/')[0] + '/app' + '/' : "/";

  useEffect(() => {

    return () => {
    //  LocalDB.clearBorrowerFromStorage();
    //  LocalDB.clearLoanGoalFromStorage();
     // LocalDB.clearLoanPurposeFromStorage();
      LocalDB.clearSessionStorage();
    };
  }, []);

  return (
    <Router basename={basePath}>
      <StoreProvider>
        < InitApp />
      </StoreProvider>
    </Router>
  )
}

export default App;
