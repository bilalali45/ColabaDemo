import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from '../../../../App';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('should render navigation Links', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const manageDocumentTemplateBtn = getByText('Manage Document Template');
  expect(manageDocumentTemplateBtn).toBeInTheDocument();

  const backBtn = getByText('Back');
  expect(backBtn).toBeInTheDocument();

  const backIcon = getByText((content, element) => element.className === "zmdi zmdi-arrow-left")
  expect(backIcon).toBeInTheDocument()

});
