const cookieStorageMock = (() => {

    Object.defineProperty(document, "doctype", {
      value: "<!DOCTYPE html>"
     });
     let setCookie = (v: boolean) => v;
     
     Object.defineProperty(window.navigator, 'cookieEnabled', (function (_value) {
       return {
         get: function _get() {
           return _value;
         },
         set: function _set(v: boolean) {
         _value = v;
         },
         configurable: true
       };
     })(window.navigator.cookieEnabled));
     
     setCookie = (v) => v;
     
     Object.defineProperty(window.document, 'cookie', (function (_value) {
       return {
         get: function _get() {
           return _value;
         },
         set: function _set(v: boolean) {
           _value = setCookie(v);
         },
         configurable: true
       };
     })(window.navigator.cookieEnabled));
  })()
  
    //  Rainmaker2Token
  
    export const MockCookies = () => {
      cookieStorageMock;
    };
    
    
    