import { fireEvent, getAllByText, render, waitFor, waitForDomChange, waitForElement } from '@testing-library/react';
import { createMemoryHistory } from 'history';
import React from 'react';
import { MemoryRouter } from 'react-router-dom';
import App from '../../../App';
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NeedListActions');
jest.mock('../../../Store/actions/ReviewDocumentActions');
jest.mock('../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory();
    history.push('/');
  });

  describe('REVIEW DOCUMENT', () => {
      test('Should render Document Details text and close button when clictk on Details button of list item.', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
            <MemoryRouter initialEntries={[Url]} >
               <App/>
            </MemoryRouter>
           );

           await waitForDomChange()

          const detailBtns: any = getAllByTestId('needList-detailBtnts');     
          fireEvent.click(detailBtns[0]);

          await waitFor(() => {
            let reviewHeader = getByTestId('review-headerts');
            let reviewHeaderCloseBtn = getByTestId('review-closeBtnTs');
            expect(reviewHeader).toHaveTextContent('Document Detail');
            expect(reviewHeaderCloseBtn).toBeInTheDocument();
          })
      });
      
      test('Should click on close button and redirect to NeedList table', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );
  
         await waitForDomChange();
  
         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

         await waitFor(() => {
          let reviewHeaderCloseBtn = getByTestId('review-closeBtnTs');
          fireEvent.click(reviewHeaderCloseBtn);
         });

         const loanSnapshot = getByText((content, element) => element.className === "loansnapshot")
         expect(loanSnapshot).toBeDefined();
  
      });

      test('Should render file viewer with viewer header detail', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-view--header")
          expect(fileViewer).toBeDefined();
        })    
      })

      test('Should render file viewer with zoomIn, zoomOut and fitToScreen', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-view--floating-options")
          expect(fileViewer).toBeDefined();
        });    
      });

      test('Should render Activity log section', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-statement--header")
          expect(fileViewer).toBeDefined();
        });           
      });

      test('Should render document name in Activity log section', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-statement--header")
          expect(fileViewer).toHaveTextContent("Government Issued Identification");
        });    
    });

      test('Should render activity button and "Activity log" text in Activity log section', async () => {
      const {getByTestId, getByText, getAllByTestId } = render(
        <MemoryRouter initialEntries={[Url]} >
           <App/>
        </MemoryRouter>
       );

       await waitForDomChange();

       const detailBtns: any = getAllByTestId('needList-detailBtnts');     
       fireEvent.click(detailBtns[0]);

      await waitFor(() => {    
        const fileViewer = getByText((content, element) => element.className === "document-statement--header")
        expect(fileViewer.children[1]).toHaveClass("dropdown");
        expect(fileViewer.children[1]).toHaveTextContent('Activity Log');
      }); 
  });
      
      test('Should click on Activity log button and open activity detail section', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange()

        const detailBtns: any = getAllByTestId('needList-detailBtnts');     
        fireEvent.click(detailBtns[0]);
        let activityLogBtn: any = null;
        await waitFor(() => {
           activityLogBtn = getByTestId("activity-logBtn");                 
        })
        fireEvent.click(activityLogBtn);
        let logDetailSection = getByTestId("logDetail-section");
        expect(logDetailSection).toBeInTheDocument();
      });   
     
      test('Should render ""Request", "Log Details", "View Email Log" titles on header of activity detail section', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange()

        const detailBtns: any = getAllByTestId('needList-detailBtnts');     
        fireEvent.click(detailBtns[0]);
        let activityLogBtn: any = null;
        await waitFor(() => {
           activityLogBtn = getByTestId("activity-logBtn");                 
        })
        fireEvent.click(activityLogBtn);
        let logDetailSection = getByTestId("logDetail-section");
        expect(logDetailSection).toHaveTextContent('Requests');
        expect(logDetailSection).toHaveTextContent('Log Details');
        expect(logDetailSection).toHaveTextContent('View Email Log');
      });

      test('Should render activity log Request list in activity detail section', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange()

        const detailBtns: any = getAllByTestId('needList-detailBtnts');     
        fireEvent.click(detailBtns[0]);
        let activityLogBtn: any = null;
        let activityLogList: any = null;
        await waitFor(() => {
           activityLogBtn = getByTestId("activity-logBtn");                 
        })
        fireEvent.click(activityLogBtn);

        await waitFor(() => {
          activityLogList = getAllByTestId("activity-log-list");                 
       })
       
       expect(activityLogList[0]).toHaveTextContent('Re-requested By');
       expect(activityLogList[0]).toHaveTextContent('System Administrator');
       expect(activityLogList[0]).toHaveTextContent('Sep, 24 at 04:12 PM');

      });


      test('Should move between documents', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();


          
          expect(getByTestId("document-preview")).toBeInTheDocument();
          
          const fileName = getByText((content, element) => element.className === "document-view--header---title")
            expect(documents[0]).toHaveTextContent("download.jpeg");

          fireEvent.keyDown(documents[0].children[0],{
            key: "ArrowDown",
      code: "ArrowDown",
      keyCode: 40,
      charCode: 40
          });

          await waitFor(() => {
            
            expect(fileName).toHaveTextContent("download.jpeg");
          });
         
      })


      test('Should move to next document', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();


          
          expect(getByTestId("document-preview")).toBeInTheDocument();
          
          const fileName = getByText((content, element) => element.className === "document-view--header---title")
            expect(documents[0]).toHaveTextContent("download.jpeg");

            fireEvent.click(documents[2].children[0]);
            fireEvent.click(documents[2].children[0]);
            
            await waitFor(() => {
            const fileName = getByText((content, element) => element.className === "document-view--header---title")
            expect(fileName).toHaveTextContent("download.jpeg");
            });
            
          fireEvent.keyDown(documents[2],{
            key: 'ArrowDown',
      code: "ArrowDown",
      keyCode: 40,
      charCode: 40
          });

          await waitFor(() => {
            
            expect(fileName).toHaveTextContent("download.jpeg");
          });
         
      })


      test('Should rename document', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

          
          fireEvent.doubleClick(documents[0]);
          const renameDocInput = getByTestId("rename-doc");
          expect(renameDocInput).toBeInTheDocument();

          fireEvent.change(renameDocInput, { target: { value: "newTestFile" } });
          fireEvent.blur(renameDocInput, { target: { value: "newTestFile" } })

          expect(documents[0]).toHaveTextContent("newTestFile");
         
      })

      test('Should show empty string error', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

          
          fireEvent.doubleClick(documents[0]);
          const renameDocInput = getByTestId("rename-doc");
          expect(renameDocInput).toBeInTheDocument();

          fireEvent.change(renameDocInput, { target: { value: "" } });
          fireEvent.blur(renameDocInput, { target: { value: "" } })

          expect(getByTestId("empty-file-name-error")).toBeInTheDocument();
         
      })

      test('Should show unique name error', async () => {
        const {getByTestId, getAllByTestId, getByTitle } = render(
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

          
          fireEvent.doubleClick(documents[0]);
          const renameDocInput = getByTestId("rename-doc");
          expect(renameDocInput).toBeInTheDocument();

          fireEvent.change(renameDocInput, { target: { value: "sampleabc" } });
          fireEvent.blur(renameDocInput, { target: { value: "sampleabc" } })

          expect(getByTestId("unique-file-name-error")).toBeInTheDocument();
         
      })


      test('Should show special character error', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

          
          fireEvent.doubleClick(documents[0]);
          const renameDocInput = getByTestId("rename-doc");
          expect(renameDocInput).toBeInTheDocument();

          fireEvent.change(renameDocInput, { target: { value: "~" } });
          fireEvent.blur(renameDocInput, { target: { value: "~" } })

          expect(getByTestId("special-character-error")).toBeInTheDocument();
         
      })


      test('Should accept doc on accept btn click', async () => {
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
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

        const acceptDocBtn = getByTestId("accept-doc-btn");
        expect(acceptDocBtn).toBeInTheDocument();

        fireEvent.click(acceptDocBtn);

        await waitFor(() => {
          const acceptDocBtn = getByTestId("accept-doc-btn");
          expect(acceptDocBtn).toBeInTheDocument();
          // expect(getByText("This document has been accepted"));
        });
      })


      test('Should reject doc on reject btn click', async () => {
        const {getByTestId, getByText, getAllByTestId, getByTitle, getAllByText } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-reviewBtnts');     
         expect(detailBtns[2]).toBeInTheDocument();
         fireEvent.click(detailBtns[2]);

         await waitFor(() => {
          const filesHeader = getByTitle("W-2s - Last Two years")
          expect(filesHeader).toBeInTheDocument();

        }); 
          const documents = getAllByTestId("document-item");
          expect(documents[0]).toBeInTheDocument();

        const rejecttDocBtn = getByTestId("reject-doc-btn");
        expect(rejecttDocBtn).toBeInTheDocument();

        const acceptDocBtn = getByText("Accept Document");
        expect(acceptDocBtn).toBeInTheDocument();

        fireEvent.click(rejecttDocBtn);
       
        await waitFor(() => {
        //   const acceptDocBtn = getByText("Accept Document");
        // expect(acceptDocBtn).toBeInTheDocument();
        
<<<<<<< HEAD
        //   const doc = getByTestId("req-doc-dialog");

        // expect(doc).toBeInTheDocument();
=======
          const doc = getByTestId("req-doc-dialog");

        expect(doc).toBeInTheDocument();
>>>>>>> pspdfkit-trial
        });
      })
    });