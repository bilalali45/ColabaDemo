import { fireEvent, render, waitFor } from "@testing-library/react";
import React from "react";
import { MemoryRouter } from "react-router-dom";
import App from "../../../../App";
import { DocumentFile } from "../../../../Models/DocumentFile";
import { DocumentRequest } from "../../../../Models/DocumentRequest";
import DocumentActions from "../../../../Store/actions/DocumentActions";
import { DocumentActionsType } from "../../../../Store/reducers/documentsReducer";
import { ViewerActionsType } from "../../../../Store/reducers/ViewerReducer";
import { StoreProvider } from "../../../../Store/Store";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockLocalStorage } from "../../../../test_utilities/LocalStoreMock";
import { FileUpload } from "../../../../Utilities/helpers/FileUpload";
import { DocumentsHeader } from "../../DocumentsContainer/DocumentsHeader/DocumentsHeader";

export const createMockFile = (name, size, mimeType) => {
    let range = '';
    for (let i = 0; i < size; i++) {
        range += 'a';
    }
    let blob: any = new Blob([range], { type: mimeType });
    blob.lastModified = 1600081543628;
    blob.lastModifiedDate = 'Mon Sep 14 2020 16:05:43 GMT+0500 (Pakistan Standard Time)';
    blob.name = name;
    return blob;
}
let uploadPercent = 0;
const Url = '/DocManager/2515'
beforeEach(() => {
    // @ts-ignore
    delete window.location;
    // @ts-ignore
    // window.location = new URL('http://localhost/');


    MockEnvConfig();
    MockLocalStorage();

    // FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
    // FileUpload.isSizeAllowed = jest.fn(() => true);

    // const submitDocuments = (documents: any,
    //     currentSelected: DocumentRequest,
    //     fileId: string,
    //     file: File,
    //     dispatchProgress: Function,) => {

    //         let selectedFile = new DocumentFile(
    //             fileId.toString(),
    //             FileUpload.removeSpecialChars(file.name),
    //             FileUpload.todayDate(),
    //             0,
    //             0,
    //             FileUpload.getDocLogo(file, "slash"),
    //             file,
    //             "pending",
    //             "",
    //             currentSelected.docId,
    //             "System Administrator"
          
    //           );

        
    //     let p = Math.floor(uploadPercent * 100);
    //     selectedFile.uploadProgress = p;
    //     if (p === 100) {
    //       selectedFile.uploadStatus = "done";

    //       dispatchProgress({
    //         type: ViewerActionsType.SetFileProgress,
    //         payload: 0,
    //       });
    //     }

    //     let allDocs = documents.map((doc: any) => {
    //       if (doc.docId === currentSelected.docId) {
    //         doc = currentSelected
    //       }
    //       return doc
    //     })

    //     dispatchProgress({
    //       type: DocumentActionsType.SetDocumentItems,
    //       payload: allDocs,
    //     });

    //     return selectedFile
    // }

    // DocumentActions.submitDocuments = jest.fn((a, b, c, d,e) => Promise.resolve(submitDocuments(a, b, c, d, e)))
});

describe('Doc Manager Header', () => {

    test('Should show Add Document Overlay', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
            
            

            fireEvent.click(addDocumentText);
            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });
    
            let docCats = getAllByTestId('doc-cat');
    
            fireEvent.click(docCats[2]);
    
            const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');
    
            const itemsToClick = getAllByTestId('doc-item');
    
            expect(itemsToClick[2]).toHaveTextContent('Rental Agreement');
    
            fireEvent.click(itemsToClick[2]);

            await waitFor(() => {
            const fileInput = getByTestId("file-input")

            // const file = createMockFile('test.jpg', 30000, 'image/jpeg');

            // fireEvent.change(fileInput, { target: { files: [file] } });
            })
    });
})
