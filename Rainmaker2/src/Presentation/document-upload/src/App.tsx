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

  const history = useHistory();
  

  return (
    <div className="app">
      <StoreProvider>
        <Router>
          <Switch>
            <Route path="/" component={Home} />
            {/* <Route path="/home" component={Home} /> */}
          </Switch>
        </Router>
      </StoreProvider>
    </div>
  );
}

export default App;
