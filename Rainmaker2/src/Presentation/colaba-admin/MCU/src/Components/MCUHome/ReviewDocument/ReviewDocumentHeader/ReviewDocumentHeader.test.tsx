import { fireEvent, getAllByText, render, waitFor, waitForDomChange } from '@testing-library/react';
import { createMemoryHistory } from 'history';
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import App from '../../../../App';
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Store/actions/ReviewDocumentActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory();
    history.push('/');
  });

  describe('REVIEW DOCUMENT', () => {
    test('Should disable review previous document on first doc preview', async () => {
        const {getByTestId, getByText, getAllByTestId, getByTitle, getAllByText } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-reviewBtnts');     
         expect(detailBtns[0]).toBeInTheDocument();
         fireEvent.click(detailBtns[0]);

         await waitFor(() => {
          const filesHeader = getByTitle("Covid-19")
          expect(filesHeader).toBeDefined();

        }); 
          const prevDoc = getByTestId("previous-doc-btn");
          expect(prevDoc).toHaveAttribute("disabled")
          });
         
 
          test('Should enable review previous document on first doc preview', async () => {
            const {getByTestId, getByText, getAllByTestId, getByTitle, getAllByText } = render(
              <MemoryRouter initialEntries={[Url]} >
                 <App/>
              </MemoryRouter>
             );
    
             await waitForDomChange();
    
             const detailBtns: any = getAllByTestId('needList-reviewBtnts');     
             expect(detailBtns[0]).toBeInTheDocument();
             fireEvent.click(detailBtns[1]);
    
             await waitFor(() => {
              const filesHeader = getByTitle("HOA or Condo Association Fee Statements")
              expect(filesHeader).toBeDefined();
    
            }); 
              
              const prevDoc = getByTestId("previous-doc-btn");
              expect(prevDoc).not.toHaveAttribute("disabled")
              });

              test('Should show next document on review next document btn click', async () => {
                const {getByTestId, getByText, getAllByTestId, getByTitle, getAllByText } = render(
                  <MemoryRouter initialEntries={[Url]} >
                     <App/>
                  </MemoryRouter>
                 );
        
                 await waitForDomChange();
        
                 const detailBtns: any = getAllByTestId('needList-reviewBtnts');     
                 expect(detailBtns[0]).toBeInTheDocument();
                 fireEvent.click(detailBtns[0]);
        
                 await waitFor(() => {
                  const filesHeader = getByTitle("Covid-19")
                  expect(filesHeader).toBeDefined();
        
                }); 
                  
                  const nextDoc = getByTestId("next-doc-btn");
                  expect(nextDoc).toBeInTheDocument();

                  fireEvent.click(nextDoc);


                  await waitFor(() => {
                    const fileName = getByText((content, element) => element.className === "document-view--header---title")
                    expect(fileName.children).toHaveTextContent("download.jpeg");
                    });

                  await waitFor(() => {
                    const filesHeader = getByTitle("HOA or Condo Association Fee Statements")
                    expect(filesHeader).toBeDefined();

                  });
                  });

                  test('Should show previous document on review previous document btn click', async () => {
                    const {getByTestId, getByText, getAllByTestId, getByTitle, getAllByText } = render(
                      <MemoryRouter initialEntries={[Url]} >
                         <App/>
                      </MemoryRouter>
                     );
            
                     await waitForDomChange();
            
                     const detailBtns: any = getAllByTestId('needList-reviewBtnts');     
                     expect(detailBtns[1]).toBeInTheDocument();
                     fireEvent.click(detailBtns[1]);
            
                     await waitFor(() => {
                      const filesHeader = getByTitle("HOA or Condo Association Fee Statements")
                      expect(filesHeader).toBeDefined();
            
                    }); 
                      
                      const prevDoc = getByTestId("previous-doc-btn");
                      expect(prevDoc).toBeInTheDocument();
    
                      fireEvent.click(prevDoc);
    
    
                      await waitFor(() => {
                        const fileName = getByText((content, element) => element.className === "document-view--header---title")
                        expect(fileName.children).toHaveTextContent("download.jpeg");
                        });
    
                      await waitFor(() => {
                        const filesHeader = getByTitle("Covid-19")
                        expect(filesHeader).toBeDefined();
    
                      });
                      });
      
    });