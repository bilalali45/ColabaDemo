import React, { useState } from 'react'
import FileViewer from 'react-file-viewer';
import { Document, Page, pdfjs } from 'react-pdf';
import {SVGprint, SVGdownload, SVGclose, SVGfullScreen} from './../Assets/SVG';
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

export const DocumentView = () => {

  //let fullscreen = false;
  const [fullscreen, exitscreen] = useState(false);

  /* Get into full screen */
const GoInFullscreen = (element) => {
	if(element.requestFullscreen)
		element.requestFullscreen();
	else if(element.mozRequestFullScreen)
		element.mozRequestFullScreen();
	else if(element.webkitRequestFullscreen)
		element.webkitRequestFullscreen();
	else if(element.msRequestFullscreen)
		element.msRequestFullscreen();
}

/* Get out of full screen */
const GoOutFullscreen = () => {
	if(document.exitFullscreen)
		document.exitFullscreen();
	else if(document.mozCancelFullScreen)
		document.mozCancelFullScreen();
	else if(document.webkitExitFullscreen)
		document.webkitExitFullscreen();
	else if(document.msExitFullscreen)
		document.msExitFullscreen();
}

/* Is currently in full screen or not */
const IsFullScreenCurrently = () => {
	var full_screen_element = document.fullscreenElement || document.webkitFullscreenElement || document.mozFullScreenElement || document.msFullscreenElement || null;
	// If no element is in full-screen
	if(full_screen_element === null)
		return false;
	else
		return true;
}
  
  const updateState = (e) => {
    console.log(fullscreen);
    //exitscreen('false')

    if(fullscreen == false){
      // var ev = new Event('keypress');
      // ev.which = 122; // Character F11 equivalent.
      // ev.altKey=false;
      // ev.ctrlKey=false;
      // ev.shiftKey=false;
      // ev.metaKey=false;
      // ev.bubbles=true;
      //document.dispatchEvent(e);
      
    }

    if(IsFullScreenCurrently())
      GoOutFullscreen();
    else
      GoInFullscreen();
    
  }


  return (
    <div className="document-view" id="screen">
      <div className="document-view--header">

      <div className="document-view--header---options">
          <ul>
            <li>
              <button className="document-view--button"><SVGprint /></button>
            </li>
            <li>
              <button className="document-view--button"><SVGdownload /></button>
            </li>
            <li>
              <button className="document-view--button"><SVGclose /></button>
            </li>
          </ul>
        </div>

        <span className="document-view--header---title">Bank-Statement-Jul-to-July-2020.pdf</span>

        <div className="document-view--header---controls">
          <ul>
            <li>
              <button className="document-view--arrow-button"><em className="zmdi zmdi-chevron-left"></em></button>
            </li>
            <li>
              <span className="document-view--counts"><input type="text" size="4" value="1/2"/></span>
            </li>
            <li>
              <button className="document-view--arrow-button"><em className="zmdi zmdi-chevron-right"></em></button>
            </li>
          </ul>
        </div>

        

      </div>
      <div className="document-view--body">
          <FileViewer
          fileType={typea}
          filePath={filef}
        // errorComponent={CustomErrorComponent}
        // onError={this.onError}
        />
      </div>    
      <div className="document-view--floating-options">
        <ul>
          <li>
            <button className="button-float"><em className="zmdi zmdi-plus"></em></button>
          </li>
          <li>
             <button className="button-float"><em className="zmdi zmdi-minus"></em></button>
          </li>
          <li>
            <button className="button-float" onClick={updateState}><SVGfullScreen /></button>
          </li>
        </ul>
      </div>
    </div>
  );
  // onError(e: any) {
  //   logger.logError(e, 'error in file-viewer');
  // }
}

