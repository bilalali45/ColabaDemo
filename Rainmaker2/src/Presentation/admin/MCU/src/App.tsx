import React, { useEffect } from 'react';
//import logo from './logo.svg';
import './App.scss';
import { MCUHome } from './Components/MCUHome/MCUHome';
import { RainMakerHeader } from './Components/RainMakerHeader/RainMakerHeader';
import { RainMakerSidebar } from './Components/RainMakerSidebar/RainMakerSidebar';
import { RainMakerFooter } from './Components/RainMakerFooter/RainMakerFooter';
import { BrowserRouter as Router, Route } from 'react-router-dom'
import { StoreProvider } from './Store/Store';

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
<<<<<<< HEAD
        <main className="main-layout">
          <StoreProvider>
            <Router>
              <Route path="/" component={MCUHome} />
              <RainMakerFooter />
            </Router>
          </StoreProvider>
=======
        <main className="main-layout hascollapssidebar">
          <Router>
            <Route path="/" component={MCUHome} />
          </Router>
          
          <RainMakerFooter />
>>>>>>> 058ceaeb7881a2ad32b1b5140d7e18000074b228
        </main>
      </section>
    </div>
  );
}

export default App;
