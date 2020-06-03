import React, { useState } from 'react'

import { Document, Page, pdfjs } from 'react-pdf';
pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

type DocumentViewPropsType = { type: string, url: string, hide: Function }

export const DocumentView = ({ type, url, hide }: DocumentViewPropsType) => {
  const [numPages, setNumPages] = useState<number | null>(null);
  const [pageNumber, setPageNumber] = useState(1);

  const onDocumentLoadSuccess = ({ numPages }: any) => {
    console.log('numPages', numPages);
    setNumPages(numPages);
  }

  const handlePage = (e: any) => {
    if (e.target.id === 'next') {
      if (numPages && pageNumber < numPages) {
        setPageNumber(pageNumber + 1);
      }
    } else {
      if (pageNumber > 1) {
        setPageNumber(pageNumber - 1);
      }
    }
  }

  const renderPdfView = () => {
    return (
      <>
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
      </>
    )
  }

  const renderTextView = () => {
    return (
      <iframe src="https://docs.google.com/viewerng/viewer?url=http://localhost:5000/pdf/file-sample_100kB.doc"></iframe>
    )
  }

  const renderImageView = () => {
    return <img style={{width: "50%", height: "50%"}} src={url} alt="" />
  }

  const renderView = () => {
    switch (type) {
      case 'application/pdf':
        return renderPdfView();
      case 'application/msword':
        return renderTextView();
      case 'image/jpeg':
        return renderImageView();

      default:
        break;
    }
  }

  return (
    <div className="modal-container">
      <button onClick={() => hide()}>X</button>
      {renderView()}
      <div className="overlay"></div>
    </div>
  );
}