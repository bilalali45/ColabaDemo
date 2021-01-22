import React from 'react';
import { render, fireEvent, waitFor, getByTestId, getAllByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../Store/Store';
import { DocumentsHeader } from '../../DocumentsHeader/DocumentsHeader';
import { MockEnvConfig } from '../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';


jest.mock('pspdfkit');
jest.mock('../../../../../Store/actions/DocumentActions');
jest.mock('../../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Doc Section', () => {

    test('Should show Docs', async () => {
        const { getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
            waitFor(() => {
                const docItems = getAllByTestId("doc-item");
                expect(docItems).toBeInTheDocument();

                expect(docItems[0]).toHaveTextContent("Bank Statements - Two Months")
            })
    });
})
