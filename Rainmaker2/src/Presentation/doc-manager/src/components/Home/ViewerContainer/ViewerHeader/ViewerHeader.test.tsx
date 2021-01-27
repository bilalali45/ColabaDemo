import React from 'react';
import { render, fireEvent, waitFor, getByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { MemoryRouter } from 'react-router-dom';
import { createMockFile } from '../../AddDocument/AddFileToDoc/AddFileToDoc.test';
import App from '../../../../App';
import { Home } from '../../Home';
import { ViewerHeader } from './ViewerHeader';


jest.mock('pspdfkit');
jest.mock('../../../../Store/actions/DocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Viewer Header', () => {

    test('Should show current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        
            await waitFor(() => {
                const currentFileName = getByTestId('current-file-name');
                expect(currentFileName).toBeInTheDocument();

                expect(currentFileName).toHaveTextContent("images 1.jpeg")
            })
    });

    test('Should do nothing when same name entered on changing current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "images 1" } });

            fireEvent.blur(currentFileNameInput)

            await waitFor(() => {
                const currentFileName = getByTestId('current-file-name');
                expect(currentFileName).toBeInTheDocument();

                expect(currentFileName).toHaveTextContent("images 1.jpeg")
            })
    });

    test('Should change current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "abcd" } });

            fireEvent.blur(currentFileNameInput)

            await waitFor(() => {
                const currentFileName = getByTestId('current-file-name');
                expect(currentFileName).toBeInTheDocument();

                expect(currentFileName).toHaveTextContent("abcd.jpeg")
            })
    });

    test('Should change current file name on enter', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "abcd" } });

            fireEvent.keyDown(currentFileNameInput, { key: 'Enter', code: 13 })

            
                const currentFileName = getByTestId('current-file-name');
                expect(currentFileName).toBeInTheDocument();

                expect(currentFileName).toHaveTextContent("abcd.jpeg")
    });
    

    test('Should show empty name error on changing current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "" } })
                const errorMsg = getByTestId('empty-file-name-error');
                expect(errorMsg).toBeInTheDocument();

                expect(errorMsg).toHaveTextContent("File name cannot be empty")
    });


    test('Should show special character error on changing current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "!" } });

                const errorMsg = getByTestId('special-character-error');
                expect(errorMsg).toBeInTheDocument();

                expect(errorMsg).toHaveTextContent("File name cannot contain any special characters")
    });

    test('Should show uniqu name error on changing current file name', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let currentFileNameDiv
            await waitFor(() => {
                currentFileNameDiv = getByTestId('current-file-name-div');
                expect(currentFileNameDiv).toBeInTheDocument();
            });
                userEvent.dblClick(currentFileNameDiv)
                console.log(currentFileNameDiv)
        

            let currentFileNameInput;
            await waitFor(() => {
                currentFileNameInput = getByTestId("rename-file")
                expect(currentFileNameInput).toBeInTheDocument()
            })

            fireEvent.change(currentFileNameInput, { target: { value: "Portrait-family-1-600-xxxq87" } });


                const errorMsg = getByTestId('unique-file-name-error');
                expect(errorMsg).toBeInTheDocument();

                expect(errorMsg).toHaveTextContent("File name must be unique")
    });

});