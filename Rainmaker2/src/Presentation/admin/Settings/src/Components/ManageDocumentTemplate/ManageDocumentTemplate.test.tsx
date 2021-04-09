import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, getByText, screen, getByTestId } from '@testing-library/react'
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../Store/Store';
import ManageDocumentTemplate from './ManageDocumentTemplate';



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
  
  const { queryByTestId } = render(
   <StoreProvider>
    <ManageDocumentTemplate/>
   </StoreProvider>);
  
  expect(queryByTestId("contentHeader")).toBeTruthy();
  expect(queryByTestId("contentHeader")).toHaveClass('manage-doc-temp-header');
  expect(queryByTestId("loader")).toBeTruthy();

  await waitFor(()=>{
    expect(queryByTestId("contentBody")).toBeTruthy();
    expect(queryByTestId("contentBody")).toHaveClass('manage-doc-temp-body');
  });

});