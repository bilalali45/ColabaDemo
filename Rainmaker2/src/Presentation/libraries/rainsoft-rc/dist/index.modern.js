import React, { useState, useCallback, useEffect, Fragment } from 'react';
import Dropdown from 'react-bootstrap/Dropdown';
import FileViewer from 'react-file-viewer';
import printJS from 'print-js';
import { TransformWrapper, TransformComponent } from 'react-zoom-pan-pinch';

var HeaderMenu = function HeaderMenu(_ref) {
  var options = _ref.options,
      name = _ref.name;
  return React.createElement(Dropdown, {
    className: "userdropdown"
  }, React.createElement(Dropdown.Toggle, {
    id: "dname",
    className: "d-name d-none d-sm-block",
    as: "a"
  }, "Hello, ", name), React.createElement(Dropdown.Toggle, {
    id: "dropdownMenuButton",
    className: "hd-shorname",
    as: "span"
  }), React.createElement(Dropdown.Menu, null, options.map(function (item) {
    return React.createElement(Dropdown.Item, {
      key: item.name,
      onClick: function onClick(e) {
        return item.callback(e);
      }
    }, item.name);
  })));
};

var RainsoftRcHeader = function RainsoftRcHeader(_ref) {
  var logoSrc = _ref.logoSrc,
      displayName = _ref.displayName,
      options = _ref.options;
  return React.createElement("header", {
    className: "header-main"
  }, React.createElement("div", {
    className: "container-fluid"
  }, React.createElement("div", {
    className: "row"
  }, React.createElement("div", {
    className: "col-12"
  }, React.createElement("nav", {
    className: "navbar navbar-default"
  }, React.createElement("div", {
    className: "navbar-header h-logo"
  }, React.createElement("a", {
    className: "logo-link",
    href: "/"
  }, React.createElement("img", {
    alt: "",
    src: logoSrc,
    className: "d-none d-sm-block"
  }))), React.createElement("div", {
    className: "s-account pull-right",
    "data-private": true
  }, React.createElement(HeaderMenu, {
    options: options,
    name: displayName
  })))))));
};

var RainsoftRcFooter = function RainsoftRcFooter(_ref) {
  var content = _ref.content;
  return React.createElement("section", null, React.createElement("footer", {
    className: "mainfooter"
  }, React.createElement("div", {
    className: "container"
  }, React.createElement("div", {
    className: "row"
  }, React.createElement("div", {
    className: "col-12 text-center"
  }, content)))));
};

