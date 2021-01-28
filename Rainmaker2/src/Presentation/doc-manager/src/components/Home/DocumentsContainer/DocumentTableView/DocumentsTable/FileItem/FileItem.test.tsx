import React from 'react';
import { render, fireEvent, waitFor, getByTestId, getAllByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../../Store/Store';
import { MockEnvConfig } from '../../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../../test_utilities/LocalStoreMock';
import { DocumentsTable } from '../DocumentsTable';


jest.mock('pspdfkit');
jest.mock('../../../../../../Store/actions/DocumentActions');
jest.mock('../../../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('File Items ', () => {

    test('Should show File Name', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const fileItems = getAllByTestId("file-item-div");
                // expect(fileItems).toBeInTheDocument();

                expect(fileItems[0]).toHaveTextContent("images 1.jpeg")
            })
    });
})
