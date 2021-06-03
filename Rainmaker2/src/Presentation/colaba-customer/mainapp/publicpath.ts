console.log("before __webpack_public_path__", __webpack_public_path__)
console.log('window.location.pathname', window.location.pathname);

const basePath = !window.location.href.includes("localhost") ? window.location.pathname.toLowerCase().split('/app/')[0] + '/app/' : "/"
__webpack_public_path__ = basePath;
console.log("after __webpack_public_path__", __webpack_public_path__, basePath);

export default '';