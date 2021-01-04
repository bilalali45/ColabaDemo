import React, { useRef, useEffect, useState } from 'react'
import PSPDFKit from "pspdfkit";

let _instance : any = null;

export const PSPDFKitWebViewer = ({ documentURL, appBaseURL, licenseKey, clientName } : any) => {
    console.log('values',{ documentURL, appBaseURL, licenseKey, clientName })
    const viewerRef = useRef<HTMLDivElement>(null);

    const toolbarItems = PSPDFKit.defaultToolbarItems.filter(item => {
        return /\b(sidebar-bookmarks|sidebar-thumbnails|zoom-in|zoom-out|pager|pan|zoom-mode|marquee-zoom|search|print)\b/.test(
          item.type
        );
      });

    const downloadButton : any = {
        type: "custom",
        id: "download-pdf",
        icon: `<svg data-testid="SVGdownload" className="SVGdownload" xmlns="http://www.w3.org/2000/svg" width="17.024" height="17.024" viewBox="0 0 17.024 17.024" >
        <path id="download" d="M12.985,8.825,8.112,13.7,3.239,8.825l.9-.9,3.343,3.343V0H8.746V11.271l3.343-3.343Zm3.239,6.131H0v1.267H16.224Zm0,0" transform="translate(1 0.4)" fill="#8e8e8e" stroke="#8e8e8e" strokeWidth="0.4" />
      </svg>`,
        title: "Download",
        onPress: () => {
            _instance.exportPDF().then((buffer: any) => {
                const blob = new Blob([buffer], { type: "application/pdf" });
                const fileName = `${clientName.split('.').slice(0, -1).join('.')}.pdf`;
                if (window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveOrOpenBlob(blob, fileName);
                } else {
                    const objectUrl = window.URL.createObjectURL(blob);
                    const a : any = document.createElement("a");
                    a.href = objectUrl;
                    a.style = "display: none";
                    a.download = fileName;
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(objectUrl);
                    document.body.removeChild(a);
                }
            });
        }
    };

    const spacer = {
        type: 'spacer'
    }

    const titleText: any = {
        type: "custom",
        title: clientName,
        id:'titleText'
    };

    const items = PSPDFKit.defaultToolbarItems;
    if(items?.length === 30) {
        items.push(downloadButton);
        // Add the download button to the toolbar.
    }

    useEffect(() => {
        const load = async () => {
            console.log('calling');
            if(documentURL && documentURL.byteLength){
            
            let config : any = {
                document: documentURL,
                container: viewerRef.current || '',
                licenseKey,
                baseUrl: appBaseURL,
                toolbarItems: [titleText, spacer, ...toolbarItems, downloadButton].map((item: any) => {
                    if(item.id === 'rightSpacer') {
                        item.className = 'pspdkit-toolbar-item-right-spacer';
                        return item;   
                    }
                    
                    if(item.id === 'titleText') {
                        item.className = 'pspdkit-toolbar-item-left-titleText';
                        return item;   
                    }

                    item.className = 'pspdkit-toolbar-item';
                    
                    return item;
                }),
                styleSheets: [`${appBaseURL}assets/css/pspdfkit-styles.css`],
                theme: PSPDFKit.Theme.DARK || ''
            }

            try {
                _instance = await PSPDFKit.load(config);
            } catch (error) {
                console.log('error', error)
            }
        }
        }

        load();

        return () => {
            if(_instance)
                PSPDFKit.unload(_instance)
            
            _instance = null;
        }
    }, [appBaseURL, documentURL, downloadButton, licenseKey, spacer, titleText, toolbarItems])

    return (
        <div
            ref={viewerRef}
            style={{width: "100%", height: "100%", position: 'absolute'}}>
            {/* style={{width: "50%", height: "100%", margin: 'auto'}}> */}

        </div>
    )
}
