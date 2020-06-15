import React from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { Home } from './components/Home/Home';
import Header from './shared/Components/Header/Header';
import Footer from './shared/Components/Footer/Footer';
import DummyLogin from './components/DummyLogin/DummyLoging';
import { StoreProvider } from './store/store';
import { Loading } from './components/Loading/Loading';


const App = () => {
  // let baseURL;
  // if (process.env.NODE_ENV.toLowerCase() === 'development') {
  //     baseURL = process.env.PUBLIC_URL;
  // } else {
  //     baseURL = true ? "" : "/react"
  // }
  const history = useHistory();
  

  return (
    <div className="app">
      <StoreProvider>
        <Router basename="/documentmanagement" >
          <Switch>
            <Route path="/" component={Home} />
          </Switch>
        </Router>
      </StoreProvider>
    </div>
  );
}

export default App;
