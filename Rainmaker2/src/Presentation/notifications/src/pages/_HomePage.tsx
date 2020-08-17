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
    <div onClick={onClick}>
      <h3>Bell Icon</h3>
    </div>
  );
};