var SVGprint = function SVGprint() {
  return React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    width: "19.364",
    height: "20.705",
    viewBox: "0 0 19.364 20.705"
  }, React.createElement("path", {
    id: "Path_497",
    "data-name": "Path 497",
    d: "M35.272,4.459h-1.25l-1.4-2.247a.592.592,0,0,0-.5-.278H30.5V.592A.592.592,0,0,0,29.9,0H22.959a.592.592,0,0,0-.592.592V1.934H20.749a.592.592,0,0,0-.5.278l-1.4,2.247h-1.25A.592.592,0,0,0,17,5.051v10.1a.592.592,0,0,0,.592.592H21.42v3.867a.592.592,0,0,0,.592.592h8.84a.592.592,0,0,0,.592-.592V15.746h3.828a.592.592,0,0,0,.592-.592V5.051A.592.592,0,0,0,35.272,4.459ZM31.786,3.118l.839,1.342H30.5V3.118ZM23.551,1.184h5.762V4.459H23.551ZM21.077,3.118h1.29V4.459H20.238Zm9.183,15.9H22.6v-5.8H30.26Zm4.42-4.459H31.444V13.22h.671a.592.592,0,1,0,0-1.184H20.749a.592.592,0,1,0,0,1.184h.671v1.342H18.184V5.643h16.5Z",
    transform: "translate(-16.75 0.25)",
    fill: "#fff",
    stroke: "#000",
    "stroke-width": "0.5"
  }));
};
var SVGdownload = function SVGdownload() {
  return React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    width: "17.024",
    height: "17.024",
    viewBox: "0 0 17.024 17.024"
  }, React.createElement("path", {
    id: "download",
    d: "M12.985,8.825,8.112,13.7,3.239,8.825l.9-.9,3.343,3.343V0H8.746V11.271l3.343-3.343Zm3.239,6.131H0v1.267H16.224Zm0,0",
    transform: "translate(0.4 0.4)",
    fill: "#000",
    stroke: "#000",
    "stroke-width": "0.8"
  }));
};
var SVGclose = function SVGclose() {
  return React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    width: "14.016",
    height: "13.969",
    viewBox: "0 0 14.016 13.969"
  }, React.createElement("path", {
    id: "Path_499",
    "data-name": "Path 499",
    d: "M7.008-14.578,1.383-9,7.008-3.422,5.6-2.016-.023-7.594-5.6-2.016-7.008-3.422-1.43-9l-5.578-5.578L-5.6-15.984l5.578,5.578L5.6-15.984Z",
    transform: "translate(7.008 15.984)",
    fill: "#000"
  }));
};
var SVGfullScreen = function SVGfullScreen() {
  return React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    width: "13.665",
    height: "13.665",
    viewBox: "0 0 13.665 13.665"
  }, React.createElement("path", {
    id: "Fit_To_Width-595b40b65ba036ed117d1c56",
    "data-name": "Fit To Width-595b40b65ba036ed117d1c56",
    d: "M4,4V9.124H5.139V5.957L8.146,8.964l.818-.818L5.957,5.139H9.124V4H4Zm8.541,0V5.139h3.167L12.7,8.146l.818.818,3.007-3.007V9.124h1.139V4H12.541ZM4,12.541v5.124H9.124V16.526H5.957l3.007-3.007L8.146,12.7,5.139,15.708V12.541Zm12.526,0v3.167L13.519,12.7l-.818.818,3.007,3.007H12.541v1.139h5.124V12.541Z",
    transform: "translate(-4 -4)",
    fill: "#7e829e"
  }));
};

var Loader = function Loader(_ref) {
  var width = _ref.width,
      height = _ref.height,
      containerHeight = _ref.containerHeight,
      marginBottom = _ref.marginBottom,
      hasBG = _ref.hasBG;
  return React.createElement("div", {
    className: "row loader-row",
    style: {
      marginBottom: marginBottom
    }
  }, React.createElement("div", {
    className: "container"
  }, React.createElement("div", {
    className: hasBG ? "loader bg" : "loader",
    style: {
      minHeight: containerHeight
    }
  }, React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    width: width ? width : "40px",
    height: height ? height : "40px",
    viewBox: "0 0 100 100",
    preserveAspectRatio: "xMidYMid"
  }, React.createElement("circle", {
    cx: "50",
    cy: "50",
    fill: "none",
    stroke: "#9d9d9d",
    "stroke-width": "2",
    r: "44",
    "stroke-dasharray": "207.34511513692632 71.11503837897544",
    transform: "rotate(262.673 50 50)"
  }, React.createElement("animateTransform", {
    attributeName: "transform",
    type: "rotate",
    repeatCount: "indefinite",
    dur: "2.2222222222222223s",
    values: "0 50 50;360 50 50",
    keyTimes: "0;1"
  }))))));
};

