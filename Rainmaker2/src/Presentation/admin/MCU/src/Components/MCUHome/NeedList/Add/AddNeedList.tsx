import React, {useEffect, useContext} from 'react';
import {AddNeedListHeader} from './Header/AddNeedListHeader';
import {AddNeedListHome} from './Home/AddNeedListHome';
import {Http} from 'rainsoft-js';

export const AddNeedList = () => {
  return (
    <main className="NeedListAddDoc-wrap">
      <AddNeedListHeader />
      <AddNeedListHome />
    </main>
  );
};
