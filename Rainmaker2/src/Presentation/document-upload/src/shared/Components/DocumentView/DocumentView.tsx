import React, { useState } from 'react'

import { Document, Page, pdfjs } from 'react-pdf';
pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

type DocumentViewPropsType = { url: string, hide: Function }

export const DocumentView = ({ url, hide }: DocumentViewPropsType) => {
  const [numPages, setNumPages] = useState<number | null>(null);
  const [pageNumber, setPageNumber] = useState(1);

  function onDocumentLoadSuccess({ numPages }: any) {
    console.log('numPages', numPages);
    setNumPages(numPages);
  }

  const handlePage = (e: any) => {
    if (e.target.id === 'next') {
      if(numPages && pageNumber < numPages) {
        setPageNumber(pageNumber + 1);
      }
    } else {
      if (pageNumber > 1) {
        setPageNumber(pageNumber - 1);
      }
    }
  }

  return (
    <div className="modal-container">
        <button onClick={() => hide()}>X</button>
      <div className="modal-content">
        <Document

          file={url}
          onLoadSuccess={onDocumentLoadSuccess}
        >
          <Page pageNumber={pageNumber} />
        </Document>
        <p>Page {pageNumber} of {numPages}</p>
      </div>
      <div className="page-controls">
        <button id="previous" onClick={handlePage}>Previous</button>
        <h1>{pageNumber}</h1>
        <button id="next" onClick={handlePage}>Next</button>
      </div>
      <div className="overlay"></div>
    </div>
  );
}