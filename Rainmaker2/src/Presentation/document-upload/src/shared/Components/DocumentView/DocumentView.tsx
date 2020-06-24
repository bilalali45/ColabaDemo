import React, { useState } from 'react'
import FileViewer from 'react-file-viewer';

import { Document, Page, pdfjs } from 'react-pdf';
pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

type DocumentViewPropsType = { type: string, url: string, file: File | null, hide: Function }

// export const DocumentView = ({ type, url, file, hide }: DocumentViewPropsType) => {
//   const [numPages, setNumPages] = useState<number | null>(null);
//   const [pageNumber, setPageNumber] = useState(1);
//   const [imageSrc, setImageSrc] = useState<string>('');

//   const onDocumentLoadSuccess = ({ numPages }: any) => {
//     console.log('numPages', numPages);
//     setNumPages(numPages);
//   }

//   const readLocalFile = (file: any) => {
//     const reader = new FileReader();

//     reader.addEventListener('loadstart', () => console.log('Read file...'));

//     reader.addEventListener('load', (e: any) => {
//       const fileContent = e.target.result;
//       setImageSrc(fileContent);
//     });

//     reader.addEventListener('error', (e) => console.log(e));

//     reader.addEventListener('progress', (e: any) => {
//       if (e.lengthComputable) {
//         const percentRead = e.loaded;
//         console.log(percentRead);
//       }
//     });

//     reader.readAsDataURL(file);
//   }

//   const handlePage = (e: any) => {
//     if (e.target.id === 'next') {
//       if (numPages && pageNumber < numPages) {
//         setPageNumber(pageNumber + 1);
//       }
//     } else {
//       if (pageNumber > 1) {
//         setPageNumber(pageNumber - 1);
//       }
//     }
//   }

//   const renderPdfView = () => {
//     console.log('----------------', file);
//     return (
//       <>
//         <div className="modal-content">
//           <Document

//             file={file}
//             onLoadSuccess={onDocumentLoadSuccess}
//           >
//             <Page pageNumber={pageNumber} />
//           </Document>
//           <p>Page {pageNumber} of {numPages}</p>
//         </div>
//         <div className="page-controls">
//           <button id="previous" onClick={handlePage}>Previous</button>
//           <h1>{pageNumber}</h1>
//           <button id="next" onClick={handlePage}>Next</button>
//         </div>
//       </>
//     )
//   }

//   const renderTextView = () => {
//     return (
//       <iframe src="https://docs.google.com/viewerng/viewer?url=http://localhost:5000/pdf/file-sample_100kB.doc"></iframe>
//     )
//   }

//   const renderPlanTextView = () => {
//     // return <div className={'text-file-viewer'}>{imageSrc}</div>
//   }

//   const renderImageView = () => {
//     if(imageSrc) {
//       return <img style={{ width: "50%", height: "50%" }} src={imageSrc} alt="" />
//     }
//     return '';
//   }

//   const renderView = () => {
//     switch (type) {
//       case 'application/pdf':
//         return renderPdfView();
//       case 'application/msword':
//          return renderTextView();
//       case 'image/jpeg':
//       case 'image/png':
//       case 'image/jpg':
//       case 'image/gif':
//         readLocalFile(file);
//         return renderImageView();

//       default:
//         break;
//     }
//   }

//   return (
//     <div className="modal-container">
//       <button onClick={() => hide()}>X</button>
//       {renderView()}
//       <div className="overlay"></div>
//     </div>
//   );
// }


// import React, { Component } from 'react';
// import logger from 'logging-library';
// import { CustomErrorComponent } from 'custom-error';

const filef = 'http://localhost:5000/pdf/A Sample PDF.pdf'
const typea = 'pdf'

export const DocumentView = ({ type, url, file, hide }: DocumentViewPropsType) => {

  return (
    <FileViewer
      fileType={typea}
      filePath={filef}
    // errorComponent={CustomErrorComponent}
    // onError={this.onError}
    />
  );
  // onError(e: any) {
  //   logger.logError(e, 'error in file-viewer');
  // }
}