var DocumentView = function DocumentView(_ref) {
  var id = _ref.id,
      requestId = _ref.requestId,
      docId = _ref.docId,
      fileId = _ref.fileId,
      clientName = _ref.clientName,
      hideViewer = _ref.hideViewer,
      file = _ref.file,
      blobData = _ref.blobData,
      submittedDocumentCallBack = _ref.submittedDocumentCallBack,
      _ref$loading = _ref.loading,
      loading = _ref$loading === void 0 ? false : _ref$loading,
      _ref$showCloseBtn = _ref.showCloseBtn,
      showCloseBtn = _ref$showCloseBtn === void 0 ? true : _ref$showCloseBtn,
      isMobile = _ref.isMobile;

  var _useState = useState({
    blob: new Blob(),
    filePath: '',
    fileType: ''
  }),
      documentParams = _useState[0],
      setDocumentParams = _useState[1];

  var _useState2 = useState(true),
      pan = _useState2[0],
      setPan = _useState2[1];

  var _useState3 = useState(1),
      scale = _useState3[0],
      setScale = _useState3[1];

  var getDocumentForViewBeforeUpload = useCallback(function () {
    var fileBlob = new Blob([file], {
      type: 'image/png'
    });
    var filePath = URL.createObjectURL(fileBlob);
    file && setDocumentParams({
      blob: file,
      filePath: filePath,
      fileType: file.type.replace('image/', '').replace('application/', '')
    });
  }, [file]);
  useEffect(function () {
    setPan(pan);
  }, [pan]);

  var enabalePan = function enabalePan(e) {
    setScale(e.scale);
    return e.scale;
  };

  useEffect(function () {
    getPanValue(scale);
  }, [scale]);
  var getSubmittedDocumentForView = useCallback(function () {
    try {
      try {
        if (submittedDocumentCallBack) {
          submittedDocumentCallBack(id, requestId, docId, fileId);
        }
      } catch (error) {
        console.log(error);
        alert('Something went wrong. Please try again later.');
        hideViewer({});
      }

      return Promise.resolve();
    } catch (e) {
      return Promise.reject(e);
    }
  }, [docId, fileId, id, requestId]);
  var printDocument = useCallback(function () {
    var filePath = documentParams.filePath,
        fileType = documentParams.fileType;
    var type = ['jpeg', 'jpg', 'png'].includes(fileType) ? 'image' : 'pdf';
    printJS({
      printable: filePath,
      type: type
    });
  }, [documentParams.filePath, documentParams.fileType]);

  var downloadFile = function downloadFile() {
    var temporaryDownloadLink;
    temporaryDownloadLink = document.createElement('a');
    temporaryDownloadLink.href = documentParams.filePath;
    temporaryDownloadLink.setAttribute('download', clientName);
    temporaryDownloadLink.click();
  };

  var onEscapeKeyPressed = useCallback(function (event) {
    if (event.keyCode === 27) {
      hideViewer({});
    }
  }, [hideViewer]);
  useEffect(function () {
    if (file) {
      getDocumentForViewBeforeUpload();
    } else {
      if (!blobData) {
        getSubmittedDocumentForView();
      } else {
        var fileType = blobData.headers['content-type'];
        var documentBlob = new Blob([blobData.data], {
          type: fileType
        });
        var filePath = URL.createObjectURL(documentBlob);
        setDocumentParams({
          blob: documentBlob,
          filePath: filePath,
          fileType: fileType.replace('image/', '').replace('application/', '')
        });
      }
    }
  }, [getSubmittedDocumentForView, getDocumentForViewBeforeUpload, file, blobData]);
  useEffect(function () {
    window.addEventListener('keydown', onEscapeKeyPressed, false);
    return function () {
      window.removeEventListener('keydown', onEscapeKeyPressed, false);
    };
  }, [onEscapeKeyPressed]);

  var getPanValue = function getPanValue(e) {
    if (e > 1) {
      setPan(false);
      console.log(e);
    } else {
      setPan(true);
    }

    console.log(pan);
  };

  return React.createElement("div", {
    className: 'document-view',
    id: 'screen'
  }, React.createElement("div", {
    "data-testid": "file-viewer-header",
    className: 'document-view--header'
  }, React.createElement("div", {
    className: 'document-view--header---options'
  }, React.createElement("ul", null, !!documentParams.filePath && React.createElement(Fragment, null, React.createElement("li", null, React.createElement("button", {
    className: 'document-view--button',
    onClick: printDocument
  }, React.createElement(SVGprint, null))), React.createElement("li", null, React.createElement("button", {
    className: 'document-view--button',
    onClick: downloadFile
  }, React.createElement(SVGdownload, null)))), showCloseBtn && React.createElement("li", null, React.createElement("button", {
    className: 'document-view--button',
    onClick: function onClick() {
      return hideViewer(false);
    }
  }, React.createElement(SVGclose, null))))), React.createElement("span", {
    className: 'document-view--header---title'
  }, clientName), React.createElement("div", {
    className: 'document-view--header---controls'
  }, React.createElement("ul", null, React.createElement("li", null, React.createElement("button", {
    className: 'document-view--arrow-button'
  }, React.createElement("em", {
    className: 'zmdi '
  }))), React.createElement("li", null, React.createElement("span", {
    className: 'document-view--counts'
  }, React.createElement("input", {
    type: 'text',
    size: 4,
    value: ''
  }))), React.createElement("li", null, React.createElement("button", {
    className: 'document-view--arrow-button'
  }, React.createElement("em", {
    className: 'zmdi '
  })))))), React.createElement("div", {
    className: 'zoomview-wraper'
  }, (isMobile === null || isMobile === void 0 ? void 0 : isMobile.value) ? React.createElement(TransformWrapper, {
    defaultScale: 1,
    wheel: {
      wheelEnabled: false,
      touchPadEnabled: true
    },
    pan: {
      disabled: pan,
      animationTime: 0
    },
    zoomIn: {
      animation: false,
      animationTime: 0
    },
    zoomOut: {
      animation: false,
      animationTime: 0
    },
    reset: {
      animation: false,
      animationTime: 0
    },
    doubleClick: {
      disabled: true
    },
    scalePadding: {
      animationTime: 0
    },
    onZoomChange: function onZoomChange(e) {
      enabalePan(e);
    }
  }, function (_ref2) {
    var zoomIn = _ref2.zoomIn,
        zoomOut = _ref2.zoomOut,
        resetTransform = _ref2.resetTransform;
    return React.createElement("div", {
      className: 'wrap-zoom-view'
    }, React.createElement(TransformComponent, null, React.createElement("div", {
      className: 'document-view--body'
    }, !!documentParams.filePath && !loading ? React.createElement(FileViewer, {
      fileType: documentParams.fileType,
      filePath: documentParams.filePath
    }) : React.createElement(Loader, {
      height: '94vh'
    }))), React.createElement("div", {
      className: 'document-view--floating-options'
    }, React.createElement("ul", null, React.createElement("li", null, React.createElement("button", {
      className: 'button-float',
      onClick: zoomIn
    }, React.createElement("em", {
      className: 'zmdi zmdi-plus'
    }))), React.createElement("li", null, React.createElement("button", {
      className: 'button-float',
      onClick: zoomOut
    }, React.createElement("em", {
      className: 'zmdi zmdi-minus'
    }))), React.createElement("li", null, React.createElement("button", {
      className: 'button-float',
      onClick: resetTransform
    }, React.createElement(SVGfullScreen, null))))));
  }) : React.createElement(TransformWrapper, {
    defaultScale: 1,
    wheel: {
      wheelEnabled: false
    }
  }, function (_ref3) {
    var zoomIn = _ref3.zoomIn,
        zoomOut = _ref3.zoomOut,
        resetTransform = _ref3.resetTransform;
    return React.createElement("div", null, React.createElement(TransformComponent, null, React.createElement("div", {
      className: "document-view--body"
    }, !!documentParams.filePath ? React.createElement(FileViewer, {
      fileType: documentParams.fileType,
      filePath: documentParams.filePath
    }) : React.createElement(Loader, {
      height: "94vh"
    }))), React.createElement("div", {
      className: "document-view--floating-options"
    }, React.createElement("ul", null, React.createElement("li", null, React.createElement("button", {
      className: "button-float",
      onClick: zoomIn
    }, React.createElement("em", {
      className: "zmdi zmdi-plus"
    }))), React.createElement("li", null, React.createElement("button", {
      className: "button-float",
      onClick: zoomOut
    }, React.createElement("em", {
      className: "zmdi zmdi-minus"
    }))), React.createElement("li", null, React.createElement("button", {
      className: "button-float",
      onClick: resetTransform
    }, React.createElement(SVGfullScreen, null))))));
  })));
};

export { DocumentView, RainsoftRcFooter, RainsoftRcHeader };
//# sourceMappingURL=index.modern.js.map
