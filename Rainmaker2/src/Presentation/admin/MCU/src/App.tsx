import React from 'react';
//import logo from './logo.svg';
import './App.scss';
import { MCUHome } from './Components/MCUHome/MCUHome';
import { RainMakerHeader } from './Components/RainMakerHeader/RainMakerHeader';
import { RainMakerSidebar } from './Components/RainMakerSidebar/RainMakerSidebar';
import { RainMakerFooter } from './Components/RainMakerFooter/RainMakerFooter';
import { BrowserRouter as Router, Route } from 'react-router-dom'

declare global {
  interface Window { envConfig: any; }
}
window.envConfig = window.envConfig || {};

function App() {
  return (
    <div className="App">
      <RainMakerHeader />
      <section className="d-layout">
        <RainMakerSidebar />
        <main className="main-layout">
          <Router>
            <Route path="/" component={MCUHome} />
            <RainMakerFooter />
          </Router>
        </main>
      </section>
    </div>
  );
}

export default App;
