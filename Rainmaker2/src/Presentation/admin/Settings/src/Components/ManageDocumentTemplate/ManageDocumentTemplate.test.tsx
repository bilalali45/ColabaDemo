import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen, getByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { AssignedRoleActions } from '../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../Utils/LocalDB';
// Render Components
import ManageDocumentTemplate from './ManageDocumentTemplate';
import ManageDocumentTemplateHeader from './_ManageDocumentTemplate/ManageDocumentTemplateHeader';
import ManageDocumentTemplateBody from './_ManageDocumentTemplate/ManageDocumentTemplateBody';


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/ManageDocumentTemplate');
});

// Render Check For Component
it("Render : ManageDocumentTemplateBody", async ()=>{  
  
  const {queryAllByTestId, queryByTestId, getByTestId, debug } = render(<StoreProvider>
    <ManageDocumentTemplate/>
  </StoreProvider>);

  //debug();
  
  expect(queryByTestId("contentHeader")).toBeTruthy();
  expect(queryByTestId("contentHeader")).toHaveClass('manage-doc-temp-header');
  expect(queryByTestId("loader")).toBeTruthy();

  await waitFor(()=>{
    expect(queryByTestId("contentBody")).toBeTruthy();
    expect(queryByTestId("contentBody")).toHaveClass('manage-doc-temp-body');
  });

});