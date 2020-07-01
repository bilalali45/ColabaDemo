import React from 'react';
import logo from './logo.svg';
import './App.css';
import { MCUHome } from './Components/MCUHome/MCUHome';
import { RainMakerHeader } from './Components/RainMakerHeader/RainMakerHeader';
import { RainMakerSidebar } from './Components/RainMakerSidebar/RainMakerSidebar';
import { RainMakerFooter } from './Components/RainMakerFooter/RainMakerFooter';

function App() {
  return (
    <div className="App">
      <RainMakerHeader/>
      <RainMakerSidebar/>
      <RainMakerFooter/>
      <MCUHome/>
    </div>
  );
}

export default App;
