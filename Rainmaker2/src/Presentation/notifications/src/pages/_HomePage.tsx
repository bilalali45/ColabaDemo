import React from 'react';

export const Header = () => {
  return (
    <div>
      <h1>Notifications</h1>
    </div>
  );
};

export const BellIcon = ({onClick}: {onClick: () => void}) => {
  return (
    <button onClick={onClick} className="btn-notify"><i className="zmdi zmdi-notifications"></i></button>
  );
};
