import React from 'react';
import { Store } from '../../Store/Store';
import {Notification} from './Notification';
import { NotificationBody } from './_Notification/NotificationBody';
import { NotificationHeader } from './_Notification/NotificationHeader';

import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import { NotificationActions } from '../../Store/actions/NotificationActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Notification');
});

describe('Notification', () => {

   test('Notification : Render', async()=>{
      const {getByTestId} = render(<StoreProvider><Notification/></StoreProvider>);
      expect(getByTestId('Notification')).toBeTruthy();
   })
   
   test('Notification : Header', async () => {
      render(<NotificationHeader/>);
      expect(screen.getAllByTestId('NotificationHeader')).toBeTruthy();      
  });

  test('Notification : Body', async()=>{
   render(<StoreProvider><NotificationBody/></StoreProvider>);
   expect(screen.getAllByTestId('loader')).toBeTruthy();
   await waitFor(()=>{
      expect(screen.getAllByTestId('contentBody')).toBeTruthy();
   });
  })

});