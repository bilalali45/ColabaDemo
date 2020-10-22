const localStorageMock = (function () {
    var store: { [key: string]: string } = {};
    return {
        getItem: function (key: string) {
            return store[key];
        },
        setItem: function (key: string, value: string) {
            store[key] = value.toString();
        },
        clear: function () {
            store = {};
        },
        removeItem: function (key: string) {
            delete store[key];
        },
    };
})();

export const MockLocalStorage = () => {
    localStorageMock;
    Object.defineProperty(window, "localStorage", { value: localStorageMock });
    Object.defineProperty(window, "open", jest.fn());
};
