import React, { useEffect } from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { Home } from './components/Home/Home';
import Header from './shared/Components/Header/Header';
import Footer from './shared/Components/Footer/Footer';
import { Http } from './services/http/Http';
import DummyLogin from './components/DummyLogin/DummyLoging';
const httpClient = new Http();

httpClient.setBaseUrl('http://localhost:5000');

function App() {

  return (
    <div className="app">
      <Header></Header>
      <Router>
        <Switch>
          <Route path="/login" component={DummyLogin} />
          <Route path="/" component={Home} />
        </Switch>
      </Router>
      <Footer></Footer>
    </div>
  );
}

export default App;
