import React from 'react';
import { render, fireEvent, waitFor, getByTestId, act } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../Store/Store';
import { MockEnvConfig } from '../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';
import { DocumentsHeader } from '../../../DocumentsContainer/DocumentsHeader/DocumentsHeader';
import {} from '../../../../../Store/actions/TemplateActions'
import { Home } from '../../../Home';
import { ViewerThumbnails } from './ViewerThumbnails';
import { ViewerHome } from '../ViewerHome';

jest.mock('pspdfkit');
jest.mock('../../../../../Store/actions/TemplateActions');
jest.mock('../../../../../Store/actions/UserActions');

const Url = '/DocManager/2515'





beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    JSON.parse = jest.fn((data:any)=> data)
});

describe('Doc Manager Header', () => {

    test('Should drag file from trash bin', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ViewerHome></ViewerHome>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinText :any;
         await waitFor(() => {
            let thumbnailDrag:any;       
        
            const file =  {"indexes":[0],"isFromThumbnail":true,"isFromWorkbench":false,"isFromCategory":false}
            thumbnailDrag  = getAllByTestId('thumbnail-drag');
        
            expect(thumbnailDrag).toBeInTheDocument()
            const mockdt = { setData: () =>  {file} };
            // fireEvent.dragStart(thumbnailDrag[0], { dataTransfer: mockdt});
            act(() => {
                fireEvent.dragStart(thumbnailDrag, {
                    dataTransfer: mockdt});
            });
        })
       
            // expect(mockdt.setData).toBeCalled();
        
    });
})