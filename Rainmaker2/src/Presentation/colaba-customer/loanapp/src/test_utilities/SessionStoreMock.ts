const sessionStorageMock = (function () {
    let store: { [key: string]: string } = {
        ["rain-planner-users"]: JSON.stringify([
            {
                email: "kane@wood.com",
                password: "kane123",
                firstName: "john",
                lastName: "doe",
                todoList: [],
                selectedList: null,
            },
        ]),
    };
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

export const MockSessionStorage = () => {
    sessionStorageMock;
    Object.defineProperty(window, "sessionStorage", { value: sessionStorageMock });
    Object.defineProperty(window, "open", jest.fn());
};
