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
  console.log(history);
  

  return (
    <div className="app">
      <StoreProvider>
        <Router>
          <Switch>
            {/* <Route path="/login" component={DummyLogin} /> */}
            <Route exact path="/" component={Loading} />
            <Route path="/home" component={Home} />
          </Switch>
        </Router>
      </StoreProvider>
    </div>
  );
}

export default App;
