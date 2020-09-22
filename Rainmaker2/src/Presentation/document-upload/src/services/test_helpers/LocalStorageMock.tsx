const localStorageMock = (function () {
  var store: { [key: string]: string } = {
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

export const MockLocalStorage = () => {
  localStorageMock;
  Object.defineProperty(window, "localStorage", { value: localStorageMock });
};
