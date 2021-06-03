import React, { useRef, useEffect } from 'react'
import PSPDFKit from "pspdfkit";
import { SVGclose, SVGdownload } from "../Assets/SVG";

let _instance: any = null;



export const PSPDFKitWebViewer = ({ documentURL, appBaseURL, licenseKey, clientName, closeViewer }) => {
    const viewerRef = useRef<HTMLDivElement>(null);
    useEffect(() => {
        load();
    }, [document])

    useEffect(() => {
        return () => {
            unload();
        }
    }, []);
    

    const toolbarItems = PSPDFKit.defaultToolbarItems.filter(item => {
        return /\b(sidebar-bookmarks|sidebar-thumbnails|zoom-in|zoom-out|pager|pan|zoom-mode|marquee-zoom|search|print)\b/.test(
            item.type
        );
    });

    const downloadButton: any = {
        type: "custom",
        id: "download-pdf",
        icon: `<svg data-testid="SVGdownload" className="SVGdownload" xmlns="http://www.w3.org/2000/svg" width="17.024" height="17.024" viewBox="0 0 17.024 17.024" >
        <path id="download" d="M12.985,8.825,8.112,13.7,3.239,8.825l.9-.9,3.343,3.343V0H8.746V11.271l3.343-3.343Zm3.239,6.131H0v1.267H16.224Zm0,0" transform="translate(1 0.4)" fill="#8e8e8e" stroke="#8e8e8e" strokeWidth="0.4" />
      </svg>`,
        title: "Download",
        onPress: () => {
            _instance.exportPDF().then(buffer => {
                const blob = new Blob([buffer], { type: "application/pdf" });
                const fileName = "document.pdf";
                if (window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveOrOpenBlob(blob, fileName);
                } else {
                    const objectUrl = window.URL.createObjectURL(blob);
                    const a: any = document.createElement("a");
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

    const titleText: any = {
        type: "custom",
        title: clientName,
        id:"titleText",

    };

    const items = PSPDFKit.defaultToolbarItems;
    if (items?.length === 30) {
        // items.push(downloadButton);
        // Add the download button to the toolbar.
    }

    const rightSpacer = {
        type: "custom",
        title: clientName,
        id: 'rightSpacer'
    }

    const closeIcon = {
        type: 'custom',
        onPress: closeViewer,
        icon: `<svg data-testid="SVGclose" className="SVGclose" xmlns="http://www.w3.org/2000/svg" width="14.016" height="13.969" viewBox="0 0 14.016 13.969">
        <path id="Path_499" data-name="Path 499" d="M7.008-14.578,1.383-9,7.008-3.422,5.6-2.016-.023-7.594-5.6-2.016-7.008-3.422-1.43-9l-5.578-5.578L-5.6-15.984l5.578,5.578L5.6-15.984Z" transform="translate(7.008 15.984)" fill="#fff"/>
      </svg>`,
      id: 'closeIcon'
        
    }

    const closeIconSpace = {
        type: 'custom',
        onPress: closeViewer,
        icon: `<svg data-testid="SVGclose" className="SVGclose" xmlns="http://www.w3.org/2000/svg" width="14.016" height="13.969" viewBox="0 0 14.016 13.969">
        <path id="Path_499" data-name="Path 499" d="M7.008-14.578,1.383-9,7.008-3.422,5.6-2.016-.023-7.594-5.6-2.016-7.008-3.422-1.43-9l-5.578-5.578L-5.6-15.984l5.578,5.578L5.6-15.984Z" transform="translate(7.008 15.984)" fill="#000"/>
      </svg>`,
        id: 'closeIconSpace'
    }


    const spacer = {
        type: 'spacer'
    }

    items.push(titleText);

    const load = async () => {
        let config: any = {
            document: await documentURL,
            container: viewerRef.current || '',
            licenseKey,
            baseUrl: appBaseURL,
            toolbarItems: [ titleText,closeIconSpace, spacer, ...toolbarItems, downloadButton, spacer, rightSpacer, closeIcon].map((item: any) => {
                if(item.id === 'rightSpacer') {
                    item.className = 'pspdkit-toolbar-item-right-spacer';
                    return item;
                }
                if (item.id === 'closeIconSpace') {
                    item.className = 'pspdkit-toolbar-item-left-close';
                    return item;
                }
                if(item.id === 'closeIcon') {
                    item.className = 'pspdkit-toolbar-item-right-close';
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
        _instance = await PSPDFKit.load(config);
    }

    const unload = () => {
        PSPDFKit.unload(_instance || viewerRef?.current)
        _instance = null;
    }

    return (
        <div
            ref={viewerRef}
            style={{ width: "100%", height: "100%", margin: 'auto' }}>

        </div>
    )
}
