import React, { useEffect } from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { Home } from './components/Home/Home';
import Header from './shared/Components/Header/Header';
import Footer from './shared/Components/Footer/Footer';
import DummyLogin from './components/DummyLogin/DummyLoging';
import { StoreProvider } from './store/store';


function App() {

  return (
    <div className="app">
      <StoreProvider>
        <Header></Header>
        <Router>
          <Switch>
            <Route path="/login" component={DummyLogin} />
            <Route path="/" component={Home} />
          </Switch>
        </Router>
        <Footer></Footer>
      </StoreProvider>
    </div>
  );
}

export default App;
